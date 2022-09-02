namespace websearch.app;

public class DataService
{
    private readonly FrontierDbContext _dbContext;
    private readonly GoogleDataService _googleDataService;


    public DataService
    (

        GoogleDataService googleDataService,
        FrontierDbContextFactory dbContextFactory
    )
    {
        _dbContext = dbContextFactory.GetNew();
        _googleDataService = googleDataService;
    }

    public async Task RunWebSearch(string searchTerm)
    {
        var searchResponse = await _googleDataService.RunGoogleSearch(searchTerm);
        if (searchResponse == null)
            throw new DataServiceException("Failed to perform Google search");

        var searchResults = searchResponse.Items.ToArray();
        await StoreSearchResultsAsync(searchTerm, searchResults);
    }

    public IEnumerable<dynamic> GetWebSearchResults(string? filterTerm, int limit = 10, int offset = 0, DataOrder sortOrder = DataOrder.ByDataCreatedDescending)
    {
        var query =
            from search in _dbContext.WebSearches
            join result_set in _dbContext.WebSearchResultSets on search.Id equals result_set.WebSearch.Id
            select new { Search = search, Results = result_set.WebSearchResults };

        query =
            sortOrder == DataOrder.ByDataCreatedDescending
                ? query.OrderByDescending(e => e.Search.Created)
                : query.OrderBy(e => e.Search.Created);

        var mappedQuery = query
            .SelectMany(e => e.Results)
            .Where(e => filterTerm == null || EF.Functions.Like(e.Title, $"%{filterTerm}%"))
            .Select(r => new { Title = r.Title, Link = r.Url })
            .Skip(offset).Take(limit);

        return mappedQuery;
    }

    private async Task StoreSearchResultsAsync(string searchTerm, Item[] searchResults)
    {
        var now = DateTimeOffset.Now;
        var search = new WebSearch() { Query = searchTerm, Created = now };
        var searchResultsM = searchResults.Select(r => new WebSearchResult() { Url = r.FormattedUrl, Title = r.Title, Created = now }).ToList();
        var searchResultsSet = new WebSearchResultSet() { WebSearch = search, WebSearchResults = searchResultsM };

        await _dbContext.WebSearches.AddAsync(search);
        await _dbContext.WebSearchResults.AddRangeAsync(searchResultsM);
        await _dbContext.WebSearchResultSets.AddAsync(searchResultsSet);
        await _dbContext.SaveChangesAsync();
    }
}

[System.Serializable]
public class DataServiceException : System.Exception
{
    public DataServiceException() { }
    public DataServiceException(string message) : base(message) { }
    public DataServiceException(string message, System.Exception inner) : base(message, inner) { }
    protected DataServiceException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}