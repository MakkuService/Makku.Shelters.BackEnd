using System.ComponentModel.DataAnnotations;

namespace Makku.Shelters.Domain.ShelterProfileAggregate
{
    public class ShelterProfile
    {
        private ShelterProfile()
        {
        }
        public Guid ShelterProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime ModifiedOn { get; private set; }

        // Factory method
        public static ShelterProfile CreateShelterProfile(string identityId, BasicInfo basicInfo)
        {
            return new ShelterProfile
            {
                IdentityId = identityId,
                BasicInfo = basicInfo,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };
        }

        //public methods

        public void UpdateBasicInfo(BasicInfo newInfo)
        {
            BasicInfo = newInfo;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
