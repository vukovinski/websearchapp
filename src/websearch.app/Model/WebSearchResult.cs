namespace websearch.app;

#pragma warning disable CS8618
public class WebSearchResult
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public DateTimeOffset Created { get; set; }
}
#pragma warning restore CS8618