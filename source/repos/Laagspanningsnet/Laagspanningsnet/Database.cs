﻿/* Communicatie met de mySQL database server loopt via deze klasse.
 * 
 * Aanpassingen :
 *  - 20180317 :
 *      - Parameters.AddWithValue("@para", .... gebruikt : sql-injection
 *  - 20180409 :
 *      - search aangepast : aansluitingen-machines + aansluitingen-aansluitpunten
 */

using System;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Laagspanningsnet
{
    public class Database
    {
        // Gegevens nodig om met de MySqlDatabase verbinding te maken.

        // Mysql via localhost :
        ///*
        private const string Server = "localhost";
        private const string MySqlDatabase = "laagspanningsnet";
        private const string User = "root";
        private const string Password = "root";
        //*/

        // MySql via FreeBSD Server alix2d13
        /*
        private const Server = "192.168.1.17";
        private const string MySqlDatabase = "laagspanningsnet";
        private const string User = "jan";
        private const string Password = ".ShlorcunJad9";
        */

        private const string Connectiestring = "SERVER=" + Server +
            ";DATABASE=" + MySqlDatabase + ";UID=" + User +
            ";PASSWORD=" + Password + ";";
        private static readonly MySqlConnection MySqlConnection = new MySqlConnection(Connectiestring);
        private MySqlCommand _mySqlCommand;
        private MySqlDataAdapter _mySqlDataAdapter;
        
        /* Openen van de connectie met de MySqlDatabase.
         * Toont een MessageBox op het scherm als er een probleem is.
         * 
         * RETURN : false = mislukt
         *          true  = openen gelukt
         */
        private static bool Open()
        {
            try
            {
                MySqlConnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Dit kan evt. nog verbeterd worden door "ex" niet af, een switch case te gebruiken
                // voor de meest gangbare fouten.
                MessageBox.Show("Kan geen verbinding maken met MySqlDatabase\n\n" + ex.Message);
                return false;
            }
        }

        /* Sluiten van de connectie met de MySqlDatabase.
         * Toont een MessageBox op het scherm als er een probleem is.
         * 
         * RETURN : false = mislukt
         *          true  = sluiten gelukt
         */
        private static bool Close()
        {
            try
            {
                MySqlConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // Dit kan evt. nog verbeterd worden door "ex" niet af te drukken en een switch case te gebruiken
                // voor de meest gangbare fouten.
                MessageBox.Show("Kan geen verbinding maken met MySqlDatabase\n\n" + ex.Message);
                return false;
            }
        }

        /* Ophalen van de Machine Omschrijving
         * 
         * RETURN : String omschrijving
         */
        public string GetMachineOmschrijving(string machine)
        {
            const string query = "select M_omschrijving from laagspanningsnet.machines WHERE M_id = @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", machine);
            return (GetString());
        }

        /* Ophalen van de Machine Locatie
         * 
         * RETURN : string locatie
         */
        public string GetMachineLocatie(string machine)
        {
            const string query = "select M_locatie from laagspanningsnet.machines WHERE M_id = @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", machine);
            return (GetString());
        }

        /* Ophalen van de Aansluitpunt Locatie
         * 
         * RETURN string locatie
         */
        public string GetAansluitpuntLocatie(string aansluitpunt)
        {
            const string query = "select AP_locatie from laagspanningsnet.aansluitpunten WHERE AP_id = @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", aansluitpunt);
            return (GetString());
        }

        /* Ophalen van de Aansluitpunt Voeding
         *
         * RETURN : string voeding , = "-" als er geen voeding is gevonden
         */
        public string GetVoeding(string aansluitpunt)
        {
            // Haal het aansluitpunt op waar de voeding van komt
            string query = "select AP_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", aansluitpunt);
            string voedingAp = GetString();
            // Haal de aansluitng op waar de voeding van komt
            query = "select A_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", aansluitpunt);
            string voedingA = GetString();
            return (voedingAp + " - " + voedingA).Trim();
        }

        /* Ophalen van de Aansluitpunt Voedingskabel
         * 
         * RETURN : string voedingskabel , = "-" als er geen voedingskabel is gevonden
         */
        public string GetKabel(string aansluitpunt)
        {
            // Haal het aansluitpunt op waar de voeding van komt
            string query = "select Kabeltype from laagspanningsnet.aansluitingen WHERE Naar_AP_id = @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", aansluitpunt);
            string kabeltype = GetString();
            // Haal de aansluitng op waar de voeding van komt
            query = "select Kabelsectie from laagspanningsnet.aansluitingen WHERE Naar_AP_id = @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", aansluitpunt);
            string kabelsectie = GetString();
            return (kabeltype + " - " + kabelsectie).Trim();
        }

        /* Ophalen van het Aansluitpunt zijn stroomtoevoer
         * 
         * RETURN : string stroom , = "-" als er geen stroom is gevonden
         */
        public string GetStroom(string aansluitpunt)
        {
            // Haal het aansluitpunt op waar de voeding van komt
            _mySqlCommand = new MySqlCommand("select Stroom from laagspanningsnet.aansluitingen WHERE Naar_AP_id = @para;", MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", aansluitpunt);
            string stroom = GetString();
            stroom = stroom.Equals("") ? "-" : (stroom + "A").Trim();
            return stroom;
        }

        /* Opvragen van alle aanwezige Transormatoren in het bedrijf.
         * We geven de transformatoren terug als aansluitpunten (Naar_AP_id) die zijn aangesloten op het aansluitpunt "Hoogspanning" 
         * om de code in Hoofdscherm zo uniform mogelijk te houden.
         *
         * RETURN : DataSet met gegevens alle transfo's
         */
        public DataSet GetTransfos()
        {
            const string query = "SELECT " +
                "'Hoogspanning' as A_id, " +
                "'Hoogspanning' as AP_id, " +
                "AP_id AS Naar_AP_id, " +
                "NULL AS Naar_M_ID, " +
                "NULL AS Omschrijving, " +
                "NULL AS Kabeltype, " +
                "NULL AS Kabelsectie, " +
                "NULL AS Stroom, " +
                "3 AS Polen " +
                "FROM laagspanningsnet.aansluitpunten WHERE AP_id like 'T%';";
            _mySqlDataAdapter = new MySqlDataAdapter(query, MySqlConnection);
            return GetDataSet();
        }

        /* Opvragen van alle aansluitingen voor een bepaald aansluitpunt.
         * 
         * RETURN : DataSet met gegevens alle aansluitingen van een aansluitpunt
         */
        public DataSet GetAansluitingen(string aansluitpunt)
        {
            const string query = "select * from laagspanningsnet.aansluitingen WHERE AP_id=@para;";
            _mySqlDataAdapter = new MySqlDataAdapter(query, MySqlConnection);
            _mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@para", aansluitpunt);
            return GetDataSet();
        }

        /* Opvragen zoek-resultaten.
         * 
         * RETURN : DataSet met alle zoekresultaten
         */
        public DataSet GetSearch(string search)
        {
            /* MySQL kent geen FULL JOIN, dus we doen een LEFT JOIN + RIGHT JOIN + UNION
             * We doen dit voor de tables:
             *  - aansluitingen FULL JOIN machines
             *  - aansluitingen FULL JOIN aansluitpunten
             */
            search = "%" + search + "%";
            const string what = "aansluitingen.AP_id, A_id, Kabeltype, Kabelsectie, Stroom, Polen, Omschrijving, ";
            const string whatM = what + "Naar_AP_id, machines.M_id AS Naar_M_id";
            const string whatA = what + "aansluitpunten.AP_id AS Naar_AP_id, Naar_M_id";
            const string whereM = "WHERE Naar_AP_id LIKE @para " + 
                                  "OR M_id LIKE @para " + 
                                  "OR Omschrijving LIKE @para " + 
                                  "OR M_omschrijving LIKE @para ";
            const string whereA = "WHERE aansluitpunten.AP_id LIKE @para " + 
                                  "OR aansluitingen.AP_id LIKE @para " + 
                                  "OR Omschrijving LIKE @para ";
            const string query = "SELECT " + whatM + " FROM laagspanningsnet.aansluitingen " +
                                 "LEFT JOIN laagspanningsnet.machines ON Naar_M_id = M_id " +
                                 whereM +
                                 "UNION " +
                                 "SELECT " + whatM + " FROM laagspanningsnet.aansluitingen " +
                                 "RIGHT JOIN laagspanningsnet.machines ON Naar_M_id = M_id " +
                                 whereM +
                                 "UNION " +
                                 "SELECT " + whatA + " FROM laagspanningsnet.aansluitingen " +
                                 "LEFT JOIN laagspanningsnet.aansluitpunten ON Naar_AP_id = aansluitpunten.AP_id " +
                                 whereA +
                                 "UNION " +
                                 "SELECT " + whatA + " FROM laagspanningsnet.aansluitingen " +
                                 "RIGHT JOIN laagspanningsnet.aansluitpunten ON Naar_AP_id = aansluitpunten.AP_id " +
                                 whereA + ";";
            _mySqlDataAdapter = new MySqlDataAdapter(query, MySqlConnection);
            _mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@para", search);
            return GetDataSet();
        }

        /* Data van alle aansluitingen van een aansluitpunt opslaan in de MySqlDatabase.
         * 
         * RETURN : false = mislukt
         *          true  = bewaren gelukt
         */
        public bool SetAansluitingen(string aansluitpunt, DataSet dsDatabase)
        {
            bool _return = true;    // Bijhouden of er fouten optreden, we gaan er van uit dat alles goed zal verlopen

            // DELETE alle aansluitingen van dit aansluitpunt
            string nonQuery = "DELETE FROM laagspanningsnet.aansluitingen WHERE AP_id = @para ;";
            _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", aansluitpunt);
            if (!NonQueryCommon())
            {
                _return = false;
            }
            
            foreach (DataRow dataRow in dsDatabase.Tables["aansluitingen"].Rows)
            {
                // Steek de gegevens van deze row in losse var's
                var dbApId = dataRow["AP_id"];
                var dbAId = dataRow["A_id"];
                var dbNaarApId = dataRow["Naar_AP_id"];
                var dbNaarMId = dataRow["Naar_M_id"];
                var dbOmschrijving = dataRow["Omschrijving"];
                var dbKabeltype = dataRow["Kabeltype"];
                var dbKabelsectie = dataRow["Kabelsectie"];
                var dbStroom = dataRow["Stroom"];
                var dbPolen = dataRow["Polen"];
                
                // INSERT de gegevens in de MySqlDatabase
                nonQuery = "INSERT INTO laagspanningsnet.aansluitingen " + 
                    "(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , `Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`)" +
                    "VALUES(" +
                    " @para1 , " +
                    " @para2 , " +
                    " @para3 , " +
                    " @para4 , " +
                    " @para5 , " +
                    " @para6 , " +
                    " @para7 , " +
                    " @para8 , " +
                    " @para9 ); ";
                _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
                _mySqlCommand.Parameters.AddWithValue("@para1", dbApId);
                _mySqlCommand.Parameters.AddWithValue("@para2", dbAId);
                _mySqlCommand.Parameters.AddWithValue("@para3", dbNaarApId);
                _mySqlCommand.Parameters.AddWithValue("@para4", dbNaarMId);
                _mySqlCommand.Parameters.AddWithValue("@para5", dbOmschrijving);
                _mySqlCommand.Parameters.AddWithValue("@para6", dbKabeltype);
                _mySqlCommand.Parameters.AddWithValue("@para7", dbKabelsectie);
                _mySqlCommand.Parameters.AddWithValue("@para8", dbStroom);
                _mySqlCommand.Parameters.AddWithValue("@para9", dbPolen);
                if (!NonQueryCommon())
                {
                    _return = false;
                }
            }
            return _return;
        }

        /* Ophalen van een string uit de MySqlDatabase
         * private gemeenschappelijke routine 
         * 
         * RETURN : String data
         */
        private string GetString()
        {
            string _return = "";
            if (!Open())
            {
                return _return;
            }
            try
            {
                var tmp = _mySqlCommand.ExecuteScalar();
                if (tmp != null)
                {
                    _return = tmp.ToString();          // omzetten van var naar String, geen cast gebruiken, want var kan ook een int zijn.
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
            return _return;
        }

        /* Ophalen van een DataSet uit de MySqlDatabase
         * private gemeenschappelijke routine.
         *
         * RETURN : DataSet data
         */
        private DataSet GetDataSet()
        {
            DataSet dataSet = new DataSet();
            if (!Open())
            {
                return dataSet;
            }
            try
            { 
                _mySqlDataAdapter.Fill(dataSet);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
            return dataSet;
        }

        /*  Gemeenschappelijke code voor Insert/Update/Delete (ExecuteNonQuery()).
         * 
         *  RETURN : false/true : mislukt/gelukt 
         */
        private bool NonQueryCommon()
        {
            if (!Open())
            {
                return false;
            }
            try
            { 
                _mySqlCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                Close();
                return false;
            }
            return Close();
        }
        
        /* Haal een lijst op van alle machines die in de machine table aanwezig zijn
         * 
         * RETURN: List<String> met alle machines
         */
        public BindingList<String> GetMachines()
        {
            return GetMachines(false);
        }

        /* Haal een lijst op van alle aansluitpunten die in de aansluitpunt table aanwezig zijn
         * 
         * RETURN: List<String> met alle aansluitpunten
         */
        public BindingList<String> GetAansluitpunten()
        {
            return GetAansluitpunten(false);
        }

        /* Haal een lijst op van alle machines die in de machine table aanwezig zijn 
         * 
         * Naargelang _notconnected = true/false worden enkel de niet aangesloten machines geRETURNed
         * 
         * RETURN: List<String> met alle machines
         */
        public BindingList<string> GetMachines(bool notConnected)
        { 
            string query = "SELECT M_id FROM laagspanningsnet.machines ";
            if (notConnected)
            {
                query = query + "LEFT JOIN laagspanningsnet.aansluitingen ON M_id = Naar_M_id WHERE Naar_M_id IS NULL";
            }
            query = query + ";";
            _mySqlDataAdapter = new MySqlDataAdapter(query, MySqlConnection);
            DataSet dataSet = GetDataSet();
            
            // Zet DataSet om naar een List
            BindingList<string> convert = new BindingList<string>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                convert.Add((String)dataRow["M_id"]);
            }
            return convert;
        }
        
        /* Haal een lijst op van alle aansluitpunten die in de aansluitpunt table aanwezig zijn 
         * 
         * Naargelang _notconnected = true/false worden enkel de niet aangesloten aansluitpunten geRETURNed
         * 
         * RETURN: List<String> met alle aansluitpunten
         */
        public BindingList<String> GetAansluitpunten(bool notConnected)
        {
            string query = "SELECT aansluitpunten.AP_id FROM laagspanningsnet.aansluitpunten";
            if (notConnected)
            {
                /* Om na te gaan of een aansluitpunt NIET aangesloten is, moeten er twee zaken gecontroleerd worden.
                 * 1. Krijgt het aansluitpunt voeding van een ander aansluitpunt?
                 *      SELECT aansluitpunten.AP_id FROM laagspanningsnet.aansluitpunten 
                 *          LEFT JOIN laagspanningsnet.aansluitingen ON aansluitpunten.AP_id = Naar_AP_id
                 *          WHERE Naar_AP_id IS NULL;
                 *
                 * 2. Is een ander aansluitpunt aangesloten op dit aansluitpunt?
                 *      SELECT aansluitpunten.AP_id FROM laagspanningsnet.aansluitpunten 
                 *          LEFT JOIN laagspanningsnet.aansluitingen ON aansluitpunten.AP_id = aansluitingen.AP_id
                 *          WHERE aansluitingen.AP_id IS NULL;
                 *
                 * Enkel wanneer een aansluitpunt in deze twee gevallen niet meer aangesloten is, mag het in onze lijst
                 * geretourneerd worden --> 1/2 combineren met IN
                 *
                 */
                query = query + " LEFT JOIN laagspanningsnet.aansluitingen ON aansluitpunten.AP_id = Naar_AP_id " +
                        "WHERE Naar_AP_id IS NULL";

                /*" AND aansluitpunten.AP_id IN(" +
                        "SELECT aansluitpunten.AP_id FROM laagspanningsnet.aansluitpunten " +
                        "LEFT JOIN laagspanningsnet.aansluitingen ON aansluitpunten.AP_id = aansluitingen.AP_id " +
                        "WHERE aansluitingen.AP_id IS NULL)";*/
            }
            query = query + ";";
            _mySqlDataAdapter = new MySqlDataAdapter(query, MySqlConnection);
            DataSet dataSet = GetDataSet();

            // Zet DataSet om naar een List
            BindingList<string> convert = new BindingList<string>();
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                convert.Add((String)dataRow["AP_id"]);
            }
            return convert;
        }

        /* Ga na of een machine ID reeds in de MySqlDatabase aanwezig is
         *
         * RETURN : bool : false/true : bestaat niet/bestaat
         */
        public bool IsMachine(string id)
        {
            _mySqlCommand = new MySqlCommand("SELECT M_id FROM laagspanningsnet.machines WHERE M_id LIKE @para;", MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", id);
            return !GetString().Equals("");
        }

        /* Ga na of een aansluitpunt ID reeds in de MySqlDatabase aanwezig is
         *
         * RETURN : bool : false/true : bestaat niet/bestaat
         */
        public bool IsAansluitpunt(string id)
        {
            const string query = "SELECT AP_id FROM laagspanningsnet.aansluitpunten WHERE AP_id LIKE @para;";
            _mySqlCommand = new MySqlCommand(query, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", id);
            return !GetString().Equals("");
        }

        /* Toevoegen van een nieuwe machine aan de MySqlDatabase
         * 
         * RETURN : bool : false/true : mislukt/gelukt
         */
        public bool InsertMachine(string id, string omschrijving, string locatie)
        {
            const string nonQuery = "INSERT INTO `laagspanningsnet`.`machines` (`M_id`, `M_omschrijving`, `M_locatie`) VALUES(" +
                " @para1 ," +
                " @para2 ," +
                " @para3 );";
            _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para1", id);
            _mySqlCommand.Parameters.AddWithValue("@para2", omschrijving);
            _mySqlCommand.Parameters.AddWithValue("@para3", locatie);
            return NonQueryCommon();
        }

        /* Toevoegen van een nieuw aansluitpunt aan de MySqlDatabase
         * 
         * RETURN : bool : false/true : mislukt/gelukt
         */
        public bool InsertAansluitpunt(string id, string locatie)
        {
            const string nonQuery = "INSERT INTO `laagspanningsnet`.`aansluitpunten` (`AP_id`, `AP_locatie`) VALUES(" +
                " @para1 ," +
                " @para2 );";
            _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para1", id);
            _mySqlCommand.Parameters.AddWithValue("@para2", locatie);
            return NonQueryCommon();
        }

        /* Aanpassen van een machine in de MySqlDatabase
         * 
         * RETURN : bool : false/true : mislukt/gelukt
         */
        public bool UpdateMachine(string id, string omschrijving, string locatie)
        {
            const string nonQuery = "UPDATE `laagspanningsnet`.`machines` SET `M_omschrijving`=" +
                " @para1 , `M_locatie`=" +
                " @para2 WHERE `M_id`=" +
                " @para3 ;";
            _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para1", omschrijving);
            _mySqlCommand.Parameters.AddWithValue("@para2", locatie);
            _mySqlCommand.Parameters.AddWithValue("@para3", id);
            return NonQueryCommon();
        }

        /* Aanpassen van een aansluitpunt in de MySqlDatabase
         * 
         * RETURN : bool : false/true : mislukt/gelukt
         */
        public bool UpdateAansluitpunt(string id, string locatie)
        {
            const string nonQuery = "UPDATE `laagspanningsnet`.`aansluitpunten` SET `AP_locatie`=" +
                " @para1 WHERE `AP_id`=" +
                " @para2 ;";
            _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para1", locatie);
            _mySqlCommand.Parameters.AddWithValue("@para2", id);
            return NonQueryCommon();
        }

        /* Verwijderen van een machine uit de MySqlDatabase
         * 
         * RETURN : bool : false/true : mislukt/gelukt
         */
        public bool DeleteMachine(string id)
        {
            const string nonQuery = "DELETE FROM `laagspanningsnet`.`machines` WHERE `M_id`= @para ;";
            _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", id);
            return NonQueryCommon();
        }

        /* Verwijderen van een aansluitpunt uit de MySqlDatabase
         * 
         * RETURN : bool : false/true : mislukt/gelukt
         */
        public bool DeleteAansluitpunt(string id)
        {
            const string nonQuery = "DELETE FROM `laagspanningsnet`.`aansluitpunten` WHERE `AP_id`= @para ;";
            _mySqlCommand = new MySqlCommand(nonQuery, MySqlConnection);
            _mySqlCommand.Parameters.AddWithValue("@para", id);
            return NonQueryCommon();
        }
    }
}
