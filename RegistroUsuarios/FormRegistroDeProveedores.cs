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
    public partial class FormRegistroDeProveedores : Form
    {
        ClaseColaPrioridadProveedores.ColaPrioridad ColaPrioridad = new ClaseColaPrioridadProveedores.ColaPrioridad();
        int Cantidad;

        public FormRegistroDeProveedores(ClaseColaPrioridadProveedores.ColaPrioridad colaPrioridad, int cantidad)
        {
            ColaPrioridad = colaPrioridad;
            Cantidad = cantidad;
            InitializeComponent();
        }

        private void FormRegistroDeProveedores_Load(object sender, EventArgs e)
        {

            MostrarDGV();
        }

        //Boton Para Volver al formulario del menu del administrador
       
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (VerificarVacio())
            {
                MessageBox.Show("Capture correctamente los valores en los espacios", "ERROR");
            }
            else
            {
                ColaPrioridad.queuePrioridad(int.Parse(txtRuc.Text), txtRazon.Text, txtNombre.Text, Convert.ToDouble(txtPrecio.Text), int.Parse(DPrioridad.Text));
                Cantidad = Cantidad + 1;
                //Limpiar los datos de los text box
                MostrarDGV();
                txtNombre.Clear();
                txtRazon.Clear();
                txtPrecio.Clear();
                txtRuc.Clear();
                txtRuc.Focus(); //el cursor se enfoca en el text box del Ruc
            }
            
        }

        public bool VerificarVacio()
        {
            if (txtNombre.Text == "" || txtRazon.Text == "" || txtPrecio.Text == "" || txtRuc.Text == "")
                return true;
            else
                return false;
        }
        private void CrearDGV()
        {
            dataGridView1.Columns.Clear(); // Limpia las columnas
            dataGridView1.Rows.Clear(); //LImpia los renglones

            dataGridView1.Columns.Add("Num de RUC", "Num de RUC");
            dataGridView1.Columns.Add("Razón Social", "Razón Social");
            dataGridView1.Columns.Add("Nombre del Producto", "Nombre del Producto");
            dataGridView1.Columns.Add("Precio", "Precio");
            dataGridView1.Columns.Add("Prioridad", "Prioridad");
        }

        private void MostrarDGV()
        {
            CrearDGV(); // Crea el dataGriedView
            ClaseColaPrioridadProveedores.Nodo RecorridoColaPrioridad = ColaPrioridad.colaprioridad;
            //Recorre cada nodo de la lista utilizando el iterador
            while (RecorridoColaPrioridad != null)
            {
                dataGridView1.Rows.Add(RecorridoColaPrioridad.RUC,RecorridoColaPrioridad.razonsocial,RecorridoColaPrioridad.nombredelproducto, RecorridoColaPrioridad.precio, RecorridoColaPrioridad.prio);
                RecorridoColaPrioridad = RecorridoColaPrioridad.sgte;

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            ColaPrioridad.dequeuPrioridad();
            MostrarDGV();
            txtRuc.Clear();
            
        }

        private void FormRegistroDeProveedores_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUsuarioOpciones.Cantidad = Cantidad;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClaseColaPrioridadProveedores.Nodo recorrido = ColaPrioridad.colaprioridad;
            try
            {
                int valor = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
                while (recorrido != null)
                {
                    if (recorrido.RUC == valor)
                    {
                        txtRuc.Text = recorrido.RUC.ToString();
                        txtRazon.Text = recorrido.razonsocial;
                        txtPrecio.Text = recorrido.precio.ToString();
                        txtNombre.Text = recorrido.nombredelproducto;
                        DPrioridad.Text = recorrido.prio.ToString();
                    }
                    recorrido = recorrido.sgte;
                }
            }
            catch (Exception)
            {

                return;
            }
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Ingrese correctamente el RUC", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Ingrese correctamente el precio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //FormUsuarioOpciones form = new FormUsuarioOpciones();
            this.Owner.Show();
            this.Close();
        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        int posY = 0;
        int posX = 0;
        private void panel5_MouseMove(object sender, MouseEventArgs e)
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
