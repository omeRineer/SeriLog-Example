using Serilog;
using Serilog.Core;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger=new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * Aþaðýdaki kod bloðundda AddLogging metodunun 2 varyasyonu vardýr
 * AddLogging() : .Net Core üzerindeki mevcut loglama yapýsýný servislere ekler. aslýnda bu kod yazýlmasada
 *                host yapýsý ile default loglama ayaða kalkar.
 *                
 * AddLogging(ILoggerFactory) : Yeni bir loglama servisi eklemek için kulanýlýr. Fakat .Net Core daki default
 *                              loglama servisini ezmez sadece loglama servis saðlayýcýsýna yeni bir loglama
 *                              servisini ekler.Yani bu þekilde kullanýldýðýnda hem .Net Core kendi loglamasýný
 *                              yaparken hemde ayrýca serilog kendi loglamasýný çalýþtýrýr.
 */
builder.Services.AddLogging(x =>
{
    x.AddSerilog();
});






/*
  UseSerilog metodu serilog kütüphanesiyle gelen ve loglama yapýsýný sadece servis olarak deðil host
  yapýsý ile uygulama ayaða kalktýðýnda çalýþmaya baþlayacaktýr. Ayrýca dependency injection ile 
  ILogger ile manuel loglama gerçekleþtirilebilir.Bu yöntem diðer loglama yapýlarýný ezerek sadece serilog ile
  loglama altyapýsýnýn oluþturulmasýný saðlar.
 */
builder.Host.UseSerilog(Log.Logger);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
