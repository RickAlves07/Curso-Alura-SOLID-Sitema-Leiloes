using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Dados.EfCore
{
	public class LeilaoDaoEfCore : IleilaoDao
	{
		AppDbContext _context;

		public LeilaoDaoEfCore()
		{
			_context = new AppDbContext();
		}

		public IEnumerable<Categoria> BuscarCategorias()
		{
			return _context.Categorias.ToList();
		}

		public Leilao BuscasLeilaoPorId(int id)
		{
			return _context.Leiloes.Find(id);
		}

		public IEnumerable<Leilao> BuscarLeiloesList()
		{
			return _context.Leiloes
				.Include(l => l.Categoria)
				.ToList();
		}

		public void IncluirLeilao(Leilao leilao)
		{
			_context.Leiloes.Add(leilao);
			_context.SaveChanges();
		}

		public void AlterarLeilao(Leilao leilao)
		{
			_context.Leiloes.Update(leilao);
			_context.SaveChanges();
		}

		public void ExcluirLeilao(Leilao leilao)
		{
			_context.Leiloes.Remove(leilao);
			_context.SaveChanges();
		}
	}
}
