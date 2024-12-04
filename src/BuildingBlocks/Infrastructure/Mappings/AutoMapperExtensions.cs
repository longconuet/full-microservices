using AutoMapper;
using System.Reflection;

namespace Infrastructure.Mappings
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination> 
            (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var destinationProperty in destinationProperties)
            {
                if (sourceType.GetProperty(destinationProperty.Name, flags) == null)
                {
                    expression.ForMember(destinationProperty.Name, opt => opt.Ignore());
                }
            }

            return expression;
        }
    }
}
