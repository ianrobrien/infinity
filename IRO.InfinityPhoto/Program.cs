using System;

namespace IRO.InfinityPhoto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var application = new Application();
            application.ResetExpirationKeys("0", "a", "b", "c", DateTime.Now);
        }
    }
}
