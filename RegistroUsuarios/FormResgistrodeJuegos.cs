
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
    public partial class FormResgistrodeJuegos : Form
    {
        List<string> listaCategoria = new List<string>();
        ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos Listajuegos = new ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos();
        ClaseColaPrioridadProveedores.ColaPrioridad ColaPrioridadProveedores = new ClaseColaPrioridadProveedores.ColaPrioridad();
        ClaseArbolJuegos.ArbolJuegos ArbolJuegos = new ClaseArbolJuegos.ArbolJuegos();
        int Cantidad;
        ClaseAuxAlmacenDeproveedores.ColaAuxiliar ColaAux = new ClaseAuxAlmacenDeproveedores.ColaAuxiliar();
        public FormResgistrodeJuegos(ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos listajuegos, List<string> listacat, ClaseColaPrioridadProveedores.ColaPrioridad colaProveedores,int cantidad, ClaseArbolJuegos.ArbolJuegos arbolJ)
        {
            listaCategoria = listacat;
            Listajuegos = listajuegos;
            ColaPrioridadProveedores = colaProveedores;
            Cantidad = cantidad;
            ArbolJuegos = arbolJ;
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //FormUsuarioOpciones form = new FormUsuarioOpciones();
            this.Close();
            this.Owner.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int codigoaux = 0;
            string nombreaux;
            string categoriaaux;
            double precioaux = 0;

            try
            {
                codigoaux = int.Parse(txtCodigo.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Coloque correctamente el codigo del producto ", "E R R O R", MessageBoxButtons.OK);
                return;
            }

            nombreaux = comboJuegos.Text;

            categoriaaux = DCategoria.Text;

            try
            {
                precioaux = double.Parse(txtPrecio.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Coloque correctamente el precio del producto ", "E R R O R", MessageBoxButtons.OK);
                return;
            }


            if (Listajuegos == null)
            {
                string auxRuta = string.Empty;
                DialogResult Respuesta = MessageBox.Show("Desea Agregar Imagen al Juego?", "Confirmacion de Imagen", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (Respuesta == DialogResult.Yes)
                {
                    OpenFileDialog abrirImagen = new OpenFileDialog();

                    ////Abrir el explorador de Windows
                    if (abrirImagen.ShowDialog() == DialogResult.OK)
                    {
                        auxRuta = abrirImagen.FileName;
                        lblJuego.Visible = true;
                        lblJuego.Text = comboJuegos.Text;
                        PBJuego.ImageLocation = abrirImagen.FileName;
                        PBJuego.SizeMode = PictureBoxSizeMode.StretchImage;

                    }
                }
                Listajuegos.insertaInicio(codigoaux, nombreaux, categoriaaux, precioaux, auxRuta);
                MostrarDGV();
                txtCodigo.Clear();
                comboJuegos.Text = "";
                txtPrecio.Clear();
                txtCodigo.Focus();
                MessageBox.Show("El Juego fue registrado con Exito", "E X I T O", MessageBoxButtons.OK);
                ArbolJuegos.insertarJuego(codigoaux, nombreaux, categoriaaux, precioaux, 0);
                return;
            }
            else
            {
                ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorrido = Listajuegos.lista;
                int aver = 0;
                while (recorrido != null)
                {
                    if (codigoaux == recorrido.Codigo || nombreaux == recorrido.NombredelJuego)
                    {
                        
                        aver = 1;
                    }

                    recorrido = recorrido.sgte;
                }

                if (aver == 1)
                {
                    MessageBox.Show("El juego ya fue registrado", "E R R O R", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    
                        string auxRuta = string.Empty;
                        DialogResult Respuesta = MessageBox.Show("Desea Agregar Imagen al Juego?", "Confirmacion de Imagen", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (Respuesta == DialogResult.Yes)
                        {
                            OpenFileDialog abrirImagen = new OpenFileDialog();

                            ////Abrir el explorador de Windows
                            if (abrirImagen.ShowDialog() == DialogResult.OK)
                            {
                                auxRuta = abrirImagen.FileName;
                                lblJuego.Visible = true;
                                lblJuego.Text = comboJuegos.Text;
                                PBJuego.ImageLocation = abrirImagen.FileName;
                                PBJuego.SizeMode = PictureBoxSizeMode.StretchImage;

                            }
                        }
                        Listajuegos.insertaInicio(codigoaux, nombreaux, categoriaaux, precioaux, auxRuta);
                        MostrarDGV();
                        txtCodigo.Clear();
                        comboJuegos.Text = "";
                        txtPrecio.Clear();
                        txtCodigo.Focus();
                        MessageBox.Show("El Juego fue registrado con Exito", "E X I T O", MessageBoxButtons.OK);
                        ArbolJuegos.insertarJuego(codigoaux, nombreaux, categoriaaux, precioaux, 0);
                        return;
                    
                }
            }

        }

        private void FormResgistrodeJuegos_Load(object sender, EventArgs e)
        {   

            MostrarDGV();

            ClaseColaPrioridadProveedores.Nodo auxPrioProveedores;
            for (int i = 0; i < Cantidad; i++)
            {
                auxPrioProveedores = ColaPrioridadProveedores.dequeuPrioridad();
                comboJuegos.Items.Add(auxPrioProveedores.nombredelproducto);
                //Lo almaceno temporariamente en una cola
                ColaAux.queue(auxPrioProveedores.RUC, auxPrioProveedores.razonsocial, auxPrioProveedores.nombredelproducto, auxPrioProveedores.precio, auxPrioProveedores.prio);
            }

            ClaseAuxAlmacenDeproveedores.NodoAux auxCola;
            for (int i = 0; i < Cantidad; i++)
            {
                auxCola = ColaAux.deqeue();
                ColaPrioridadProveedores.queuePrioridad(auxCola.RUC, auxCola.razonsocial, auxCola.nombredelproducto, auxCola.precio, auxCola.prio);
            }

            for (int i = 0; i < listaCategoria.Count; i++)
            {
                DCategoria.Items.Add(listaCategoria[i]);
            }
        }

        
        private void MostrarDGV()
        {
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorrido = Listajuegos.lista;
            CrearDataGV();

            if (recorrido != null)
            { 
                while (recorrido != null)
                {
                    dataGridView1.Rows.Add(recorrido.Codigo.ToString(), recorrido.NombredelJuego, recorrido.Categoria, recorrido.Precio.ToString());
                    recorrido = recorrido.sgte;
                }
            }

        }

        private void CrearDataGV()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            dataGridView1.Columns.Add("Codigo del producto", "Codigo del producto");
            dataGridView1.Columns.Add("Nombre del producto", "Nombre del producto");
            dataGridView1.Columns.Add("Categoria", "Categoria");
            dataGridView1.Columns.Add("Precio", "Precio");


        }

        private void DCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormResgistrodeJuegos_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUsuarioOpciones.listaCategoria = listaCategoria;
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorrido = Listajuegos.lista;

            if (recorrido == null)
            {
                MessageBox.Show("La lista de juegos se encuentra vacia no se puede eliminar", "E R R O R", MessageBoxButtons.OK);

            }
            else
            {
                if (!Vacia())
                {
                    if (Listajuegos.eliminar(int.Parse(txtCodigo.Text)))
                    {
                        MessageBox.Show("El juego fue eliminado", "EXITO", MessageBoxButtons.OK);
                        MostrarDGV();
                        txtCodigo.Clear();
                        comboJuegos.Text = "";
                        DCategoria.Text = "";
                        txtPrecio.Clear();
                        txtCodigo.Focus();
                        return;
                    }
                    MostrarDGV();
                }
            }


            MostrarDGV();
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorrido = Listajuegos.lista;

            if (recorrido == null)
            {
                MessageBox.Show("La lista de juegos se encuentra vacia no se puede modificar", "E R R O R", MessageBoxButtons.OK);
            }
            else
            {
                if (!Vacia())
                {
                    if (Listajuegos.modificar(int.Parse(txtCodigo.Text), comboJuegos.Text, DCategoria.Text, int.Parse(txtPrecio.Text),""))
                    {
                        ArbolJuegos.ModificarNodoArbol(ArbolJuegos.arbolJ, int.Parse(txtCodigo.Text), comboJuegos.Text, DCategoria.Text, int.Parse(txtPrecio.Text), 0);
                        MostrarDGV();
                        txtCodigo.Clear();
                        comboJuegos.Text = "";
                        DCategoria.Text = "";
                        txtPrecio.Clear();
                        txtCodigo.Focus();
                        MessageBox.Show("Los datos fueron modificados", "EXITO",MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("El datos no se modificaron","E R R O R",MessageBoxButtons.OK);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar correctamente un juego", "E R R O R");
                }
                //Listajuegos.modificar();
            }
            MostrarDGV();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorrido = Listajuegos.lista;
            try
            {
                int valor = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value);
                //Recorrer los juegos
                while (recorrido != null)
                {
                    if (valor == recorrido.Codigo)
                    {
                        txtCodigo.Text = recorrido.Codigo.ToString();
                        comboJuegos.Text = recorrido.NombredelJuego;
                        DCategoria.Text = recorrido.Categoria;
                        txtPrecio.Text = recorrido.Precio.ToString();
                    }
                    recorrido = recorrido.sgte;
                }

            }
            catch (Exception)
            {
                return;
            }
        }

        public bool Vacia()
        {
            if (txtCodigo.Text =="" || comboJuegos.Text =="" || DCategoria.Text == "" || txtPrecio.Text == "")
                return true;
            
            else
                return false;
        }

        private void btnCat_Click(object sender, EventArgs e)
        {
            if (txtNuevaCategoria.Text == "")
            {
                MessageBox.Show("Llene el espacio para agregar una nueva categoria", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DCategoria.Items.Add(txtNuevaCategoria.Text);
                listaCategoria.Add(txtNuevaCategoria.Text);
                MessageBox.Show("Se agrego la nueva categoria", "AGREGADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNuevaCategoria.Clear();
                txtCodigo.Focus();
            }
        }

        private void btnEliminarCat_Click(object sender, EventArgs e)
        {
            if (txtNuevaCategoria.Text == "")
            {
                MessageBox.Show("Llene el espacio para eliminar una nueva categoria", "E R R O R", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                
                for (int i = 0; i < listaCategoria.Count; i++)
                {
                    if (DCategoria.Items[i+1].ToString() == txtNuevaCategoria.Text)
                    {
                        DCategoria.Items.Remove(txtNuevaCategoria.Text);
                        listaCategoria.Remove(txtNuevaCategoria.Text);
                        ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorridoJuegos = Listajuegos.lista;
                        while (recorridoJuegos != null)
                        {
                            if (recorridoJuegos.Categoria == txtNuevaCategoria.Text)
                            {
                                recorridoJuegos.Categoria = "";
                            }
                            recorridoJuegos = recorridoJuegos.sgte;
                        }
                        QuitarCategoria(ArbolJuegos.arbolJ, txtNuevaCategoria.Text);
                        MessageBox.Show("Se ah eliminado la categoria", "ELIMINADO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtNuevaCategoria.Clear();
                        txtCodigo.Focus();
                        MostrarDGV();
                        return;
                    }
                }
                MessageBox.Show("El elemento no existe");
                return;
            }
        }

        public void QuitarCategoria(ClaseArbolJuegos.NodoJuegosArbol arb, string categoria)
        {
            if (arb == null)
            {
                return;
            }
            else
            {
                QuitarCategoria(arb.izq, categoria);
                QuitarCategoria(arb.der, categoria);
                if (arb.Categoria == categoria)
                {
                    arb.Categoria = "";
                }
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 32 && e.KeyChar <= 47 || e.KeyChar >= 58 && e.KeyChar <= 255)
            {
                MessageBox.Show("Ingrese correctamente el codigo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void comboJuegos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = comboJuegos.SelectedIndex;
            string juego = comboJuegos.Items[indice].ToString();

            ClaseColaPrioridadProveedores.Nodo auxPrioProveedores;
            for (int i = 0; i < Cantidad; i++)
            {
                auxPrioProveedores = ColaPrioridadProveedores.dequeuPrioridad();

                if (auxPrioProveedores.nombredelproducto == juego)
                {
                    txtPrecio.Text = auxPrioProveedores.precio.ToString();
                }

                ColaAux.queue(auxPrioProveedores.RUC, auxPrioProveedores.razonsocial, auxPrioProveedores.nombredelproducto, auxPrioProveedores.precio, auxPrioProveedores.prio);
            }

            ClaseAuxAlmacenDeproveedores.NodoAux auxCola;
            for (int i = 0; i < Cantidad; i++)
            {
                auxCola = ColaAux.deqeue();
                ColaPrioridadProveedores.queuePrioridad(auxCola.RUC, auxCola.razonsocial, auxCola.nombredelproducto, auxCola.precio, auxCola.prio);
            }

        }

        public bool VerificarVacio(ClaseArbolJuegos.NodoJuegosArbol arb, string categoria)
        {
            if (arb == null)
            {
                return false;
            }
            else
            {
                QuitarCategoria(arb.izq, categoria);
                QuitarCategoria(arb.der, categoria);
                if (arb.Categoria == categoria)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            //Verifica si la categoria de lista esta vacia sino para llenarla
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorridoJuegos = Listajuegos.lista;
            while (recorridoJuegos != null)
            {
                if (recorridoJuegos.Categoria == "")
                {
                    MessageBox.Show("Debe llenar todos los espacios del registro", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                recorridoJuegos = recorridoJuegos.sgte;
            }

            //FormUsuarioOpciones form = new FormUsuarioOpciones();
            this.Close();
            this.Owner.Show();
        }

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picturecerrar_Click(object sender, EventArgs e)
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
