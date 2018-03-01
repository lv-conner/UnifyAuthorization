using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Interface
{
    public interface IApp<TInp,TId> where TInp:class,new()
    {
        void Delete(TId key);
        Task DeleteAsync(TId key);
        TId SubmitForm(TInp obj);
        Task<TId> SubmitFormAsync(TInp obj);
    }
}
