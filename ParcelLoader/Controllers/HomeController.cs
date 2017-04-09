using ParcelLoader.Business;
using ParcelLoader.Core;
using ParcelLoader.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ParcelLoader.Controllers
{
    [RegisterSingleton]
    public class HomeController : Controller
    {
        private IParcelHelper _parcelHelper;

        public HomeController(IParcelHelper parcelHelper)
        {
            _parcelHelper = parcelHelper;
        }

        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public JsonResult GetParcelCost(RequestData RequestData)
        {
            var parcelDimension = new ParcelProperties();

            foreach (var item in RequestData.RequestParams)
            {
                if (item.key == "Height")
                {
                    parcelDimension.Height = item.value;
                }

                if (item.key == "Length")
                {
                    parcelDimension.Length = item.value;
                }

                if (item.key == "Breadth")
                {
                    parcelDimension.Breadth = item.value;
                }

                if (item.key == "Weight")
                {
                    parcelDimension.Weight = item.value;
                }
            }

            var returnedObject = _parcelHelper.GetCost(Map.MapParcelDimensions(parcelDimension));

            if (returnedObject == null)
            {
                return Json(new { message = "Sorry we do not provide any packaging solution for the dimensions or weight you have provided." });
            }

            var returnMessage = "You can send a " + returnedObject.Type + " for the dimensions you have provided at a cost of $" + returnedObject.Cost;

            return Json(new { message = returnMessage });
        }
    }
}