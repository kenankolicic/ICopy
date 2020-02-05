namespace iCopy.SERVICES.Extensions
{
    public static class CSharpExtensions
    {
        public static int ToInt(this string obj)
        {
            return int.Parse(obj);
        }

        public static int ToIntOrDefault(this string obj)
        {
            if (int.TryParse(obj, out int result))
                return result;
            return default(int);
        }
    }
}
