using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Event_Management_System
{
    class Event
    {
        private String E_ID;
        private String E_Name;
        private String Expected_P;
        private String Scalee;
        private String Date;
        private String Organized_C;

        DBConnection obj = new DBConnection();
        //Setters and Getters
        public void setE_ID(String e_ID)
        {
            E_ID = e_ID;
        }

        public String getE_ID()
        {
            return E_ID;
        }
        public void setE_Name(String e_Name)
        {
            E_Name = e_Name;
        }

        public String getE_Name()
        {
            return E_Name;
        }
        public void setExpected_P(String expected_P)
        {
            Expected_P = expected_P;
        }

        public String getExpected_P()
        {
            return Expected_P;
        }
       
      
        public void setScalee(String scalee)
        {
            Scalee = scalee;
        }

        public String getScalee()
        {
            return Scalee;
        }
        public void setDate(String date)
        {
            Date = date;
        }

        public String getDate()
        {
            return Date;
        }

        public void setOrganized_C(String organized_C)
        {
            Organized_C = organized_C;
        }

        public String getOrganized_C()
        {
            return Organized_C;
        }

        //Display No.Events of the day
        public int EventBooked()
        {
            String SQL = ("select count(Event_ID)from Events where Scheduled_Date = '" + getDate() + "' and Status ='Active'");
            int date = obj.Scalar(SQL);
            return date;

        }

        //Display Total Scale of the day
        public int Scale()
        {
            String SQL = ("select Sum(isnull(cast(Scale as float), 0)) from Events where Scheduled_Date = '" 
                + getDate() + "' and Status = 'Active'");
            int Scale = obj.Scalar(SQL);
            
            return Scale;

        }

        //Generate ID for the event
        public String AutoID()
        {
            string IDS = "";
            try
            {
                String SQL = "Select Event_ID from Events";
                SqlDataReader dr = obj.Searchvalues(SQL);
                string id = "";

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = dr["Event_ID"].ToString();
                    }
                    string idString = id.Substring(1); //001
                    int CTR = Int32.Parse(idString); //1
                    if (CTR >= 1 && CTR < 9)
                    {
                        CTR = CTR + 1;
                        IDS = "E00" + CTR;
                    }
                    else if (CTR >= 9 && CTR < 99)
                    {
                        CTR = CTR + 1;
                        IDS = "E0" + CTR;
                    }
                    else if (CTR >= 99)
                    {
                        CTR = CTR + 1;
                        IDS = "E" + CTR;
                    }
                }
                else
                {
                    IDS = "E001";
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Generating AutoID" + ex, "Event Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return IDS;
        }

        //Get CostumerID in database
       public string getCostumerID(String FirstName)
        {
            string CostumerID = "";
            try
            {
                String SQL = "Select Costumer_ID from Costumer where F_Name = '" + FirstName + "'";
                SqlDataReader dr = obj.Searchvalues(SQL);
                Boolean records = dr.HasRows;
                if (records)
                {
                    while (dr.Read())
                    {
                        CostumerID = dr["Costumer_ID"].ToString();
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data" + ex, "Event Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return CostumerID;
        }

        //Insert Event to database
        public int InsertEvent(String Costumer_ID) {
            String SQL = "insert into Events values('"+getE_ID()+"','" + getE_Name() + "','" + getExpected_P() + "','" + getScalee() + "','" 
                + getDate() + "','" + Costumer_ID + "','Active');";

            int Norows = obj.Insertvalues(SQL);
            return Norows;
        }
        //Search Event to database
        public SqlDataReader SearchEvent()
        {
            
            SqlDataReader Event = null;
            String sql = "Select * from Events where Event_ID = '" + getE_ID() + "';";
            try
            {
                Event = obj.Searchvalues(sql);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error in searching User " + ex, "ManageLogin Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Event;
        }

        //Validation for Event Registration
        public Boolean Validation() {
            bool valid = true;
            if (string.IsNullOrEmpty(getE_Name()) && string.IsNullOrEmpty(getExpected_P()) && string.IsNullOrEmpty(getScalee()))
            {
                MessageBox.Show("Fields should be filled  ", "Event Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            else if (string.IsNullOrEmpty(getE_Name()))
            {
                MessageBox.Show("Enter an Event Name ", "Event Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            else if (string.IsNullOrEmpty(getExpected_P()))
            {
                MessageBox.Show("Enter Number of Expected Participants ", "Event Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            else if (string.IsNullOrEmpty(getScalee()))
            {
                

                MessageBox.Show("Enter a scale ", "Event Management Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
                valid = false;
            }
            else
            {
                Int32 Scale = Convert.ToInt32(getScalee());
                if (Scale > 100) {
                    MessageBox.Show("Enter a scale between 1 - 100 % by consideration of the  day ", "Event Management Form", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    valid = false;
                }
                
            }
                return valid;
            }

        //Update Event Details

        public int UpdateEvent() {
            

            int NoRows = 0;
            try
            {
                String SQL = "update Events set E_Name = '" + getE_Name() + "',Expected_P = '" + getExpected_P() + "',Scale = '" 
                    + getScalee() + "',Scheduled_Date = '" + getDate() + "' where Event_ID = '"+getE_ID()+"'";

                NoRows = obj.Insertvalues(SQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Updating Event" + ex, "Registration Participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return NoRows;
        }

        //Display No.Active Participants 
        public int NoofParticipants()
        {
            String SQL = ("select count(Participant_ID)from Participant where Event_ID = '" + getE_ID() + "'");
            int value = obj.Scalar(SQL);
            return value;

        }
        //Deactivate Event
        public int Deactivate()
        {

            int NoRows = 0;
            try
            {
                String SQL = "update Events set Status = 'Deactivate' where Event_ID = '" + getE_ID() + "'";

                NoRows = obj.Insertvalues(SQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Deactivating Event" + ex, "Registration Participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return NoRows;
        }
        //Activae Event
        public int Activate()
        {


            int NoRows = 0;
            try
            {
                String SQL = "update Events set Status = 'Active' where Event_ID = '" + getE_ID() + "'";

                NoRows = obj.Insertvalues(SQL);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Deactivating Event" + ex, "Registration Participants", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return NoRows;
        }


    }

    }

       
        
    


