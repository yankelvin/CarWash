using System;
using System.Collections.Generic;

#nullable disable

namespace CarWash.WebApp.MVC.Models
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Lavagens = new HashSet<Lavagen>();
        }

        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }

        public virtual ICollection<Lavagen> Lavagens { get; set; }
    }
}
