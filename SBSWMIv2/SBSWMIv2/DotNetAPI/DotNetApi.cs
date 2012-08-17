using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace DotNetApi
{
    [Guid("3C2BF8C1-DD23-4ACB-B47A-80EE42F7E936")]
    public interface DotNetApi
    {
        [DispId(1)]
        int Add(int a);
    }
    [Guid("448C3FB9-45E6-42F0-B0A3-2B6DBA5ED058")]
    [ClassInterface(ClassInterfaceType.None)]
    public class DotNetApiImpl : DotNetApi
    {
        public int Add(int a)
        {
            return a + 1;
        }
    }
}
