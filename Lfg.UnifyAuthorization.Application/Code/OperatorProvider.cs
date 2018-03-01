using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Application.Code;
using Lfg.UnifyAuthorization.Application.Operator.Interface;

namespace Lfg.UnifyAuthorization.Application.Operator
{
    public class OperatorProvider : IOperatorProvider
    {
        public OperatorModel Current => throw new NotImplementedException();

        public ICollection<PermissionModel> CurrentPermission => throw new NotImplementedException();
    }
}
