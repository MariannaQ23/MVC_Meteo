using SoapCore;
using static SOAP.BusinessLogic.SOAPService;
/*
namespace Soap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
*/


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSoapCore();
builder.Services.AddScoped<ISoapService, SoapService>();



var app = builder.Build();

app.UseRouting();

//configuro l'endpoint per il servizio SoapService
app.UseEndpoints(Endpoints =>
{
    Endpoints.UseSoapEndpoint<ISoapService>("/Service.wsdl", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}



app.Run();