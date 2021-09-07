using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace Projeto.Data
{


	public class ContatoRepository : IBaseRepository<Contato>
	{

		private IDbConnection _db;

		public ContatoRepository(string connectionString)
		{
			this._db = new SqlConnection(connectionString);
		}

		public Contato Altualizar(Contato entidade)
		{
			this._db.Update<Contato>(entidade);
			return entidade;
		}

		public Contato Busca(int id)
		{
			return this._db.Get<Contato>(id);
		}

		public Contato Cadastrar(Contato entidade)
		{
			entidade.Id = Convert.ToInt32(this._db.Insert<Contato>(entidade));
			return entidade;
		}

		public void Exluir(int id)
		{
			var contato = Busca(id);
			this._db.Delete<Contato>(contato);
		}

		public List<Contato> PegarTodos()
		{
			return this._db.GetAll<Contato>().ToList();
		}
	}
}
