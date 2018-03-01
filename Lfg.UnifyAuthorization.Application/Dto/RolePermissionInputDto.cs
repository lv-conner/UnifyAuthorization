using System;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class RolePermissionInputDto
    {
        /// <summary>
        /// 角色系统ID
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 权限系统ID
        /// </summary>
        public Guid PermissionId { get; set; }
    }
}
