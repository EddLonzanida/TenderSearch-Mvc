using System.Threading.Tasks;
using System.Web.Mvc;

namespace TenderSearch.Web.Infrastructure.Contracts
{
    public interface IPersonSuggestion
    {
        Task<ActionResult> PersonSuggestions(string search = "", string param = null);
    }
}
