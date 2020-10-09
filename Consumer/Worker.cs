using System;
using System.Collections;
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

            int id;
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(
            await GetItemByIdAsync(id));

            await PostItemAsync(new Item(9, "claus", "low", 24));
            Console.WriteLine(await GetItemByIdAsync(9));
            
            
            
            
            await PutItemAsync(new Item(9, "Klaus", "low", 24));
            Console.WriteLine(await GetItemByIdAsync(9));
            
            
            await DeleteItemAsync(9);








        }

        //public string URI { get; } = "http://restyservice.azurewebsites.net/api/items";
        public string URI { get; } = "http://localhost:5000/api/items";


        public async Task<IList<Item>> GetALlItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI);
                IList<Item> cList =
                    JsonConvert.DeserializeObject<IList<Item>>(content);
                return cList;
            }
        }
        public async Task<Item> GetItemByIdAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(URI +$"/{id}");
                Item item =
                    JsonConvert.DeserializeObject<Item>(content);
                
                return item;
            }
        }
        public async Task PutItemAsync(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = JsonConvert.SerializeObject(item);
                StringContent stringContent =
                new StringContent(content, encoding: Encoding.UTF8, "application/json");

                await client.PutAsync(URI + $"/{item.Id}", stringContent);
            }
        }
        public async Task DeleteItemAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                await client.DeleteAsync(URI + $"/{id}");
                Console.WriteLine($"{id}: Deleted");
            }
        }


        public async Task PostItemAsync(Item item)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = JsonConvert.SerializeObject(new Item(item.Id, item.Name, item.Quality, item.Quantity));
                StringContent stringContent = 
                    new StringContent(content, Encoding.UTF8, "application/json");
                await client.PostAsync(URI, stringContent);
            }
        }





    }
}
