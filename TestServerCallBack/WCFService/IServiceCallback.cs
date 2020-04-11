using System.ServiceModel;

namespace TestServerCallBack.WCFService
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServiceCallback))]
    public interface IService
    {
        [OperationContract(IsOneWay = true)]
        void GetData();

        [OperationContract]
        bool GetTest();

        [OperationContract()]
        int Subscribe();

        [OperationContract()]
        bool Unsubscribe(int id);
    }

    public interface IServiceCallback
    {
        [OperationContract()]
        void SendResultBroadcast(string text);

        [OperationContract()]
        void SendResult(int id, string text);
    }
}