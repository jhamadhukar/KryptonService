using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using KryptonService;
using System.Threading;
using System.IO;

namespace KryptonHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(WCFServices));
            
                host.Open();
                foreach (ServiceEndpoint se in host.Description.Endpoints)
                {
                    Console.WriteLine("Address : {0}, Binding : {1}, Contract : {2}", se.Address, se.Binding, se.Contract.ContractType);
                }
                Console.WriteLine("               ");
                Console.WriteLine("               ");
                Console.WriteLine("               ");
                Console.WriteLine("Service Started");
                Console.ReadLine();

                host.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}