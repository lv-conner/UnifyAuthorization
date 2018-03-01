using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class FunctionInputDto
    {
        public Guid? Id { get; set; }
        public string SystemNo { get;  set; }
        public string ModuleNo { get;  set; }
        public string ModuleName { get;  set; }
        public string ActionNo { get;  set; }
        public string ActionName { get;  set; }
    }
}
