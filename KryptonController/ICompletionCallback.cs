using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KryptonController
{
    interface ICompletionCallback
    {
        [OperationContract(IsOneWay = true)]
        void CallBackFunction(byte[] fileContent, string fileName);
        //void CallBackFunction(string str); 
    }
}
