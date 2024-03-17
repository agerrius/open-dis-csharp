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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using OpenDis.Core;
using OpenDis.Core.PduFamily;
using OpenDis.Core.DataTypes;

namespace OpenDis.Dis1995
{
    /// <summary>
    /// Section 5.3.6.8. Request for data from an entity
    /// </summary>
    [Serializable]
    [XmlRoot]
    [XmlInclude(typeof(FixedDatum))]
    [XmlInclude(typeof(VariableDatum))]
    public partial class DataQueryPdu : SimulationManagementFamilyPdu, IEquatable<DataQueryPdu>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataQueryPdu"/> class.
        /// </summary>
        public DataQueryPdu() : base(Enumerations.ProtocolVersion.Ieee1278_1_1995)
        {
            PduType = 18;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        ///    <c>true</c> if operands are not equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(DataQueryPdu left, DataQueryPdu right) => !(left == right);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        ///    <c>true</c> if both operands are equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(DataQueryPdu left, DataQueryPdu right)
            => ReferenceEquals(left, right) || (left is not null && right is not null && left.Equals(right));

        public override int GetMarshalledSize()
        {
            int marshalSize = base.GetMarshalledSize();
            marshalSize += 4;  // this._requestID
            marshalSize += 4;  // this._timeInterval
            marshalSize += 4;  // this._fixedDatumRecordCount
            marshalSize += 4;  // this._variableDatumRecordCount
            marshalSize += 4 * FixedDatumIDs.Count;
            marshalSize += 4 * VariableDatumIDs.Count;
            return marshalSize;
        }

        /// <summary>
        /// Gets or sets the ID of request
        /// </summary>
        [XmlElement(Type = typeof(uint), ElementName = "requestID")]
        public uint RequestID { get; set; }

        /// <summary>
        /// Gets or sets the time issues between issues of Data PDUs. Zero means send once only.
        /// </summary>
        [XmlElement(Type = typeof(uint), ElementName = "timeInterval")]
        public uint TimeInterval { get; set; }

        /// <summary>
        /// Gets or sets the Number of fixed datum records
        /// </summary>
        /// <remarks>
        /// Note that setting this value will not change the marshalled value. The list whose length this describes is used
        /// for that purpose.
        /// The getfixedDatumRecordCount method will also be based on the actual list length rather than this value.
        /// The method is simply here for completeness and should not be used for any computations.
        /// </remarks>
        [XmlElement(Type = typeof(uint), ElementName = "fixedDatumRecordCount")]
        public uint FixedDatumRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the Number of variable datum records
        /// </summary>
        /// <remarks>
        /// Note that setting this value will not change the marshalled value. The list whose length this describes is used
        /// for that purpose.
        /// The getvariableDatumRecordCount method will also be based on the actual list length rather than this value.
        /// The method is simply here for completeness and should not be used for any computations.
        /// </remarks>
        [XmlElement(Type = typeof(uint), ElementName = "variableDatumRecordCount")]
        public uint VariableDatumRecordCount { get; set; }

        /// <summary>
        /// Gets the variable length list of fixed datums
        /// </summary>
        [XmlElement(ElementName = "fixedDatumsList", Type = typeof(List<uint>))]
        public List<uint> FixedDatumIDs { get; } = new();

        /// <summary>
        /// Gets the variable length list of variable length datums
        /// </summary>
        [XmlElement(ElementName = "variableDatumsList", Type = typeof(List<uint>))]
        public List<uint> VariableDatumIDs { get; } = new();

        ///<inheritdoc/>
        public override void MarshalAutoLengthSet(DataOutputStream dos)
        {
            // Set the length prior to marshalling data
            Length = (ushort)GetMarshalledSize();
            Marshal(dos);
        }

        /// <inheritdoc/>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Due to ignoring errors.")]
        public override void Marshal(DataOutputStream dos)
        {
            base.Marshal(dos);
            if (dos != null)
            {
                try
                {
                    dos.WriteUnsignedInt(RequestID);
                    dos.WriteUnsignedInt(TimeInterval);
                    dos.WriteUnsignedInt((uint)FixedDatumIDs.Count);
                    dos.WriteUnsignedInt((uint)VariableDatumIDs.Count);

                    for (int idx = 0; idx < FixedDatumIDs.Count; idx++)
                    {
                        dos.WriteUnsignedInt(FixedDatumIDs[idx]);
                    }

                    for (int idx = 0; idx < VariableDatumIDs.Count; idx++)
                    {
                        dos.WriteUnsignedInt(VariableDatumIDs[idx]);
                    }
                }
                catch (Exception e)
                {
                    if (TraceExceptions)
                    {
                        Trace.WriteLine(e);
                        Trace.Flush();
                    }

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
                    RequestID = dis.ReadUnsignedInt();
                    TimeInterval = dis.ReadUnsignedInt();
                    FixedDatumRecordCount = dis.ReadUnsignedInt();
                    VariableDatumRecordCount = dis.ReadUnsignedInt();

                    for (int idx = 0; idx < FixedDatumRecordCount; idx++)
                    {
                        FixedDatumIDs.Add(dis.ReadUnsignedInt());
                    }

                    for (int idx = 0; idx < VariableDatumRecordCount; idx++)
                    {
                        VariableDatumIDs.Add(dis.ReadUnsignedInt());
                    }
                }
                catch (Exception e)
                {
                    if (TraceExceptions)
                    {
                        Trace.WriteLine(e);
                        Trace.Flush();
                    }

                    RaiseExceptionOccured(e);

                    if (ThrowExceptions)
                    {
                        throw;
                    }
                }
            }
        }

        /// <inheritdoc/>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Due to ignoring errors.")]
        public override void Reflection(StringBuilder sb)
        {
            sb.AppendLine("<DataQueryPdu>");
            base.Reflection(sb);
            try
            {
                sb.AppendLine("<requestID type=\"uint\">" + RequestID.ToString(CultureInfo.InvariantCulture) + "</requestID>");
                sb.AppendLine("<timeInterval type=\"uint\">" + TimeInterval.ToString(CultureInfo.InvariantCulture) + "</timeInterval>");
                sb.AppendLine("<numberOfFixedDatums type=\"uint\">" + FixedDatumIDs.Count.ToString(CultureInfo.InvariantCulture) + "</numberOfFixedDatums>");
                sb.AppendLine("<numberOfVariableDatums type=\"uint\">" + VariableDatumIDs.Count.ToString(CultureInfo.InvariantCulture) + "</numberOfVariableDatums>");
                for (int idx = 0; idx < FixedDatumIDs.Count; idx++)
                {
                    sb.AppendLine("<fixedDatumID" + idx.ToString(CultureInfo.InvariantCulture) + " type=\"uint\">" + FixedDatumIDs[idx].ToString(CultureInfo.InvariantCulture) + "</fixedDatumID" + idx.ToString(CultureInfo.InvariantCulture) + ">");
                }
                
                for (int idx = 0; idx < VariableDatumIDs.Count; idx++)
                {
                    sb.AppendLine("<variableDatumID" + idx.ToString(CultureInfo.InvariantCulture) + " type=\"uint\">" + VariableDatumIDs[idx].ToString(CultureInfo.InvariantCulture) + "</variableDatumID" + idx.ToString(CultureInfo.InvariantCulture) + ">");
                }
                
                sb.AppendLine("</DataQueryPdu>");
            }
            catch (Exception e)
            {
                if (TraceExceptions)
                {
                    Trace.WriteLine(e);
                    Trace.Flush();
                }

                RaiseExceptionOccured(e);

                if (ThrowExceptions)
                {
                    throw;
                }
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj) => this == obj as DataQueryPdu;

        ///<inheritdoc/>
        public bool Equals(DataQueryPdu obj)
        {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            bool ivarsEqual = base.Equals(obj);
            if (RequestID != obj.RequestID)
            {
                ivarsEqual = false;
            }

            if (TimeInterval != obj.TimeInterval)
            {
                ivarsEqual = false;
            }

            if (FixedDatumRecordCount != obj.FixedDatumRecordCount)
            {
                ivarsEqual = false;
            }

            if (VariableDatumRecordCount != obj.VariableDatumRecordCount)
            {
                ivarsEqual = false;
            }

            if (FixedDatumIDs.Count != obj.FixedDatumIDs.Count)
            {
                ivarsEqual = false;
            }

            if (ivarsEqual)
            {
                for (int idx = 0; idx < FixedDatumIDs.Count; idx++)
                {
                    if (!FixedDatumIDs[idx].Equals(obj.FixedDatumIDs[idx]))
                    {
                        ivarsEqual = false;
                    }
                }
            }

            if (VariableDatumIDs.Count != obj.VariableDatumIDs.Count)
            {
                ivarsEqual = false;
            }

            if (ivarsEqual)
            {
                for (int idx = 0; idx < VariableDatumIDs.Count; idx++)
                {
                    if (!VariableDatumIDs[idx].Equals(obj.VariableDatumIDs[idx]))
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
        private static int GenerateHash(int hash) => hash << (5 + hash);

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int result = 0;

            result = GenerateHash(result) ^ base.GetHashCode();

            result = GenerateHash(result) ^ RequestID.GetHashCode();
            result = GenerateHash(result) ^ TimeInterval.GetHashCode();
            result = GenerateHash(result) ^ FixedDatumRecordCount.GetHashCode();
            result = GenerateHash(result) ^ VariableDatumRecordCount.GetHashCode();

            if (FixedDatumIDs.Count > 0)
            {
                for (int idx = 0; idx < FixedDatumIDs.Count; idx++)
                {
                    result = GenerateHash(result) ^ FixedDatumIDs[idx].GetHashCode();
                }
            }

            if (VariableDatumIDs.Count > 0)
            {
                for (int idx = 0; idx < VariableDatumIDs.Count; idx++)
                {
                    result = GenerateHash(result) ^ VariableDatumIDs[idx].GetHashCode();
                }
            }

            return result;
        }
    }
}
