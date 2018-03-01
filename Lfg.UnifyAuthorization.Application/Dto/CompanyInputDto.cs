using System;

namespace Lfg.UnifyAuthorization.Application.Dto
{
    public class CompanyInputDto
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Remark { get; set; }
        public Guid? Administrator { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNo { get; set; }
    }
}
