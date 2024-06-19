namespace Patcher
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var patcher = new Patcher();
            patcher.PatchFileAsync().Wait();
        }
    }
}