using System;
using System.Collections.Generic;
using System.Reflection;
using OpenDis.Core;
using OpenDis.Core.Pdu;
using OpenDis.Core.DataTypes;
using NUnit.Framework;
using OpenDis.Enumerations;
using PduFactory = OpenDis.Core.PduFactory;

namespace OpenDis.Test
{
    [TestFixture]
    public class PduTests
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
        [TestCase(8, ProtocolVersion.Ieee1278_1_2012, Ignore = "Not implemented for DIS 7")]
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
        /*
[TestCase(28, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(28, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(29, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(29, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(30, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(30, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(31, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(31, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(32, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(32, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(33, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(33, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(34, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(34, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(35, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(35, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(36, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(36, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(37, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(37, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(38, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(38, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(39, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(39, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(40, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(40, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(41, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(41, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(42, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(42, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(43, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(43, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(44, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(44, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(45, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(45, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(46, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(46, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(47, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(47, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(48, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(48, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(49, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(49, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(50, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(50, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(51, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(51, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(52, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(52, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(53, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(53, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(54, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(54, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(55, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(55, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(56, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(56, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(57, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(57, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(58, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(58, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(59, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(59, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(60, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(60, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(61, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(61, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(62, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(62, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(63, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(63, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(64, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(64, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(65, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(65, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(66, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(66, ProtocolVersion.Ieee1278_1_2012)]
[TestCase(67, ProtocolVersion.Ieee1278_1A_1998)]
[TestCase(67, ProtocolVersion.Ieee1278_1_2012)]
*/
        public void Marshal_PduProcessor_Unmarshal(byte pduType, ProtocolVersion disVersion)
        {
            DataStream dat = new DataStream();
            DataOutputStream dos = new DataOutputStream(dat, Endian.Big);

            IPdu pdu_out = PduFactory.CreatePdu(pduType, disVersion);
            FillPduWithDummyData(pdu_out, (byte)disVersion);
            pdu_out.MarshalAutoLengthSet(dos);
            byte[] data = dos.ConvertToBytes();

            PduProcessor pduProcessor = new PduProcessor();
            List<object> pdus = pduProcessor.ProcessPdu(data, Endian.Big);

            Assert.AreEqual(1, pdus.Count);
            Assert.AreEqual(pdu_out, pdus[0]);
        }

        // Entity State
        [TestCase(1, ProtocolVersion.Ieee1278_1_1995, 160)]
        [TestCase(1, ProtocolVersion.Ieee1278_1A_1998, 160)]
        [TestCase(1, ProtocolVersion.Ieee1278_1_2012, 176)]
        // Fire
        [TestCase(2, ProtocolVersion.Ieee1278_1_1995, 96)]
        [TestCase(2, ProtocolVersion.Ieee1278_1A_1998, 96)]
        [TestCase(2, ProtocolVersion.Ieee1278_1_2012, 96)]
        // Detonation
        [TestCase(3, ProtocolVersion.Ieee1278_1_1995, 120)]
        [TestCase(3, ProtocolVersion.Ieee1278_1A_1998, 120)]
        [TestCase(3, ProtocolVersion.Ieee1278_1_2012, 136)]
        // Collision
        [TestCase(4, ProtocolVersion.Ieee1278_1_1995, 60)]
        [TestCase(4, ProtocolVersion.Ieee1278_1A_1998, 60)]
        [TestCase(4, ProtocolVersion.Ieee1278_1_2012, 60)]
        // Service Request
        [TestCase(5, ProtocolVersion.Ieee1278_1_1995, 40)]
        [TestCase(5, ProtocolVersion.Ieee1278_1A_1998, 40)]
        [TestCase(5, ProtocolVersion.Ieee1278_1_2012, 40)]
        // Resupply Offer
        [TestCase(6, ProtocolVersion.Ieee1278_1_1995, 40)]
        [TestCase(6, ProtocolVersion.Ieee1278_1A_1998, 40)]
        [TestCase(6, ProtocolVersion.Ieee1278_1_2012, 40)]
        // Resupply Received
        [TestCase(7, ProtocolVersion.Ieee1278_1_1995, 40)]
        [TestCase(7, ProtocolVersion.Ieee1278_1A_1998, 40)]
        [TestCase(7, ProtocolVersion.Ieee1278_1_2012, 40)]
        // Resupply Cancel
        [TestCase(8, ProtocolVersion.Ieee1278_1_1995, 24)]
        [TestCase(8, ProtocolVersion.Ieee1278_1A_1998, 24)]
        [TestCase(8, ProtocolVersion.Ieee1278_1_2012, 24, Ignore = "Not implemented for DIS 7")]
        // Repair Complete
        [TestCase(9, ProtocolVersion.Ieee1278_1_1995, 28)]
        [TestCase(9, ProtocolVersion.Ieee1278_1A_1998, 28)]
        [TestCase(9, ProtocolVersion.Ieee1278_1_2012, 28)]
        // Repair Response
        [TestCase(10, ProtocolVersion.Ieee1278_1_1995, 28)]
        [TestCase(10, ProtocolVersion.Ieee1278_1A_1998, 28)]
        [TestCase(10, ProtocolVersion.Ieee1278_1_2012, 28)]
        // Create Entity
        [TestCase(11, ProtocolVersion.Ieee1278_1_1995, 28)]
        [TestCase(11, ProtocolVersion.Ieee1278_1A_1998, 28)]
        [TestCase(11, ProtocolVersion.Ieee1278_1_2012, 28)]
        // Remove Entity
        [TestCase(12, ProtocolVersion.Ieee1278_1_1995, 28)]
        [TestCase(12, ProtocolVersion.Ieee1278_1A_1998, 28)]
        [TestCase(12, ProtocolVersion.Ieee1278_1_2012, 28)]
        // Start / Resume
        [TestCase(13, ProtocolVersion.Ieee1278_1_1995, 44)]
        [TestCase(13, ProtocolVersion.Ieee1278_1A_1998, 44)]
        [TestCase(13, ProtocolVersion.Ieee1278_1_2012, 44)]
        // Stop / Freeze
        [TestCase(14, ProtocolVersion.Ieee1278_1_1995, 40)]
        [TestCase(14, ProtocolVersion.Ieee1278_1A_1998, 40)]
        [TestCase(14, ProtocolVersion.Ieee1278_1_2012, 40)]
        // Acknowledge
        [TestCase(15, ProtocolVersion.Ieee1278_1_1995, 32)]
        [TestCase(15, ProtocolVersion.Ieee1278_1A_1998, 32)]
        [TestCase(15, ProtocolVersion.Ieee1278_1_2012, 32)]
        // Action request
        [TestCase(16, ProtocolVersion.Ieee1278_1_1995, 64)]
        [TestCase(16, ProtocolVersion.Ieee1278_1A_1998, 64)]
        [TestCase(16, ProtocolVersion.Ieee1278_1_2012, 64)]
        // Action Response
        [TestCase(17, ProtocolVersion.Ieee1278_1_1995, 64)]
        [TestCase(17, ProtocolVersion.Ieee1278_1A_1998, 64)]
        [TestCase(17, ProtocolVersion.Ieee1278_1_2012, 64)]
        // Data Query
        [TestCase(18, ProtocolVersion.Ieee1278_1_1995, 48)]
        [TestCase(18, ProtocolVersion.Ieee1278_1A_1998, 48)]
        [TestCase(18, ProtocolVersion.Ieee1278_1_2012, 48)]
        // Set Data
        [TestCase(19, ProtocolVersion.Ieee1278_1_1995, 64)]
        [TestCase(19, ProtocolVersion.Ieee1278_1A_1998, 64)]
        [TestCase(19, ProtocolVersion.Ieee1278_1_2012, 64)]
        // Data
        [TestCase(20, ProtocolVersion.Ieee1278_1_1995, 64)]
        [TestCase(20, ProtocolVersion.Ieee1278_1A_1998, 64)]
        [TestCase(20, ProtocolVersion.Ieee1278_1_2012, 64)]
        // Event Report
        [TestCase(21, ProtocolVersion.Ieee1278_1_1995, 64)]
        [TestCase(21, ProtocolVersion.Ieee1278_1A_1998, 64)]
        [TestCase(21, ProtocolVersion.Ieee1278_1_2012, 64)]
        // Comment
        [TestCase(22, ProtocolVersion.Ieee1278_1_1995, 56)]
        [TestCase(22, ProtocolVersion.Ieee1278_1A_1998, 56)]
        [TestCase(22, ProtocolVersion.Ieee1278_1_2012, 56)]
        // Electromagnetic Emission
        [TestCase(23, ProtocolVersion.Ieee1278_1_1995, 108, Ignore = "Not implemented for DIS 5")]
        [TestCase(23, ProtocolVersion.Ieee1278_1A_1998, 108)]
        [TestCase(23, ProtocolVersion.Ieee1278_1_2012, 108)]
        // Designator
        [TestCase(24, ProtocolVersion.Ieee1278_1_1995, 88)]
        [TestCase(24, ProtocolVersion.Ieee1278_1A_1998, 88)]
        [TestCase(24, ProtocolVersion.Ieee1278_1_2012, 88)]
        // Transmitter
#warning TODO check if last two fields should be vectors
        [TestCase(25, ProtocolVersion.Ieee1278_1_1995, 128)]
        [TestCase(25, ProtocolVersion.Ieee1278_1A_1998, 128)]
        [TestCase(25, ProtocolVersion.Ieee1278_1_2012, 128)]
        // Signal
        [TestCase(26, ProtocolVersion.Ieee1278_1_1995, 96)]
        [TestCase(26, ProtocolVersion.Ieee1278_1A_1998, 96)]
        [TestCase(26, ProtocolVersion.Ieee1278_1_2012, 96)]
        // Receiver
        [TestCase(27, ProtocolVersion.Ieee1278_1_1995, 36)]
        [TestCase(27, ProtocolVersion.Ieee1278_1A_1998, 36)]
        [TestCase(27, ProtocolVersion.Ieee1278_1_2012, 36)]
        public void PduLength(byte pduType, ProtocolVersion disVersion, int expLength)
        {
            DataStream dat = new DataStream();
            DataOutputStream dos = new DataOutputStream(dat, Endian.Big);

            IPdu pdu_out = PduFactory.CreatePdu(pduType, disVersion);
            FillPduWithDummyData(pdu_out, (byte)disVersion);
            pdu_out.MarshalAutoLengthSet(dos);
            byte[] data = dos.ConvertToBytes();

            Assert.AreEqual(expLength, (pdu_out as Pdu).Length);
            Assert.AreEqual(expLength, data.Length);
        }
        
        void FillPduWithDummyData(IPdu pdu, byte disVersion)
        {
            PropertyInfo[] properties = pdu.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!property.Name.Contains("Padding"))
                {
                    switch (property.Name)
                    {
                        case "Length":
                        case "ProtocolVersion":
                        case "ProtocolFamily":
                        case "PduType":
                        case "Timestamp":
                        case "PduStatus":
                        case "Pad":
                            // Ignore these
                            break;
                        case "EntityLinearVelocity":
                        case "DesignatorSpotWrtDesignated":
                        case "EntityLinearAcceleration":
                        case "Velocity":
                        case "Location":
                        case "LocationOfEntityCoordinates":
                        case "LocationInEntityCoordinates":
                            property.SetValue(pdu, GetVectorFloat(), null);
                            break;
                        case "RelativeAntennaLocation":
                            property.SetValue(pdu, GetVectorFloat(), null);
                            break;
                        case "EntityLocation":
                        case "DesignatorSpotLocation":
                        case "LocationInWorldCoordinates":
                        case "AntennaLocation":
                            property.SetValue(pdu, GetVectorDouble(), null);
                            break;
                        case "EntityOrientation":
                            property.SetValue(pdu, GetOrientation(disVersion), null);
                            break;
                        case "EntityID":
                        case "EntityId":
                        case "ReceivingEntityID":
                        case "ReceivingID":
                        case "RepairingEntityID":
                        case "OriginatingEntityID":
                        case "OriginatingID":
                        case "DesignatingEntityID":
                        case "DesignatedEntityID":
                        case "EmittingEntityID":
                        case "MunitionID":
                        case "ExplodingEntityID":
                        case "IssuingEntityID":
                        case "CollidingEntityID":
                        case "RequestingEntityID":
                        case "ServicingEntityID":
                        case "SupplyingEntityID":
                        case "TransmitterEntityId":
                        case "MunitionExpendibleID":
                        case "FiringEntityID":
                        case "TargetEntityID":
                            property.SetValue(pdu, GetEntityId(), null);
                            break;
                        case "EntityType":
                        case "AlternativeEntityType":
                            property.SetValue(pdu, GetEntityType(), null);
                            break;
                        case "RadioEntityType":
                            property.SetValue(pdu, GetRadioEntityType(disVersion), null);
                            break;
                        case "ForceId":
                        case "ExerciseID":
                        case "ArticulationParameterCount":
                        case "NumberOfArticulationParameters":
                        case "RepairResult":
                        case "ServiceTypeRequested":
                        case "NumberOfSupplyTypes":
                        case "DeadReckoningAlgorithm":
                        case "Reason":
                        case "FrozenBehavior":
                        case "CollisionType":
                        case "StateUpdateIndicator":
                        case "NumberOfSystems":
                        case "TransmitState":
                        case "InputSource":
                        case "ModulationParameterCount":
                        case "DetonationResult":
                            property.SetValue(pdu, (byte)1, null);
                            break;
                        case "NumberOfVariableParameters":
                            property.SetValue(pdu, (byte)2, null);
                            break;
                        case "Capabilities":
                            if (disVersion == 7)
                            {
                                property.SetValue(pdu, (uint)1, null);
                            }
                            else
                            {
                                property.SetValue(pdu, (int)1, null);
                            }

                            break;
                        case "FireMissionIndex":
                            property.SetValue(pdu, (uint)1, null);
                            break;
                        case "EntityAppearance":
                            if (disVersion == 7)
                            {
                                property.SetValue(pdu, (uint)10, null);
                            }
                            else
                            {
                                property.SetValue(pdu, (int)10, null);
                            }

                            break;
                        case "RequestID":
                        case "ActionID":
                        case "RequestStatus":
                        case "TimeInterval":
                        case "EventType":
                        case "SampleRate":
                        case "FixedDatumRecordCount":
                        case "NumberOfFixedDatumRecords":
                        case "VariableDatumRecordCount":
                        case "NumberOfVariableDatumRecords":
                            property.SetValue(pdu, (uint)1, null);
                            break;
                        case "CodeName":
                        case "DesignatorCode":
                        case "AcknowledgeFlag":
                        case "ResponseFlag":
                        case "Repair":
                        case "ReceiverState":
                        case "EncodingScheme":
                        case "TdlType":
                        case "TransmitterRadioId":
                        case "RadioId":
                        case "AntennaPatternType":
                        case "AntennaPatternCount":
                        case "CryptoSystem":
                        case "CryptoKeyId":
                            property.SetValue(pdu, (UInt16)1, null);
                            break;
                        case "Samples":
                            property.SetValue(pdu, (Int16)1, null);
                            break;
                        case "Frequency":
                            property.SetValue(pdu, (UInt64)123456, null);
                            break;
                        case "DesignatorPower":
                        case "DesignatorWavelength":
                        case "Mass":
                        case "ReceivedPoser":
                        case "Range":
                        case "TransmitFrequencyBandwidth":
                        case "Power":
                            property.SetValue(pdu, (float)12.34, null);
                            break;
                        case "Marking":
                            property.SetValue(pdu, GetMarking(disVersion), null);
                            break;
                        case "DeadReckoningParameters":
                            property.SetValue(pdu, GetDeadReckoningParameters(disVersion), null);
                            break;
                        case "RealWorldTime":
                        case "SimulationTime":
                            property.SetValue(pdu, GetClockTime(disVersion), null);
                            break;
                        case "EventID":
                            property.SetValue(pdu, GetEventID(), null);
                            break;
                        case "BurstDescriptor":
                        case "Descriptor":
                            property.SetValue(pdu, GetBurstDescriptor(), null);
                            break;
                        case "ModulationType":
                            property.SetValue(pdu, GetModulationType(disVersion), null);
                            break;
                        case "DataLength":
                            property.SetValue(pdu, (Int16)512, null);
                            break;
                        case "Data":
                            property.SetValue(pdu, GetString(64), null);
                            break;
                        case "ArticulationParameters":
                        case "VariableParameters":
                            if (disVersion == 5)
                            {
                                (property.GetValue(pdu, null) as List<Dis1995.ArticulationParameter>).Add(
                                    GetArticulationParameter(disVersion) as Dis1995.ArticulationParameter);
                            }
                            else if (disVersion == 6)
                            {
                                (property.GetValue(pdu, null) as List<Dis1998.ArticulationParameter>).Add(
                                    GetArticulationParameter(disVersion) as Dis1998.ArticulationParameter);
                            }
                            else if (disVersion == 7)
                            {
                                (property.GetValue(pdu, null) as List<Dis2012.VariableParameter>).Add(
                                    GetVariableParameterArticulated());
                                (property.GetValue(pdu, null) as List<Dis2012.VariableParameter>).Add(
                                    GetVariableParameterAttached());
                            }

                            break;
                        case "FixedDatums":
                            if (disVersion == 5)
                            {
                                (property.GetValue(pdu, null) as List<Dis1995.FixedDatum>).Add(
                                    GetFixedDatum(disVersion) as Dis1995.FixedDatum);
                            }
                            else if (disVersion == 6)
                            {
                                (property.GetValue(pdu, null) as List<FixedDatum>).Add(
                                    GetFixedDatum(disVersion) as FixedDatum);
                            }
                            else if (disVersion == 7)
                            {
                                (property.GetValue(pdu, null) as List<Dis2012.FixedDatum>).Add(
                                    GetFixedDatum(disVersion) as Dis2012.FixedDatum);
                            }

                            break;
                        case "VariableDatums":
                            if (disVersion == 5)
                            {
                                (property.GetValue(pdu, null) as List<Dis1995.VariableDatum>).Add(
                                    GetVariableDatum(disVersion) as Dis1995.VariableDatum);
                            }
                            else if (disVersion == 6)
                            {
                                (property.GetValue(pdu, null) as List<VariableDatum>).Add(
                                    GetVariableDatum(disVersion) as VariableDatum);
                            }
                            else if (disVersion == 7)
                            {
                                (property.GetValue(pdu, null) as List<Dis2012.VariableDatum>).Add(
                                    GetVariableDatum(disVersion) as Dis2012.VariableDatum);
                            }

                            break;
                        case "FixedDatumIDs":
                        case "VariableDatumIDs":
                            (property.GetValue(pdu, null) as List<uint>).Add(1234);
                            break;
                        case "Supplies":
                            if (disVersion == 5)
                            {
                                (property.GetValue(pdu, null) as List<Dis1995.SupplyQuantity>).Add(
                                    GetSupplyQuantity(disVersion) as Dis1995.SupplyQuantity);
                            }
                            else if (disVersion == 6)
                            {
                                (property.GetValue(pdu, null) as List<Dis1998.SupplyQuantity>).Add(
                                    GetSupplyQuantity(disVersion) as Dis1998.SupplyQuantity);
                            }
                            else if (disVersion == 7)
                            {
                                (property.GetValue(pdu, null) as List<Dis2012.SupplyQuantity>).Add(
                                    GetSupplyQuantity(disVersion) as Dis2012.SupplyQuantity);
                            }

                            break;
                        case "ModulationParametersList":
                        case "AntennaPatternList":
                            (property.GetValue(pdu, null) as List<Vector3Float>).Add(GetVectorFloat());
                            break;
                        case "Systems":
                            if (disVersion == 6)
                            {
                                (property.GetValue(pdu, null) as List<Dis1998.ElectronicEmissionSystemData>).Add(
                                    GetEmitterSystem(disVersion) as Dis1998.ElectronicEmissionSystemData);
                            }
                            else if (disVersion == 7)
                            {
                                (property.GetValue(pdu, null) as List<Dis2012.ElectronicEmissionSystemData>).Add(
                                    GetEmitterSystem(disVersion) as Dis2012.ElectronicEmissionSystemData);
                            }

                            break;
                        default:
                            throw new NotSupportedException(property.ToString());
                    }
                }
            }

            EntityType GetEntityType()
            {
                return new EntityType()
                {
                    EntityKind = 1, Domain = 1, Country = 123, Category = 2, Subcategory = 3, Specific = 4,
                    Extra = 5
                };
            }

            object GetRadioEntityType(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.RadioEntityType
                        {
                            EntityKind = 1, Domain = 1, Country = 123, Category = 2, Nomenclature = 1,
                            NomenclatureVersion = 1
                        };
                    case 6:
                        return new Dis1998.RadioEntityType()
                        {
                            EntityKind = 1, Domain = 1, Country = 123, Category = 2, Nomenclature = 1,
                            NomenclatureVersion = 1
                        };
                    case 7:
                        return new Dis2012.RadioType()
                        {
                            EntityKind = 1, Domain = 1, Country = 123, Category = 2, Subcategory = 3, Specific = 4,
                            Extra = 5
                        };
                    default:
                        throw new NotSupportedException();
                }
            }

            EntityID GetEntityId()
            {
                return new EntityID { Site = 12, Application = 34, Entity = 56 };
            }

            Vector3Float GetVectorFloat()
            {
                return new Vector3Float() { X = 12.34f, Y = 34.56f, Z = 56.78f };
            }

            Vector3Double GetVectorDouble()
            {
                return new Vector3Double() { X = 12.34, Y = 34.56, Z = 56.78 };
            }

            object GetOrientation(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.Orientation() { Phi = 12.34f, Theta = 34.56f, Psi = 56.78f };
                    case 6:
                        return new Dis1998.Orientation() { Phi = 12.34f, Theta = 34.56f, Psi = 56.78f };
                    case 7:
                        return new Dis2012.EulerAngles() { Phi = 12.34f, Theta = 34.56f, Psi = 56.78f };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetDeadReckoningParameters(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.DeadReckoningParameter()
                        {
                            DeadReckoningAlgorithm = 4,
                            EntityAngularVelocity = GetVectorFloat(),
                            EntityLinearAcceleration = GetVectorFloat(),
                        };
                    case 6:
                        return new Dis1998.DeadReckoningParameter()
                        {
                            DeadReckoningAlgorithm = 4,
                            EntityAngularVelocity = GetVectorFloat(),
                            EntityLinearAcceleration = GetVectorFloat(),
                        };
                    case 7:
                        return new Dis2012.DeadReckoningParameters()
                        {
                            DeadReckoningAlgorithm = 4,
                            EntityAngularVelocity = GetVectorFloat(),
                            EntityLinearAcceleration = GetVectorFloat(),
                        };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetArticulationParameter(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.ArticulationParameter()
                        {
                            ParameterType = 1000,
                            ParameterValue = 12.34,
                            PartAttachedTo = 0,
                            ParameterTypeDesignator = 1,
                            ChangeIndicator = 1,
                        };
                    case 6:
                        return new Dis1998.ArticulationParameter()
                        {
                            ParameterType = 1000,
                            ParameterValue = 12.34,
                            PartAttachedTo = 0,
                            ParameterTypeDesignator = 1,
                            ChangeIndicator = 1,
                        };
                    default:
                        throw new NotSupportedException();
                }
            }

            Dis2012.VariableParameterArticulated GetVariableParameterArticulated()
            {
                return new Dis2012.VariableParameterArticulated()
                {
                    ParameterType = 1000,
                    ParameterValue = 12.34,
                    PartAttachedTo = 2,
                    ChangeIndicator = 1
                };
            }

            Dis2012.VariableParameterAttached GetVariableParameterAttached()
            {
                return new Dis2012.VariableParameterAttached()
                {
                    ParameterType = 1000,
                    PartAttachedTo = 2,
                    ChangeIndicator = 1,
                    DetachedIndicator = 1,
                    AttachedPartType = GetEntityType()
                };
            }

            object GetClockTime(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.ClockTime() { TimePastHour = 1, Hour = 2 };
                    case 6:
                        return new Dis1998.ClockTime() { TimePastHour = 1, Hour = 2 };
                    case 7:
                        return new Dis2012.ClockTime() { TimePastHour = 1, Hour = 2 };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetSupplyQuantity(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.SupplyQuantity()
                            { Quantity = 12.34f, SupplyType = GetEntityType() };
                    case 6:
                        return new Dis1998.SupplyQuantity()
                            { Quantity = 12.34f, SupplyType = GetEntityType() };
                    case 7:
                        return new Dis2012.SupplyQuantity()
                            { Quantity = 12.34f, SupplyType = GetEntityType() };
                    default:
                        throw new NotSupportedException();
                }
            }

            EventID GetEventID()
            {
                return new EventID { Application = 12, Site = 34, EventNumber = 56 };
            }

            object GetFixedDatum(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.FixedDatum { FixedDatumID = 1, FixedDatumValue = 2 };
                    case 6:
                        return new FixedDatum { FixedDatumID = 1, FixedDatumValue = 2 };
                    case 7:
                        return new Dis2012.FixedDatum { FixedDatumID = 1, FixedDatumValue = 2 };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetVariableDatum(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        Dis1995.VariableDatum vd5 = new Dis1995.VariableDatum
                            { VariableDatumID = 1, VariableDatumLength = 64 };
                        vd5.VariableDatums.Add(GetEightByteChunk());
                        return vd5;
                    case 6:
                        VariableDatum vd6 = new VariableDatum
                            { VariableDatumID = 1, VariableDatumLength = 64 };
                        vd6.VariableDatums.Add(GetEightByteChunk());
                        return vd6;
                    case 7:
                        Dis2012.VariableDatum vd7 = new Dis2012.VariableDatum
                            { VariableDatumID = 1, VariableDatumLength = 64 };
                        vd7.VariableDatums.Add(GetEightByteChunk());
                        return vd7;
                    default:
                        throw new NotSupportedException();
                }
            }

            EightByteChunk GetEightByteChunk()
            {
                return new EightByteChunk { OtherParameters = GetString(8) };
            }

            object GetModulationType(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.ModulationType { Detail = 1, Major = 2, System_ = 3, SpreadSpectrum = 4 };
                    case 6:
                        return new Dis1998.ModulationType { Detail = 1, Major = 2, System_ = 3, SpreadSpectrum = 4 };
                    case 7:
                        return new Dis2012.ModulationType
                            { Detail = 1, MajorModulation = 2, RadioSystem = 3, SpreadSpectrum = 4 };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetEmitterSystem(byte disVersion)
            {
                switch (disVersion)
                {
                    case 6:
                        Dis1998.ElectronicEmissionSystemData es6 = new Dis1998.ElectronicEmissionSystemData
                        {
                            NumberOfBeams = 1,
                            EmitterSystem = new Dis1998.EmitterSystem
                                { EmitterName = 1234, EmitterIdNumber = 1, Function = 4 },
                            Location = GetVectorFloat()
                        };
                        es6.BeamDataRecords.Add(GetBeam(disVersion) as Dis1998.ElectronicEmissionBeamData);
                        return es6;
                    case 7:
                        Dis2012.ElectronicEmissionSystemData es7 = new Dis2012.ElectronicEmissionSystemData
                        {
                            NumberOfBeams = 1,
                            EmitterSystem = new Dis2012.EmitterSystem
                                { EmitterName = 1234, EmitterIdNumber = 1, Function = 4 },
                            Location = GetVectorFloat()
                        };
                        es7.BeamDataRecords.Add(GetBeam(disVersion) as Dis2012.ElectronicEmissionBeamData);
                        return es7;
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetBeam(byte disVersion)
            {
                switch (disVersion)
                {
                    case 6:
                        Dis1998.ElectronicEmissionBeamData bd6 = new Dis1998.ElectronicEmissionBeamData
                        {
                            NumberOfTrackJamTargets = 1, BeamIDNumber = 2, BeamFunction = 3, BeamParameterIndex = 1,
                            HighDensityTrackJam = 0, JammingModeSequence = 5,
                            FundamentalParameterData = new Dis1998.FundamentalParameterData
                            {
                                BeamAzimuthCenter = 1.0f, BeamAzimuthSweep = 2.0f, BeamElevationCenter = -1.0f,
                                BeamElevationSweep = 1.0f, BeamSweepSync = 0.25f, Frequency = 123456.78f,
                                FrequencyRange = 12.34f, PulseRepetitionFrequency = 12.34f, PulseWidth = 12.34f,
                                EffectiveRadiatedPower = 12345.67f
                            }
                        };
                        bd6.TrackJamTargets.Add(new Dis1998.TrackJamTarget
                            { EmitterID = 1, BeamID = 1, TrackJam = GetEntityId() });
                        return bd6;
                    case 7:
                        Dis2012.ElectronicEmissionBeamData bd7 = new Dis2012.ElectronicEmissionBeamData
                        {
                            NumberOfTrackJamTargets = 1, BeamIDNumber = 2, BeamFunction = 3, BeamParameterIndex = 1,
                            HighDensityTrackJam = 0, Category = 1, Specific = 2, Subcategory = 3, Kind = 4,
                            BeamStatus = 5,
                            FundamentalParameterData = new Dis2012.FundamentalParameterData
                            {
                                BeamAzimuthCenter = 1.0f, BeamAzimuthSweep = 2.0f, BeamElevationCenter = -1.0f,
                                BeamElevationSweep = 1.0f, BeamSweepSync = 0.25f, Frequency = 123456.78f,
                                FrequencyRange = 12.34f, PulseRepetitionFrequency = 12.34f, PulseWidth = 12.34f,
                                EffectiveRadiatedPower = 12345.67f
                            }
                        };
                        bd7.TrackJamTargets.Add(new Dis2012.TrackJamData
                        {
                            EmitterNumber = 1, BeamNumber = 1, EntityID = GetEntityId()
                        });
                        return bd7;
                    default:
                        throw new NotSupportedException();
                }
            }

            BurstDescriptor GetBurstDescriptor()
            {
                return new BurstDescriptor
                {
                    Fuse = 2, Quantity = 1, Rate = 1, Warhead = 3,
                    Munition = GetEntityType()
                };
            }

            object GetMarking(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return GetString(12);
                    case 6:
                        return new Dis1998.Marking() { CharacterSet = 1, Characters = GetString(11) };
                    case 7:
                        return new Dis2012.EntityMarking() { CharacterSet = 1, Characters = GetString(11) };
                    default:
                        throw new NotSupportedException();
                }
            }

            byte[] GetString(int length)
            {
                byte[] str = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    str[i] = (byte)(65 + i);
                }

                return str;
            }
        }
    }
}