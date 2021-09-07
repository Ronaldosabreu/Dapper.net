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
	public class DepartamentoRepository : IBaseRepository<Departamento>
	{

		private IDbConnection _db;

		public DepartamentoRepository(string connectionString)
		{
			this._db = new SqlConnection(connectionString);
		}

		public Departamento Altualizar(Departamento entidade)
		{
			this._db.Update<Departamento>(entidade);
			return entidade;
		}

		public Departamento Busca(int id)
		{
			return this._db.Get<Departamento>(id);
		}

		public Departamento Cadastrar(Departamento entidade)
		{
			entidade.Id = Convert.ToInt32(this._db.Insert<Departamento>(entidade));
			return entidade;
		}

		public void Exluir(int id)
		{
			var contato = Busca(id);
			this._db.Delete<Departamento>(contato);
		}

		public List<Departamento> PegarTodos()
		{
			return this._db.GetAll<Departamento>().ToList();
		}
	}
}
