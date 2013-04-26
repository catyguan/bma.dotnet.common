using bmathrift.core.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bmathrift.ext.Protocol
{
    public class TProtocolWrap : TProtocol
    {
        private TProtocol _protocol;

        public TProtocolWrap(TProtocol prot)
            : base(prot.Transport)
        {
            _protocol = prot;
        }

        public TProtocol protocol
        {
            get
            {
                return _protocol;
            }
            set
            {
                _protocol =value;
            }
        }

        public override void WriteMessageBegin(TMessage message)
        {
            _protocol.WriteMessageBegin(message);
        }
        public override void WriteMessageEnd()
        {
            _protocol.WriteMessageEnd();
        }
        public override void WriteStructBegin(TStruct struc)
        {
            _protocol.WriteStructBegin(struc);
        }
        public override void WriteStructEnd()
        {
            _protocol.WriteStructEnd();
        }
        public override void WriteFieldBegin(TField field)
        {
            _protocol.WriteFieldBegin(field);
        }
        public override void WriteFieldEnd()
        {
            _protocol.WriteFieldEnd();
        }
        public override void WriteFieldStop()
        {
            _protocol.WriteFieldStop();
        }
        public override void WriteMapBegin(TMap map)
        {
            _protocol.WriteMapBegin(map);
        }
        public override void WriteMapEnd()
        {
            _protocol.WriteMapEnd();
        }
        public override void WriteListBegin(TList list)
        {
            _protocol.WriteListBegin(list);
        }
        public override void WriteListEnd()
        {
            _protocol.WriteListEnd();
        }
        public override void WriteSetBegin(TSet set)
        {
            _protocol.WriteSetBegin(set);
        }
        public override void WriteSetEnd()
        {
            _protocol.WriteSetEnd();
        }
        public override void WriteBool(bool b)
        {
            _protocol.WriteBool(b);
        }
        public override void WriteByte(byte b)
        {
            _protocol.WriteByte(b);
        }
        public override void WriteI16(short i16)
        {
            _protocol.WriteI16(i16);
        }
        public override void WriteI32(int i32)
        {
            _protocol.WriteI32(i32);
        }
        public override void WriteI64(long i64)
        {
            _protocol.WriteI64(i64);
        }
        public override void WriteDouble(double d)
        {
            _protocol.WriteDouble(d);
        }
        public override void WriteBinary(byte[] b)
        {
            _protocol.WriteBinary(b);
        }


        public override TMessage ReadMessageBegin()
        {
            return _protocol.ReadMessageBegin();
        }
        public override void ReadMessageEnd()
        {
            _protocol.ReadMessageEnd();
        }
        public override TStruct ReadStructBegin()
        {
            return _protocol.ReadStructBegin();
        }
        public override void ReadStructEnd()
        {
            _protocol.ReadStructEnd();
        }
        public override TField ReadFieldBegin()
        {
            return _protocol.ReadFieldBegin();
        }
        public override void ReadFieldEnd()
        {
            _protocol.ReadFieldEnd();
        }
        public override TMap ReadMapBegin()
        {
            return _protocol.ReadMapBegin();
        }
        public override void ReadMapEnd()
        {
            _protocol.ReadMapEnd();
        }
        public override TList ReadListBegin()
        {
            return _protocol.ReadListBegin();
        }
        public override void ReadListEnd()
        {
            _protocol.ReadListEnd();
        }
        public override TSet ReadSetBegin()
        {
            return _protocol.ReadSetBegin();
        }
        public override void ReadSetEnd()
        {
            _protocol.ReadSetEnd();
        }
        public override bool ReadBool()
        {
            return _protocol.ReadBool();
        }
        public override byte ReadByte()
        {
            return _protocol.ReadByte();
        }
        public override short ReadI16()
        {
            return _protocol.ReadI16();
        }
        public override int ReadI32()
        {
            return _protocol.ReadI32();
        }
        public override long ReadI64()
        {
            return _protocol.ReadI64();
        }
        public override double ReadDouble()
        {
            return _protocol.ReadDouble();
        }
        public override byte[] ReadBinary()
        {
            return _protocol.ReadBinary();
        }
    }
}
