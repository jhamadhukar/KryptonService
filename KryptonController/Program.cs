using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControllerLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string command ="C:\\Krypton\\Krypton.exe \"Browser=chrome\" \"TestCaseId=Demo.0001\"\r\n exit";
            string ip = "localhost";
            CallBackFunctionSignature callback = new CallBackFunctionSignature(CallBackFunction);
            ServiceAgent agent = new ServiceAgent();
            agent.ExecuteCommand(ip, command, callback);
           
            Console.WriteLine("Press any key to stop execution...");
            Console.ReadLine();
        }

        public static void CallBackFunction(string completionFlag, string returnAddress)
        {

        }
    }
}