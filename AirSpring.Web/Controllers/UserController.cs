using AirSpring.Web.Models.DTO;
using AirSpring.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AirSpring.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        [HttpPost]
        public IActionResult Login(LoginDTO formData)
        {
            try
            {
                if (formData != null && string.IsNullOrEmpty(formData.Username) != true && string.IsNullOrEmpty(formData.Password) != true)
                {
                    using (var client = new HttpClient())
                    {
                        var url = _configuration["UrlBaseApi"] + "User/Login";
                        var jsonObject = JsonConvert.SerializeObject(formData);
                        var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                        var taskRequest = client.PostAsync(url, content);
                        taskRequest.Wait();
                        if (taskRequest.Result.IsSuccessStatusCode)
                        {
                            var taskResult = taskRequest.Result.Content.ReadAsStringAsync();
                            taskResult.Wait();
                            var result = taskResult.Result;
                            var userDeserialize = JsonConvert.DeserializeObject<User>(result);
                            if (userDeserialize != null && userDeserialize.Id > 0)
                            {
                                HttpContext.Session.SetString("IdUser", Convert.ToString(userDeserialize.Id));
                                HttpContext.Session.SetString("Username", userDeserialize.Username);
                                return Json(1);
                            }
                        }
                    }
                }
                return Json(0);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return Json(0);
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
