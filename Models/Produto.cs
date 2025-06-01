using LiteDB;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LuizaLabs.Models 
{
    public class Produto
    {
        [BsonId] // PK do LiteDB
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        public DateTime DataVigenciaInicio { get; set; } = DateTime.UtcNow; // Data de vigência padrão é a data atual em UTC

        public DateTime DataVigenciaTermino { get; set; } = DateTime.UtcNow; // Data de vigência padrão é a data atual em UTC


    }
}
