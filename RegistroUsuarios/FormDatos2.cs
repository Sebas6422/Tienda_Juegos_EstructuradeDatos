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
    public partial class FormDatos2 : Form
    {
        ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuario = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        public FormDatos2(ClaseRegistroUsuarios.ListaUsuariosDoble listausuarios)
        {
            ListaUsuario = listausuarios;
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (txtcontrasena.PasswordChar == '*')
                {
                    txtcontrasena.PasswordChar = '\0';
                }
            }
            else 

            {
                txtcontrasena.PasswordChar = '*';
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            
            this.Close();
            this.Owner.Show();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            //Verificacion de datos
            ClaseRegistroUsuarios.NodoUsuarios recorrido = ListaUsuario.listaDoble;
            int codaux;
            string nomaux;
            string usuaux;
            string contraaux;
            double saldoaux;
            string preguntaaux;
            string respuestaaux;
            try
            {
                codaux = int.Parse(txtcodigo.Text);

            }
            catch (Exception)
            {
                MessageBox.Show("Ingrese correctamente el codigo del usuario", "E R R O R", MessageBoxButtons.OK);
                return;
            }
            nomaux = txtnombre.Text;
            usuaux = txtusuario.Text;
            contraaux = txtcontrasena.Text;
            saldoaux = 20.00;
            preguntaaux = comboPreguntas.Text;
            respuestaaux = txtRespuesta.Text;
            if (EstaVacio())
            {
                MessageBox.Show("Ingrese Correctamente los datos del Usuario", "E R R O R");
                return;
            }

            List<string> auxLista = new List<string>();
            if (recorrido == null)
            {
                string auxRuta = string.Empty;
                DialogResult Respuesta = MessageBox.Show("Desea Agregar Imagen al Usuario?", "Confirmacion de Imagen", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (Respuesta == DialogResult.Yes)
                {
                    OpenFileDialog abrirImagen = new OpenFileDialog();
                    if (abrirImagen.ShowDialog() == DialogResult.OK)
                    {
                        auxRuta = abrirImagen.FileName;
                    }
                    auxLista.Add("");
                    ListaUsuario.insertarInicioDoble(codaux, nomaux, usuaux, contraaux, saldoaux, preguntaaux, respuestaaux, auxRuta,auxLista,0);
                    txtcodigo.Clear();
                    txtcontrasena.Clear();
                    txtnombre.Clear();
                    txtusuario.Clear();
                    txtcodigo.Focus();
                    MessageBox.Show("Se ingreso correctamente el Usuario", "USUARIO REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    auxLista.Add("");
                    ListaUsuario.insertarInicioDoble(codaux, nomaux, usuaux, contraaux, saldoaux, preguntaaux, respuestaaux, "", auxLista, 0);
                    txtcodigo.Clear();
                    txtcontrasena.Clear();
                    txtnombre.Clear();
                    txtusuario.Clear();
                    txtcodigo.Focus();
                    MessageBox.Show("Se ingreso correctamente el Usuario", "USUARIO REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                

                return;
            }
            else
            {
                List<string> auxListau = new List<string>();
                while (recorrido != null)
                {
                    if (codaux == recorrido.Codigo || usuaux == recorrido.Usuario)
                    {
                        MessageBox.Show("El usuario ya fue registrado", "E R R O R", MessageBoxButtons.OK);
                        return;
                    }
                    if (codaux != recorrido.Codigo && usuaux != recorrido.Usuario)
                    {
                        string auxRuta = string.Empty;
                        DialogResult Respuesta = MessageBox.Show("Desea Agregar Imagen al Usuario?", "Confirmacion de Imagen", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        auxLista.Add("");
                        if (Respuesta == DialogResult.Yes)
                        {
                            OpenFileDialog abrirImagen = new OpenFileDialog();
                            if (abrirImagen.ShowDialog() == DialogResult.OK)
                            {
                                auxRuta = abrirImagen.FileName;
                            }
                            ListaUsuario.insertarInicioDoble(codaux, nomaux, usuaux, contraaux, saldoaux, preguntaaux, respuestaaux, auxRuta,auxLista,0);
                            txtcodigo.Clear();
                            txtcontrasena.Clear();
                            txtnombre.Clear();
                            txtusuario.Clear();
                            txtcodigo.Focus();
                            comboPreguntas.Text = "";
                            txtRespuesta.Clear();
                            MessageBox.Show("Se ingreso correctamente el Usuario", "USUARIO REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        return;

                    }
                    recorrido = recorrido.sgte;
                }
               
            }
           
        }

        //Verificar que los datos esten llenos en los text box
        public bool EstaVacio()
        {
            if (txtcodigo.Text == "" || txtcontrasena.Text == "" || txtnombre.Text == "" || txtusuario.Text == "" || comboPreguntas.Text =="" || txtRespuesta.Text =="")
                return true;
            else
                return false;  
            
        }

        private void FormDatos2_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FormDatos2_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = comboPreguntas.SelectedIndex;

            string seleccionado = comboPreguntas.Items[indice].ToString();
            if (seleccionado != "")
            {
                lblRespuesta.Visible = true;
                txtRespuesta.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Show();
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

        private void txtcodigo_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtcodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Ingrese correctamente el Codigo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
