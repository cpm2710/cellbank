using RotorsWorkFlow.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
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
            //string administrativeFullName = ConstructAdministrativeDestination(fileItem.DestinationFullName);
            Logger.Log("replacing file begins: source: {0} destination: {1}", fileItem.SourceFullName, fileItem.DestinationFullName);
            try
            {
                //NetworkCredential networkCredential = new NetworkCredential(Constants.UserName, Constants.PassWord, Constants.Domain);
                //string sharePath = administrativeFullName.Substring(0, administrativeFullName.LastIndexOf("\\"));
                //using (NetworkConnection nc = new NetworkConnection(sharePath, networkCredential))
                //{
                if(!File.Exists(fileItem.DestinationFullName + ".bak"))
                    {
                        File.Copy(fileItem.DestinationFullName, fileItem.DestinationFullName + ".bak");
                    }
                File.Copy(fileItem.SourceFullName, fileItem.DestinationFullName, true);
                //}
            }
            catch (Exception e)
            {
                Logger.Error("exception encounterred: {0}", e);
            }
            Logger.Log("replacing file ends: source: {0} destination: {1}", fileItem.SourceFullName, fileItem.DestinationFullName);
        }

        [Flags]
        public enum AccessPrivileges : uint
        {
            FILE_READ_DATA = 0x00000001,
            FILE_WRITE_DATA = 0x00000002,
            FILE_APPEND_DATA = 0x00000004,
            FILE_READ_EA = 0x00000008,
            FILE_WRITE_EA = 0x00000010,
            FILE_EXECUTE = 0x00000020,
            FILE_DELETE_CHILD = 0x00000040,
            FILE_READ_ATTRIBUTES = 0x00000080,
            FILE_WRITE_ATTRIBUTES = 0x00000100,

            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000,

            ACCESS_SYSTEM_SECURITY = 0x01000000,
            MAXIMUM_ALLOWED = 0x02000000,

            GENERIC_ALL = 0x10000000,
            GENERIC_EXECUTE = 0x20000000,
            GENERIC_WRITE = 0x40000000,
            GENERIC_READ = 0x80000000
        }


        [Flags]
        enum AceFlags : uint
        {
            NonInheritAce = 0,
            ObjectInheritAce = 1,
            ContainerInheritAce = 2,
            NoPropagateInheritAce = 4,
            InheritOnlyAce = 8,
            InheritedAce = 16
        }

        [Flags]
        enum AceType : uint
        {
            AccessAllowed = 0,
            AccessDenied = 1,
            Audit = 2
        }
        /// <summary>
        /// the fileItem.DestinationFullName is like this \\computername\c$\aaa\xx.dll
        /// </summary>
        /// <param name="fileItem"></param>
        public static void TakeOwnership(FileItem fileItem)
        {
            Logger.Log("taking ownership of file begins: destination: {0} ", fileItem.DestinationFullName);

            string localFormatPath = @"C:" + fileItem.DestinationFullName.Substring(fileItem.DestinationFullName.IndexOf("c$", StringComparison.OrdinalIgnoreCase) + 2);

            string objPath = string.Format("CIM_DataFile.Name='{0}'", localFormatPath);

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
                    Logger.Log("take ownership for file: {0} result: {1}", localFormatPath, returnValue);




                    ManagementClass trustee = new ManagementClass("Win32_Trustee");
                    trustee.Properties["Name"].Value = "Everyone";
                    trustee.Properties["Domain"].Value = null;
                    trustee.Properties["SID"].Value = new byte[] { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 };

                    //设置只读/运行权限
                    ManagementClass ace = new ManagementClass("Win32_ACE");
                    ace.Properties["AccessMask"].Value = AccessPrivileges.GENERIC_ALL
                     | AccessPrivileges.FILE_READ_DATA | AccessPrivileges.FILE_READ_ATTRIBUTES | AccessPrivileges.FILE_READ_EA
                     | AccessPrivileges.READ_CONTROL | AccessPrivileges.FILE_EXECUTE;
                    ace.Properties["AceFlags"].Value = 3;//AceFlags.ObjectInheritAce | AceFlags.ContainerInheritAce ;
                    ace.Properties["AceType"].Value = 0;//AceType.AccessAllowed;
                    ace.Properties["Trustee"].Value = trustee;

                    //修改ACL设置
                    ManagementObject secDescriptor = new ManagementClass("Win32_SecurityDescriptor");
                    secDescriptor["ControlFlags"] = 4;
                    secDescriptor["DACL"] = new ManagementObject[] { ace };

                    ManagementBaseObject inParams = serviceObj.GetMethodParameters("ChangeSecurityPermissions");

                    inParams["Option"] = 4;
                    inParams["SecurityDescriptor"] = secDescriptor;

                    ManagementBaseObject outParamsOfChangingPermission = serviceObj.InvokeMethod("ChangeSecurityPermissions", inParams, null);
                    uint returnValueOfChangingPermission = (uint)outParamsOfChangingPermission["ReturnValue"];
                    Logger.Log("changing permission for file: {0} result: {1}", localFormatPath, returnValueOfChangingPermission);
                }
                catch (Exception ex)
                {
                    Logger.Error("could not take ownership of file {0} , {1}", localFormatPath, ex);
                    //throw ex;
                }
            }


            Logger.Log("taking ownership of file ends: destination: {0}", localFormatPath);
        }

    }
}
