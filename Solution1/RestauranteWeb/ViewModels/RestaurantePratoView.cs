using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteWeb.ViewModels
{
    public class RestaurantePratoView
    {
        public int PratoID { get; set; }

        public string NomePrato { get; set; }

        public double PrecoPrato { get; set; }

        public bool Marcado { get; set; }
    }
}