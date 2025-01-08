using DiscountCalculator_FrontEnd.Models;
using DiscountCalculator_FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace DiscountCalculator_FrontEnd.Controllers
{
    public class TransactionFormController : Controller
    {
        private readonly TransactionServices _trService;
        public TransactionFormController(TransactionServices trService)
        {
            _trService = trService;            
        }
        [HttpPost]
        public async Task<IActionResult> SubmitTransaction([FromBody] TransactionModel data)
        {
            try
            {
                if (data == null || data.CustomerType == "" || data.PointReward.ToString() == "" || data.TotalBelanja == "")
                {
                    var responseError = new
                    {
                        Message = "Please fill all needed data!",
                        Outputstring = ""

                    };
                    return Json(responseError);
                }

                TransactionModel trModel = new TransactionModel();
                trModel.CustomerType = data.CustomerType;
                trModel.PointReward = data.PointReward;
                trModel.TotalBelanja = data.TotalBelanja;

                await _trService.SubmitTransaction(trModel);
                //ViewBag.Message = "Items saved successfully!";
                //return View();
                return Json(new { message = "Items saved successfully!" });
            }
            catch(Exception ex)
            {
                return Json(new { message = "An error occured!" });
            }
            

        }
        public IActionResult TransactionForm()
        {
            return View();
        }
    }
}
