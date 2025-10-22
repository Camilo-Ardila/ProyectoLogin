using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebApplication1.Models
{
    public class ReservaModel
    {
        private static readonly string FilePath = @"C:\Users\camil\Desktop\ProyectoLogin\LoginProject\WebApplication1\Models\reservas.txt";

        public int IdUsuario { get; set; } // User's ID from UserModel
        public int IdEspacio { get; set; } // Space ID from SpaceModel (generalized)
        public DateTime DiaReserva { get; set; } // Reservation day

        public ReservaModel() { }

        public ReservaModel(int idUsuario, int idEspacio, DateTime diaReserva)
        {
            IdUsuario = idUsuario;
            IdEspacio = idEspacio;
            DiaReserva = diaReserva.Date; // Ensure only date part is stored
        }

        private static List<ReservaModel> LoadReservas()
        {
            var reservas = new List<ReservaModel>();
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || !line.Contains(",")) continue;
                    var parts = line.Split(new[] { ',' }, 3); // Split into 3 parts: IdUsuario, IdEspacio, DiaReserva
                    if (parts.Length == 3)
                    {
                        if (int.TryParse(parts[0].Trim(), out int idUsuario) &&
                            int.TryParse(parts[1].Trim(), out int idEspacio) &&
                            DateTime.TryParse(parts[2].Trim().TrimEnd(';'), out DateTime diaReserva))
                        {
                            var reserva = new ReservaModel(idUsuario, idEspacio, diaReserva);
                            reservas.Add(reserva);
                        }
                    }
                }
            }
            return reservas;
        }

        private static void SaveReservas(List<ReservaModel> reservas)
        {
            var lines = reservas.Select(r => $"{r.IdUsuario}, {r.IdEspacio}, {r.DiaReserva:yyyy-MM-dd};");
            File.WriteAllLines(FilePath, lines);
        }

        public static ReservaModel AddReserva(int idUsuario, int idEspacio, DateTime diaReserva)
        {

            Console.WriteLine(SpaceModel.Spaces);

            // Check if the space exists
            var space = SpaceModel.GetSpaceById(idEspacio);
            if (space == null)
            {
                Console.WriteLine($"Rechazada: Espacio con Id {idEspacio} no encontrado.");
                return null; // Space not found
            }

            Console.WriteLine($"Espacio encontrado: {space.Name} (Id: {space.Id})");

            // Validate past date
            if (diaReserva.Date < DateTime.Now.Date)
            {
                Console.WriteLine("Rechazada: Fecha pasada seleccionada.");
                return null; // Reject reservation for previous dates
            }

            // Load current reservations from file
            var currentReservas = LoadReservas();
            Console.WriteLine("Reservas cargadas desde archivo.");

            // Check if the space is already reserved for that day
            if (currentReservas.Any(r => r.IdEspacio == idEspacio && r.DiaReserva.Date == diaReserva.Date))
            {
                Console.WriteLine("Rechazada: Espacio ya reservado para ese día.");
                return null; // Reject if already reserved
            }

            Console.WriteLine("Espacio disponible para el día seleccionado.");

            // Create and save the reservation
            var nuevaReserva = new ReservaModel(idUsuario, idEspacio, diaReserva);
            currentReservas.Add(nuevaReserva);
            space.Status = "Not Available"; // Update status after reservation
            space.Disponible_Dia = diaReserva.AddDays(1); // Update available date
            SpaceModel.SaveSpaces(); // Update space status
            SaveReservas(currentReservas); // Save updated reservations
            Console.WriteLine("Reserva exitosa y guardada.");
            return nuevaReserva;
        }
    }
}