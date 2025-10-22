using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApplication1.Models
{
    public class SpaceModel
    {
        public static List<SpaceModel> Spaces = LoadSpaces();
        private static int _nextId = 1;
        private static readonly string FilePath = @"C:\Users\camil\Desktop\ProyectoLogin\LoginProject\WebApplication1\Models\spaces.txt";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // e.g., "Aula", "Cancha", "Laboratorio"
        public string Status { get; set; } // "Available" or "Not Available"
        public DateTime Disponible_Dia { get; set; } // When it becomes free again

        public SpaceModel() { }

        public SpaceModel(string name, string type, string status, DateTime disponible_dia)
        {
            Id = _nextId++;
            Name = name;
            Type = type;
            Status = status;
            Disponible_Dia = disponible_dia;
        }

        public static List<SpaceModel> LoadSpaces()
        {
            var spaces = new List<SpaceModel>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || !line.Contains(",")) continue;
                    var parts = line.Split(new[] { ',' }, 5); // Split into 5 parts: Id, Name, Type, Status, Disponible_Dia
                    if (parts.Length == 5)
                    {
                        if (int.TryParse(parts[0].Trim(), out int id) &&
                            DateTime.TryParse(parts[4].Trim().TrimEnd(';'), out DateTime disponibleDia))
                        {
                            var space = new SpaceModel
                            {
                                Id = id,
                                Name = parts[1].Trim(),
                                Type = parts[2].Trim(),
                                Status = parts[3].Trim(),
                                Disponible_Dia = disponibleDia
                            };
                            spaces.Add(space);
                            _nextId = Math.Max(_nextId, id + 1); // Update _nextId
                        }
                        else
                        {
                            Console.WriteLine($"Error parsing line: {line}");
                        }
                    }
                }
                Console.WriteLine($"Loaded {spaces.Count} spaces from {FilePath}");
            }
            else
            {
                Console.WriteLine($"File not found: {FilePath}");
            }
            return spaces;
        }

        public static void SaveSpaces()
        {
            var lines = Spaces.Select(s => $"{s.Id}, {s.Name}, {s.Type}, {s.Status}, {s.Disponible_Dia:yyyy-MM-dd};");
            File.WriteAllLines(FilePath, lines);
            Console.WriteLine($"Saved {Spaces.Count} spaces to {FilePath}");
        }

        public static SpaceModel AddSpace(string name, string type, string status, DateTime disponible_dia)
        {
            var space = new SpaceModel(name, type, status, disponible_dia);
            Spaces.Add(space);
            SaveSpaces();
            return space;
        }

        public static SpaceModel GetSpaceById(int id)
        {
            Console.WriteLine($"Searching for space with Id: {id}, Spaces count: {Spaces.Count}");
            return Spaces.FirstOrDefault(s => s.Id == id);
        }
    }
}