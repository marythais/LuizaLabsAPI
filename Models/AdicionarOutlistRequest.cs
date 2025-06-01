namespace LuizaLabs.Models
{
    public class AdicionarOutlistRequest
    {
        public int ProdutoId { get; set; }

        public DateTime DataVigenciaInicio { get; set; }

        public DateTime DataVigenciaTermino { get; set; }
    }
}
