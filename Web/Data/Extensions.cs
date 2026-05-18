using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Web.Data
{
    public static class Extensions
    {

        /// <summary>
        /// Fix decimal to show trailing zeros
        /// </summary>
        /// <param name="input">Input decimal</param>
        /// <param name="scale">Number of decimal places from 0 to 28</param>
        /// <returns>Fixed precision decimal</returns>
        public static decimal SetScale(this decimal input, int scale)
        {
            if (scale < 0 || scale > 28)
                throw new ArgumentOutOfRangeException(nameof(scale));

            if (input.Scale == scale)
                return input;

            //normalize
            input /= 1.000000000000000000000000000000000m;
            var scaleToadd = scale - input.Scale;
            for (int i = 0; i < scaleToadd; i++)
            {
                input *= 1.0M;
            }


            return input;
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
               where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        public static string GetAttributeName(this Enum enumValue)
        {
            return enumValue.GetAttribute<DisplayAttribute>().Name;
        }



    }
}
