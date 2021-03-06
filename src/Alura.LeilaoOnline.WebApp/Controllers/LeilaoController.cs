using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
	public class LeilaoController : Controller
	{
		IleilaoDao _leilaoDao;

		public LeilaoController(IleilaoDao ileilaoDao)
		{
			_leilaoDao = ileilaoDao;
		}

		public IActionResult Index()
		{
			var leiloes = _leilaoDao.BuscarLeiloesList();
			return View(leiloes);
		}

		[HttpGet]
		public IActionResult Insert()
		{
			ViewData["Categorias"] = _leilaoDao.BuscarCategorias();
			ViewData["Operacao"] = "Inclusão";
			return View("Form");
		}

		[HttpPost]
		public IActionResult Insert(Leilao model)
		{
			if (ModelState.IsValid)
			{
				_leilaoDao.IncluirLeilao(model);
				return RedirectToAction("Index");
			}
			ViewData["Categorias"] = _leilaoDao.BuscarCategorias();
			ViewData["Operacao"] = "Inclusão";
			return View("Form", model);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			ViewData["Categorias"] = _leilaoDao.BuscarCategorias();
			ViewData["Operacao"] = "Edição";
			var leilao = _leilaoDao.BuscasLeilaoPorId(id);
			if (leilao == null) return NotFound();
			return View("Form", leilao);
		}

		[HttpPost]
		public IActionResult Edit(Leilao model)
		{
			if (ModelState.IsValid)
			{
				_leilaoDao.AlterarLeilao(model);
				return RedirectToAction("Index");
			}
			ViewData["Categorias"] = _leilaoDao.BuscarCategorias();
			ViewData["Operacao"] = "Edição";
			return View("Form", model);
		}

		[HttpPost]
		public IActionResult Inicia(int id)
		{
			var leilao = _leilaoDao.BuscasLeilaoPorId(id);
			if (leilao == null) return NotFound();
			if (leilao.Situacao != SituacaoLeilao.Rascunho) return StatusCode(405);
			leilao.Situacao = SituacaoLeilao.Pregao;
			leilao.Inicio = DateTime.Now;
			_leilaoDao.AlterarLeilao(leilao);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Finaliza(int id)
		{
			var leilao = _leilaoDao.BuscasLeilaoPorId(id);
			if (leilao == null) return NotFound();
			if (leilao.Situacao != SituacaoLeilao.Pregao) return StatusCode(405);
			leilao.Situacao = SituacaoLeilao.Finalizado;
			leilao.Termino = DateTime.Now;
			_leilaoDao.AlterarLeilao(leilao);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Remove(int id)
		{
			var leilao = _leilaoDao.BuscasLeilaoPorId(id);
			if (leilao == null) return NotFound();
			if (leilao.Situacao == SituacaoLeilao.Pregao) return StatusCode(405);
			_leilaoDao.ExcluirLeilao(leilao);
			return NoContent();
		}

		[HttpGet]
		public IActionResult Pesquisa(string termo)
		{
			ViewData["termo"] = termo;
			var leiloes = _leilaoDao.BuscarLeiloesList()
				.Where(l => string.IsNullOrWhiteSpace(termo) ||
					l.Titulo.ToUpper().Contains(termo.ToUpper()) ||
					l.Descricao.ToUpper().Contains(termo.ToUpper()) ||
					l.Categoria.Descricao.ToUpper().Contains(termo.ToUpper())
				);
			return View("Index", leiloes);
		}
	}
}
