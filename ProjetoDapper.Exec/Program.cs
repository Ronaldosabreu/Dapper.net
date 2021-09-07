using Projeto.Data;
using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;

namespace ProjetoDapper.Exec
{
	class Program
	{
		static string conString = @"Data Source = NOTBD7C5G3; Initial Catalog = DbProjetoDapper; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
		static readonly IBaseRepository<Usuario> respository = new UsuarioRepository(conString);

		static readonly IUsuarioRepository UsuarioRespository = new UsuarioRepository(conString);

		static void Main(string[] args)
		{

			//PegarTodosUsuariosComDapper();
			//BuscarUsuarioId();
			//CadastrarUsuario();
			//AtualizarUsuario();
			//ExcluirUsuario();
			//BuscaCompleta();
			//BuscaCompletaComJoin();
			//BuscaComJoinDepartamentos();

			//ContatoComDapperContrib();
			//buscaUserStoraged();

			//InsertInLoteDapperPlus();
			PegarApenasPorArray();
			Console.WriteLine("Hello World!");
		}

		static void PegarApenasPorArray()
		{

			var usuarios = UsuarioRespository.PegarApenas(1,2,3,4,5);
			foreach (var usuario in usuarios)
			{
				Console.WriteLine(usuario.Nome);
			}
		}

		static void InsertInLoteDapperPlus()
		{
			Usuario usuario01 = new Usuario()
			{
				Nome = "Joice Fidel abreu",
				Email = "joice@gmail.com",
				Rg = "rg",
				Cpf = "cpf",
				Sexo = "f",
				NomeMae = "Margarida",
				SituacaoCadastro = "a",
				DataCadastro = DateTime.Now
			};
			Usuario usuario02 = new Usuario()
			{
				Nome = "Ronaldo",
				Email = "Ronaldo@gmail.com",
				Rg = "rgcdds",
				Cpf = "cpffsdfsd",
				Sexo = "ffsdfds",
				NomeMae = "Margaridafsdfs",
				SituacaoCadastro = "afsd",
				DataCadastro = DateTime.Now
			};


			var user = new List<Usuario>() { usuario01, usuario02 };



			UsuarioRespository.CadastrarUsuarioEmLote(user);


		}

		static void buscaUserStoraged()
		{
			var retorno = UsuarioRespository.BuscaUsuarioDapperProcedure(1);

			Console.WriteLine(retorno.Nome + " - " + retorno.Id);
		}


		static void ContatoComDapperContrib()
		{
			IBaseRepository<Contato> repositoru = new ContatoRepository(conString);

			//busca
			var contatoNome = repositoru.Busca(6);
			Console.WriteLine($"contato : {contatoNome.Telefone}");


			//cadastras
			var contato = new Contato()
			{
				Id = contatoNome.Id,
				Telefone = "5645646",
				Celular = "111",
				UsuarioId = 1
			};

			//repositoru.Cadastrar(contato);
			//Console.WriteLine($"contato id: {contato.Id}");

			repositoru.Altualizar(contato);
			Console.WriteLine($"contato atualizado: {contato.Telefone}");

			//repositoru.Exluir(contato.Id);

		}

		static void ExcluirUsuario()
		{
			respository.Exluir(2);
			Console.WriteLine($"Excluido com sucesso");
		}

		static void AtualizarUsuario()
		{
			Usuario usuario = new Usuario()
			{
				Id = 2,
				Nome = "Joice Silva Fidel abreu",
				Email = "joice.fidel@hotmail.com",
				Rg = "rg",
				Cpf = "cpf",
				Sexo = "f",
				NomeMae = "Margarida Silva Barbosa",
				SituacaoCadastro = "a",
				DataCadastro = DateTime.Now
			};
			IBaseRepository<Usuario> respository = new UsuarioRepository(conString);
			respository.Altualizar(usuario);

			Console.WriteLine($"Usuario alterado {usuario.Nome}");
		}

		static void CadastrarUsuario()
		{
			Usuario usuario = new Usuario() 
			{
					Nome = "Joice Fidel abreu", Email="joice@gmail.com",
					Rg= "rg", Cpf = "cpf",Sexo = "f", NomeMae="Margarida",
					SituacaoCadastro="a", DataCadastro=DateTime.Now
			};


			respository.Cadastrar(usuario);
			Console.WriteLine($"Usuario inserido: {usuario.Id}");
		}
		static void BuscaComJoinDepartamentos()
		{
			var usuario = UsuarioRespository.BuscaComJoinDepartamentos(1);
			if (usuario != null)
			{
				Console.WriteLine($@"Usuario localizado: {usuario.Nome}");
				foreach (var departamento in usuario.Departamento)
				{
					Console.WriteLine($@"{departamento.Nome}");
				}

			}
			else
			{
				Console.WriteLine($"Usuario não localizado");
			}
		}
		static void BuscaCompletaComJoin()
		{
			var usuario = UsuarioRespository.BuscaCompletaComJoin(1);
			if (usuario != null)
			{
				Console.WriteLine($@"Usuario localizado: {usuario.Contato.Celular}, {usuario.EnderecoEntrega.Count}");
				foreach ( var endereco in usuario.EnderecoEntrega)
				{
					Console.WriteLine($@"{endereco.NomeEndereco}");
				}

			}
			else
			{
				Console.WriteLine($"Usuario não localizado");
			}

		}
		static void BuscaCompleta()
		{


			var usuario = UsuarioRespository.BuscaCompleta(1);
			if (usuario != null)
			{
				Console.WriteLine($@"Usuario localizado: {usuario.Contato.Celular}, 
									{usuario.EnderecoEntrega.Count}");
			}
			else
			{
				Console.WriteLine($"Usuario não localizado");
			}

		}
		static void BuscarUsuarioId()
		{
			

			var usuario = respository.Busca(1);
			if (usuario != null) 
			{
				Console.WriteLine($"Usuario localizado: {usuario.Nome}");
			}
			else 
			{
				Console.WriteLine($"Usuario não localizado");
			}

		}

		static void PegarTodosUsuariosComDapper()
		{ 
			

			Console.WriteLine($"Quantidade de usuario:  {respository.PegarTodos().Count} ");
			

		}
	}
}
