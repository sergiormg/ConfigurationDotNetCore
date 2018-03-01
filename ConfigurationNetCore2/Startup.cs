using ConfigurationNetCore2.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConfigurationNetCore2
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment HostingEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var configuration = GetConfiguration();
            services.AddSingleton(x => Options.Create(configuration.GetSection(nameof(CassandraConfiguration)).Get<CassandraConfiguration>()));
            services.AddSingleton(x => Options.Create(configuration.GetSection(nameof(KafkaConfiguration)).Get<KafkaConfiguration>()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private IConfigurationRoot GetConfiguration()
        {
            var environment = ConfigurationEnviroment();

            return new ConfigurationBuilder()
                            .SetBasePath(HostingEnvironment.ContentRootPath)
                            .AddJsonFile($"cassandra{environment}.json")
                            .AddJsonFile($"kafka{environment}.json")
                            .Build();
        }

        private string ConfigurationEnviroment()
        {
            return HostingEnvironment.IsProduction() ? string.Empty : $".{HostingEnvironment.EnvironmentName}";
        }
    }
}
