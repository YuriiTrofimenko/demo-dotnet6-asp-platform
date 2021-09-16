using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Platform.Models;

namespace Platform.Middlewares {
    public class LocationMiddleware {
        private RequestDelegate next;
        private MessageOptions options;
        public LocationMiddleware(RequestDelegate nextDelegate, IOptions<MessageOptions> opts) {
            next = nextDelegate;
            options = opts.Value;
        }
        public async Task Invoke(HttpContext context) {
            if (context.Request.Path == "/location") {
                await context.Response
                    .WriteAsync($"{options.CityName},{options.CountryName}");
            } else {
                await next(context);
            }
        }
    }
}