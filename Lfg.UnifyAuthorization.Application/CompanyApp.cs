
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
    public class CompanyApp:ICompanyApp
    {
        private readonly IRepository<Company> _comStore;
        private readonly IRepository<User> _userStore;
        private readonly IOperatorProvider _operatorProvider;
        //private readonly IAccountValidatePolicy _validPolicy;

        public CompanyApp(IRepository<Company> comStore, IRepository<User> userStore,IOperatorProvider operatorProvider/*IAccountValidatePolicy validPolicy*/)
        {
            _comStore = comStore;
            _userStore = userStore;
            _operatorProvider = operatorProvider;
            //_validPolicy = validPolicy;
        }

        public void Delete(Guid key)
        {
            this.ExecuteAction(() =>
            {
                Company com = GetById(key);
                _comStore.Delete(com);
            });
        }

        public Guid SubmitForm(CompanyInputDto obj)
        {
            this.ExecuteScope(() =>
            {
                if (obj.Id.HasValue)
                    Update(obj);
                else
                    Regist(obj);
            });
            return obj.Id.Value;
        }

        public void Active(CompanyActiveDto obj)
        {
            this.ExecuteAction(() =>
            {
                Company com = GetById(obj.Id);
                com.SetActive(obj.Active);
            });

        }
        private void Regist(CompanyInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Company com = new Company(obj.Code, obj.Name, obj.Url, obj.Remark);
                _comStore.Insert(com);
                User user = new User(com.Id, "", obj.UserNo, obj.UserName, obj.Password, "", obj.PhoneNo, obj.EmailAddress, _operatorProvider.Current.UserId);
                _userStore.Insert(user);
                com.SetAdministrator(user.Id);
                obj.Id = com.Id;
            });
        }

        private async Task RegistAsync(CompanyInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Company com = new Company(obj.Code, obj.Name, obj.Url, obj.Remark);
                _comStore.Insert(com);
                User user = new User(com.Id, "", obj.UserNo, obj.UserName, obj.Password, "", obj.PhoneNo, obj.EmailAddress, _operatorProvider.Current.UserId);
                _userStore.Insert(user);
                com.SetAdministrator(user.Id);
                await _comStore.SaveChangesAsync();
                obj.Id = com.Id;
            });
        }

        private void Update(CompanyInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Company com = GetById(obj.Id.Value);
                com.Update(obj.Code, obj.Name, obj.Url, obj.Remark, _operatorProvider.Current.UserId);

                if (obj.Administrator.HasValue && obj.Administrator != com.Administrator)
                {
                    com.SetAdministrator(obj.Administrator.Value);
                }
                _comStore.SaveChanges();
            });
        }

        private async Task UpdateAsync(CompanyInputDto obj)
        {
            await this.ExecuteActionAsync( async () =>
            {
                Company com = GetById(obj.Id.Value);
                com.Update(obj.Code, obj.Name, obj.Url, obj.Remark, _operatorProvider.Current.UserId);

                if (obj.Administrator.HasValue && obj.Administrator != com.Administrator)
                {
                    com.SetAdministrator(obj.Administrator.Value);
                }
                await _comStore.SaveChangesAsync();
            });
        }

        private Company GetById(Guid Id)
        {
            Company company = _comStore.Find(Id);
            if (company == null)
                throw new Exception("找不到指定的公司");
            return company;
        }

        public async Task ActiveAsync(CompanyActiveDto obj)
        {
            await this.ExecuteActionAsync( async () =>
            {
                Company com = GetById(obj.Id);
                com.SetActive(obj.Active);
                await _comStore.SaveChangesAsync();
            });
        }

        public async Task DeleteAsync(Guid key)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Company com = GetById(key);
                _comStore.Delete(com);
                await _comStore.SaveChangesAsync();
            });
        }

        public async Task<Guid> SubmitFormAsync(CompanyInputDto obj)
        {
            this.ExeccuteScopeAsync( async () =>
            {
                if (obj.Id.HasValue)
                   await UpdateAsync(obj);
                else
                   await RegistAsync(obj);
            });
            return await Task.FromResult(obj.Id.Value);
        }
    }
}
