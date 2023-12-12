create table public.motorista
(
	id varchar(48) primary key,
	nome varchar not null,
	cpf varchar not null
);

create table public.motorista_corrida
(
	id varchar(48) primary key,
	id_cliente varchar(48),
	id_motorista varchar(48),
	finalizado boolean
);