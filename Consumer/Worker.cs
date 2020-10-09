using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using ModelLib.Model;
using Newtonsoft.Json;

namespace Consumer
{
    public class  Worker
    {
        public async void Start()
        {
            Console.WriteLine(string.Join("\n", GetALlItemsAsync().Result));

        }
        public async Task<IList<Item>> GetALlItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync("http://restyservice.azurewebsites.net/api/items");
                IList<Item> cList =
                    JsonConvert.DeserializeObject<IList<Item>>(content);
                return cList;
            }
        }
        public async Task<Item> GetItemByIdAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync($"http://restyservice.azurewebsites.net/api/items/{id}");
                Item item =
                    JsonConvert.DeserializeObject<Item>(content);
                
                return item;
            }
        }
        public async Task PutItemAsync(int id, string name, string quality, double quantity)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = JsonConvert.SerializeObject(new Item(id, name, quality, quantity));
                StringContent stringContent =
                new StringContent(content, encoding: Encoding.UTF8, "application/json");

                await client.PutAsync($"http://restyservice.azurewebsites.net/api/items/{id}", stringContent);
            }
        }
        public async Task DeleteItemAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync($"http://restyservice.azurewebsites.net/api/items/{id}");
            }
        }


        public async Task PostItemAsync(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = JsonConvert.SerializeObject(new Item(item.Id, item.Name, item.Quality, item.Quantity));
                StringContent stringContent = 
                    new StringContent(content, Encoding.UTF8, "application/json");
                await client.PostAsync($"http://restyservice.azurewebsites.net/api/items/{item.Id}", stringContent);
            }
        }





    }
}
