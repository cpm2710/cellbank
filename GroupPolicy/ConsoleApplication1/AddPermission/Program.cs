using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace AddPermission
{
    class Program
    {
//        strUser = "guests"
//strPath = "D:\\abc.txt"
//RetVal = AddPermission(strUser,strPath,"R",True)

//'-------------------------------------------------------------------------

//'用于给文件和文件夹添加一条权限设置.返回值: 0-成功,1-账户不存在,2-路径不存在
//'strUser表示用户名或组名
//'strPath表示文件夹路径或文件路径
//'strAccess表示允许权限设置的字符串,字符串中带有相应字母表示允许相应权限: R-读,C-读写,F-完全控制
//'blInherit表示是否继承父目录权限.True为继承,False为不继承

//Function AddPermission(strUser,strPath,strAccess,blInherit)
//        Set objWMIService = GetObject("winmgmts:\\.\root\Cimv2")
//        Set fso = CreateObject("Scripting.FileSystemObject")
//        '得到Win32_SID并判断用户/组/内置账户是否存在
//        Set colUsers = objWMIService.ExecQuery("SELECT * FROM Win32_Account WHERE Name='"&strUser&"'")
//        If colUsers.count<>0 Then
//                For Each objUser In colUsers
//                        strSID = objUser.SID
//                Next
//        Else
//                AddPermission = 1
//                Exit Function
//        End If
//        Set objSID = objWMIService.Get("Win32_SID.SID='"&strSID&"'")
//        '判断文件/文件夹是否存在
//        pathType = ""
//        If fso.fileExists(strPath) Then pathType = "FILE"
//        If fso.folderExists(strPath) Then pathType = "FOLDER"
//        If pathType = "" Then
//                AddPermission = 2
//                Exit Function
//        End If
//        '设置Trustee
//        Set objTrustee = objWMIService.Get("Win32_Trustee").SpawnInstance_()
//        objTrustee.Domain = objSID.ReferencedDomainName
//        objTrustee.Name = objSID.AccountName
//        objTrustee.SID = objSID.BinaryRepresentation
//        objTrustee.SidLength = objSID.SidLength
//        objTrustee.SIDString = objSID.Sid
//        '设置ACE
//        Set objNewACE = objWMIService.Get("Win32_ACE").SpawnInstance_()
//        objNewACE.Trustee = objTrustee
//        objNewACE.AceType = 0
//        If InStr(UCase(strAccess),"R") > 0 Then objNewACE.AccessMask = 1179817
//        If InStr(UCase(strAccess),"C") > 0 Then objNewACE.AccessMask = 1245631
//        If InStr(UCase(strAccess),"F") > 0 Then objNewACE.AccessMask = 2032127
//        If pathType = "FILE" And blInherit = True Then objNewACE.AceFlags = 16
//        If pathType = "FILE" And blInherit = False Then objNewACE.AceFlags = 0
//        If pathType = "FOLDER" And blInherit = True Then objNewACE.AceFlags = 19
//        If pathType = "FOLDER" And blInherit = False Then objNewACE.AceFlags = 3
//        '设置SD
//        Set objFileSecSetting = objWMIService.Get("Win32_LogicalFileSecuritySetting.Path='"&strPath&"'")
//        Call objFileSecSetting.GetSecurityDescriptor(objSD)
//        blSE_DACL_AUTO_INHERITED = True
//        If (objSD.ControlFlags And &H400) = 0 Then
//                blSE_DACL_AUTO_INHERITED = False
//                objSD.ControlFlags = (objSD.ControlFlags Or &H400)                '自动继承位置位,如果是刚创建的目录或文件该位是不置位的,需要置位
//        End If
//        If blInherit = True Then
//                objSD.ControlFlags = (objSD.ControlFlags And &HEFFF)        '阻止继承复位
//        Else
//                objSD.ControlFlags = (objSD.ControlFlags Or &H1400)                '阻止继承位置位,自动继承位置位
//        End If
//        objOldDacl = objSD.Dacl
//        ReDim objNewDacl(0)
//        Set objNewDacl(0) = objNewACE
//        If IsArray(objOldDacl) Then                '权限为空时objOldDacl不是集合不可遍历
//                For Each objACE In objOldDacl
//                        If (blSE_DACL_AUTO_INHERITED=False And blInherit=True) Or ((objACE.AceFlags And 16)>0 And (blInherit=True) Or (LCase(objACE.Trustee.Name)=LCase(strUser))) Then
//                                'Do nothing
//                                '当自动继承位置位为0时即使时继承的权限也会显示为非继承,这时所有权限都不设置
//                                '当自动继承位置位为0时,在继承父目录权限的情况下不设置继承的权限.账户和需要加权限的账户一样时不设置权限
//                        Else
//                                Ubd = UBound(objNewDacl)
//                                ReDim preserve objNewDacl(Ubd+1)
//                                Set objNewDacl(Ubd+1) = objACE
//                        End If
//                Next
//        End If
//        objSD.Dacl = objNewDacl
//        '提交设置修改
//        Call objFileSecSetting.SetSecurityDescriptor(objSD)
//        AddPermission = 0
//        Set fso = Nothing
//End Function
        static void Main(string[] args)
        {


            ConnectionOptions options = new ConnectionOptions();
            options.EnablePrivileges = true;
            options.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\cimv2", options);

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Account WHERE Name='" + "andyl_000" + "'");
            ManagementObjectSearcher searcher =
                                    new ManagementObjectSearcher(scope, query);
            ManagementObjectCollection queryCollection = searcher.Get();
            string sidToGet = string.Empty;
            ManagementObject accountInstance=null;
            foreach (ManagementObject m in queryCollection)
            {
                // access properties of the WMI object
                //Console.WriteLine("AccountName : {0}", m["AccountName"]);
                sidToGet = m["SID"].ToString();
                accountInstance = m;
            }

            //SELECT * FROM Win32_Account WHERE Name='"&strUser&"'
            ManagementObject sidInstance = new ManagementObject("Win32_SID.SID='" + sidToGet + "'");

            ManagementClass trustee = new ManagementClass("Win32_Trustee");
            ManagementObject objTrustee = trustee.CreateInstance();
            objTrustee["Domain"] = sidInstance["ReferencedDomainName"];//sidInstance["ReferencedDomainName"];
            objTrustee["Name"] = sidInstance["AccountName"];
            objTrustee["SID"] = sidInstance["BinaryRepresentation"];
            objTrustee["SidLength"] = sidInstance["SidLength"];
            objTrustee["SIDString"] = sidInstance["Sid"];

            ManagementClass ace = new ManagementClass("Win32_ACE");
            ManagementObject objNewACE = ace.CreateInstance();

            objNewACE["Trustee"] = objTrustee;
            objNewACE["AceType"] = 0;
            objNewACE["AccessMask"] = 2032127;
            objNewACE["AceFlags"] = 19;


            ManagementObject secDescriptor = new ManagementClass(new ManagementPath("Win32_SecurityDescriptor"), null);
            secDescriptor["ControlFlags"] = 4; //SE_DACL_PRESENT 
            secDescriptor["DACL"] = new object[] { objNewACE };


            ManagementObject share = new ManagementObject(@"\\.\root\cimv2:Win32_Share.Name='ShareFolder'");
            object invodeResult = share.InvokeMethod("SetShareInfo", new object[] { Int32.MaxValue, "This is John's share", secDescriptor });
            Console.WriteLine(invodeResult);
           //Set objNewACE = objWMIService.Get("Win32_ACE").SpawnInstance_()
//        objNewACE.Trustee = objTrustee
//        objNewACE.AceType = 0
//        If InStr(UCase(strAccess),"R") > 0 Then objNewACE.AccessMask = 1179817
//        If InStr(UCase(strAccess),"C") > 0 Then objNewACE.AccessMask = 1245631
//        If InStr(UCase(strAccess),"F") > 0 Then objNewACE.AccessMask = 2032127
//        If pathType = "FILE" And blInherit = True Then objNewACE.AceFlags = 16
//        If pathType = "FILE" And blInherit = False Then objNewACE.AceFlags = 0
//        If pathType = "FOLDER" And blInherit = True Then objNewACE.AceFlags = 19
//        If pathType = "FOLDER" And blInherit = False Then objNewACE.AceFlags = 3


            //classs
            //create object query
            //ObjectQuery query = new ObjectQuery("SELECT * FROM WIN32_SID");

            ////create object searcher
            //ManagementObjectSearcher searcher =
            //                        new ManagementObjectSearcher(scope, query);

            ////get collection of WMI objects
            //ManagementObjectCollection queryCollection = searcher.Get();

            ////enumerate the collection.
            //foreach (ManagementObject m in queryCollection)
            //{
            //    // access properties of the WMI object
            //    Console.WriteLine("AccountName : {0}", m["AccountName"]);

            //}
            Console.Read();
        }
    }
}
