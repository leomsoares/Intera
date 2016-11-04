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
        public List<Pessoa> Read()
        {
            List<Pessoa> lista = new List<Pessoa>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Status = 0 OR Status = 1 OR Status = 2";

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

        public List<Pessoa> Read(string busca)
        {
            List<Pessoa> lista = new List<Pessoa>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa LIKE @nome";

            cmd.Parameters.AddWithValue("@nome", busca);

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
            cmd.CommandText = "Login";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

        public void CreateAluno(Aluno aluno)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"AddAluno";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", aluno.Nome);
            cmd.Parameters.AddWithValue("@email", aluno.Email);
            cmd.Parameters.AddWithValue("@senha", aluno.Senha);
            cmd.Parameters.AddWithValue("@ra", aluno.Ra);
            cmd.Parameters.AddWithValue("@curso", aluno.Curso);

            cmd.ExecuteNonQuery();
        }

        public void CreateProfessor(Professor professor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"AddProfessor";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", professor.Nome);
            cmd.Parameters.AddWithValue("@email", professor.Email);
            cmd.Parameters.AddWithValue("@senha", professor.Senha);
            cmd.Parameters.AddWithValue("@rs", professor.Rs);

            cmd.ExecuteNonQuery();
        }
        
        public  void Update(Pessoa p)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE Pessoa SET Nome= @nome, Email = @email, Senha = @senha WHERE IdPessoa = @idPessoa";

            cmd.Parameters.AddWithValue("@nome", p.Nome);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@senha", p.Senha);
            cmd.Parameters.AddWithValue("@idPessoa", p.IdPessoa);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int IdPessoa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "DELETE FROM Pessoa WHERE IdPessoa = @idPessoa";

            cmd.Parameters.AddWithValue("@idPessoa", IdPessoa);

            cmd.ExecuteNonQuery();
        }
    }
}