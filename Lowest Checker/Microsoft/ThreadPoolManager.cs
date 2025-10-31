using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lowest_Checker.Microsoft
{
    public class ThreadPoolManager : IDisposable
    {
        private readonly BlockingCollection<Action> _tasks;
        private readonly Thread[] _workers;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _disposed = false;

        public ThreadPoolManager(int numberOfThreads)
        {
            _tasks = new BlockingCollection<Action>();
            _cancellationTokenSource = new CancellationTokenSource();
            _workers = new Thread[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                _workers[i] = new Thread(Worker);
                _workers[i].Start();
            }
        }

        private void Worker()
        {
            foreach (var task in _tasks.GetConsumingEnumerable(_cancellationTokenSource.Token))
            {
                try
                {
                    task();
                }
                catch (OperationCanceledException)
                {
                    // Handle operation cancellation exception if needed
                }
            }
        }

        public void EnqueueTask(Action task)
        {
            if (!_tasks.IsAddingCompleted)
            {
                _tasks.Add(task);
            }
        }

        public void Stop()
        {
            _tasks.CompleteAdding();
            _cancellationTokenSource.Cancel();

            foreach (var worker in _workers)
            {
                worker.Join();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _cancellationTokenSource.Dispose();
                    _tasks.Dispose();
                }
                _disposed = true;
            }
        }

        ~ThreadPoolManager()
        {
            Dispose(false);
        }
    }
}