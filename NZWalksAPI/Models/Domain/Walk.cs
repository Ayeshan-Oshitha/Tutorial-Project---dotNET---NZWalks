namespace NZWalksAPI.Models.Domain
{
    public class Walk  // Walks renamed to walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghthInKm { get; set; }
        public string? WalkImageUrl { get; set; }


        // ( Below 2 lines have been deleted in unknown manner in 5.5 step, But those should be deleted from 'WalkDto file. These changes should be applied to 5.6 step too) 
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation Properties
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
