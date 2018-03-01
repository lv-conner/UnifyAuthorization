using System;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class RoleUserInputDto
    {
        /// <summary>
        /// 角色系统ID
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// 用户系统ID
        /// </summary>
        public Guid UserId { get; set; }
    }
}
