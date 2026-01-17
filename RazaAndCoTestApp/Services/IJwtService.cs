namespace RazaAndCoTestApp.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username, string role);
    }

}
