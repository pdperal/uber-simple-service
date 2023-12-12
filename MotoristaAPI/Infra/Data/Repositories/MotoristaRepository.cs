using Domain.DTO;
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
    public class MotoristaRepository : IMotoristaRepository
    {
        private readonly string _connectionString;

        public MotoristaRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString.Database;
        }

        public bool AssociarCorridaAoMotorista(CorridaMotorista corridaMotorista)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("insert into motorista_corrida (id, id_cliente, id_motorista, finalizado) " +
                    " values (@p1, @p2, @p3, @p4) ", conn)
                {
                    Parameters =
                    {
                        new("p1", corridaMotorista.IdCotacao),
                        new("p2", corridaMotorista.IdCliente),
                        new("p3", corridaMotorista.IdMotorista),
                        new("p4", false),
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

        public string BuscarIdMotoristaPorCpf(string cpf)
        {
            string result = string.Empty;
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("select * from motorista where cpf = @p1 ", conn)
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

        public List<MotoristaDTO> BuscarTodosMotoristas()
        {
            List<MotoristaDTO> result = new();
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("select * from motorista", conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new MotoristaDTO
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

        public bool FinalizarCorrida(Guid corridaId)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand($"update motorista_corrida set finalizado = true where id = '{corridaId}'", conn);

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

        public List<CorridaDTO> ListarCorridas()
        {
            List<CorridaDTO> result = new();
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("select * from motorista_corrida", conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new CorridaDTO
                    {
                        IdCorrida = Guid.Parse(reader.GetString(0)),
                        IdCliente = Guid.Parse(reader.GetString(1)),
                        IdMotorista = Guid.Parse(reader.GetString(2)),
                        Finalizado = reader.GetBoolean(3),

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

        public bool SalvarMotorista(Motorista motorista)
        {
            using var dataSource = NpgsqlDataSource.Create(_connectionString);

            var conn = dataSource.OpenConnection();
            try
            {
                var cmd = new NpgsqlCommand("insert into motorista (id, nome, cpf) " +
                    " values (@p1, @p2, @p3) ", conn)
                {
                    Parameters =
                    {
                        new("p1", motorista.Id),
                        new("p2", motorista.Nome),
                        new("p3", motorista.CPF),
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
