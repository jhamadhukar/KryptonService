﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KryptonController
{
    class KryptonServiceProxy : DuplexClientBase<IWCFServices>, IWCFServices
    {
        public KryptonServiceProxy(InstanceContext inputContext, NetTcpBinding wsDualHttpBinding, string endPointAddress)
            : base(inputContext, wsDualHttpBinding, new EndpointAddress(endPointAddress))
        {
        }

        public void AddRecord(string message)
        {
            this.Channel.AddRecord(message);
        }
    }
}
