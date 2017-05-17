--Pessoa--

insert into Pessoa		values ('Luiz'  , 1,'luiz@fatec.sp.gov.br', 'fatec123');
insert into Pessoa		values ('j�o'  ,  1, 'jao@fatec.sp.gov.br', 'fatec123');
insert into Pessoa		values ('leo'  ,  2, 'leonardomsoares1@gmail.com', 'fatec123');
insert into Pessoa		values ('Lucimar', 2,'lucimar@fatec.sp.gov.br', 'fatec123');
insert into Pessoa		values ('Vianna', 2,'vianna@fatec.sp.gov.br', 'fatec123');
insert into Pessoa		values ('Carlos', 2,'carlos@fatec.sp.gov.br', 'fatec123');
insert into Pessoa		values ('Admin' , 3,'admin@admin.com', 'admin');
insert into Pessoa		values ('Professor Email', 2, 'leonardo.soares3@fatec.sp.gov.br', 'fatec123');


--Aluno--
insert into Aluno		values (1, '112111', 'ADS');
insert into Aluno		values (2, '112211', 'ADS');
insert into Aluno		values (3, '112131', 'ADS');

--Professor--
insert into Professor		values (4, '1234');
insert into Professor		values (5, '3211');
insert into Professor		values (6, '1123');
insert into Professor		values (8, '2346');





--TipoProjeto--
insert into TipoProjeto		values (1,'TCC');
insert into TipoProjeto		values (2,'Scientific Research');

--TipoRedeSocial--
insert into TipoRedeSocial	values (1, 'Facebook');
insert into TipoRedeSocial	values (2, 'Twitter');
insert into TipoRedeSocial	values (3, 'GooglePlus');
insert into TipoRedeSocial	values (4, 'Linkedin');
--RedeSocial--
insert into Social			values (1, '#', 1);
insert into Social			values (1, '#', 2);
insert into Social			values (1, '#', 3);
insert into Social			values (1, '#', 4);

--Projeto�
insert into projeto (Professor_id, TipoProjeto_id, NomeProjeto, Status, DataInicio, Descricao) 
			values	(8,2,'SEHA',1,'2016-05-05','Hor�rios FATEC');
insert into projeto (Professor_id, TipoProjeto_id, NomeProjeto, Status, DataInicio, Descricao) 
			values	(5,1,'SEJA',1,'2016-05-05','O Palmeiras � a equipe brasileira com o maior n�mero de t�tulos de abrang�ncia nacional conquistados, sendo o �nico a vencer todas as competi��es oficiais que disputou criadas no Pa�s, inicialmente pela Confedera��o Brasileira de Desportos (CBD) e, a partir da d�cada de 1980, pela Confedera��o Brasileira de Futebol (CBF).[12] O alviverde possui 13 conquistas[13][14] deste porte, com destaque maior para seus 9 t�tulos do Campeonato Brasileiro: 1960, 1967(1), 1967(2), 1969, 1972, 1973, 1993, 1994 e 2016. Al�m destes campeonatos, o Palmeiras j� venceu no pa�s as Copas do Brasil de 1998, 2012 e de 2015 e a Copa dos Campe�es de 2000, competi��es tamb�m organizadas pela entidade m�xima do futebol brasileiro.');
insert into projeto (Professor_id, TipoProjeto_id, NomeProjeto, Status, DataInicio, Descricao) 
			values	(6,2,'FoodGo',2,'2016-05-05','Food Truck');
insert into projeto (Professor_id, TipoProjeto_id, NomeProjeto, Status, DataInicio, Descricao) 
			values	(5,1,'Mobile',2,'2016-05-05','Sistema respons�vel pelo gerenciamento de mobiles da empresa');

--Mensagem�

insert into Mensagem(Pessoa_id, Projeto_id, Descricao) values (1,1,'Ol�');
insert into Mensagem(Pessoa_id, Projeto_id, Descricao) values (2,3,'Ol�');

insert into Mensagem(Pessoa_id, Projeto_id, Descricao) values (3,1,'Ol�');

--AlunoData�

insert into AlunoData (Aluno_id, Projeto_id, DataInicio) values (1,1,'2016-06-01');
insert into AlunoData (Aluno_id, Projeto_id, DataInicio) values (3,1,'2016-06-01');
insert into AlunoData (Aluno_id, Projeto_id, DataInicio) values (1,2,'2016-06-01');
insert into AlunoData (Aluno_id, Projeto_id, DataInicio) values (2,3,'2016-06-01');
insert into AlunoData (Aluno_id, Projeto_id, DataInicio) values (2,4,'2016-06-01');



--Referencia--
insert into Referencia (Projeto_id,  Descricao) values (1,'Andrew Stuart Tanenbaum');
insert into Referencia (Projeto_id,  Descricao) values (1,'Abraham Silberschatz');
insert into Referencia (Projeto_id,  Descricao) values (1,'Maya');
