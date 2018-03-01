using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Application.Dto;
using Lfg.UnifyAuthorization.Application.Extension;
using Lfg.UnifyAuthorization.Application.Interface;
using Lfg.UnifyAuthorization.Application.Operator;
using Lfg.UnifyAuthorization.Application.Operator.Interface;
using Lfg.UnifyAuthorization.Domain;
using Lfg.UnifyAuthorization.Repository.Interface;

namespace Lfg.UnifyAuthorization.Application
{
    public class UserApp : IUserApp
    {
        private readonly IRepository<User> _userStore;
        private readonly IOperatorProvider _operatorProvide;
        //private readonly IRoleUserRepository _roleUserStore;
        //private readonly IAccountValidatePolicy _validPolicy;
        //private readonly IUserDeletePolicy _userDeletePolicy;

        public UserApp(IRepository<User> userStore, IOperatorProvider operatorProvider /*IRoleUserRepository roleUserStore, IAccountValidatePolicy userPolicy, IUserDeletePolicy userDeletePolicy*/)
        {
            _userStore = userStore;
            _operatorProvide = operatorProvider;
            //_validPolicy = userPolicy;
            //_userDeletePolicy = userDeletePolicy;
        }

        public void Delete(Guid key)
        {
            this.ExecuteScope(() =>
            {
                User user = GetById(key);
                //_userDeletePolicy.CheckDeleteEnable(key);
                _userStore.Delete(user);
                //_roleUserStore.DeleteRoleInUserByUserId(key);
            });

        }


        public Guid SubmitForm(UserInputDto obj)
        {

            if (obj.Id.HasValue)
                Update(obj);
            else
                Regist(obj);

            return obj.Id.Value;
        }

        public void UpdatePassword(UserModifPwdDto obj)
        {
            this.ExecuteAction(() =>
            {
                User user = GetById(obj.Id);

                user.UpdatePassword(obj.Password, obj.OriginalPassword);
                _userStore.SaveChanges();

            });
        }


        public void ActiveUser(Guid userId, bool flag)
        {
            this.ExecuteAction(() =>
            {
                User user = GetById(userId);
                user.ActiveUser(flag);
                _userStore.SaveChanges();
            });
        }

        public void LoginCheck(UserLoginDto obj)
        {
            this.ExecuteAction(() =>
            {
                //_validPolicy.CheckUserLogin(obj.UserNo, obj.Password);
            });
        }

        private void Regist(UserInputDto obj)
        {
            this.ExecuteScope(() =>
            {
                //_validPolicy.CheckUserRegistration(obj.UserNo);
                User user = new User(obj.CompanyId, obj.Department, obj.UserNo, obj.UserName, obj.Password, obj.Duty, obj.PhoneNo, obj.EmailAddress, _operatorProvide.Current.UserId);
                _userStore.Insert(user);

                obj.Id = user.Id;
            });
        }

        private async Task RegistAsync(UserInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                User user = new User(obj.CompanyId, obj.Department, obj.UserNo, obj.UserName, obj.Password, obj.Duty, obj.PhoneNo, obj.EmailAddress, _operatorProvide.Current.UserId);
                await _userStore.InsertAsync(user);
                obj.Id = user.Id;
            });
        }

        private void Update(UserInputDto obj)
        {
            this.ExecuteScope(() =>
            {
                User user = GetById(obj.Id.Value);

                user.Update(obj.CompanyId, obj.Department, obj.UserNo, obj.UserName, obj.Duty, obj.PhoneNo, obj.EmailAddress, obj.Password, obj.PasswordOriginal, _operatorProvide.Current.UserId);
                _userStore.SaveChanges();
            });
        }

        private async Task UpdateAsync(UserInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                User user = await GetByIdAsync(obj.Id.Value);

                user.Update(obj.CompanyId, obj.Department, obj.UserNo, obj.UserName, obj.Duty, obj.PhoneNo, obj.EmailAddress, obj.Password, obj.PasswordOriginal, _operatorProvide.Current.UserId);
                await _userStore.SaveChangesAsync();
            });
        }

        private async Task<User> GetByIdAsync(Guid key)
        {
            User user = await _userStore.FindAsync(key);
            if (user == null)
                throw new Exception("找不到指定的用户");

            return user;
        }

        private User GetById(Guid Id)
        {
            User user = _userStore.Find(Id);
            if (user == null)
                throw new Exception("找不到指定的用户");

            return user;
        }

        public async Task DeleteAsync(Guid key)
        {
            await this.ExecuteActionAsync(async () =>
            {
                User user = await GetByIdAsync(key);
                await _userStore.DeleteAsync(user);
            });
        }

        public async Task<Guid> SubmitFormAsync(UserInputDto obj)
        {
            if(obj.Id.HasValue)
            {
                await UpdateAsync(obj);
            }
            else
            {
                await RegistAsync(obj);
            }
            return await Task.FromResult(obj.Id.Value);
        }
    }
}
