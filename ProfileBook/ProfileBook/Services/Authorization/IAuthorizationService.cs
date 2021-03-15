namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }

        int CurrentUserId { get; }

        void Authorize(int id, string login);

        void UnAuthorize();
    }
}
