create DATABASE BDIntera
go 

use BDIntera
go

create table Pessoa
(
	  IdPessoa			int			 not null primary key identity 
	, Nome				varchar(50)	 not null
	, Status			int			 check(status in (0, 1, 2, 3))
	, Email				varchar(50)	 not null unique
	, Senha				varchar(20)	 not null
)

create table TipoRedeSocial
(
	 IdRedeSocial	 int		  not null primary key
	, NomeRedeSocial varchar(100) not null
)

create table Social 
(
	  Pessoa_id	 int		   not null	
	, Nick		 varchar(50)   not null
	, Social_id	 int		   not null

	, foreign key (Pessoa_id) references Pessoa
	, foreign key (Social_id) references TipoRedeSocial
)



create table Aluno 
(
	  Pessoa_id  int			not null	primary key
	, Ra		 varchar(15)	not null
	, Curso		 varchar(50)

	, foreign key (Pessoa_id) references Pessoa
)

create table Professor 
(
	  Pessoa_id   int			not null  primary key 
	, Rs	      varchar(15)	not null
	, foreign key(Pessoa_id) references Pessoa
)

create table TipoProjeto
(
	  IdTipo	int			not null	primary key
	, Descricao varchar(30) not null
)

create table Projeto
(
	  IdProjeto			int			not null	primary key identity
	, Professor_id		int			not null
	, Coorientador_id	int 
	, TipoProjeto_id	int			not null
	, NomeProjeto		varchar(50) not null
	, Status			int			check(status in (1, 2))
	, Link				varchar(max)
	, DataInicio		date		not null
	, DataFinal			date 
	, Descricao			varchar(max)

	, foreign key (Professor_id) references Professor
	, foreign key (Coorientador_id) references Professor
	, foreign key (TipoProjeto_id) references TipoProjeto
)

create table AlunoData
(
	  Aluno_id		int  not null
	, Projeto_id	int  not null
	, DataInicio	date not null
	, DataFinal		date

	, primary key (Aluno_id, Projeto_id)
	, foreign key (Aluno_id) references Aluno
	, foreign key (Projeto_id) references Projeto
)

create table Mensagem
(
	  IdMensagem	int				not null	primary key identity
	, Pessoa_id		int				not null
	, Projeto_id	int				not null
	, Descricao		varchar(MAX)	not null

	, foreign key (Pessoa_id) references Pessoa
	, foreign key (Projeto_id) references Projeto
)

create table Referencia
(
	  Projeto_id   int not null
	, IdReferencia int not null identity
	, Descricao	   varchar(200) not null

	, foreign key (Projeto_id) references Projeto
)