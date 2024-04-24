    using BookApp.Core.DTO;

    namespace BookApp.Models
    {
        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            
            public bool IsStandard { get; set; }
            public List<CategoryDTO> Categories { get; set; }

            public CategoryViewModel() { }
            
            public CategoryViewModel(string name) { Name = name; }
            
            public CategoryViewModel(string name, bool isStandard) 
            { Name = name; IsStandard = isStandard; }
        }
    }
