using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Event_Management_System
{
    class Users
    {
        private String F_Name;
        private String L_Name;
        private String Contact;
        private String NIC;
        private String Username;
        private String Password;
        private String Usertype;

        DBConnection obj = new DBConnection();
    //Setters and Getter
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

        public void setUsername(String username)
        {
            Username = username;
        }

        public String getUsername()
        {
            return Username;
        }

        public void setPassword(String password)
        {
            Password = password;
        }

        public String getPassword()
        {
            return Password;
        }

        public void setUsertype(String usertype)
        {
            Usertype = usertype;
        }

        public String getUsertype()
        {
            return Usertype;
        }
        //Registration of User
        public int Inserting()
        {
            int NoRows = 0;
            try
            {
                String sql = "insert into Login values('" + getF_Name() + "','" + getL_Name() + "','" 
                    + getContact() + "','" + getNIC() + "','" + getUsername() + "','" + getPassword() + "','" + getUsertype() + "');";

                NoRows = obj.Insertvalues(sql);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data or Username already exists" + ex);
            }
            return NoRows;
        }
        //Searching User Method
        public SqlDataReader Search()
        {
            
            SqlDataReader User = null;
            String sql = "Select Usertype,F_Name,L_Name,NIC from Login where Username = '" + getUsername() + "';";
            try
            {
                User = obj.Searchvalues(sql);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in searching User "+ex, "ManageLogin Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return User;
        }
        //Validation of Login Form
        public Boolean Validation(String Confirm_P)
        {
            String Password = getPassword();
            bool valid = true;
              if (string.IsNullOrEmpty(getF_Name()) || string.IsNullOrEmpty(getL_Name()) ||
                string.IsNullOrEmpty(getContact()) || string.IsNullOrEmpty(getNIC()) ||
                string.IsNullOrEmpty(getUsername()) || string.IsNullOrEmpty(getPassword()) ||
                string.IsNullOrEmpty(Confirm_P))
            {
                MessageBox.Show("All Blanks should be filled");
                valid = false;
            }
            else if (getContact().Length > 10 || getContact().Length < 10)
            {
                MessageBox.Show("Number should contain 10 numbers");
                valid = false;
            }
            else if (Password != Confirm_P)
            {
                MessageBox.Show("Password and Confirm Password Should be matched", "Create Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            else
            {
                valid = true;
            }
            return valid;
        }

        //Validation of Password Verfication
        public Boolean Validation(String Password, String Newpass)
        {
            bool valid = true;
            if (Password != Newpass)
            {
                MessageBox.Show("New Password and Confirm Password Should be matched", "Login Management",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            else if (string.IsNullOrEmpty(Password)|| string.IsNullOrEmpty(Newpass))
            {
                MessageBox.Show("Password Field should not be empty", "Login Managemnt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            else
            {
                valid = true;
            }
            return valid;
        }

        //Update Username
        public int ChangeUsername(String Username) {
            int Rows=0;

            String SQL = "update Login set Username  ='" + getUsername() + "' where Username='" + Username + "';";
            try {
                Rows = obj.Insertvalues(SQL);

            }
            catch (Exception ex) {
                MessageBox.Show("Error in updation of Username"+ex, "Manage Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Rows;
        }

        //Update Password
        public int ChangePassword()
        {
            int Rows = 0;

            String SQL = "update Login set Password  ='" + getPassword() + "' where Username='" + getUsername() + "';";
            try
            {
                Rows = obj.Insertvalues(SQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in updation of Password" + ex, "Manage Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Rows;
        }
        //Generation of ID in Registration Form
        public String AutoID()
        {
            string IDS = "";
            try
            {
                String SQL = "Select Reg_ID from Registration";
                SqlDataReader dr = obj.Searchvalues(SQL);
                string id = "";
                
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr["Reg_ID"].ToString();
                    }
                    string idString = id.Substring(1); //001
                    int CTR = Int32.Parse(idString); //1
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        IDS = "R00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        IDS = "R0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        IDS = "R" + CTR;
                    }
                }
                else
                {
                    IDS = "R001";
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Generating AutoID" + ex, "Registration Participant Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return IDS;
        }

        //Register Participants
        public int Inserting(String Reg_ID,String F_name, String L_Name, String Contact, String NIC, String Gender,String DOB) {
            int NoRows = 0;
            try
            {
                String sql = "insert into Registration values('" + Reg_ID + "','" + F_name + "','" + L_Name + "','" + DOB + "','" 
                    + Contact + "','" + NIC + "','" + Gender + "');";

                NoRows = obj.Insertvalues(sql);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Registering Participants" + ex, "Registration Participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return NoRows;
        }
        //Generation of ID to Participants
        public String AutoIDParticipants()
        {
            string IDS = "";
            try
            {
                String SQL = "Select Participant_ID from Participant";
                SqlDataReader dr = obj.Searchvalues(SQL);
                string id = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr["Participant_ID"].ToString();
                    }
                    string idString = id.Substring(1); //001
                    int CTR = Int32.Parse(idString); //1
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        IDS = "P00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        IDS = "P0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        IDS = "P" + CTR;
                    }
                }
                else
                {
                    IDS = "P001";
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Generating AutoID" + ex, "Student Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return IDS;
        }

        //Searching Registrations

        public SqlDataReader Search(String REG_ID)
        {

            SqlDataReader User = null;
            String sql = "Select * from Registration where Reg_ID = '" + REG_ID + "';";
            try
            {
                User = obj.Searchvalues(sql);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in searching User " + ex, "ManageLogin Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return User;
        }

        //Inserting Participant to the Event
        public int Inserting(String ParticipantID,String Event_ID, String Reg_ID)
        {
            int NoRows = 0;
            try
            {
                String sql = "insert into Participant values('" + ParticipantID + "','" + Event_ID + "','" + Reg_ID + "');";

                NoRows = obj.Insertvalues(sql);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data " + ex);
            }
            return NoRows;
        }

        //No.of Participants in an event
        public int ParticipantNumber(String Reg_ID,String Event_ID)
        {
            String SQL = ("select count(Reg_ID)from Participant where Reg_ID = '" + Reg_ID + "'and Event_ID = '"+ Event_ID + "'");
            int value = obj.Scalar(SQL);
            return value;

        }

        //Remove Participant in an event
        public int Remove(String Reg_ID, String Event_ID)
        {
            String SQL = ("delete from Participant where Reg_ID = '" + Reg_ID + "'and Event_ID = '" + Event_ID + "'");
            int value = obj.Insertvalues(SQL);
            return value;

        }

        //Update Registration Details

        public int UpdateRegistrations(String F_Name,String L_Name, String DOB, String Contact, String NIC, String Gender, String Reg_ID)
        {
            int NoRows = 0;
            try
            {
                String SQL = "update Registration set F_Name = '" + F_Name + "',L_Name = '" + L_Name + "',DOB = '" 
                    + DOB + "',Contact = '" + Contact + "',NIC = '" + NIC + "',Gender = '" + Gender + "' where Reg_ID = '" + Reg_ID + "'";

                NoRows = obj.Insertvalues(SQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Updating Costumer" + ex, "Registration Participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return NoRows;
        }

        //Validation of Participant
        public Boolean Validation(String FirstName, String LastName, String NIC,String Contact)
        {
            bool valid = true;
            
             if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(NIC) || string.IsNullOrEmpty(Contact))
            {
                MessageBox.Show("All fields should be filled", "Participant Registration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
           else if (Contact.Length > 10 || Contact.Length < 10)
            {
                MessageBox.Show("Contact Should be 10 numbers", "Participant Registration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }

            else
            {
                valid = true;
            }
            return valid;
        }
    }
}
