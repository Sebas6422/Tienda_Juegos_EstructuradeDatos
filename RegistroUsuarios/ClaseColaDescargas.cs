using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseColaDescargas
    {
        public class NodoColaDescarga
        {
            public string User;
            public string nombreDescarga;
            public string categoriaDescarga;
            public int porcentajeDescarga;
            public NodoColaDescarga sgte;

            public NodoColaDescarga(string NomDescarga, string CatDescarga, int PorceDescarga,string user)
            {
                nombreDescarga = NomDescarga;
                categoriaDescarga = CatDescarga;
                porcentajeDescarga = PorceDescarga;
                User = user;
                sgte = null;
            }
        }


        public class ColaDescarga
        {
            public NodoColaDescarga cola;

            public ColaDescarga()
            {
                cola = null;
            }

            public void queue(string NomDescarga, string CatDescarga, int PorceDescarga, string user)
            {
                NodoColaDescarga q = new NodoColaDescarga(NomDescarga, CatDescarga, PorceDescarga, user);
                q.sgte = cola;
                cola = q;
            }

            public string deqeue(string User)
            {
                NodoColaDescarga t = cola;
                NodoColaDescarga ant = null;
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
                            val = t.nombreDescarga;
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
