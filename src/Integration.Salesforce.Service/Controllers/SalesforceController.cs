using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Integration.Salesforce.Service.Controllers
{
  [Route("api/[conroller]")]
  public class SalesforceController : Controller
  {
    public async Task<IActionResult> Get()
    {
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
