using System;
using System.Collections.Generic;

#nullable disable

namespace CarWash.WebApp.MVC.Models
{
    public partial class TipoLavagem
    {
        public TipoLavagem()
        {
            Lavagens = new HashSet<Lavagen>();
        }

        public int Codigo { get; set; }
        public string NomeLavagem { get; set; }

        public virtual ICollection<Lavagen> Lavagens { get; set; }
    }
}
