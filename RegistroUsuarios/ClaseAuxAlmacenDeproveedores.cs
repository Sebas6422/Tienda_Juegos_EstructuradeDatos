using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseAuxAlmacenDeproveedores
    {
        public class NodoAux
        {
            public int RUC;
            public string razonsocial;
            public string nombredelproducto;
            public double precio;
            public int prio;
            public NodoAux sgte;

            public NodoAux(int ruc, string razonsocial, string nombredelproducto, double precio, int prio)
            {
                this.RUC = ruc;
                this.razonsocial = razonsocial;
                this.nombredelproducto = nombredelproducto;
                this.precio = precio;
                this.prio = prio;
                sgte = null;
            }
            
        }


        public class ColaAuxiliar
        {
            public NodoAux colaaux;

            public ColaAuxiliar()
            {
                colaaux = null;
            }

            public void queue(int ruc, string razonsocial, string nombredelproducto, double precio, int prio)
            {
                NodoAux q = new NodoAux(ruc, razonsocial, nombredelproducto, precio, prio);
                q.sgte = colaaux;
                colaaux = q;
            }

            public NodoAux deqeue()
            {
                NodoAux t = colaaux;
                NodoAux ant = null;

                while (t != null)
                {
                    if (colaaux != null)
                    {
                        if (colaaux.sgte == null)
                        {
                            colaaux = null;
                            return t;
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
                        
                        return t;
                    }
                   
                    t = t.sgte;
                }
                return null;
            }
        }
    }
}
