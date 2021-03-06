// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Monitoring.Performance
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;


    public class MessagePerformanceCounters :
        PerformanceCounters
    {
        public const string CategoryName = "MassTransit Messages";
        public const string CategoryHelp = "Message types consumed by a MassTransit consumer";

        MessagePerformanceCounters()
            : base(CategoryName, CategoryHelp)
        {
        }

        public static CounterCreationData ConsumedPerSecond => Cached.Instance.Value.Data[0];
        public static CounterCreationData TotalReceived => Cached.Instance.Value.Data[1];
        public static CounterCreationData ConsumeDuration => Cached.Instance.Value.Data[2];
        public static CounterCreationData ConsumeDurationBase => Cached.Instance.Value.Data[3];
        public static CounterCreationData Faulted => Cached.Instance.Value.Data[4];
        public static CounterCreationData FaultPercentage => Cached.Instance.Value.Data[5];
        public static CounterCreationData FaultPercentageBase => Cached.Instance.Value.Data[6];

        public static IPerformanceCounter CreateCounter(string counterName, string instanceName)
        {
            return Cached.Instance.Value.CreatePerformanceCounter(counterName, instanceName);
        }

        protected override IEnumerable<CounterCreationData> GetCounterData()
        {
            yield return
                new CounterCreationData("Messages/s", "Number of messages consumed per second", PerformanceCounterType.RateOfCountsPerSecond32);
            yield return
                new CounterCreationData("Total Message", "Total number of messages consumed", PerformanceCounterType.NumberOfItems64);
            yield return
                new CounterCreationData("Average Duration", "The average time spent consuming a message", PerformanceCounterType.AverageCount64);
            yield return
                new CounterCreationData("Average Duration Base", "The average time spent consuming a message", PerformanceCounterType.AverageBase);
            yield return
                new CounterCreationData("Total Faults", "Total number of consumer faults generated", PerformanceCounterType.NumberOfItems64);
            yield return
                new CounterCreationData("Fault %", "The percentage of messages generating faults", PerformanceCounterType.AverageCount64);
            yield return
                new CounterCreationData("Fault % Base", "The percentage of messages generating faults", PerformanceCounterType.AverageBase);
        }

        public static void Install()
        {
            MessagePerformanceCounters value = Cached.Instance.Value;
        }


        static class Cached
        {
            internal static readonly Lazy<MessagePerformanceCounters> Instance = new Lazy<MessagePerformanceCounters>(() => new MessagePerformanceCounters());
        }
    }
}