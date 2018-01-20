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

        /* Opvragen van alle aanwezige Transormatoren in het bedrijf.
         */
        public DataSet getTransfos()
        {
            Open();
            // Data ophalen en in DataSet ds stoppen
            string query = "select AP_id AS 'Transfo', AP_locatie AS 'Locatie' from laagspanningsnet.aansluitpunten WHERE AP_id LIKE 'T%';";
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
    }
}
