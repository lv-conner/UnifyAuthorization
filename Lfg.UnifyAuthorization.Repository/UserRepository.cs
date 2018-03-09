using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfg.UnifyAuthorization.Domain;
using Lfg.UnifyAuthorization.Repository.Interface;
using Lfg.UnifyAuthorization.Repository;

namespace Lfg.UnifyAuthorization.Repository
{
    public class UserRepository:BaseRepository<User>
    {
        public UserRepository()
        {
            DbContext = new UnifyContext();
        }
    }
}
