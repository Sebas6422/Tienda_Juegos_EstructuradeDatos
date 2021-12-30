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
    public partial class FormDescargas : Form
    {
        int cont;
        ClaseColaDescargas.ColaDescarga ColaDescarga = new ClaseColaDescargas.ColaDescarga();
        ClasePilaBlibioteca.PilaBiblioteca PilaBiblioteca = new ClasePilaBlibioteca.PilaBiblioteca();
        string User;
        public FormDescargas(ClaseColaDescargas.ColaDescarga coladescarga, ClasePilaBlibioteca.PilaBiblioteca pilabiblioteca, string user)
        {
            ColaDescarga = coladescarga;
            PilaBiblioteca = pilabiblioteca;
            User = user;
            InitializeComponent();
        }

        private void FormDescargas_Load(object sender, EventArgs e)
        {
            MostrarDGV();

        }

        private void Timer_carga_Tick(object sender, EventArgs e)
        {
            
            cont++;
            string nombredeljuego;
            if (progressBar1.Value < 100)
            {
                progressBar1.Value++;
            }
            if (progressBar1.Value == 100)
            {
                Timer_carga.Enabled = false;
                progressBar1.Value = 0;
                nombredeljuego = ColaDescarga.deqeue(User);
                if (nombredeljuego == "Vacia")
                {
                    MessageBox.Show("La cola de descarga del usuario se encuentra vacia");
                    return;
                }
                else
                {
                    MessageBox.Show("Descarga del juego " + nombredeljuego + " completa");
                    dataGridView1.ClearSelection();
                    MostrarDGV();
                    ClasePilaBlibioteca.NodoBiblioteca recorrido = PilaBiblioteca.pila;
                    while (recorrido != null)
                    {
                        if (recorrido.nombrebiblioteca == nombredeljuego)
                        {
                            recorrido.cargadeljuego = 100;
                        }
                        recorrido = recorrido.sgte;
                    }
                    if (ColaDescarga.cola != null)
                    {
                        VolveraCargar();
                    }
                    return;
                }
            }
        }

        private void CrearDGV()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("NOMBRE DEL JUEGO", "NOMBRE DEL JUEGO");
        }
        private void MostrarDGV()
        {
            ClaseColaDescargas.NodoColaDescarga recorrido = ColaDescarga.cola;
            CrearDGV();
            while (recorrido != null)
            {

                if (recorrido.User == User)
                {
                    dataGridView1.Rows.Add(recorrido.nombreDescarga);
                }
                recorrido = recorrido.sgte;
            }

        }

        private void VolveraCargar()
        {
            Timer_carga.Enabled = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Timer_carga.Enabled = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Timer_carga.Enabled = true;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
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
