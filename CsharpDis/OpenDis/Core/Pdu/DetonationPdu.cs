using OpenDis.Core.PduFamily;
using OpenDis.Enumerations;
using System.Xml.Serialization;
using OpenDis.Core.DataTypes;

namespace OpenDis.Core.Pdu
{
    public abstract class DetonationPdu : WarfareFamilyPdu
    {
        public DetonationPdu(ProtocolVersion protocolVersion) : base(protocolVersion)
        {
        }
        
        /// <summary>
        /// Gets or sets the ID of muntion that was fired
        /// </summary>
        [XmlElement(Type = typeof(EntityID), ElementName = "munitionID")]
        public EntityID MunitionID { get; set; } = new EntityID();

        /// <summary>
        /// Gets or sets the ID firing event
        /// </summary>
        [XmlElement(Type = typeof(EventID), ElementName = "eventID")]
        public EventID EventID { get; set; } = new EventID();

        /// <summary>
        /// Gets or sets the ID firing event
        /// </summary>
        [XmlElement(Type = typeof(Vector3Float), ElementName = "velocity")]
        public Vector3Float Velocity { get; set; } = new();

        /// <summary>
        /// Gets or sets the where the detonation is, in world coordinates
        /// </summary>
        [XmlElement(Type = typeof(Vector3Double), ElementName = "locationInWorldCoordinates")]
        public Vector3Double LocationInWorldCoordinates { get; set; } = new Vector3Double();

        /// <summary>
        /// Gets or sets the Describes munition used
        /// </summary>
        [XmlElement(Type = typeof(BurstDescriptor), ElementName = "burstDescriptor")]
        public BurstDescriptor BurstDescriptor { get; set; } = new BurstDescriptor();

        /// <summary>
        /// Gets or sets the location of the detonation or impact in the target entity's coordinate system. This information
        /// should be used for damage assessment.
        /// </summary>
        [XmlElement(Type = typeof(Vector3Float), ElementName = "locationInEntityCoordinates")]
        public Vector3Float LocationInEntityCoordinates { get; set; } = new Vector3Float();

        /// <summary>
        /// Gets or sets the result of the explosion
        /// </summary>
        [XmlElement(Type = typeof(byte), ElementName = "detonationResult")]
        public byte DetonationResult { get; set; }
    }
}