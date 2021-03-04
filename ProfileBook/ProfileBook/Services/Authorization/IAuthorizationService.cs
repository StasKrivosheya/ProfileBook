namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool Authorized { get; }

        int CurrentUserId { get; }

        void Authorize(int id, string login);

        void UnAuthorize();
    }
}
