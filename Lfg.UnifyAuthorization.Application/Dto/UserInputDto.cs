using System;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class UserInputDto
    {
        public Guid? Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Department { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 如果修改用户资料时同时修改密码（Password不为空），需要提供原始密码，原始密码不正确则会修改失败。
        /// </summary>
        public string PasswordOriginal { get; set; }
        public string Duty { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
    }
}
