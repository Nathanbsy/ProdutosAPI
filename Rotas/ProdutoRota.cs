using ProdutosAPI.Models;
using ProdutosAPI.Repositorio;

namespace ProdutosAPI.Rotas
{
    public static class ProdutoRota
    {
        public static void ProdutoRotas(this WebApplication app, IProdutoRepositorio produtoRepositorio)
        {
            var rota = app.MapGroup("produto");

            // Rota para cadastrar um novo produto
            rota.MapPost("", (ProdutoRequest req) =>
            {
                var produto = new Produto()
                {
                    Nome = req.nome,
                    Categoria = req.categoria,
                    Preco = req.preco,
                };
                produtoRepositorio.CadastrarProduto(produto);
                return Results.Created($"/produto/{produto.Id}", produto);
            });

            // Rota para obter todos os produtos
            rota.MapGet("", () =>
            {
                var produtos = produtoRepositorio.TodosProdutos();
                if (produtos == null) return Results.NotFound();
                return Results.Ok(produtos);
            });

            // Rota para obter produtos por categoria
            rota.MapGet("categoria/{categoria}", (string categoria) =>
            {
                var produtosCategoria = produtoRepositorio.ProdutoPorCategoria(categoria);
                if (produtosCategoria == null) return Results.NotFound();
                return Results.Ok(produtosCategoria);
            });

            // Rota para obter produtos por preço
            rota.MapGet("preco/{preco:decimal}", (decimal preco) =>
            {
                var produtosPreco = produtoRepositorio.ProdutoPorPreco(preco);
                if (produtosPreco == null) return Results.NotFound();
                return Results.Ok(produtosPreco);
            });

            // Rota para atualizar um produto
            rota.MapPatch("{id:int}", (int id, ProdutoRequest req) =>
            {
                var produto = produtoRepositorio.AcharProduto(id);
                if (produto == null) return Results.NotFound();

                produto.Nome = req.nome;
                produto.Categoria = req.categoria;
                produto.Preco = req.preco;

                produtoRepositorio.AtualizarProduto(produto);
                return Results.Ok(produto);
            });

            // Rota para excluir um produto
            rota.MapDelete("{id:int}", (int id) =>
            {
                var produto = produtoRepositorio.AcharProduto(id);
                if (produto == null) return Results.NotFound();
                produtoRepositorio.ExcluirProduto(id);
                return Results.Ok(produto);
            });
        }
    }
}
