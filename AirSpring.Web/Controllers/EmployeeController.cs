using AirSpring.Models.DTO;
using AirSpring.Web.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AirSpring.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult NewEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewEmployee(NewEmployeeDTO formData)
        {
            try
            {
                if (formData != null && string.IsNullOrEmpty(formData.EmployeeLastName) != true)
                {
                    var IdUser = Convert.ToInt64(HttpContext.Session.GetString("IdUser"));
                    if (IdUser > 0)
                    {
                        using (var client = new HttpClient())
                        {
                            var url = _configuration["UrlBaseApi"] + "Employee";
                            var jsonObject = JsonConvert.SerializeObject(formData);
                            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                            var taskRequest = client.PostAsync(url, content);
                            taskRequest.Wait();
                            if (taskRequest.Result.IsSuccessStatusCode)
                            {
                                var taskResult = taskRequest.Result.Content.ReadAsStringAsync();
                                taskResult.Wait();
                                var result = taskResult.Result;
                                if (Convert.ToInt32(result) > 0)
                                {
                                    return Json(1);
                                }
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

        [HttpPost]
        public IActionResult DeleteEmployee(DeleteEmployeeDTO formData)
        {
            try
            {
                if (formData != null && string.IsNullOrEmpty(formData.employeeId.ToString()) != true)
                {
                    if (formData.employeeId > 0)
                    {
                        //HttpClientHandler clientHandler = new HttpClientHandler();
                        //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                        using (var client = new HttpClient())
                        {
                            var url = _configuration["UrlBaseApi"] + "Employee/Delete/";
                            var jsonObject = JsonConvert.SerializeObject(formData);
                            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                            var taskRequest = client.PostAsync(url, content);
                            taskRequest.Wait();
                            if (taskRequest.Result.IsSuccessStatusCode)
                            {
                                var taskResult = taskRequest.Result.Content.ReadAsStringAsync();
                                taskResult.Wait();
                                var result = taskResult.Result;
                                if (Convert.ToInt32(result) > 0)
                                {
                                    return Json(1);
                                }
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

        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeDTO formData)
        {
            try
            {
                if (formData != null && string.IsNullOrEmpty(formData.EmployeeId.ToString()) != true)
                {
                    if (formData.EmployeeId > 0)
                    {
                        using (var client = new HttpClient())
                        {
                            var url = _configuration["UrlBaseApi"] + "Employee";
                            var jsonObject = JsonConvert.SerializeObject(formData);
                            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                            var taskRequest = client.PutAsync(url, content);
                            taskRequest.Wait();
                            if (taskRequest.Result.IsSuccessStatusCode)
                            {
                                var taskResult = taskRequest.Result.Content.ReadAsStringAsync();
                                taskResult.Wait();
                                var result = taskResult.Result;
                                if (Convert.ToInt32(result) > 0)
                                {
                                    return Json(1);
                                }
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

        public IActionResult AllEmployees()
        {
            var data = new List<EmployeeDTO>();
            data = GetAllEmployees();
            return View(data);
        }

        public IActionResult UpdateEmployee(int id)
        {
            var data = GetEmployee(id);
            return View(data);
        }

        private EmployeeDTO GetEmployee(int id)
        {
            try
            {
                EmployeeDTO employee = new EmployeeDTO();
                using (var client = new HttpClient())
                {
                    var url = _configuration["UrlBaseApi"] + "Employee/GetEmployee/";
                    var jsonObject = JsonConvert.SerializeObject(id);
                    var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
                    var taskRequest = client.PostAsync(url, content);
                    taskRequest.Wait();
                    if (taskRequest.Result.IsSuccessStatusCode)
                    {
                        var taskResult = taskRequest.Result.Content.ReadAsStringAsync();
                        taskResult.Wait();
                        var result = taskResult.Result;
                        var data = JsonConvert.DeserializeObject<EmployeeDTO>(result);
                        employee = data;
                    }
                }
                return employee;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new EmployeeDTO();
            }
        }

        private List<EmployeeDTO> GetAllEmployees()
        {
            try
            {
                List<EmployeeDTO> _lista = new List<EmployeeDTO>();
                using (var client = new HttpClient())
                {
                    var url = _configuration["UrlBaseApi"] + "Employee";
                    var taskRequest = client.GetAsync(url);
                    taskRequest.Wait();
                    if (taskRequest.Result.IsSuccessStatusCode)
                    {
                        var taskResult = taskRequest.Result.Content.ReadAsStringAsync();
                        taskResult.Wait();
                        var result = taskResult.Result;
                        var data = JsonConvert.DeserializeObject<List<EmployeeDTO>>(result);
                        _lista = data;
                    }
                }
                return _lista;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return new List<EmployeeDTO>();
            }
        }
    }
}
