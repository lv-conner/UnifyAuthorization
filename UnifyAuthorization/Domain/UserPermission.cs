using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifyAuthorization.Domain
{
    public class UserPermission:BasePermission
    {
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public UserPermission()
        {
        }

        public UserPermission(Guid userId, Guid actionId, Guid createUserId)
        {

            Id = Guid.NewGuid();
            UserId = userId;
            FunctionId = actionId;

            CreateTime = DateTime.Now;
            CreateUserId = createUserId;
        }

    }
}
