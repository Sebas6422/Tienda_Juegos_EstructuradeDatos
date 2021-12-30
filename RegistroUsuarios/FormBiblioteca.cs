using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace RegistroUsuarios
{
    public partial class FormBiblioteca : Form
    {
        public static ClasePilaBlibioteca.PilaBiblioteca PilaBiblioteca = new ClasePilaBlibioteca.PilaBiblioteca();
        public static ClaseColaDescargas.ColaDescarga ColaDescarga = new ClaseColaDescargas.ColaDescarga();
        public static ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos Listajuegos = new ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos();
        public static string User;

        public FormBiblioteca(ClasePilaBlibioteca.PilaBiblioteca pilabiblioteca, ClaseColaDescargas.ColaDescarga coladescarga, ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos listajuegos,string user)
        {
            PilaBiblioteca = pilabiblioteca;
            ColaDescarga = coladescarga;
            Listajuegos = listajuegos;
            User = user;
            InitializeComponent();
        }

       
      

        private void MostrarDataGried()
        {
            ClasePilaBlibioteca.NodoBiblioteca recorrido = PilaBiblioteca.pila;
            //CrearDataGried();
          
         
            while(recorrido != null)
            {
                if (recorrido.User == User)
                {
                    dataGridView1.Rows.Add(recorrido.nombrebiblioteca);
                }
                recorrido = recorrido.sgte;
            }
        }

        

        private void FormBiblioteca_Load(object sender, EventArgs e)
        {

            MostrarDataGried();
        }

        private void AgregaraDescargas(string nom)
        {
            ClasePilaBlibioteca.NodoBiblioteca recorrido = PilaBiblioteca.pila;
            ClaseColaDescargas.NodoColaDescarga recorridocola = ColaDescarga.cola;

            //recorre la biblioteca
            while (recorrido != null)
            {

                if (recorrido.cargadeljuego == 100)
                {
                    MessageBox.Show("El juego ya ah sido descargado");
                }
                else
                {
                    if (nom == recorrido.nombrebiblioteca)
                    {
                        //Cuando el recorrido de la cola descarga esta vacio
                        if (recorridocola == null)
                        {
                            ColaDescarga.queue(recorrido.nombrebiblioteca, recorrido.categoriaBiblioteca, 0, User);
                            MessageBox.Show("Su juego se esta descargando");
                            return;

                        }
                        else
                        {
                            while (recorridocola != null)
                            {
                                if (recorrido.nombrebiblioteca == recorridocola.nombreDescarga && recorrido.User == recorridocola.User)
                                {
                                    MessageBox.Show("El juego ya esta dentro de la cola de descarga");
                                    return;
                                }
                                else
                                {

                                    ColaDescarga.queue(recorrido.nombrebiblioteca, recorrido.categoriaBiblioteca, 0, User);
                                    MessageBox.Show("Su juego se esta descargando");
                                    return;
                                }
                            }
                        }


                    }
                }
                recorrido = recorrido.sgte;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string nom;
                nom = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                AgregaraDescargas(nom);
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormDescargas form = new FormDescargas(ColaDescarga, PilaBiblioteca,User);
            this.Hide();
            form.ShowDialog(this);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string nombreJuego;
                lblJuego.Text = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                nombreJuego = lblJuego.Text;
                MostrarImagen(nombreJuego);
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
                return;
            }
        }

        public void MostrarImagen(string NombreJuego)
        {
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorridoJuego = Listajuegos.lista;

            if (Listajuegos != null)
            {
                while (recorridoJuego != null)
                {
                    if (NombreJuego == recorridoJuego.NombredelJuego)
                    {
                        PBJuego.ImageLocation = recorridoJuego.Ruta;
                        PBJuego.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    recorridoJuego = recorridoJuego.sgte;
                }
            }
            

            
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            ClasePilaBlibioteca.NodoBiblioteca recorridoBiblioteca = PilaBiblioteca.pila;
            if (lblJuego.Text == "")
            {
                MessageBox.Show("Seleecione correctamente el juego", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (PilaBiblioteca != null)
                {
                    while (recorridoBiblioteca != null)
                    {
                        if (lblJuego.Text == recorridoBiblioteca.nombrebiblioteca)
                        {
                            if (recorridoBiblioteca.cargadeljuego == 100)
                            {
                                AbrirJuegos(recorridoBiblioteca.nombrebiblioteca);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Primero tienes que descargar el juego", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        
                        recorridoBiblioteca = recorridoBiblioteca.sgte;
                    }
                }
            }
        }

        public void AbrirJuegos(string nombre)
        {

            if (nombre == "Valorant")
            {
                Process proceso = new Process();
                proceso.StartInfo.FileName = @"C:\Users\AAA\Desktop\Alvaro\Jogos\VALORANT.lnk";
                proceso.Start();
            }

            if (nombre=="Left 4 Dead 2")
            {
                Process proceso = new Process();
                proceso.StartInfo.FileName = @"C:\Users\AAA\Desktop\Alvaro\Jogos\Left 4 Dead 2.url";
                proceso.Start();
            }
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
    }
}

