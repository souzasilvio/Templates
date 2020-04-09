using AssinaturaDocumento.DataAcess;
using System;

namespace CatalogoApi.Dominio
{
    public class Catalogo : ICatalogo
    {
        private readonly IProdutoRepository produtoRepository;
        public Catalogo(IProdutoRepository repository)
        {
            produtoRepository = repository; 
        }
    }
}
