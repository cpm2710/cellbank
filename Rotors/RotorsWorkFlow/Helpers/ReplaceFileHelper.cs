﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RotorsWorkFlow
{
    public static class ReplaceFileHelper
    {
        /// <summary>
        /// destinationFullName is like c:\abc\xx.dll
        /// </summary>
        /// <param name="destinationFullName"></param>
        private static string ConstructAdministrativeDestination(string destinationFullName)
        {
            return string.Format(@"\\{0}\{1}", Constants.MachineName, destinationFullName.Replace(":", "$"));
        }

        public static void ReplaceFile(FileItem fileItem)
        {
            string administrativeFullName = ConstructAdministrativeDestination(fileItem.DestinationFullName);
            Logger.Log("replacing file begins: source: {0} destination: {1}", fileItem.SourceFullName, administrativeFullName);
            try
            {
                File.Replace(fileItem.SourceFullName, administrativeFullName, administrativeFullName + ".bak");
            }
            catch (Exception e)
            {
                Logger.Log("exception encounterred: {0}", e);
            }
            Logger.Log("replacing file ends: source: {0} destination: {1}", fileItem.SourceFullName, administrativeFullName);
        }

        /// <summary>
        /// the fileItem.DestinationFullName should be like c:\aaa\xx.dll
        /// </summary>
        /// <param name="fileItem"></param>
        public static void TakeOwnership(FileItem fileItem)
        {
            Logger.Log("taking ownership of file begins: destination: {0} ", fileItem.DestinationFullName);

            string objPath = string.Format("CIM_DataFile.Name='{0}'", fileItem.DestinationFullName);

            ConnectionOptions connectionOptions = new ConnectionOptions();
            connectionOptions.Authentication = AuthenticationLevel.Packet;
            connectionOptions.EnablePrivileges = true;
            connectionOptions.Username = Constants.UserName;
            connectionOptions.Password = Constants.PassWord;
            connectionOptions.Impersonation = ImpersonationLevel.Impersonate;

            ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", Constants.MachineName), connectionOptions);
            scope.Connect();
            using (ManagementObject serviceObj = new ManagementObject(scope, new ManagementPath(objPath), null))
            {
                try
                {
                    ManagementBaseObject outParams = serviceObj.InvokeMethod("TakeOwnerShip", null, null);


                    uint returnValue = (uint)outParams["ReturnValue"];
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }


            Logger.Log("taking ownership of file ends: destination: {0}", fileItem.DestinationFullName);
        }

    }
}
