using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DBConnect
{
    public class CarDB
    {
        public MySqlDataReader GetCars(DBConnection db)
        {
            string sql = "SELECT * FROM car";
            using var cmd = new MySqlCommand(sql, db.Connection);

            MySqlDataReader rdr = cmd.ExecuteReader();

            return rdr;
        }

        public bool Insert(DBConnection db, string vehicleRegNo, string make, string engineSize, DateTime dateRegistered, double rentalPerDay, int available)
        {
            var sql = "INSERT INTO car(VehicleRegNo, Make, EngineSize, DateRegistered, RentalPerDay, Available) ";
            sql +=    "VALUES(@vehicleRegNo, @make, @engineSize, @dateRegistered, @rentalPerDay, @available)";
            using var cmd = new MySqlCommand(sql, db.Connection);

            cmd.Parameters.AddWithValue("@vehicleRegNo", vehicleRegNo);
            cmd.Parameters.AddWithValue("@make", make);
            cmd.Parameters.AddWithValue("@engineSize", engineSize);
            cmd.Parameters.AddWithValue("@dateRegistered", dateRegistered);
            cmd.Parameters.AddWithValue("@rentalPerDay", rentalPerDay);
            cmd.Parameters.AddWithValue("@available", available);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Update(DBConnection db, string vehicleRegNo, string make, string engineSize, DateTime dateRegistered, double rentalPerDay, int available)
        {
            string sql = "UPDATE car SET VehicleRegNo = @vehicleRegNo, Make = @make, EngineSize = @engineSize, DateRegistered = @dateRegistered, RentalPerDay = @rentalPerDay, Available = @available WHERE VehicleRegNo = @vehicleRegNo";

            MySqlCommand cmd = new MySqlCommand(sql, db.Connection);

            cmd.Parameters.AddWithValue("@vehicleRegNo", vehicleRegNo);
            cmd.Parameters.AddWithValue("@make", make);
            cmd.Parameters.AddWithValue("@engineSize", engineSize);
            cmd.Parameters.AddWithValue("@dateRegistered", dateRegistered);
            cmd.Parameters.AddWithValue("@rentalPerDay", rentalPerDay);
            cmd.Parameters.AddWithValue("@available", available);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Delete(DBConnection db, string vehicleRegNo)
        {
            string sql = "DELETE FROM car WHERE VehicleRegNo = @vehicleRegNo";

            MySqlCommand cmd = new MySqlCommand(sql, db.Connection);

            cmd.Parameters.AddWithValue("@vehicleRegNo", vehicleRegNo);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            return true;
        }
    }
}
