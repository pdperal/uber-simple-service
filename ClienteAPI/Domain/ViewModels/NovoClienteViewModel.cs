using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class NovoClienteViewModel
    {
        public Guid Id { get; private set; }

        public NovoClienteViewModel(Guid id)
        {
            Id = id;            
        }
    }
}
