using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lfg.UnifyAuthorization.Application.Code
{
    public class DependencyResolver
    {
        public DependencyResolver()
        {

        }
        private static IServiceProvider _current;
        public static IServiceProvider Current
        {
            get
            {
                if(_current == null)
                {

                }
                return _current;
            }
        }
        public static void SetResolver(IServiceProvider resolver)
        {
            _current = resolver;
        }
        private static void SetDefaultResolver()
        {

        }
    }
}
