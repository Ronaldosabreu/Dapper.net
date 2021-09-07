/*One to One*/
create table Contatos(
[Id] INT Identity(1,1) not null,
[UsuarioId] int not null,
[Telefone] varchar(15) null,
[Celular] varchar(15) null,


constraint [pk_contatos] primary key clustered ([Id] asc) ,
constraint [fk_contatos_usuarios] foreign key ([UsuarioId]) REFERENCES Usuarios ([Id]) on DELETE CASCADE
);