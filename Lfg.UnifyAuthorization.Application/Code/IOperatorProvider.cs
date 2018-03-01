using Lfg.UnifyAuthorization.Application.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Operator.Interface
{
    public interface IOperatorProvider
    {
        OperatorModel Current { get; }
        ICollection<PermissionModel> CurrentPermission { get; }
    }
}
