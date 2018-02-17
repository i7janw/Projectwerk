/*  Communicatie met de mySQL database server loopt via deze klasse.
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// usings nodig om met de database te verbinden
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace Laagspanningsnet
{
    class Database
    {
        // Gegevens nodig om met de database verbinding te maken.

        // Mysql via localhost :
        ///*
        private static string server = "localhost";
        private static string database = "laagspanningsnet";
        private static string user = "root";
        private static string password = "root";
        //*/

        // MySql via FreeBSD server alix2d13
        /*
        private static string server = "192.168.1.17";
        private static string database = "laagspanningsnet";
        private static string user = "jan";
        private static string password = ".ShlorcunJad9";
        */

        private static string connectiestring = "SERVER=" + server +
            ";DATABASE=" + database + ";UID=" + user +
            ";PASSWORD=" + password + ";";
        private static MySqlConnection connectie = new MySqlConnection(connectiestring);

        /* Openen van de connectie met de database.
         * Toont een MessageBox op het scherm als er een probleem is.
         * 
         * RETURN : false = mislukt
         *          true  = openen gelukt
         */
        private bool Open()
        {
            try
            {
                connectie.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Dit kan evt. nog verbeterd worden door "ex" niet af, een switch case te gebruiken
                // voor de meest gangbare fouten.
                MessageBox.Show("Kan geen verbinding maken met database\n\n" + ex.Message);
                return false;
            }
        }

        /* Sluiten van de connectie met de database.
         * Toont een MessageBox op het scherm als er een probleem is.
         * 
         * RETURN : false = mislukt
         *          true  = sluiten gelukt
         */
        private bool Close()
        {
            try
            {
                connectie.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // Dit kan evt. nog verbeterd worden door "ex" niet af te drukken en een switch case te gebruiken
                // voor de meest gangbare fouten.
                MessageBox.Show("Kan geen verbinding maken met database\n\n" + ex.Message);
                return false;
            }
        }

        /* Ophalen van de Machine Omschrijving
         * 
         * RETURN : String omschrijving
         */
        public String GetMachineOmschrijving(String _machine)
        {
            string _query = "select M_omschrijving from laagspanningsnet.machines WHERE M_id = '" + _machine + "';";
            return(GetString(_query));
        }

        /* Ophalen van de Machine Locatie
         * 
         * RETURN : string locatie
         */
        public String GetMachineLocatie(String _machine)
        {
            string _query = "select M_locatie from laagspanningsnet.machines WHERE M_id = '" + _machine + "';";
            return (GetString(_query));
        }

        /* Ophalen van de Aansluitpunt Locatie
         * 
         * RETURN string locatie
         */
        public String GetAansluitpuntLocatie(String _aansluitpunt)
        {
            string _query = "select AP_locatie from laagspanningsnet.aansluitpunten WHERE AP_id = '" + _aansluitpunt + "';";
            return (GetString(_query));
        }

        /* Ophalen van de Aansluitpunt Voeding
         *
         * RETURN : string voeding , = "-" als er geen voeding is gevonden
         */
        public String GetVoeding(String _aansluitpunt)
        {
            // Haal het aansluitpunt op waar de voeding van komt
            string _query = "select AP_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + _aansluitpunt + "';";
            string _voedingAP = GetString(_query);
            // Haal de aansluitng op waar de voeding van komt
            _query = "select A_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + _aansluitpunt + "';";
            string _voedingA = GetString(_query);
            return (_voedingAP + " - " + _voedingA).Trim();
        }

        /* Ophalen van de Aansluitpunt Voedingskabel
         * 
         * RETURN : string voedingskabel , = "-" als er geen voedingskabel is gevonden
         */
        public String GetKabel(String _aansluitpunt)
        {
            // Haal het aansluitpunt op waar de voeding van komt
            string _query = "select Kabeltype from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + _aansluitpunt + "';";
            string _kabeltype = GetString(_query);
            // Haal de aansluitng op waar de voeding van komt
            _query = "select Kabelsectie from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + _aansluitpunt + "';";
            string _kabelsectie = GetString(_query);
            return (_kabeltype + " - " + _kabelsectie).Trim();
        }

        /* Ophalen van het Aansluitpunt zijn stroomtoevoer
         * 
         * RETURN : string stroom , = "-" als er geen stroom is gevonden
         */
        public String GetStroom(String _aansluitpunt)
        {
            // Haal het aansluitpunt op waar de voeding van komt
            string _query = "select Stroom from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + _aansluitpunt + "';";
            string _stroom = GetString(_query);
            if (_stroom == "")
            {
                _stroom = "-";
            }
            else
            {
                _stroom = (_stroom + "A").Trim();
            }
            return _stroom;
        }

        /* Opvragen van alle aanwezige Transormatoren in het bedrijf.
         * We geven de transformatoren terug als aansluitpunten (Naar_AP_id) die zijn aangesloten op het aansluitpunt "Hoogspanning" 
         * om de code in Hoofdscherm zo uniform mogelijk te houden.
         *
         * RETURN : DataSet met gegevens alle transfo's
         */
        public DataSet GetTransfos()
        {
            string _query = "SELECT " +
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
            return GetDataSet(_query);
        }

        /* Opvragen van alle aansluitingen voor een bepaald aansluitpunt.
         * 
         * RETURN : DataSet met gegevens alle aansluitingen van een aansluitpunt
         */
        public DataSet GetAansluitingen(String _aansluitpunt)
        {
            string _query = "select * from laagspanningsnet.aansluitingen WHERE AP_id ='" + _aansluitpunt + "';";
            return GetDataSet(_query);
        }

        /* Opvragen zoek-resultaten.
         * 
         * RETURN : DataSet met alle zoekresultaten
         */
        public DataSet GetSearch(String _search)
        {
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // TODO Query e.d. nog te bekijken, momenteel voldoende om te testen...
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            string _what = "AP_id, A_id, Kabeltype, Kabelsectie, Stroom, Polen, Omschrijving, Naar_AP_id, M_id AS Naar_M_id";
            string _where = "WHERE Naar_AP_id LIKE '%" +
                _search + "%' OR M_id LIKE '%" +
                _search + "%' OR Omschrijving LIKE '%" +
                _search + "%' OR M_omschrijving LIKE '%" +
                _search + "%' ";
            string _query = "SELECT " + _what + " FROM laagspanningsnet.aansluitingen " +
                "LEFT JOIN laagspanningsnet.machines ON Naar_M_id = M_id " +
                _where +
                "UNION " +
                "SELECT " + _what + " FROM laagspanningsnet.aansluitingen " +
                "RIGHT JOIN laagspanningsnet.machines ON Naar_M_id = M_id " +
                _where + "";
            return GetDataSet(_query);
        }

        /* Data van alle aansluitingen van een aansluitpunt opslaan in de database.
         * 
         * RETURN : false = mislukt
         *          true  = bewaren gelukt
         */
        public bool SetAansluitingen(DataSet dsDatabase)
        {
            bool _return = true;    // Bijhouden of er fouten optreden, we gaan er van uit dat alles goed zal verlopen
            int _count = 0;
            foreach (DataRow _row in dsDatabase.Tables["aansluitingen"].Rows)
            {
                // Steek de gegevens van deze row in losse var's
                var _db_AP_id = "'" + _row["AP_id"] + "'";
                var _db_A_id = "'" + _row["A_id"] + "'";
                var _db_Naar_AP_id = _row["Naar_AP_id"];
                var _db_Naar_M_id = _row["Naar_M_id"];
                var _db_Omschrijving = _row["Omschrijving"];
                var _db_Kabeltype = "'" + _row["Kabeltype"] + "'";
                var _db_Kabelsectie = "'" + _row["Kabelsectie"]+ "'";
                var _db_Stroom = _row["Stroom"];
                var _db_Polen = _row["Polen"];
                // Afhandelen van items waar evt. NULL in kan zitten.
                if(_db_Naar_AP_id == DBNull.Value)
                {
                    _db_Naar_AP_id = "NULL";
                }
                else
                {
                    _db_Naar_AP_id = "'" + _db_Naar_AP_id + "'";
                }
                //
                if (_db_Naar_M_id == DBNull.Value)
                {
                    _db_Naar_M_id = "NULL";
                }
                else
                {
                    _db_Naar_M_id = "'" + _db_Naar_M_id + "'";
                }
                //
                if (_db_Omschrijving == DBNull.Value)
                {
                    _db_Omschrijving = "NULL";
                }
                else
                {
                    _db_Omschrijving = "'" + _db_Omschrijving + "'";
                }
                //
                if (_db_Stroom == DBNull.Value)
                {
                    _db_Stroom = "NULL";
                }
                else
                {
                    _db_Stroom = "'" + _db_Stroom + "'";
                }
                //
                if (_db_Polen == DBNull.Value)
                {
                    _db_Polen = "NULL";
                }
                else
                {
                    _db_Polen = "'" + _db_Polen + "'";
                }

                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // !!!! TODO : Voorlopig deleten en dan inserten, kan misschien verbeterd worden door updaten , maar dan is test op reeds bestaan nodig !!!!
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                string _query;
                if (_count == 0) { 
                    // DELETE alles van een bepaald aansluitpunt van de database (enkel 1 maal uitvoeren is voldoende)
                    _query = "DELETE FROM laagspanningsnet.aansluitingen WHERE AP_id = " + _db_AP_id + ";";
                    if (!NonQueryCommon(_query))
                    {
                        _return = false;
                    }
                }

                // INSERT de gegevens in de database
                _query = "INSERT INTO laagspanningsnet.aansluitingen " + 
                    "(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , `Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`)" +
                    "VALUES(" + 
                    _db_AP_id + ", " +
                    _db_A_id + ", " +
                    _db_Naar_AP_id + ", " +
                    _db_Naar_M_id + ", " +
                    _db_Omschrijving + ", " +
                    _db_Kabeltype + ", " +
                    _db_Kabelsectie + ", " +
                    _db_Stroom + ", " +
                    _db_Polen + ");";
                if (!NonQueryCommon(_query))
                {
                    _return = false;
                }
                _count++;
            }
            return _return;
        }

        /* Ophalen van een string uit de database
         * private gemeenschappelijke routine 
         * 
         * RETURN : String data
         */
        private String GetString(String _query)
        {
            string _return = "";
            if (!Open())
            {
                return _return;
            }
            MySqlCommand cmd = new MySqlCommand(_query, connectie);
            try
            {
                var _tmp = cmd.ExecuteScalar();
                if (_tmp != null)
                {
                    _return = _tmp.ToString();          // omzetten van var naar String, geen cast gebruiken, want var kan ook een int zijn.
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
            return _return;
        }

        /* Ophalen van een DataSet uit de database
         * private gemeenschappelijke routine.
         *
         * RETURN : DataSet data
         */
        private DataSet GetDataSet(String _query)
        {
            DataSet _ds = new DataSet();
            if (!Open())
            {
                return _ds;
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter(_query, connectie);
            try
            { 
                adapter.Fill(_ds);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
            return _ds;
        }

        /*  Gemeenschappelijke code voor Insert/Update/Delete (ExecuteNonQuery()).
         * 
         *  RETURN : false/true : mislukt/gelukt 
         */
        private bool NonQueryCommon(String _nonQuery)
        {
            if (!Open())
            {
                return false;
            }
            MySqlCommand cmd = new MySqlCommand(_nonQuery, connectie);
            try
            { 
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Close();
                return false;
            }
            return Close();
        }
        
        /* Haal een lijst op van alle machines die in de machine table aanwezig zijn
         * 
         * RETURN: List<String> met alle machines
         */
        public List<String> GetMachines()
        {
            return GetMachines(false);
        }

        /* Haal een lijst op van alle machines die in de machine table aanwezig zijn 
         * 
         * Naargelang _notconnected = true/false worden enkel de niet aangesloten machines geRETURNed
         * 
         * RETURN: List<String> met alle machines
         */
        public List<String> GetMachines(bool _notConnected)
        { 
            string _query = "SELECT M_id FROM laagspanningsnet.machines ";
            if (_notConnected)
            {
                _query = _query + "LEFT JOIN laagspanningsnet.aansluitingen ON M_id = Naar_M_id WHERE Naar_M_id IS NULL";
            }
            _query = _query + ";";
            DataSet _ds = GetDataSet(_query);
            
            // Zet DataSet om naar een List
            List<string> _convert = new List<string>();
            foreach (DataRow _row in _ds.Tables[0].Rows)
            {
                _convert.Add((String)_row["M_id"]);
            }
            return _convert;
        }

        /* Haal een lijst op van alle aansluitpunten die in de aansluitpunt table aanwezig zijn
         * 
         * RETURN: List<String> met alle aansluitpunten
         */
        public List<String> GetAansluitpunten()
        {
            return GetAansluitpunten(false);
        }

        /* Haal een lijst op van alle aansluitpunten die in de aansluitpunt table aanwezig zijn 
         * 
         * Naargelang _notconnected = true/false worden enkel de niet aangesloten aansluitpunten geRETURNed
         * 
         * RETURN: List<String> met alle aansluitpunten
         */
        public List<String> GetAansluitpunten(bool _notConnected)
        {
            Open();
            string query = "SELECT aansluitpunten.AP_id FROM laagspanningsnet.aansluitpunten ";
            if (_notConnected)
            {
                query = query + "LEFT JOIN laagspanningsnet.aansluitingen ON aansluitpunten.AP_id = Naar_AP_id WHERE Naar_AP_id IS NULL";
            }
            query = query + ";";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Close();

            List<string> convert = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                convert.Add((String)row["AP_id"]);
            }
            return convert;
        }
    }
}
