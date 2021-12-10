using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class ChangedData
    {
        public Data OldData { get; private set; }

        public Data NewData { get; private set; }

        public ChangedData(Data oldData, Data newData)
        {
            OldData = oldData;
            NewData = newData;
        }
    }
}
