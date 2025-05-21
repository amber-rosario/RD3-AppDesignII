using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IGadgetRepository repository;
        // GET: Admin
        public AdminController(IGadgetRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Gadgets);
        }

        public ViewResult Edit(int gadgetID)
        {
            Gadget gadget = repository.Gadgets
                .FirstOrDefault(g => g.GadgetID == gadgetID);
            return View(gadget);
        }

        [HttpPost]
        public ActionResult Edit(Gadget gadget, HttpPostedFileBase image=null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    gadget.ImageMimeType = image.ContentType;
                    gadget.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(gadget.ImageData, 0,
                    image.ContentLength);
                }

                repository.SaveGadget(gadget);

                TempData["message"] = string.Format
                    ("{0} has been saved", gadget.Name);
                return RedirectToAction("Index");
            }

            else
            {
                return View(gadget);
            }
        }

        public ViewResult Create()
        {
            ViewBag.operation = "create";
            return View("Edit", new Gadget());
        }

        [HttpPost]
        public ActionResult Delete(int gadgetID)
        {
            Gadget deletedGadget = repository.DeleteGadget(gadgetID);
            if (deletedGadget != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedGadget.Name);
            }
            return RedirectToAction("Index");
        }
    }
}