using System;
using System.Diagnostics;
using System.Threading.Tasks;

static async Task DescargarAsync(string archivo, int segundos)
{
    Console.WriteLine($"Descargando {archivo} ({segundos}s)...");
    await Task.Delay(segundos * 1000);
    Console.WriteLine($"{archivo} completado.");
}

Console.WriteLine("=== 1. UNA DETRÁS DE OTRA (Secuencial) ===");
var sw = Stopwatch.StartNew();
await DescargarAsync("Archivo1", 1);
await DescargarAsync("Archivo2", 2);
await DescargarAsync("Archivo3", 3);
sw.Stop();
Console.WriteLine($"Tiempo total secuencial: {sw.ElapsedMilliseconds} ms (~{sw.Elapsed.TotalSeconds:F1} s)\n");

Console.WriteLine("=== 2. TODAS A LA VEZ (Task.WhenAll) ===");
sw.Restart();
Task t1 = DescargarAsync("Archivo1", 1);
Task t2 = DescargarAsync("Archivo2", 2);
Task t3 = DescargarAsync("Archivo3", 3);

await Task.WhenAll(t1, t2, t3);
sw.Stop();
Console.WriteLine($"Tiempo total con Task.WhenAll: {sw.ElapsedMilliseconds} ms (~{sw.Elapsed.TotalSeconds:F1} s)");