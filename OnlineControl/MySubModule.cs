using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Network;

namespace OnlineControl
{
    public class MySubModule : MBSubModuleBase
    {
        private Thread serverThread;
        private HttpServer httpServer;
        private void StartServer()
        {
            httpServer = new HttpServer();
            httpServer.StartListener();
        }
        private void AbortServer()
        {
            if (serverThread != null)
            {
                httpServer.StopListener();
                serverThread.Abort();
            }
        }
        private void StartServerThread()
        {
            AbortServer();
            serverThread = new Thread(new ThreadStart(StartServer));
            serverThread.Start();
        }
        public override void OnGameInitializationFinished(Game game)
        {
            StartServerThread();
        }
        protected override void OnSubModuleLoad()
        {
            AbortServer(); //just in case
        }
    }

}
