using System;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class UserModifPwdDto
    {
        public Guid Id { get; set; }
        public string Password { get; set; }

        public string OriginalPassword { get; set; }
    }
}
