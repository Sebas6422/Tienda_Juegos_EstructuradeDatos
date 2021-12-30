using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ColaAmigosAgregados
    {
        public class NodoAmigos
        {
            public int Codigo;
            public string Nombre;
            public string Usuario;
            public string RutaImagen;
            public string User;
            public NodoAmigos sgte;

            public NodoAmigos(int cod, string nom, string us , string ruta, string user)
            {
                Codigo = cod;
                Nombre = nom;
                Usuario = us;
                User = user;
                RutaImagen = ruta;
            }
        }

        public class ColaAmigos
        {
            public NodoAmigos cola;

            public ColaAmigos()
            {
                cola = null;
            }

            public void queue(int cod, string nom, string us, string ruta, string user)
            {
                NodoAmigos q = new NodoAmigos(cod, nom, us,ruta, user);
                q.sgte = cola;
                cola = q;
            }

            public string deqeue(string User)
            {
                NodoAmigos t = cola;
                NodoAmigos ant = null;
                string val;

                while (t != null)
                {
                    if (t.User == User)
                    {
                        if (cola != null)
                        {
                            if (cola.sgte == null)
                            {
                                cola = null;
                            }
                            else
                            {
                                while (t.sgte != null)
                                {
                                    ant = t;
                                    t = t.sgte;
                                }
                                ant.sgte = null;
                            }
                            val = t.Usuario;
                            return val;
                        }
                    }
                    t = t.sgte;
                }
                return "Vacia";
            }

        }

    }
}
