using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class MotoristaViewModel
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }

        public MotoristaViewModel(Guid id, string nome, string cPF)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
        }
    }
}
