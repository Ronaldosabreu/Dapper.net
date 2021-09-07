using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Data
{
	public interface IBaseRepository<T>
	{
		//crud
		T Busca(int id);

		List<T> PegarTodos();

		T Cadastrar(T entidade);

		T Altualizar(T entidade);

		void Exluir(int id);


	}
}
