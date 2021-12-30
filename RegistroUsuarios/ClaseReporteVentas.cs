using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseReporteVentas
    {
        public class Nodo
        {
            public int codigo;
            public string fecha;
            public string nombreJuego;
            public double importe;
            public string almacen;
            public string dispositivo;
            public string estado;
            public string User;
            public Nodo sgte;

            public Nodo(int valor, string Fecha, string NombreJuego, double Importe, string Almacen, string Dispositivo,string Estado, string user)
            {
                codigo = valor;
                fecha = Fecha;
                nombreJuego = NombreJuego;
                importe = Importe;
                almacen = Almacen;
                dispositivo = Dispositivo;
                estado = Estado;
                User = user;

                sgte = null;
            }
        }
        public class Bicola
        {
            public Nodo bicola;

            public Bicola()
            {
                bicola = null;
            }



            public void ponerFrente(int valor, string Fecha, string NombreJuego, double Importe, string Almacen, string Dispisitivo,string Estado, string user)
            {
                Nodo q = new Nodo(valor, Fecha, NombreJuego, Importe, Almacen, Dispisitivo,Estado, user);
                q.sgte = bicola;
                bicola = q;
            }

            public string quitarFrente()
            {
                int val = 0;
                if (bicola != null)
                {
                    val = bicola.codigo;
                    bicola = bicola.sgte;
                    return val.ToString();
                }
                else
                {

                    return "Bicola Vacia";
                }
            }


            public void ponerFinal(int valor, string Fecha, string NombreJuego, double Importe, string Almacen, string Dispositivo,string Estado, string user)
            {
                Nodo t = bicola;
                Nodo q = new Nodo(valor, Fecha, NombreJuego, Importe, Almacen, Dispositivo,Estado, user);
                if (bicola == null)
                {
                    bicola = q;
                }
                else
                {
                    while (t.sgte != null)
                    {
                        t = t.sgte;
                    }
                    t.sgte = q;
                }
            }

            public int quitarFinal()
            {
                Nodo t = bicola;
                Nodo ant = null;
                int val = 0;
                if (bicola != null)
                {
                    if (bicola.sgte == null)//hay un elemento
                    {
                        val = bicola.codigo;
                        bicola = null;
                    }
                    else
                    {
                        while (t.sgte != null)
                        {
                            ant = t;
                            t = t.sgte;
                        }
                        val = t.codigo;
                        ant.sgte = null;
                    }
                    return val;
                }
                else
                {
                    
                    return -10000;
                }
            }
        }
    }
}
