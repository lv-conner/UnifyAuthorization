using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnifyAuthorization.Domain
{
    public class Role : AggregateRoot<Guid>
    {
        public Company Company { get; private set; }
        public Guid CompanyId { get; private set; }
        public string RoleName { get; private set; }
        public string Description { get; private set; }

        public Guid? CreateUserId { get; private set; }
        public DateTime? CreateTime { get; private set; }
        public Guid? LastModifyUserId { get; private set; }
        public DateTime? LastModifyTime { get; private set; }

        public virtual ICollection<RoleUser> RoleUsers { get; private set; }

        public virtual ICollection<RolePermission> RolePermissions { get; private set; }

        public Role()
        {
            RoleUsers = new HashSet<RoleUser>();
            RolePermissions = new HashSet<RolePermission>();
        }

        public Role(Guid companyId, string roleName, string description, Guid createUserId)
            : this()
        {
            Id = Guid.NewGuid();
            CompanyId = companyId;
            RoleName = roleName;
            Description = description;

            CreateTime = DateTime.Now;
            CreateUserId = createUserId;
        }

        public void Update(string roleName, string description, Guid modifUserId)
        {
            RoleName = roleName;
            Description = description;

            LastModifyTime = DateTime.Now;
            LastModifyUserId = modifUserId;
        }

        public void AddMember(Guid userId, Guid createUserId)
        {
            if (RoleUsers.FirstOrDefault(m => m.UserId == userId) == null)
                RoleUsers.Add(new RoleUser(Id, userId, createUserId));
        }

        public void RemoveMember(Guid userId)
        {
            RoleUser find = RoleUsers.FirstOrDefault(m => m.RoleId == Id && m.UserId == userId);
            if (find != null)
            {
                find.RoleId = Guid.Empty;
                RoleUsers.Remove(find);
            }
        }

        public void AddPermission(Guid actionId, Guid createUserId)
        {
            if (RolePermissions.FirstOrDefault(m => m.FunctionId == actionId) == null)
                RolePermissions.Add(new RolePermission(Id, actionId, createUserId));
        }

        public void RemovePermission(Guid actionId)
        {
            RolePermission find = RolePermissions.FirstOrDefault(m => m.RoleId == Id && m.FunctionId == actionId);
            if (find != null)
            {
                find.RoleId = Guid.Empty;
                RolePermissions.Remove(find);
            }
        }

        public void SavePermission(string actionIds, Guid createUserId)
        {
            foreach (RolePermission p in RolePermissions)
            {
                p.RoleId = Guid.Empty;
            }
            RolePermissions.Clear();
            if (actionIds != null)
            {
                string[] actions = actionIds.Split(',');
                foreach (string action in actions)
                {
                    Guid id = Guid.Empty;
                    if (Guid.TryParse(action, out id))
                    {
                        AddPermission(id, createUserId);
                    }
                }
            }
        }
    }
}
