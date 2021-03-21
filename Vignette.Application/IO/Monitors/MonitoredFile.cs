// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

namespace Vignette.Application.IO.Monitors
{
    public class MonitoredFile
    {
        public string Name { get; }

        public MonitoredFile(string name)
        {
            Name = name;
        }
    }

    public class MonitoredFile<T> : MonitoredFile
        where T : class
    {
        public T Data { get; }

        public MonitoredFile(string name, T data)
            : base(name)
        {
            Data = data;
        }

        public override string ToString() => Name;
    }
}
