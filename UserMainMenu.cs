using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Event_Management_System
{
    public partial class UserMainMenu : Form
    {
        public UserMainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Event_Management inter = new Event_Management();
            inter.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            R_Participant inter = new R_Participant();
            inter.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Client_Management inter = new Client_Management();
            inter.Show();
        }

       

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Do you want to Logout? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (Confirm == DialogResult.Yes)
            {
                this.Hide();
                Login Log = new Login();
                Log.Show();
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Do you want to Exit Application? ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

            if (Confirm == DialogResult.Yes)
            {
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
