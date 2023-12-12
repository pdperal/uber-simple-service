

using Domain.DTO.Clientes;
using Domain.Entities;
using Domain.Interfaces.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString.Database;
        }

        public string BuscarIdClientePorCpf(string cpf)
        {
            string result = string.Empty;
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("select * from cliente where cpf = @p1 ", conn)
                {
                    Parameters =
                    {
                        new("p1", cpf)
                    }
                };

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result =  reader.GetString(0);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public List<ClienteDTO> BuscarTodosClientes()
        {
            List<ClienteDTO> result = new();
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("select * from cliente", conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new ClienteDTO
                    {
                        Id = Guid.Parse(reader.GetString(0)),
                        Nome = reader.GetString(1),
                        CPF = reader.GetString(2)
                    });
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public bool SalvarCliente(Cliente cliente)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("insert into cliente (id, nome, cpf) " +
                    " values (@p1, @p2, @p3) ", conn)
                {
                    Parameters =
                    {
                        new("p1", cliente.Id),
                        new("p2", cliente.Nome),
                        new("p3", cliente.CPF)
                    }
                };

                var result = cmd.ExecuteNonQuery();

                return result > 0;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
