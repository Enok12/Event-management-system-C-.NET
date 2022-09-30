using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Event_Management_System
{
    public partial class Login : Form
    {
        SqlConnection sqlCon=null;
        public Login()
        {
            try
            {
                DBConnection obj = new DBConnection();
                sqlCon = obj.getConnection();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting" + ex,
                    "Login Form",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            InitializeComponent();
            
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            bool valid = true;
            if (string.IsNullOrEmpty(txt_Username.Text))
            {
                MessageBox.Show("User Id cant be empty", "login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            if (string.IsNullOrEmpty(txt_password.Text))
            {
                MessageBox.Show("Password cant be empty", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            if (valid)
            {
                String UserType = null;
                SqlCommand cmd = new SqlCommand("select Usertype from Login where Username ='"+txt_Username.Text+"' and Password='"+txt_password.Text+"'", sqlCon);
                SqlDataReader dr = cmd.ExecuteReader();
                Boolean records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        UserType = dr["Usertype"].ToString();
                    }
                    if (UserType.Equals("Admin"))
                    {
                        MessageBox.Show("Welcome  Admin");
                        this.Hide();
                        Manage_Logins main = new Manage_Logins();
                        main.Show();
                    }
                    else if (UserType.Equals("Manager"))
                    {

                        MessageBox.Show("Welcome Manager");
                        this.Hide();
                        UserMainMenu main =new UserMainMenu();
                        main.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid login", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                dr.Close();
            }
        }
            
}
    }

