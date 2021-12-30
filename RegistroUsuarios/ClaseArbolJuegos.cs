using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseArbolJuegos
    {
        public class NodoJuegosArbol
        {
            public int Codigo;
            public string NombredelJuego;
            public string Categoria;
            public double Precio;
            public int ContadorTop;
            public NodoJuegosArbol izq, der;

            public NodoJuegosArbol(int codigo, string nombrejuego, string categoria, double precio, int top)
            {
                Codigo = codigo;
                NombredelJuego = nombrejuego;
                Categoria = categoria;
                Precio = precio;
                ContadorTop = top;
                izq = null;
                der = null;
            }
        }

        public class ArbolJuegos
        {
            public NodoJuegosArbol arbolJ;

            public ArbolJuegos()
            {
                arbolJ = null;
            }

            public void insertarJuego(int codigo, string nombrejuego, string categoria, double precio, int top)
            {
                int ValorRaiz;
                
                NodoJuegosArbol t = arbolJ;//raiz
                NodoJuegosArbol nod = new NodoJuegosArbol(codigo, nombrejuego, categoria, precio, top);

                if (arbolJ == null)
                {
                    arbolJ = nod;
                }
                else
                {
                    while (t != null)
                    {
                        ValorRaiz = t.Codigo;
                        if (codigo < ValorRaiz)
                        {
                            if (t.izq != null)
                            {
                                t = t.izq;
                            }
                            else
                            {
                                t.izq = nod;
                                return;
                            }
                        }
                        else
                        {
                            if (t.der != null)
                            {
                                t = t.der;
                            }
                            else
                            {
                                t.der = nod;
                                return;
                            }
                        }
                    }
                }
            }
            
            public NodoJuegosArbol buscandoValor(NodoJuegosArbol arbolJuegos, int ValorBuscado)
            {
                if (arbolJuegos.der == null && arbolJuegos.izq == null && arbolJuegos.Codigo != ValorBuscado)
                {
                    return null;
                }
                else
                {
                    //validammos si el valor a buscar esta en la raiz del nodo
                    if (ValorBuscado == arbolJuegos.Codigo)
                    {
                        return arbolJuegos;
                    }
                    //validamos si el numero de cliente el menor del padre
                    else

                    if (ValorBuscado < arbolJuegos.Codigo)
                    {

                        //validamos si el nodo izquierdo
                        if (arbolJuegos.izq.Codigo == ValorBuscado)
                        {
                            return arbolJuegos.izq;
                        }
                        //si no buscamos de nuevo en el nodo izquierdo
                        else
                        {
                            return buscandoValor(arbolJuegos.izq, ValorBuscado);
                        }
                    }
                    else
                    {
                        //si el nodo derecho existe
                        if (arbolJuegos.der.Codigo== ValorBuscado)
                        {
                            return arbolJuegos.der;
                        }
                        //si no existe se vuelve a buscar el valor en la derecha
                        else
                        {
                            return buscandoValor( arbolJuegos.der, ValorBuscado);
                        }
                    }

                }
            }

            public bool ModificarNodoArbol(NodoJuegosArbol arbolJuegos, int codigo, string nombrejuego, string categoria, double precio, int Top)
            {
                if (arbolJuegos == null)
                {
                    return false;
                }
                else
                {
                    if (buscandoValor(arbolJuegos, codigo) == null)
                    {
                        return false;

                    }
                    else
                    {
                        
                        NodoJuegosArbol auxModificar = buscandoValor(arbolJuegos, codigo);
                        if (auxModificar != null)
                        {
                        auxModificar.NombredelJuego = nombrejuego;
                        auxModificar.Categoria = categoria;
                        auxModificar.Precio = precio;
                        auxModificar.ContadorTop = auxModificar.ContadorTop + Top;
                        return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                }
            }
        }
    }
}
