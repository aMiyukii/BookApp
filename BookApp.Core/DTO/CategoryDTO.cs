namespace BookApp.Core.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStandard { get; set; }

        public CategoryDTO()
        {
        }

        public CategoryDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryDTO(int id, string name, bool isStandard)
        {
            Id = id;
            Name = name;
            IsStandard = isStandard;
        }
    }
}