using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Middlewares {
    public class QueryStringMiddleWare {
        private readonly RequestDelegate _next;
        public QueryStringMiddleWare() {
            // do nothing
        }
        public QueryStringMiddleWare(RequestDelegate nextDelegate) {
            _next = nextDelegate;
        }
        public async Task Invoke(HttpContext context) {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true") {
                await context.Response.WriteAsync("Class-based Middleware \n");
            }
            // await next(context);
            if (_next != null) {
                await _next(context);
            }
        }
    }
}