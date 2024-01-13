namespace Service.ControllerService.Service.GetXRayConfig
{
    public class Result
    {
        public List<Inbound> inbounds;
        public List<Outbound> outbounds;
        public Dns dns;
        public Routing routing;

        public Result(string name,
            string ip,
            int port,
            string protocol,
            string guid,
            string network,
            string security,
            string publicKey,
            string fingerprint,
            string serverName,
            string shortId)
        {
            inbounds =
            [
                new Inbound()
                {
                    tag = "socks",
                    port = 10808,
                    listen = "127.0.0.1",
                    protocol = "socks",
                    sniffing = new Sniffing()
                    {
                        enabled = true,
                        destOverride = ["http", "tls"],
                        routeOnly = false
                    },
                    settings = new Settings()
                    {
                        auth = "noauth",
                        udp = true,
                        allowTransparent = false,
                    }
                },
            ];

            outbounds = [
                new Outbound()
                {
                    tag = "proxy",
                    protocol = protocol,
                    settings = new Settings()
                    {
                        vnext = [
                            new Vnext()
                            {
                                address = ip,
                                port = port,
                                users = [new User()
                                {
                                    id = guid,
                                    alterId = 0,
                                    email = name,
                                    security = "auto",
                                    encryption = "none"
                                }]
                            }]
                    },
                    streamSettings = new StreamSettings()
                    {
                        network = network,
                        security = security,
                        realitySettings = new RealitySettings()
                        {
                            serverName = serverName,
                            fingerprint = fingerprint,
                            show = false,
                            publicKey = publicKey,
                            shortId = shortId,
                            spiderX = "/"
                        }
                    },
                    mux = new Mux()
                    {
                        enabled = true,
                        concurrency = 4
                    }
                },
                new Outbound()
                {
                    tag = "direct",
                    protocol = "freedom",
                    settings = new Settings()
                    {

                    }
                },
                new Outbound()
                {
                    tag = "block",
                    protocol = "blackhole",
                    settings = new Settings()
                    {
                        response = new Response()
                        {
                            type = "http"
                        }
                    }
                }
            ];

            dns = new Dns()
            {
                servers = ["1.1.1.1", "8.8.8.8"]
            };

            routing = new Routing()
            {
                domainStrategy = "AsIs",
                rules = [
                    new Rule()
                    {
                        type = "field",
                        inboundTag = ["api"],
                        outboundTag = "api"
                    },
                    new Rule()
                    {
                        type = "field",
                        port = "0-65535",
                        outboundTag = "proxy"
                    }]
            };
        }

        public class Dns
        {
            public List<string> servers;
        }

        public class Inbound
        {
            public string tag;
            public int port;
            public string listen;
            public string protocol;
            public Sniffing sniffing;
            public Settings settings;
        }

        public class Mux
        {
            public bool enabled;
            public int concurrency;
        }

        public class Outbound
        {
            public string tag;
            public string protocol;
            public Settings settings;
            public StreamSettings streamSettings;
            public Mux mux;
        }

        public class RealitySettings
        {
            public string serverName;
            public string fingerprint;
            public bool show;
            public string publicKey;
            public string shortId;
            public string spiderX;
        }

        public class Response
        {
            public string type;
        }

        public class Routing
        {
            public string domainStrategy;
            public List<Rule> rules;
        }

        public class Rule
        {
            public string type;
            public List<string> inboundTag;
            public string outboundTag;
            public string port;
        }

        public class Settings
        {
            public string auth;
            public bool udp;
            public bool allowTransparent;
            public List<Vnext> vnext;
            public Response response;
        }

        public class Sniffing
        {
            public bool enabled;
            public List<string> destOverride;
            public bool routeOnly;
        }

        public class StreamSettings
        {
            public string network;
            public string security;
            public RealitySettings realitySettings;
        }

        public class User
        {
            public string id;
            public int alterId;
            public string email;
            public string security;
            public string encryption;
        }

        public class Vnext
        {
            public string address;
            public int port;
            public List<User> users;
        }
    }
}
