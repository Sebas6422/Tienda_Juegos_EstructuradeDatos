using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;

namespace RegistroUsuarios
{
    public partial class FormReporteVenta : Form
    {
        ClaseReporteVentas.Bicola BicolaReporte = new ClaseReporteVentas.Bicola();
        public FormReporteVenta(ClaseReporteVentas.Bicola bicolareporte)
        {
            BicolaReporte = bicolareporte;
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        { if(vacio())
            {
                MessageBox.Show("Favor de llenar los campos incompletos..!");
            }
        else
            BicolaReporte.ponerFrente(int.Parse(txtcodigo.Text), dateTime.Text, txtnombredeljuego.Text, double.Parse(txtImporte.Text), txtAlmacen.Text, Ddispositivo.Text,txtEstado.Text, txtUser.Text);
           
            limpiarTXT();
            mostrarData();
        }
        public bool vacio()
        {
            if (txtcodigo.Text == "" || txtnombredeljuego.Text == "" || txtAlmacen.Text == "" || txtImporte.Text == "" || txtEstado.Text == "")
                return true;
            else
                return false;
        }
        //Metodo para limpiar los controles
        public void limpiarTXT()
        {
            txtcodigo.Text = "";
            txtnombredeljuego.Text = "";
            txtAlmacen.Text = "";
            txtImporte.Text = "";
            txtEstado.Text = "";
        }
        public void CrearDGV()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Codigo", "Codigo");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("Juego", "Juego");
            dataGridView1.Columns.Add("Importe", "Importe");
            dataGridView1.Columns.Add("Almacen", "Almacen");
            dataGridView1.Columns.Add("Dispositivo", "Dispositivo");
            dataGridView1.Columns.Add("Estado", "Estado");
            dataGridView1.Columns.Add("Usuario", "Usuario");
        }
        private void mostrarData()
        {
                CrearDGV();
            ClaseReporteVentas.Nodo Recorrido = BicolaReporte.bicola;
            while (Recorrido != null)
            {
                dataGridView1.Rows.Add(Recorrido.codigo, Recorrido.fecha, Recorrido.nombreJuego, Recorrido.importe, Recorrido.almacen, Recorrido.dispositivo,Recorrido.estado,Recorrido.User);
                Recorrido = Recorrido.sgte;
            }
        }

       
        private void FormReporteVenta_Load_1(object sender, EventArgs e)
        {
            mostrarData();
        }

        private void FormReporteVenta_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            FormUsuarioOpciones.BicolaReporte = BicolaReporte;
        }
        private void btnQuitarFrente_Click(object sender, EventArgs e)
        {
            
            BicolaReporte.quitarFrente();
            mostrarData();
        }

        private void btnAgregarFinal_Click(object sender, EventArgs e)
        {
            BicolaReporte.ponerFinal(int.Parse(txtcodigo.Text), dateTime.Text, txtnombredeljuego.Text, double.Parse(txtImporte.Text), txtAlmacen.Text, Ddispositivo.Text,txtEstado.Text,txtUser.Text);

            limpiarTXT();
            mostrarData();
        }

        private void btnQuitarFinal_Click(object sender, EventArgs e)
        {
            BicolaReporte.quitarFinal();
            mostrarData();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.Landscape = true;
            doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = doc };
            ((Form)ppd).WindowState = FormWindowState.Maximized;

            doc.PrintPage += delegate (object ev, PrintPageEventArgs ep)
            {
                const int DGV_ALTO = 35;
                int left = ep.MarginBounds.Left, top = ep.MarginBounds.Top;

                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    ep.Graphics.DrawString(col.HeaderText, new Font("Segoe UI", 12, FontStyle.Bold), Brushes.DeepSkyBlue, left, top);
                    left += col.Width;
                }
                left = ep.MarginBounds.Left;
                ep.Graphics.FillRectangle(Brushes.Black, left, top + 40, ep.MarginBounds.Right - left, 3);
                top += 43;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Index == dataGridView1.RowCount - 1) break;
                    left = ep.MarginBounds.Left;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        ep.Graphics.DrawString(Convert.ToString(cell.Value), new Font("Segoe UI", 10), Brushes.Black, left, top + 4);
                        left += cell.OwningColumn.Width;
                    }
                    top += DGV_ALTO;
                }
            };
            ppd.ShowDialog();
        }

        int posY = 0;
        int posX = 0;
        private void panel8_MouseMove(object sender, MouseEventArgs e)
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
