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
    public partial class FormCarrito : Form
    {
       
        public static ClasePilaCarrito.PilaCarrito PilaCarrito = new ClasePilaCarrito.PilaCarrito();
        public static ClasePilaBlibioteca.PilaBiblioteca PilaBiblioteca = new ClasePilaBlibioteca.PilaBiblioteca();
        public static ClaseReporteVentas.Bicola BicolaReporte = new ClaseReporteVentas.Bicola();
        public static ClaseRegistroUsuarios.ListaUsuariosDoble ListaDeUsuarios = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        public static ClaseArbolJuegos.ArbolJuegos ArbolJuegos = new ClaseArbolJuegos.ArbolJuegos();
        public static string User;
        ClasePilaCarrito.NodoCarrito aux;
        public FormCarrito(ClasePilaCarrito.PilaCarrito pilacarrito,  ClasePilaBlibioteca.PilaBiblioteca pilabiblioteca, ClaseReporteVentas.Bicola bicolareporte, ClaseRegistroUsuarios.ListaUsuariosDoble listaUsuarios,string user, ClaseArbolJuegos.ArbolJuegos arbolJuegos)
        {
            PilaCarrito = pilacarrito;
            PilaBiblioteca = pilabiblioteca;
            BicolaReporte = bicolareporte;
            ListaDeUsuarios = listaUsuarios;
            User = user;
            ArbolJuegos = arbolJuegos;
            
            InitializeComponent();
        }

        private void FormCarrito_Load(object sender, EventArgs e)
        {
            
            MostrarDataGridView();
        }

        //Pasamos a realizar la compra del viedojuego
        private void button1_Click(object sender, EventArgs e)
        {
            //Creamos un Nodo para hacer el recorrido y verificar si el usuario tiene el sueldo suficiente para realizar la compra
            ClaseRegistroUsuarios.NodoUsuarios recorridoUsuario = ListaDeUsuarios.listaDoble;
           
            
            FormCarrito.ActiveForm.StartPosition = FormStartPosition.CenterScreen;

            if (ValidadVAcio())
            {
                MessageBox.Show("Agrege correctamente los datos en los espacios", "ERROR");
            }
            else
            {
                if (ListaDeUsuarios != null)
                {
                    double restaPrecio;
                    double auxTXT = double.Parse(txtPrecio.Text);
                    while (recorridoUsuario != null)
                    {
                        if (recorridoUsuario.Usuario == User)
                        {
                            restaPrecio = recorridoUsuario.Saldo - auxTXT;
                            if (restaPrecio >= 0)
                            {
                                //Des que  verifico que es el usuario y puede comprar el juego se mofica su Salario actual
                                recorridoUsuario.Saldo = restaPrecio;
                                //Pasa a la pila Biblioteca
                                PilaBiblioteca.push(txtCategoria.Text, txtNombre.Text, 0, User);

                                //Pasa a la Bicola de reporte
                                BicolaReporte.ponerFrente(int.Parse(txtCodigoCompra.Text), dateTimeFecha.Text, txtNombre.Text, double.Parse(txtPrecio.Text), txtAlmacen.Text, DDispositivo.Text,txtEstado.Text, User);
                                MessageBox.Show("Su compra se realizo con exito", "EXITO", MessageBoxButtons.OK);

                                //Agregar el top en el Arbol
                                ArbolJuegos.ModificarNodoArbol(ArbolJuegos.arbolJ, aux.CodigoJuego, txtNombre.Text, txtCategoria.Text, double.Parse(txtPrecio.Text),1);

                                MostrarDataGridView();
                                

                                ClaseArbolJuegos.NodoJuegosArbol auxarbol = ArbolJuegos.arbolJ;
                                if (auxarbol == null)
                                {
                                    MessageBox.Show("Esta Vacio");
                                }
                                else
                                {
                                    Size = new Size(920, 700);
                                    MostrarArbol(auxarbol);
                                    MostrarCategoria(auxarbol, txtCategoria.Text);
                                    limpiarTXT();
                                }
                                
                                return;
                            }
                        }
                        recorridoUsuario = recorridoUsuario.sgte;
                    }
                    
                        MessageBox.Show("No cuenta con saldo suficiente", "Falla en la compra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
            }
            

            

        }
        void listamostrarJuegos(int codigo,string nombre,string categoria,double precio)
        {
            ListViewItem items = new ListViewItem(codigo.ToString());
            items.SubItems.Add(nombre);
            items.SubItems.Add(categoria);
            items.SubItems.Add(precio.ToString());
            
            

            listarbol.Items.Add(items);
        }
        public void MostrarCategoria(ClaseArbolJuegos.NodoJuegosArbol arb,string categoria)
        {
            if (arb == null)
            {
                return;
            }
            else
            {
                MostrarCategoria(arb.izq, categoria);
                MostrarCategoria(arb.der, categoria);
                if (arb.Categoria == categoria)
                {
                    listamostrarJuegos(arb.Codigo, arb.NombredelJuego, arb.Categoria, arb.Precio);
                }
            }
        }
        
        


        public void MostrarArbol(ClaseArbolJuegos.NodoJuegosArbol arbJuegos)
        {
            if (arbJuegos == null)
            {
                return;
            }
            else
            {

                Listadejuegos(arbJuegos.Codigo,arbJuegos.NombredelJuego,arbJuegos.Categoria,arbJuegos.Precio,arbJuegos.ContadorTop);
                MostrarArbol(arbJuegos.izq);
                MostrarArbol(arbJuegos.der);
            }
        }
        void Listadejuegos(int codigo,string nombrejuego,string categoria,double precio,int top)
        {
            ListViewItem items = new ListViewItem(codigo.ToString());
            items.SubItems.Add(nombrejuego);
            items.SubItems.Add(categoria);
            items.SubItems.Add(precio.ToString());
            items.SubItems.Add(top.ToString());
            

            Listtop.Items.Add(items);
        }
        public bool limpiarTXT()
        {
            txtCodigoCompra.Text = "";
            txtNombre.Text = "";
            txtCategoria.Text = "";
            txtPrecio.Text = "";
            txtEstado.Text = "";
            return true;
        }

        public bool ValidadVAcio()
        {
            if (txtAlmacen.Text == "" || txtCategoria.Text == "" || txtCodigoCompra.Text == "" || txtEstado.Text == "" || txtNombre.Text == "" || txtPrecio.Text == "")
                return true;
            else
                return false;
        }
        //Metodo para Crear el Data Gried View
        private void CrearDataGrid()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns.Add("Categoria", "Categoria");
            dataGridView1.Columns.Add("Precio", "Precio");

        }
        private void MostrarDataGridView()
        {
            ClasePilaCarrito.NodoCarrito recorrido = PilaCarrito.pila;
            CrearDataGrid();
            while (recorrido != null)
            {
                if (recorrido.User == User)
                {
                    dataGridView1.Rows.Add(recorrido.nombreCarrito, recorrido.categoriaCarrito, recorrido.precioCarrito);

                }
                recorrido = recorrido.sgte;
            }

        }

       

        private void FormCarrito_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMenuUsuario.PilaBiblioteca = PilaBiblioteca;
        }

        

        private void btnpasar_Click_1(object sender, EventArgs e)
        {
            //Creamos un auxiliar para mantener el nodo retornado del pop de la Pila Carrito
            
            aux = PilaCarrito.pop(User);

            //Creamos un Random para generar un codigo Random
            Random randomCod = new Random();
            int codCompra;
            if (aux == null)
            {
                MessageBox.Show("El carrito del usuario se encuentra vacia", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                codCompra = 100 + randomCod.Next(50, 10000);
                txtCodigoCompra.Text = codCompra.ToString();
                txtNombre.Text = aux.nombreCarrito;
                txtCategoria.Text = aux.categoriaCarrito;
                txtPrecio.Text = aux.precioCarrito.ToString();
                MostrarDataGridView();
                return;
            }  
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (!limpiarTXT())
            {
                txtAlmacen.Clear();
                txtCategoria.Clear();
                txtCodigoCompra.Clear();
                txtEstado.Clear();
                txtNombre.Clear();
                txtPrecio.Clear();
                MessageBox.Show("Compra del juego cancelada", "CANCELAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ERROR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
