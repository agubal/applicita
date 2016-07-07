using System;
using System.Net;
using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;

namespace Applicita.Host
{
    public class Startup
    {
        private static SiloHost _siloHost;
        private static string _hostName = "OrleansHost";
        private static string _configName = "OrleansConfiguration.xml";

        public static AppDomain Start()
        {
            var domain =  AppDomain.CreateDomain(_hostName, null, new AppDomainSetup { AppDomainInitializer = InitSilo });
            var config = ClientConfiguration.LocalhostSilo(30000);
            GrainClient.Initialize(config);
            return domain;
        }

        public static void InitSilo(string[] args)
        {
            _siloHost = new SiloHost(Dns.GetHostName()) { ConfigFileName = _configName };
            _siloHost.InitializeOrleansSilo();
            var startedok = _siloHost.StartOrleansSilo();
            if (!startedok)
            {
                throw new SystemException($"Failed to start Orleans silo '{_siloHost.Name}' as a {_siloHost.Type} node");
            }
        }

        public static void ShutdownSilo()
        {
            if (_siloHost == null) return;
            _siloHost.Dispose();
            GC.SuppressFinalize(_siloHost);
            _siloHost = null;
        }
    }
}
