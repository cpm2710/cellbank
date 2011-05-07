using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wx;
namespace BPACheck
{
    class BPACheck : App
    {
        public override bool OnInit()
        {
            // Create the main frame
            BPACheckFrame frame = new BPACheckFrame();

            // Show it
            frame.Show(true);
            // Return true, to indicate that everything was initialized
            // properly.
            return true;
        }

        [STAThread]
        public static void Main()
        {
            // Create an instance of our application class
            BPACheck app = new BPACheck();

            // Run the application
            app.Run();
        }
    }
}
