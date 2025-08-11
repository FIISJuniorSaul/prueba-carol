using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GestorTareas
{
    // Clase que representa una tarea
    public class Tarea
    {
        public string Titulo { get; set; }
        public bool Completada { get; set; }

        public override string ToString()
        {
            return $"{Titulo} - {(Completada ? "✔ Completada" : "Pendiente")}";
        }
    }

    // Clase principal del programa
    class Program
    {
        static string rutaArchivo = "tareas.json";
        static List<Tarea> tareas = new List<Tarea>();

        static void Main(string[] args)
        {
            CargarTareas();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTOR DE TAREAS ===");
                Console.WriteLine("1. Ver tareas");
                Console.WriteLine("2. Agregar tarea");
                Console.WriteLine("3. Marcar como completada");
                Console.WriteLine("4. Guardar y salir");
                Console.Write("Seleccione una opción: ");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MostrarTareas();
                        break;
                    case "2":
                        AgregarTarea();
                        break;
                    case "3":
                        CompletarTarea();
                        break;
                    case "4":
                        GuardarTareas();
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MostrarTareas()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE TAREAS ===");
            if (tareas.Count == 0)
            {
                Console.WriteLine("No hay tareas registradas.");
            }
            else
            {
                for (int i = 0; i < tareas.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tareas[i]}");
                }
            }
            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }

        static void AgregarTarea()
        {
            Console.Clear();
            Console.Write("Ingrese el título de la nueva tarea: ");
            string titulo = Console.ReadLine();
            tareas.Add(new Tarea { Titulo = titulo, Completada = false });
            Console.WriteLine("Tarea agregada correctamente.");
            Console.ReadKey();
        }

        static void CompletarTarea()
        {
            Console.Clear();
            MostrarTareas();
            Console.Write("\nIngrese el número de la tarea a completar: ");
            if (int.TryParse(Console.ReadLine(), out int numero) && numero >= 1 && numero <= tareas.Count)
            {
                tareas[numero - 1].Completada = true;
                Console.WriteLine("Tarea marcada como completada.");
            }
            else
            {
                Console.WriteLine("Número inválido.");
            }
            Console.ReadKey();
        }

        static void GuardarTareas()
        {
            var opcionesJson = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(rutaArchivo, JsonSerializer.Serialize(tareas, opcionesJson));
            Console.WriteLine("Tareas guardadas en el archivo.");
        }

        static void CargarTareas()
        {
            if (File.Exists(rutaArchivo))
            {
                string json = File.ReadAllText(rutaArchivo);
                tareas = JsonSerializer.Deserialize<List<Tarea>>(json) ?? new List<Tarea>();
            }
        }
    }
}
