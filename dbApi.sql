-- Criando a database --
create database dbapi;
-- Usando a database --
use dbapi;
-- Criando as tabelas --
create table tbProdutos(
Id int primary key auto_increment,
Nome varchar(50) not null,
Categoria varchar(40) not null,
Preco decimal(9,2) not null
);