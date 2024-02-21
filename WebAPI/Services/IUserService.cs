namespace WebAPI;

public interface IUserService
{
    string Login(JwtPayload jwtPayload);

    List<JwtPayload> All();
}
