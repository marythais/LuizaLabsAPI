using LiteDB;
using LuizaLabs.Models;
using LuizaLabs.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LuizaLabs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private const string DATABASE = "produtos.db";
        private const string COLLECTION = "produtos";

        private readonly ProdutoService _produtoService;
        private readonly OutlistService _outlistService;

        public ProdutoController()
        {
            _produtoService = new ProdutoService();
            _outlistService = new OutlistService(); 
        }

        // PUT api/produto/{id}
        [HttpPut("{id}")]
        public ActionResult AtualizarProduto(int id, Produto produtoAtualizado)
        {
            using var db = new LiteDatabase(DATABASE);
            var colecao = db.GetCollection<Produto>(COLLECTION);
            var produto = colecao.FindById(id);


            if (produto == null)
                return NotFound("Produto n�o encontrado.");

            // Verifica se o produto est� na Outlist
            bool bloqueado = _outlistService.EstaNaOutlist(id);

            if (bloqueado && produtoAtualizado.Preco != produto.Preco)
                return BadRequest("Este produto est� na Outlist e n�o pode ser alterado no per�odo de vig�ncia.");

            produto.Nome = produtoAtualizado.Nome;

            // S� altera o pre�o se n�o estiver bloqueado
            if (!bloqueado)
            {
                produto.Preco = produtoAtualizado.Preco;
            }

            colecao.Update(produto);
            return Ok(produto);
        }

    
    }
}
