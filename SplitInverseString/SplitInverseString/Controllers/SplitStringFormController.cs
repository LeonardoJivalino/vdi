using Microsoft.AspNetCore.Mvc;
using SplitInverseString.Models;

namespace SplitInverseString.Controllers
{
    public class SplitStringFormController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SplitString([FromBody] StringModel data)
        {
            try
            {
                if (data == null || data.InputString == "")
                {
                    var responseError = new
                    {
                        Message = "Please fill string!",
                        Outputstring = ""

                    };
                    return Json(responseError);
                }
                string input = data.InputString;                
                int midpoint = input.Length / 2;                
                string firstHalf = input.Substring(0, midpoint);
                string secondHalf = input.Substring(midpoint);                
                string reversedFirstHalf = new string(firstHalf.Reverse().ToArray());
                string reversedSecondHalf = new string(secondHalf.Reverse().ToArray());
                string splitStringresult = reversedFirstHalf + reversedSecondHalf;

                var response = new
                {
                    Message = "Split string success!",
                    Outputstring = splitStringresult
                };

                return Json(response);                 
            }
            catch (Exception ex)
            {
                return Json(new { message = "An error occured!" });
            }
        }
        public IActionResult SplitStringForm()
        {
            return View();
        }
    }
}
