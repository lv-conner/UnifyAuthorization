using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifyAuthorization.Base
{
    public interface IAggregateRoot
    {
        string Id { get; }
    }
}
