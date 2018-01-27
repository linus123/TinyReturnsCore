using Dimensional.TinyReturns.Core.DateExtend;
using Dimensional.TinyReturns.Core.PublicWebSite;
using Microsoft.AspNetCore.Mvc;

namespace TinyReturnsCore.Controllers
{
    public class PublicWebController : Controller
    {
        private readonly PortfolioListPageAdapter _portfolioListPageAdapter;

        public PublicWebController(
            PortfolioListPageAdapter portfolioListPageAdapter)
        {
            _portfolioListPageAdapter = portfolioListPageAdapter;
        }

        public IActionResult Get(
            string id)
        {
            var split = id.Split('-');

            var year = int.Parse(split[0]);
            var month = int.Parse(split[1]);

            var monthYear = new MonthYear(year, month);

            var result = _portfolioListPageAdapter.GetPortfolioPageRecords(monthYear);

            return new ObjectResult(result);
        }
    }
}