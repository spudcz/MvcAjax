using System;
using System.Linq;
using System.Web.Mvc;
using MvcAjax.Models;

namespace MvcAjax.Controllers
{
    public class HomeController : Controller
    {
        private static void SlowDownTask() {
            System.Threading.Thread.CurrentThread.Join(1000);
        }

        public ActionResult Index(int? skip, int? take)
        {
            ViewBag.Skip = skip ?? 0;
            ViewBag.Take = take ?? 10;
            return View();
        }

        public PartialViewResult IndexPartial(int? skip, int? take)
        {
            if (Request.IsAjaxRequest()) {
                SlowDownTask();
            }
            var people = PeopleRepository.Current.Skip(skip ?? 0).Take(take ?? 10);
            return PartialView("_Index", people);
        }

        public PartialViewResult CreatePartial()
        {
            SlowDownTask();
            var person = new Person();
            return PartialView("_Create", person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult CreatePartial(Person person)
        {
            SlowDownTask();
            if (ModelState.IsValid) {
                PeopleRepository.Current.Add(person);
            }
            return PartialView("_Create", person);
        }

        public PartialViewResult UpdatePartial(int id)
        {
            SlowDownTask();
            var person = PeopleRepository.Current.FirstOrDefault(x => x.Id == id);
            return PartialView("_Update", person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult UpdatePartial(Person person)
        {
            SlowDownTask();
            if (ModelState.IsValid) {
                var index = PeopleRepository.Current.Select((x, y) => new { Person = x, Index = new int?(y) }).Where(x => x.Person.Id == person.Id).Select(x => x.Index).FirstOrDefault();
                if (index.HasValue) {
                    PeopleRepository.Current.RemoveAt(index.Value);
                    PeopleRepository.Current.Insert(index.Value, person);
                }
            }
            return PartialView("_Update", person);
        }

        [HttpPost]
        public PartialViewResult Delete(int id)
        {
            SlowDownTask();
            var person = PeopleRepository.Current.FirstOrDefault(x => x.Id == id);
            if (person != null) {
                PeopleRepository.Current.Remove(person);
            }
            return null;
        }

        
    }
}