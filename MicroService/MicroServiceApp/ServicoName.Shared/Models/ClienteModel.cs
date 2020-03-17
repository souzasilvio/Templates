using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServicoName.Shared.Models
{
    [Table("Cliente")]
    public class ClienteModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string Email { get; set; }
    }
}
