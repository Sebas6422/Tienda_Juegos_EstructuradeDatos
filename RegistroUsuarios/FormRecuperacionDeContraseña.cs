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
    public partial class FormRecuperacionDeContraseña : Form
    {
        public static ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuario = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        string Usuario;
        public FormRecuperacionDeContraseña(ClaseRegistroUsuarios.ListaUsuariosDoble listus)
        {
            ListaUsuario = listus;
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            Usuario = txtBuscar.Text;
            ClaseRegistroUsuarios.NodoUsuarios recorrido = ListaUsuario.listaDoble;
            while (recorrido!=null)
            {
                if (recorrido.Usuario == Usuario)
                {
                    lblPregunta.Visible = true;
                    lblRespuesta.Visible = true;
                    txtRespuesta.Visible = true;
                    btnAceptar.Visible = true;
                    lblPregunta.Text = recorrido.PreguntaSeguridad;
                }
                recorrido = recorrido.sgte;
            }
        }

        //private void btnAceptar_Click(object sender, EventArgs e)
        //{
        //    string respuesta = txtRespuesta.Text;
        //    ClaseRegistroUsuarios.NodoUsuarios recorrido = ListaUsuario.listaDoble;
        //    while (recorrido!=null)
        //    {
        //        if (Usuario == recorrido.Usuario && respuesta == recorrido.Respuesta)
        //        {
        //            lblCContra.Visible = true;
        //            lblnContra.Visible = true;
        //            txtNContra.Visible = true;
        //            txtCContra.Visible = true;
        //            btnConfirmarContra.Visible = true;
        //        }
        //        recorrido = recorrido.sgte;
        //    }
        //    MessageBox.Show("La respuesta no coincide", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //    return;
        //}

        private void btnConfirmarContra_Click(object sender, EventArgs e)
        {
            if (txtCContra.Text != "" && txtNContra.Text!="")
            {
                if(txtNContra.Text == txtCContra.Text)
                {
                    ClaseRegistroUsuarios.NodoUsuarios recorrido = ListaUsuario.listaDoble;
                    while (recorrido != null)
                    {
                        if (Usuario == recorrido.Usuario)
                        {
                            recorrido.Contraseña = txtNContra.Text;
                            txtBuscar.Clear();
                            txtCContra.Clear();
                            txtNContra.Clear();
                            txtRespuesta.Clear();
                            MessageBox.Show("Se cambio su contraseña", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.Close();
                            this.Owner.Show();
                            return;
                        }
                        recorrido = recorrido.sgte;
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Llene los espcaios correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string respuesta = txtRespuesta.Text;
            ClaseRegistroUsuarios.NodoUsuarios recorrido = ListaUsuario.listaDoble;
            while (recorrido != null)
            {
                if (Usuario == recorrido.Usuario && respuesta == recorrido.Respuesta)
                {
                    lblCContra.Visible = true;
                    lblnContra.Visible = true;
                    txtNContra.Visible = true;
                    txtCContra.Visible = true;
                    btnConfirmarContra.Visible = true;
                    return;
                }
                recorrido = recorrido.sgte;
            }
            MessageBox.Show("La respuesta no coincide", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            return;
        }
    }
}
