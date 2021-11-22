using System;
using System.Collections.Generic;

#nullable disable

namespace CarWash.WebApp.MVC.Models
{
    public partial class Veiculo
    {
        public Veiculo()
        {
            Lavagens = new HashSet<Lavagen>();
        }

        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }

        public virtual ICollection<Lavagen> Lavagens { get; set; }
    }
}
