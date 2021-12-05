using Richter.THEC.Api;
using Richter.THEC.Api.Models;
using Richter_THEC_Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapPost("/api/distance", (TripInput tripInput) =>
{
    return ApiUtilities.GetOptions(tripInput); 
});

app.MapPost("/api/distance/total", (TripInput tripInput) =>
{
    var input = ApiUtilities.ConvertTripInputToTotalByDistance(tripInput);
    var output = TotalByDistance.Predict(input);
    return output.Score;
});

app.MapPost("/api/distance/duration", (TripInput tripInput) =>
{
    var input = ApiUtilities.ConvertTripInputToDurationByDistance(tripInput);
    var output = DurationByDistance.Predict(input);
    return output.Score;
});

app.Run();
