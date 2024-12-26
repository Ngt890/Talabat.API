using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
   public  interface IResponseCasheService

    {
        //Cashe Data
       Task CasheResponseAsync(string CashKey, object Resonse, TimeSpan TimetoLive);


        //Get Cashed
        Task<string?> GetCashedData(string CaskKey);  

    }
}
