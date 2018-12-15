using System.Threading.Tasks;
using System.Web.Mvc;

namespace TenderSearch.Web.Infrastructure.Contracts
{
    public interface IBarangaySuggestion
    {
        Task<ActionResult> BarangaySuggestions(string search = "");
    }
}
