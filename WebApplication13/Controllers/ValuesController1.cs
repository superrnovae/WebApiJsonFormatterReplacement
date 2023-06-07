using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication13.Controllers
{
    public class ValuesController: ApiController
    {
        public IAsyncEnumerable<int> Get(CancellationToken token)
        {
            return GetItems(token);
        }

        public async Task<IHttpActionResult> Post([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            await Task.Delay(200);
            
            return Ok(user);
        }

        private async IAsyncEnumerable<int> GetItems([EnumeratorCancellation]CancellationToken token = default)
        {
            for(int i=0; i<10; i++)
            {
                await Task.Delay(200, token).ConfigureAwait(false);
                yield return i;
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
