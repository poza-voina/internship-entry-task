namespace InternshipEntryTask.Api.IntegrationTests.Base;

public class CustomWebApplicationFactoryConfiguration
{
    public string ConnectionString { get; init; }
    public string Schema { get; init; }
    public string PathToEnvironment { get; set; }

    //public static CustomWebApplicationFactoryConfiguration DefaultWithSchema() { 
    //    return new CustomWebApplicationFactoryConfiguration
    //    {

    //    }
    //}
}
