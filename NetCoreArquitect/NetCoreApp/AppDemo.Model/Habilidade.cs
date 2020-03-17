using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppDemo.Model
{
    public class Habilidade
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
