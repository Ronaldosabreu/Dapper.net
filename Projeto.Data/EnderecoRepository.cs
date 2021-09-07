using Dapper.Contrib.Extensions;
using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Data
{
	public class EnderecoRepository : IBaseRepository<EnderecoEntrega>
	{

		private IDbConnection _db;

		public EnderecoRepository(string connectionString)
		{
			this._db = new SqlConnection(connectionString);
		}

		public EnderecoEntrega Altualizar(EnderecoEntrega entidade)
		{
			this._db.Update<EnderecoEntrega>(entidade);
			return entidade;
		}

		public EnderecoEntrega Busca(int id)
		{
			return this._db.Get<EnderecoEntrega>(id);
		}

		public EnderecoEntrega Cadastrar(EnderecoEntrega entidade)
		{
			entidade.Id = Convert.ToInt32(this._db.Insert<EnderecoEntrega>(entidade));
			return entidade;
		}

		public void Exluir(int id)
		{
			var contato = Busca(id);
			this._db.Delete<EnderecoEntrega>(contato);
		}

		public List<EnderecoEntrega> PegarTodos()
		{
			return this._db.GetAll<EnderecoEntrega>().ToList();
		}
	}
}