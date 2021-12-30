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
    public partial class FormDatos : Form
    {
        ClaseRegistroTrabajadores.ListaCircularTrabajadores ListaTrabajadores = new ClaseRegistroTrabajadores.ListaCircularTrabajadores();
        public int flag;
        public FormDatos(ClaseRegistroTrabajadores.ListaCircularTrabajadores listaTrabajadores, int flagale)
        {
            ListaTrabajadores = listaTrabajadores;
            flag = flagale;
            InitializeComponent();
        }
        public void guardarUsuario()
        {
          

            txtcodigo.Clear();
            txtnombre.Clear();
            txtusuario.Clear();
            txtcontrasena.Clear();
            txtcodigo.Focus();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        { 
            MostrarDGV();
           
        }


        private void btnguardar_Click(object sender, EventArgs e)
        {
            ClaseRegistroTrabajadores.NodoTrabajadores next = null, ant = null;
            ClaseRegistroTrabajadores.NodoTrabajadores recorrido = ListaTrabajadores.lista;
            ClaseRegistroTrabajadores.NodoTrabajadores primero = ListaTrabajadores.lista;
            
            if (recorrido != null)
            {
                do
                {
                    if (recorrido.codigo == int.Parse(txtcodigo.Text))
                    {

                        
                        if (recorrido == primero)
                        {
                            if (recorrido.sgte == primero)//Primer y ultimo elemento
                            {
                                ListaTrabajadores = null;
                            }
                            else
                            {
                                next = recorrido.sgte;
                                ListaTrabajadores.lista = next;
                                while (recorrido.sgte != primero)
                                {
                                    recorrido = recorrido.sgte;
                                }
                                recorrido.sgte = next;
                            }
                        }
                        else
                        {
                            if (recorrido.sgte == primero)//elemento ultimo
                            {
                                ant.sgte = primero;
                            }
                            else//elemento intermedio
                            {
                                next = recorrido.sgte;
                                ant.sgte = next;
                            }
                        }

                    }
                    ant = recorrido;
                    recorrido = recorrido.sgte;

                } while (recorrido != primero);
                MostrarDGV();
                return;
            }
            else
            {
                throw new Exception("La lista esta vacia");
            }
        }

        private void CrearDGV()
        {
            dgv1.Columns.Clear(); // Limpia las columnas
            dgv1.Rows.Clear(); //LImpia los renglones

            dgv1.Columns.Add("Codigo", "Codigo");
            dgv1.Columns.Add("Nombre", "Nombre");
            dgv1.Columns.Add("Usuario", "Usuario");
            dgv1.Columns.Add("Contraseña", "Contraseña");
        }

        private void MostrarDGV()
        {
            CrearDGV(); // Crea el dataGriedView

            if (ListaTrabajadores.lista != null)
            {
                ClaseRegistroTrabajadores.NodoTrabajadores recorrido = ListaTrabajadores.lista;

                if (recorrido != null)
                {
                    while (recorrido.sgte != ListaTrabajadores.lista)
                    {
                        dgv1.Rows.Add(recorrido.codigo, recorrido.nombretrabajador, recorrido.usuariotrabajador, recorrido.contraseñatrabajador);
                        recorrido = recorrido.sgte;
                    }
                    dgv1.Rows.Add(recorrido.codigo, recorrido.nombretrabajador, recorrido.usuariotrabajador, recorrido.contraseñatrabajador);
                }
                return;
            }

        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                if(txtcontrasena.PasswordChar == '*')
                {
                    txtcontrasena.PasswordChar = '\0';
                }
            }
            else
            {
                txtcontrasena.PasswordChar = '*';
            }
        }

        

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ClaseRegistroTrabajadores.NodoTrabajadores next = null, ant = null;
            ClaseRegistroTrabajadores.NodoTrabajadores recorrido = ListaTrabajadores.lista;
            ClaseRegistroTrabajadores.NodoTrabajadores primero = ListaTrabajadores.lista;
            
            if (recorrido != null)
            {
                do
                {
                    if (recorrido.codigo == int.Parse(txtcodigo.Text))
                    {

                        if (recorrido == primero)
                        {
                            if (recorrido.sgte == primero)//Primer y ultimo elemento
                            {
                                ListaTrabajadores = null;
                            }
                            else
                            {
                                next = recorrido.sgte;
                                ListaTrabajadores.lista = next;
                                while (recorrido.sgte != primero)
                                {
                                    recorrido = recorrido.sgte;
                                }
                                recorrido.sgte = next;
                            }
                        }
                        else
                        {
                            if (recorrido.sgte == primero)//elemento ultimo
                            {
                                ant.sgte = primero;
                            }
                            else//elemento intermedio
                            {
                                next = recorrido.sgte;
                                ant.sgte = next;
                            }
                        }

                    }
                    ant = recorrido;
                    recorrido = recorrido.sgte;

                } while (recorrido != primero);
                MostrarDGV();
                return;
            }
            else
            {
                throw new Exception("La lista esta vacia");
            }
        }

        private void dgv1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ClaseRegistroTrabajadores.NodoTrabajadores recorrido = ListaTrabajadores.lista;
            ClaseRegistroTrabajadores.NodoTrabajadores primero = ListaTrabajadores.lista;

            do
            {
                if (recorrido.codigo == Convert.ToInt32(txtcodigo.Text))
                {
                    recorrido.codigo = int.Parse(txtcodigo.Text);
                    recorrido.nombretrabajador = txtnombre.Text;
                    recorrido.usuariotrabajador = txtusuario.Text;
                    recorrido.contraseñatrabajador = txtcontrasena.Text;
                    MostrarDGV();

                }

                recorrido = recorrido.sgte;
            } while (recorrido != primero);
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClaseRegistroTrabajadores.NodoTrabajadores recorrido = ListaTrabajadores.lista;
            ClaseRegistroTrabajadores.NodoTrabajadores primero = ListaTrabajadores.lista;

            try
            {
                int valor = Convert.ToInt32(this.dgv1.SelectedRows[0].Cells[0].Value);


                if (recorrido != null)
                {
                    do
                    {
                        if (recorrido.codigo == valor)
                        {
                            txtcodigo.Text = recorrido.codigo.ToString();
                            txtnombre.Text = recorrido.nombretrabajador;
                            txtusuario.Text = recorrido.usuariotrabajador;
                            txtcontrasena.Text = recorrido.contraseñatrabajador;
                        }

                        recorrido = recorrido.sgte;
                    } while (recorrido != primero);
                }

            }
            catch (Exception)
            {
                return;
            }
        }

        private void FormDatos_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormUsuarioOpciones.ListaTrabajadores = ListaTrabajadores;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            MostrarDGV();
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

        private void pictureBox6_Click(object sender, EventArgs e)
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
