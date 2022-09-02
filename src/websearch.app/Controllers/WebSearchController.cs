namespace websearch.app;

[EnableCors("APP_API_PAIR")]
public class WebSearchController : ControllerBase
{
    private readonly DataService _dataService;
    public WebSearchController(DataService dataService) => _dataService = dataService;

    [Route("/query-results")][HttpGet]
    public IActionResult GetResults(string? filterTerm = null, int? limit = 10, int? offset = 0, bool ascending = false)
    {
        var order = ascending
            ? DataOrder.ByDateCreatedAscending
            : DataOrder.ByDataCreatedDescending;

        var queryResult =
            _dataService.GetWebSearchResults(filterTerm, limit!.Value, offset!.Value, order);

        return new JsonResult(queryResult.ToArray());
    }

    [Route("/query")][HttpGet]
    public async Task<IActionResult> QueryAsync([FromQuery] string webSearch)
    {
        await _dataService.RunWebSearch(webSearch);
        return Redirect("https://localhost:44481/results");
    }
}