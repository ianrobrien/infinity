namespace IRO.InfinityPhoto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var application = new Application();
            application.ResetExpirationKeys("a", "b", "c");
        }
    }
}
