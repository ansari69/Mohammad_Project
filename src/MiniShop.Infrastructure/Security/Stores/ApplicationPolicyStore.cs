using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Security.Stores
{
   public class ApplicationPolicyStore
    {

        public static class Base
        {
            // FullAccess
            public const string AdminPanel = "AdminPanel";

        }


        public static class Shared
        {
            public const string Operator = "Operator";
        }

        public static class SimpleUser
        {
            public const string Simple = "Simple";
        }
    }
}
