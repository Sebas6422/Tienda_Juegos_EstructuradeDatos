using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsuarios
{
    public class ClaseRegistroTrabajadores
    {
        public class NodoTrabajadores
        {
            public int codigo;
            public string nombretrabajador;
            public string usuariotrabajador;
            public string contraseñatrabajador;
            public NodoTrabajadores sgte;

            public NodoTrabajadores(int Cod, string nomt, string usut, string contrat)
            {
                codigo = Cod;
                nombretrabajador = nomt;
                usuariotrabajador = usut;
                contraseñatrabajador = contrat;
                sgte = null;
            }
        }

        public class ListaCircularTrabajadores
        {
           

            public NodoTrabajadores lista;

            public ListaCircularTrabajadores()
            {
                lista = null;
            }

            public void GeneraTrabajador(int N)
            {
                ClaseRegistroTrabajadores.ListaCircularTrabajadores listatrabajadores = new ClaseRegistroTrabajadores.ListaCircularTrabajadores();
                
            } 

            public void insertarInicioCicular(int Cod, string nomt, string usut, string contrat)
            {
                NodoTrabajadores primero, t, q;
                t = lista;
                q = new NodoTrabajadores(Cod, nomt, usut, contrat);

                if (lista == null)
                {
                    lista = q;
                    q.sgte = q;
                }
                else
                {
                    primero = lista;
                    while (t.sgte != primero)
                    {
                        t = t.sgte;
                    }
                    t.sgte = q;
                    q.sgte = primero;
                    lista = q;
                }
            }

            public void insertarFinalCircular(int Cod, string nomt, string usut, string contrat)
            {
                NodoTrabajadores primero, t, q;
                t = lista;
                q = new NodoTrabajadores(Cod, nomt, usut, contrat);

                if (lista == null)
                {
                    lista = q;
                    q.sgte = q;
                }
                else
                {
                    primero = lista;
                    while (t.sgte != primero)
                    {
                        t = t.sgte;
                    }
                    t.sgte = q;
                    q.sgte = primero;
                }
            }
        }
    }
}
