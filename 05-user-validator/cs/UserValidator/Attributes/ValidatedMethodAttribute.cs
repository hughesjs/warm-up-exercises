using System.Reflection;
using Someta;
using UserValidator.Interfaces;

namespace UserValidator.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ValidatedMethodAttribute: Attribute, IMethodInterceptor
{
	public object Invoke(MethodInfo methodInfo, object instance, Type[] typeArguments, object[] arguments, Func<object[], object> invoker)
	{
		Attribute[]? attributes = methodInfo.GetCustomAttributes().Where(att => att.GetType().IsAssignableTo(typeof(IParameterValidator<>))).ToArray();
		// Do my thing
		return invoker.Invoke(arguments);
	}
}