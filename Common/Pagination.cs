using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Pagination
    {
        public static IEnumerable<TSorce> ToPaged<TSorce>(this IEnumerable<TSorce> sorces, int pageNumber, int pageSize, out int rowsCount)
        {
            rowsCount = sorces.Count();
            return sorces.Skip((pageNumber-1)*pageSize).Take(pageSize);
        }
    }
}
