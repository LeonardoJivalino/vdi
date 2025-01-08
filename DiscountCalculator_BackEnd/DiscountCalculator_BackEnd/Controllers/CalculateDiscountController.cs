using DiscountCalculator_BackEnd.Data;
using DiscountCalculator_BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCalc;

namespace DiscountCalculator_BackEnd.Controllers
{
    public class CalculateDiscountController : Controller
    {
        private readonly DiscountCalculatorContext _context;


        public CalculateDiscountController(DiscountCalculatorContext context)
        {
            _context = context;

        }
        [Route("api/[controller]/SaveTransaction")]

        [HttpPost]
        public async Task<IActionResult> SaveTransaction([FromQuery] string CustomerType, int PointReward, int TotalBelanja)
        {
            try
            {
                _context.Database.BeginTransaction();

                //var connectionString = _builder.Configuration.GetConnectionString("DefaultConnection");
                CustomerDiscount cd = await _context.CustomerDiscounts.Where(x => x.TipeCustomer == CustomerType && x.MinimumPointReward <= PointReward && x.MaximumPointReward >= PointReward).FirstOrDefaultAsync();
                string formula = "";
                int discountResult = 0;
                if (cd != null)
                {
                    formula = cd.DiscountFormula.Replace("TOTAL_BELANJA", TotalBelanja.ToString());
                    Expression expression = new Expression(formula);
                    var discountObj = expression.Evaluate();
                    discountResult = Convert.ToInt32(discountObj);
                }
                else
                {

                }
                

                bool anyTr = await _context.Transaksis.AnyAsync();
                string lastRunningNumber = "";
                if (anyTr)
                {
                    Transaksi lastTr = await _context.Transaksis.OrderBy(x => x.TransaksiId).LastOrDefaultAsync();
                    lastRunningNumber = lastTr.TransaksiId.Length >= 5
                ? lastTr.TransaksiId.Substring(lastTr.TransaksiId.Length - 5)
                : lastTr.TransaksiId;
                }
                else
                {
                    lastRunningNumber = "00000";
                }


                string nextRunningNumber = (int.Parse(lastRunningNumber) + 1).ToString("D5");

                Transaksi tr = new Transaksi();
                tr.TransaksiId = DateTime.Now.ToString("yyyyMMdd") + "_" + nextRunningNumber;
                tr.TipeCustomer = CustomerType;
                tr.PointReward = PointReward;
                tr.TotalBelanja = TotalBelanja;
                tr.Diskon = discountResult;
                tr.TotalBayar = TotalBelanja - discountResult;
                //tr.FakturNo = FakturNo;
                //tr.FakturDate = DateTime.Parse(FakturDate);
                //tr.PoliceNo = PoliceNo;
                //tr.LocationId = LocationId;            
                await _context.Transaksis.AddAsync(tr);

                await _context.SaveChangesAsync();
                _context.Database.CommitTransaction();
                return Json(new { message = "Transaction saved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Transaction failed!" });
            }
        }
        [Route("api/[controller]/GetTransactions")]
        [HttpGet]
        public async Task<IActionResult> GetTransactionList()
        {
            try
            {
                List<Transaksi> trList = await _context.Transaksis.ToListAsync();
                List<TransactionModel> trModelList = new List<TransactionModel>();
                foreach (var tr in trList)
                {
                    
                    TransactionModel trModel = new TransactionModel();
                    trModel.TransactionId = tr.TransaksiId;
                    trModel.CustomerType = tr.TipeCustomer;
                    trModel.PointReward = tr.PointReward;
                    trModel.Discount = tr.Diskon;
                    trModel.TotalBelanja = tr.TotalBelanja.ToString();
                    trModelList.Add(trModel);
                }
                return Ok(trModelList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
