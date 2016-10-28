using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Intera.Entity;

namespace Intera.Models
{
    public class PessoaModel : Modelbase
    {
        public List<Pessoa> Read()
        {
            List<Pessoa> lista = new List<Pessoa>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Pessoa p = new Pessoa();

                p.IdPessoa = (int)reader["IdPessoa"];
                p.Nome = (string)reader["Nome"];
                p.Status = (int)reader["Status"];
                p.Email = (string)reader["Email"];
                p.Senha = (string)reader["Senha"];

                lista.Add(p);
            }
            return lista;
        }

        public Pessoa Login(string email, string senha)
        {
            Pessoa p = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Email = @email AND Senha = @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                p = new Pessoa();
                p.IdPessoa = (int)reader["IdPessoa"];
                p.Nome = (string)reader["Nome"];
                p.Email = (string)reader["Email"];
            }
            return p;
        }

        public void CreateAluno(Pessoa p, Aluno a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Pessoa VALUES (@nome, 1, @email, @senha) 
                                INSERT INTO Aluno VALUE (@@IDENTITY, @ra)";

            cmd.Parameters.AddWithValue("@nome", p.Nome);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@senha", p.Senha);
            cmd.Parameters.AddWithValue("@ra", a.Ra);

            cmd.ExecuteNonQuery();
        }
    }
}