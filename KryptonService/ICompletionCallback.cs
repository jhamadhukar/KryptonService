﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KryptonService
{
    interface ICompletionCallback
    {
        [OperationContract(IsOneWay=true)]
        void CallBackFunction(string completionFlag); 
    }
}