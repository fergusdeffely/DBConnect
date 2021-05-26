using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;


namespace DBConnect
{
    public class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public MySqlConnection Connection { get => connection; }

        public DBConnection()
        {
            Initialise();
        }

        public void Initialise()
        {
            server = "localhost";
            database = "hire";
            uid = "csharp";
            password = "password";

            // Need to form the following connection string:
            //   SERVER=localhost;DATABASE=hire;UID=csharp;PASSWORD=password;
            
            string connectionString;
            
            connectionString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
            this.connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                this.Connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                // 0 - Cannot connect to server
                // 1045 - Invalid username/password
                switch(ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to Server. Please contact Administrator.");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password - please try again.");
                        break;
                }

                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                this.Connection.Close();
                return true;
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public MySqlDataAdapter ExecuteTableQuery(string sql)
        {
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, this.Connection);
                return adapter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

    }
}
