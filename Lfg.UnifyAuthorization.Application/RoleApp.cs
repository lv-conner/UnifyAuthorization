using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Application.Interface;
using Lfg.UnifyAuthorization.Domain;
using Lfg.UnifyAuthorization.Repository.Interface;
using Lfg.UnifyAuthorization.Application.Extension;
using Lfg.UnifyAuthorization.Application.Dto;
using Lfg.UnifyAuthorization.Application.Operator.Interface;

namespace Lfg.UnifyAuthorization.Application
{
    public class RoleApp : IRoleApp
    {
        private readonly IRepository<Role> _roleStore;
        private readonly IOperatorProvider _operatorProvider;
        //private readonly IRepository<Log> _logStore;
        //private readonly IRoleDeletePolicy _rolePolicy;

        public RoleApp(IRepository<Role> roleStore, IOperatorProvider operatorProvider  /*IRoleDeletePolicy validRole*/)
        {
            _roleStore = roleStore;
            _operatorProvider = operatorProvider;
            //_rolePolicy = validRole;
        }

        public void Delete(Guid key)
        {
            this.ExecuteAction(() =>
            {
                Role role = GetById(key);
                //_rolePolicy.CheckDeleteEnable(key);
                _roleStore.Delete(role);
            });

        }

        public Guid SubmitForm(RoleInputDto obj)
        {
            if (obj.Id.HasValue)
                Update(obj);
            else
                Regist(obj);
            return obj.Id.Value;
        }

        public void AddRoleUser(RoleUserInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Role role = GetById(obj.RoleId);
                role.AddMember(obj.UserId, _operatorProvider.Current.UserId);
                _roleStore.SaveChanges();
            });
        }

        public void RemoveRoleUser(RoleUserInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Role role = GetById(obj.RoleId);
                role.RemoveMember(obj.UserId);
                _roleStore.SaveChanges();
            });

        }

        public void AddRolePermission(RolePermissionInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Role role = GetById(obj.RoleId);
                role.AddPermission(obj.PermissionId, _operatorProvider.Current.UserId);
                _roleStore.SaveChanges();
            });

        }

        public void RemoveRolePermission(RolePermissionInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Role role = GetById(obj.RoleId);
                role.RemovePermission(obj.PermissionId);
                _roleStore.SaveChanges();
            });
        }

        public void SaveRolePermission(Guid roleId, string permissions)
        {
            this.ExecuteAction(() =>
            {
                Role role = GetById(roleId);
                role.SavePermission(permissions, _operatorProvider.Current.UserId);
                _roleStore.SaveChanges();
            });
        }

        private void Regist(RoleInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Role role = new Role(obj.CompanyId, obj.RoleName, obj.Description, _operatorProvider.Current.UserId);
                _roleStore.Insert(role);
                obj.Id = role.Id;
            });
        }

        private async Task RegistAsync(RoleInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Role role = new Role(obj.CompanyId, obj.RoleName, obj.Description, _operatorProvider.Current.UserId);
                await _roleStore.InsertAsync(role);
                obj.Id = role.Id;
            });
        }

        private void Update(RoleInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Role role = GetById(obj.Id.Value);
                role.Update(obj.RoleName, obj.Description, _operatorProvider.Current.UserId);
                _roleStore.SaveChanges();
            });
        }

        private async Task UpdateAsync(RoleInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Role role = await GetByIdAsync(obj.Id.Value);
                role.Update(obj.RoleName, obj.Description, _operatorProvider.Current.UserId);
                await _roleStore.SaveChangesAsync();
            });
        }
        private Role GetById(Guid Id)
        {
            Role role = _roleStore.Find(Id);
            if (role == null)
                throw new Exception("找不到指定的角色");
            return role;
        }

        public async Task AddRolePermissionAsync(RolePermissionInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Role role = GetById(obj.RoleId);
                role.AddPermission(obj.PermissionId, _operatorProvider.Current.UserId);
                await _roleStore.SaveChangesAsync();
            });
        }

        public async Task AddRoleUserAsync(RoleUserInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Role role = GetById(obj.RoleId);
                role.AddMember(obj.UserId, _operatorProvider.Current.UserId);
                await _roleStore.SaveChangesAsync();
            });
        }

        public async Task RemoveRolePermissionAsync(RolePermissionInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Role role = GetById(obj.RoleId);
                role.RemovePermission(obj.PermissionId);
                await _roleStore.SaveChangesAsync();
            });
        }

        public async Task RemoveRoleUserAsync(RoleUserInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Role role = await GetByIdAsync(obj.RoleId);
                role.RemoveMember(obj.UserId);
                await _roleStore.SaveChangesAsync();
            });
        }

        private async Task<Role> GetByIdAsync(Guid roleId)
        {
            Role role = await _roleStore.FindAsync(roleId);
            if(role == null)
            {
                throw new Exception("找不到对应角色");
            }
            else
            {
                return role;
            }
        }

        public async Task SaveRolePermissionAsync(Guid roleId, string permissions)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Role role = await GetByIdAsync(roleId);
                role.SavePermission(permissions, _operatorProvider.Current.UserId);
                await _roleStore.SaveChangesAsync();
            });
        }

        public async Task DeleteAsync(Guid key)
        {
            await this.ExecuteActionAsync(async () => 
            {
                Role role = await GetByIdAsync(key);
                await _roleStore.DeleteAsync(role);
            });
        }

        public async Task<Guid> SubmitFormAsync(RoleInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                if(obj.Id.HasValue)
                {
                    await UpdateAsync(obj);
                }
                else
                {
                    await RegistAsync(obj);
                }
            });
            return await Task.FromResult(obj.Id.Value);
        }
    }
}
