namespace core.Interfaces;

public interface IMyAuthorizationServiceSingelton
{
    int UserIdAuthenticated { get; }
    
    int? UserIdAuthenticatedOrNull { get; }
}