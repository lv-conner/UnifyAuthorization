using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnifyAuthorization.Base;

namespace UnifyAuthorization.Domain
{
    public class AggregateRoot<TId> : IAggregateRoot
    {
        public TId Id { get; protected set; }
        string IAggregateRoot.Id
        {
            get
            {
                if(Id == null)
                {
                    return null;
                }
                else
                {
                    return Id.ToString();
                }
            }
        }
        public byte[] RowVersion { get; protected set; }
    }
}
