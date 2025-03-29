using ProdutosAPI.Models;

namespace ProdutosAPI.Repositorio
{
    public interface IProdutoRepositorio
    {
        public void CadastrarProduto(Produto produto);
        public IEnumerable<Produto> TodosProdutos();
        public void AtualizarProduto(Produto produto);
        public void ExcluirProduto(int id);
        public IEnumerable<Produto> ProdutoPorCategoria(string categoria);
        public IEnumerable<Produto> ProdutoPorPreco(decimal preco);
        public Produto AcharProduto(int id);
    }
}
