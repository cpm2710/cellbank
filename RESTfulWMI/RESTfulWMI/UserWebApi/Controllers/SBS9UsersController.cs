using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserBusinessObject;

namespace WindowsServer2012RESTfulService.Controllers
{
    public class SBS9UsersController : ApiController
    {
        

        public IEnumerable<SBS9User> GetAllProducts()
        {
            return MockRepository.sbsUsers;
        }

        public SBS9User GetSBS9UserById(string id)
        {
            var product = MockRepository.sbsUsers.FirstOrDefault((p) => p.UserId == id);
            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return product;
        }

        public IEnumerable<SBS9User> GetSBS9UsersByName(string name)
        {
            return MockRepository.sbsUsers.Where(
                (p) => string.Equals(p.UserName, name,
                    StringComparison.OrdinalIgnoreCase));
        } 

    }
}
