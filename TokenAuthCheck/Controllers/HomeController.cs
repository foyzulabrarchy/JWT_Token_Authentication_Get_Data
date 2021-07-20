using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TokenAuthCheck.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Test()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Verify(string username, string password)
        {
            username = "\""+username +"\"";
            password = "\""+ password + "\"";
            var client = new RestClient("https://service.elpl.app/ServiceApi/Authenticate");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
" + "\n" +
            @"  ""username"": "+username+ ",\n" +
            @"  ""password"": " + password + " \n " +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string token = response.Content.ToString().Split('"')[1];
            return Json(token);
        }
        [HttpPost]
        public JsonResult GetData(string token)
        {
            var client = new RestClient("https://service.elpl.app/ServiceApi/Getinfo");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            IRestResponse response = client.Execute(request);
            return Json(response.Content);
        }
    }
}