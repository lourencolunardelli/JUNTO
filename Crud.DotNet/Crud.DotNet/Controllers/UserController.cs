using BLL;
using Microsoft.AspNetCore.Mvc;
using MODEL;
using Newtonsoft.Json;
using Ninject;

namespace Crud.DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("Get")]
        public ActionResult<string> Get()
        {
            return JsonConvert.SerializeObject(UserBLO.Get());
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<string> GetById(int id)
        {
            return JsonConvert.SerializeObject(UserBLO.GetById(id));
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult<string> Insert(UserModel model)
        {
            return JsonConvert.SerializeObject(UserBLO.Insert(model));
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<string> Update(UserModel model)
        {
            return JsonConvert.SerializeObject(UserBLO.Update(model));
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public ActionResult<string> UpdatePassword(int id, string password)
        {
            return JsonConvert.SerializeObject(UserBLO.UpdatePassword(id, password));
        }

        [HttpDelete]
        [Route("api/[controller]")]
        public ActionResult<string> Delete(int id)
        {
            return JsonConvert.SerializeObject(UserBLO.Delete(id));
        }
    }
}