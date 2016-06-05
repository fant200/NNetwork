using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTests
{
    using System.Reflection;

    public static class ReflectionExtensions
    {
        public static T As<T>(this object obj)
        {
            return (T)obj;
        }
        public static object GetFieldValue(this object obj, FieldInfo info)
        {
            if (info == null)
                throw new ArgumentException(nameof(info));
            return info.As<FieldInfo>().GetValue(obj);
        }
        public static object GetFieldValue(this object obj, string name)
        {
            var fields = GetAllFields(obj.GetType());
            return GetFieldValue(obj, fields.FirstOrDefault(x => x.Name.Equals(name)));

        }
        public static IEnumerable<FieldInfo> GetAllFields(this Type t)
        {
            if (t == null)
                return Enumerable.Empty<FieldInfo>();
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic |
                         BindingFlags.Static | BindingFlags.Instance |
                         BindingFlags.DeclaredOnly;
            return t.GetFields(flags).Concat(GetAllFields(t.BaseType));
        }

        public static string GetFullName(this MethodInfo info)
        {
            var types = info.GetParameters();
            return $"{info.ReturnType} {info.Name}({string.Join(",", types.Select(x => x.ParameterType.FullName))})";
        }
    }
}

