using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DBConnect
{
    public partial class FormDatabaseConnect : Form
    {
        DBConnection db = null;

        public FormDatabaseConnect()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            this.db = new DBConnection();

            if(this.db.OpenConnection())
            {
                MessageBox.Show("Connected to database.");
            }
            else
            {
                MessageBox.Show("Problem connecting to database.");
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            CarDB carDB = new CarDB();

            MySqlDataReader carReader = carDB.GetCars(this.db);

            while (carReader.Read())
            {
                string reg = carReader.GetString(0);
                string manufacturer = carReader.GetString(1);
                string engineSize = carReader.GetString(2);
                DateTime dateRegistered = carReader.GetDateTime(3);
                int rentalPerDay = carReader.GetInt32(4);
                int available = carReader.GetInt32(5);

                string availableText = "Yes";
                if (available == 0)
                {
                    availableText = "No";
                }

                string rowDataText = string.Format("Registration: {0},  Manufacturer: {1}, Engine Size: {2}, Date Registered: {3:MM/dd/yyyy}, Rental Per Day: €{4:n2}, Available: {5}",
                                               reg, manufacturer, engineSize, dateRegistered, rentalPerDay, availableText);
                MessageBox.Show(rowDataText);
            }

            carReader.Close();
        }

        private void FormDatabaseConnect_Close(object sender, EventArgs e)
        {
            if(this.db != null)
            {
                this.db.CloseConnection();
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            CarDB carDB = new CarDB();

            DateTime dateRegistered = new DateTime(2006, 9, 13);

            carDB.Insert(this.db, "YZ785HJK", "Mercedes", "1.4L", dateRegistered, 90, 0);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            CarDB carDB = new CarDB();

            DateTime dateRegistered = new DateTime(2006, 9, 20);

            carDB.Update(this.db, "YZ785HJK", "Mercedes", "1.4L", dateRegistered, 95, 0);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            CarDB carDB = new CarDB();

            carDB.Delete(this.db, "YZ785HJK");
        }
    }
}
