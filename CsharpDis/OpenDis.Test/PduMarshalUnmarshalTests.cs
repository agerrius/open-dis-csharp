using System;
using System.Collections.Generic;
using System.Reflection;
using OpenDis.Core;
using NUnit.Framework;
using OpenDis.Enumerations;
using PduFactory = OpenDis.Core.PduFactory;

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
                        case "RelativeAntennaLocation":
                        case "LocationOfEntityCoordinates":
                        case "LocationInEntityCoordinates":
                            property.SetValue(pdu, GetVectorFloat(disVersion), null);
                            break;
                        case "EntityLocation":
                        case "DesignatorSpotLocation":
                        case "LocationInWorldCoordinates":
                        case "AntennaLocation":
                            property.SetValue(pdu, GetVectorDouble(disVersion), null);
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
                            property.SetValue(pdu, GetEntityId(disVersion), null);
                            break;
                        case "EntityType":
                        case "AlternativeEntityType":
                            property.SetValue(pdu, GetEntityType(disVersion), null);
                            break;
                        case "RadioEntityType":
                            property.SetValue(pdu, GetRadioEntityType(disVersion), null);
                            break;
                        case "ForceId":
                        case "ExerciseID":
                        case "ArticulationParameterCount":
                        case "NumberOfArticulationParameters":
                        case "NumberOfVariableParameters":
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
                        case "Capabilities":
                        case "FireMissionIndex":
                            if (disVersion == 7)
                            {
                                property.SetValue(pdu, (uint)1, null);
                            }
                            else
                            {
                                property.SetValue(pdu, (int)1, null);
                            }

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
                        case "DataLength":
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
                            property.SetValue(pdu, GetEventID(disVersion), null);
                            break;
                        case "BurstDescriptor":
                        case "Descriptor":
                            property.SetValue(pdu, GetBurstDescriptor(disVersion), null);
                            break;
                        case "ModulationType":
                            property.SetValue(pdu, GetModulationType(disVersion), null);
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
                                    GetArticulationParameter(disVersion) as Dis2012.VariableParameter);
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
                                (property.GetValue(pdu, null) as List<Dis1998.FixedDatum>).Add(
                                    GetFixedDatum(disVersion) as Dis1998.FixedDatum);
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
                                (property.GetValue(pdu, null) as List<Dis1998.VariableDatum>).Add(
                                    GetVariableDatum(disVersion) as Dis1998.VariableDatum);
                            }
                            else if (disVersion == 7)
                            {
                                (property.GetValue(pdu, null) as List<Dis2012.VariableDatum>).Add(
                                    GetVariableDatum(disVersion) as Dis2012.VariableDatum);
                            }

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
                            if (disVersion == 5)
                            {
                                (property.GetValue(pdu, null) as List<Dis1995.Vector3Float>).Add(
                                    GetVectorFloat(disVersion) as Dis1995.Vector3Float);
                            }
                            else if (disVersion == 6)
                            {
                                (property.GetValue(pdu, null) as List<Dis1998.Vector3Float>).Add(
                                    GetVectorFloat(disVersion) as Dis1998.Vector3Float);
                            }
                            else if (disVersion == 7)
                            {
                                (property.GetValue(pdu, null) as List<Dis2012.Vector3Float>).Add(
                                    GetVectorFloat(disVersion) as Dis2012.Vector3Float);
                            }

                            break;
                        default:
                            throw new NotSupportedException(property.ToString());
                    }
                }
            }

            object GetEntityType(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.EntityType() { EntityKind = 1, Domain = 1, Country = 123, Category = 2, Subcategory = 3, Specific = 4, Extra = 5 };
                    case 6:
                        return new Dis1998.EntityType() { EntityKind = 1, Domain = 1, Country = 123, Category = 2, Subcategory = 3, Specific = 4, Extra = 5 };
                    case 7:
                        return new Dis2012.EntityType() { EntityKind = 1, Domain = 1, Country = 123, Category = 2, Subcategory = 3, Specific = 4, Extra = 5 };
                    default:
                        throw new NotSupportedException();
                }
            }
            
            object GetRadioEntityType(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.RadioEntityType { EntityKind = 1, Domain = 1, Country = 123, Category = 2, Subcategory = 3, Nomenclature = 1, NomenclatureVersion = 1};
                    case 6:
                        return new Dis1998.RadioEntityType() { EntityKind = 1, Domain = 1, Country = 123, Category = 2, Nomenclature  = 1, NomenclatureVersion = 1};
                    case 7:
                        return new Dis2012.RadioType() { EntityKind = 1, Domain = 1, Country = 123, Category = 2, Subcategory = 3, Specific = 4, Extra = 5 };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetEntityId(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.EntityID { Site = 12, Application = 34, Entity = 56 };
                    case 6:
                        return new Dis1998.EntityID { Site = 12, Application = 34, Entity = 56 };
                    case 7:
                        return new Dis2012.EntityID { SimulationAddress = new Dis2012.SimulationAddress { Site = 12, Application = 34 }, EntityNumber = 56 };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetVectorFloat(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.Vector3Float() { X = 12.34f, Y = 34.56f, Z = 56.78f };
                    case 6:
                        return new Dis1998.Vector3Float() { X = 12.34f, Y = 34.56f, Z = 56.78f };
                    case 7:
                        return new Dis2012.Vector3Float() { X = 12.34f, Y = 34.56f, Z = 56.78f };
                    default:
                        throw new NotSupportedException();
                }
            }
            
            object GetVectorDouble(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.Vector3Double() { X = 12.34, Y = 34.56, Z = 56.78 };
                    case 6:
                        return new Dis1998.Vector3Double() { X = 12.34, Y = 34.56, Z = 56.78 };
                    case 7:
                        return new Dis2012.Vector3Double() { X = 12.34, Y = 34.56, Z = 56.78 };
                    default:
                        throw new NotSupportedException();
                }
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
                            EntityAngularVelocity = GetVectorFloat(5) as Dis1995.Vector3Float,
                            EntityLinearAcceleration = GetVectorFloat(5) as Dis1995.Vector3Float,
                        };
                    case 6:
                        return new Dis1998.DeadReckoningParameter()
                        {
                            DeadReckoningAlgorithm = 4,
                            EntityAngularVelocity = GetVectorFloat(6) as Dis1998.Vector3Float,
                            EntityLinearAcceleration = GetVectorFloat(6) as Dis1998.Vector3Float,
                        };
                    case 7:
                        return new Dis2012.DeadReckoningParameters()
                        {
                            DeadReckoningAlgorithm = 4,
                            EntityAngularVelocity = GetVectorFloat(7) as Dis2012.Vector3Float,
                            EntityLinearAcceleration = GetVectorFloat(7) as Dis2012.Vector3Float,
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
                    case 7:
                        return new Dis2012.VariableParameter()
                        {
                            ParameterType = 1000,
                            ParameterValue = 12.34,
                            PartAttachedTo = 0,
                            RecordType = 1,
                            ChangeIndicator = 1,
                        };
                    default:
                        throw new NotSupportedException();
                }
                
                
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
                        return new Dis1995.SupplyQuantity() { Quantity = 1, SupplyType = GetEntityId(disVersion) as Dis1995.EntityID };
                    case 6:
                        return new Dis1998.SupplyQuantity() { Quantity = 1, SupplyType = GetEntityType(disVersion) as Dis1998.EntityType };
                    case 7:
                        return new Dis2012.SupplyQuantity() { Quantity = 1, SupplyType = GetEntityType(disVersion) as Dis2012.EntityType };
                    default:
                        throw new NotSupportedException();
                }
            }
            
            object GetEventID(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.EventID { Application = 12, Site = 34, EventNumber = 56 };
                    case 6:
                        return new Dis1998.EventID { Application = 12, Site = 34, EventNumber = 56 };
                    case 7:
                        return new Dis2012.EventIdentifier { SimulationAddress = new Dis2012.SimulationAddress { Application = 12, Site = 34 }, EventNumber = 56 };
                    default:
                        throw new NotSupportedException();
                }
            }
            
            object GetFixedDatum(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.FixedDatum { FixedDatumID = 1, FixedDatumValue = 2 };
                    case 6:
                        return new Dis1998.FixedDatum { FixedDatumID = 1, FixedDatumValue = 2 };
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
                        Dis1995.VariableDatum vd5 = new Dis1995.VariableDatum { VariableDatumID = 1, VariableDatumLength = 64 };
                        vd5.VariableDatums.Add(GetEightByteChunk(disVersion) as Dis1995.EightByteChunk);
                        return vd5;
                    case 6:
                        Dis1998.VariableDatum vd6 = new Dis1998.VariableDatum { VariableDatumID = 1, VariableDatumLength = 64 };
                        vd6.VariableDatums.Add(GetEightByteChunk(disVersion) as Dis1998.EightByteChunk);
                        return vd6;
                    case 7:
                        Dis2012.VariableDatum vd7 = new Dis2012.VariableDatum { VariableDatumID = 1, VariableDatumLength = 64 };
                        vd7.VariableDatums.Add(GetEightByteChunk(disVersion) as Dis2012.EightByteChunk);
                        return vd7;
                    default:
                        throw new NotSupportedException();
                }
            }
            
            object GetEightByteChunk(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.EightByteChunk { OtherParameters = GetString(7) };
                    case 6:
                        return new Dis1998.EightByteChunk { OtherParameters = GetString(7) };
                    case 7:
                        return new Dis2012.EightByteChunk { OtherParameters = GetString(7) };
                    default:
                        throw new NotSupportedException();
                }
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
                        return new Dis2012.ModulationType { Detail = 1, MajorModulation = 2, RadioSystem = 3, SpreadSpectrum = 4 };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetBurstDescriptor(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return new Dis1995.BurstDescriptor
                        {
                            Fuse = 2, Quantity = 1, Rate = 1, Warhead = 3,
                            Munition = GetEntityType(disVersion) as Dis1995.EntityType
                        };
                    case 6:
                        return new Dis1998.BurstDescriptor
                        {
                            Fuse = 2, Quantity = 1, Rate = 1, Warhead = 3,
                            Munition = GetEntityType(disVersion) as Dis1998.EntityType
                        };
                    case 7:
                        return new Dis2012.MunitionDescriptor
                        {
                            Fuse = 2, Quantity = 1, Rate = 1, Warhead = 3,
                            MunitionType = GetEntityType(disVersion) as Dis2012.EntityType
                        };
                    default:
                        throw new NotSupportedException();
                }
            }

            object GetMarking(byte disVersion)
            {
                switch (disVersion)
                {
                    case 5:
                        return GetString(11);
                    case 6:
                        return new Dis1998.Marking() { Characters = GetString(11) };
                    case 7:
                        return new Dis2012.EntityMarking() { Characters = GetString(11) };
                    default:
                        throw new NotSupportedException();
                }
            }

            byte[] GetString(int length)
            {
                byte[] str = new byte[length + 1];
                for (int i = 0; i < length; i++)
                {
                    str[i] = (byte)(65 + i);
                }

                return str;
            }
        }
    }
}