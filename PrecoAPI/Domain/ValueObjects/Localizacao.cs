using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public struct Localizacao
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Localizacao(double latitude, double longitude)
        {
            Latitude =  latitude;
            Longitude = longitude;
        }
    }
}
