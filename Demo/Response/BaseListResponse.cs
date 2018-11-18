using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Response
{
    public class BaseListResponse<T> where T: class
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public long TotalRecord { get; set; }
        public List<T> Data { get; set; }
    }
}
