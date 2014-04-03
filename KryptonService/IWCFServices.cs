using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace KryptonService
{
    [ServiceContract(Name = "KryptonServiceContract", CallbackContract = typeof(ICompletionCallback))]
    public interface IWCFServices
    {
        [OperationContract(IsOneWay = true)]
        void ExecuteCommand(string command);
    }
}