using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Platform;
using Platform.Middlewares;
using Platform.Models;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.Configure<MessageOptions>(options => {});
/* builder.Services.Configure<MessageOptions>(options =>
{
    options.CityName= "Odessa";
}); */


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

/* app.UseMiddleware<LocationMiddleware>();

app.Map(pathMatch:"/branch", configuration:branch => {
    branch.Run(new QueryStringMiddleWare().Invoke);
}); */

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

// app.MapGet("/", () => "Hello ASP.NET!\n");

// app.Use(async (context, next) => await next());

// app.UseMiddleware<Population>();
// app.UseMiddleware<Capital>();

app.UseRouting();
app.UseEndpoints(endpoints => {
    /* endpoints.MapGet("routing", async context => {
        await context.Response.WriteAsync("Request Was Routed");
    });
    endpoints.MapGet("capital/uk", new Capital().Invoke);
    endpoints.MapGet("population/paris", new Population().Invoke); */
    /* endpoints.MapGet("{first}/{second}/{third}", async context
        => {
        await context.Response.WriteAsync("Request Was Routed\n");
        foreach (var kvp in context.Request.RouteValues) {
            await context.Response
                .WriteAsync($"{kvp.Key}: {kvp.Value}\n");}
    }); */
    // TODO
    // 1. создать класс новой конечной точки на одну из тем:
    // - узнать название государственного языка
    // - узнать название государства по площади (указывать целую составляющую в км^2),
    // тогда при попадании на площать Антарктиды возвращать строку "Антарктида",
    // а при попадании на площадь Монако -
    // выполнять перенаправление на уже существующую конечную точку с шаблоном адреса capital/{country},
    // передавая через анонимный объект значение country = "monaco"
    // 2. перенаправление на capital/{country} для площади Монако
    // должно происходить даже тогда, когда в исходном коде проекта шаблон конечной точки capital/{country} изменится
    // на c/{country}
    endpoints.MapGet("capital/{country}", Capital.Endpoint);
    endpoints.MapGet("size/{city}", Population.Endpoint)
        .WithMetadata(new RouteNameMetadata("population"));
});

app.Run(async context => {
    await context.Response.WriteAsync("Terminal Middleware Reached");
});
app.Run();

