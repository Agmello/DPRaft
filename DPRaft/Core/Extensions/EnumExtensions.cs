using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class EnumExtensions
    {
        public static T TryCast<T>(this int value, out bool success)
        {
            try
            {
                var cast = (T)(object)value;
                success = true;
                return cast;
            }
            catch { }
            success = false;
            return default(T);
        }
    }
}
