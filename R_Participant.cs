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
    public partial class R_Participant : Form
    {
        Users us = new Users();
        public R_Participant()
        {
            InitializeComponent();
        }

        private void R_Participant_Load(object sender, EventArgs e)
        {
            txt_Reg_ID.Text = us.AutoID();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
     
            dateTimePicker1.CustomFormat = "MM/dd/yyyy";
          
        }




        private void button1_Click_1(object sender, EventArgs e)
        {
            String Gender = "";
            if (R_Male.Checked)
            {
                Gender = "Male";

            }
            else if (R_Female.Checked)
            {
                Gender = "Female";

            }
            else
            {
                Gender = null;
            }

            String Reg_ID = txt_Reg_ID.Text;
            String F_Name = txt_Firstname.Text;
            String L_Name = txt_L_Name.Text;
            String DOB = dateTimePicker1.Text.ToString();
            String Contact = txt_Contact.Text;
            String NIC = txt_NIC.Text;

            bool valid = us.Validation(F_Name, L_Name, NIC, Contact);
            if (valid)
            {
                int Rows = us.Inserting(Reg_ID, F_Name, L_Name, Contact, NIC, Gender, DOB);

                if (Rows > 0)
                {
                    MessageBox.Show("Record/s Successfuly Added ", "Register Participant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Record/s Fail to Register ", "Register Participant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            String Gender = "";
            try
            {
                SqlDataReader rs = us.Search(Search_REG_ID.Text);
                if (rs.Read())
                {

                    txt_F_Name2.Text = rs["F_Name"].ToString();
                    txt_L_Name2.Text = rs["L_Name"].ToString();
                    txt_DOB2.Text = rs["DOB"].ToString();
                    txt_Contact2.Text = rs["Contact"].ToString();
                    txt_NIC2.Text = rs["NIC"].ToString();
                    Gender = rs["Gender"].ToString();

                    if (Gender == "Male")
                    {
                        RA_Male.Checked = true;
                        RA_Female.Checked = false;
                    }
                    else
                    {
                        RA_Female.Checked = true;
                        RA_Male.Checked = false;
                    }


                    MessageBox.Show("Registration Found");
                }
                else
                {
                    txt_F_Name2.Text = "";
                    txt_L_Name2.Text = "";
                    txt_DOB2.Text = "";
                    txt_Contact2.Text = "";
                    txt_NIC2.Text = "";
                    RA_Female.Checked = false;
                    RA_Male.Checked = false;
                    MessageBox.Show("Registration not found");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured" + ex);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String Reg_ID = Search_REG_ID.Text;
            String F_Name = txt_F_Name2.Text;
            String L_Name = txt_L_Name2.Text;
            String DOB = txt_DOB2.Text;
            String Contact = txt_Contact2.Text;
            String NIC = txt_NIC2.Text;
            String Gender = "";

            if (RA_Male.Checked)
            {
                Gender = "Male";
                RA_Male.Checked = true;
                RA_Female.Checked = false;
            }
            else
            {
                Gender = "Female";
                RA_Female.Checked = true;
                RA_Male.Checked = false;
            }

            int Rows = us.UpdateRegistrations(F_Name,L_Name,DOB,Contact,NIC, Gender,Reg_ID);

            if (Rows > 0) {

                MessageBox.Show("Record Successfuly Updated ", "Register Participant", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else {

                MessageBox.Show("Record Failed to Update ", "Register Participant", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserMainMenu main = new UserMainMenu();
            main.Show();

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Reg_ID.Text = us.AutoID();
            txt_Firstname.Text = "";
            txt_L_Name.Text = "";
            txt_NIC.Text = "";
            txt_Contact.Text = "";

            RA_Male.Checked = false;
            RA_Female.Checked = false;
        }
    }
}
