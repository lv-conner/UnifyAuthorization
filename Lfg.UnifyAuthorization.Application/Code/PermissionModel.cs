using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Code
{
    public class PermissionModel
    {
        public Guid PermissionId { get; set; }
        public string ActionNo { get; set; }
        public string ModuleNo { get; set; }
        public string SystemName { get; set; }
    }
}
