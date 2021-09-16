using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform.Middlewares;
using Platform.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.Configure<MessageOptions>(options => {});
builder.Services.Configure<MessageOptions>(options =>
{
    options.CityName= "Odessa";
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<LocationMiddleware>();

app.Map(pathMatch:"/branch", configuration:branch => {
    branch.Run(new QueryStringMiddleWare().Invoke);
});

/* app.Map(
    pathMatch:"/branch", configuration: branch => {
        branch.UseMiddleware<QueryStringMiddleWare>();
        branch.Run(async context =>
        {
            await context.Response.WriteAsync($"Branch Middleware");
        });
    });

app.MapWhen(context =>
        context.Request.Query.Keys.Contains("branch2") || context.Request.Path.ToString().Contains("branch2"),
    branch => {
        // ...add middleware components here...
        branch.UseMiddleware<QueryStringMiddleWare>();
        branch.Run(async context =>
        {
            await context.Response.WriteAsync($"Branch Middleware 2");
        });
    }); */


/* app.Use(async (context, next) =>
{
    if (context.Request.Path == "/branch")
    {
        app.UseMiddleware<QueryStringMiddleWare>();
        app.Run(async context2 =>
        {
            await context2.Response.WriteAsync($"Branch Middleware");
            await next();
        });
    }
    await next();
}); */

/* app.Use(async (context, next) =>
{
    // await context.Response.WriteAsync("Demo Header\n");
    await next();
    await context.Response
        .WriteAsync($"Status Code: {context.Response.StatusCode}\n");
}); */

/* app.Use(async (context, next) => {
    if (context.Request.Path == "/short") {
        await context.Response
            .WriteAsync($"Request Short Circuited");
    } else {
        await next();
    }
}); */


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
// app.UseMiddleware<CheckImagePathMiddleWare>();

app.MapGet("/", () => "Hello ASP.NET!\n");

// app.Use(async (context, next) => await next());

app.Run();