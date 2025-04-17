namespace TaskFlow.Application.ApplicationUser
{
    public class CurrentUser
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<string> Roles { get; private set; }

        public CurrentUser(string id, string email, IEnumerable<string> roles)
        {
            Id = id;
            Email = email;
            Roles = roles;
        }

        public bool IsInRole(string role) => Roles.Contains(role);
    }
}