using System;
using System.Collections;
using System.Reflection;

namespace ReflectionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Sword sword = new Sword(20);
            Console.WriteLine($"Sword damage before: {sword.damage}.");

            ArrayList list = new ArrayList
            {
                new Goblin(),
                sword,
                new Orc()
            };

            foreach (var item in list)
            {
                DoStart(item);
                DoClamp(item);
            }

            Console.WriteLine($"Sword damage after: {sword.damage}.");
        }

        static void DoStart(object item)
        {
            BindingFlags bindingFlags = BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic;

            MethodInfo methodInfo = item.GetType().GetMethod("Start", bindingFlags);
            if (methodInfo != null)
            {
                methodInfo.Invoke(item, null);
            }
        }

        static void DoClamp(object item)
        {
            FieldInfo[] fieldInfos = item.GetType().GetFields();
            foreach (var field in fieldInfos)
            {
                ClampAttribute attr = (ClampAttribute) Attribute.GetCustomAttribute(field, typeof(ClampAttribute));
                if (attr != null)
                {
                    int value = (int) field.GetValue(item);
                    value = Math.Min(value, attr.Min);
                    value = Math.Max(value, attr.Max);
                    field.SetValue(item, value);
                }
            }
        }
    }
}
