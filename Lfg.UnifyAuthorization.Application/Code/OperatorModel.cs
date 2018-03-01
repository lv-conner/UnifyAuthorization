using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Code
{
    public class OperatorModel
    {
        public Guid UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyNo { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsSystem { get; set; }
    }
}
