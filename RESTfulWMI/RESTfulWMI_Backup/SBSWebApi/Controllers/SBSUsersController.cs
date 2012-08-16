using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SBSBusinessObject;

namespace WindowsServer2012RESTfulService.Controllers
{
    public class SBSUsersController : ApiController
    {

        [Queryable(ResultLimit = 20)]
        public IQueryable<SBSUser> GetAllSBSUsers()
        {
            return MockRepository.sbsUsers.AsQueryable();
        }

        public SBSUser GetSBSUserById(string id)
        {
            return SBSUser.GetInstance(id);
            /*var product = MockRepository.sbsUsers.FirstOrDefault((p) => p.UserId == id);
            if (product == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(resp);
            }
            return product;*/
        }

        public IEnumerable<SBSUser> GetSBSUserByByUserName(string UserName)
        {
            return MockRepository.sbsUsers.Where(
                (p) => string.Equals(p.UserName, UserName,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
