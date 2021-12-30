using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseListaSimpleJuegosAlmacen
    {
        public class NodoJuegosAlmacen
        {
            public int Codigo;
            public string NombredelJuego;
            public string Categoria;
            public double Precio;
            public string Ruta;
            public NodoJuegosAlmacen sgte;

            public NodoJuegosAlmacen(int codigo, string nombrejuego, string categoria, double precio, string ruta)
            {
                Codigo = codigo;
                NombredelJuego = nombrejuego;
                Categoria = categoria;
                Precio = precio;
                Ruta = ruta;
                sgte = null;
            }
        }

        public class ListaSimpleJuegos
        {
            public NodoJuegosAlmacen lista;

            public ListaSimpleJuegos()
            {
                lista = null;
            }

            public void insertaInicio(int codigo, string nombrejuego, string categoria, double precio, string ruta)
            {
                NodoJuegosAlmacen q = new NodoJuegosAlmacen(codigo, nombrejuego, categoria, precio, ruta);
                q.sgte = lista;
                lista = q;
            }

            public void insertarfinal(int codigo, string nombrejuego, string categoria, double precio, string ruta)
            {
                NodoJuegosAlmacen t = lista;
                NodoJuegosAlmacen q = new NodoJuegosAlmacen(codigo, nombrejuego, categoria, precio, ruta);
                while (t.sgte != null)
                {
                    t = t.sgte;
                }
                t.sgte = q;
            }

            public bool modificar(int codigo, string nombrejuego, string categoria, double precio, string ruta)
            {
                NodoJuegosAlmacen t = lista;
                NodoJuegosAlmacen aux = lista;

                while (t != null)
                {
                    // El juego se modificara por medio del codigo.
                    if (codigo == t.Codigo)
                    {

                        while (aux != null)
                        {
                            if (aux.NombredelJuego != nombrejuego)
                            {
                                t.Codigo = codigo;
                                t.NombredelJuego = nombrejuego;
                                t.Categoria = categoria;
                                t.Precio = precio;
                                return true;
                            }
                            aux = aux.sgte;
                        }
                    }
                    t = t.sgte;
                }
                return false;
            }


            //Verificar el eliminar
            public bool eliminar(int codigo)
            {
                NodoJuegosAlmacen ant = null;
                NodoJuegosAlmacen recorrido = lista;
                bool encontrado = false;

                if (lista != null)
                {
                    while (recorrido != null && encontrado == false)
                    {
                        if (recorrido.Codigo.Equals(codigo))
                        {
                            if (ant == null)
                            {
                                lista = recorrido.sgte;
                                recorrido.sgte = null;
                                return true;
                            }
                            else
                            {
                                ant.sgte = recorrido.sgte;
                                recorrido.sgte = null;

                            }
                            encontrado = true;
                        }
                        ant = recorrido;
                        recorrido = recorrido.sgte;
                    }
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
