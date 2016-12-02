namespace LinkShortener.BLL.Contracts
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(int length);
    }
}
