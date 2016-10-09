using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Intera.Entity;

namespace Intera.Models
{
    public class PessoaModel : ModelBase
    {
        public Pessoa Login(string email, string senha)
        {
            Pessoa e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM Pessoa WHERE email = @email AND senha = @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                e = new Pessoa();
                e.IdPessoa = (int)reader["id"];
                e.Nome = (string)reader["nome"];
                e.Email = (string)reader["email"];
            }

            return e;
        }
    }
}