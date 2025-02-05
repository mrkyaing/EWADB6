using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using TodoAPI.Config;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);//This ensures if client doesn't specify an API version. The default version should be considered. 
    opt.AssumeDefaultVersionWhenUnspecified = true;//default API version
    opt.ReportApiVersions = true;//allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),//config for url versioning
                                                    new HeaderApiVersionReader("x-api-version"),//config for header versioning
                                                    new MediaTypeApiVersionReader("x-api-version"));//config for MediaType versioning
});
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";//format the version number “‘v’major[.minor][-status]”
    setup.SubstituteApiVersionInUrl = true;//resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
});
builder.Services.ConfigureOptions<ConfigureSwaggerOption>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(o=>{
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",description.GroupName.ToUpperInvariant());
        }
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
