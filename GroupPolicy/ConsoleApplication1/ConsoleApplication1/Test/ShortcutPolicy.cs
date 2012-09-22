using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ShortcutPolicyCollection
    {
        private const string ShortcutsClsid = "{872ECB34-B2EC-401b-A585-D32574AA90EE}";
        private const string ShortcutClsid = "{4F2F7C55-2790-433e-8127-0739D1CFA327}";
        public string clsid { get; set; }
        private List<ShortcutPolicy> ShortcutPolicies { get; set; }
        public ShortcutPolicyCollection()
        {

        }
        //      <Shortcuts clsid="{872ECB34-B2EC-401b-A585-D32574AA90EE}">
        //<Shortcut clsid="{4F2F7C55-2790-433e-8127-0739D1CFA327}" userContext="1" name="ff" status="ff" image="0" changed="2012-09-21 02:28:21" 
        //          uid="{9574730A-10EB-4265-9C59-A0D98559607C}">
        //  <Properties pidl="" targetType="FILESYSTEM" action="C" comment="" shortcutKey="0" startIn="" arguments="" iconIndex="0" targetPath="C:\Users\AuroraUser"
        //              iconPath="" window="" shortcutPath="ff"/>
        //</Shortcut>
    }
    public class ShortcutPolicy
    {
        public string clsid { get; set; }
        public int userContext { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public int image { get; set; }
        public string changed { get; set; }
        public string uid { get; set; }
        public ShortcutPolicyProperties Properties { get; set; }
    }
    public class ShortcutPolicyProperties
    {
        public string pidl { get; set; }
        public string targetType { get; set; }
        public string action { get; set; }
        public string comment { get; set; }
        public int shortcutKey { get; set; }
        public string startIn { get; set; }
        public string arguments { get; set; }
        public int iconIndex { get; set; }
        public string targetPath { get; set; }
        public string iconPath { get; set; }
        public string shortcutPath { get; set; }
    }
}
