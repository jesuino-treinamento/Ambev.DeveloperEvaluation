namespace Ambev.DeveloperEvaluation.Application.Users.ListUser
{
    public class PageUserResult
    {
        public IEnumerable<UserResult> Data { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
