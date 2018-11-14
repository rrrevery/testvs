using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;

namespace MPOSServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("aaa");
            ServiceHost host = new ServiceHost(typeof(ChangYi.Crm.Server.Wcf.POSService));
            Console.WriteLine("bbb");
            try
            {
                //ChangYi.Crm.Server.CrmServerPlatform.InitData();
                host.Open();
                Console.WriteLine("ccc");
/*                
                Console.WriteLine("服务端终结点地址列表：");

                foreach (ChannelDispatcher cd in host.ChannelDispatchers)
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        Console.WriteLine("服务终结点逻辑地址：{0}", ed.EndpointAddress.Uri.ToString());
                    }
*/
                Console.WriteLine("The Service is ready");
                Console.WriteLine("Presee <Enter> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();
            }
            finally
            {
                if (host.State == CommunicationState.Faulted)
                    host.Abort();
                else
                    host.Close();
                //ChangYi.Crm.Server.CrmServerPlatform.DisposeData();
            }
                   
        }
    }
}
