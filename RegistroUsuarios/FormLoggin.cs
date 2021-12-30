using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace RegistroUsuarios
{

    public partial class FormLoggin : Form
    {
        //manterner las categorias en el codigo
        public static List<string> listaCategoria = new List<string>();
        //Creo una list para almacenar los nombres de los juegos momentaneamente
        public static List<string> NombreJuegos = new List<string>();

        string usuario = "admin";
        string contra = "password";
        public string User;
        public int flag;

        public int Cantidad;
        //creando la variable global de las estructuras que usaremos en todo el codigo 

        //Lista simple con la lista de juegos
        public static ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos ListaJuegos = new ClaseListaSimpleJuegosAlmacen.ListaSimpleJuegos();

        //Lista enlazada doble
        public static ClaseRegistroUsuarios.ListaUsuariosDoble ListaUsuario = new ClaseRegistroUsuarios.ListaUsuariosDoble();

        //Lista circular con la lista de trabajadores
        public static ClaseRegistroTrabajadores.ListaCircularTrabajadores listaTrabajadores = new ClaseRegistroTrabajadores.ListaCircularTrabajadores();

        //Pilas que se usaran en el carrito y en la biblioteca
        public static ClasePilaCarrito.PilaCarrito PilaCarrito = new ClasePilaCarrito.PilaCarrito();
        public static ClasePilaBlibioteca.PilaBiblioteca PilaBiblioteca = new ClasePilaBlibioteca.PilaBiblioteca();

        //La cola que usaremos sera en la descarga para la simulacion
        public static ClaseColaDescargas.ColaDescarga ColaDescarga = new ClaseColaDescargas.ColaDescarga();
        public static ClaseAuxAlmacenDeproveedores.ColaAuxiliar ColaAuxiliar = new ClaseAuxAlmacenDeproveedores.ColaAuxiliar();
        public static ColaAmigosAgregados.ColaAmigos ColaAmigos = new ColaAmigosAgregados.ColaAmigos();

        //Bicola que usaremos para el reporte de Ventas de los usuarios
        public static ClaseReporteVentas.Bicola BicolaReporte = new ClaseReporteVentas.Bicola();

        //Cola prioridad para priorizar los proveedrores y los cuales tienen mayor prestigio
        public static ClaseColaPrioridadProveedores.ColaPrioridad ColaPrioridadProveedores = new ClaseColaPrioridadProveedores.ColaPrioridad();

        //Arboles Binarias para tener un top con los atributos de la lista simple de juegos
        public static ClaseArbolJuegos.ArbolJuegos ArbolJuegos = new ClaseArbolJuegos.ArbolJuegos();

        public FormLoggin()
        {
            InitializeComponent();
            flag = 0;
            listaCategoria.Insert(0, "Acción");
            listaCategoria.Add("Deportes");
            listaCategoria.Add("Disparos");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Abrir siguiente formulario
            FormDatos2 Form = new FormDatos2(ListaUsuario);
            this.Hide();
            Form.ShowDialog(this);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (tbContraseña.PasswordChar == '*')
                {
                    tbContraseña.PasswordChar = '\0';
                }
            }
            else
            {
                tbContraseña.PasswordChar = '*';
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            ClaseRegistroUsuarios.NodoUsuarios recorridous = ListaUsuario.listaDoble;
            if (tbUsuario.Text != usuario || tbContraseña.Text != contra)
            {
                //Valida el Usuario del cliente
                if (recorridous != null)
                {
                    while (recorridous != null)
                    {
                        if (tbUsuario.Text == recorridous.Usuario && tbContraseña.Text == recorridous.Contraseña)
                        {
                            tbUsuario.Clear();
                            tbContraseña.Clear();
                            User = recorridous.Usuario;
                            //Abrir siguiente formulario
                            FormMenuUsuario form1 = new FormMenuUsuario(ListaJuegos, PilaCarrito, PilaBiblioteca, ColaDescarga, BicolaReporte, listaCategoria, User, ListaUsuario, ArbolJuegos,ColaAmigos);
                            this.Hide();
                            form1.ShowDialog(this);
                            return;
                        }
                        recorridous = recorridous.sgte;
                    }

                }


                //Valida el Usuario del trabajador
                ClaseRegistroTrabajadores.NodoTrabajadores recorritotrab = listaTrabajadores.lista;
                ClaseRegistroTrabajadores.NodoTrabajadores primero = listaTrabajadores.lista;

                if (recorritotrab != null)
                {
                    do
                    {
                        if (tbUsuario.Text == recorritotrab.usuariotrabajador && tbContraseña.Text == recorritotrab.contraseñatrabajador)
                        {
                            tbUsuario.Clear();
                            tbContraseña.Clear();
                            //Abrir siguiente formulario
                            FormUsuarioOpciones form2 = new FormUsuarioOpciones(ListaJuegos, BicolaReporte, ColaPrioridadProveedores, listaTrabajadores, ListaUsuario, listaCategoria, Cantidad,ArbolJuegos);
                            this.Hide();
                            form2.ShowDialog(this);
                            return;
                        }
                        recorritotrab = recorritotrab.sgte;
                    } while (recorritotrab != primero);

                }

                MessageBox.Show("Usuario ingresado es incorrecto", "INVALID", MessageBoxButtons.OK);
            }
            else
            {

                //Entramos al formulario de opciones del trabajador
                tbUsuario.Clear();
                tbContraseña.Clear();
                //Abrir siguiente formulario
                FormUsuarioOpciones Form = new FormUsuarioOpciones(ListaJuegos, BicolaReporte, ColaPrioridadProveedores, listaTrabajadores, ListaUsuario, listaCategoria, Cantidad,ArbolJuegos);
                this.Hide();
                Form.ShowDialog(this);

            }
        }

        private void FormLoggin_Load(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                GenerarListaTrabajadoresCircular(10);
                GenerarProveedores(5);
                GenerarListaJuegos(Cantidad);
                GenerarListaUsuarios(10);
                flag = 1;
            }
        }


        public void Descolar()
        {

        }

        //Generar aleatorios de las listas
        //Lista circualr generar lista de trabajadores
        public void GenerarListaTrabajadoresCircular(int N)
        {

            string[] nombres = { "Daniel", "Sebastian", "Renzo", "Carlos", "Diego" };
            string usuarios = "User";
            string[] contrasenas = { "paredes", "neciosup", "huamani", "crisostomo", "Ponce" };

            int auxCodigo;
            string auxNombre;
            string auxUsuario;
            string auxContrasena;
            Random R = new Random();

            for (int i = 0; i < N; i++)
            {
                auxCodigo = R.Next(1000, 99999);
                auxNombre = nombres[R.Next(0, 5)];
                auxUsuario = usuarios + R.Next(0, 100).ToString();
                auxContrasena = contrasenas[R.Next(0, 5)];
                listaTrabajadores.insertarInicioCicular(auxCodigo, auxNombre, auxUsuario, auxContrasena);
            }

        }

        //Lista doble generacion aleatoria, genera lista de usuarios
        private void GenerarListaUsuarios(int U)
        {

            string[] nombres = { "Juan", "Pedro", "Piero", "Eduardo", "Diego", "Raul", "Nicolas", "Eperanza" };
            string usuarios = "User";
            string[] contrasenas = { "Contraseña", "Enemy123", "xXjinx345Xx", "234567890", "akshaniscoming" };
            string[] dias = { "lunes, ", "martes, ", "miércoles, ", "jueves, ", "viernes, ", "sábado, ", "domingo, " };
            string[] meses = { " de enero ", " de febrero ", " de marzo ", " de abril ", " de mayo ", " de junio", " de julio ", " de agosto ", " de septiembre ", " de octubre ", "de noviembre ", " de diciembre " };
            string auxDispositivo;
            string[] Dispos = { "PC", "Play Station", "XBox", "NINTENDO" };
            string[] Preguntas = { "¿Cúal es el nombre de tu primera mascota?", "¿Color favorito?", "¿Cúal es el nombre de tu ex?", "¿Cúal es tu pelicula Favorita?", "¿Cúal es tu apodo o como más te conocen?" };
            string respuesta = "Respuesta";
            string auxFecha;
            int auxCodigo;
            string auxNombre;
            string auxUsuario;
            string auxContrasena;
            double auxSaldo;
            string auxNombrejuego;
            int auxCodCompra;
            string auxPregunta;
            string auxRespuesta;
            List<string> auxLista = new List<string>();
            Random R = new Random();

            for (int i = 0; i < U; i++)
            {
                auxFecha = dias[R.Next(0, 6)] + R.Next(1, 30).ToString() + meses[R.Next(0, 11)] + " de 20" + R.Next(15, 21).ToString();
                auxDispositivo = Dispos[R.Next(0, 3)];
                auxCodigo = R.Next(10000, 99999);
                auxNombre = nombres[R.Next(0, 8)];
                auxUsuario = usuarios + R.Next(1, 100).ToString();
                auxContrasena = contrasenas[R.Next(0, 5)];
                auxSaldo = R.Next(120, 200);
                auxPregunta = Preguntas[R.Next(0, 4)];
                auxRespuesta = respuesta + R.Next(0, 500);
                auxLista.Add("");
                // MessageBox.Show(auxUsuario);
                ListaUsuario.insertarInicioDoble(auxCodigo, auxNombre, auxUsuario, auxContrasena, auxSaldo,auxPregunta,auxRespuesta,"",auxLista,0);
                //Para la venta Aleatoria
                auxNombrejuego = NombreJuegos[R.Next(0, Cantidad)];
                auxCodCompra = 1000 + R.Next(50, 10000);
                GenerarVentasAleatorias(auxUsuario, auxNombrejuego, auxCodCompra,auxFecha, auxDispositivo);
            }

        }


        //Aleatorio de la cola prioridad para crear la lista de proveedores
        private void GenerarProveedores(int N)
        {
            Random rndProveedores = new Random();
            int auxRuc;
            string razon = "Razon Social";
            string auxrazon;
            string producto = "Juego";
            string auxproducto;
            double auxprecio;
            int auxprio;
            
            Cantidad = N;
            for (int i = 0; i < N; i++)
            {
                auxRuc = rndProveedores.Next(100, 9999);
                auxrazon = razon + rndProveedores.Next(0, 100).ToString();
                auxproducto = producto + rndProveedores.Next(0, 100);
                auxprecio = rndProveedores.Next(1, 120);
                auxprio = rndProveedores.Next(1, 3);
                ColaPrioridadProveedores.queuePrioridad(auxRuc, auxrazon, auxproducto, auxprecio, auxprio);
            }

        }

        //Aleatorio de la lista simple para crear la lista de juegos
        private void GenerarListaJuegos(int U)
        {

            int auxCodigo;
            string auxNombre;
            string auxCategoria;
            double auxPrecio;
            Random R = new Random();
            ClaseColaPrioridadProveedores.Nodo auxNodo;


            for (int i = 0; i < Cantidad; i++)
            {
                auxNodo = ColaPrioridadProveedores.dequeuPrioridad();
                auxCodigo = R.Next(1000, 99999);
                auxNombre = auxNodo.nombredelproducto;
                auxCategoria = listaCategoria[R.Next(0, 3)];
                auxPrecio = auxNodo.precio;
                ListaJuegos.insertaInicio(auxCodigo, auxNombre, auxCategoria, auxPrecio, "");
                NombreJuegos.Add(auxNombre);
                ColaAuxiliar.queue(auxNodo.RUC, auxNodo.razonsocial, auxNodo.nombredelproducto, auxNodo.precio, auxNodo.prio);
                ArbolJuegos.insertarJuego(auxCodigo, auxNombre, auxCategoria, auxPrecio, 0);
            }

            ClaseAuxAlmacenDeproveedores.NodoAux auxCola;
            for (int i = 0; i < Cantidad; i++)
            {
                auxCola = ColaAuxiliar.deqeue();
                ColaPrioridadProveedores.queuePrioridad(auxCola.RUC, auxCola.razonsocial, auxCola.nombredelproducto, auxCola.precio, auxCola.prio);
            }

           
        }

        private void GenerarVentasAleatorias(string user, string auxNombrejuego, int auxCodCompra, string auxFecha, string auxDispositivo)
        {
            //Hare las generacion aleatoria de ventas por cada usuario
            //Creo un nodo de la clase de usuario para hacer el recorrido en la lista 
            ClaseRegistroUsuarios.NodoUsuarios recorridoUsuario = ListaUsuario.listaDoble;
            ClaseListaSimpleJuegosAlmacen.NodoJuegosAlmacen recorridoJuegos = ListaJuegos.lista;

            //Creo un random para elegir un juego random para que el usuario compre
            Random rndCompra = new Random();
            string auxNombreUsuario;
            double auxCompra;
            
            

            if (ListaUsuario != null)
            {
                while (recorridoUsuario != null)
                {
                    if (user == recorridoUsuario.Usuario)
                    {
                        auxNombreUsuario = recorridoUsuario.Usuario;


                        while (recorridoJuegos != null)
                        {
                            if (auxNombrejuego == recorridoJuegos.NombredelJuego)
                            {
                                auxCompra = recorridoUsuario.Saldo - recorridoJuegos.Precio;
                                recorridoUsuario.Saldo = auxCompra;
                                
                                BicolaReporte.ponerFrente(auxCodCompra, auxFecha, recorridoJuegos.NombredelJuego, recorridoJuegos.Precio, "ALMACEN GENERAL", auxDispositivo, "CANCELADO", recorridoUsuario.Usuario);
                                PilaBiblioteca.push(recorridoJuegos.Categoria, recorridoJuegos.NombredelJuego, 0, user);

                                //Para agregar mas uno al contador del arbol 
                                ArbolJuegos.ModificarNodoArbol(ArbolJuegos.arbolJ, recorridoJuegos.Codigo, recorridoJuegos.NombredelJuego, recorridoJuegos.Categoria, recorridoJuegos.Precio, 1);
                                return;
                            }
                            recorridoJuegos = recorridoJuegos.sgte;
                        }
                    }

                    recorridoUsuario = recorridoUsuario.sgte;
                }
            }

        }

        

        private void minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRecuperacionDeContraseña Form = new FormRecuperacionDeContraseña(ListaUsuario);
            this.Hide();
            Form.ShowDialog(this);
        }

        int posY = 0;
        int posX = 0;
        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                posX = e.X;
                posY = e.Y;
            }
            else
            {
                Left = Left + (e.X - posX);
                Top = Top + (e.Y - posY);
            }
        }
    }
}