using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Application.Dto;
namespace Lfg.UnifyAuthorization.Application.Interface
{
    public interface ICompanyApp: IApp<CompanyInputDto,Guid>
    {
        void Active(CompanyActiveDto obj);
        Task ActiveAsync(CompanyActiveDto obj);
    }
}
