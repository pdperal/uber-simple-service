create table public.cliente
(
	id varchar(48) primary key,
	nome varchar not null,
	cpf varchar not null,
	saldo DECIMAL not null
);