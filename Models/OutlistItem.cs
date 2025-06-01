using LiteDB;
using System.ComponentModel.DataAnnotations;

namespace LuizaLabs.Models
{
    public class OutlistItem
    {
        [BsonId]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        public DateTime DataVigenciaInicio { get; set; }

        public DateTime DataVigenciaTermino { get; set; }
    }
}
