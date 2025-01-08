using Anagrams.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Anagrams.Controllers
{
    public class CheckAnagramsFormController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CheckAnagram([FromBody] StringModel data)
        {
            try
            {
                if(data == null || data.FirstWords == "" || data.SecondWords == "")
                {
                    var responseError = new
                    {
                        Message = "Please fill first words and second words!",
                        Outputstring = ""
                        
                    };
                    return Json(responseError);
                }
                Regex regex = new Regex("^[a-z,\"]+$");
                bool firstWordsValid = regex.IsMatch(data.FirstWords);
                bool secondWordsValid = regex.IsMatch(data.FirstWords);
                if (!secondWordsValid && !firstWordsValid)
                {
                    var responseError = new
                    {
                        Message = "First words or second words contains forbidden character!",
                        Outputstring = ""

                    };
                    return Json(responseError);
                }

                string output = "";

                string[] firstWordsArray = data.FirstWords.Split(',');
                string[] secondWordsArray = data.SecondWords.Split(',');
                
                for (int i = 0; i < firstWordsArray.Length; i++)
                {
                    bool isAnagram = false;
                    string word1 = firstWordsArray[i].Trim();
                    string word2 = secondWordsArray[i].Trim();

                    // Check if they are anagrams
                    // Remove spaces and make them lowercase
                    word1 = word1.Replace(" ", "").ToLower();
                    word1 = word1.Replace("\"", "").ToLower();
                    word2 = word2.Replace(" ", "").ToLower();
                    word2 = word2.Replace("\"", "").ToLower();

                    // Check if lengths are the same
                    if (word1.Length != word2.Length)
                    {
                        isAnagram = false;
                    }

                    // Sort the characters in the words and compare
                    char[] word1Array = word1.ToCharArray();
                    char[] word2Array = word2.ToCharArray();

                    Array.Sort(word1Array);
                    Array.Sort(word2Array);
                    if (new string(word1Array) == new string(word2Array))
                    {
                        output = output + "1";  // Anagram
                    }
                    else
                    {
                        output = output + "0";  // Not an anagram
                    }
                }                                
                var response = new
                {
                    Message = "Anagram check completed!",
                    Outputstring = output
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                return Json(new { message = "An error occured!" });
            }
        }
        public IActionResult CheckAnagramsForm()
        {
            return View();
        }
    }
}
