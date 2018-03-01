using ConfigurationNetCore2.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationNetCore2.Controllers
{
    [Route("api/[controller]")]
    public class ConfigsController : Controller
    {
        private IOptions<CassandraConfiguration> cassandraConfiguration;
        private IOptions<KafkaConfiguration> kafkaConfiguration;

        public ConfigsController(IOptions<CassandraConfiguration> cassandraConfiguration, IOptions<KafkaConfiguration> kafkaConfiguration)
        {
            this.cassandraConfiguration = cassandraConfiguration;
            this.kafkaConfiguration = kafkaConfiguration;
        }

        [HttpGet("cassandra")]
        public CassandraConfiguration GetCassandra()
        {
            return cassandraConfiguration.Value;
        }

        [HttpGet("kafka")]
        public KafkaConfiguration GetKafka()
        {
            return kafkaConfiguration.Value;
        }
    }
}
