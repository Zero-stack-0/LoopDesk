namespace Service.Dto.CompanyInformation
{
    public class CreateCompanyInformationRequest
    {
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string CityId { get; set; }
    }
}