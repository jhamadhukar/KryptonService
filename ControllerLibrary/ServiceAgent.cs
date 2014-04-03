using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO.Compression;
using System.IO;

namespace ControllerLibrary
{
    public class ServiceAgent: IWCFServices, ICompletionCallback, IDisposable
    {
        private KryptonServiceProxy _proxy;
        private CallBackFunctionSignature _callback;
        string testCaseId = "", browser = "", ipAddress = "", Url = "";
        private void InitializeProxy(string IpAddress, CallBackFunctionSignature callback)
        {
            try
            {
                 Url = string.Format("net.tcp://{0}:8001/KryptonService", IpAddress);
                 _callback = callback;
          
                 InstanceContext context = new InstanceContext(this);
                 NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
                 binding.MaxReceivedMessageSize = 2147483647;
                 binding.ReaderQuotas.MaxDepth= 2147483647;
                 binding.ReaderQuotas.MaxStringContentLength=2147483647;
                 binding.ReaderQuotas.MaxArrayLength= 2147483647;
                 binding.ReaderQuotas.MaxBytesPerRead= 2147483647;
                 binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                 binding.OpenTimeout = TimeSpan.FromMinutes(10);
                 binding.SendTimeout = TimeSpan.FromMinutes(20);
                 binding.ReceiveTimeout = TimeSpan.FromMinutes(20);
                 binding.MaxBufferPoolSize = 2147483647;

                _proxy = new KryptonServiceProxy(context, binding, Url);
                _proxy.Open();
            }
           catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _proxy = null;
            }
        }
        public void ExecuteCommand(string command)
        {
            if (_proxy != null)
                _proxy.ExecuteCommand(command);
           
        }
        public void ExecuteCommand(string IpAddress, string command, CallBackFunctionSignature callback)
        {
            InitializeProxy(IpAddress, callback);
            ExecuteCommand(command);
        }
        public void Dispose()
        {
            if (_proxy != null)
                _proxy.Close();
        }
        public void CallBackFunction(string completionFlag)
        {
            EndpointAddress clientAddress = OperationContext.Current.Channel.RemoteAddress;

           if(_callback != null)
               _callback(completionFlag, clientAddress.Uri.Host);

           Dispose();
        }
    }
}