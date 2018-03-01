using Lfg.UnifyAuthorization.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lfg.UnifyAuthorization.Application.Extension
{
    public static class Extesion
    {
        public static void ExecuteScope<TInp, TId>(this IApp<TInp, TId> App, Action action) where TInp : class, new()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    action.Invoke();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("产生了系统未处理的异常", ex);
                }
            }
        }
        public static async void ExeccuteScopeAsync<TInp, TId>(this IApp<TInp, TId> app, Func<Task> func) where TInp : class, new()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    await func.Invoke();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("产生了系统未处理的异常", ex);
                }
            }
        }
        public static void ExecuteAction<TInp, TId>(this IApp<TInp, TId> App, Action action) where TInp : class, new()
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("产生了系统未处理的异常", ex);
            }
        }

        public static async Task ExecuteActionAsync<TInp, TId>(this IApp<TInp, TId> app, Func<Task> func) where TInp : class, new()
        {
            try
            {
                await func.Invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("产生了系统未处理的异常", ex);
            }
        }

        public static async Task<TResult> ExecuteFuncAsync<TInp, TId, TResult>(this IApp<TInp, TId> app, Func<Task<TResult>> func) where TInp : class, new()
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                throw new Exception("产生了系统未处理的异常", ex);
            }
        }
    }
}
