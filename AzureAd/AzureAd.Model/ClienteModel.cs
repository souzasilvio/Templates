using System;
using System.Collections.Generic;

namespace AzureAd.Model
{
    public class ClienteModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public static List<ClienteModel> GetTestData()
        {
            var lista = new List<ClienteModel>();
            for (int i = 1; i <= 10; i++)
            {
                lista.Add(new ClienteModel() { Id = Guid.NewGuid(), Nome = $"Cliente {i}"});
            }
            return lista;
        }

        public static ClienteModel GetTestDataById(string id)
        {
            return new ClienteModel() { Id = Guid.NewGuid(), Nome = $"Cliente {id}" };
        }

    }
}
