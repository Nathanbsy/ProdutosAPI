﻿namespace ProdutosAPI.Models
{
    public class Produto
    {

        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Categoria { get; set; }
        public decimal Preco { get; set; }
    }
}
