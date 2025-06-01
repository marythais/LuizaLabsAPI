using LiteDB;
using LuizaLabs.Models;

namespace LuizaLabs.Services
{
    public class OutlistService
    {
        private const string DATABASE = "produtos.db";
        private const string COLLECTION = "outlist";

        public void Adicionar(OutlistItem item)
        {
            using var db = new LiteDatabase(DATABASE);
            var col = db.GetCollection<OutlistItem>(COLLECTION);
            col.Insert(item);
        }

        public bool Remover(int id)
        {
            using var db = new LiteDatabase(DATABASE);
            var col = db.GetCollection<OutlistItem>(COLLECTION);
            return col.Delete(id);
        }

        public void AlterarVigencia(int id, DateTime inicio, DateTime termino)
        {
            using var db = new LiteDatabase(DATABASE);
            var col = db.GetCollection<OutlistItem>(COLLECTION);
            var item = col.FindById(id);
            if (item != null)
            {
                item.DataVigenciaInicio = inicio;
                item.DataVigenciaTermino = termino;
                col.Update(item);
            }
        }

        public List<OutlistItem> ListarTodos()
        {
            using var db = new LiteDatabase(DATABASE);
            var col = db.GetCollection<OutlistItem>(COLLECTION);
            return col.FindAll().ToList();
        }

        public bool EstaNaOutlist(int produtoId)
        {
            using var db = new LiteDatabase(DATABASE);
            var col = db.GetCollection<OutlistItem>(COLLECTION);
            var agora = DateTime.UtcNow;

            return col.Find(x =>
                x.ProdutoId == produtoId &&
                x.DataVigenciaInicio <= agora &&
                x.DataVigenciaTermino >= agora
            ).Any();
        }
    }
}
