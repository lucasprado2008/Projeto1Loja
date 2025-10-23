using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using MySql.Data.MySqlClient;
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
                var cmd = conexao.CreateCommand();
                cmd.CommandText = "INSERT INTO tbProduto (nome, descricao, preco, quantidade) VALUES (@nome, @descricao, @preco, @quantidade)";
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.preco;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Produto ProcurarProduto(string nome)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM tbProduto WHERE nome = @nome", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Produto produto = null;
                    if (dr.Read())
                    {
                        produto = new Produto()
                        {
                            nome = dr["nome"].ToString(),
                            descricao = dr["descricao"].ToString(),
                            preco = Convert.ToDecimal(dr["preco"]),
                            quantidade = Convert.ToInt32(dr["quantidade"])
                        };
                    }
                    return produto;
                }
            }
        }
    }
}
