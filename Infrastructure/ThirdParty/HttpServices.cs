using Core.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Infrastructure.ThirdParty
{
    public class HttpServices : IHttpService
    {
        HttpClient client = new HttpClient();
        public HttpServices(IConfiguration configuration)
        {
            client.BaseAddress = new Uri(configuration.GetSection("BaseUrl").Value);
        }

        public async Task<GetStudents> GetListOfStudents()
        {
            List<GetStudents> students = new List<GetStudents>();
            client.DefaultRequestHeaders.Clear();
            HttpResponseMessage res = await client.GetAsync("https://localhost:44353/get-list-of-student");
            if (res.IsSuccessStatusCode)
            {
                var stdRes = res.Content.ReadAsStringAsync().Result;
                students = JsonConvert.DeserializeObject<List<GetStudents>>(stdRes);
                
            }
            var stdnt = new GetStudents();
            return stdnt;
        }
    }
}
