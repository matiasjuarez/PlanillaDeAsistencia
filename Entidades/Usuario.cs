using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    [Serializable]
    public class Usuario
    {
        private string nombre;
        private string password;
        private bool habilitado;
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

        public bool Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }

        public RolUsuario Rol
        {
            get { return rol; }
            set { rol = value; }
        }
    }
}