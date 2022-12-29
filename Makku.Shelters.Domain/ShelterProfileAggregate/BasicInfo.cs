using Makku.Shelters.Domain.ShelterProfileValidators;

namespace Makku.Shelters.Domain.ShelterProfileAggregate
{
    public class BasicInfo
    {
        private BasicInfo()
        {
        }
        public string Email { get; private set; }
        public string ShelterName { get; private set; }

        /// <summary>
        /// Creates a new BasicInfo instance
        /// </summary>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="emailAddress">Emnail address</param>
        /// <param name="shelterName">Phone</param>
        /// <param name="dateOfBirth">Date of Birth</param>
        /// <param name="currentCity">Current city</param>
        /// <returns><see cref="BasicInfo"/></returns>
        /// <exception cref="UserProfileNotValidException"></exception>
        public static BasicInfo CreateBasicInfo(string emailAddress, string shelterName)
        {
            var validator = new BasicInfoValidator();

            var objToValidate = new BasicInfo
            {
                Email = emailAddress,
                ShelterName = shelterName
            };

            var validationResult = validator.Validate(objToValidate);

            if (validationResult.IsValid) return objToValidate;

            //var exception = new NotValidException("The user profile is not valid");
            //foreach (var error in validationResult.Errors)
            //{
            //    exception.ValidationErrors.Add(error.ErrorMessage);
            //}

            throw new Exception();
        }
    }
}
