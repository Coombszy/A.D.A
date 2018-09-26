using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.D.A_Host
{
    class UserInterpreter
    {
        private MemoryHandler MemoryUnit;
        private List<string> EventDicitionary;
        public UserInterpreter(MemoryHandler activeMemoryHandler)
        {
            this.MemoryUnit = activeMemoryHandler;
        }
        public void buildEventDictionary()
        {
            
        }
    }
}
