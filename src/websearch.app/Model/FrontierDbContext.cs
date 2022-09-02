namespace websearch.app;

#pragma warning disable CS8618
public class FrontierDbContext : DbContext
{
    public DbSet<WebSearch> WebSearches { get; set; }
    public DbSet<WebSearchResult> WebSearchResults { get; set; }
    public DbSet<WebSearchResultSet> WebSearchResultSets { get; set; }

    public string ConnectionString { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(ConnectionString);
    }
}
#pragma warning restore CS8618