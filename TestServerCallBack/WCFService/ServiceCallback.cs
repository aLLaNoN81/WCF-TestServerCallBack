using System.ServiceModel;
using System.Threading;

namespace TestServerCallBack.WCFService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceCallback : IService
    {
        public IServiceCallback Callback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IServiceCallback>();
            }
        }

        public void GetData()
        {
            Thread.Sleep(5000);
            Callback.SendResult();
        }
    }
}
