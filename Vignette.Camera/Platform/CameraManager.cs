// Copyright 2020 - 2021 Vignette Project
// Licensed under MIT. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using osu.Framework;
using osu.Framework.Bindables;
using osu.Framework.Threading;

namespace Vignette.Camera.Platform
{
    public abstract class CameraManager : IDisposable
    {
        /// <summary>
        /// An <see cref="IEnumerable{string}"/> of camera device names.
        /// </summary>
        public IEnumerable<string> CameraDeviceNames => deviceNames;

        /// <summary>
        /// Invoked when a camera device has been connected.
        /// </summary>
        public event Action<string> OnNewDevice;

        /// <summary>
        /// Invoked when a camera device has been disconnected.
        /// </summary>
        public event Action<string> OnLostDevice;

        /// <summary>
        /// The currently selected device as a <see cref="Bindable{string}"/>.
        /// </summary>
        public readonly Bindable<string> Current = new Bindable<string>();

        private CameraInfo current;
        private ImmutableList<CameraInfo> devices = ImmutableList<CameraInfo>.Empty;
        private ImmutableList<string> deviceNames = ImmutableList<string>.Empty;
        private readonly CameraInfoComparer cameraInfoComparer = new CameraInfoComparer();
        private readonly Scheduler scheduler;
        private readonly ScheduledDelegate scheduled;

        public CameraManager(Scheduler scheduler)
        {
            this.scheduler = scheduler;
            
            Current.ValueChanged += onDeviceChanged;

            scheduled = scheduler.AddDelayed(() => syncCameraDevices(), 1000, true);
            syncCameraDevices();
        }

        public static CameraManager CreateSuitableManager(Scheduler scheduler)
        {
            switch (RuntimeInfo.OS)
            {
                case RuntimeInfo.Platform.Windows:
                    return new WindowsCameraManager(scheduler);

                case RuntimeInfo.Platform.Linux:
                    return new LinuxCameraManager(scheduler);

                default:
                    throw new PlatformNotSupportedException();
            }
        }

        protected abstract IEnumerable<CameraInfo> EnumerateAllDevices();

        private void syncCameraDevices()
        {
            var updatedCameraDevices = EnumerateAllDevices().ToImmutableList();

            if (devices.SequenceEqual(updatedCameraDevices, cameraInfoComparer))
                return;

            devices = updatedCameraDevices;

            onDevicesChanged();

            var oldDeviceNames = deviceNames;
            var newDeviceNames = deviceNames = devices.Select(d => d.Name).ToImmutableList();

            var addedDevices = newDeviceNames.Except(oldDeviceNames).ToList();
            var lostDevices = oldDeviceNames.Except(newDeviceNames).ToList();

            if (addedDevices.Count > 0 || lostDevices.Count > 0)
            {
                scheduler.Add(delegate
                {
                    foreach (var d in addedDevices)
                        OnNewDevice?.Invoke(d);

                    foreach (var d in lostDevices)
                        OnLostDevice?.Invoke(d);
                });
            }
        }

        private void onDeviceChanged(ValueChangedEvent<string> args)
        {
            scheduler.Add(() =>
            {
                var device = devices.FirstOrDefault(d => d.Name == args.NewValue);
                if (devices.Count > 0  && !devices.Contains(device))
                    current = devices.Last();
            });
        }

        private void onDevicesChanged()
        {
            scheduler.Add(() =>
            {
                if (!devices.Contains(current, cameraInfoComparer))
                    current = devices.Last();
            });
        }

        private bool isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                scheduled.Cancel();
                OnNewDevice = null;
                OnLostDevice = null;
            }

            isDisposed = true;
        }

        ~CameraManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private class CameraInfoComparer : IEqualityComparer<CameraInfo>
        {
            public bool Equals(CameraInfo a, CameraInfo b) => a.Path == b.Path;

            public int GetHashCode(CameraInfo camera) => camera.Path.GetHashCode();
        }
    }
}
