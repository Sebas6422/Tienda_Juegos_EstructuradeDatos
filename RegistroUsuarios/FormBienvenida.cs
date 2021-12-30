using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroUsuarios
{
    public partial class Bienvenida : Form
    {
        

        public Bienvenida()
        {
            
            InitializeComponent();
        }

        //Evento Timer_Tick que hara correr para llegar a un cierto tiempo para que se pase al siguiente formulario
        private void Timer_Tick(object sender, EventArgs e)
        {

            progressbar.Value += 1; ;

            progressbar.Text = progressbar.Value.ToString();
            //label1.Text = Convert.ToString(cont);
            if (progressbar.Value == 30)
            {
               pictureBox1.Image =Properties.Resources.left_4_dead_2_campaign_posters_by_cybik_d2bd1kh_fullview;            
            }
            if (progressbar.Value == 50)
            {
                pictureBox1.Image = Properties.Resources.The_last_of_us;
            }
            if (progressbar.Value == 80)
            {
                pictureBox1.Image = Properties.Resources.dota;
            }

            if (progressbar.Value == 101)
            {
                Timer.Enabled = false;
                FormLoggin form = new FormLoggin();
                this.Hide();
                form.ShowDialog();
            }
        }

        private void FormBienvenida_FormClosed(object sender, FormClosedEventArgs e)
        {
            Timer.Enabled = false;
        }

        private void FormBienvenida_Load(object sender, EventArgs e)
        {
            Timer.Enabled = true;
            progressbar.Value = 0;
            progressbar.Minimum = 0;
            progressbar.Maximum = 101;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
