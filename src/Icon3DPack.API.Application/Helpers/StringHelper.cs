namespace Icon3DPack.API.Application.Helpers
{
    public static class StringHelper
    {
        public static bool IsNotNullOrEmpty(this string? value) => !string.IsNullOrEmpty(value);
    }
}
