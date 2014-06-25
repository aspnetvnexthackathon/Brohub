using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Brohub
{
    public class RepositoryController
    {
        [Activate]
        public BrohubContext Context { get; set; }

        [HttpPost]
        public async Task<IActionResult> AnalyzeRepository(string repoUrl)
        {
            var repo = await Context.Repositories.FirstOrDefaultAsync(r => r.Url == repoUrl);
            if (repo == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var badges = await Context.RepositoryBadges
                                      .Include(r => r.Badge)
                                      .Where(r => r.RepositoryId == repo.Id && r.Rank == 1)
                                      .Select(r =>
                                                new BadgeModel
                                                {
                                                    id = r.Badge.Id,
                                                    title = r.Badge.Description,
                                                    image_url = r.Badge.Url
                                                })
                                      .ToArrayAsync();

            return new ObjectContentResult(
                new RepositoryModel
                {
                    url = repo.Url,
                    badges = badges
                });
        }
    }
}