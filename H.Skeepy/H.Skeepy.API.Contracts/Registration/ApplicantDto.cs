using H.Skeepy.Model.Storage;

namespace H.Skeepy.API.Contracts.Registration
{
    public class ApplicantDto
    {
        public class DetailEntry
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DetailsHolderDto FacebookDetails { get; set; } = new DetailsHolderDto();
    }
}
