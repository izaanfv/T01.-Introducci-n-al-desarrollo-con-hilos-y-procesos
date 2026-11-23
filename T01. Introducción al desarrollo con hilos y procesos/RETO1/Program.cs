using System;
using System.Threading;

Console.WriteLine($"[Main - ID {Environment.CurrentManagedThreadId}] Inicio del programa");

var hilo = new Thread(() =>
{
    for (int i = 1; i <= 5; i++)
    {
        Console.WriteLine($"[Hilo - ID {Environment.CurrentManagedThreadId}] Número: {i}");
        Thread.Sleep(500);
    }
});

hilo.Start(); // Arranca el hilo secundario sin detener Main

// Si pusiéramos hilo.Join(); aquí, Main esperaría antes de mostrar su mensaje
Console.WriteLine($"[Main - ID {Environment.CurrentManagedThreadId}] Mensaje propio del hilo principal");

hilo.Join(); // Espera al final a que el hilo secundario termine
Console.WriteLine($"[Main - ID {Environment.CurrentManagedThreadId}] Fin del programa");