namespace NServiceBus.Setup.Windows
{
    using System;
    using System.Security.Principal;
    using System.ServiceProcess;
    using System.ComponentModel;
    using System.Diagnostics;

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

        public static Process StartProccessDelegate(string fileName, string arguments)
        {
            Process process;

            using(process = new Process())
            {
                var processInfo = new ProcessStartInfo(fileName, arguments)
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false,
                    LoadUserProfile = true,
                    UseShellExecute = false
                };

                process.StartInfo = processInfo;
                
                process.Start();
                
                var errStreamReader = process.StandardError;                
                
                var result = string.Empty;

                string standardError = errStreamReader.ReadToEnd();

                Console.WriteLine("Waiting for process to complete.");

                process.WaitForExit();


                if (standardError != string.Empty)
                {
                    Console.WriteLine("Error invoking process: {0} with arguments: {1}, the process returnd an error: {2}, ExitCode was: {3}", fileName, arguments, standardError, process.ExitCode.ToString());
                    
                    return null;
                }

                // not sure we need a separate case for this...
                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Error invoking process: {0} with arguments: {1}, the process returnd an error: {2}, ExitCode was: {3}", fileName, arguments, standardError, process.ExitCode.ToString());

                    return null;
                }
            }
            
            return process;
        }

        public static bool  StartProccess(string fileName, string arguments)
        {
            Process process;

            bool result = true;

            using(process = new Process())
            {
                var processInfo = new ProcessStartInfo(fileName, arguments)
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,                   
                    CreateNoWindow = false,
                    LoadUserProfile = true,
                    UseShellExecute = false
                };

                process.StartInfo = processInfo;

                process.EnableRaisingEvents = true;
                
                process.Start();
                
                var errStreamReader = process.StandardError;                
                
                string standardError = errStreamReader.ReadToEnd();

                Console.WriteLine("Waiting for process to complete.");

                process.WaitForExit();

                if (standardError != string.Empty)
                {
                    result = false;

                    Console.WriteLine("Error invoking process: {0} with arguments: {1}, the process returnd an error: {2}", fileName, arguments, standardError);
                }
            }
            
            return result;
        }
    }
}
