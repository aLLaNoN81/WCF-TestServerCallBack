using System.ServiceModel;

namespace TestServerCallBack.WCFService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServiceCallback))]
    public interface IService
    {
        [OperationContract(IsOneWay = true)]
        void GetData();
    }


    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendResult();
    }
}
