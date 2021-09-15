using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Platform.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

/* app.Use(async (context, next) =>
    {
        if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
        {
            await context.Response.WriteAsync("Custom Middleware \n");
        }
        Console.WriteLine($"HTTP Method: {context.Request.Method}, HTTP Params:");
        foreach (var keyValuePair in context.Request.Query)
        {
            Console.WriteLine($"{keyValuePair.Key} = {keyValuePair.Value}");
        }
        await next();
    }
); */

// app.UseMiddleware<QueryStringMiddleWare>();
app.UseMiddleware<CheckImagePathMiddleWare>();

app.MapGet("/", () => "Hello ASP.NET!");

app.Run();
