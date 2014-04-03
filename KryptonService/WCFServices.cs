using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.IO.Compression;
using System.Diagnostics;
using System.Configuration;

namespace KryptonService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class WCFServices: IWCFServices
    {
    
        public void CopyProjectData(byte[] fileContent, string destinationFolder)
        {

        }
        public void ExecuteCommand(string command)
        {
            string exitMessage = "";
            int iCount = 0;
            try
            {
                FileStream fs1 = new FileStream("command.bat", FileMode.Create, FileAccess.Write);
                StreamWriter writer = new StreamWriter(fs1);
                writer.Write(command);
                writer.Close();
                fs1.Close();
                fs1.Dispose();
                
                ProcessStartInfo processStartInfo = new ProcessStartInfo("command.bat");
                Process _process = new Process();
                _process.StartInfo = processStartInfo;
                _process.StartInfo.RedirectStandardOutput = true;
                _process.StartInfo.UseShellExecute = false;
                _process.Start();
                _process.WaitForExit();

                System.IO.StreamReader SR = _process.StandardOutput;
                exitMessage = SR.ReadToEnd();
                

                SR.Close();

                foreach (var item in exitMessage.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (iCount == 1)
                    {
                        if (item.StartsWith("-------------------------------"))
                        {
                            exitMessage = "";
                            break;
                        }
                    }
                    iCount++;
                }
                //exitCode = _process.ExitCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);        
            }

            //if (exitCode != 0)
            //    exitMessage = "Command executed successfully ";
            //else
            //    exitMessage = "Command exited with exit code " + exitCode;
            ICompletionCallback callback = OperationContext.Current.GetCallbackChannel<ICompletionCallback>();
            callback.CallBackFunction(exitMessage);
        }
    }
}