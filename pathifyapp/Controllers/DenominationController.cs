using Microsoft.AspNetCore.Mvc;

// REFERENCES
// for dictionary -> https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-8.0
// for where clause -> https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-8.0
// for convertion to int -> https://learn.microsoft.com/en-us/dotnet/api/system.convert.toint32?view=net-8.0


namespace pathifyapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenominationController : ControllerBase
    {
        // Create a denomination dictionary
        Dictionary<int, string> denominationDict = new Dictionary<int, string>();


        [HttpGet(Name = "GetDenomination")]
        public List<Denomination> Get(int submittedCurrency)
        {
            // create a response
            var apiResponse = new List<Denomination>();

            // add the denominations to the dict
            denominationDict.Add(5, "5c");
            denominationDict.Add(10, "10c");
            denominationDict.Add(20, "20c");
            denominationDict.Add(50, "50c");
            denominationDict.Add(100, "$1");
            denominationDict.Add(200, "$2");
            denominationDict.Add(500, "$5");
            denominationDict.Add(1000, "$10");
            denominationDict.Add(2000, "$20");
            denominationDict.Add(5000, "$50");
            denominationDict.Add(10000, "$100");

            // sort the key in descending order where the key value is < submitted value
            var sortedKeys = denominationDict.Keys.Where(k => k <= submittedCurrency)
                .OrderByDescending(k => k).ToList();

            foreach (var key in sortedKeys)
            {
                Denomination denomination = new Denomination();

                // divide submitted currency by the key and convert to int
                // this should give us the denomination amount
                int denominationAmount = Convert.ToInt32(submittedCurrency / key);

                // if denomination amount is larger than 0, add it to the response
                if (denominationAmount > 0)
                {
                    denomination.Type = denominationDict[key];
                    denomination.Amount = denominationAmount;

                    apiResponse.Add(denomination);

                    // set the new submitted currency
                    // submitted currency - total of the current key 
                    submittedCurrency = submittedCurrency - (key * denominationAmount);
                }
            }

            return apiResponse;
        }
    }
}
