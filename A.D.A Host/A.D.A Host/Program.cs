using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.D.A_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadingBar Test = new LoadingBar(1000, 0);
            while(!Test.Finished())
            {
                Test.DrawIncrement(1);
            }
        }
    }
}
