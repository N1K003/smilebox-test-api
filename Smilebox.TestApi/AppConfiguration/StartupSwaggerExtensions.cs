using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Smilebox.TestApi.AppConfiguration
{
    public static class StartupSwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IMvcCoreBuilder mvcBuilder)
        {
            services.AddSwaggerGen(c =>
            {
                var contact = new Contact
                {
                    Name = "Nick Kuleba",
                    Url = "skype:n1k003?chat"
                };
                c.SwaggerDoc(
                    "TestApi",
                    new Info
                    {
                        Version = "1.0",
                        Title = "Smilebox Test API",
                        Description = "Smilebox Test API developer documentation",
                        Contact = contact
                    });

                c.DescribeAllEnumsAsStrings();
                c.UseReferencedDefinitionsForEnums();
                c.DescribeAllParametersInCamelCase();
                c.DescribeStringEnumsInCamelCase();

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                c.EnableAnnotations();
            });

            mvcBuilder.AddApiExplorer();

            return services;
        }


        public static IApplicationBuilder UseSwaggerAndConfigure(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Smilebox Test API documentation";
                c.SwaggerEndpoint("/swagger/TestApi/swagger.json", "Smilebox Test API");
                c.DisplayRequestDuration();
                c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
                c.DocExpansion(DocExpansion.List);
                c.DefaultModelRendering(ModelRendering.Model);
                c.DefaultModelExpandDepth(10);
                c.DefaultModelsExpandDepth(10);
                c.ShowExtensions();
            });

            return applicationBuilder;
        }
    }
}