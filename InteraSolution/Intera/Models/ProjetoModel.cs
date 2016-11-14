using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intera.Entity;
using System.Data.SqlClient;

namespace Intera.Models
{
    public class ProjetoModel : ModelBase
    {
        public int CreateProject(Projeto projeto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT into Projeto values (@idProfessor, @idCoorientador, @idTipo, @nome, 1, null , @dataInicio, null, @descricao) select SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@idProfessor", projeto.IdProfessor);
            cmd.Parameters.AddWithValue("@idCoorientador", projeto.IdCoorientador);
            cmd.Parameters.AddWithValue("@idTipo", projeto.IdTipo);
            cmd.Parameters.AddWithValue("@nome", projeto.NomeProjeto);
            cmd.Parameters.AddWithValue("@dataInicio", projeto.DataInicio);
            cmd.Parameters.AddWithValue("@descricao", projeto.Descricao);

            int idProjeto = Convert.ToInt32(cmd.ExecuteScalar());
            return idProjeto;
        }

        public void AddAluno(int idAluno, int idProjeto, DateTime data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO AlunoData VALUES (@idAluno, @idProjeto, @dataInicio, null)";
            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);
            cmd.Parameters.AddWithValue("@dataInicio", data);

            cmd.ExecuteNonQuery();
        }

        public void DelAluno(int idAluno)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "DELETE FROM AlunoData WHERE Aluno_id = @idAluno";
            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.ExecuteNonQuery();
        }

        public List<AlunoProjeto> ReadAlunoProjeto(int idProjeto)
        {
            List<AlunoProjeto> lista = new List<AlunoProjeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM AlunoData WHERE Projeto_id = @idProjeto";
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AlunoProjeto aProjeto = new AlunoProjeto();
                aProjeto.IdPessoa = (int)reader["Aluno_id"];
                aProjeto.DataEntrada = (DateTime)reader["DataInicio"];
                if (reader["DataFinal"] == null)
                {
                    aProjeto.DataSaida = (DateTime)reader["DataFinal"];
                }
                lista.Add(aProjeto);
            }
            return lista;
        }

        public List<Pessoa> ReadAluno()
        {
            List<Pessoa> lista = new List<Pessoa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Status = 1";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Pessoa pessoa = new Pessoa();
                pessoa.IdPessoa = (int)reader["IdPessoa"];
                pessoa.Nome = (string)reader["Nome"];
                pessoa.Email = (string)reader["Email"];

                lista.Add(pessoa);
            }
            return lista;
        }

        public Pessoa GetAluno(int id)
        {
            Pessoa p = null;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Pessoa WHERE IdPessoa = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                p.Nome = (string)reader["Nome"];
                p.Email = (string)reader["Email"];
            }
            return p;
        }
    }
}