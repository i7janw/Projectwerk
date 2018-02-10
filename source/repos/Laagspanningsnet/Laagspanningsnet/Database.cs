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
        private static string server = "localhost";
        private static string database = "laagspanningsnet";
        private static string user = "root";
        private static string password = "root";
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
            connectie = new MySqlConnection(connectiestring);
            try
            {
                connectie.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // Dit kan evt. nog verbeterd worden door "ex" niet af, een switch case te gebruiken
                // voor de meest gangbare fouten.
                MessageBox.Show("Kan geen verbinding maken met database\n\n" + ex);
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
                MessageBox.Show("Kan geen verbinding maken met database\n\n" + ex);
                return false;
            }
        }

        /* Ophalen van de Machine Omschrijving
         * 
         * RETURN : String omschrijving
         */
        public String getMachineOmschrijving(String machine)
        {
            Open();
            string query = "select M_omschrijving from laagspanningsnet.machines WHERE M_id = '" + machine +"';";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            string omschrijving = (String)cmd.ExecuteScalar();
            Close();
            return omschrijving;
        }

        /* Ophalen van de Machine Locatie
         * 
         * RETURN : string locatie
         */
        public String getMachineLocatie(String machine)
        {
            Open();
            string query = "select M_locatie from laagspanningsnet.machines WHERE M_id = '" + machine + "';";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            string locatie = (String)cmd.ExecuteScalar();
            Close();
            return locatie;
        }

        /* Ophalen van de Aansluitpunt Locatie
         * 
         * RETURN string locatie
         */
        public String getAansluitpuntLocatie(String aansluitpunt)
        {
            Open();
            string query = "select AP_locatie from laagspanningsnet.aansluitpunten WHERE AP_id = '" + aansluitpunt + "';";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            string locatie = (String)cmd.ExecuteScalar();
            Close();
            return locatie;
        }

        /* Ophalen van de Aansluitpunt Voeding
         *
         * RETURN : string voeding , = "-" als er geen voeding is gevonden
         */
        public String getVoeding(String aansluitpunt)
        {
            Open();
            // Haal het aansluitpunt op waar de voeding van komt
            string query = "select AP_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + aansluitpunt + "';";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            string voedingAP = (String)cmd.ExecuteScalar();
            // Haal de aansluitng op waar de voeding van komt
            query = "select A_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + aansluitpunt + "';";
            cmd = new MySqlCommand(query, connectie);
            string voedingA = (String)cmd.ExecuteScalar();
            Close();
            return (voedingAP + " - " + voedingA).Trim();
        }

        /* Ophalen van de Aansluitpunt Voedingskabel
         * 
         * RETURN : string voedingskabel , = "-" als er geen voedingskabel is gevonden
         */
        public String getKabel(String aansluitpunt)
        {
            Open();
            // Haal het aansluitpunt op waar de voeding van komt
            string query = "select Kabeltype from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + aansluitpunt + "';";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            string kabeltype = (String)cmd.ExecuteScalar();
            // Haal de aansluitng op waar de voeding van komt
            query = "select Kabelsectie from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + aansluitpunt + "';";
            cmd = new MySqlCommand(query, connectie);
            string kabelsectie = (String)cmd.ExecuteScalar();
            Close();
            return (kabeltype + " - " + kabelsectie).Trim();
        }

        /* Ophalen van het Aansluitpunt zijn stroomtoevoer
         * 
         * RETURN : string stroom , = "-" als er geen stroom is gevonden
         */
        public String getStroom(String aansluitpunt)
        {
            Open();
            // Haal het aansluitpunt op waar de voeding van komt
            string query = "select Stroom from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + aansluitpunt + "';";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            var stroom = cmd.ExecuteScalar();
            if (stroom == null)
            {
                return "-";
            }
            return (stroom + "A").Trim();
        }

        /* Opvragen van alle aanwezige Transormatoren in het bedrijf.
         *
         * RETURN : DataSet met gegevens alle transfo's
         */
        public DataSet getTransfos()
        {
            Open();
            // Data ophalen en in DataSet ds stoppen
            string query = "select AP_id, AP_locatie from laagspanningsnet.aansluitpunten WHERE AP_id LIKE 'T%';";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Close();
            return ds;
        }

        /* Opvragen van alle aansluitingen voor een bepaald aansluitpunt.
         * 
         * RETURN : DataSet met gegevens alle aansluitingen van een aansluitpunt
         */
        public DataSet getAansluitingen(String aansluitpunt)
        {
            Open();
            // Data ophalen en in DataSet ds stoppen

            // !!!!! TODO * in query later aanpassen, enkel opvragen wat nodig is !!!!!

            string query = "select * from laagspanningsnet.aansluitingen WHERE AP_id ='" + aansluitpunt + "';";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Close();
            return ds;
        }

        /* Opvragen zoek-resultaten.
         * 
         * RETURN : DataSet met alle zoekresultaten
         */
        public DataSet getSearch(String search)
        {
            Open();
            // Data ophalen en in DataSet ds stoppen

            // !!!!! * in query later aanpassen, enkel opvragen wat nodig is !!!!!
            // TODO Query e.d. nog te bekijken, momenteel voldoende om te testen...
            string what = "AP_id, A_id, Kabeltype, Kabelsectie, Stroom, Polen, Omschrijving, Naar_AP_id, M_id AS Naar_M_id";
            string where = "WHERE Naar_AP_id LIKE '%" +
                search + "%' OR M_id LIKE '%" +
                search + "%' OR Omschrijving LIKE '%" +
                search + "%' OR M_omschrijving LIKE '%" +
                search + "%' ";
            string query = "SELECT " + what + " FROM laagspanningsnet.aansluitingen " +
                "LEFT JOIN laagspanningsnet.machines ON Naar_M_id = M_id " +
                where +
                "UNION " +
                "SELECT " + what + " FROM laagspanningsnet.aansluitingen " +
                "RIGHT JOIN laagspanningsnet.machines ON Naar_M_id = M_id " +
                where + "";
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Close();
            return ds;
        }

        /* Data van alle aansluitingen van een aansluitpunt opslaan in de database.
         * 
         * RETURN : false = mislukt
         *          true  = bewaren gelukt
         */
        public bool setAansluitingen(DataSet dsDatabase)
        {
            int count = 0;
            foreach (DataRow row in dsDatabase.Tables["aansluitingen"].Rows)
            {
                // Steek de gegevens van deze row in losse var's
                var db_AP_id = "'" + row["AP_id"] + "'";
                var db_A_id = "'" + row["A_id"] + "'";
                var db_Naar_AP_id = row["Naar_AP_id"];
                var db_Naar_M_id = row["Naar_M_id"];
                var db_Omschrijving = row["Omschrijving"];
                var db_Kabeltype = "'" + row["Kabeltype"] + "'";
                var db_Kabelsectie = "'" + row["Kabelsectie"]+ "'";
                var db_Stroom = row["Stroom"];
                var db_Polen = row["Polen"];
                // Afhandelen van items waar evt. NULL in kan zitten.
                if(db_Naar_AP_id == DBNull.Value)
                {
                    db_Naar_AP_id = "NULL";
                }
                else
                {
                    db_Naar_AP_id = "'" + db_Naar_AP_id + "'";
                }
                //
                if (db_Naar_M_id == DBNull.Value)
                {
                    db_Naar_M_id = "NULL";
                }
                else
                {
                    db_Naar_M_id = "'" + db_Naar_M_id + "'";
                }
                //
                if (db_Omschrijving == DBNull.Value)
                {
                    db_Omschrijving = "NULL";
                }
                else
                {
                    db_Omschrijving = "'" + db_Omschrijving + "'";
                }
                //
                if (db_Stroom == DBNull.Value)
                {
                    db_Stroom = "NULL";
                }
                else
                {
                    db_Stroom = "'" + db_Stroom + "'";
                }
                //
                if (db_Polen == DBNull.Value)
                {
                    db_Polen = "NULL";
                }
                else
                {
                    db_Polen = "'" + db_Polen + "'";
                }

                // !!!! TODO : Voorlopig deleten en dan inserten, kan misschien verbeterd worden door updaten , maar dan is test op reeds bestaan nodig !!!!
                Open();
                string query;
                MySqlCommand cmd;
                if (count == 0) { 
                    // DELETE alles van een bepaald aansluitpunt van de database (enkel 1 maal uitvoeren is voldoende)
                    query = "DELETE FROM laagspanningsnet.aansluitingen WHERE AP_id = " + db_AP_id + ";";
                    cmd = new MySqlCommand(query, connectie);
                    cmd.ExecuteNonQuery();
                }

                // INSERT de gegevens in de database
                query = "INSERT INTO laagspanningsnet.aansluitingen " + 
                    "(`AP_id`, `A_id`, `Naar_AP_id`, `Naar_M_id` , `Omschrijving`, `Kabeltype`, `Kabelsectie`, `Stroom`, `Polen`)" +
                    "VALUES(" + 
                    db_AP_id + ", " +
                    db_A_id + ", " +
                    db_Naar_AP_id + ", " +
                    db_Naar_M_id + ", " +
                    db_Omschrijving + ", " +
                    db_Kabeltype + ", " +
                    db_Kabelsectie + ", " +
                    db_Stroom + ", " +
                    db_Polen + ");";
                Console.WriteLine(query);
                cmd = new MySqlCommand(query, connectie);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred: " +  e);
                }
                Close();
                count++;
            }
            return true;    // TODO als bewaren mislukt false catch try e.d. nog toe te voegen
        }

        // NEW ------------------------------------------------------------------------------------

        /* Haal een lijst op van alle machines die in de machine table aanwezig zijn
         * 
         * RETURN: List<String> met alle machines
         */
        public List<String> getMachines()
        {
            return getMachines(false);
        }

        /* Haal een lijst op van alle machines die in de machine table aanwezig zijn 
         * 
         * Naargelang _notconnected = true/false worden enkel de niet aangesloten machines geRETURNed
         * 
         * RETURN: List<String> met alle machines
         */
        public List<String> getMachines(bool _notConnected)
        { 
            Open();
            string query = "SELECT M_id FROM laagspanningsnet.machines ";
            if (_notConnected)
            {
                query = query + "LEFT JOIN laagspanningsnet.aansluitingen ON M_id = Naar_M_id WHERE Naar_M_id IS NULL";
            }
            query = query + ";";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Close();

            List<string> convert = new List<string>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                convert.Add((String)row["M_id"]);
            }
            return convert;
        }

        /* Haal een lijst op van alle aansluitpunten die in de aansluitpunt table aanwezig zijn
         * 
         * RETURN: List<String> met alle aansluitpunten
         */
        public List<String> getAansluitpunten()
        {
            return getAansluitpunten(false);
        }

        /* Haal een lijst op van alle aansluitpunten die in de aansluitpunt table aanwezig zijn 
         * 
         * Naargelang _notconnected = true/false worden enkel de niet aangesloten aansluitpunten geRETURNed
         * 
         * RETURN: List<String> met alle aansluitpunten
         */
        public List<String> getAansluitpunten(bool _notConnected)
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
