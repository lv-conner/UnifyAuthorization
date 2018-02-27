using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnifyAuthorization;

namespace UnifyAuthorizaiton.Test
{
    [TestClass]
    public class TestUnifyContext
    {
        [TestMethod]
        public void TestContextCreate()
        {
            using (var db = new UnifyContext())
            {
            }
        }
    }
}
