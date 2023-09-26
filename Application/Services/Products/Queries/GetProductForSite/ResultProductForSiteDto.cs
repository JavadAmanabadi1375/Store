using System.Collections.Generic;

namespace Application.Services.Products.Queries.GetProductForSite
{
    public class ResultProductForSiteDto
    {

        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

}
