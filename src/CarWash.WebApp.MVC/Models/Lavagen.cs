using System;
using System.Collections.Generic;

#nullable disable

namespace CarWash.WebApp.MVC.Models
{
    public partial class Lavagen
    {
        public string Cpf { get; set; }
        public string Placa { get; set; }
        public DateTime DataLavagem { get; set; }
        public int CodTipoLavagem { get; set; }
        public decimal Valor { get; set; }

        public virtual TipoLavagem CodTipoLavagemNavigation { get; set; }
        public virtual Funcionario CpfNavigation { get; set; }
        public virtual Veiculo PlacaNavigation { get; set; }
    }
}
