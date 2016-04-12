using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RestauranteWeb.Models
{
    public class Prato
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome do Prato é obrigatório.")]
        [StringLength(30, ErrorMessage = "O nome do Prato não pode ter mais que 30 caracteres.")]
        public string NomePrato { get; set; }

        [Required(ErrorMessage = "O preço do Prato é obrigatório.")]
        public double PrecoPrato { get; set; }

        public virtual ICollection<Restaurante> Restaurantes { get; set; }
    }
}
