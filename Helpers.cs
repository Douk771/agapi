using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace AutoGRAPHService
{
    internal delegate void OnMessageOutgoingDelegate(string msg);
    internal delegate void OnMessageIncomingDelegate(string msg);

    public class ClientMessageInspector : IClientMessageInspector
    {
        readonly AuthenticateEndpointBehavior parentBehavior;
        internal event OnMessageOutgoingDelegate OnMessageOutgoing;
        internal event OnMessageIncomingDelegate OnMessageIncoming;

        public ClientMessageInspector(AuthenticateEndpointBehavior parent) { parentBehavior = parent; }
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            if (!request.Properties.ContainsKey(HttpRequestMessageProperty.Name))
                request.Properties.Add(HttpRequestMessageProperty.Name, new HttpRequestMessageProperty());
            var rmp = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            rmp.Headers.Add("AG-TOKEN", parentBehavior.Token);
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            //MemoryStream stm = new MemoryStream();
            //reply.(XmlDictionaryWriter.CreateTextWriter(stm));
            //if (OnMessageIncoming != null)
            //    OnMessageIncoming("1");
        }
    }

    public class AuthenticateEndpointBehavior : IEndpointBehavior
    {
        internal string Token;

        internal event OnMessageOutgoingDelegate OnMessageOutgoing;
        internal event OnMessageIncomingDelegate OnMessageIncoming;

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            var cmi = new ClientMessageInspector(this);
            cmi.OnMessageIncoming += InvokeMessageIncoming;
            cmi.OnMessageOutgoing += InvokeMessageOutoing;
            clientRuntime.MessageInspectors.Add(cmi);
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }
        public void Validate(ServiceEndpoint endpoint) { }

        void InvokeMessageOutoing(string msg)
        {
            if (OnMessageOutgoing != null)
                OnMessageOutgoing(msg);
        }

        void InvokeMessageIncoming(string msg)
        {
            if (OnMessageIncoming != null)
                OnMessageIncoming(msg);
        }
    }


    class TimeCalc : IDisposable
    {
        readonly DateTime DT;
        readonly string name;
        internal TimeCalc(string name) { DT = DateTime.Now; this.name = name; }
        void IDisposable.Dispose() { Console.WriteLine(name + ": " + DateTime.Now.Subtract(DT).TotalSeconds.ToString("F3", CultureInfo.InvariantCulture)); }
    }
}
