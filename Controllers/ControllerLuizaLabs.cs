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

            public ProdutoController()
            {
                _produtoService = new ProdutoService();
            }

            [HttpPost("popular")]
            public IActionResult PopularProdutos()
            {
                _produtoService.PopularProdutos();
                return Ok("Banco populado com 40 produtos aleatórios!");
            }


        // POST api/produto
        [HttpPost]
        public ActionResult<Produto> CriarProduto(Produto novoProduto)
        {
            using var db = new LiteDatabase(DATABASE);
            var colecao = db.GetCollection<Produto>(COLLECTION);

            var ultimoId = colecao.Query().OrderByDescending(p => p.Id).Limit(1).FirstOrDefault()?.Id ?? 0;
            novoProduto.Id = ultimoId + 1;

            colecao.Insert(novoProduto);

            return CreatedAtAction(nameof(GetPorId), new { id = novoProduto.Id }, novoProduto);
        }


        // DELETE api/produto/2
        [HttpDelete("{id}")]
        public ActionResult DeletarProduto(int id)
        {
            using var db = new LiteDatabase(DATABASE);
            var colecao = db.GetCollection<Produto>(COLLECTION);
            var sucesso = colecao.Delete(id);

            if (!sucesso)
                return NotFound("Produto não encontrado.");

            return NoContent(); // 204
        }

        // GET api/produto/{id}
        [HttpGet("{id}")]
        public ActionResult<Produto> GetPorId(int id)
        {
            using var db = new LiteDatabase(DATABASE);
            var colecao = db.GetCollection<Produto>(COLLECTION);
            var produto = colecao.FindById(id);

            if (produto == null)
                return NotFound("Produto não encontrado.");

            return Ok(produto);
        }

        // GET api/produto
        [HttpGet]
        public ActionResult<List<Produto>> GetTodos()
        {
            using var db = new LiteDatabase(DATABASE);
            var colecao = db.GetCollection<Produto>(COLLECTION);
            var produtos = colecao.FindAll().ToList();

            return Ok(produtos);
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

            produto.Nome = produtoAtualizado.Nome;
            produto.Preco = produtoAtualizado.Preco;

            colecao.Update(produto);

            return Ok(produto);
        }
    }

}
