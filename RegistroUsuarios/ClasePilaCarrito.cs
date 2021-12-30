namespace RegistroUsuarios
{
    public class ClasePilaCarrito
    {
        public class NodoCarrito
        {
            public string User;
            public int CodigoJuego;
            public string categoriaCarrito;
            public string nombreCarrito;
            public double precioCarrito;
            public NodoCarrito sgte;
            
            public NodoCarrito(string CatCarrito, int codigo, string NomCarrito, double PrecioCarrito, string user)
            {
                categoriaCarrito = CatCarrito;
                nombreCarrito = NomCarrito;
                precioCarrito = PrecioCarrito;
                CodigoJuego = codigo;
                User = user;
                sgte = null;
            }
        }

        public class PilaCarrito
        {
            public NodoCarrito pila;
            public PilaCarrito()
            {
                pila = null;
            }

            public void push(string CatCarrito, int codigo,string NomCarrito, double PrecioCarrito, string user)
            {
                NodoCarrito q = new NodoCarrito(CatCarrito, codigo,NomCarrito, PrecioCarrito, user);

                q.sgte = pila;
                pila = q;
            }

            public NodoCarrito pop(string User)
            {
                NodoCarrito aux = pila;

                string nom;
                while (aux != null)
                {
                    if (aux.User == User)
                    {
                        if (pila != null)
                        {
                            nom = aux.nombreCarrito;
                            pila = aux.sgte;
                        }
                        return aux;
                    }
                    aux = aux.sgte;
                } 
                return null;
            }

            public bool VerificarsiExiste(string nombre, string user)
            {
                NodoCarrito t = pila;
                if (pila != null)
                {
                    while (t.sgte != null)
                    {
                        if (nombre == t.nombreCarrito && user == t.User)
                        {
                            return true;
                        }
                        t = t.sgte;
                    } 
                }
                return false;
            }

        }
    }
}
