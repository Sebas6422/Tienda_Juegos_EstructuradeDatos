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

    public partial class FormListadeUsuarios : Form
    {
        ClaseRegistroUsuarios.ListaUsuariosDoble Listausuarios = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        public FormListadeUsuarios(ClaseRegistroUsuarios.ListaUsuariosDoble listaUsuarios)
        {
            Listausuarios = listaUsuarios;
            InitializeComponent();
        }

        private void FormListadeUsuarios_Load(object sender, EventArgs e)
        {
            if (Listausuarios != null)
            {
                MostrarDGV();
            }
            
        }

        private void MostrarDGV()
        {
            CrearDGV();
            ClaseRegistroUsuarios.NodoUsuarios recorrido = Listausuarios.listaDoble;
            while (recorrido != null)
            {
                dataGridView1.Rows.Add(recorrido.Codigo,recorrido.Nombre, recorrido.Usuario, recorrido.Contraseña, recorrido.Saldo);
                recorrido = recorrido.sgte;
            }
           
        }

        private void CrearDGV()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns.Add("Nombre del Usuario", "Nombre del Usuario");
            dataGridView1.Columns.Add("Usuario", "Usuario");
            dataGridView1.Columns.Add("Contraseña", "Contraseña");
            dataGridView1.Columns.Add("Saldo", "Saldo");
        }


        private void FormListadeUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex == 3 && e.Value != null)
            //{
            //    e.Value = new string('*', e.Value.ToString().Length);
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictsalir_Click(object sender, EventArgs e)
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
