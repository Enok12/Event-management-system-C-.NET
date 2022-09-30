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
    public partial class Event_Management : Form
    {
        Event eve = new Event();
        DBConnection obj = new DBConnection();
        Costumer co = new Costumer();
        SqlConnection SQL;
        public Event_Management()
        {
            InitializeComponent();
            SQL = obj.getConnection();
        }
         int TotalScale = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                eve.setDate(dateTimePicker1.Text.ToString());
                int Scale = eve.Scale();

                L_Scale.Text = Scale.ToString();



            }
            catch (Exception ex)
            {
                L_Scale.Text = "0";
            }
            try {
                eve.setDate(dateTimePicker1.Text.ToString());
                eve.setE_Name(txt_E_Name.Text);
                eve.setScalee(txt_Scale.Text);
                eve.setExpected_P(txt_E_P.Text);

                bool valid = eve.Validation();
                if (valid)
                {
                    int count = eve.EventBooked();
                    if (count == 0)
                    {
                        MessageBox.Show("No events booked");
                        TotalScale = 0;
                        ScaleDisplay();
                        Display();
                        if (btn_Addevent.Enabled == false)
                        {
                            btn_Addevent.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show(count + " Event/s are booked");
                        TotalScale = 0;
                       // Int32 TextScale = Convert.ToInt32(txt_Scale.Text);
                        //TotalScale = eve.Scale() + TextScale;
                        ScaleDisplay();
                        Display();

                        if (btn_Addevent.Enabled ==false) {
                            btn_Addevent.Enabled = true;
                        }
                    }
                }

                }

                
            catch (Exception ex) {
                MessageBox.Show("error " +
          ex, "Student Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Display Events to Datagridview
        private void Display() {
            try
            {
                SqlCommand Searchevent = new SqlCommand("select Event_ID,E_Name,Expected_P,Costumer_ID,Scale from Events where Scheduled_Date = '" + dateTimePicker1.Text.ToString() + "' and Status = 'Active'", SQL);

                SqlDataAdapter da = new SqlDataAdapter(Searchevent);
                DataSet ds = new DataSet();
                da.Fill(ds, "Events");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Events";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error Searching Date " +
          ex, "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       //Display Scale
        private void ScaleDisplay() {
            try
            {
                eve.setDate(dateTimePicker1.Text.ToString());
                int Scale = eve.Scale();

                L_Scale.Text = Scale.ToString();



            }
            catch (Exception ex)
            {
                L_Scale.Text = "0";
            }
        }
           
        private void Event_Management_Load(object sender, EventArgs e)
        {

            btn_AddRem.Enabled = false;
            btn_Addevent.Enabled = false;
            txt_E_ID.Text = eve.AutoID();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            dateTimePicker2.CustomFormat = "MM/dd/yyyy";
            dateTimePicker3.CustomFormat = "MM/dd/yyyy";

            try
            {
                SqlCommand cmd = new SqlCommand("Select F_Name from Costumer", SQL);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds, "Costumer");

                comboO_CLient.DataSource = ds;
                comboO_CLient.DisplayMember = "Costumer.F_Name";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Event Management Form Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        
        private void btn_Addevent_Click(object sender, EventArgs e)
        {
            try
            {
                eve.setDate(dateTimePicker1.Text.ToString());
                int Scale = eve.Scale();
                Int32 TextScale = Convert.ToInt32(txt_Scale.Text);
                TotalScale = Scale + TextScale;

            }
            catch (Exception ex)
            {
                TotalScale = 0;
            }

            eve.setE_ID(txt_E_ID.Text);
            eve.setE_Name(txt_E_Name.Text);
            eve.setExpected_P(txt_E_P.Text);
            eve.setScalee(txt_Scale.Text);
            eve.setDate(dateTimePicker1.Text.ToString());

            string CostumerID = eve.getCostumerID(comboO_CLient.Text);
            bool valid = eve.Validation();
            if (valid)
            {
                if (TotalScale <= 100)
            {
                int Rows = eve.InsertEvent(CostumerID);
                if (Rows > 0)
                {
                        ScaleDisplay();
                        Display();
                        MessageBox.Show("Event Successfully Booked");
                }
            }
            else
            {
                MessageBox.Show("Scale is limited. No events can be booked");
            }
                }
        }

        
    private void btn_Search_Click(object sender, EventArgs e)
        {
            
            String E_Name = "";
            String E_Participants = "";
            String S_Date = "";
            String Status = "";
            String Costumer_ID = "";
            eve.setE_ID(Search_E_ID.Text);
            try
            {
                SqlDataReader rs = eve.SearchEvent();
                if (rs.Read())
                {

                    E_Name = rs["E_Name"].ToString();
                    E_Participants = rs["Expected_P"].ToString();
                    S_Date = rs["Scheduled_Date"].ToString();
                    Costumer_ID = rs["Costumer_ID"].ToString();
                    Status = rs["Status"].ToString();



                    if (Status == "Active")
                    {
                        L_E_Name.Text = E_Name;
                        L_E_Paerticipants.Text = E_Participants;
                        L_S_Date.Text = S_Date;
                        L_A_Participants.Text = eve.NoofParticipants().ToString();
                        MessageBox.Show("Event Found");

                        if (btn_AddRem.Enabled == false)
                        {
                            btn_AddRem.Enabled = true;
                        }

                        try
                        {
                            co.setC_ID(Costumer_ID);
                            SqlDataReader dr = co.SearchCostumer();
                            if (dr.Read())
                            {

                                L_Costumer.Text = dr["F_Name"].ToString();
                            }
                            else
                            {
                                Console.Write("Costumer not found");
                            }


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error occured" + ex);

                        }

                        int value1 = eve.NoofParticipants();
                        Int32 value2 = Convert.ToInt32(L_E_Paerticipants.Text);
                        if (value1 == value2)
                        {

                            MessageBox.Show("Expected Participant Limit has been reached");
                        }
                    }
                    else if (Status == "Deactivate")
                    {
                        btn_AddRem.Enabled = false;
                        L_E_Name.Text = "";
                        L_E_Paerticipants.Text = "";
                        L_S_Date.Text = "";
                        L_A_Participants.Text = "";
                        L_Costumer.Text = "";
                        MessageBox.Show("Event not Found");
                    }

                }
                else {
                    btn_AddRem.Enabled = false;
                    L_E_Name.Text = "";
                    L_E_Paerticipants.Text = "";
                    L_S_Date.Text = "";
                    L_A_Participants.Text = "";
                    L_Costumer.Text = "";
                    MessageBox.Show("Event not Found");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured" + ex);

            }

            
        }
        public static String Event_ID; // Variable to send to another form
        private void btn_AddRem_Click(object sender, EventArgs e)
        {
            Event_ID = Search_E_ID.Text;

           
            AddRem_Participants inter = new AddRem_Participants();
            inter.Show();   

        }

        private void button2_Click(object sender, EventArgs e)
        {
            eve.setDate(dateTimePicker3.Text.ToString());
            int count = eve.EventBooked();
            if (count == 0)
            {
                MessageBox.Show("No events booked");
                Display2();
                ScaleDisplayy();
            }
            else
            {
                MessageBox.Show(count + " Event/s are booked");
               
                Display2();
                ScaleDisplayy();

            }

            try
            {
                eve.setDate(dateTimePicker3.Text.ToString());
                int Scale = eve.Scale();

                L_Scale.Text = Scale.ToString();

            }
            catch (Exception ex)
            {
                L_Scale.Text = "0";
            }

        }

        //Display All Events
        public void Display2() {
            try
            {
                SqlCommand cmd = new SqlCommand("Select * from Events where Scheduled_Date = '" + dateTimePicker3.Text.ToString() + "'", SQL);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds, "Events");

                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "Events";
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "Event Management Form Form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            eve.setE_ID(L_E_ID.Text);
            eve.setE_Name(txt_E_Name2.Text);
            eve.setExpected_P(txt_E_P2.Text);
            eve.setScalee(txt_Scale2.Text);
            eve.setDate(dateTimePicker2.Text.ToString());

            int Rows = eve.UpdateEvent();

            if (Rows > 0)
            {
                MessageBox.Show("Event Successfully Updated ", "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display2();
            }
            else {
                MessageBox.Show("Event Failed to Update", "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0) {

                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                L_E_ID.Text = row.Cells["Event_ID"].Value.ToString();
                txt_E_Name2.Text = row.Cells["E_Name"].Value.ToString();
                txt_E_P2.Text = row.Cells["Expected_P"].Value.ToString();
                dateTimePicker2.Text = row.Cells["Scheduled_Date"].Value.ToString();
                txt_Scale2.Text = row.Cells["Scale"].Value.ToString();

            }
        }

        //Scale Display 2

        private void ScaleDisplayy()
        {
            try
            {
                eve.setDate(dateTimePicker3.Text.ToString());
                int Scale = eve.Scale();

                L_Scale2.Text = Scale.ToString();
                if (Scale >= 90) {
                    MessageBox.Show("Note that Scale of Day is reaching/reached the limit. Do not update Scale exceeding the limit");
                }



            }
            catch (Exception ex)
            {
                L_Scale.Text = "0";
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
            txt_E_ID.Text = eve.AutoID();
            txt_E_Name.Text = "";
            txt_E_P.Text = "";
            txt_Scale.Text = "";
            L_Scale.Text = "0";

            if (btn_Addevent.Enabled == true)
            {
                btn_Addevent.Enabled = false;
            }

        }

        private void btn_Deactivate_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Do you want to Deactivate Event? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (Confirm == DialogResult.Yes)
            {
                eve.setE_ID(L_E_ID.Text);
                int Rows = eve.Deactivate();

                if (Rows > 0)
                {
                    MessageBox.Show("Successfully Deactivated ", "Event Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    MessageBox.Show("Failed to Deactivate  ", "Event Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Do you want to Activate Event? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (Confirm == DialogResult.Yes)
            {
                eve.setE_ID(L_E_ID.Text);
                int Rows = eve.Activate();

                if (Rows > 0)
                {
                    MessageBox.Show("Successfully Activated ", "Event Management", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Activate  ", "Event Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }

      
    }
    }

