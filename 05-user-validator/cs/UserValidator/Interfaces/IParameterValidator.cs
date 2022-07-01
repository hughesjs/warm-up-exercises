namespace UserValidator.Interfaces;

public interface IParameterValidator<in T>
{
	public void Validate(T param);
}
