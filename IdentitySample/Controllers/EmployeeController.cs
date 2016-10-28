using IdentitySample.Models;
using System.Collections.Generic;
using System.Web.Http;
using Thinktecture.IdentityModel.WebApi;

namespace IdentitySample.Controllers
{
    [Authorize]
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        // GET api/<controller>
        //api/values/employees
        [Route("all")]
        //[ScopeAuthorize("employees")]
        public IEnumerable<Employee> Get()
        {

            return new List<Employee>()
            {
                new Employee { Id = 1,Name="Biny"},
                new Employee { Id = 2,Name="Joy"}
            };
            //return new string[] { "Client1", "Client2" };
        }

        [Route("managers")]
        //[ScopeAuthorize("managers")]
        //api/values/managers
        public IEnumerable<Employee> GetCodes()
        {
            return new List<Employee>()
            {
                new Employee { Id = 1,Name="Sandeep"},
                new Employee { Id = 2,Name="Prakash"}
            };
        }


    }
}