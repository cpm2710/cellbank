using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiskLib;

namespace DiskLibTest
{
    [TestClass]
    public class DiskHelperTest
    {
        [TestMethod]
        public void TestConvertToVhd()
        {
            DiskHelper.ConvertDiskToVhd(@"d:\abcdefghijklmnopqrstuvwzyz.vhd");
        }
    }
}
