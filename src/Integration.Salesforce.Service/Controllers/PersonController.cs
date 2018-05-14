using System.Collections.Generic;
using System.Threading.Tasks;
using Integration.Salesforce.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Integration.Salesforce.Context;
using Microsoft.Extensions.Options;

namespace Integration.Salesforce.Service.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private IOptions<Settings> _settings;
        private DbContext<Person> context;
        public PersonController(IOptions<Settings> settings)
        {
            _settings = settings;
            context = new DbContext<Person>(_settings);
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await Task.Run(() => context.ReadMongoEntries());
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            Person p = context.GetModelById(id);
            p.Active = false;
            List<Person> pList = new List<Person>();
            pList.Add(p);
            context.UpdateMongoEntries(pList);
            return await Task.Run(() => Ok());
        }
    }

}
