using System;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class RoleInputDto 
    {
        /// <summary>角色系统ID
        /// Id等于null表示注册角色，否则表示修改角色信息
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>角色所属公司Id
        /// </summary>
        public Guid CompanyId { get; set; }
        /// <summary>角色名称，例如“管理员”。
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>描述信息
        /// </summary>
        public string Description { get; set; }
    }
}
