namespace Makku.Shelters.Domain.Exceptions
{
    public class ShelterProfileNotValidException : DomainModelInvalidException
    {
        internal ShelterProfileNotValidException() { }
        internal ShelterProfileNotValidException(string message) : base(message) { }
        internal ShelterProfileNotValidException(string message, Exception inner) : base(message, inner) { }
    }
}
