﻿namespace CloudHRMS.WebAPIs.ConfigSwaggerOptions {
    using Asp.Versioning.ApiExplorer;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    namespace CloudHRMS.WebAPIs.Configurations {
        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions> {
            private readonly IApiVersionDescriptionProvider _provider;

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) {
                _provider = provider;
            }
            public void Configure(SwaggerGenOptions options) {
                foreach (var description in _provider.ApiVersionDescriptions) {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo {
                        Title = $"CloudHRMS API",
                        Version = description.ApiVersion.ToString(),
                        Description = "API documentation for CloudHRMS",
                        Contact = new OpenApiContact {
                            Name = "ProDev ICT Solution",
                            Email = "mr.kyaing7@gmail.com"
                        }
                    });
                }
            }
        }
    }

}
