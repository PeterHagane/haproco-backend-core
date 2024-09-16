using Microsoft.EntityFrameworkCore;

namespace haproco_backend_core.Data
{
    public class TestTable
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/testtable", async (DataContext dataContext) =>
            {
                return await dataContext.testtable.ToListAsync();
            })
            .WithName("GetTestTable")
            .WithOpenApi();
        }
    }
}
