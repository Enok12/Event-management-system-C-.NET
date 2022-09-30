using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Event_Management_System
{
    class Costumer
    {
        private String C_ID;
        private String F_Name;
        private String L_Name;
        private String Contact;
        private String NIC;
        private String DOB;
        private String Gender;

        DBConnection obj = new DBConnection();

        //Setters and Getters
        public void setC_ID(String c_ID)
        {
            C_ID = c_ID;
        }

        public String getC_ID()
        {
            return C_ID;
        }

        public void setF_Name(String f_Name)
        {
            F_Name = f_Name;
        }

        public String getF_Name()
        {
            return F_Name;
        }

        public void setL_Name(String l_Name)
        {
            L_Name = l_Name;
        }

        public String getL_Name()
        {
            return L_Name;
        }

        public void setContact(String contact)
        {
            Contact = contact;
        }

        public String getContact()
        {
            return Contact;
        }

        public void setNIC(String nic)
        {
            NIC = nic;
        }

        public String getNIC()
        {
            return NIC;
        }

        public void setDOB(String dob)
        {
            DOB = dob;
        }

        public String getDOB()
        {
            return DOB;
        }

        public void setGender(String gender)
        {
            Gender = gender;
        }

        public String getGender()
        {
            return Gender;
        }

        //Generation of ID to Costumer
        public String AutoID()
        {
            string IDS = "";
            try
            {
                String SQL = "Select Costumer_ID from Costumer";
                SqlDataReader dr = obj.Searchvalues(SQL);
                string id = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr["Costumer_ID"].ToString();
                    }
                    string idString = id.Substring(1); //001
                    int CTR = Int32.Parse(idString); //1
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        IDS = "C00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        IDS = "C0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        IDS = "C" + CTR;
                    }
                }
                else
                {
                    IDS = "C001";
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Generating AutoID" + ex, "Student Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return IDS;
        }
        //Registering Costumer to the database

        public int Insert_Costumer() {
            String SQL = "insert into Costumer values('" + getC_ID() + "','" + getF_Name() + "','" + getL_Name() + "','" 
                + getDOB() + "','" + getContact() + "','" + getNIC() + "','" + getGender() + "');";
            int Norows=0;
            try
            {
                 Norows = obj.Insertvalues(SQL);
            }
            catch (Exception ex) {

                MessageBox.Show("Error in inserting values "+ex, "Register Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Norows;
        }

        //Validation in Costumer Form
        public Boolean validation()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(getF_Name()) || string.IsNullOrEmpty(getL_Name()) || string.IsNullOrEmpty(getNIC())
               || string.IsNullOrEmpty(getContact()))
            {
                MessageBox.Show("Fields Should not be blank", "Client Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            else if (getContact().Length > 10)
            {
                MessageBox.Show("Contact Number should contain 10 numbers", "Client Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            else {
                valid = true;
            }
            return valid;
        }

        //Search Costumer
        public SqlDataReader SearchCostumer()
        {

            SqlDataReader Costumer = null;
            String sql = "Select * from Costumer where Costumer_ID = '" + getC_ID() + "';";
            try
            {
                Costumer = obj.Searchvalues(sql);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in searching User " + ex, "ManageLogin Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Costumer;
        }

        //Update Costumer Details

        public int UpdateCostumer()
        { 
            int NoRows = 0;
            try
            {
                String SQL = "update Costumer set F_Name = '" + getF_Name() + "',L_Name = '" + getL_Name() + "',DOB = '"
                    + getDOB() + "',Contact = '" + getContact() + "',NIC = '" + getNIC() + "',Gender = '" 
                    + getGender() + "' where Costumer_ID = '" + getC_ID() + "'";

                NoRows = obj.Insertvalues(SQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Updating Costumer" + ex, "Registration Participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return NoRows;
        }
    }
}
