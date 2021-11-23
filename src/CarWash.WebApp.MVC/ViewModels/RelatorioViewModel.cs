using CarWash.WebApp.MVC.Models;
using System.Collections.Generic;

namespace CarWash.WebApp.MVC.ViewModels
{
    public class RelatorioViewModel
    {
        public List<Lavagen> Lavagens { get; set; }
        public decimal TotalArrecadado { get; set; }
    }
}
