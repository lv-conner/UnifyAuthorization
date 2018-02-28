using System;

namespace UnifyAuthorization.Domain
{
    public class RolePermission: BasePermission
    {
        public virtual Role Role { get; private set; }
        public Guid RoleId { get; set; }

        public RolePermission() { }

        public RolePermission(Guid roleId, Guid actionId, Guid createUserId)
        {

            Id = Guid.NewGuid();
            RoleId = roleId;
            FunctionId = actionId;

            CreateTime = DateTime.Now;
            CreateUserId = createUserId;
        }
    }
}