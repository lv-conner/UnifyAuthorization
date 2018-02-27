using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifyAuthorization.Domain
{
    /// <summary>
    /// 功能模型
    /// </summary>
    public class FunctionModel:AggregateRoot<Guid>
    {
        public string SystemNo { get; private set; }
        public string ModuleNo { get; private set; }
        public string ModuleName { get; private set; }
        public string ActionNo { get; private set; }
        public string ActionName { get; private set; }

        public Guid? CreateUserId { get; private set; }
        public DateTime? CreateTime { get; private set; }
        public virtual ICollection<RolePermission> RolePermission { get; private set; }
        public FunctionModel()
        {
            RolePermission = new HashSet<RolePermission>();
        }

        public FunctionModel(string system, string module, string moduleName, string action, string actionName, Guid createUserId)
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
