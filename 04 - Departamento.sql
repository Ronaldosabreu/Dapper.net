/* many to many*/
create table Departamentos
(
	[Id] INT Identity(1,1) not null,
	[Nome] varchar(150) not null


	constraint [pk_Departamentos] primary key clustered ([Id] asc) ,

);


create table UsuariosDepartamentos
(
	[Id] INT Identity(1,1) not null,
	[UssuarioId] int not null,
	[DepartamentoId] int not null


	constraint [pk_UsuariosDepartamentos] primary key clustered ([Id] asc) ,

	constraint [fk_UsuariosDepartamentos] foreign key ([UsuarioId]) REFERENCES Usuarios ([Id]) on DELETE CASCADE,
	constraint [fk_Departamentos] foreign key ([DepartamentoId]) REFERENCES Departamentos ([Id]) on DELETE CASCADE

);
