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
    public partial class FormUsuario : Form
    {
        ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuarios = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        ClasePilaBlibioteca.PilaBiblioteca PilaBiblioteca = new ClasePilaBlibioteca.PilaBiblioteca();
        string User;
        public FormUsuario(string user, ClaseRegistroUsuarios.ListaUsuariosDoble listaus, ClasePilaBlibioteca.PilaBiblioteca pilaBiblioteca)
        {
            ListaUsuarios = listaus;
            PilaBiblioteca = pilaBiblioteca;
            User = user;
            InitializeComponent();
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            MostrarBiblioteca();
            ClaseRegistroUsuarios.NodoUsuarios recorridoUs = ListaUsuarios.listaDoble;
            while (recorridoUs != null)
            {
                if (User == recorridoUs.Usuario)
                {
                    picBoxImagenUsuario.ImageLocation = recorridoUs.RutaImagen;
                    picBoxImagenUsuario.SizeMode = PictureBoxSizeMode.StretchImage;
                    lblNombre.Text = recorridoUs.Nombre;
                    lblUsuario.Text = recorridoUs.Usuario;
                    return;
                }
                recorridoUs = recorridoUs.sgte;
            }
        }

        private void btnModificarPErfil_Click(object sender, EventArgs e)
        {
            lblnNombre.Visible = true;
            lblnUsuario.Visible = true;
            txtNNombre.Visible = true;
            txtNUsuario.Visible = true;
            btnAceptar.Visible = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtNNombre.Text == "" || txtNUsuario.Text=="")
            {
                MessageBox.Show("Agregue correctamten los datos", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                ClaseRegistroUsuarios.NodoUsuarios recorridoUs = ListaUsuarios.listaDoble;
                while (recorridoUs != null)
                {
                    if (User == recorridoUs.Usuario)
                    {
                        recorridoUs.Usuario = txtNUsuario.Text;
                        recorridoUs.Nombre = txtNNombre.Text;
                        User = recorridoUs.Usuario;
                        lblUsuario.Text = recorridoUs.Usuario;
                        lblNombre.Text = recorridoUs.Nombre;
                        MessageBox.Show("Se cambio exitosamente", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNNombre.Clear();
                        txtNUsuario.Clear();
                        lblnNombre.Visible = false;
                        lblnUsuario.Visible = false;
                        txtNNombre.Visible = false;
                        txtNUsuario.Visible = false;
                        btnAceptar.Visible = false;
                        return;
                    }
                    recorridoUs = recorridoUs.sgte;
                }

            }
           
            
        }

        private void btnCambiarImagen_Click(object sender, EventArgs e)
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
                ClaseRegistroUsuarios.NodoUsuarios recorridoUs = ListaUsuarios.listaDoble;
                while (recorridoUs != null)
                {
                    if (User == recorridoUs.Usuario)
                    {
                        recorridoUs.RutaImagen = auxRuta;
                        picBoxImagenUsuario.ImageLocation = recorridoUs.RutaImagen;
                        picBoxImagenUsuario.SizeMode = PictureBoxSizeMode.StretchImage;
                        return;
                    }
                    recorridoUs = recorridoUs.sgte;
                }
            }
        }

        void CrearDGVBiblioteca()
        {
            DGVPilaBiblioteca.Rows.Clear();
            DGVPilaBiblioteca.Columns.Clear();

            DGVPilaBiblioteca.Columns.Add("Nombre del juego", "Nombre del juego");
        }

        void MostrarBiblioteca()
        {
            ClasePilaBlibioteca.NodoBiblioteca recorrido = PilaBiblioteca.pila;
            CrearDGVBiblioteca();


            while (recorrido != null)
            {
                if (recorrido.User == User)
                {
                    DGVPilaBiblioteca.Rows.Add(recorrido.nombrebiblioteca);
                }
                recorrido = recorrido.sgte;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void FormUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMenuUsuario.User = User;
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
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

        private void FormUsuario_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
