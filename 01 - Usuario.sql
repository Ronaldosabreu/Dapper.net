create table Usuarios
(
	[Id] INT Identity(1,1) not null,
	
	
	[Nome] varchar(70) not null,
	[Email] varchar(250) not null,
	[Sexo] char(1) null,
	[RG] varchar(20) null,
	[CPF] varchar(14) null,
	[NomeMae] varchar(70) null,
	[SituacaoCadastro] char(1) not null,
	[DataCadastro] Datetime not null,


constraint [pk_usuarios] primary key clustered ([Id] asc) ,
/*constraint [fk_contatos_usuarios] foreign key ([UsuarioId]) REFERENCES Usuarios ([Id]) on DELETE CASCADE*/
);