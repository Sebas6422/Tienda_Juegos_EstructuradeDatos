using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseRegistroUsuarios
    {
        public class NodoUsuarios
        {
            public int Codigo;
            public string Nombre;
            public string Usuario;
            public string Contraseña;
            public double Saldo;
            public string PreguntaSeguridad;
            public string Respuesta;
            public string RutaImagen;
            public List<string> Solicitud = new List<string>();
            public int ContadorS;
            public NodoUsuarios ant;
            public NodoUsuarios sgte;

            public NodoUsuarios(int cod, string nom, string usu, string contra, double saldo,string pregunta, string respuesta, string ruta,List<string> solicitud, int cont)
            {
                Codigo = cod;
                Nombre = nom;
                Usuario = usu;
                Contraseña = contra;
                Saldo = saldo;
                PreguntaSeguridad = pregunta;
                Respuesta = respuesta;
                RutaImagen = ruta;
                Solicitud = solicitud;
                ContadorS = cont;
                ant = null;
                sgte = null;
            }
        }

        public class ListaUsuariosDoble
        {
            public NodoUsuarios listaDoble;

            public ListaUsuariosDoble()
            {
                listaDoble = null;
            }

            public void insertarInicioDoble(int cod, string nom, string usu, string contra, double saldo, string pregunta, string respuesta, string ruta,List<string> solicitud, int cont)
            {
                NodoUsuarios q = new NodoUsuarios(cod, nom, usu, contra, saldo,pregunta,respuesta,ruta,solicitud, cont);
                q.sgte = listaDoble;
                if(listaDoble != null)
                {
                    listaDoble.ant = q;
                }
                listaDoble = q;
            }

            public void insertaFinalDoble(int cod, string nom, string usu, string contra, double saldo, string pregunta, string respuesta, string ruta,List<string> solicitud, int cont)
            {
                NodoUsuarios q = new NodoUsuarios(cod, nom, usu, contra, saldo,pregunta, respuesta, ruta,solicitud, cont);
                NodoUsuarios t = listaDoble;
                if (listaDoble != null)
                {
                    while (t.sgte != null)
                    {
                        t = t.sgte;
                    }
                    t.sgte = q;
                    q.ant = t;
                }
            }
        }
    }
}
