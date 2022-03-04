using Microsoft.EntityFrameworkCore;

using FastEndpoints;

using Pasta.Shared;

// using System.Text.Json;
// using Pasta.Shared.Enums;
// using Pasta.Shared.Requests;
//
// Console.WriteLine(JsonSerializer.Serialize(new JobRequest
// {
//     Webhooks =
//     {
//         new WebhookRequest
//         {
//             Type = Enum.GetName(WebhookType.Slack)!,
//             Address = "https://google.com"
//         }
//     }
// }));
// Environment.Exit(0);

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(nameof(Pasta)));
#else
builder.Services.AddSqlite<ApplicationDbContext>("DataSource=app.db;Cache=Shared", options =>
{
    options.CommandTimeout(6);
});
#endif
builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseFastEndpoints();

// #region Jobs Endpoints
//
// app.MapPost("/jobs", () =>
// {
//     
// });
//
// app.MapGet("/jobs", () =>
// {
//     
// });
//
// app.MapGet("/jobs/{guid:guid}", (Guid guid) =>
// {
//     
// });
//
// app.MapDelete("/jobs/{guid:guid}", (Guid guid) =>
// {
//     
// });
//
// #endregion
//
// #region Reports Endpoints
//
// app.MapGet("/reports", () =>
// {
//     
// });
//
// app.MapGet("/reports/{guid:guid}", (Guid guid) =>
// {
//     
// });
//
// app.MapDelete("/reports/{guid:guid}", (Guid guid) =>
// {
//     
// });
//
// #endregion
//
// #region Webhooks Endpoints
//
// app.MapPost("/webhooks", () =>
// {
//     
// });
//
// app.MapGet("/webhooks", () =>
// {
//     
// });
//
// app.MapGet("/webhooks/{guid:guid}", (Guid guid) =>
// {
//     
// });
//
// app.MapDelete("/webhooks/{guid:guid}", (Guid guid) =>
// {
//     
// });
//
// app.MapMethods("/webhooks/{guid:guid}", new []{"PATCH"} ,(Guid guid) =>
// {
//     
// });
//
// #endregion

app.Run();