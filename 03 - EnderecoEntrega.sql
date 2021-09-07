create table EnderecosEntrega
(
	[Id] INT Identity(1,1) not null,
	[UsuarioId] int not null,

	[NomeEndereco] varchar(100) not null,
	[Cep] varchar(10) not null,
	[Estado] char(2) not null,
	[Cidade] varchar(120) not null,
	[Bairro] varchar(200) not null,
	[Endereco] varchar(70) not null,
	[Numero] varchar(20) null,
	[Complemento] varchar(30) null,
	[DataCadastro] Datetime not null,


constraint [pk_EnderecosEntrega] primary key clustered ([Id] asc) ,
constraint [fk_EnderecosEntrega_usuarios] foreign key ([UsuarioId]) REFERENCES Usuarios ([Id]) on DELETE CASCADE
);