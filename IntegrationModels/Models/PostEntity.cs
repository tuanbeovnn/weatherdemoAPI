namespace IntegrationModels;

public class PostEntity : ModelBaseEntity
{
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public List<string> Images { get; set; } = new List<string>();
    public string Slug { get; set; }
}