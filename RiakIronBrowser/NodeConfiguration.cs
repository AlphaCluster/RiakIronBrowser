using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CorrugatedIron.Config;

namespace RiakIronBrowser
{
    public class NodeConfiguration
    {
        IRiakNodeConfiguration localNodeConfig = null;

        public NodeConfiguration(IRiakNodeConfiguration config)
        {
            localNodeConfig = config;
        }

        public IRiakNodeConfiguration GetNode()
        {
            return localNodeConfig;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(localNodeConfig.HostAddress);
            builder.Append(":");
            builder.Append(localNodeConfig.PbcPort);
            builder.Append(" ");
            builder.Append(localNodeConfig.RestScheme);
            builder.Append(":");
            builder.Append(localNodeConfig.RestPort);
            return builder.ToString();
        }
    }
}
