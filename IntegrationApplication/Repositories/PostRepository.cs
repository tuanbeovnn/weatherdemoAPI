using IntegrationApplication.EF;
using IntegrationModels;

namespace IntegrationApplication.Repositories;

public class PostRepository(IntegrationDbContext context) : GenericRepository<PostEntity>(context)
{
}