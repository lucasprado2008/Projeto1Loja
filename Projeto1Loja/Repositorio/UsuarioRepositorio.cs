using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using MySql.Data.MySqlClient;
using Projeto1Loja.Models;
using System.Data;
namespace Projeto1Loja.Repositorio
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void CadastrarUsuario(Usuario usuario)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                var cmd = conexao.CreateCommand();
                cmd.CommandText = "INSERT INTO tbUsuario (nome, email, senha) VALUES (@nome, @email, @senha)";
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = usuario.nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.email;
                cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = usuario.senha;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Usuario ProcurarUsuario(string email)
        {   
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM tbUsuario WHERE email = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {
                        usuario = new Usuario()
                        {
                            id = Convert.ToInt32(dr["id"]),
                            email = dr["email"].ToString(),
                            senha = dr["senha"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }
    }
}
