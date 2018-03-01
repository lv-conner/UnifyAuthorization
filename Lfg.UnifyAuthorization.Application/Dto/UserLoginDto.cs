using System;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class UserLoginDto
    {
        public string UserNo { get; set; }
        public string Password { get; set; }

        public string Code { get; set; }
    }
}
