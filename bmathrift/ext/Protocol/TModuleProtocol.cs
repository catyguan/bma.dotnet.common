using bmathrift.core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmathrift.ext.Protocol
{
    public class TModuleProtocol : TProtocolWrap
    {
        private String _module;

        public TModuleProtocol(TProtocol prot)
            : base(prot)
        {            
        }

        public TModuleProtocol(TProtocol prot,String module)
            : base(prot)
        {
            _module = module;
        }

        public String module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
            }
        }

        public override void WriteMessageBegin(TMessage message)
        {
            message.Name = module + "." + message.Name;
            base.WriteMessageBegin(message);
        }
    }
}
