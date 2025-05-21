using GadgetHub.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Entities;
using GadgetHub.WebUI.Models;
using System.ComponentModel;

namespace GadgetHub.WebUI.Controllers
{
    public class GadgetController : Controller
    {
        private IGadgetRepository myrepository;

        public GadgetController(IGadgetRepository gadgetRepository)
        {
            this.myrepository = gadgetRepository;
        }

        public int PageSize = 3;
        public ViewResult List(string category, int page = 1)
        {
            GadgetListViewModel model = new GadgetListViewModel
            {

                Gadgets = myrepository.Gadgets
                .Where(g => category == null || g.Category == category)
                .OrderBy(g => g.GadgetID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {

                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //TotalItems = myrepository.Gadgets.Count()
                    TotalItems = category == null ?
                                    myrepository.Gadgets.Count() :
                                    myrepository.Gadgets.Where(e => e.Category == category).Count()

                },
                CurrentCategory = category

            };
            return View(model);
        }


        //public ViewResult List()
        //{
        //    return View(myrepository.Gadgets);
        //}

        public FileContentResult GetImage(int gadgetID)
        {
            Gadget gadget = myrepository.Gadgets.FirstOrDefault
                                            (g => g.GadgetID == gadgetID);

            if (gadget != null)
            {
                return File(gadget.ImageData, gadget.ImageMimeType);
            }

            else
            {
                return null;
            }
        }

    }
}
