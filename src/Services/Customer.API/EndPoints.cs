using Customer.API.Services.Interfaces;

namespace Customer.API
{
    public static class EndPoints
    {
        public static void MapCustomerApi(this WebApplication app)
        {
            app.MapGet("/api/customers", async (ICustomerService customerService) =>
            {
                return await customerService.GetCustomersAsync();
            });

            app.MapGet("/api/customers/{username}", async (ICustomerService customerService, string username) =>
            {
                var result = await customerService.GetCustomerByUsernameAsync(username);
                return result != null ? result : Results.NotFound();
            });
        }
    }
}
