using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Domain
{
    public class BasePermission: AggregateRoot<Guid>
    {
        public virtual Function Function { get; protected set; }
        public Guid FunctionId { get; protected set; }

        public Guid? CreateUserId { get; protected set; }
        public DateTime? CreateTime { get; protected set; }
    }
}
