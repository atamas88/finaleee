﻿using DtoModel;
using LifeInEsbjerg.Models.ViewModel;
using LifeInEsbjergGateway;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LifeInEsbjerg.Controllers
{
    public class CompanyController : Controller
    {
        private Facade facade = new Facade();

        // GET: Company
        public ActionResult Index()
        {
            IEnumerable<Company> companies = facade.GetCompanyGateway().ReadAll();
            Debug.WriteLine(companies);
            return View(companies);


        }

        public ActionResult Create()
        {
            var model = new LifeInEsbjergViewModel() { Category = new SelectList(facade.GetCategoryGateway().ReadAll(), "Id", "Name") };
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(LifeInEsbjergViewModel model)
        {
                List<Category> allCat = new List<Category>(facade.GetCategoryGateway().ReadAll());
                for(int i = 0; i < allCat.Count; ++i)
                {
                    if(allCat.ElementAt(i).Id == model.selectedCat)
                    {
                        model.Company.Category = allCat.ElementAt(i);
                    }
                }
            
            facade.GetCompanyGateway().Add(model.Company);
            return RedirectToAction("Index", "Company");
        }

        public ActionResult Delete(int? id)
        {
            Company company = facade.GetCompanyGateway().Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            facade.GetCompanyGateway().Delete(company);
            return RedirectToAction("Index", "Company");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = facade.GetCompanyGateway().Find(id);

            int selectedCat = company.Category.Id;

            var model = new LifeInEsbjergViewModel()
            {
                Company = company,
                Category = new SelectList(facade.GetCategoryGateway().ReadAll(), "Id", "Name"),
                selectedCat = selectedCat
            };

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Company, Category, selectedCat")] LifeInEsbjergViewModel model)
        {

            //ViewBag.Genres = new SelectList(db.Genres, "Id", "Name");


            List<Category> allCat = new List<Category>(facade.GetCategoryGateway().ReadAll());
            for (int i = 0; i < allCat.Count; ++i)
            {
                if (allCat.ElementAt(i).Id == model.selectedCat)
                {
                    model.Company.Category = allCat.ElementAt(i);
                }
            }
            if (ModelState.IsValid)
            {

                facade.GetCompanyGateway().Update(model.Company);
                return RedirectToAction("Index");
            }
            //facade.GetMovieRepository().Edit(movie);
            return View(model.Company);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = facade.GetCompanyGateway().Find(id);
            //var selectGenres = new List<int>();
            //foreach (var item in movie.Genres)
            //{
            //    selectGenres.Add(item.Id);
            //}
            //var model = new CreateMovieViewModel()
            //{
            //    Movie = movie,
            //    Genres = new MultiSelectList(facade.GetGenreGateway().ReadAll(), "Id", "Name"),
            //    SelectedGenres = selectGenres
            //};

            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

    }
}