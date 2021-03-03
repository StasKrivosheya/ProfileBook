namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool Authorized { get; }

        void UnAuthorize();
    }
}
