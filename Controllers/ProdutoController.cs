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
                return NotFound("Produto não encontrado.");

            // Verifica se o produto está na Outlist
            bool bloqueado = _outlistService.EstaNaOutlist(id);

            if (bloqueado && produtoAtualizado.Preco != produto.Preco)
                return BadRequest("Este produto está na Outlist e não pode ser alterado no período de vigência.");

            produto.Nome = produtoAtualizado.Nome;

            // Só altera o preço se não estiver bloqueado
            if (!bloqueado)
            {
                produto.Preco = produtoAtualizado.Preco;
            }

            colecao.Update(produto);
            return Ok(produto);
        }

    
    }
}
