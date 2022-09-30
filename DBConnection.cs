using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Event_Management_System
{
    class DBConnection
    {

        public SqlConnection getConnection()
        {

            SqlConnection sqlCon = null;
            try
            {
                sqlCon = new SqlConnection("Data Source=LAPTOP-MQB4IL01\\SQLEXPRESS;Initial Catalog=Event Management System;User ID=Admin;Password=12345678");
                //sqlCon = new SqlConnection("data source=10.0.0.4;initial catalog=WA_Net;Trusted Connection=True");
                sqlCon.Open();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Db" + ex);

            }
            return sqlCon;
        }

        //Search Value
        public SqlDataReader Searchvalues(String SQL) {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand(SQL, con);
            SqlDataReader rs =cmd.ExecuteReader();
            return rs;
        }

        //Insert Value
        public int Insertvalues(String SQL)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand(SQL, con);
            int Rows = cmd.ExecuteNonQuery();
            con.Close();
            return Rows;
        }
        //Execute Scalar
        public int Scalar(String SQL)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand(SQL, con);
            Int32 value = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return value;
        }
    }
}
