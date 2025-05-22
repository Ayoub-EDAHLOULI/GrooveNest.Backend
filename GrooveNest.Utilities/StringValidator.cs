namespace GrooveNest.Utilities
{
    public static class StringValidator
    {
        public static bool IsNullOrWhiteSpace(string? value)
            => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);

        public static string TrimOrEmpty(string? value)
            => string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
    }
}
