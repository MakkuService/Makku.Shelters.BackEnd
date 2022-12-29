namespace Makku.Shelters.Application.Common.Exceptions
{
    public class NotCreatedException : Exception
    {
        public NotCreatedException(string name, string errors)
            :base($"Entity \"{name}\" did not created. Errors: {errors}.")
        {
        }
    }
}
