using HellCSharp.Persistence;

namespace HelloCSharp.Rest
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configure database connection
            var defaultConnection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDatabaseService(defaultConnection);
        }
    }
}
