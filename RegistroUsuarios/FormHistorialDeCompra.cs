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

namespace RegistroUsuarios
{
    public partial class FormHistorialDeCompra : Form
    {
        ClaseReporteVentas.Bicola BicolaReporte = new ClaseReporteVentas.Bicola();
        string User;
        public FormHistorialDeCompra(string user, ClaseReporteVentas.Bicola bicolaReporte)
        {
            BicolaReporte = bicolaReporte;
            User = user;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void FormHistorialDeCompra_Load(object sender, EventArgs e)
        {
            MostrarDGVHistorial();
        }

        void CrearDGVHistorial()
        {
            DGVHistorial.Rows.Clear();
            DGVHistorial.Columns.Clear();

            DGVHistorial.Columns.Add("Codigo", "Codigo");
            DGVHistorial.Columns.Add("Fecha", "Fecha");
            DGVHistorial.Columns.Add("Juego", "Juego");
            DGVHistorial.Columns.Add("Importe", "Importe");
            DGVHistorial.Columns.Add("Almacen", "Almacen");
            DGVHistorial.Columns.Add("Dispositivo", "Dispositivo");
            DGVHistorial.Columns.Add("Estado", "Estado");
            DGVHistorial.Columns.Add("Usuario", "Usuario");
        }

        void MostrarDGVHistorial()
        {
            CrearDGVHistorial();
            ClaseReporteVentas.Nodo Recorrido = BicolaReporte.bicola;
            while (Recorrido != null)
            {
                if (Recorrido.User == User)
                {
                    DGVHistorial.Rows.Add(Recorrido.codigo, Recorrido.fecha, Recorrido.nombreJuego, Recorrido.importe, Recorrido.almacen, Recorrido.dispositivo, Recorrido.estado, Recorrido.User);
                }
                Recorrido = Recorrido.sgte;
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

                foreach (DataGridViewColumn col in DGVHistorial.Columns)
                {
                    ep.Graphics.DrawString(col.HeaderText, new Font("Segoe UI", 12, FontStyle.Bold), Brushes.DeepSkyBlue, left, top);
                    left += col.Width;
                }
                left = ep.MarginBounds.Left;
                ep.Graphics.FillRectangle(Brushes.Black, left, top + 40, ep.MarginBounds.Right - left, 3);
                top += 43;

                foreach (DataGridViewRow row in DGVHistorial.Rows)
                {
                    if (row.Index == DGVHistorial.RowCount - 1) break;
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
