using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Intera.Entity;
using System.Web.Mail;

namespace Intera.Models
{
    public class PessoaModel : ModelBase
    {
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
                p.Senha = (string)reader["Senha"];
                p.Status = (int)reader["Status"];
            }
            return p;
        }

        public Pessoa ResgatarSenha(string email)
        {
            Pessoa p = new Pessoa();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Nome, Senha FROM Pessoa WHERE Email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                p.Nome = (string)reader["Nome"];
                p.Senha = (string)reader["Senha"];
            }

            return p;
        }

        public List<Pessoa> Read()
        {
            List<Pessoa> lista = new List<Pessoa>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Status != 0 AND Status != 3";

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

        public List<Pessoa> Search(string nome)
        {
            List<Pessoa> lista = new List<Pessoa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Status != 0 AND Status != 3 AND Nome LIKE '%' + @nome + '%'";
            cmd.Parameters.AddWithValue("@nome", nome);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Pessoa p = new Pessoa();
                p.IdPessoa = (int)reader["IdPessoa"];
                p.Nome = (string)reader["Nome"];
                p.Email = (string)reader["Email"];
                p.Status = (int)reader["Status"];
                lista.Add(p);
            }
            return lista;
        }
        public List<Pessoa> ReadProfessor()
        {
            List<Pessoa> lista = new List<Pessoa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select pe.*, p.Rs from Pessoa pe inner join Professor p on pe.IdPessoa = p.Pessoa_id where pe.Status = 2";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Professor p = new Professor();
                p.IdPessoa = (int)reader["IdPessoa"];
                p.Nome = (string)reader["Nome"];
                p.Email = (string)reader["Email"];
                p.Status = (int)reader["Status"];
                p.Rs = (string)reader["Rs"];

                lista.Add(p);
            }
            reader.Close();
            return lista;
        }
        public Pessoa ReadProfessor(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select pe.*, p.Rs from Pessoa pe inner join Professor p on pe.IdPessoa = p.Pessoa_id where pe.Status = 2 and IdPessoa = @IdPessoa";
            cmd.Parameters.AddWithValue("@IdPessoa", id);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            Professor p = new Professor();
            p.IdPessoa = (int)reader["IdPessoa"];
            p.Nome = (string)reader["Nome"];
            p.Email = (string)reader["Email"];
            p.Status = (int)reader["Status"];
            p.Rs = (string)reader["Rs"];
            reader.Close();
            return p;
        }

        public Pessoa ReadAluno(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select pe.*, a.Ra, a.Curso from Pessoa pe inner join Aluno a on pe.IdPessoa = a.Pessoa_id where pe.Status = 1 and IdPessoa = @IdPessoa";
            cmd.Parameters.AddWithValue("@IdPessoa", id);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            Aluno p = new Aluno();
            p.IdPessoa = (int)reader["IdPessoa"];
            p.Nome = (string)reader["Nome"];
            p.Email = (string)reader["Email"];
            p.Status = (int)reader["Status"];
            p.Ra = (string)reader["Ra"];
            p.Curso = (string)reader["Curso"];
            reader.Close();
            return p;
        }
        public int CreateAluno(Aluno aluno)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"AddAluno";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", aluno.Nome);
            cmd.Parameters.AddWithValue("@email", aluno.Email);
            cmd.Parameters.AddWithValue("@ra", aluno.Ra);
            cmd.Parameters.AddWithValue("@curso", aluno.Curso);

            int idPessoa = Convert.ToInt32(cmd.ExecuteScalar());
            return idPessoa;
        }

        public int CreateProfessor(Professor professor)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"AddProfessor";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@nome", professor.Nome);
            cmd.Parameters.AddWithValue("@email", professor.Email);
            cmd.Parameters.AddWithValue("@rs", professor.Rs);

            int idPessoa = Convert.ToInt32(cmd.ExecuteScalar());
            return idPessoa;
        }

        public void CreateSocial(int id)
        {
            for (int i = 1; i < 5; i++)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "INSERT INTO Social VALUES (@id, '#', @idsocial)";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@idsocial", i);
                cmd.ExecuteNonQuery();
            }
        }

        public int UpdateReadPessoa(int IdPessoa)
        {
            Pessoa p = new Pessoa();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Status FROM Pessoa WHERE IdPessoa = @idPessoa";
            cmd.Parameters.AddWithValue("@idPessoa", IdPessoa);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                p.Status = (int)reader["Status"];
            }

            return p.Status;
        }

        public Aluno UpdateReadAluno(int IdPessoa)
        {
            Aluno a = new Aluno();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT p.IdPessoa Id, p.Nome Nome, p.Status Status, p.Email Email, p.Senha Senha, a.Ra Ra, a.Curso Curso FROM Pessoa p, Aluno a WHERE IdPessoa = @idPessoa";
            cmd.Parameters.AddWithValue("@idPessoa", IdPessoa);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                a.IdPessoa = (int)reader["Id"];
                a.Nome = (string)reader["Nome"];
                a.Status = (int)reader["Status"];
                a.Email = (string)reader["Email"];
                a.Senha = (string)reader["Senha"];
                a.Ra = (string)reader["Ra"];
                a.Curso = (string)reader["Curso"];
            }
            return a;
        }

        public Professor UpdateReadProfessor(int IdPessoa)
        {
            Professor prof = new Professor();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT p.IdPessoa Id, p.Nome Nome, p.Status Status, p.Email Email, p.Senha Senha, pr.Rs Rs FROM Pessoa p, Professor pr WHERE IdPessoa = @idPessoa";
            cmd.Parameters.AddWithValue("@idPessoa", IdPessoa);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                prof.IdPessoa = (int)reader["Id"];
                prof.Nome = (string)reader["Nome"];
                prof.Status = (int)reader["Status"];
                prof.Email = (string)reader["Email"];
                prof.Senha = (string)reader["Senha"];
                prof.Rs = (string)reader["Rs"];
            }
            reader.Close();
            return prof;
        }

        public bool VerificarSenha(string senha, int id)
        {
            bool retorno = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Pessoa WHERE Senha = @senha and IdPessoa = @id";
            cmd.Parameters.AddWithValue("@senha", senha);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                retorno = true;
            }
            reader.Close();
            return retorno;
        }

        public void TrocarSenha(string senha, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE Pessoa SET Senha = @senha WHERE IdPessoa = @id";
            cmd.Parameters.AddWithValue("@senha", senha);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void UpdateAluno(Aluno a)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UpAluno";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPessoa", a.IdPessoa);
            cmd.Parameters.AddWithValue("@nome", a.Nome);
            cmd.Parameters.AddWithValue("@email", a.Email);
            cmd.Parameters.AddWithValue("@ra", a.Ra);
            cmd.Parameters.AddWithValue("@Curso", a.Curso);
            cmd.ExecuteNonQuery();
        }

        public void UpdateProfessor(Professor p)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UpProf";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPessoa", p.IdPessoa);
            cmd.Parameters.AddWithValue("@nome", p.Nome);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@rs", p.Rs);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int IdPessoa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "DelPessoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idPessoa", IdPessoa);

            cmd.ExecuteNonQuery();
        }

        public List<Social> ReadSocial(int id)
        {
            List<Social> lista = new List<Social>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select p.IdPessoa Id, p.Nome Nome, p.Email Email, p.Senha Senha, s.Nick Nick, s.Social_id Social from Pessoa as p full outer join Social as s on (p.IdPessoa = s.Pessoa_id) where IdPessoa = @id";
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Social social = new Social();
                social.IdPessoa = id;
                social.Nome = (string)reader["Nome"];
                social.Senha = (string)reader["Senha"];
                social.Nick = (string)reader["Nick"];
                social.IdSocial = (int)reader["Social"];
                lista.Add(social);
            }
            return lista;
        }

        public List<Social> ReadSocialAluno(int id)
        {
            List<Social> lista = new List<Social>();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "SELECT * FROM Social WHERE Pessoa_id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Social social = new Social();
                social.Nick = (string)reader["Nick"];
                social.IdSocial = (int)reader["Social_id"];
                lista.Add(social);
            }
            reader.Close();
            return lista;
        }

        public void UpdateProfile(Pessoa pessoa, List<Social> lista)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "UPDATE Pessoa SET Email = @email, Senha = @senha WHERE IdPessoa = @idPessoa";
            cmd.Parameters.AddWithValue("@email", pessoa.Email);
            cmd.Parameters.AddWithValue("@senha", pessoa.Senha);
            cmd.Parameters.AddWithValue("@idPessoa", pessoa.IdPessoa);
            cmd.ExecuteNonQuery();


            foreach (Social social in lista)
            {
                SqlCommand cmdSocial = new SqlCommand();
                cmdSocial.Connection = connection;
                cmdSocial.CommandText = "UPDATE Social SET Nick = @nick WHERE Pessoa_id = @pessoaId AND Social_id = @socialId";
                cmdSocial.Parameters.AddWithValue("@nick", social.Nick);
                cmdSocial.Parameters.AddWithValue("@pessoaId", pessoa.IdPessoa);
                cmdSocial.Parameters.AddWithValue("@socialId", social.IdSocial);
                cmdSocial.ExecuteNonQuery();
            }
        }

        public List<Projeto> ReadProjetoProfileProf(int id)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select IdProjeto, Professor_id, ISNULL(Coorientador_id, '0') as Coorientador_id, TipoProjeto_id, NomeProjeto, Status, ISNULL(Link, '') as Link, DataInicio, ISNULL(DataFinal, '') as DataFinal, Descricao from Projeto where Professor_id = @id or Coorientador_id = @id2";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@id2", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto projeto = new Projeto();
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
                lista.Add(projeto);
            }
            reader.Close();
            return lista;
        }

        public List<Projeto> ReadProjetoProfileAluno(int id)
        {
            List<Projeto> lista = new List<Projeto>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "select p.IdProjeto, p.Professor_id, ISNULL(p.Coorientador_id, '0') as Coorientador_id, p.TipoProjeto_id, p.NomeProjeto, p.Status, ISNULL(p.Link, '') as Link, p.DataInicio, ISNULL(p.DataFinal, '') as DataFinal, p.Descricao from Projeto as p full outer join AlunoData as a on(p.IdProjeto = a.Projeto_id) where a.Aluno_id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Projeto projeto = new Projeto();
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
                lista.Add(projeto);
            }
            reader.Close();
            return lista;
        }
    }
}