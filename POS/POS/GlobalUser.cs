// GlobalUser.cs
namespace POS
{
    public static class GlobalUser
    {
        public static string Username { get; set; }
        public static string Name { get; set; }
        public static string Role { get; set; }
        public static bool IsLoggedIn { get; set; }

        public static void Clear()
        {
            Username = string.Empty;
            Name = string.Empty;
            Role = string.Empty;
            IsLoggedIn = false;
        }
    }
}