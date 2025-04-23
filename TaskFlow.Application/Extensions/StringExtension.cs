namespace TaskFlow.Application.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string? str)
        {
            return str == null || str == "";
        }
    }
}