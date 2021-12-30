using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseColaPrioridadProveedores
    {
        public class Nodo
        {
            public int RUC;
            public string razonsocial;
            public string nombredelproducto;
            public double precio;
            public int prio;
            public Nodo sgte;

            public Nodo(int ruc,string razonsocial, string nombredelproducto, double precio, int prio)
            {
                this.RUC = ruc;
                this.razonsocial = razonsocial;
                this.nombredelproducto = nombredelproducto;
                this.precio = precio;
                this.prio = prio;
                sgte = null;
            }
        }
        public class ColaPrioridad
        {
            public Nodo colaprioridad;
            public ColaPrioridad()

            {
                colaprioridad = null;
            }
            public void queuePrioridad(int ruc,string razonsocial, string nombredelproducto, double precio, int prio)
            {
                Nodo q, aux, aux2, t;
                t = colaprioridad;
                aux = t;
                aux2 = null;

                q = new Nodo(ruc, razonsocial, nombredelproducto, precio, prio);

                if (colaprioridad == null) // cola vacia
                {
                    colaprioridad = q;
                    return;
                }

                else // cola no vacia
                {
                    if (colaprioridad.sgte == null) // unico elemento
                    {
                        if (colaprioridad.prio >= q.prio)
                        {
                            q.sgte = colaprioridad;
                            colaprioridad = q;
                            return;
                        }

                        else
                        {
                            colaprioridad.sgte = q;
                            return;
                        }
                    }

                    else
                    {
                        if (q.prio <= colaprioridad.prio) // nuevo con minima prioridad
                        {
                            q.sgte = colaprioridad;
                            colaprioridad = q;
                            return;
                        }

                        while (t.sgte != null)
                        {
                            if (aux.sgte != null) // elemento intermedio
                            {
                                aux2 = t.sgte;
                                if ((q.prio > t.prio) && (q.prio <= aux2.prio))
                                {
                                    t.sgte = q;
                                    q.sgte = aux2;
                                    return;
                                }
                            }

                            else // ultimo elemento
                            {
                                t.sgte = q;
                                return;
                            }

                            aux = t;
                            t = t.sgte;
                        }

                        if (aux2.prio < q.prio) // ultimo elemento;
                        {
                            aux2.sgte = q;
                            return;
                        }
                    }
                }
            }

            public Nodo dequeuPrioridad()
            {
                Nodo ant = null;
                Nodo t = colaprioridad;
                if (colaprioridad != null)
                {
                    if (colaprioridad.sgte == null)// Cola de prioridad tiene 1 solo elemento
                    {
                        colaprioridad = null;
                        return t;
                    }
                    else
                    {
                        //Si es mas de un elemento recorremos
                        while (t.sgte != null)
                        {
                            ant = t;
                            t = t.sgte;
                        }
                        ant.sgte = null;
                        return t;
                    }
                }
                return null;
            }
        }
    }
}