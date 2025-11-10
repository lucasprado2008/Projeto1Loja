using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Projeto1Loja.Models;
using System.Data;

namespace Projeto1Loja.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand ("INSERT INTO tbProduto (nome, descricao, preco, quantidade) VALUES (@nome, @descricao, @preco, @quantidade)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool AtualizarProduto(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update cliente set nome=@nome, descricao=@descricao, preco=@preco, quantidade=@quantidade " + " where idProduto=@idProduto", conexao);
                    cmd.Parameters.Add("@idProduto", MySqlDbType.Int32).Value = produto.idProduto;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                return false;

            }
        }

        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> Produtolista = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbProduto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    Produtolista.Add(
                        new Produto
                            {
                                idProduto = Convert.ToInt32(dr["idProduto"]),
                                nome = ((string)dr["nome"]),
                                descricao = ((string)dr["descricao"]),
                                preco = Convert.ToDecimal((string)dr["preco"]),
                                quantidade = Convert.ToInt32(dr["quantidade"])
                            });
                }
                return Produtolista;    
            }
        }

        public Produto ObterProduto(int idProduto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from tbProduto where idProduto=@idProduto ", conexao);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.idProduto = Convert.ToInt32(dr["idProduto"]);
                    produto.nome = (string)(dr["nome"]);
                    produto.descricao = (string)(dr["descricao"]);
                    produto.preco = Convert.ToDecimal(dr["preco"]);
                    produto.quantidade = Convert.ToInt32("quantidade");
                }
                return produto;
            }
        }

        public void Excluir(int idProduto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbProduto where idProduto=@idProduto", conexao);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}
