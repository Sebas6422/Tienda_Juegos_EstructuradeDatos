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
    public partial class FormJuegosUsuario : Form
    {
        ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos Listajuegos = new ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos();
        ClasePilaCarrito.PilaCarrito PilaCarrito = new ClasePilaCarrito.PilaCarrito();
        List<string> listaCategoria = new List<string>();
        string User;

        public FormJuegosUsuario(ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos listajuegos ,ClasePilaCarrito.PilaCarrito pilaCarrito, List<string> listacat, string user)
        {
            Listajuegos = listajuegos;
            PilaCarrito = pilaCarrito;
            listaCategoria = listacat;
            User = user;
            InitializeComponent();
        }


        private void FormJuegosUsuario_Load(object sender, EventArgs e)
        {
            CrearDataJuegos();
            MostrarDataGridView4();
            for (int i = 0; i < listaCategoria.Count; i++)
            {
                CbCategoria.Items.Add(listaCategoria[i]);
            }

        }

        private void CrearDataGridView4()
        {

            DGV4.Columns.Clear();
            DGV4.Rows.Clear();


            DGV4.Columns.Add("Nombre del producto", "Nombre del producto");
            DGV4.Columns.Add("Categoria", "Categoria");
            DGV4.Columns.Add("Precio", "Precio");
        }

        private void MostrarDataGridView4()
        {
            //limpiamos el datagrid
            DGV4.Rows.Clear();
            DGV4.Columns.Clear();
            ClasePilaCarrito.NodoCarrito recorrido = PilaCarrito.pila;

            CrearDataGridView4();
            //ciclo para recorre las celdas de la pila
            while (recorrido != null)
            {
                if(recorrido.User == User)
                {
                    DGV4.Rows.Add(recorrido.nombreCarrito, recorrido.categoriaCarrito, recorrido.precioCarrito);
                }
                recorrido = recorrido.sgte;
            }
        }


        public bool ValidarVacio()
        {
            if (txtCodigo.Text == "" || int.Parse(txtCodigo.Text) == 0)
                return true;
            else
                return false;
        }

        private void FormJuegosUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMenuUsuario.PilaCarrito = PilaCarrito;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = CbCategoria.SelectedIndex;

            string categoria = CbCategoria.Items[indice].ToString();
            MostrarDataJuegos(categoria);
        }

        private void MostrarDataJuegos(string Categoria)
        {
            CrearDataJuegos();
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorridoAccion = Listajuegos.lista;
            while (recorridoAccion != null)
            {
                if (recorridoAccion.Categoria == Categoria)
                {
                    dgvJuegos.Rows.Add(recorridoAccion.Codigo, recorridoAccion.NombredelJuego, recorridoAccion.Categoria, recorridoAccion.Precio);
                }
                recorridoAccion = recorridoAccion.sgte;
            }
        }

        private void CrearDataJuegos()
        {

            dgvJuegos.Columns.Clear();
            dgvJuegos.Rows.Clear();

            dgvJuegos.Columns.Add("Codigo del producto", "Codigo del producto");
            dgvJuegos.Columns.Add("Nombre del producto", "Nombre del producto");
            dgvJuegos.Columns.Add("Categoria", "Categoria");
            dgvJuegos.Columns.Add("Precio", "Precio");
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void PicturCompra_Click_1(object sender, EventArgs e)
        {
            if (ValidarVacio())
            {
                MessageBox.Show("Seleccione correctamente el juego para añadir al carrito", "ERROR");
            }
            else
            {
                int valor = int.Parse(txtCodigo.Text);

                ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorridoJuegos = Listajuegos.lista;

                while (recorridoJuegos != null)
                {
                    if (valor == recorridoJuegos.Codigo)
                    {
                        if (PilaCarrito == null)
                        {
                            //Añade un primer valor a la pila
                            PilaCarrito.push(recorridoJuegos.Categoria, recorridoJuegos.Codigo,recorridoJuegos.NombredelJuego, recorridoJuegos.Precio, User);
                            MessageBox.Show("Se añadio el juego en su carrito", "EXITO DE COMPRA", MessageBoxButtons.OK);
                            MostrarDataGridView4();
                            return;
                        }
                        else
                        {                          
                            if (PilaCarrito.VerificarsiExiste(recorridoJuegos.NombredelJuego, User))
                            {
                                MessageBox.Show("El juego ya se añadio en el carrito", "E R R O R", MessageBoxButtons.OK);
                                return;
                            }
                            else
                            {
                                PilaCarrito.push(recorridoJuegos.Categoria, recorridoJuegos.Codigo,recorridoJuegos.NombredelJuego, recorridoJuegos.Precio, User);
                                MessageBox.Show("Se añadio el juego en su carrito", "EXITO DE COMPRA", MessageBoxButtons.OK);
                                MostrarDataGridView4();
                            }
                        }
                    }
                    recorridoJuegos = recorridoJuegos.sgte;
                }
                MostrarDataGridView4();
            }
        }

        private void dgvJuegos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int Valor = Convert.ToInt32(this.dgvJuegos.SelectedRows[0].Cells[0].Value);
                txtCodigo.Text = Valor.ToString();
            }
            catch (Exception)
            {
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

        private void cerrar_Click(object sender, EventArgs e)
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


