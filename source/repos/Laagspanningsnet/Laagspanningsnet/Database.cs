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
        private static MySqlConnection connectie = new MySqlConnection(connectiestring);
        private static string connectiestring = "SERVER=" + server +
            ";DATABASE=" + database + ";UID=" + user +
            ";PASSWORD=" + password + ";";

        /* Openen van de connectie met de database.
         * Toont een MessageBox op het scherm als er een probleem is.
         * 
         * RETURN : false = mislukt
         *          true  = sluiten gelukt
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
         */
        public String getVoeding(String aansluitpunt)
        {
            Open();
            // Haal het aansluitpunt op waar de voeding van komt
            string query = "select AP_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + aansluitpunt + "';";
            MySqlCommand cmd = new MySqlCommand(query, connectie);
            string voedingAP = (String)cmd.ExecuteScalar();
            // Haal de aansluiitng op waar de voeding van komt
            query = "select A_id from laagspanningsnet.aansluitingen WHERE Naar_AP_id = '" + aansluitpunt + "';";
            cmd = new MySqlCommand(query, connectie);
            string voedingA = (String)cmd.ExecuteScalar();
            Close();
            return (voedingAP + " - " + voedingA).Trim();
        }

        /* Ophalen van de Aansluitpunt Voedingskabel
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
         */
        public DataSet getAansluitingen(String aansluitpunt)
        {
            Open();
            // Data ophalen en in DataSet ds stoppen

            // !!!!! * in query later aanpassen, enkel opvragen wat nodig is !!!!!

            string query = "select * from laagspanningsnet.aansluitingen WHERE AP_id ='" + aansluitpunt + "';";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, connectie);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Close();
            return ds;
        }

        /* Data van alle aansluitingen van een aansluitpunt opslaan in de database.
         */
        public void setAansluitingen(DataSet dsDatabase)
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
                var db_Stroom = "'" + row["Stroom"] + "'";
                var db_Polen = "'" + row["Polen"] + "'";
                // 
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

                // !!!! Voorlopig deleten en dan inserten, kan verbeterd worden door updaten , maar dan is test op reeds bestaan nodig !!!!
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
                cmd.ExecuteNonQuery();
                Close();
                count++;
            }
            
        }

        
    }
}
