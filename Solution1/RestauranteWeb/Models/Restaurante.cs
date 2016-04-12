using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestauranteWeb.Models
{
    public class Restaurante
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome do Restaurante é obrigatório.")]
        [StringLength(30, ErrorMessage = "O nome do Restaurante não pode ter mais que 30 caracteres.")]
        public string NomeRestaurante { get; set; }

        public virtual ICollection<Prato> Pratos { get; set; }

    }
}