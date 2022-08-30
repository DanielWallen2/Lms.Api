using Lms.Data.Data;
using Lms.Data;

namespace Lms.Api.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<LmsApiContext>();

                try
                {
                    await SeedData.InitAsync(db);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return app;
        }

    }
}
