﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Sri.Bolid.Car.Providers;
using Sri.Bolid.Shared;
using Timer = System.Timers.Timer;

namespace Sri.Bolid.Car
{
    class Program
    {
        private const int broadcastTimerInterval = 2000;

        private const int raceTimerInterval = 10;

        private static readonly CarParamsProvider carParamsProvider = new CarParamsProvider();

        private static long raceMiliseconds = 0;

        static void Main(string[] args)
        {

            Timer raceTimer = new Timer(raceTimerInterval);
            raceTimer.Elapsed += (sender, eventArgs) => raceMiliseconds += raceTimerInterval;
            raceTimer.Start();

            Timer broadcastTimer = new Timer(broadcastTimerInterval);
            broadcastTimer.Elapsed += BroadcastCarParams;
            broadcastTimer.Start();

            while (true)
            {
                Thread.Sleep(1);
            }
        }

        private static void BroadcastCarParams(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            CarParams carParams = carParamsProvider.Get();
            carParams.RaceTime = TimeSpan.FromMilliseconds(raceMiliseconds);
            Console.WriteLine(carParams.ToString());
        }
    }
}