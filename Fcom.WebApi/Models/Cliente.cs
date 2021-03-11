using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fcom.WebApi.Models
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Nome do Cliente é campo obrigatório!")]
        [MinLength(3, ErrorMessage ="Nome do Cliente deve possuir pelo menos 3 caracteres.")]
        public string Nome { get; set; }

        public int Idade { get; set; }
    }
}
