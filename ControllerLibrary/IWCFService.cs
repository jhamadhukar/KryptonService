﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLibrary
{
    [ServiceContract(Name = "KryptonServiceContract", CallbackContract = typeof(ICompletionCallback))]


    public interface IWCFServices
    {
        [OperationContract(IsOneWay = true)]
        void ExecuteCommand(string command);
    }
}
