using Alura.LeilaoOnline.WebApp.Models;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Dados
{
	public interface IleilaoDao
	{
		IEnumerable<Categoria> BuscarCategorias();

		Leilao BuscasLeilaoPorId(int id);

		IEnumerable<Leilao> BuscarLeiloesList();

		void IncluirLeilao(Leilao leilao);

		void AlterarLeilao(Leilao leilao);

		void ExcluirLeilao(Leilao leilao);
	}
}
