using System;
using System.Collections.Generic;
using System.IO;

namespace ClinicaVeterinaria{
    // Interfaces

    public interface IRegistrable
    {
        void Registrar();
    }

    public interface IAtendible
    {
        void Atender();
    }

    public interface INotificable
    {
        void EnviarNotificacion(string mensaje);
    }


    // Excepción personalizada

    public class MascotaNoEncontradaException : Exception
    {
        public MascotaNoEncontradaException(string nombre)
            : base($"La mascota '{nombre}' no fue encontrada en el sistema.") { }
    }


    // Clases principales

    public class Paciente : IRegistrable, INotificable
    {
        public string Nombre { get; set; }

        public Paciente(string nombre)
        {
            Nombre = nombre;
        }

        public void Registrar()
        {
            Console.WriteLine($"Paciente {Nombre} registrado en el sistema.");
        }

        public void EnviarNotificacion(string mensaje)
        {
            Console.WriteLine($"[Notificación a {Nombre}] {mensaje}");
        }
    }

    public class Mascota : IRegistrable
    {
        public string Nombre { get; set; }

        public Mascota(string nombre)
        {
            Nombre = nombre;
        }

        public void Registrar()
        {
            Console.WriteLine($"Mascota {Nombre} registrada en el sistema.");
        }
    }

    public class ConsultaGeneral : IAtendible
    {
        public void Atender()
        {
            Console.WriteLine("Se realiza una consulta general.");
        }
    }

    public class Vacunacion : IAtendible
    {
        public void Atender()
        {
            Console.WriteLine("Se aplica una vacuna al paciente.");
        }
    }


    // Sistema con logging básico
 
    public static class Logger
    {
        private static string logFile = "errores.log";

        public static void Log(string mensaje)
        {
            Console.WriteLine($"[LOG] {mensaje}");
            File.AppendAllText(logFile, $"{DateTime.Now}: {mensaje}{Environment.NewLine}");
        }
    }


    // Programa principal

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Breakpoint sugerido: verificar estado inicial
                Paciente paciente = new Paciente("Carlos");
                Mascota mascota = new Mascota("Firulais");

                paciente.Registrar();
                mascota.Registrar();

                paciente.EnviarNotificacion("Recuerde su cita mañana a las 9 AM.");

                // Servicios
                IAtendible consulta = new ConsultaGeneral();
                IAtendible vacuna = new Vacunacion();

                consulta.Atender();
                vacuna.Atender();

                // Forzar error para probar manejo de excepciones
                Console.WriteLine("Intentando buscar una mascota inexistente...");
                BuscarMascota("Desconocido");

            }
            catch (MascotaNoEncontradaException ex)
            {
                Logger.Log(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Log($"Error inesperado: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Ejecución finalizada (bloque finally).");
            }
        }

        // Método que fuerza excepción personalizada
        static void BuscarMascota(string nombre)
        {
            // Breakpoint sugerido: ver valor de 'nombre'
            throw new MascotaNoEncontradaException(nombre);
        }
    }
}
