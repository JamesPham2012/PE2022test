public abstract class BaseDALConnection {
    protected string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
}
