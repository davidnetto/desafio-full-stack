namespace CleanCar.Domain
{
    public class LocadoraDTO
    {
        public int Id { get; set; }
        public string? NomeFantasia { get; set; }
        public string? RazaoSocial { get; set; }
        public string? CNPJ { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Cep { get; set; }
        public string? Rua { get; set; }
        public int? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public int? EnderecoId { get; set; }
        public List<ModeloDTO>? ListaModeloDTO { get; set; }
    }
}
