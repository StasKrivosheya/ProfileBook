namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool Authorized { get; }

        void Authorize(int id, string login);

        void UnAuthorize();
    }
}
