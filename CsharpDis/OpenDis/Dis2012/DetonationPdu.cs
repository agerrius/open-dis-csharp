// Copyright (c) 1995-2009 held by the author(s).  All rights reserved.
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions
// are met:
// * Redistributions of source code must retain the above copyright
//    notice, this list of conditions and the following disclaimer.
// * Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer
//   in the documentation and/or other materials provided with the
//   distribution.
// * Neither the names of the Naval Postgraduate School (NPS)
//   Modeling Virtual Environments and Simulation (MOVES) Institute
//   (http://www.nps.edu and http://www.MovesInstitute.org)
//   nor the names of its contributors may be used to endorse or
//   promote products derived from this software without specific
//   prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// AS IS AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
// COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
//
// Copyright (c) 2008, MOVES Institute, Naval Postgraduate School. All 
// rights reserved. This work is licensed under the BSD open source license,
// available at https://www.movesinstitute.org/licenses/bsd.html
//
// Author: DMcG
// Modified for use with C#:
//  - Peter Smith (Naval Air Warfare Center - Training Systems Division)
//  - Zvonko Bostjancic (Blubit d.o.o. - zvonko.bostjancic@blubit.si)

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using OpenDis.Core;
using OpenDis.Core.DataTypes;
using OpenDis.Core.PduFamily;

namespace OpenDis.Dis2012
{
    /// <summary>
    /// Detonation or impact of munitions, as well as, non-munition explosions, the burst or initial bloom of chaff, and the ignition of a flare shall be indicated. Section 7.3.3  COMPLETE
    /// </summary>
    [Serializable]
    [XmlRoot]
    [XmlInclude(typeof(EntityID))]
    [XmlInclude(typeof(EventID))]
    [XmlInclude(typeof(Vector3Float))]
    [XmlInclude(typeof(Vector3Double))]
    [XmlInclude(typeof(BurstDescriptor))]
    [XmlInclude(typeof(VariableParameter))]
    public partial class DetonationPdu : Core.Pdu.DetonationPdu, IEquatable<DetonationPdu>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetonationPdu"/> class.
        /// </summary>
        public DetonationPdu() : base(Enumerations.ProtocolVersion.Ieee1278_1_2012)
        {
            PduType = (byte)3;
        }
        
        public EntityID ExplodingEntityID
        {
            get => base.MunitionID;
            set => base.MunitionID = value;
        }
        
        [Obsolete("Replaced by ExplodingEntityID in DIS7", true)]
        public new EntityID MunitionID
        {
            get => base.MunitionID;
            set => base.MunitionID = value;
        }
        
        public BurstDescriptor Descriptor
        {
            get => base.BurstDescriptor;
            set => base.BurstDescriptor = value;
        }
        
        [Obsolete("Replaced by Descriptor in DIS7", true)]
        public new BurstDescriptor BurstDescriptor
        {
            get => base.BurstDescriptor;
            set => base.BurstDescriptor = value;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// 	<c>true</c> if operands are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(DetonationPdu left, DetonationPdu right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// 	<c>true</c> if both operands are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(DetonationPdu left, DetonationPdu right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return true;
            }

            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }

            return left.Equals(right);
        }

        public override int GetMarshalledSize()
        {
            int marshalSize = 0; 

            marshalSize = base.GetMarshalledSize();
            marshalSize += this.ExplodingEntityID.GetMarshalledSize();  // this.ExplodingEntityID
            marshalSize += this.EventID.GetMarshalledSize();  // this.EventID
            marshalSize += this.Velocity.GetMarshalledSize();  // this.Velocity
            marshalSize += this.LocationInWorldCoordinates.GetMarshalledSize();  // this.LocationInWorldCoordinates
            marshalSize += this.Descriptor.GetMarshalledSize();  // this.Descriptor
            marshalSize += this.LocationInEntityCoordinates.GetMarshalledSize();  // this.LocationOfEntityCoordinates
            marshalSize += 1;  // this.DetonationResult
            marshalSize += 1;  // this._numberOfVariableParameters
            marshalSize += 2;  // this._pad
            for (int idx = 0; idx < this.VariableParameters.Count; idx++)
            {
                VariableParameter listElement = (VariableParameter)this.VariableParameters[idx];
                marshalSize += listElement.GetMarshalledSize();
            }

            return marshalSize;
        }
        
        /// <summary>
        /// Gets or sets the How many articulation parameters we have
        /// </summary>
        /// <remarks>
        /// Note that setting this value will not change the marshalled value. The list whose length this describes is used
        /// for that purpose.
        /// The getnumberOfArticulationParameters method will also be based on the actual list length rather than this value.
        /// The method is simply here for completeness and should not be used for any computations.
        /// </remarks>
        [XmlElement(Type = typeof(byte), ElementName = "numberOfVariableParameters")]
        public byte NumberOfVariableParameters { get; set; }

        /// <summary>
        /// Gets or sets the padding
        /// </summary>
        [XmlElement(Type = typeof(short), ElementName = "pad")]
        public ushort Pad { get; set; }

        /// <summary>
        /// Gets the articulationParameters
        /// </summary>
        [XmlElement(ElementName = "variableParametersList", Type = typeof(List<VariableParameter>))]
        public List<VariableParameter> VariableParameters { get; } = new();
        
        /// <summary>
        /// Automatically sets the length of the marshalled data, then calls the marshal method.
        /// </summary>
        /// <param name="dos">The DataOutputStream instance to which the PDU is marshaled.</param>
        public override void MarshalAutoLengthSet(DataOutputStream dos)
        {
            // Set the length prior to marshalling data
            this.Length = (ushort)this.GetMarshalledSize();
            this.Marshal(dos);
        }

        /// <summary>
        /// Marshal the data to the DataOutputStream.  Note: Length needs to be set before calling this method
        /// </summary>
        /// <param name="dos">The DataOutputStream instance to which the PDU is marshaled.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Due to ignoring errors.")]
        public override void Marshal(DataOutputStream dos)
        {
            base.Marshal(dos);
            if (dos != null)
            {
                try
                {
                    this.ExplodingEntityID.Marshal(dos);
                    this.EventID.Marshal(dos);
                    this.Velocity.Marshal(dos);
                    this.LocationInWorldCoordinates.Marshal(dos);
                    this.Descriptor.Marshal(dos);
                    this.LocationInEntityCoordinates.Marshal(dos);
                    dos.WriteUnsignedByte((byte)this.DetonationResult);
                    dos.WriteUnsignedByte((byte)this.VariableParameters.Count);
                    dos.WriteUnsignedShort((ushort)this.Pad);

                    for (int idx = 0; idx < this.VariableParameters.Count; idx++)
                    {
                        VariableParameter aVariableParameter = (VariableParameter)this.VariableParameters[idx];
                        aVariableParameter.Marshal(dos);
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Trace.WriteLine(e);
                    Trace.Flush();
#endif
                    RaiseExceptionOccured(e);

                    if (ThrowExceptions)
                    {
                        throw;
                    }
                }
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Due to ignoring errors.")]
        public override void Unmarshal(DataInputStream dis)
        {
            base.Unmarshal(dis);

            if (dis != null)
            {
                try
                {
                    this.ExplodingEntityID.Unmarshal(dis);
                    this.EventID.Unmarshal(dis);
                    this.Velocity.Unmarshal(dis);
                    this.LocationInWorldCoordinates.Unmarshal(dis);
                    this.Descriptor.Unmarshal(dis);
                    this.LocationInEntityCoordinates.Unmarshal(dis);
                    this.DetonationResult = dis.ReadUnsignedByte();
                    this.NumberOfVariableParameters = dis.ReadUnsignedByte();
                    this.Pad = dis.ReadUnsignedShort();
                    for (int idx = 0; idx < this.NumberOfVariableParameters; idx++)
                    {
                        byte recordType = dis.ReadByte();
                        if (recordType == 0)
                        {
                            VariableParameterArticulated anX = new VariableParameterArticulated();
                            anX.Unmarshal(dis);
                            this.VariableParameters.Add(anX);
                        }
                        else if (recordType == 1)
                        {
                            VariableParameterAttached anX = new VariableParameterAttached();
                            anX.Unmarshal(dis);
                            this.VariableParameters.Add(anX);    
                        }
                    };

                }
                catch (Exception e)
                {
#if DEBUG
                    Trace.WriteLine(e);
                    Trace.Flush();
#endif
                    RaiseExceptionOccured(e);

                    if (ThrowExceptions)
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// This allows for a quick display of PDU data.  The current format is unacceptable and only used for debugging.
        /// This will be modified in the future to provide a better display.  Usage: 
        /// pdu.GetType().InvokeMember("Reflection", System.Reflection.BindingFlags.InvokeMethod, null, pdu, new object[] { sb });
        /// where pdu is an object representing a single pdu and sb is a StringBuilder.
        /// Note: The supplied Utilities folder contains a method called 'DecodePDU' in the PDUProcessor Class that provides this functionality
        /// </summary>
        /// <param name="sb">The StringBuilder instance to which the PDU is written to.</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Due to ignoring errors.")]
        public override void Reflection(StringBuilder sb)
        {
            sb.AppendLine("<DetonationPdu>");
            base.Reflection(sb);
            try
            {
                sb.AppendLine("<explodingEntityID>");
                this.ExplodingEntityID.Reflection(sb);
                sb.AppendLine("</explodingEntityID>");
                sb.AppendLine("<eventID>");
                this.EventID.Reflection(sb);
                sb.AppendLine("</eventID>");
                sb.AppendLine("<velocity>");
                this.Velocity.Reflection(sb);
                sb.AppendLine("</velocity>");
                sb.AppendLine("<locationInWorldCoordinates>");
                this.LocationInWorldCoordinates.Reflection(sb);
                sb.AppendLine("</locationInWorldCoordinates>");
                sb.AppendLine("<descriptor>");
                this.Descriptor.Reflection(sb);
                sb.AppendLine("</descriptor>");
                sb.AppendLine("<locationOfEntityCoordinates>");
                this.LocationInEntityCoordinates.Reflection(sb);
                sb.AppendLine("</locationOfEntityCoordinates>");
                sb.AppendLine("<detonationResult type=\"byte\">" + this.DetonationResult.ToString(CultureInfo.InvariantCulture) + "</detonationResult>");
                sb.AppendLine("<variableParameters type=\"byte\">" + this.VariableParameters.Count.ToString(CultureInfo.InvariantCulture) + "</variableParameters>");
                sb.AppendLine("<pad type=\"ushort\">" + this.Pad.ToString(CultureInfo.InvariantCulture) + "</pad>");
                for (int idx = 0; idx < this.VariableParameters.Count; idx++)
                {
                    sb.AppendLine("<variableParameters" + idx.ToString(CultureInfo.InvariantCulture) + " type=\"VariableParameter\">");
                    VariableParameter aVariableParameter = (VariableParameter)this.VariableParameters[idx];
                    aVariableParameter.Reflection(sb);
                    sb.AppendLine("</variableParameters" + idx.ToString(CultureInfo.InvariantCulture) + ">");
                }

                sb.AppendLine("</DetonationPdu>");
            }
            catch (Exception e)
            {
#if DEBUG
                    Trace.WriteLine(e);
                    Trace.Flush();
#endif
                RaiseExceptionOccured(e);

                if (ThrowExceptions)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this == obj as DetonationPdu;
        }

        /// <summary>
        /// Compares for reference AND value equality.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if both operands are equal; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(DetonationPdu obj)
        {
            bool ivarsEqual = true;

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            ivarsEqual = base.Equals(obj);

            if (!this.ExplodingEntityID.Equals(obj.ExplodingEntityID))
            {
                ivarsEqual = false;
            }

            if (!this.EventID.Equals(obj.EventID))
            {
                ivarsEqual = false;
            }

            if (!this.Velocity.Equals(obj.Velocity))
            {
                ivarsEqual = false;
            }

            if (!this.LocationInWorldCoordinates.Equals(obj.LocationInWorldCoordinates))
            {
                ivarsEqual = false;
            }

            if (!this.Descriptor.Equals(obj.Descriptor))
            {
                ivarsEqual = false;
            }

            if (!this.LocationInEntityCoordinates.Equals(obj.LocationInEntityCoordinates))
            {
                ivarsEqual = false;
            }

            if (this.DetonationResult != obj.DetonationResult)
            {
                ivarsEqual = false;
            }

            if (this.NumberOfVariableParameters != obj.NumberOfVariableParameters)
            {
                ivarsEqual = false;
            }

            if (this.Pad != obj.Pad)
            {
                ivarsEqual = false;
            }

            if (this.VariableParameters.Count != obj.VariableParameters.Count)
            {
                ivarsEqual = false;
            }

            if (ivarsEqual)
            {
                for (int idx = 0; idx < this.VariableParameters.Count; idx++)
                {
                    if (!this.VariableParameters[idx].Equals(obj.VariableParameters[idx]))
                    {
                        ivarsEqual = false;
                    }
                }
            }

            return ivarsEqual;
        }

        /// <summary>
        /// HashCode Helper
        /// </summary>
        /// <param name="hash">The hash value.</param>
        /// <returns>The new hash value.</returns>
        private static int GenerateHash(int hash)
        {
            hash = hash << (5 + hash);
            return hash;
        }

        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            int result = 0;

            result = GenerateHash(result) ^ base.GetHashCode();

            result = GenerateHash(result) ^ this.ExplodingEntityID.GetHashCode();
            result = GenerateHash(result) ^ this.EventID.GetHashCode();
            result = GenerateHash(result) ^ this.Velocity.GetHashCode();
            result = GenerateHash(result) ^ this.LocationInWorldCoordinates.GetHashCode();
            result = GenerateHash(result) ^ this.Descriptor.GetHashCode();
            result = GenerateHash(result) ^ this.LocationInEntityCoordinates.GetHashCode();
            result = GenerateHash(result) ^ this.DetonationResult.GetHashCode();
            result = GenerateHash(result) ^ this.NumberOfVariableParameters.GetHashCode();
            result = GenerateHash(result) ^ this.Pad.GetHashCode();

            if (this.VariableParameters.Count > 0)
            {
                for (int idx = 0; idx < this.VariableParameters.Count; idx++)
                {
                    result = GenerateHash(result) ^ this.VariableParameters[idx].GetHashCode();
                }
            }

            return result;
        }
    }
}
