﻿using Humanizer;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Sri.Bolid.Shared
{
    [Serializable]
    public class CarParams
    {
        public const decimal MaxPressure = 3;
        public const decimal MinPressure = 1.5M;
        public const decimal MinTemperature = 30;
        public const decimal MaxTemperature = 200;

        public TimeSpan RaceTime { get; set; }

        public decimal TyresPressure { get; set; }

        public decimal RadiatorFluidTemperature { get; set; }

        public decimal EngineTemperature { get; set; }

        public override string ToString()
        {
            string carParams = $"Tyres press.: {TyresPressure} Radiator fluid temp.: {RadiatorFluidTemperature} Engine temp.: {EngineTemperature}";
            var carParamsFull = this.RaceTime.Milliseconds == 0 ? carParams : $"[{this.RaceTime.Humanize()}] {carParams}";
            return carParamsFull;
        }

        public static byte[] Serialize(CarParams carParams)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, carParams);
                stream.Flush();
                stream.Position = 0;
                return stream.ToArray();
            }
        }

        public static CarParams Deserialize(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (CarParams)formatter.Deserialize(stream);
            }
        }

        public static void Print(WarningLevel warningLevel, string text)
        {
            switch (warningLevel)
            {
                case WarningLevel.Lv1:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case WarningLevel.Lv2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.Write(text);
            Console.Write(" ");

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
