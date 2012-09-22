using Microsoft.GroupPolicy;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GroupPlicyUtil
    {
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
        
        public static void SetSecurityTemplatePolicy(Guid gpoGuid, Dictionary<string, string> defaultPolicy)
        {

        }
        public static void EnableFolderRedirection(string deployIniFilePath)
        {

        }
    }
}
