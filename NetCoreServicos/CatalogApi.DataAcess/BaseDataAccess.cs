using CatalogoApi.Model.Db;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.DataAcess
{
    public class BaseDataAccess<T> where T : BaseDbModel
    {
        protected IDbConnection Connection { get; }

        public BaseDataAccess(IDbConnection connection)
        {
            Connection = connection;
        }

        public async Task<T> Obter(string query, Guid Id)
        {
            var parametros = new DynamicParameters();
            if (Id != Guid.Empty)
            {
                parametros.Add("@Id", Id, DbType.Guid);
                query += " where c.Id = @Id";
            }

            try
            {
                Connection.Open();
                var retorno = await Connection.QueryFirstOrDefaultAsync<T>(query, parametros);
                return retorno;
            }
            finally
            {
                Connection.Close();
            }
        }

        public int Execute(string query, DynamicParameters parametros)
        {
            try
            {
                Connection.Open();
                return Connection.Execute(query, parametros);
            }
            finally
            {
                Connection.Close();
            }
        }

        public async Task<List<T>> Listar()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var retorno = dbConnection.GetAll<T>().ToList();
                dbConnection.Close();
                return retorno;
            }
        }

        //public void Inserir(T registro)
        //{
        //    try
        //    {
        //        Connection.Open();
        //        Connection.Insert<T>(registro);
        //    }
        //    finally
        //    {
        //        Connection.Close();
        //    }
        //}

        ///// <summary>
        ///// Excluir registro
        ///// </summary>
        ///// <param name="registro">Objeto a excluir</param>
        //public async void Excluir(T registro)
        //{
        //    using (IDbConnection dbConnection = Connection)
        //    {
        //        dbConnection.Open();
        //        dbConnection.Delete<T>(registro);
        //        dbConnection.Close();
        //    }
        //}



    }
}
