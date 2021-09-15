using System;

namespace Platform
{
    public static class CustomExtensions
    {
        public delegate bool Validator(string source);
        public static bool CheckContent(this string source, Validator validator)
        {
            return validator.Invoke(source);
        }
    }
}