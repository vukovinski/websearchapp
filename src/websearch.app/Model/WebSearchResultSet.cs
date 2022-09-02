namespace websearch.app;

#pragma warning disable CS8618
public class WebSearchResultSet
{
    public int Id { get; set; }
    public WebSearch WebSearch { get; set; }
    public List<WebSearchResult> WebSearchResults { get; set; }
}
#pragma warning restore CS8618