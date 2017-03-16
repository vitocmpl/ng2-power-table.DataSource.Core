using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ng2YaTable.DataSource.Demo.Models;

namespace Ng2YaTable.DataSource.Demo.Repos
{
    public class DemoRepository
    {
        private JArray data = null;

        public async Task<IQueryable<User>> GetUsers()
        {
            if(data == null)
            {
                using(var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://jsonplaceholder.typicode.com" + "/users");
                    response.EnsureSuccessStatusCode();

                    data = JArray.FromObject(JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()));
                }
            }

            return data.Select(p => p.ToObject<User>()).AsQueryable();;
        }
    }
}