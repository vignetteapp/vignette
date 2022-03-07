// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ClearScript;
using Stride.Core.Diagnostics;

namespace Vignette.Core.Extensions.Vendor.Modules
{
    public class ConsoleModule : VendorExtensionModule
    {
        public override string Name => @"console";
        private readonly Logger logger;
        private readonly Dictionary<string, Stopwatch> timers = new Dictionary<string, Stopwatch>();
        private readonly Dictionary<string, int> counters = new Dictionary<string, int>();

        public ConsoleModule(VendorExtension extension)
            : base(extension)
        {
            logger = GlobalLogger.GetLogger($"Extensions:{extension.Identifier}");
        }

        [ScriptMember("log")]
        public void Log(object message)
            => logger.Info(message.ToString());

        [ScriptMember("warn")]
        public void Warn(object message)
            => logger.Warning(message.ToString());

        [ScriptMember("error")]
        public void Error(object message)
            => logger.Error(message.ToString());

        [ScriptMember("debug")]
        public void Debug(object message)
            => logger.Debug(message.ToString());

        [ScriptMember("time")]
        public void Time(string label = "default")
        {
            if (timers.ContainsKey(label))
                return;

            var timer = new Stopwatch();
            timers.Add(label, timer);
            timer.Start();
        }

        [ScriptMember("timeLog")]
        public void TimeLog(string label = "default")
        {
            if (!timers.TryGetValue(label, out var timer))
            {
                Warn($@"Timer ""{label}"" doesn't exist.");
                return;
            }

            Log($"{label}: {timer.ElapsedMilliseconds}ms");
        }

        [ScriptMember("timeEnd")]
        public void TimeEnd(string label = "default")
        {
            if (!timers.TryGetValue(label, out var timer))
            {
                Warn($@"Timer ""{label}"" doesn't exist.");
                return;
            }

            timer.Stop();
            timers.Remove(label);
            Log($"{label}: {timer.ElapsedMilliseconds}ms - timer ended");
        }

        [ScriptMember("count")]
        public void Count(string label = "default")
        {
            if (!counters.TryGetValue(label, out int count))
            {
                count = 0;
                counters.Add(label, count);
            }

            count++;
            Log($"{label}: {count}");
        }

        [ScriptMember("countReset")]
        public void CountReset(string label = "default")
        {
            if (!counters.ContainsKey(label))
                return;

            counters[label] = 0;
        }
    }
}
