namespace LinkShortener.BLL.Contracts
{
    public interface INumberEncodingService
    {
        string Encode(int value);
        int Decode(string str);
    }
}
