using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClasePilaCompra
    {
        private string nombrejue;
        private string categoriapi;
        private double preciopi;
        private string usuario;
        private int cantidad;
        private string dispositivo;

        public string Nombrejue
        {
            get { return nombrejue; }
            set { nombrejue = value; }
        }
        public string Categoriapi
        {
            get { return categoriapi; }
            set { categoriapi = value; }
        }
        public double Preciopi
        {
            get { return preciopi; }
            set { preciopi = value; }
        }

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public string Dispositivo
        {
            get { return dispositivo; }
            set { dispositivo = value; }
        }
    }
}
