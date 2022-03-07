// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System.Threading.Tasks;

namespace Vignette.Core.Util
{
    public static class TaskExtensions
    {
        public static object GetResult(this Task task)
        {
            var type = task.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
            {
                return type.GetProperty("Result").GetValue(task);
            }
            else
            {
                return null;
            }
        }
    }
}
