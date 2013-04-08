// author: andyliuliming@outlook.com

using RotorsGui;
using RotorsLib;
using RotorsLib.Helpers;
using RotorsWorkFlow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rotors
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                App app = new App();

                app.MainWindow = new MainWindow();
                app.MainWindow.Show();
                app.Run();
            }
            else
            {
                InitializeParameterSet();
                // now we came into command line mode.
                if (args[0] == "/?" || args[0] == "/h")
                {
                    PrintHelp();
                }
                else
                {
                    ParseParameter(args);
                    if (!ParasAllSet())
                    {
                        PrintHelp();
                    }
                    else
                    {

                    }
                }
            }
        }
        // -m machine name
        // -u username
        // -p password
        // -d home directory

        private static Dictionary<string, bool> ParameterSet = new Dictionary<string, bool>();
        static void InitializeParameterSet()
        {
            ParameterSet.Add("-u",false);
            ParameterSet.Add("-m",false);
            ParameterSet.Add("-p",false);
            ParameterSet.Add("-d",false);
        }
        static bool ParasAllSet()
        {
            return ParameterSet[UserNamePara] 
                && ParameterSet[PassWordPara] 
                && ParameterSet[MachineNamePara];
                //&& ParameterSet[BinaryHomePara];
        }
        private const string UserNamePara="-u";

        private const string PassWordPara="-p";

        private const string MachineNamePara = "-m";

        private const string BinaryHomePara = "-d";
        static void ParseParameter(string[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                switch (args[i])
                {
                    case UserNamePara:
                        Singleton<Constants>.UniqueInstance.UserName = args[i + 1];
                        ParameterSet[UserNamePara]=true;
                        break;
                    case MachineNamePara:
                        Singleton<Constants>.UniqueInstance.MachineName = args[i + 1];
                        ParameterSet[MachineNamePara] = true;
                        break;
                    case PassWordPara:
                        Singleton<Constants>.UniqueInstance.PassWord = args[i + 1];
                        ParameterSet[PassWordPara] = true;
                        break;
                    case BinaryHomePara:
                        Singleton<Constants>.UniqueInstance.SourceRootPath = args[i + 1];
                        ParameterSet[BinaryHomePara] = true;
                        break;
                    default:
                        break;
                }
            }
            if (!ParameterSet[BinaryHomePara])
            {
                Singleton<Constants>.UniqueInstance.SourceRootPath = Environment.GetEnvironmentVariable("_NTTREE");
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine(@"Rotors.exe -m andesss1serverxx -u aurorauser -p User@123 -d d:\winmain\fbl_srv4_sh_dev_1.binaries.amd64fre");
            Console.WriteLine(@"-u means the user name of the target machine");
            Console.WriteLine(@"-p means the password of the user");
            Console.WriteLine(@"-d means the source depot output directory, if you run it in razzle and do not specify this parameter, Rotors will use the __NTTREE as the binary home. ");
        }
    }
}
