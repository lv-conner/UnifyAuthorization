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

        public virtual ICollection<RoleUser> Members { get; private set; }

        public virtual ICollection<RolePermission> Permissions { get; private set; }

        public Role()
        {
            Members = new HashSet<RoleUser>();
            Permissions = new HashSet<RolePermission>();
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
            if (Members.FirstOrDefault(m => m.UserId == userId) == null)
                Members.Add(new RoleUser(Id, userId, createUserId));
        }

        public void RemoveMember(Guid userId)
        {
            RoleUser find = Members.FirstOrDefault(m => m.RoleId == Id && m.UserId == userId);
            if (find != null)
            {
                find.RoleId = Guid.Empty;
                Members.Remove(find);
            }
        }

        public void AddPermission(Guid actionId, Guid createUserId)
        {
            if (Permissions.FirstOrDefault(m => m.ActionId == actionId) == null)
                Permissions.Add(new RolePermission(Id, actionId, createUserId));
        }

        public void RemovePermission(Guid actionId)
        {
            RolePermission find = Permissions.FirstOrDefault(m => m.RoleId == Id && m.ActionId == actionId);
            if (find != null)
            {
                find.RoleId = Guid.Empty;
                Permissions.Remove(find);
            }
        }

        public void SavePermission(string actionIds, Guid createUserId)
        {
            foreach (RolePermission p in Permissions)
            {
                p.RoleId = Guid.Empty;
            }
            Permissions.Clear();
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
