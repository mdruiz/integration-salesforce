using System.Threading.Tasks;
using Integration.Salesforce.Context;
using Integration.Salesforce.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Integration.Salesforce.Service.Controllers
{
    [Route("api/[controller]")]
    public class SalesforceController : Controller
    {
        private IOptions<Settings> _settings;
        
        public SalesforceController(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        [HttpGet("update")]
        public async Task<IActionResult> Get()
        {
            var updateJob = new ScheduleUpdate(_settings);
            await updateJob.Update();
            return await Task.Run(() => Ok());
        }

        [HttpGet("delete")]
        public async Task<IActionResult> delete()
        {
            DbContext<Person> context = new DbContext<Person>(_settings);
            context.RemoveAllMongoEntries();

            return await Task.Run(() => Ok());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return await Task.Run(() => Ok());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]object value)
        {
            return await Task.Run(() => Ok());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]object value)
        {
            return await Task.Run(() => Ok());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await Task.Run(() => Ok());
        }
    }
}
