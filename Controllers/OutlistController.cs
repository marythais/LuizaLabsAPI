using LiteDB;
using LuizaLabs.Models;
using LuizaLabs.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuizaLabs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OutlistController : ControllerBase
    {
        private readonly OutlistService _outlistService;

        public OutlistController()
        {
            _outlistService = new OutlistService();
        }

        // POST /outlist
        [HttpPost]
        public IActionResult Adicionar([FromBody] AdicionarOutlistRequest request)
        {
            var novoItem = new OutlistItem
            {
                ProdutoId = request.ProdutoId,
                DataVigenciaInicio = request.DataVigenciaInicio,
                DataVigenciaTermino = request.DataVigenciaTermino
            };

            _outlistService.Adicionar(novoItem);

            return Ok("Produto adicionado à Outlist com sucesso.");
        }

       

        // DELETE /outlist/{id}
        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            var estaBloqueado = _outlistService.EstaNaOutlist(id);
            if (estaBloqueado)
                return BadRequest("Produto está com vigência ativa e não pode ser removido da Outlist.");

            var sucesso = _outlistService.Remover(id);
            if (!sucesso)
                return NotFound("Item não encontrado na Outlist.");

            return NoContent();
        }
        

        // PUT /outlist/{id}/vigencia
        [HttpPut("{id}/vigencia")]
        public IActionResult AlterarVigencia(int id, [FromBody] AlterarVigenciaRequest request)
        {
            var estaBloqueado = _outlistService.EstaNaOutlist(id);
            if (estaBloqueado)
                return BadRequest("Produto está com vigência ativa e não pode ter a vigência alterada.");

            _outlistService.AlterarVigencia(id, request.DataInicio, request.DataTermino);
            return Ok("Vigência alterada com sucesso.");
        }

        // GET /outlist
        [HttpGet]
        public ActionResult<IEnumerable<OutlistItem>> ListarTodos()
        {
            var lista = _outlistService.ListarTodos();
            return Ok(lista);
        }

        // GET /outlist/verificar/{produtoId}
        [HttpGet("verificar/{produtoId}")]
        public IActionResult VerificarSeEstaNaOutlist(int produtoId)
        {
            var bloqueado = _outlistService.EstaNaOutlist(produtoId);
            return Ok(new { ProdutoId = produtoId, Bloqueado = bloqueado });
        }

        public class AlterarVigenciaRequest
        {
            public DateTime DataInicio { get; set; }
            public DateTime DataTermino { get; set; }
        }
    }
}
