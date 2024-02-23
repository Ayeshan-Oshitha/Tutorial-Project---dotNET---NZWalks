namespace NZWalksAPI.Models.Domain
{
    public class Region
    {

        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }


        // '?' means , that this property can be a nullable one 
        public string? RegionImageUrl { get; set; }
    }
}
