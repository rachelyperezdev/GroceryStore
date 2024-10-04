namespace Backend.Core.Application.Helpers
{
    public class IngredientQueryObject
    {
        public string? Name { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public bool IsDescendant { get; set; } = false;
        public string? SortBy { get; set; }
    }
}
