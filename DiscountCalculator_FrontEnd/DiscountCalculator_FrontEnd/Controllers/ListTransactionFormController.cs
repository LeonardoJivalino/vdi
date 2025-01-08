using DiscountCalculator_FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscountCalculator_FrontEnd.Controllers
{
    public class ListTransactionFormController : Controller
    {
        private readonly TransactionServices _trService;
        
        public ListTransactionFormController(TransactionServices trService)
        {
            _trService = trService;            
        }
        public async Task<IActionResult> ListTransactionForm()
        {
            //var locations = await _bpkbService.GetLocation();
            var trList = await _trService.GetTransactions();
            return View(trList);
        }
    }
}
