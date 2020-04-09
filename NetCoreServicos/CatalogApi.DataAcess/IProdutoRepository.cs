using CatalogoApi.Model.Db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssinaturaDocumento.DataAcess
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> Listar();
        //Task<DocumentoDbModel> Obter(Guid Id);        
        //void Inserir(DocumentoDbModel contrato);
        //bool AtualizarIdEnvelope(Guid id, string idEnvelope, int status);
        //void MarcarEnvelopeCompleto(string idEnvelope);
    }
}
