using System;
using System.Collections.Generic;
using System.Reflection;
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
                            // Ignore these
                            break;
                        case "EntityLinearVelocity":
                        case "DesignatorSpotWrtDesignated":
                        case "EntityLinearAcceleration":
                            property.SetValue(pdu, GetVectorFloat(disVersion), null);
                            break;
                        case "EntityLocation":
                        case "DesignatorSpotLocation":
                            property.SetValue(pdu, GetVectorDouble(disVersion), null);
                            break;
                        case "EntityOrientation":
                            property.SetValue(pdu, GetOrientation(disVersion), null);
                            break;
                        case "EntityID":
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
                            property.SetValue(pdu, GetEntityId(disVersion), null);
                            break;
                        case "EntityType":
                        case "AlternativeEntityType":
                            property.SetValue(pdu, GetEntityType(disVersion), null);
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
                            property.SetValue(pdu, (byte)1, null);
                            break;
                        case "Capabilities":
                            if (disVersion == 7)
                            {
                                property.SetValue(pdu, (uint)0, null);
                            }
                            else
                            {
                                property.SetValue(pdu, (int)0, null);
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
                            property.SetValue(pdu, (uint)10, null);
                            break;
                        case "CodeName":
                        case "DesignatorCode":
                            property.SetValue(pdu, (UInt16)1234, null);
                            break;
                        case "DesignatorPower":
                        case "DesignatorWavelength":
                            property.SetValue(pdu, (float)12.34, null);
                            break;
                        case "Marking":
                            property.SetValue(pdu, GetMarking(disVersion), null);
                            break;
                        case "DeadReckoningParameters":
                            property.SetValue(pdu, GetDeadReckoningParameters(disVersion), null);
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
                        default:
                            throw new NotSupportedException(property.Name);
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