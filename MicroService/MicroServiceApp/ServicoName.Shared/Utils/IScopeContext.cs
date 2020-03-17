using System;

namespace ServicoName.Shared.Utils
{
    public interface IScopeContext
    {
        Guid CorrelationId { get; set; }
        string IdUsuario { get; set; }
        int MinutosReagendamento { get; set; }
        int QuantidadeRetentativa { get; set; }
    }
}