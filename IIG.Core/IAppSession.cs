
namespace EntityFrameWorkCore
{
    public interface IAppSession
    {
        string? UserId { get; }
        string? UserName { get; }
        string? Ip { get; }
    }

}
