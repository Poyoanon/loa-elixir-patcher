using System;
using System.Windows.Forms;

namespace Patcher
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var patcher = new Patcher();
            patcher.PatchFileAsync().Wait();
        }
    }
}
