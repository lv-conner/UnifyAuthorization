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
    public class FunctionApp : IFunctionApp
    {
        private readonly IRepository<Function> _funStore;
        private readonly IOperatorProvider _operatorProvider;
        public FunctionApp(IRepository<Function> funStore, IOperatorProvider operatorProvider)
        {
            _funStore = funStore;
            _operatorProvider = operatorProvider;
        }
        public void Delete(Guid key)
        {
            this.ExecuteAction(() =>
            {
                Function function = GetById(key);
                _funStore.Delete(function);
            });
        }

        private Function GetById(Guid key)
        {
            Function function = _funStore.Find(key);
            if(function == null)
            {
                throw new Exception("找不到指定的功能");
            }
            return function;
        }

        private async Task<Function> GetByIdAsync(Guid key)
        {
            Function function = await _funStore.FindAsync(key);
            if(function == null)
            {
                throw new Exception("找不到指定的功能");
            }
            return function;
        }

        public async Task DeleteAsync(Guid key)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Function function = await _funStore.FindAsync(key);
                await _funStore.DeleteAsync(function);
            });
        }

        private void Regist(FunctionInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Function function = new Function(obj.SystemNo, obj.ModuleNo, obj.ModuleName, obj.ActionNo, obj.ActionName, _operatorProvider.Current.UserId);
                _funStore.Insert(function);
            });
        }

        private async Task RegistAsync(FunctionInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Function function = new Function(obj.SystemNo, obj.ModuleNo, obj.ModuleName, obj.ActionNo, obj.ActionName, _operatorProvider.Current.UserId);
                await _funStore.InsertAsync(function);
            });
        }

        private void Update(FunctionInputDto obj)
        {
            this.ExecuteAction(() =>
            {
                Function function = GetById(obj.Id.Value);
                function.Update(obj.SystemNo, obj.ModuleNo, obj.ModuleName, obj.ActionNo, obj.ActionName, _operatorProvider.Current.UserId);
                _funStore.SaveChanges();
            });
        }

        private async Task UpdateAsync(FunctionInputDto obj)
        {
            await this.ExecuteActionAsync(async () =>
            {
                Function function = await GetByIdAsync(obj.Id.Value);
                function.Update(obj.SystemNo, obj.ModuleNo, obj.ModuleName, obj.ActionNo, obj.ActionName, _operatorProvider.Current.UserId);
                await _funStore.SaveChangesAsync();
            });
        }


        public Guid SubmitForm(FunctionInputDto obj)
        {
            if (obj.Id.HasValue)
            {
                Update(obj);
            }
            else
            {
                Regist(obj);
            }
            return obj.Id.Value;
        }

        public async Task<Guid> SubmitFormAsync(FunctionInputDto obj)
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
