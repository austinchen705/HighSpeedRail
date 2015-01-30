using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            //var key = Console.ReadLine();
            //switch (key) 
            //{ 
            //    case "1":
            //        System.Diagnostics.Process.Start("iexplore.exe", "http://tw.yahoo.com");
            //        break;
            //    default:
            //        break;
            //}
            Connect();
            Shutdown();

            //string computerName = "127.0.0.1";

            //try
            //{
            //    ConnectionOptions options = new ConnectionOptions();
            //    options.EnablePrivileges = true;
            //    // To connect to the remote computer using a different account, specify these values:
            //    //options.Username = "user1";
            //    //options.Password = "abc123";
            //    // options.Authority = "ntlmdomain:DOMAIN";

            //    ManagementScope scope = new ManagementScope(
            //      "\\\\" + computerName + "\\root\\CIMV2", options);
            //    scope.Connect();

            //    SelectQuery query = new SelectQuery("Win32_OperatingSystem");
            //    ManagementObjectSearcher searcher =
            //        new ManagementObjectSearcher(scope, query);

            //    foreach (ManagementObject os in searcher.Get())
            //    {
            //        // Obtain in-parameters for the method
            //        ManagementBaseObject inParams =
            //            os.GetMethodParameters("Win32Shutdown");

            //        // Add the input parameters.
            //        inParams["Flags"] = "1";
            //        inParams["Reserved"] = "0";

            //        // Execute the method and obtain the return values.
            //        ManagementBaseObject outParams =
            //            os.InvokeMethod("Win32Shutdown", inParams, null);
            //    }
            //}
            //catch (ManagementException err)
            //{
            //    Console.WriteLine("An error occurred while trying to execute the WMI method: " + err.Message);
            //}
            //catch (System.UnauthorizedAccessException unauthorizedErr)
            //{
            //    Console.WriteLine("Connection error (user name or password might be incorrect): " + unauthorizedErr.Message);
            //}
        }

        static void Connect()
        {
            var result = Process.Start("cmd.exe", "/C" + @"net use \\192.168.0.103\IPC$ user1 /USER:user1");
            result.WaitForExit();
        }

        static void Shutdown()
        {



                        Process process1 = new Process();
            string path = @" C:\Windows\System32";
            string shut_args = " -a -m 192.168.0.103 ";

            process1.StartInfo.FileName = @"C:\Windows\System32\shutdown.exe";

            // choose appropriate arguments from the section ['Shutdown Arguments explained']
            // and store the value in shut_args variable
            // Fill the shutdown argument 'shut_args' yourselves.

            process1.StartInfo.Arguments = shut_args;

            process1.StartInfo.CreateNoWindow = true;
            process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            var result = process1.Start();
            Console.WriteLine(result);

            process1.WaitForExit(10000);
            if (!process1.HasExited)
                process1.Kill();
            process1.Close();
        }
    }
}
