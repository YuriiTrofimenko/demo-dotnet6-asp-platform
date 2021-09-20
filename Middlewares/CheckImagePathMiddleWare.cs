using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platform.Middlewares {
    public class CheckImagePathMiddleWare {
        private RequestDelegate next;
        public CheckImagePathMiddleWare(RequestDelegate nextDelegate) {
            next = nextDelegate;
        }
        public async Task Invoke(HttpContext context) {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query.ContainsKey("path"))
            {
                await context.Response.WriteAsync("Class-based Middleware CheckImagePathMiddleWare \n");
                string path = context.Request.Query["path"];
                if (path.CheckContent(source => source.EndsWith(".gif") || source.EndsWith(".jpg") || source.EndsWith(".png")))
                {
                    await context.Response.WriteAsync($"Image Path Is: {path} \n");
                }
                else
                {
                    await context.Response.WriteAsync("Incorrect image path (suffix .gif, .jpg or .png does not present) \n");
                }
                /* try
                {
                    ImagePath path = new ImagePath() { Path = context.Request.Query["path"]};
                    await context.Response.WriteAsync($"Image Path Is: {path} \n");
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync($"{ex.Message} \n");
                } */
                context.Response.StatusCode = StatusCodes.Status200OK;
            }
            await next(context);
        }
    }
}