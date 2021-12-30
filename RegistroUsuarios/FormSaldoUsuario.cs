using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace RegistroUsuarios
{
    public partial class FormSaldoUsuario : Form
    {
        public static ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuario = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        public string User;
        public FormSaldoUsuario(string user, ClaseRegistroUsuarios.ListaUsuariosDoble listausuario)
        {
            User = user;
            ListaUsuario = listausuario;
            InitializeComponent();
        }

        private void FormSaldoUsuario_Load(object sender, EventArgs e)
        {
            
            //txtNTarjeta.Text = "    " + "/" + "    " + "/" + "    ";
            //txtFecha.Text="  " +"/" + "  ";
            //txtCvv.Text ="   "+"";
            ClaseRegistroUsuarios.NodoUsuarios recorrido = ListaUsuario.listaDoble;
            while (recorrido != null)
            {
                if( User== recorrido.Usuario)
                {
                    lblSaldo.Text = Convert.ToString(recorrido.Saldo);
                    return;
                }
                
                recorrido = recorrido.sgte;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidadVAcio())
            {
                MessageBox.Show("Rellenar los espacios Vacios", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ClaseRegistroUsuarios.NodoUsuarios recorrido = ListaUsuario.listaDoble;
            double recarga;
            while (recorrido != null)
            {
                if (User == recorrido.Usuario)
                {
                     recarga= Convert.ToDouble(lblSaldo.Text)+ Convert.ToDouble(txtMonto.Text);
                     recorrido.Saldo = recarga;
                     lblSaldo.Text = Convert.ToString(recarga);
                     limpiarTXT();
           
                     SoundPlayer Player = new SoundPlayer();
                     Player.Stream = Properties.Resources.recarga;
                     Player.Play();
                    return;
                }

                recorrido = recorrido.sgte;
            }
            
           
        }
        public bool limpiarTXT()
        {
            txtMonto.Text = "";
            txtCorreo.Text = "";
            txtNTarjeta.Text = "";
            txtFecha.Text = "";
            txtCvv.Text = "";
            return true;
        }
        public bool ValidadVAcio()
        {
            if (txtMonto.Text == "" || txtCorreo.Text == "" || txtNTarjeta.Text == "" || txtFecha.Text == "" || txtCvv.Text == "" )
                
            return true;
            else
                return false;
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar>=32 && e.KeyChar<=47 || e.KeyChar>=58&& e.KeyChar <= 255)
            {
                MessageBox.Show("Solo numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Solo numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Solo numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCvv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Solo numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        int posY = 0;
        int posX = 0;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
