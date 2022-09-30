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

    public partial class AddRem_Participants : Form
    {
        Users us = new Users();
        DBConnection obj = new DBConnection();
        SqlConnection SQL;
        public AddRem_Participants()
        {
            InitializeComponent();
            SQL = obj.getConnection();

        }

        private void AddRem_Participants_Load(object sender, EventArgs e)
        {

            txt_Par_ID.Text = us.AutoIDParticipants();
            R_Male.Enabled = false;
            R_Female.Enabled = false;
            Display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Gender = "";
            try
            {
                SqlDataReader rs = us.Search(txt_Reg_ID.Text);
                if (rs.Read())
                {

                    L_F_Name.Text = rs["F_Name"].ToString();
                    L_L_Name.Text = rs["L_Name"].ToString();
                    L_DOB.Text = rs["DOB"].ToString();
                    L_Contatct.Text = rs["Contact"].ToString();
                    L_NIC.Text = rs["NIC"].ToString();
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


                    MessageBox.Show("Registration Found");
                }
                else
                {
                    MessageBox.Show("Registration not found");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured" + ex);

            }
        }

        //Display Participants to Datagridview
        private void Display()
        {
            try
            {
                SqlCommand Searchevent = new SqlCommand("select Participant_ID,Reg_ID from Participant where Event_ID = '" + Event_Management.Event_ID + "'", SQL);

                SqlDataAdapter da = new SqlDataAdapter(Searchevent);
                DataSet ds = new DataSet();
                da.Fill(ds, "Participant");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Participant";

            }
            catch (Exception ex)
            {
                MessageBox.Show("error Searching Date " +
          ex, "Event Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            int Pariticipant = us.ParticipantNumber(txt_Reg_ID.Text, Event_Management.Event_ID);

            if (Pariticipant >= 1)
            {
                MessageBox.Show("Participant Already Registered");
            }
            else
            {
                int Rows = us.Inserting(txt_Par_ID.Text, Event_Management.Event_ID, txt_Reg_ID.Text);

                if (Rows > 0)
                {

                    MessageBox.Show("Participant Added", "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Display();
                }
                else
                {
                    MessageBox.Show("Failed to Add", "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            txt_Par_ID.Text = us.AutoIDParticipants();

            txt_Reg_ID.Text = "";
            L_F_Name.Text = "";
            L_L_Name.Text = "";
            L_DOB.Text = "";
            L_Contatct.Text = "";
            L_NIC.Text = "";

            R_Male.Checked = false;
            R_Female.Checked = false;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                txt_Reg_ID.Text = row.Cells["Reg_ID"].Value.ToString();

            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            int Rows = us.Remove(txt_Reg_ID.Text,Event_Management.Event_ID);

            if (Rows > 0)
            {
                MessageBox.Show("Participant Removed", "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Display();

            }
            else {
                MessageBox.Show("Participant Failed to Remove", "Event Management ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Do you want to Add more Participants? ", "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Error);

            if (Confirm == DialogResult.No)
            {
                this.Hide();
            }
        }
    }
    }

    

