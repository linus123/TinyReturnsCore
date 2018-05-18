using Microsoft.AspNetCore.Mvc;
using TinyReturns.SharedKernel.DateExtend;
using TinyReturns.SharedKernel.PublicWebSite;

namespace TinyReturnsCore.Controllers
{
    public class PublicWebController : Controller
    {
        private readonly PortfolioListPageFacade _portfolioListPageFacade;

        public PublicWebController(
            PortfolioListPageFacade portfolioListPageFacade)
        {
            _portfolioListPageFacade = portfolioListPageFacade;
        }

        public IActionResult Get(
            string id)
        {
            var split = id.Split('-');

            var year = int.Parse(split[0]);
            var month = int.Parse(split[1]);

            var monthYear = new MonthYear(year, month);

            var result = _portfolioListPageFacade.GetPortfolioPageRecords(monthYear);

            return new ObjectResult(result);
        }
    }
}