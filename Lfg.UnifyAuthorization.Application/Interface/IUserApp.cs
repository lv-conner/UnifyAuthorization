using Lfg.UnifyAuthorization.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Interface
{
    public interface IUserApp : IApp<UserInputDto, Guid>
    {
        void LoginCheck(UserLoginDto obj);
        void UpdatePassword(UserModifPwdDto obj);
        void ActiveUser(Guid userId, bool flag);
    }
}
