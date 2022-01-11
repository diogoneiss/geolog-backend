using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoLogBackend.GeoLogBackend.Dominio.Entidades
{
    public class LoginRequest
    {
        public LoginRequest(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public LoginRequest()
        {
        }

        public String Email { get; set; }
        public String Senha { get; set; }
    }
}
