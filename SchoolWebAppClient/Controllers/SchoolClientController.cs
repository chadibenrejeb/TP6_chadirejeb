using Microsoft.AspNetCore.Mvc;
using SchoolWebAppClient.Models;
using System.Net.Http;
using System.Collections.Generic;

namespace SchoolWebAppClient.Controllers
{
    public class SchoolClientController : Controller
    {
        private readonly HttpClient _client;

        public SchoolClientController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5255/"); // Assurez-vous que cette URL est correcte
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: SchoolClient/GetAllSchools
        public async Task<IActionResult> GetAllSchools()
        {
            HttpResponseMessage response = await _client
                .GetAsync("api/SchoolsRepo/get-all-schools"); // Correct route

            if (response.IsSuccessStatusCode)
            {
                var schools = await response.Content
                    .ReadFromJsonAsync<IEnumerable<SchoolClient>>();
                return View(schools);
            }

            return View(new List<SchoolClient>());
        }

        // GET: SchoolClient/GetSchoolById/5
        public async Task<IActionResult> GetSchoolById(int id)
        {
            HttpResponseMessage response = await _client
                .GetAsync($"api/SchoolsRepo/get-school-by-id/{id}"); // Correct route

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content
                    .ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // GET: SchoolClient/CreateSchool
        public IActionResult CreateSchool()
        {
            return View();
        }

        // POST: SchoolClient/CreateSchool
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSchool(SchoolClient school)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _client
                    .PostAsJsonAsync("api/SchoolsRepo/create-school", school); // Correct route

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(GetAllSchools));
                }
            }
            return View(school);
        }

        // GET: SchoolClient/EditSchool/5
        public async Task<IActionResult> EditSchool(int id)
        {
            HttpResponseMessage response = await _client
                .GetAsync($"api/SchoolsRepo/get-school-by-id/{id}"); // Correct route

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content
                    .ReadFromJsonAsync<SchoolClient>();
                return View(school);
            }

            return NotFound();
        }

        // POST: SchoolClient/EditSchool/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchool(SchoolClient school)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _client
                    .PutAsJsonAsync($"api/SchoolsRepo/edit-school/{school.Id}", school); // Correct route

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(GetAllSchools));
                }
            }
            return View(school);
        }

        // GET: SchoolClient/DeleteSchool/5
        public async Task<IActionResult> DeleteSchool(int id)
        {
            HttpResponseMessage response = await _client
                .GetAsync($"api/SchoolsRepo/get-school-by-id/{id}"); // Correct route

            if (response.IsSuccessStatusCode)
            {
                var school = await response.Content
                    .ReadFromJsonAsync<SchoolClient>();

                if (school == null)
                {
                    return NotFound();
                }

                return View(school);
            }

            return NotFound();
        }

        // POST: SchoolClient/DeleteSchool/5
        [HttpPost, ActionName("DeleteSchool")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _client
                .DeleteAsync($"api/SchoolsRepo/delete-school/{id}"); // Correct route

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAllSchools));
            }

            var school = await _client
                .GetFromJsonAsync<SchoolClient>($"api/SchoolsRepo/get-school-by-id/{id}"); // Correct route

            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }
    }

}
