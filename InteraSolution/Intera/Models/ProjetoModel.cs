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

        public void DelAluno(int idAluno, int idProjeto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "DELETE FROM AlunoData WHERE Aluno_id = @idAluno AND Projeto_id = @idProjeto";
            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);
            cmd.ExecuteNonQuery();
        }

        public List<AlunoProjeto> ReadAlunoProjeto(int idProjeto)
        {
            List<AlunoProjeto> lista = new List<AlunoProjeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT ad.Projeto_id Projeto, p.IdPessoa IdAluno, p.Nome Nome, p.Email Email, ad.DataInicio DataInicio, ad.DataFinal DataFinal FROM Pessoa as p FULL OUTER JOIN AlunoData as ad ON(p.IdPessoa = ad.Aluno_id) WHERE ad.Projeto_id = @idProjeto";
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AlunoProjeto aProjeto = new AlunoProjeto();
                aProjeto.IdPessoa = (int)reader["IdAluno"];
                aProjeto.Nome = (string)reader["Nome"];
                aProjeto.Email = (string)reader["Email"];
                aProjeto.DataEntrada = (DateTime)reader["DataInicio"];
                //aProjeto.DataSaida = (DateTime)reader["DataFinal"];
                lista.Add(aProjeto);
            }
            return lista;
        }
        public List<Projeto> ReadProjeto(int IdProjeto)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT  IdProjeto, Professor_id, Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where IdProjeto = @IdProjeto";
            
            cmd.Parameters.AddWithValue("@IdProjeto", IdProjeto);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto Projeto = new Projeto();
                Projeto.IdProjeto = (int)reader["IdProjeto"];
                Projeto.IdProfessor = (int)reader["Professor_id"];
                Projeto.IdCoorientador = (int)reader["Coorientador_id"];
                Projeto.IdTipo = (int)reader["TipoProjeto_id"];
                Projeto.NomeProjeto = (string)reader["NomeProjeto"];
                Projeto.Status = (int)reader["Status"];
                Projeto.Link = (string)reader["Link"];
                Projeto.DataInicio = (DateTime)reader["DataInicio"];
                Projeto.DataFinal = (DateTime)reader["DataFinal"];
                Projeto.Descricao = (string)reader["Descricao"];
                lista.Add(Projeto);
            }
            return lista;
        }
        public List<Projeto> ReadProjeto(String Professor, String NameProject, String Status)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            if (Professor == "" && Status == "")
            {
                cmd.CommandText = "SELECT  IdProjeto, Professor_id, Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where NomeProjeto like '%' + @NameProject + '%' order by DataInicio desc"; //@OrderBy //and Status = @Status 
            }
            else if (Professor == "")
            {
                cmd.CommandText = "SELECT  IdProjeto, Professor_id, Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where NomeProjeto like '%' + @NameProject + '%' and Status = @Status order by DataInicio desc"; //@OrderBy
            }
            else if (Status == "")
            {
                cmd.CommandText = "SELECT  IdProjeto, Professor_id, Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where Professor_id = @Professor and NomeProjeto like '%' + @NameProject + '%' order by DataInicio desc"; //@OrderBy
            }

            cmd.Parameters.AddWithValue("@Professor", Professor);
            cmd.Parameters.AddWithValue("@NameProject", NameProject);
            cmd.Parameters.AddWithValue("@Status", Status);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto Projeto = new Projeto();
                Projeto.IdProjeto = (int)reader["IdProjeto"];
                Projeto.IdProfessor = (int)reader["Professor_id"];
                Projeto.IdCoorientador = (int)reader["Coorientador_id"];
                Projeto.IdTipo = (int)reader["TipoProjeto_id"];
                Projeto.NomeProjeto = (string)reader["NomeProjeto"];
                Projeto.Status = (int)reader["Status"];
                Projeto.Link = (string)reader["Link"];
                Projeto.DataInicio = (DateTime)reader["DataInicio"];
                Projeto.DataFinal = (DateTime)reader["DataFinal"];
                Projeto.Descricao = (string)reader["Descricao"];
                lista.Add(Projeto);
            }
            return lista;
        }
        public List<Projeto> ReadProjetoProfessor(int idProfessor)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT  IdProjeto, Professor_id, Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto WHERE Professor_id = @idProfessor";
            cmd.Parameters.AddWithValue("@idProfessor", idProfessor);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto Projeto = new Projeto();
                Projeto.IdProjeto = (int)reader["IdProjeto"];
                Projeto.IdProfessor = (int)reader["Professor_id"];
                Projeto.IdCoorientador = (int)reader["Coorientador_id"];
                Projeto.IdTipo = (int)reader["TipoProjeto_id"];
                Projeto.NomeProjeto = (string)reader["NomeProjeto"];
                Projeto.Status = (int)reader["Status"];
                Projeto.Link = (string)reader["Link"];
                Projeto.DataInicio = (DateTime)reader["DataInicio"];
                Projeto.DataFinal = (DateTime)reader["DataFinal"];
                Projeto.Descricao = (string)reader["Descricao"];
                lista.Add(Projeto);
            }
            return lista;
        }
        public List<Projeto> ReadProjetoAluno(int idAluno)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT  IdProjeto, Professor_id, Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto WHERE IdProjeto IN (SELECT  Projeto_id FROM AlunoData where Aluno_id = @idAluno) ";
            cmd.Parameters.AddWithValue("@idAluno", idAluno);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto Projeto = new Projeto();
                Projeto.IdProjeto = (int)reader["IdProjeto"];
                Projeto.IdProfessor = (int)reader["Professor_id"];
                Projeto.IdCoorientador = (int)reader["Coorientador_id"];
                Projeto.IdTipo = (int)reader["TipoProjeto_id"];
                Projeto.NomeProjeto = (string)reader["NomeProjeto"];
                Projeto.Status = (int)reader["Status"];
                Projeto.Link = (string)reader["Link"];
                Projeto.DataInicio = (DateTime)reader["DataInicio"];
                Projeto.DataFinal = (DateTime)reader["DataFinal"];
                Projeto.Descricao = (string)reader["Descricao"];
                lista.Add(Projeto);
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