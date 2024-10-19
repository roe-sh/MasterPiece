namespace LoveSeedM.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }               // Unique ID of the category
        public string CategoryName { get; set; }  // Name of the category
        public string Description { get; set; }   // Description of the category
        public string Image { get; set; }         // Image URL or path for the category
    }
}
