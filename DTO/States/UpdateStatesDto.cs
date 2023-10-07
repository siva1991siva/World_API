namespace World.Api.DTO.States
{
    public class UpdateStatesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Population { get; set; }
        public int CountryId { get; set; }
    }
}
