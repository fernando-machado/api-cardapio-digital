namespace System.Linq
{
    using Collections.Generic;

    public static class IEnumberableExtensions
    {
        public static void Apply<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
        }
    }
}