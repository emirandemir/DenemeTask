using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskUI.Controllers
{
    public class StudentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync("https://localhost:5001/api/Student/");
            var jsonstring = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<Student>>(jsonstring);
            return View(values);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student s)
        {
            var httpclient = new HttpClient();
            var JsonStudent = JsonConvert.SerializeObject(s);
            var content = new StringContent(JsonStudent, Encoding.UTF8, "application/json");
            var response = await httpclient.PostAsync("https://localhost:5001/api/Student", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Student");
            }
            return View(s);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateStudent(int id)
        {
            var httpclient = new HttpClient();
            var response = await httpclient.GetAsync("https://localhost:5001/api/Student/" + id);
            if (response.IsSuccessStatusCode)
            {
                var JsonStudent = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<Student>(JsonStudent);
                return View(values);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStudent(Student s)
        {
            var httpclient = new HttpClient();
            var JsonStudent = JsonConvert.SerializeObject(s);
            var content = new StringContent(JsonStudent, Encoding.UTF8, "application/json");
            var response = await httpclient.PutAsync("https://localhost:5001/api/Student", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Student");
            }
            return View(s);
        }

        public async Task<IActionResult> DeleteStudent(int id)
        {
            var httpclient = new HttpClient();
            var response = await httpclient.DeleteAsync("https://localhost:5001/api/Student/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Vize { get; set; }
        public int Final { get; set; }
    }
}

