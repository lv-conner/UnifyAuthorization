using System;
using System.Collections.Generic;

namespace UnifyAuthorization.Domain
{
    public class Company : AggregateRoot<Guid>
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Remark { get; private set; }
        public bool IsActive { get; private set; }
        /// <summary>
        /// 注册公司的同时,需添加管理员资料,然后设置公司管理员.
        /// </summary>
        public Guid Administrator { get; private set; }

        public DateTime? CreatorTime { get; private set; }
        public Guid? LastModifyUserId { get; private set; }
        public DateTime? LastModifyTime { get; private set; }

        public Guid? DeleteId { get; private set; }
        public DateTime? DeleteTime { get; private set; }
        public bool? DeleteStaus { get; private set; }
        public virtual ICollection<User> Users { get; private set; }
        public virtual ICollection<Role> Roles { get; private set; }

        public Company()
        {
            Users = new HashSet<User>();
            Roles = new HashSet<Role>();
        }

        public Company(string code, string name, string url, string remark)
            :this()
        {
            Id = Guid.NewGuid();
            Code = code;
            Name = name;
            Url = url;
            Remark = remark;

            CreatorTime = DateTime.Now;
        }

        public void SetActive(bool flag)
        {
            IsActive = flag;
        }

        public void SetAdministrator(Guid userId)
        {
            Administrator = userId;
        }

        public void SetDelete(Guid deleteUserId, bool flag)
        {
            DeleteTime = DateTime.Now;
            DeleteId = deleteUserId;
            DeleteStaus = flag;
        }

        public void Update(string code, string name, string url, string remark, Guid modifUserId)
        {
            Code = code;
            Name = name;
            Url = url;
            Remark = remark;

            LastModifyTime = DateTime.Now;
            LastModifyUserId = modifUserId;
        }

    }
}