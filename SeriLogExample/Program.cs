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
 * A�a��daki kod blo�undda AddLogging metodunun 2 varyasyonu vard�r
 * AddLogging() : .Net Core �zerindeki mevcut loglama yap�s�n� servislere ekler. asl�nda bu kod yaz�lmasada
 *                host yap�s� ile default loglama aya�a kalkar.
 *                
 * AddLogging(ILoggerFactory) : Yeni bir loglama servisi eklemek i�in kulan�l�r. Fakat .Net Core daki default
 *                              loglama servisini ezmez sadece loglama servis sa�lay�c�s�na yeni bir loglama
 *                              servisini ekler.Yani bu �ekilde kullan�ld���nda hem .Net Core kendi loglamas�n�
 *                              yaparken hemde ayr�ca serilog kendi loglamas�n� �al��t�r�r.
 */
builder.Services.AddLogging(x =>
{
    x.AddSerilog();
});






/*
  UseSerilog metodu serilog k�t�phanesiyle gelen ve loglama yap�s�n� sadece servis olarak de�il host
  yap�s� ile uygulama aya�a kalkt���nda �al��maya ba�layacakt�r. Ayr�ca dependency injection ile 
  ILogger ile manuel loglama ger�ekle�tirilebilir.Bu y�ntem di�er loglama yap�lar�n� ezerek sadece serilog ile
  loglama altyap�s�n�n olu�turulmas�n� sa�lar.
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
