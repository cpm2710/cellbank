using Microsoft.GroupPolicy;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GroupPlicyUtil
    {
        private const string fdeployIniFileName = "fdeploy.ini";
        private const string fdeploy1IniFileName = "fdeploy1.ini";
        private const UInt32 RegFileSignature = 0x67655250;
        private const UInt32 RegFileVersion = 0x00000001;
        private const string RegKeyBase = "Software\\Policies\\Microsoft";
        private const string SidEveryone = "S-1-1-0";

        public static void CreateFileGroupPolicy(Guid gpoGuid,bool computer, FilesFile file)
        {
            if (gpoGuid == null)
            {
                throw new ArgumentNullException("gpoGuid");
            }
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }
            string filePath = GetFilesPolicyFilePath(gpoGuid, computer);
            Files filePolicies = XmlSerializeHelper.XmlDeserializeFromFile<Files>(filePath);
            FilesFile[] files = filePolicies.File;
            if (files != null)
            {
                List<FilesFile> current = new List<FilesFile>(files);
                current.Add(file);
                filePolicies.File = current.ToArray();
            }
            else
            {
                List<FilesFile> current = new List<FilesFile>();
                current.Add(file);
                filePolicies.File = current.ToArray();
            }
            XmlSerializeHelper.XmlSerializeToFile<Files>(filePolicies, filePath);
        }
        public static void CreateShortcutGroupPolicy(Guid gpoGuid, bool computer, ShortcutsShortcut shortcut)
        {
            if (gpoGuid == null)
            {
                throw new ArgumentNullException("gpoGuid");
            }
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }
            string filePath = GetShortcutsPolicyFilePath(gpoGuid, computer);
            Shortcuts shortcuts = XmlSerializeHelper.XmlDeserializeFromFile<Shortcuts>(filePath);
            ShortcutsShortcut[] shotcutsArray = shortcuts.Shortcut;
            if (shotcutsArray != null)
            {
                List<ShortcutsShortcut> current = new List<ShortcutsShortcut>(shotcutsArray);
                current.Add(shortcut);
                shortcuts.Shortcut = current.ToArray();
            }
            else
            {
                List<ShortcutsShortcut> current = new List<ShortcutsShortcut>();
                current.Add(shortcut);
                shortcuts.Shortcut = current.ToArray();
            }
            XmlSerializeHelper.XmlSerializeToFile<Shortcuts>(shortcuts, filePath);
        }

        public static string GetFilesPolicyFilePath(Guid gpoGuid,bool computer)
        {
            return GetExistingGPOPath(gpoGuid, computer) + @"\Preferences\Files\Files.xml";
        }

        public static string GetShortcutsPolicyFilePath(Guid gpoGuid, bool computer)
        {
            return GetExistingGPOPath(gpoGuid, computer) + @"\Preferences\Shortcuts\Shortcuts.xml";
        }

        private static string GetExistingGPOPath(Guid gpoGuid, bool computer)
        {
            Domain domain = Domain.GetCurrentDomain();
            using (GroupPolicyObject existGPO = new GroupPolicyObject())
            {
                try
                {
                    existGPO.OpenDSGpo(domain, gpoGuid, false, false);
                }
                catch (ActiveDirectoryObjectNotFoundException ex)
                {
                    //Tracer.WriteInformation(ex.ToString());
                    return String.Empty;
                }

                if (computer)
                {
                    return existGPO.GetFileSystemPath(GpoSection.Computer);
                }
                else
                {
                    return existGPO.GetFileSystemPath(GpoSection.User);
                }
            }            
        }

        [DllImport("dbgHelp", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool MakeSureDirectoryPathExists(string name);


        public static void SetSecurityTemplatePolicy(Guid gpoGuid, bool enable, Dictionary<string, string> defaultPolicy)
        {
            string gpoPath = GetExistingGPOPath(gpoGuid, true);
            string regfile = Path.Combine(gpoPath, "registry.pol");

            int lastError = 0;
            if (File.Exists(regfile) == false)
            {
                bool successMakeSureDirectoryPathExists = true;
                if (!Directory.Exists(gpoPath))
                {
                    successMakeSureDirectoryPathExists = MakeSureDirectoryPathExists(gpoPath);
                }
                lastError = Marshal.GetLastWin32Error();
                if (successMakeSureDirectoryPathExists)
                {
                    using (FileStream stream = new FileStream(regfile, FileMode.CreateNew))
                    using (BinaryWriter binwriter = new BinaryWriter(stream))
                    {
                        binwriter.Write(RegFileSignature);
                        binwriter.Write(RegFileVersion);
                        binwriter.Flush();
                        binwriter.Close();
                        stream.Close();
                    }
                }
                else
                {
                    throw new MultipleGroupPolicyObjectsFoundException(String.Format(CultureInfo.InvariantCulture, "registry.pol folder could not create with error code:{0:X}, make sure your AD is in correct status.", lastError));
                }
            }
            using (RegistryPolicy regpol = new RegistryPolicy(Domain.GetCurrentDomain(), gpoGuid, false))
            {
                string key, name;
                UInt32 value;
                if (enable == true)
                {
                    foreach (KeyValuePair<string, string> p in defaultPolicy)
                    {
                        // RegKeyBase = "Software\\Policies\\Microsoft"
                        string temp = p.Key;
                        key = Path.Combine(RegKeyBase, temp.Substring(0, temp.LastIndexOf('\\')));
                        name = temp.Substring(temp.LastIndexOf('\\') + 1);
                        value = UInt32.Parse(p.Value, System.Globalization.NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
                        // sample
                        // key = "Software\\Policies\\Microsoft\\Windows Defender\\Scan";
                        // name = "CheckForSignaturesBeforeRunningScan";
                        regpol.WriteDWordValue(RegistryHive.LocalMachine, key, name, value);                        
                    }
                }
                else
                {
                    if (defaultPolicy.Count > 0)
                    {
                        // RegKeyBase = "Software\\Policies\\Microsoft"
                        foreach (KeyValuePair<string, string> p in defaultPolicy)
                        {
                            // RegKeyBase = "Software\\Policies\\Microsoft"
                            string temp = p.Key;
                            key = Path.Combine(RegKeyBase, temp.Substring(0, temp.LastIndexOf('\\')));
                            name = temp.Substring(temp.LastIndexOf('\\') + 1);
                            // sample
                            // key = "Software\\Policies\\Microsoft\\Windows Defender\\Scan";
                            // name = "CheckForSignaturesBeforeRunningScan";
                            Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Delete(HKLM, \"{0}\", \"{1}\")", key, name));
                            try
                            {
                                regpol.Delete(RegistryHive.LocalMachine, key, name);
                            }
                            catch (ObjectDisposedException ex)
                            {
                                // some error, but not care
                                Console.WriteLine("RegistryPolicy.Delete() failed, but ignore: " + ex.Message);
                            }
                            catch (InvalidOperationException ex)
                            {
                                // some error, but not care
                                Console.WriteLine("RegistryPolicy.Delete() failed, but ignore: " + ex.Message);
                            }
                            catch (ArgumentNullException ex)
                            {
                                // some error, but not care
                                Console.WriteLine("RegistryPolicy.Delete() failed, but ignore: " + ex.Message);
                            }
                            catch (ArgumentException ex)
                            {
                                // some error, but not care
                                Console.WriteLine("RegistryPolicy.Delete() failed, but ignore: " + ex.Message);
                            }
                        }
                    }
                }
                Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Registry policy file is saved in \"{0}\".", regfile));
                regpol.Save(true);
            }

        }


        public static void SetFolderRedirectionPolicy(Guid guidGPO, Guid guidFolder, string nameFolder, int flags, string share)
        {
            string pathGPOUser = GetExistingGPOPath(guidGPO, false);
            if (pathGPOUser == String.Empty)
            {
                // error
                Console.WriteLine("Could not get existing GPO pasth.");
                return;
            }

            // Generate folder path for Ini files
            string pathIniFolder = Path.Combine(pathGPOUser, "Documents & Settings");
            if (Directory.Exists(pathIniFolder) == false)
            {
                Console.WriteLine(String.Format(CultureInfo.InvariantCulture, "Ini folder: \"{0}\" did not exist, therefore create one.", pathIniFolder));
                Directory.CreateDirectory(pathIniFolder);
            }

            // Generate Ini files
            string fdeployPath = Path.Combine(pathIniFolder, fdeployIniFileName);
            string fdeploy1Path = Path.Combine(pathIniFolder, fdeploy1IniFileName);

            if ((flags & (int)FlagBits.MoveContents) != 0)
            {
                // Enable

                // Just create fdeploy.ini without any contents
                // lower compatibility
                if (File.Exists(fdeployPath) == false)
                {
                    using (StreamWriter writer = new StreamWriter(fdeployPath, false, Encoding.Unicode))
                    {
                        writer.Write(String.Empty);
                        writer.Close();
                    }
                }

                // Create the ini file in Unicode
                if (File.Exists(fdeploy1Path) == false)
                {
                    using (StreamWriter writer = new StreamWriter(fdeploy1Path, false, Encoding.Unicode))
                    {
                        writer.Write(String.Empty);
                        writer.Close();
                    }
                }

                // Writing ini file, sample format
                // [version]
                // version=100
                // 
                // [Folder_Redirection]
                // {FDD39AD0-238F-46AF-ADB4-6C85480369C7}=S-1-1-0;
                // 
                // [{FDD39AD0-238F-46AF-ADB4-6C85480369C7}_S-1-1-0]
                // FullPath=\\SomeServerName\SharedFolderName\%USERNAME%\Documents
                // Flags=1031

                IniFile ini = new IniFile(fdeploy1Path);
                if (!ini.WriteValue("version", "version", "100")) throw new WSSGGroupPolicyException("Error writing to ini file");
                if (!ini.WriteValue("Folder_Redirection", String.Format(CultureInfo.InvariantCulture, "{{{0}}}", guidFolder), String.Format(CultureInfo.InvariantCulture, "{0};", SidEveryone))) throw new WSSGGroupPolicyException("Error writing to ini file");

                string folderkey = String.Format(CultureInfo.InvariantCulture, "{{{0}}}_{1}", guidFolder, SidEveryone);
                string fullpath = Path.Combine(share, "%USERNAME%", nameFolder);

                if (!ini.WriteValue(folderkey, "FullPath", fullpath)) throw new WSSGGroupPolicyException("Error writing to ini file");
                if (!ini.WriteValue(folderkey, "Flags", String.Format(CultureInfo.InvariantCulture, "{0:X}", flags))) throw new WSSGGroupPolicyException("Error writing to ini file");

                Console.WriteLine("Completed setting ini file.");
            }
            else
            {
                // Disable

                if (File.Exists(fdeploy1Path) == true)
                {
                    IniFile ini = new IniFile(fdeploy1Path);

                    string folderkey = String.Format(CultureInfo.InvariantCulture, "{{{0}}}_{1}", guidFolder, SidEveryone);
                    string existing = ini.ReadValue(folderkey, "Flags");

                    if (String.IsNullOrEmpty(existing) == false)
                    {
                        // Configured before
                        if (!ini.WriteValue(folderkey, "FullPath", null)) throw new WSSGGroupPolicyException("Error writing to ini file");
                        if (!ini.WriteValue(folderkey, "Flags", String.Format(CultureInfo.InvariantCulture, "{0:X}", flags))) throw new WSSGGroupPolicyException("Error writing to ini file");

                        Console.WriteLine("Completed un-setting ini file.");
                    }
                    else
                    {
                        // Never configured
                        Console.WriteLine("Flags does not exists, therefore do nothing, but completed un-setting ini file.");
                    }
                }
                else
                {
                    Console.WriteLine("fdeploy1.ini does not exits, therefore do nothing, but completed un-setting ini file.");
                }
            }
        }
    }
}
