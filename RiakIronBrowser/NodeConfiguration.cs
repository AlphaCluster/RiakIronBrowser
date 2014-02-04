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
            if (config != null)
                localNodeConfig = config;
            else
                localNodeConfig = new RiakNodeConfiguration();
        }

        public IRiakNodeConfiguration Node
        {
            get { return localNodeConfig; }
            set { localNodeConfig = value; }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(localNodeConfig.Name);
            builder.Append(" - ");
            builder.Append(localNodeConfig.HostAddress);
            builder.Append(":");
            builder.Append(localNodeConfig.PbcPort);
            //builder.Append(" ");
            //builder.Append(localNodeConfig.RestScheme);
            //builder.Append(":");
            //builder.Append(localNodeConfig.RestPort);
            return builder.ToString();
        }
    }
}
