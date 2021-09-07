using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Z.Dapper.Plus;

namespace Projeto.Data
{
	public class UsuarioRepository : IBaseRepository<Usuario>, IUsuarioRepository
	{

		private IDbConnection _db;

		public UsuarioRepository(string connectionString)
		{
			this._db = new SqlConnection(connectionString);
		}


		public Usuario Altualizar(Usuario entidade)
		{
			this._db.Execute(@"update usuarios set nome=@nome,
						email=@email, sexo=@sexo, rg=@rg, cpf=@cpf, nomemae=@nomemae, situacaocadastro=@situacaocadastro, 
													datacadastro=@datacadastro where Id= @id", entidade);
			return entidade;
		}
		public Usuario BuscaCompletaComJoin(int id)
		{
			string sql = @"select * from usuarios 
							left join contatos on usuarios.id = contatos.usuarioid
							left join enderecosentrega on enderecosentrega.usuarioid = usuarios.id
							where usuarios.id = @id;";


			var usuarioDic = new Dictionary<int, Usuario>();

			var usuario =  this._db.Query<Usuario, Contato, EnderecoEntrega, Usuario>(sql,(usuario, contato, endereco) => 
								{
									if (usuarioDic.Count == 0)
									{
										usuarioDic.Add(usuario.Id, usuario);
										usuario.EnderecoEntrega = new List<EnderecoEntrega>();
									}

									var usuarioDoDic = usuarioDic.GetValueOrDefault(usuario.Id);

									//usuario.Contato = contato;
									//usuario.EnderecoEntrega.Add(endereco);
									usuarioDoDic.Contato = contato;
									
									usuarioDoDic.EnderecoEntrega.Add(endereco);
									return usuario;
								},new { id = id });


			return usuarioDic[usuarioDic.Keys.First()];
		}
		public Usuario BuscaCompleta(int id)
		{
			string sql = @"select * from usuarios where id = @id;
											select * from contatos where usuarioid = @id;
											select * from enderecosentrega where  usuarioid = @id;
			";
					using (var multploResult = this._db.QueryMultiple(sql, new { id = id }))
					{
						var Usuario = multploResult.Read<Usuario>().SingleOrDefault();
						var Contato = multploResult.Read<Contato>().SingleOrDefault();
						var EnderecoEntrega = multploResult.Read<EnderecoEntrega>().ToList();

							if (Usuario != null)
							{
								if (Contato != null)
								{
									Usuario.Contato = Contato;
								}
								if (EnderecoEntrega != null)
								{
									Usuario.EnderecoEntrega = EnderecoEntrega;
								}
							}
						return Usuario;
					}
		}
		public Usuario Busca(int id)
		{
			//3 opções
			//return this._db.Query<Usuario>($"select * from usuarios where id = @Id", new { Id  = id} ).SingleOrDefault();
			//return this._db.QueryFirstOrDefault<Usuario>($"select * from usuarios where id = @Id", new { Id = id });
			return this._db.QuerySingle<Usuario>($"select * from usuarios where id = @Id", new { Id = id });
		}
		public Usuario Cadastrar(Usuario entidade)
		{
			//var trans = this._db.BeginTransaction();

			string sql = @"insert into usuarios (nome, email, sexo, rg, cpf, nomemae, situacaocadastro, datacadastro) 
													values (@nome, @email, @sexo, @rg, @cpf, @nomemae, @situacaocadastro, @datacadastro); 
												select cast(scope_identity() as int);";


			//this._db.Execute
			//, transaction: trans
			entidade.Id = this._db.Query<int>(sql, entidade).Single();

			//trans.Commit();

			return entidade;
		}
		public void Exluir(int id)
		{
			this._db.Execute("delete from usuarios where id= @id", new { id = id });
		}
		public List<Usuario> PegarTodos()
		{
			
			return this._db.Query<Usuario>("select * from usuarios").ToList();
		}

		public Usuario BuscaComJoinDepartamentos(int id)
		{
			string sql = @"select 
										Usuarios.id, 
										Usuarios.Nome,
										Usuarios.Email,
										Usuarios.Sexo,
										Usuarios.RG,
										Usuarios.CPF,
										Usuarios.NomeMae,
										Usuarios.SituacaoCadastro,
										Usuarios.DataCadastro,
										Departamentos.id as DepartamentoId,
										Departamentos.nome
										from Usuarios 
											join UsuariosDepartamentos on Usuarios.Id = UsuariosDepartamentos.UsuarioId
											join Departamentos on Departamentos.Id = UsuariosDepartamentos.DepartamentoId
											where Usuarios.id = @id";


			var usuarioDic = new Dictionary<int, Usuario>();

			this._db.Query<Usuario, Departamento, Usuario>(sql, (usuario, departamento) =>
			{
				if (!usuarioDic.TryGetValue(usuario.Id, out var usuarioAtual))
				{
					usuarioAtual = usuario;
					usuarioDic.Add(usuarioAtual.Id, usuarioAtual);
				}
				usuarioAtual.Departamento = usuarioAtual.Departamento ?? new List<Departamento>();
				usuarioAtual.Departamento.Add(departamento);
				return usuarioAtual;

			}, new { id = id }, splitOn: "DepartamentoId");

			return usuarioDic[usuarioDic.Keys.First()];
		}

		public Usuario BuscaUsuarioDapperProcedure(int id)
		{
			return this._db.Query<Usuario>("GetUsuarioNome", new { id = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();

		}

		public void CadastrarUsuarioEmLote(List<Usuario> usuario)
		{
			DapperPlusManager.Entity<Usuario>().Table("Usuarios");

			this._db.BulkInsert(usuario);

		}

		public List<Usuario> PegarApenas(params int[] ids)
		{
			string sql = "select * from usuarios where id in @ids";

			return this._db.Query<Usuario>(sql, new { ids = ids}).ToList();
		}
	}
}
