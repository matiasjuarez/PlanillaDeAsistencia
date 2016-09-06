using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Usuario
    {
        private string nombre;
        private string password;
        private RolUsuario rol;

        public Usuario()
        {
            rol = new RolUsuario();
        }

        public string Nombre
        {
            set { nombre = value; }
            get { return nombre; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public RolUsuario Rol
        {
            get { return rol; }
            set { rol = value; }
        }
    }
}
