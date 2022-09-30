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
    public partial class Manage_Logins : Form
    {
        SqlConnection conn;
        DBConnection obj = new DBConnection();
        Users ad = new Users();
        public Manage_Logins()
        {
            conn = obj.getConnection();
            InitializeComponent();
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
            ad.setUsername(txt_Usernam.Text);
            try
            {
                SqlDataReader rs = ad.Search();
                if (rs.Read())
                {
                    
                    L_F_Name.Text = rs["F_Name"].ToString();
                    L_L_Name.Text = rs["L_Name"].ToString();
                    L_NIC.Text = rs["NIC"].ToString();
                    L_Usertype.Text = rs["Usertype"].ToString();

                    if (L_DisplayMSG.Visible== true) {
                        L_DisplayMSG.Visible = false;
                    }
                    MessageBox.Show("User Found");
                }
                else {
                    MessageBox.Show("User not found");
                }
            
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured" + ex);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ad.setF_Name(txt_F_Name.Text);
            ad.setL_Name(txt_L_Name.Text);
            ad.setContact(txt_Contact.Text);
            ad.setNIC(txt_NIC.Text);
            ad.setUsername(txt_Username.Text);
            ad.setPassword(txt_Password.Text);
            ad.setUsertype(ComboUser.SelectedItem.ToString());
           

            String Confirm_P = txt_C_Password.Text;
            bool valid = ad.Validation(Confirm_P);
         
            if (valid)
            {
                int Rows = ad.Inserting();
                if (Rows > 0)
                {
                    
                    MessageBox.Show("User Succesfully Created ", "Login Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Fail to Create User", "Login Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            L_Usertype.Text = "";
            L_F_Name.Text = "";
            L_L_Name.Text = "";
            L_NIC.Text = "";

            txt_Usernam.Text = "";
            txt_Username2.Text = "";
            txt_OldPass.Text = "";
            txt_Newpass.Text = "";

            if (L_DisplayMSG.Visible == false)
            {
                L_DisplayMSG.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String Password = txt_OldPass.Text;
            String Newpass = txt_Newpass.Text;

            bool valid= ad.Validation(Password, Newpass);
          
            if (valid) {
                ad.setPassword(Newpass);
                ad.setUsername(txt_Usernam.Text);

               int Rows =  ad.ChangePassword();

                if (Rows > 0)
                {
                    MessageBox.Show("Password Updated ", "Login Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("Password Fail to change.", "Login Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_chg_Username_Click(object sender, EventArgs e)
        {
            ad.setUsername(txt_Username2.Text);

            int Rows = ad.ChangeUsername(txt_Usernam.Text);

            if (Rows > 0)
            {
                MessageBox.Show("Username Successfuly Updated ", "Login Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Record Fail to update. Check searched Username or Updated Username Already exists", "Login Management", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Do you want to Logout? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (Confirm ==DialogResult.Yes) {
                this.Hide();
                Login Log = new Login();
                Log.Show();
            }
        }

        private void Comboselection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display();
        }

       

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Do you want to Exit Application? ", "Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (Confirm == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        //Display Information according to Usertypes to Datagridview
        private void Display()
        {
            try
            {
                SqlCommand Searchevent = new SqlCommand("select * from Login where Usertype = '" + Comboselection.SelectedItem.ToString() + "'", 
                    conn);

                SqlDataAdapter da = new SqlDataAdapter(Searchevent);
                DataSet ds = new DataSet();
                da.Fill(ds, "Login");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Login";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error Searching data " +
          ex, "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Manage_Logins_Load(object sender, EventArgs e)
        {

        }
    }
}
