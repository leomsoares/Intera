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
        public void CreateProject(Projeto projeto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Projeto VALUES (@idProfessor, @idCoorientador, @idTipo, @nome, 1, null , @dataInicio, null, @descricao";
            cmd.Parameters.AddWithValue("@idProfessor", projeto.IdProfessor);
            cmd.Parameters.AddWithValue("@idCoorientador", projeto.IdCoorientador);
            cmd.Parameters.AddWithValue("@idTipo", projeto.IdTipo);
            cmd.Parameters.AddWithValue("@nome", projeto.NomeProjeto);
            cmd.Parameters.AddWithValue("@dataInicio", projeto.DataInicio);
            cmd.Parameters.AddWithValue("@descricao", projeto.Descricao);

            cmd.ExecuteNonQuery();
        }

        public void AddAluno(int idAluno, int idProjeto, DateTime data)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "ISNERT INTO AlunoData VALUES (@idAluno, @idProjeto, @dataInicio, null)";
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

        public List<Pessoa> ReadAluno()
        {
            List<Pessoa> lista = new List<Pessoa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Status = 1";

            SqlDataReader reader = cmd.ExecuteReader();
        }
    }
}