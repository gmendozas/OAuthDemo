using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthDemo.Models
{
    public class LoginModel
    {
        public static bool LoginUser()
        {
            return true;
        }
    }

    public class EcoColaborador
    {
        public int NumeroNomina { get; set; }
        public string Password { get; set; }
        public string DispositivoMovil { get; set; }
        public string Chip { get; set; }

        public bool IniciarSesion()
        {
            Random r = new Random();            
            return r.Next(0, 2) == 1 ? true : false;
        }
    }
}