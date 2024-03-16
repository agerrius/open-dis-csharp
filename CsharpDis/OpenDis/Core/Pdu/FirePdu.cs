using OpenDis.Core.PduFamily;
using OpenDis.Core.DataTypes;
using OpenDis.Enumerations;
using System.Xml.Serialization;

namespace OpenDis.Core.Pdu
{
    public abstract class FirePdu : WarfareFamilyPdu
    {
        public FirePdu(ProtocolVersion protocolVersion) : base(protocolVersion){}
        
        /// <summary>
        /// Gets or sets the ID of the munition that is being shot
        /// </summary>
        [XmlElement(Type = typeof(EntityID), ElementName = "munitionID")]
        public virtual EntityID MunitionID { get; set; } = new EntityID();

        /// <summary>
        /// Gets or sets the ID of event
        /// </summary>
        [XmlElement(Type = typeof(EventID), ElementName = "eventID")]
        public EventID EventID { get; set; } = new EventID();

        /// <summary>
        /// Gets or sets the fireMissionIndex
        /// </summary>
        [XmlElement(Type = typeof(int), ElementName = "fireMissionIndex")]
        public uint FireMissionIndex { get; set; }

        /// <summary>
        /// Gets or sets the location of the firing event
        /// </summary>
        [XmlElement(Type = typeof(Vector3Double), ElementName = "locationInWorldCoordinates")]
        public Vector3Double LocationInWorldCoordinates { get; set; } = new Vector3Double();

        /// <summary>
        /// Gets or sets the Describes munitions used in the firing event
        /// </summary>
        [XmlElement(Type = typeof(BurstDescriptor), ElementName = "burstDescriptor")]
        public BurstDescriptor BurstDescriptor { get; set; } = new BurstDescriptor();

        /// <summary>
        /// Gets or sets the Velocity of the ammunition
        /// </summary>
        [XmlElement(Type = typeof(Vector3Float), ElementName = "velocity")]
        public Vector3Float Velocity { get; set; } = new Vector3Float();

        /// <summary>
        /// Gets or sets the range to the target
        /// </summary>
        [XmlElement(Type = typeof(float), ElementName = "range")]
        public float Range { get; set; }
    }
}