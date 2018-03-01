using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Application.Dto;

namespace Lfg.UnifyAuthorization.Application.Interface
{
    public interface IRoleApp : IApp<RoleInputDto, Guid>
    {
        void AddRolePermission(RolePermissionInputDto obj);
        Task AddRolePermissionAsync(RolePermissionInputDto obj);
        void AddRoleUser(RoleUserInputDto obj);
        Task AddRoleUserAsync(RoleUserInputDto obj);
        void RemoveRolePermission(RolePermissionInputDto obj);
        Task RemoveRolePermissionAsync(RolePermissionInputDto obj);
        void RemoveRoleUser(RoleUserInputDto obj);
        Task RemoveRoleUserAsync(RoleUserInputDto obj);
        void SaveRolePermission(Guid roleId, string permissions);
        Task SaveRolePermissionAsync(Guid roleId, string permissions);
    }
}
