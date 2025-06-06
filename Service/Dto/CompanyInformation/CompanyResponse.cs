namespace Service.Dto.CompanyInformation
{
    public class CompanyResponse
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string Domain { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string LogoUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public string Description { get; set; }

        public string CountryId { get; set; }

        public string StateId { get; set; }

        public string CityId { get; set; }

        public string OwnerId { get; set; }
    }
}