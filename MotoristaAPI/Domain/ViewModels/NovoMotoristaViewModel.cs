using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class NovoMotoristaViewModel
    {
        public Guid Id { get; private set; }

        public NovoMotoristaViewModel(Guid id)
        {
            Id = id;            
        }
    }
}
