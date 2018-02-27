using System;

namespace UnifyAuthorization.Domain
{
    public class RolePermission: AggregateRoot<Guid>
    {
        public virtual Role Role { get; private set; }
        public virtual FunctionModel Function { get; private set; }
        public Guid RoleId { get; set; }
        public Guid ActionId { get; private set; }

        public Guid? CreateUserId { get; private set; }
        public DateTime? CreateTime { get; private set; }

        public RolePermission() { }

        public RolePermission(Guid roleId, Guid actionId, Guid createUserId)
        {

            Id = Guid.NewGuid();
            RoleId = roleId;
            ActionId = actionId;

            CreateTime = DateTime.Now;
            CreateUserId = createUserId;
        }
    }
}