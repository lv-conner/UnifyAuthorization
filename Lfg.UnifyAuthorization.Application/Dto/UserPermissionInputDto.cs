using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class UserPermissionInputDto
    {
        public Guid UserId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
