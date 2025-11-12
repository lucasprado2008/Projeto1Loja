-- CRIANDO O BANCO DE DADOS
CREATE DATABASE dbProjeto1Loja;

-- USANDO O BANCO DE DADOS
USE dbProjeto1Loja;

-- CRIANDO AS TABELAS
CREATE TABLE tbUsuario(
	idUsuario int primary key auto_increment,
    nome varchar(40) not null,
    email varchar(40) not null,
    senha varchar(40) not null
);

CREATE TABLE tbProduto(
	idProduto int primary key auto_increment,
    nome varchar(40) not null,
    descricao varchar(80) not null,
    preco decimal(8,2) not null,
    quantidade int not null
);

insert into tbUsuario (nome, email, senha) values ("L", "l@1", "1" );

select * from tbUsuario;
select * from tbProduto;