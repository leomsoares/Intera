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

            if (projeto.IdCoorientador == 0 && projeto.Link == null)
            {
                cmd.CommandText = "INSERT into Projeto values (@idProfessor, null, @idTipo, @nome, 1, null , @dataInicio, null, @descricao) select SCOPE_IDENTITY()";
            }
            else if (projeto.IdCoorientador == 0 && projeto.Link != null)
            {
                cmd.CommandText = "INSERT into Projeto values (@idProfessor, null, @idTipo, @nome, 1, @link , @dataInicio, null, @descricao) select SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@link", projeto.Link);
            }
            else if (projeto.IdCoorientador != 0 && projeto.Link == null)
            {
                cmd.CommandText = "INSERT into Projeto values (@idProfessor, @idCoorientador, @idTipo, @nome, 1, null , @dataInicio, null, @descricao) select SCOPE_IDENTITY()";
            }
            else if (projeto.IdCoorientador != 0 && projeto.Link != null)
            {
                cmd.CommandText = "INSERT into Projeto values (@idProfessor, @idCoorientador, @idTipo, @nome, 1, @link , @dataInicio, null, @descricao) select SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@link", projeto.Link);
            }
            cmd.Parameters.AddWithValue("@idProfessor", projeto.IdProfessor);
            cmd.Parameters.AddWithValue("@idCoorientador", projeto.IdCoorientador);
            cmd.Parameters.AddWithValue("@idTipo", projeto.IdTipo);
            cmd.Parameters.AddWithValue("@nome", projeto.NomeProjeto);

            cmd.Parameters.AddWithValue("@dataInicio", projeto.DataInicio);
            cmd.Parameters.AddWithValue("@descricao", projeto.Descricao);

            int idProjeto = Convert.ToInt32(cmd.ExecuteScalar());
            return idProjeto;
        }

        public void UpProjectDesc(string desc, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE Projeto SET Descricao = @desc WHERE IdProjeto = @id";
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void UpProjectLink(string link, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE Projeto SET Link = @link WHERE IdProjeto = @id";
            cmd.Parameters.AddWithValue("@link", link);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void EndProject(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE Projeto SET DataFinal = GETDATE(), Status = 2 WHERE IdProjeto = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public int VerificarAluno(int idAluno, int idProjeto)
        {
            int cont = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM AlunoData WHERE Aluno_id = @idAluno and Projeto_id = @idProjeto";
            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cont++;
            }
            return cont;
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
            cmd.CommandText = "SELECT ad.Projeto_id Projeto, p.IdPessoa IdAluno, p.Nome Nome, p.Email Email, ad.DataInicio DataInicio, ISNULL(ad.DataFinal, '') AS DataFinal FROM Pessoa as p FULL OUTER JOIN AlunoData as ad ON(p.IdPessoa = ad.Aluno_id) WHERE ad.Projeto_id = @idProjeto";
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AlunoProjeto aProjeto = new AlunoProjeto();
                aProjeto.IdPessoa = (int)reader["IdAluno"];
                aProjeto.Nome = (string)reader["Nome"];
                aProjeto.Email = (string)reader["Email"];
                aProjeto.DataEntrada = (DateTime)reader["DataInicio"];
                aProjeto.DataSaida = (DateTime)reader["DataFinal"];
                lista.Add(aProjeto);
            }
            reader.Close();
            return lista;
        }
        public List<AlunoProjeto> ReadAlunoData()
        {
            List<AlunoProjeto> lista = new List<AlunoProjeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "select A.Aluno_id, P.Nome ,A.Projeto_id, A.DataInicio, ISNULL(DataFinal,'') DataFinal from AlunoData as A inner join Pessoa as P on (A.Aluno_id = P.IdPessoa)";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                AlunoProjeto Aluno = new AlunoProjeto();
                Aluno.IdPessoa = (int)reader["Aluno_id"];
                Aluno.Nome = (string)reader["Nome"];
                Aluno.IdProjeto = (int)reader["Projeto_id"];
                Aluno.DataEntrada = (DateTime)reader["DataInicio"];
                Aluno.DataSaida = (DateTime)reader["DataFinal"];
                lista.Add(Aluno);
            }
            reader.Close();
            return lista;
        }
        public int Count(int tipo)
        {
            SqlCommand cmd = new SqlCommand();
            int qtd;
            cmd.Connection = connection;
            if (tipo == 1)
            {
                cmd.CommandText = "select count(*) as quantidade from Pessoa where Status = 1";
            }
            else if (tipo == 2)
            {
                cmd.CommandText = "select count(*) as quantidade from Pessoa where Status = 2";
            }
            else if (tipo == 3)
            {
                cmd.CommandText = "select count(*) as quantidade from Pessoa where Status = 3";
            }
            else if (tipo == 4)
            {
                cmd.CommandText = "select count(*) as quantidade from projeto";
            }

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            qtd = (int)reader["quantidade"];


            reader.Close();
            return qtd;
        }
        public List<Pessoa> ReadProfessorProjeto(int id)
        {
            List<Pessoa> lista = new List<Pessoa>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa  WHERE IdPessoa = (Select Professor_id from Projeto where IdProjeto = @id)";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Pessoa Professor = new Pessoa();
                Professor.Nome = (string)reader["Nome"];
                lista.Add(Professor);
            }

            reader.Close();
            return lista;
        }
        public List<Projeto> ReadScientificResearch()
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT IdProjeto, Professor_id, ISNULL(Coorientador_id,0) Coorientador_id, NomeProjeto, P.Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao , Nome FROM Projeto as P inner join Pessoa as Pes on (Professor_id = IdPessoa) where TipoProjeto_id = 2 order by DataInicio desc";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto Projeto = new Projeto();
                Projeto.IdProjeto = (int)reader["IdProjeto"];
                Projeto.IdProfessor = (int)reader["Professor_id"];
                Projeto.IdCoorientador = (int)reader["Coorientador_id"];
                Projeto.NomeProjeto = (string)reader["NomeProjeto"];
                Projeto.Status = (int)reader["Status"];
                Projeto.Link = (string)reader["Nome"];
                Projeto.DataInicio = (DateTime)reader["DataInicio"];
                Projeto.DataInicio.ToUniversalTime();
                Projeto.DataFinal = (DateTime)reader["DataFinal"];
                Projeto.Descricao = (string)reader["Descricao"];
                lista.Add(Projeto);
            }
            reader.Close();
            return lista;
        }
        public List<Projeto> ReadProjeto()
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT IdProjeto, Professor_id, ISNULL(Coorientador_id,0) Coorientador_id, NomeProjeto, P.Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao , Nome FROM Projeto as P inner join Pessoa as Pes on (Professor_id = IdPessoa) order by DataInicio desc";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto Projeto = new Projeto();
                Projeto.IdProjeto = (int)reader["IdProjeto"];
                Projeto.IdProfessor = (int)reader["Professor_id"];
                Projeto.IdCoorientador = (int)reader["Coorientador_id"];
                Projeto.NomeProjeto = (string)reader["NomeProjeto"];
                Projeto.Status = (int)reader["Status"];
                Projeto.Link = (string)reader["Nome"];
                Projeto.DataInicio = (DateTime)reader["DataInicio"];
                Projeto.DataFinal = (DateTime)reader["DataFinal"];
                Projeto.Descricao = (string)reader["Descricao"];
                lista.Add(Projeto);
            }
            reader.Close();
            return lista;
        }
        public List<Projeto> ReadProjetoLastAdd()
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT TOP 3 IdProjeto, Professor_id, ISNULL(Coorientador_id,0) Coorientador_id, NomeProjeto, P.Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao , Nome FROM Projeto as P inner join Pessoa as Pes on (Professor_id = IdPessoa) order by IdProjeto desc";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto Projeto = new Projeto();
                Projeto.IdProjeto = (int)reader["IdProjeto"];
                Projeto.IdProfessor = (int)reader["Professor_id"];
                Projeto.IdCoorientador = (int)reader["Coorientador_id"];
                Projeto.NomeProjeto = (string)reader["NomeProjeto"];
                Projeto.Status = (int)reader["Status"];
                Projeto.Link = (string)reader["Nome"];
                Projeto.DataInicio = (DateTime)reader["DataInicio"];
                Projeto.DataFinal = (DateTime)reader["DataFinal"];
                Projeto.Descricao = (string)reader["Descricao"];
                lista.Add(Projeto);
            }
            reader.Close();
            return lista;
        }
        public List<Projeto> ReadProjeto(int IdProjeto)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT  IdProjeto, Professor_id, ISNULL(Coorientador_id,0) Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where IdProjeto = @IdProjeto";

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
            reader.Close();
            return lista;
        }
        public Projeto ReadProjeto2(int IdProjeto)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT  IdProjeto, Professor_id, ISNULL(Coorientador_id,0) Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where IdProjeto = @IdProjeto";

            cmd.Parameters.AddWithValue("@IdProjeto", IdProjeto);

            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

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

            reader.Close();
            return Projeto;
        }
        public List<Projeto> ReadProjeto(String Professor, String NameProject, String Status)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            if (Professor == "" && Status == "")
            {
                cmd.CommandText = "SELECT  IdProjeto, Professor_id, ISNULL(Coorientador_id, '') AS Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where NomeProjeto like '%' + @NameProject + '%' order by DataInicio desc"; //@OrderBy //and Status = @Status 
            }
            else if (Professor == "")
            {
                cmd.CommandText = "SELECT  IdProjeto, Professor_id, ISNULL(Coorientador_id, '') AS Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where NomeProjeto like '%' + @NameProject + '%' and Status = @Status order by DataInicio desc"; //@OrderBy
            }
            else if (Status == "")
            {
                cmd.CommandText = "SELECT  IdProjeto, Professor_id, ISNULL(Coorientador_id, '') AS Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto where Professor_id = @Professor and NomeProjeto like '%' + @NameProject + '%' order by DataInicio desc"; //@OrderBy
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
            cmd.CommandText = "SELECT  IdProjeto, Professor_id, ISNULL(Coorientador_id, '0') Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto WHERE Professor_id = @idProfessor or Coorientador_id = @idProfessor";
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
            reader.Close();
            return lista;
        }
        public List<Projeto> ReadProjetoAluno(int idAluno)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT  IdProjeto, Professor_id, ISNULL(Coorientador_id, '0') Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao FROM Projeto WHERE IdProjeto IN (SELECT  Projeto_id FROM AlunoData where Aluno_id = @idAluno) ";
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
            reader.Close();
            return lista;
        }

        public List<Pessoa> SearchAluno(string busca)
        {
            List<Pessoa> lista = new List<Pessoa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Status = 1 AND NOME LIKE '%' + @nome + '%'";
            cmd.Parameters.AddWithValue("@nome", busca);

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

        public List<Professor> ReadProfessor()
        {
            List<Professor> lista = new List<Professor>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT p.IdPessoa Id, p.Nome Nome, prof.Rs Rs FROM Pessoa AS p FULL OUTER JOIN Professor AS prof ON (p.IdPessoa = prof.Pessoa_id) WHERE p.Status = 2";

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Professor prof = new Professor();
                prof.IdPessoa = (int)reader["Id"];
                prof.Nome = (string)reader["Nome"];
                prof.Rs = (string)reader["Rs"];

                lista.Add(prof);
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

        public bool UserProjeto(int idProjeto, int idAluno)
        {
            bool retorno = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT COUNT (Aluno_id) FROM AlunoData WHERE Aluno_id = @idAluno AND Projeto_id = @idProjeto";
            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            if (i != 0)
            {
                retorno = true;
            }
            return retorno;
        }

        public bool ProfProjeto(int idProjeto, int idProf)
        {
            bool retorno = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT COUNT (Professor_id) FROM Projeto WHERE Professor_id = @idProf AND IdProjeto = @idProjeto";
            cmd.Parameters.AddWithValue("@idProf", idProf);
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            if (i != 0)
            {
                retorno = true;
            }
            return retorno;
        }

        public void CreateMensagem(Mensagem msg, int idProjeto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Mensagem VALUES (@IdPessoa, @IdProjeto, @Mensagem)";
            cmd.Parameters.AddWithValue("@IdPessoa", msg.IdPessoa);
            cmd.Parameters.AddWithValue("@IdProjeto", idProjeto);
            cmd.Parameters.AddWithValue("@Mensagem", msg.DescricaoMsg);
            cmd.ExecuteNonQuery();
        }

        public List<Mensagem> ReadMensagem(int id)
        {
            List<Mensagem> lista = new List<Mensagem>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT m.IdMensagem IdMsg, m.Descricao Msg, p.IdPessoa IdPes, p.Nome NomePes FROM Mensagem AS m FULL OUTER JOIN Pessoa AS p ON (m.Pessoa_id = p.IdPessoa) WHERE m.Projeto_id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Mensagem msg = new Mensagem();
                msg.IdMensagem = (int)reader["IdMsg"];
                msg.DescricaoMsg = (string)reader["Msg"];
                msg.IdPessoa = (int)reader["IdPes"];
                msg.Nome = (string)reader["NomePes"];
                lista.Add(msg);
            }
            return lista;
        }

        public Projeto ReadEditProjeto(int id)
        {
            Projeto projeto = new Projeto();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT IdProjeto, Professor_id, ISNULL(Coorientador_id, '0') AS Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link, '') AS Link, DataInicio, ISNULL(DataFinal, '') AS DataFinal, Descricao FROM Projeto WHERE IdProjeto = @idProjeto";
            cmd.Parameters.AddWithValue("@idProjeto", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                projeto.IdProjeto = (int)reader["IdProjeto"];
                projeto.IdProfessor = (int)reader["Professor_id"];
                projeto.IdCoorientador = (int)reader["Coorientador_id"];
                projeto.IdTipo = (int)reader["TipoProjeto_id"];
                projeto.NomeProjeto = (string)reader["NomeProjeto"];
                projeto.Status = (int)reader["Status"];
                projeto.Link = (string)reader["Link"];
                projeto.DataInicio = (DateTime)reader["DataInicio"];
                projeto.DataFinal = (DateTime)reader["DataFinal"];
                projeto.Descricao = (string)reader["Descricao"];
            }
            reader.Close();
            return projeto;
        }
        //ReadAlunoProjeto

        public List<Referencia> ReadReferencia(int id)
        {
            List<Referencia> lista = new List<Referencia>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select r.IdReferencia, r.Descricao from Referencia r, Projeto p where r.Projeto_id = @id and p.IdProjeto = @idprojeto";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@idprojeto", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Referencia referencia = new Referencia();
                referencia.IdReferencia = (int)reader["IdReferencia"];
                referencia.DescricaoReferencia = (string)reader["Descricao"];
                lista.Add(referencia);
            }
            reader.Close();
            return lista;
        }

        public void AddReferencia(Referencia referencia)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO Referencia VALUES (@Projeto_id, @Descricao)";
            cmd.Parameters.AddWithValue("@Projeto_id", referencia.IdProjeto);
            cmd.Parameters.AddWithValue("@Descricao", referencia.DescricaoReferencia);
            cmd.ExecuteNonQuery();
        }

        public void DelReferencia(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "DELETE FROM Referencia WHERE IdReferencia = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void AlterarAlunoProjeto(int idAluno, int idProjeto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE AlunoData SET DataFinal = GETDATE() WHERE Aluno_id = @idAluno AND Projeto_id = @idProjeto";
            cmd.Parameters.AddWithValue("@idAluno", idAluno);
            cmd.Parameters.AddWithValue("@idProjeto", idProjeto);
            cmd.ExecuteNonQuery();
        }
    }
}