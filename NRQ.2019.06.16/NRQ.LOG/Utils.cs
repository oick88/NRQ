using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NRQ.LOG
{
    class Utils
    {
        public static string GetProperyName(string methodName)

        {

            if (methodName.StartsWith("get_") || methodName.StartsWith("set_") ||

                methodName.StartsWith("put_"))

            {

                return methodName.Substring("get_".Length);

            }

            throw new Exception(methodName + " not a method of Property");

        }
    }
}
