using System;
using System.Collections.Generic;
using System.Text;

namespace ChurrasTrinca.Models.ResponseService
{
   public class ResponseService<t>
    {
        public bool isSucess { get; set; }
        public int statusCode { get; set; }
        public string token { get; set; }
        public t Data { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }

        public static implicit operator ResponseService<t>(ResponseService<Participants> v)
        {
            throw new NotImplementedException();
        }
    }
}
