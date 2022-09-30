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
    public partial class Client_Management : Form
    {
        Costumer obj = new Costumer();
        public Client_Management()
        {
            InitializeComponent();
        }

        private void Client_Management_Load(object sender, EventArgs e)
        {
            txt_C_ID.Text = obj.AutoID();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            
        }

     

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            String Gender = "";
            if (RA_Male.Checked)
            {
                Gender = "Male";

            }
            else if (RA_Female.Checked)
            {
                Gender = "Female";

            }
            else
            {
                Gender = null;
            }

            obj.setC_ID(txt_C_ID.Text);
            obj.setF_Name(txt_FirstName.Text);
            obj.setL_Name(txt_L_Name.Text);
            obj.setDOB(dateTimePicker1.Text.ToString());
            obj.setNIC(txt_NIC.Text);
            obj.setContact(txt_Contact.Text);
            obj.setGender(Gender);
            bool valid = obj.validation();
            if (valid)
            {
                int Rows = obj.Insert_Costumer();

                if (Rows > 0)
                {
                    MessageBox.Show("Record/s Successfuly Added ", "Client Management Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("Record/s Fail to Add ", "Client Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String Gender = "";
            try
            {
                obj.setC_ID(Search_Costumer.Text);
                SqlDataReader rs = obj.SearchCostumer();
                if (rs.Read())
                {

                    txt_F_Name1.Text = rs["F_Name"].ToString();
                    txt_L_Name2.Text = rs["L_Name"].ToString();
                    txt_DOB.Text= rs["DOB"].ToString();
                    txt_Contact2.Text = rs["Contact"].ToString();
                    txt_NIC2.Text = rs["NIC"].ToString();
                    Gender = rs["Gender"].ToString();

                    if (Gender == "Male")
                    {
                        R_Male.Checked = true;
                        R_Female.Checked = false;
                    }
                    else
                    {
                        R_Female.Checked = true;
                        R_Male.Checked = false;
                    }
                    MessageBox.Show("Client Found");
                }
                else
                {
                    txt_F_Name1.Text = "";
                    txt_L_Name2.Text = "";
                    txt_DOB.Text = "";
                    txt_Contact2.Text = "";
                    txt_NIC2.Text = "";
                    R_Female.Checked = false;
                    R_Male.Checked = false;
                    MessageBox.Show("Client not found");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured" + ex);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String Gender = "";

            if (R_Male.Checked)
            {
                Gender = "Male";
                R_Male.Checked = true;
                R_Female.Checked = false;
            }
            else
            {
                Gender = "Female";
                R_Female.Checked = true;
                R_Male.Checked = false;
            }

            obj.setC_ID(Search_Costumer.Text);
            obj.setF_Name(txt_F_Name1.Text);
            obj.setL_Name(txt_L_Name2.Text);
            obj.setDOB(txt_DOB.Text);
            obj.setContact(txt_Contact2.Text);
            obj.setNIC(txt_NIC2.Text);
            obj.setGender(Gender);

            int Rows = obj.UpdateCostumer();

            if (Rows > 0)
            {
                MessageBox.Show("Record/s Successfuly Updated ", "CLient Management Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Record/s Fail to Add", "CLient Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserMainMenu main = new UserMainMenu();
            main.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt_C_ID.Text = obj.AutoID();
            txt_FirstName.Text = "";
            txt_L_Name.Text = "";
            txt_NIC.Text = "";
            txt_Contact.Text = "";

            RA_Male.Checked = false;
            RA_Female.Checked = false;
        }
    }
}
