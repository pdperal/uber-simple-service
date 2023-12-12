using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPrecoService
    {
        public Cotacao CalcularCotacao(string latitudeOrigem, string longitudeOrigem, string latitudeDestino, string longitudeDestino);
    }
}
