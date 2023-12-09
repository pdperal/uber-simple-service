using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPrecoService
    {
        public Result CalcularCotacao(string latitudeOrigem, string longitudeOrigem, string latitudeDestino, string longitudeDestino);
    }
}
