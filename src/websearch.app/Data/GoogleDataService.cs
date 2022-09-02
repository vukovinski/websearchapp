namespace websearch.app;

public class GoogleDataService
{
    private readonly IConfiguration _config;
    private readonly GoogleSearch.WebSearchApi _webSearchApi;

    public GoogleDataService
    (
        IConfiguration config,
        GoogleSearch.WebSearchApi webSearchApi
    )
    {
        _config = config;
        _webSearchApi = webSearchApi;
    }

    public async Task<BaseSearchResponse> RunGoogleSearch(string searchTerm)
    {
        return await _webSearchApi.QueryAsync(new WebSearchRequest()
        {
            Query = searchTerm,
            Key = _config.GetValue<string>("ApiKey")!,
            SearchEngineId = _config.GetValue<string>("SearchEngineId")!,
            Options = new SearchOptions()
            {
                ExactTerms = searchTerm,
                Number = 10
            }
        });
    }
}
