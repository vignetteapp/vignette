// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Vignette.Core.Extensions.Vendor;

namespace Vignette.Core.Extensions.Host
{
    public abstract class HostExtension : Extension
    {
        public override string Author { get; } = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company;
        public override Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;
        public virtual ExtensionIntents Intent => ExtensionIntents.None;

        protected override void Initialize()
        {
            foreach (var method in GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                var chan = method.GetCustomAttribute<ListenAttribute>();

                if (chan == null)
                    continue;

                Register(chan.Name, new Dispatcher(GetType(), method));
            }
        }

        protected sealed override void Destroy()
        {
            throw new InvalidOperationException(@"Built-in extensions cannot be disabled.");
        }

        public sealed override Task<bool> TryDispatchAsync(IExtension actor, string channel, out object value, CancellationToken token = default, params object[] args)
        {
            if (Intent != ExtensionIntents.None)
            {
                if (actor is VendorExtension vendor && !vendor.Intents.HasFlag(Intent))
                {
                    value = new InsufficientIntentsException(@"Failed to dispatch as actor lacks proper intents to perform this action.");
                    return Task.FromResult(false);
                }
            }

            return base.TryDispatchAsync(actor, channel, out value, token, args);
        }

        protected sealed override object Invoke(object method, params object[] args)
            => (method as Dispatcher)?.Invoke(this, args);

        [AttributeUsage(AttributeTargets.Method)]
        protected class ListenAttribute : Attribute
        {
            public string Name { get; set; }

            public ListenAttribute(string name)
            {
                Name = name;
            }
        }

        private class Dispatcher
        {
            private readonly Func<object, object[], object> dispatch;

            public Dispatcher(Type type, MethodInfo method)
            {
                var argsExpression = Expression.Parameter(typeof(object[]), "Params");
                var targetExpression = Expression.Parameter(typeof(object), "Target");
                var paramExpressions = method.GetParameters().Select((p, i) =>
                {
                    var constExpression = Expression.Constant(i, typeof(int));
                    var indexExpression = Expression.ArrayIndex(argsExpression, constExpression);
                    return Expression.Convert(indexExpression, p.ParameterType);
                });
                var invokeExpression = Expression.Call(Expression.Convert(targetExpression, type), method, paramExpressions);

                LambdaExpression lambdaExpression;

                if (method.ReturnType != typeof(void))
                {
                    lambdaExpression = Expression.Lambda(Expression.Convert(invokeExpression, typeof(object)), targetExpression, argsExpression);
                }
                else
                {
                    var nullExpression = Expression.Constant(null, typeof(object));
                    var bodyExpression = Expression.Block(invokeExpression, nullExpression);
                    lambdaExpression = Expression.Lambda(bodyExpression, targetExpression, argsExpression);
                }

                dispatch = (Func<object, object[], object>)lambdaExpression.Compile();
            }

            public object Invoke(object target, object[] args)
                => dispatch(target, args);
        }
    }

    public class InsufficientIntentsException : Exception
    {
        public InsufficientIntentsException(string message)
            : base(message)
        {
        }
    }
}
