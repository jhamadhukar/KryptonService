using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO.Compression;
using System.IO;

namespace KryptonController
{
    public class ServiceAgent: IWCFServices, ICompletionCallback, IDisposable
    {
        private KryptonServiceProxy _proxy;
       
        public ServiceAgent(string Url)
        {
            try
            {
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

        public void AddRecord(string message)
        {
            if (_proxy != null)
                _proxy.AddRecord(message);
        }

        public void Dispose()
        {
            if (_proxy != null)
                _proxy.Close();
        }

        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }

        byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        public byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }


       public void CallBackFunction(byte[] fileContent, string fileName)
        {
            EndpointAddress clientAddress = OperationContext.Current.Channel.RemoteAddress;
            Console.WriteLine(clientAddress.Uri);
            string foldername = clientAddress.Uri.Host;
            if (!Directory.Exists(foldername))
                Directory.CreateDirectory("E:\\" + foldername);
            ByteArrayToFile("E:\\" + foldername + "\\" + fileName, Decompress(fileContent));
        }
    }
}