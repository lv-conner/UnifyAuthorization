using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Domain
{
    public class RoleUser : AggregateRoot<Guid>
    {

        public virtual Role Role { get; private set; }
        public virtual User User { get; private set; }

        public Guid RoleId { get; set; }

        public Guid UserId { get; private set; }

        public Guid? CreateUserId { get; private set; }
        public DateTime? CreateTime { get; private set; }

        public RoleUser()
        {

        }

        public RoleUser(Guid roleId, Guid userId, Guid createUserId)
        {
            Id = Guid.NewGuid();
            RoleId = roleId;
            UserId = userId;

            CreateTime = DateTime.Now;
            CreateUserId = createUserId;
        }
    }
}
