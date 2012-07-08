using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WindowsServer2012RESTfulService.Models;

namespace WindowsServer2012RESTfulService.Controllers
{
    public class SBS9UsersController : ApiController
    {
        SBS9User[] products = new SBS9User[]  
        {  
            new SBS9User { Id = 1, Name = "Tomato Soup" },  
            new SBS9User { Id = 2, Name = "Yo-yo"},  
            new SBS9User { Id = 3, Name = "Hammer" }  
        };

        public IEnumerable<SBS9User> GetAllProducts()
        {
            return products;
        }

        public SBS9User GetSBS9UserById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return product;
        }

        public IEnumerable<SBS9User> GetSBS9UsersByName(string name)
        {
            return products.Where(
                (p) => string.Equals(p.Name, name,
                    StringComparison.OrdinalIgnoreCase));
        } 

    }
}
