using System.Collections.Generic;
using OpenDis.Core;
using NUnit.Framework;
using OpenDis.Enumerations;

namespace OpenDis.Test
{
    [TestFixture]
    public class PduMarshalUnmarshalTests
    {
        [TestCase(1, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(1, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(1, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(2, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(2, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(2, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(3, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(3, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(3, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(4, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(4, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(4, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(5, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(5, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(5, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(6, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(6, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(6, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(7, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(7, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(7, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(8, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(8, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(8, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(9, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(9, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(9, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(10, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(10, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(10, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(11, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(11, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(11, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(12, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(12, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(12, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(13, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(13, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(13, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(14, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(14, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(14, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(15, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(15, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(15, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(16, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(16, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(16, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(17, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(17, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(17, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(18, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(18, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(18, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(19, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(19, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(19, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(20, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(20, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(20, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(21, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(21, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(21, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(22, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(22, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(22, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(23, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(23, ProtocolVersion.Ieee1278_1_1995, Ignore = "Not implemented for DIS 5")]
        [TestCase(23, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(24, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(24, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(24, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(25, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(25, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(25, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(26, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(26, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(26, ProtocolVersion.Ieee1278_1_2012)]
        [TestCase(27, ProtocolVersion.Ieee1278_1A_1998)]
        [TestCase(27, ProtocolVersion.Ieee1278_1_1995)]
        [TestCase(27, ProtocolVersion.Ieee1278_1_2012)]
        public void Marshal_PduProcessor_Unmarshal(byte pduType, ProtocolVersion disVersion)
        {
            DataStream dat = new DataStream();
            DataOutputStream dos = new DataOutputStream(dat, Endian.Big);
            
            IPdu pdu_out = PduFactory.CreatePdu(pduType, disVersion);
            pdu_out.MarshalAutoLengthSet(dos);
            byte[] data = dos.ConvertToBytes();
            
            PduProcessor pduProcessor = new PduProcessor();
            List<object> pdus = pduProcessor.ProcessPdu(data, Endian.Big);

            Assert.AreEqual(1, pdus.Count);
            Assert.AreEqual(pdu_out, pdus[0]);
        }
    }
}