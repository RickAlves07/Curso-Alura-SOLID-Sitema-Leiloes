using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
	[ApiController]
	[Route("/api/leiloes")]
	public class LeilaoApiController : ControllerBase
	{
		IleilaoDao _leilaoDao;

		public LeilaoApiController(IleilaoDao ileilaoDao)
		{
			_leilaoDao = ileilaoDao;
		}

		[HttpGet]
		public IActionResult EndpointGetLeiloes()
		{
			var leiloes = _leilaoDao.BuscarLeiloesList();
			return Ok(leiloes);
		}

		[HttpGet("{id}")]
		public IActionResult EndpointGetLeilaoById(int id)
		{
			var leilao = _leilaoDao.BuscasLeilaoPorId(id);
			if (leilao == null)
			{
				return NotFound();
			}
			return Ok(leilao);
		}

		[HttpPost]
		public IActionResult EndpointPostLeilao(Leilao leilao)
		{
			_leilaoDao.IncluirLeilao(leilao);
			return Ok(leilao);
		}

		[HttpPut]
		public IActionResult EndpointPutLeilao(Leilao leilao)
		{
			_leilaoDao.AlterarLeilao(leilao);
			return Ok(leilao);
		}

		[HttpDelete("{id}")]
		public IActionResult EndpointDeleteLeilao(int id)
		{
			var leilao = _leilaoDao.BuscasLeilaoPorId(id);
			if (leilao == null)
			{
				return NotFound();
			}
			_leilaoDao.ExcluirLeilao(leilao);
			return NoContent();
		}
	}
}
