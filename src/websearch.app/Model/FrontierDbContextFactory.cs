namespace websearch.app;

public class FrontierDbContextFactory
{
    public string ConnectionString { get; init; }

    public FrontierDbContextFactory(IConfiguration configuration)
    {
        ConnectionString = configuration.GetValue<string>("ConnectionString")
            ?? throw new NullReferenceException(nameof(ConnectionString));
    }

    public FrontierDbContext GetNew() => new FrontierDbContext() { ConnectionString = ConnectionString };
}