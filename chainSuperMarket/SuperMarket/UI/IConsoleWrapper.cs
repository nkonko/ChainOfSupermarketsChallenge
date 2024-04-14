namespace SuperMarket.UI
{
    public interface IConsoleWrapper
    {
        string ReadLine();
        void WriteLine(string value);

        void Clear();
    }
}
