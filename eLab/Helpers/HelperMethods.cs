using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Helpers
{
    public class HelperMethods
    {
        public String GetPercent(int num1, int num2)
        {
            double percent = (num1 * 1.0) / (num2 * 1.0);
            return (percent * 100).ToString().Substring(0,5) + "%";
        }
    }
}
