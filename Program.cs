using LinkApi.Data;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IConnectionMultiplexer>(opt => 
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));

builder.Services.AddScoped<ILinkRepo, LinkRepo>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapPost("api/v1/link/pack/",(ILinkRepo _repo, string link) =>{
    var guid =  _repo.PackLink(link);
    return Results.Ok(guid);
}
);
app.MapGet("api/v1/link/{guid}/unpack",(ILinkRepo _repo, string guid) =>{
    var link = _repo.UnpackLink(guid);
    if (link != null)
    {
        return Results.Ok(link);
    }
    return Results.NotFound();
});
app.MapGet("api/v1/stat", (ILinkRepo _repo)=>{
    return Results.Ok(_repo.CountNumbersOfLinks());
});
app.MapDelete("api/v1/link/{guid}", (ILinkRepo _repo, string guid)=>{
    var deleted = _repo.DeleteLink(guid);
    if (deleted != null)
    {
        return Results.Ok(deleted);
    }
    return Results.NotFound();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

