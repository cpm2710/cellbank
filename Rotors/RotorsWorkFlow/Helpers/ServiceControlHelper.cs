﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public static class ServiceControlHelper
    {
        public static void ShutdownService(string service)
        {
            Logger.Log("shutting down service begins: {0}", service);
            string objPath = string.Format("Win32_Service.Name='{0}'", service);

            ConnectionOptions connectionOptions = new ConnectionOptions();
            connectionOptions.Authentication = AuthenticationLevel.Connect;
            connectionOptions.EnablePrivileges = true;
            connectionOptions.Username = Constants.UserName;
            connectionOptions.Password = Constants.PassWord;
            //connectionOptions.Impersonation = ImpersonationLevel.Impersonate; 

            ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", Constants.MachineName), connectionOptions);
            scope.Connect();
            using (ManagementObject serviceObj = new ManagementObject(scope, new ManagementPath(objPath), null))
            {
                try
                {
                    ManagementBaseObject outParams = serviceObj.InvokeMethod("StopService",
                        null, null);

                    uint returnValue = (uint)outParams["ReturnValue"];

                    Logger.Log("shutting result for service {0} is {1}", service, returnValue);
                }
                catch (Exception ex)
                {
                    
                        throw ex;
                }
            }
            //AlipaySecSvc
            Logger.Log("shutting down service ends: {0}", service);
        }
        public static void StartService(string service)
        {
            Logger.Log("starting up service begins: {0}", service);
            Logger.Log("starting up service ends: {0}", service);
        }
    }
}
