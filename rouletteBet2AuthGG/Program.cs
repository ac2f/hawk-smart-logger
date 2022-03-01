using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auth.GG_Winform_Example
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //OnProgramStart.Initialize("Hawk Smart", "718211", "5bjxi2H73452c3QR1ZjSoPtBYHas8JRy8MY", "1.0");
            //OnProgramStart.Initialize("Hawk Smart", "718211", "5bjxi2H73452c3QR1ZjSoPtBYHas8JRy8MY", "1.0");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new rouletteBet.Form1());
        }
    }
}
