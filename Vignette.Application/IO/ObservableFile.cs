// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

namespace Vignette.Application.IO
{
    public class ObservableFile
    {
        public string Name { get; }

        public ObservableFile(string name)
        {
            Name = name;
        }
    }

    public class ObservableFile<T> : ObservableFile
        where T : class
    {
        public T Data { get; }

        public ObservableFile(string name, T data)
            : base(name)
        {
            Data = data;
        }

        public override string ToString() => Name;

        public static implicit operator T(ObservableFile<T> observable) => observable?.Data ?? null;
    }
}
