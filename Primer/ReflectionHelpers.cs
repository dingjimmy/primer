using System;
using System.Linq.Expressions;

namespace Primer
{
    public static class Reflection
    {

        public static string GetPropertyName<T>(Expression<Func<T>> propertyToInspect)
        {
            return ((MemberExpression)propertyToInspect.Body).Member.Name;
        }


        public static string GetMethodName<T>(Expression<Func<T>> methodToInspect)
        {
            return ((MethodCallExpression)methodToInspect.Body).Method.Name;
        }


        public static string GetMethodName(Expression<Action> methodToInspect)
        {
            return ((MethodCallExpression)methodToInspect.Body).Method.Name;
        }

    }
}
