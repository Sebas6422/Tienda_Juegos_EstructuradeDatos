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
    public partial class FormMenuUsuario : Form
    {

        public static ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos Listajuegos = new ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos();
        public static ClasePilaCarrito.PilaCarrito PilaCarrito = new ClasePilaCarrito.PilaCarrito();
        public static ClasePilaBlibioteca.PilaBiblioteca PilaBiblioteca = new ClasePilaBlibioteca.PilaBiblioteca();
        public static ClaseColaDescargas.ColaDescarga ColaDescarga = new ClaseColaDescargas.ColaDescarga();
        public static ClaseReporteVentas.Bicola BicolaReporte = new ClaseReporteVentas.Bicola();
        public static string User;
        public List<string> listaCategoria = new List<string>();
        public static ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuarios = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        public static ClaseArbolJuegos.ArbolJuegos ArbolJuegos = new ClaseArbolJuegos.ArbolJuegos();
        public static ColaAmigosAgregados.ColaAmigos ColaAmigos = new ColaAmigosAgregados.ColaAmigos();

        public FormMenuUsuario(ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos listajuegos, ClasePilaCarrito.PilaCarrito pilaCarrito, ClasePilaBlibioteca.PilaBiblioteca pilabiblioteca, ClaseColaDescargas.ColaDescarga coladescarga, ClaseReporteVentas.Bicola bicolaReporte, List<string> listacat, string user, ClaseRegistroUsuarios.ListaUsuariosDoble listaUsuarios, ClaseArbolJuegos.ArbolJuegos arbolJuegos, ColaAmigosAgregados.ColaAmigos colaA)
        {
            Listajuegos = listajuegos;
            PilaCarrito = pilaCarrito;
            PilaBiblioteca = pilabiblioteca;
            listaCategoria = listacat;
            ColaDescarga = coladescarga;
            BicolaReporte = bicolaReporte;
            User = user;
            ListaUsuarios = listaUsuarios;
            ArbolJuegos = arbolJuegos;
            ColaAmigos = colaA;
            
            InitializeComponent();
            
        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            FormCarrito Form = new FormCarrito(PilaCarrito, PilaBiblioteca, BicolaReporte, ListaUsuarios, User, ArbolJuegos);
            this.Hide();
            Form.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormJuegosUsuario Form = new FormJuegosUsuario(Listajuegos ,PilaCarrito, listaCategoria,User);
            this.Hide();
            Form.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormBiblioteca Form = new FormBiblioteca(PilaBiblioteca,ColaDescarga, Listajuegos,User);
            this.Hide();
            Form.ShowDialog(this);
        }

        private void FormMenuUsuario_Load(object sender, EventArgs e)
        {
            label1.Text = "Bienvenido " + User;
        }

        private void FormMenuUsuario_Load_1(object sender, EventArgs e)
        {
            label1.Text = "Bienvenido " + User;
            ClaseRegistroUsuarios.NodoUsuarios recorridoUs = ListaUsuarios.listaDoble;
            while (recorridoUs!= null)
            {
                if (User == recorridoUs.Usuario)
                {
                    lblSolicitudes.Text = "Solicitudes " + recorridoUs.ContadorS;
                    picBoxImagenUser.ImageLocation = recorridoUs.RutaImagen;
                    picBoxImagenUser.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                recorridoUs = recorridoUs.sgte;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormSaldoUsuario Form = new FormSaldoUsuario(User, ListaUsuarios);
            this.Hide();
            Form.ShowDialog(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = comboConf.SelectedIndex;

            string item = comboConf.Items[indice].ToString();
            if (item == "Configuracion de Usuario")
            {
                FormUsuario Form = new FormUsuario(User, ListaUsuarios,PilaBiblioteca);
                this.Hide();
                Form.ShowDialog(this);
            }
            if (item == "Historial de Compra")
            {
                FormHistorialDeCompra Form = new FormHistorialDeCompra(User, BicolaReporte);
                this.Hide();
                Form.ShowDialog(this);
            }
            if (item == "Amigos")
            {
                FormAgregarAmigos Form = new FormAgregarAmigos(User, ListaUsuarios,ColaAmigos);
                this.Hide();
                Form.ShowDialog(this);
            }
            if (item == "Cerrar Sesión")
            {
                //FormUsuarioOpciones form = new FormUsuarioOpciones();
                this.Owner.Show();
                this.Close();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}