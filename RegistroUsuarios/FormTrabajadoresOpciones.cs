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
    public partial class FormUsuarioOpciones : Form
    {
        public int Flagale;
        public static List<string> listaCategoria = new List<string>();

        public static ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuarios = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        public static ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos ListaJuegos = new ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos();
        public static ClaseRegistroTrabajadores.ListaCircularTrabajadores ListaTrabajadores = new ClaseRegistroTrabajadores.ListaCircularTrabajadores();
        public static ClaseReporteVentas.Bicola BicolaReporte = new ClaseReporteVentas.Bicola();
        public static ClaseColaPrioridadProveedores.ColaPrioridad ColaPrioridadProveedores = new ClaseColaPrioridadProveedores.ColaPrioridad();
        public static ClaseArbolJuegos.ArbolJuegos ArbolJuegos = new ClaseArbolJuegos.ArbolJuegos();
        public static int Cantidad;
        //lista juego


        public FormUsuarioOpciones(ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos listajuegos, ClaseReporteVentas.Bicola bicolareporte, ClaseColaPrioridadProveedores.ColaPrioridad colaPrioridadProveedores, ClaseRegistroTrabajadores.ListaCircularTrabajadores listaTrabajadores, ClaseRegistroUsuarios.ListaUsuariosDoble listausuario, List<string> listacat, int cantidad, ClaseArbolJuegos.ArbolJuegos arboJ)
        {
            ListaJuegos = listajuegos;
            ListaTrabajadores = listaTrabajadores;
            ListaUsuarios = listausuario;
            BicolaReporte = bicolareporte;
            ColaPrioridadProveedores = colaPrioridadProveedores;
            listaCategoria = listacat;
            Cantidad = cantidad;
            ArbolJuegos = arboJ;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormDatos Form = new FormDatos(ListaTrabajadores, Flagale);
            this.Hide();
            Form.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormRegistroDeProveedores Form = new FormRegistroDeProveedores(ColaPrioridadProveedores, Cantidad);
            this.Hide();
            Form.ShowDialog(this);
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            FormResgistrodeJuegos form = new FormResgistrodeJuegos(ListaJuegos,listaCategoria, ColaPrioridadProveedores,Cantidad, ArbolJuegos);
            this.Hide();
            form.ShowDialog(this);
        }

       

        private void button3_Click_1(object sender, EventArgs e)
        {
            FormListadeUsuarios form = new FormListadeUsuarios(ListaUsuarios);
            this.Hide();
            form.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormReporteVenta formventa = new FormReporteVenta(BicolaReporte);
            this.Hide();
            formventa.ShowDialog(this);
        }

        private void FormUsuarioOpciones_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {//cerramos el programa
            Application.Exit();
        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
            //Application.Exit();
            //FormLoggin.listaProveedores = listaProveedores;
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
    }
}
