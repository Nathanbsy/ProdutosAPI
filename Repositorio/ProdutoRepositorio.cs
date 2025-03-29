using MySql.Data.MySqlClient;
using ProdutosAPI.Models;
using System.Data;

namespace ProdutosAPI.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly string? _conexaoMySQL;

        public ProdutoRepositorio(IConfiguration conf) => _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO tbProdutos(Nome, Categoria, Preco) values (@nome, @categoria, @preco)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@categoria", MySqlDbType.VarChar).Value = produto.Categoria;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;

                cmd.ExecuteReader();
                conexao.Close();
            }
        }
        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> lista = new List<Produto>();
            using(var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbProdutos", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();
                foreach(DataRow dr in dt.Rows)
                {
                    lista.Add(
                        new Produto()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = Convert.ToString(dr["Nome"]),
                            Categoria = Convert.ToString(dr["Categoria"]),
                            Preco = Convert.ToDecimal(dr["Preco"]),
                        });
                }
                return lista;
            }
        }
        public void AtualizarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE tbProdutos set Nome = @nome, Categoria = @categoria, Preco = @preco WHERE Id = @id", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@categoria", MySqlDbType.VarChar).Value = produto.Categoria;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = produto.Id;

                cmd.ExecuteReader();
                conexao.Close();
            }
        }
        public void ExcluirProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM tbProdutos WHERE Id = @id", conexao);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                cmd.ExecuteReader();
                conexao.Close();
            }
        }
        public IEnumerable<Produto> ProdutoPorCategoria(string categoria)
        {
            List<Produto> lista = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbProdutos WHERE Categoria = @categoria", conexao);
                cmd.Parameters.Add("@categoria", MySqlDbType.VarChar).Value = categoria;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    lista.Add(
                        new Produto()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = Convert.ToString(dr["Nome"]),
                            Categoria = Convert.ToString(dr["Categoria"]),
                            Preco = Convert.ToDecimal(dr["Preco"]),
                        });
                }
                return lista;
            }
        }
        public IEnumerable<Produto> ProdutoPorPreco(decimal preco)
        {
            List<Produto> lista = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbProdutos WHERE Preco = @preco", conexao);
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = preco;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    lista.Add(
                        new Produto()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = Convert.ToString(dr["Nome"]),
                            Categoria = Convert.ToString(dr["Categoria"]),
                            Preco = Convert.ToDecimal(dr["Preco"]),
                        });
                }
                return lista;
            }
            
        }
        public Produto AcharProduto(int id)
        {
            var produto = new Produto();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbProdutos WHERE Id = @id", conexao);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    produto = new Produto()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nome = Convert.ToString(dr["Nome"]),
                        Categoria = Convert.ToString(dr["Categoria"]),
                        Preco = Convert.ToDecimal(dr["Preco"]),
                    };
                }

                return produto;
            }
        }
    }
}
