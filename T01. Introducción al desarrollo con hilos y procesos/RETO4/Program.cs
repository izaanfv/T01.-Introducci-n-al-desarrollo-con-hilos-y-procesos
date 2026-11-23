using System;
using System.ComponentModel;
using System.Diagnostics;

Console.WriteLine("=== 1. EJECUTAR PROGRAMA EXTERNO (dotnet --version) ===");

var proceso = new Process();
proceso.StartInfo.FileName = "dotnet";
proceso.StartInfo.Arguments = "--version";
proceso.StartInfo.RedirectStandardOutput = true; // Captura el texto que imprime
proceso.StartInfo.UseShellExecute = false;       // Necesario para capturar la salida
proceso.StartInfo.CreateNoWindow = true;

proceso.Start();

string salida = proceso.StandardOutput.ReadToEnd();
proceso.WaitForExit(); // Espera a que el programa externo termine

Console.WriteLine($"Salida capturada: {salida.Trim()}");
Console.WriteLine($"Código de salida (ExitCode): {proceso.ExitCode}");

Console.WriteLine("\n=== 2. PROGRAMA QUE NO EXISTE ===");
try
{
    var procesoFalso = new Process();
    procesoFalso.StartInfo.FileName = "programa_que_no_existe_123";
    procesoFalso.StartInfo.UseShellExecute = false;
    procesoFalso.Start();
    procesoFalso.WaitForExit();
}
catch (Win32Exception ex)
{
    Console.WriteLine($"Fallo al iniciar: {ex.Message}");
}