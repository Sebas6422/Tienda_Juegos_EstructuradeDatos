using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClasePilaBlibioteca
    {
        public class NodoBiblioteca
        {
            public string User;
            public string categoriaBiblioteca;
            public string nombrebiblioteca;
            public int cargadeljuego;
            public NodoBiblioteca sgte;

            public NodoBiblioteca(string CatBiblioteca, string NomBiblioteca, int carga, string user)
            {
                categoriaBiblioteca = CatBiblioteca;
                nombrebiblioteca = NomBiblioteca;
                cargadeljuego = carga;
                User = user;
                sgte = null;
            }
        }

        public class PilaBiblioteca
        {
            public NodoBiblioteca pila;

            public PilaBiblioteca()
            {
                pila = null;
            }

            public void push(string CatBiblioteca, string NomBiblioteca, int carga, string user)
            {
                NodoBiblioteca q = new NodoBiblioteca(CatBiblioteca, NomBiblioteca, carga, user);

                q.sgte = pila;
                pila = q;
            }
        }
       

        
    }
}
