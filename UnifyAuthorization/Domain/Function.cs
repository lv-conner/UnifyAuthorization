using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifyAuthorization.Domain
{
    /// <summary>
    /// 功能操作
    /// </summary>
    public class Function:AggregateRoot<Guid>
    {
        public string SystemNo { get; private set; }
        public string ModuleNo { get; private set; }
        public string ModuleName { get; private set; }
        public string ActionNo { get; private set; }
        public string ActionName { get; private set; }
        public Guid? CreateUserId { get; private set; }
        public DateTime? CreateTime { get; private set; }
        public virtual ICollection<RolePermission> RolePermissions { get; private set; }
        public virtual ICollection<UserPermission> UserPermissions { get; private set; }
        public Function()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserPermissions = new HashSet<UserPermission>();
        }

        public Function(string system, string module, string moduleName, string action, string actionName, Guid createUserId)
            :this()
        {
            SystemNo = system;
            ModuleNo = module;
            ModuleName = moduleName;
            ActionNo = action;
            ActionName = actionName;

            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
            CreateUserId = createUserId;
        }
    }
}
