using System;
using System.Diagnostics;
using System.Threading;

object cerrojo = new object();

Console.WriteLine("=== 1. SIN PROTECCIÓN (100.000 por hilo -> debería dar 200.000) ===");
for (int intento = 1; intento <= 5; intento++)
{
    int contador = 0;
    var sw = Stopwatch.StartNew();
    var h1 = new Thread(() => { for (int i = 0; i < 100_000; i++) contador++; });
    var h2 = new Thread(() => { for (int i = 0; i < 100_000; i++) contador++; });
    h1.Start(); h2.Start();
    h1.Join(); h2.Join();
    sw.Stop();
    Console.WriteLine($"Ejecución {intento}: {contador:N0} ({sw.ElapsedMilliseconds} ms)");
}

Console.WriteLine("\n=== 2. SIN PROTECCIÓN (1.000.000 por hilo -> debería dar 2.000.000) ===");
for (int intento = 1; intento <= 5; intento++)
{
    int contador = 0;
    var sw = Stopwatch.StartNew();
    var h1 = new Thread(() => { for (int i = 0; i < 1_000_000; i++) contador++; });
    var h2 = new Thread(() => { for (int i = 0; i < 1_000_000; i++) contador++; });
    h1.Start(); h2.Start();
    h1.Join(); h2.Join();
    sw.Stop();
    Console.WriteLine($"Ejecución {intento}: {contador:N0} ({sw.ElapsedMilliseconds} ms)");
}

Console.WriteLine("\n=== 3. CON LOCK (1.000.000 por hilo -> debería dar 2.000.000) ===");
for (int intento = 1; intento <= 5; intento++)
{
    int contador = 0;
    var sw = Stopwatch.StartNew();
    var h1 = new Thread(() => { for (int i = 0; i < 1_000_000; i++) lock (cerrojo) { contador++; } });
    var h2 = new Thread(() => { for (int i = 0; i < 1_000_000; i++) lock (cerrojo) { contador++; } });
    h1.Start(); h2.Start();
    h1.Join(); h2.Join();
    sw.Stop();
    Console.WriteLine($"Ejecución {intento}: {contador:N0} ({sw.ElapsedMilliseconds} ms)");
}

Console.WriteLine("\n=== 4. CON INTERLOCKED (1.000.000 por hilo -> debería dar 2.000.000) ===");
for (int intento = 1; intento <= 5; intento++)
{
    int contador = 0;
    var sw = Stopwatch.StartNew();
    var h1 = new Thread(() => { for (int i = 0; i < 1_000_000; i++) Interlocked.Increment(ref contador); });
    var h2 = new Thread(() => { for (int i = 0; i < 1_000_000; i++) Interlocked.Increment(ref contador); });
    h1.Start(); h2.Start();
    h1.Join(); h2.Join();
    sw.Stop();
    Console.WriteLine($"Ejecución {intento}: {contador:N0} ({sw.ElapsedMilliseconds} ms)");
}