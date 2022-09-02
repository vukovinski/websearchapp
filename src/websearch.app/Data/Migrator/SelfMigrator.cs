namespace websearch.app;

// code from https://gist.github.com/floyd-may/dbbdbe05b00e218106e973a2b47527ff#file-handyselfmigrator-cs
// by Floyd May

#pragma warning disable CS8602
#pragma warning disable CS0618
public sealed class SelfMigrator
{
    public void Migrate(IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var ctxFactory = scope.ServiceProvider.GetRequiredService<FrontierDbContextFactory>();
        using var ctx = ctxFactory.GetNew();

        var sp = (ctx as IInfrastructure<IServiceProvider>).Instance;

        var modelDiffer = sp.GetRequiredService<IMigrationsModelDiffer>();
        var migrationsAssembly = sp.GetRequiredService<IMigrationsAssembly>();
        var dependencies = sp.GetRequiredService<ProviderConventionSetBuilderDependencies>();
        var relationalDependencies = sp.GetRequiredService<RelationalConventionSetBuilderDependencies>();

        var typeMappingConvention = new TypeMappingConvention(dependencies);
        typeMappingConvention.ProcessModelFinalizing(((IConventionModel)migrationsAssembly.ModelSnapshot.Model).Builder, null);

        var relationalModelConvention = new RelationalModelConvention(dependencies, relationalDependencies);
        var sourceModel = relationalModelConvention.ProcessModelFinalized(migrationsAssembly.ModelSnapshot.Model);

        var diffsExist = modelDiffer.HasDifferences(
            ((IMutableModel)sourceModel).FinalizeModel().GetRelationalModel(),
            ctx.Model.GetRelationalModel());

        if (diffsExist)
        {
            throw new InvalidOperationException("There are differences between the current database model and the most recent migration.");
        }

        ctx.Database.Migrate();
    }
}
#pragma warning restore CS8602
#pragma warning restore CS0618