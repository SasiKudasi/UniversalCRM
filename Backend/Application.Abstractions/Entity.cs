using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public abstract class Entity
    {
        public virtual Guid ID { get; }
    }
}
