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
    public partial class FormAgregarAmigos : Form
    {
        ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuarios = new ClaseRegistroUsuarios.ListaUsuariosDoble();
        string User;
        ColaAmigosAgregados.ColaAmigos ColaAmigos = new ColaAmigosAgregados.ColaAmigos();
        List<string> auxUsuariosAgregados = new List<string>();
        public FormAgregarAmigos(string user, ClaseRegistroUsuarios.ListaUsuariosDoble listaus,ColaAmigosAgregados.ColaAmigos colaA)
        {
            User = user;
            ListaUsuarios = listaus;
            ColaAmigos = colaA;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtBuscar.Visible = true;
            btnBuscar.Visible = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ClaseRegistroUsuarios.NodoUsuarios recorridoUs = ListaUsuarios.listaDoble;
            while (recorridoUs!=null)
            {
                if (txtBuscar.Text == recorridoUs.Usuario)
                {
                    picBoxImagenUsuarioBuscado.Visible = true;
                    lblUsuarioBuscado.Visible = true;
                    lblUsuarioBuscado.Text = recorridoUs.Usuario;
                    btnSolicitudes.Visible = true;
                    picBoxImagenUsuarioBuscado.ImageLocation = recorridoUs.RutaImagen;
                    picBoxImagenUsuarioBuscado.SizeMode = PictureBoxSizeMode.StretchImage;
                    DialogResult Respuesta = MessageBox.Show("Desea Agregar como amigo", "Confirmacion Solicitud", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                    if (Respuesta == DialogResult.Yes)
                    {
                        recorridoUs.Solicitud.Add(User);
                        recorridoUs.ContadorS = recorridoUs.ContadorS + 1;
                    }
                    return;
                }
                recorridoUs = recorridoUs.sgte;
            }
            MessageBox.Show("Usuario no encontrado", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }

        private void btnSolicitudes_Click(object sender, EventArgs e)
        {
            MostrarDGV();
            ClaseRegistroUsuarios.NodoUsuarios recorridoUs = ListaUsuarios.listaDoble;
            while (recorridoUs != null)
            {
                if (recorridoUs.Usuario == User)
                {
                    if (recorridoUs.ContadorS >0)
                    {
                        DGVSolli.Visible = true;
                        lblSolicitud.Visible = true;
                    }
                }
                recorridoUs = recorridoUs.sgte;
            }
        }

        void CrearDGV()
        {
            DGVSolli.Rows.Clear();
            DGVSolli.Columns.Clear();

            DGVSolli.Columns.Add("Usuario", "Usuario");
        }
        void MostrarDGV()
        {
            CrearDGV();
            ClaseRegistroUsuarios.NodoUsuarios recorridoUS = ListaUsuarios.listaDoble;
            while (recorridoUS != null)
            {
                if (User == recorridoUS.Usuario)
                {
                    for (int i = 0; i < recorridoUS.Solicitud.Count; i++)
                    {
                        if (recorridoUS.Solicitud[i]!="")
                        {
                            DGVSolli.Rows.Add(recorridoUS.Solicitud[i]);
                        }
                    }
                }
                recorridoUS = recorridoUS.sgte;
            }
        }

        private void DGVSolli_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblUsuarioAceptar.Visible = true;
                btnAceptar.Visible = true;
                lblUsuarioAceptar.Text = this.DGVSolli.SelectedRows[0].Cells[0].Value.ToString();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
                return;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string usuarioA = lblUsuarioAceptar.Text;
            ClaseRegistroUsuarios.NodoUsuarios recorridoUS = ListaUsuarios.listaDoble;
            List<string> recorridoSolicitudos = new List<string>();

            while (recorridoUS != null)
            {
                if (User == recorridoUS.Usuario)
                {
                    recorridoSolicitudos = recorridoUS.Solicitud;
                    for (int i = 0; i < recorridoSolicitudos.Count; i++)
                    {
                        if (recorridoSolicitudos[i] == usuarioA)
                        {
                            ClaseRegistroUsuarios.NodoUsuarios recorridoAux = ListaUsuarios.listaDoble;
                            recorridoSolicitudos.Remove(usuarioA);
                            while (recorridoAux != null)
                            {
                                if (recorridoAux.Usuario == usuarioA)
                                {
                                    recorridoUS.ContadorS = recorridoUS.ContadorS - 1;
                                    lblSolicitud.Visible = false;
                                    DGVSolli.Visible = false;
                                    btnSolicitudes.Visible = false;
                                    lblUsuarioAceptar.Visible = false;
                                    btnAceptar.Visible = false;
                                    ColaAmigos.queue(recorridoAux.Codigo, recorridoAux.Nombre, recorridoAux.Usuario, recorridoAux.RutaImagen, User);
                                    MostrarDGVAmigos();
                                    return;
                                }
                                recorridoAux = recorridoAux.sgte;
                            }
                        }
                        
                    }
                }
                recorridoUS = recorridoUS.sgte;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }

        private void FormAgregarAmigos_Load(object sender, EventArgs e)
        {
            MostrarDGVAmigos();
        }

        void CrearDGVAmigos()
        {
            DGVAmigos.Rows.Clear();
            DGVSolli.Columns.Clear();

            DGVAmigos.Columns.Add("Usuario", "Usuario");
        }

        void MostrarDGVAmigos()
        {
            CrearDGVAmigos();
            ColaAmigosAgregados.NodoAmigos recorridoAmigos = ColaAmigos.cola;
            while (recorridoAmigos!=null)
            {
                if (User == recorridoAmigos.User)
                {
                    DGVAmigos.Rows.Add(recorridoAmigos.Usuario);
                }
                recorridoAmigos = recorridoAmigos.sgte;
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
