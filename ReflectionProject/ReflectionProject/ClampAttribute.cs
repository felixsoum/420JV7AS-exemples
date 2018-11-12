using System;

namespace ReflectionProject
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ClampAttribute : Attribute
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public ClampAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}
