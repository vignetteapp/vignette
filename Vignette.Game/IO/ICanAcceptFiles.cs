// Copyright 2020 - 2021 Vignette Project
// Licensed under NPOSLv3. See LICENSE for details.

using System.Collections.Generic;

namespace Vignette.Game.IO
{
    /// <summary>
    /// An interface that marks that this component can accept files dropped to the window.
    /// </summary>
    public interface ICanAcceptFiles
    {
        /// <summary>
        /// Extensions that the component can accept.
        /// </summary>
        IEnumerable<string> Extensions { get; }

        /// <summary>
        /// Called when a drag and drop event has happened.
        /// </summary>
        /// <param name="files">A list of file paths the component has received.</param>
        void FileDropped(IEnumerable<string> files);
    }
}
