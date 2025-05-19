using System.ComponentModel;

namespace Ambev.DeveloperEvaluation.Domain.Common.Enumeradors
{
    public static class Enumerador
    {
        public static string? Descricao<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                var type = e.GetType();
                var name = type.GetEnumName(e);
                var membro = type.GetMember(name);

                var descriptionAttribute = membro[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

                return descriptionAttribute?.Description;
            }

            return null;
        }
    }
}
