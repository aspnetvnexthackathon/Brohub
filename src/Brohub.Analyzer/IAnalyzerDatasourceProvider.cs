using System.Threading.Tasks;

namespace Brohub.Analyzer
{
    public interface IAnalyzerDatasourceProvider
    {
        Task<object> GetDatasourceAsync(Repository repository);
    }
}