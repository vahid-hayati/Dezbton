namespace api.Interfaces;
public interface ITokenService
{
    string CreateToken(Register user);
}