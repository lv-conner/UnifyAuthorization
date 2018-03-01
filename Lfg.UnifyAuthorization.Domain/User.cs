using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Domain
{
    public class User : AggregateRoot<Guid>
    {
        public Guid CompanyId { get; private set; }
        public Company Company { get; private set; }
        public string Department { get; private set; }

        public string UserNo { get; private set; }

        public string UserName { get; private set; }

        public string Duty { get; private set; }

        public string Password { get; private set; }
        public string EmailAddress { get; private set; }
        public string PhoneNo { get; private set; }
        public bool IsActive { get; private set; }

        public DateTime? ActiveDate { get; private set; }
        public int AccessFailedCount { get; private set; }

        public DateTime? AccessFailedDateTime { get; private set; }
        public DateTime? LastLoginTime { get; private set; }

        public Guid? CreateUserId { get; private set; }
        public DateTime? CreateTime { get; private set; }
        public Guid? LastModifyUserId { get; private set; }
        public DateTime? LastModifyTime { get; private set; }

        public virtual ICollection<RoleUser> RoleUsers { get; private set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }

        public User()
        {
            RoleUsers = new HashSet<RoleUser>();
            UserPermission = new HashSet<UserPermission>();
        }

        public User(Guid companyId, string department, string userNo, string userName, string password, string duty, string phoneNo, string mail, Guid? createUserId)
            :this()
        {
            Id = Guid.NewGuid();
            CompanyId = companyId;
            Department = department;
            UserNo = userNo;
            UserName = userName;
            Password = password;
            Duty = duty;
            EmailAddress = mail;
            PhoneNo = phoneNo;
            IsActive = false;
            CreateTime = DateTime.Now;
            CreateUserId = createUserId;
        }

        public void AddLoginStatus()
        {
            this.AccessFailedCount = 0;
            this.LastLoginTime = DateTime.Now;
        }

        public void AddLoginError()
        {
            this.AccessFailedCount++;
            this.AccessFailedDateTime = DateTime.Now;
        }

        public void ActiveUser(bool flag)
        {
            this.AccessFailedCount = 0;

            IsActive = flag;
            ActiveDate = DateTime.Now;
        }

        public void Update(Guid companyId, string department, string userNo, string userName, string duty, string phoneNo, string mail, string password, string passwordOriginal, Guid modifUserId)
        {
            if (password != null)
            {
                UpdatePassword(password, passwordOriginal);
            }

            CompanyId = companyId;
            Department = department;
            UserNo = userNo;
            UserName = userName;
            Duty = duty;
            EmailAddress = mail;
            PhoneNo = phoneNo;

            LastModifyTime = DateTime.Now;
            LastModifyUserId = modifUserId;
        }

        public void UpdatePassword(string password, string passwordOriginal)
        {
            if (passwordOriginal != Password)
            {
                throw new ArgumentException("需提供正确的原始密码");
            }
            Password = password;
        }

    }
}
