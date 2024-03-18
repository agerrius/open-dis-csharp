using OpenDis.Core.PduFamily;
using OpenDis.Enumerations;
using System.Collections.Generic;
using System.Xml.Serialization;
using OpenDis.Core.DataTypes;

namespace OpenDis.Core.Pdu
{
    public class SetDataPdu : SimulationManagementFamilyPdu
    {
        public SetDataPdu(ProtocolVersion protocolVersion) : base(protocolVersion){}
        
        /// <summary>
        /// Gets or sets the ID of request
        /// </summary>
        [XmlElement(Type = typeof(uint), ElementName = "requestID")]
        public uint RequestID { get; set; }

        /// <summary>
        /// Gets or sets the padding
        /// </summary>
        [XmlElement(Type = typeof(uint), ElementName = "padding1")]
        public uint Padding1 { get; set; }

        /// <summary>
        /// Gets or sets the Number of fixed datum records
        /// </summary>
        /// <remarks>
        /// Note that setting this value will not change the marshalled value. The list whose length this describes is used
        /// for that purpose.
        /// The getnumberOfFixedDatumRecords method will also be based on the actual list length rather than this value.
        /// The method is simply here for completeness and should not be used for any computations.
        /// </remarks>
        [XmlElement(Type = typeof(uint), ElementName = "numberOfFixedDatumRecords")]
        public uint NumberOfFixedDatumRecords { get; set; }

        /// <summary>
        /// Gets or sets the Number of variable datum records
        /// </summary>
        /// <remarks>
        /// Note that setting this value will not change the marshalled value. The list whose length this describes is used
        /// for that purpose.
        /// The getnumberOfVariableDatumRecords method will also be based on the actual list length rather than this value.
        /// The method is simply here for completeness and should not be used for any computations.
        /// </remarks>
        [XmlElement(Type = typeof(uint), ElementName = "numberOfVariableDatumRecords")]
        public uint NumberOfVariableDatumRecords { get; set; }

        /// <summary>
        /// Gets the variable length list of fixed datums
        /// </summary>
        [XmlElement(ElementName = "fixedDatumsList", Type = typeof(List<FixedDatum>))]
        public List<FixedDatum> FixedDatums { get; } = new();

        /// <summary>
        /// Gets the variable length list of variable length datums
        /// </summary>
        [XmlElement(ElementName = "variableDatumsList", Type = typeof(List<VariableDatum>))]
        public List<VariableDatum> VariableDatums { get; } = new();
    }
}