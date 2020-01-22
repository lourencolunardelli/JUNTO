using BLL;
using HELPER;
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
        public ActionResult<string> Get(string ApiToken)
        {
            if (Helper.ValidateApiToken(ApiToken))
                return JsonConvert.SerializeObject(UserBLO.Get());
            else
                return JsonConvert.SerializeObject("Token inválido");
        }

        [HttpGet]
        [Route("GetById")]
        public ActionResult<string> GetById(string ApiToken, int id)
        {
            if (Helper.ValidateApiToken(ApiToken))
                return JsonConvert.SerializeObject(UserBLO.GetById(id));
            else
                return JsonConvert.SerializeObject("Token inválido");
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult<string> Insert(string ApiToken, UserModel model)
        {
            if (Helper.ValidateApiToken(ApiToken))
                return JsonConvert.SerializeObject(UserBLO.Insert(model));
            else
                return JsonConvert.SerializeObject("Token inválido");
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult<string> Update(string ApiToken, UserModel model)
        {
            if (Helper.ValidateApiToken(ApiToken))
                return JsonConvert.SerializeObject(UserBLO.Update(model));
            else
                return JsonConvert.SerializeObject("Token inválido");
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public ActionResult<string> UpdatePassword(string ApiToken, int id, string password)
        {
            if (Helper.ValidateApiToken(ApiToken))
                return JsonConvert.SerializeObject(UserBLO.UpdatePassword(id, password));
            else
                return JsonConvert.SerializeObject("Token inválido");
        }

        [HttpDelete]
        [Route("api/[controller]")]
        public ActionResult<string> Delete(string ApiToken, int id)
        {
            if (Helper.ValidateApiToken(ApiToken))
                return JsonConvert.SerializeObject(UserBLO.Delete(id));
            else
                return JsonConvert.SerializeObject("Token inválido");
        }
    }
}