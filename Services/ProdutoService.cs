using LiteDB;
using System;
using System.Collections.Generic;
using LuizaLabs.Models;

namespace LuizaLabs.Services
{
    public class ProdutoService
    {
        private const string DATABASE = @"produtos.db";
        private const string COLLECTION = "produtos";

        private static readonly string[] nomesBase = new[]
        {
            "Tênis", "Meia", "Sapato", "Chinelo", "Bota", "Sandália", "Tênis de corrida", "Meia esportiva",
            "Tênis casual", "Sapato social", "Meia de algodão", "Bota de couro", "Sapato de couro", "Sandália feminina",
            "Tênis infantil", "Meia soquete", "Tênis de basquete", "Chinelo de dedo", "Bota impermeável", "Meia cano alto"
        };

        public void PopularProdutos()
        {
            var random = new Random();

            using var db = new LiteDatabase(DATABASE);
            var colecao = db.GetCollection<Produto>(COLLECTION);

            colecao.DeleteAll();

            for (int i = 1; i <= 40; i++)
            {
                var nome = nomesBase[random.Next(nomesBase.Length)];
                var sufixo = random.Next(1, 100);
                var preco = Math.Round((decimal)(random.NextDouble() * 300 + 20), 2);

                var produto = new Produto
                {
                    Id = i,
                    Nome = $"{nome} {sufixo}",
                    Preco = preco
                };

                colecao.Insert(produto);
            }
        }

        public IEnumerable<Produto> GetTodos()
        {
            using var db = new LiteDatabase(DATABASE);
            var colecao = db.GetCollection<Produto>(COLLECTION);
            return colecao.FindAll();
        }
    }

}
