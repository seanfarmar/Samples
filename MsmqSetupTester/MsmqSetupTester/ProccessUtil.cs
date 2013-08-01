namespace NServiceBus.Setup.Windows
{
    using System;
    using System.Security.Principal;
    using System.ServiceProcess;
    using System.ComponentModel;
    using System.Security;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Utility class for changing a windows service's status.
    /// </summary>
    public static class ProcessUtil
    {
        public static bool IsRunningWithElevatedPrivileges()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Checks the status of the given controller, and if it isn't the requested state,
        /// performs the given action, and checks the state again.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="status"></param>
        /// <param name="changeStatus"></param>
        public static void ChangeServiceStatus(ServiceController controller, ServiceControllerStatus status, Action changeStatus)
        {
            if (controller.Status == status)
            {
                Console.Out.WriteLine(controller.ServiceName + " status is good: " + Enum.GetName(typeof(ServiceControllerStatus), status));
                return;
            }

            Console.Out.WriteLine((controller.ServiceName + " status is NOT " + Enum.GetName(typeof(ServiceControllerStatus), status) + ". Changing status..."));

            try
            {
                changeStatus();
            }
            catch (Win32Exception exception)
            {
                ThrowUnableToChangeStatus(controller.ServiceName, status, exception);
            }
            catch (InvalidOperationException exception)
            {
                ThrowUnableToChangeStatus(controller.ServiceName, status, exception);
            }

            var timeout = TimeSpan.FromSeconds(3);
            controller.WaitForStatus(status, timeout);
            if (controller.Status == status)
                Console.Out.WriteLine((controller.ServiceName + " status changed successfully."));
            else
                ThrowUnableToChangeStatus(controller.ServiceName, status);
        }

        private static void ThrowUnableToChangeStatus(string serviceName, ServiceControllerStatus status)
        {
            ThrowUnableToChangeStatus(serviceName, status, null);
        }

        private static void ThrowUnableToChangeStatus(string serviceName, ServiceControllerStatus status, Exception exception)
        {
            string message = "Unable to change " + serviceName + " status to " + Enum.GetName(typeof(ServiceControllerStatus), status);

            if (exception == null)
            {
                throw new InvalidOperationException(message);
            }

            throw new InvalidOperationException(message, exception);
        }

        public static Process  startProccess(string fileName, string arguments)
        {
            Process process = null;

            using(process = new Process())
            {
                
                StreamWriter streamWriter;
                StreamReader streamReader;
                StreamReader errStreamReader;

                ProcessStartInfo processInfo = new ProcessStartInfo(fileName, arguments);                
                processInfo.RedirectStandardInput = true;
                processInfo.RedirectStandardOutput = true;
                processInfo.RedirectStandardError = true;
                processInfo.CreateNoWindow = false;
                processInfo.LoadUserProfile = true;
                processInfo.UseShellExecute = false;          
                
                process.StartInfo = processInfo;
                
                process.Start();
                
                streamWriter = process.StandardInput;                
                
                streamReader = process.StandardOutput;                
                
                errStreamReader = process.StandardError;                
                
                streamWriter.AutoFlush = true;              
                
                var result = string.Empty;
                
                var erroMessage = string.Empty;
                
                streamWriter.Write(result);
                
                streamWriter.Close();
                
                result += streamReader.ReadToEnd();
                
                erroMessage = errStreamReader.ReadToEnd();
                
                if (erroMessage != string.Empty) 
                    throw new InvalidOperationException(
                        string.Format("Error invoking process: {0} with arguments: {1}, the process returnd an error: {2}", fileName, arguments, erroMessage));
            }
            
            return process;
        }
    }
}
