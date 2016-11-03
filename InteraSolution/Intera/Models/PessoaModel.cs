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
        public List<Pessoa> PessoaRead()
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

        public List<Aluno> AlunoRead()
        {
            List<Aluno> lista = new List<Aluno>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select	  p.IdPessoa	Id , p.Nome Nome, p.Email Email, a.Ra Ra, p.Status Status from Pessoa p, Aluno a where p.IdPessoa = a.Pessoa_id";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Aluno a = new Aluno();
                
                a.IdPessoa = (int)reader["Id"];
                a.Nome = (string)reader["Nome"];
                a.Email = (string)reader["Email"];
                a.Ra = (string)reader["Ra"];
                a.Status = (int)reader["Status"];

                lista.Add(a);
            }
            
            return lista;
        }

        public List<Professor> ProfessorRead()
        {
            List<Professor> lista = new List<Professor>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select	  p.IdPessoa	Id, p.Nome Nome, p.Email Email, prof.Rs Rs, p.Status Status from Pessoa p, Professor prof where p.IdPessoa = prof.Pessoa_id ";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Professor prof = new Professor();

                prof.IdPessoa = (int)reader["Id"];
                prof.Nome = (string)reader["Nome"];
                prof.Email = (string)reader["Email"];
                prof.Rs = (string)reader["Rs"];
                prof.Status = (int)reader["Status"];

                lista.Add(prof);
            }
            return lista;
        }
    }
}