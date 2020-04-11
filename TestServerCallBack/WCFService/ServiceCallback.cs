using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Linq;

namespace TestServerCallBack.WCFService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ServiceCallback : IService
    {
        private Dictionary<int, IServiceCallback> _clients = new Dictionary<int, IServiceCallback>();

        public IServiceCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            }
        }

        public void GetData()
        {
            Thread.Sleep(20000);
            Callback.SendResultBroadcast("MADONNA LAGGATA");
        }

        public bool GetTest()
        {
            return true;
        }

        public int Subscribe()
        {
            IServiceCallback callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            int id = 0;
            if (_clients.Count > 0)
            {
                int max = _clients.Max(o => o.Key);
                id = max + 1;
            }            
            _clients.Add(id, callback);
            return id;
        }

        public bool Unsubscribe(int id)
        {
            if (_clients.ContainsKey(id))
            {
                _clients.Remove(id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void BroadcastMessage(string message)
        {
            ThreadPool.QueueUserWorkItem
            (
                delegate
                {
                    lock (_clients)

                    {
                        List<int> disconnectedClientGuids = new List<int>();

                        foreach (KeyValuePair<int, IServiceCallback> client in _clients)
                        {
                            try
                            {
                                client.Value.SendResult(client.Key, "MADONNA IN BROADCAST");
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            );
        }
    }
}