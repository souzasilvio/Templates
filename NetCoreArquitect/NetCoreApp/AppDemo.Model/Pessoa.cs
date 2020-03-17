using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppDemo.Model
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public int Nome { get; set; }

        public List<Habilidade> Habililidades { get; set; }
    }
}
