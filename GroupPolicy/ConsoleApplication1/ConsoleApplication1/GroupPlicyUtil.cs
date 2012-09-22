using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class GroupPlicyUtil
    {
        public static void CreateFileGroupPolicy(string filePath, FilesFile file)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }
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
        public static void CreateShortcutGroupPolicy(string filePath, ShortcutsShortcut shortcut)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
            if (shortcut == null)
            {
                throw new ArgumentNullException("shortcut");
            }
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

    }
}
