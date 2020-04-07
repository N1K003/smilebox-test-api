using System.Collections.Generic;
using System.Globalization;
using AngleSharp.Html;
using FluentValidation.AspNetCore;
using Ganss.XSS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Smilebox.BusinessLogic.Extensions;
using Smilebox.Data.EntityFramework;
using Smilebox.TestApi.AppConfiguration;
using Smilebox.TestApi.Infrastructure;
using Smilebox.TestApi.Infrastructure.Filters;
using Smilebox.TestApi.Infrastructure.Middleware;

namespace Smilebox.TestApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        private static IConfigurationRoot Configuration { get; set; }
        private static IHostingEnvironment HostingEnvironment { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddRouting();

            var mvcBuilder = services.AddMvcCore(options => { options.Filters.Add(new ValidationFilter()); }
                )
                .AddDataAnnotations()
                .AddJsonFormatters(settings =>
                {
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.Converters = new List<JsonConverter> {new StringEnumConverter()};
                    settings.Formatting =
                        HostingEnvironment.IsDevelopment() ? Formatting.Indented : Formatting.None;
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddDatabase(Configuration)
                .AddBusinessLogic()
                .AddSingleton<IHtmlSanitizer>(_ =>
                {
                    var sanitizer = new HtmlSanitizer {OutputFormatter = new HtmlMarkupFormatter()};
                    sanitizer.AllowedTags.Remove("a");
                    sanitizer.AllowedAttributes.Remove("href");
                    return sanitizer;
                });

            services.AddSwagger(mvcBuilder);
            services.AddCors(
                options => options.AddPolicy(
                    Constants.CorsPolicyName,
                    policyBuilder => policyBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(Constants.CorsPolicyName);
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseRequestLocalization(
                new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(CultureInfo.InvariantCulture, CultureInfo.InvariantCulture),
                    SupportedCultures = new List<CultureInfo> {CultureInfo.InvariantCulture},
                    SupportedUICultures = new List<CultureInfo> {CultureInfo.InvariantCulture},
                    FallBackToParentCultures = true,
                    FallBackToParentUICultures = true,
                    RequestCultureProviders = new List<IRequestCultureProvider>()
                });
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseSwaggerAndConfigure();

            app.UseMvc();

            app.ApplyDbMigrations();
        }
    }
}