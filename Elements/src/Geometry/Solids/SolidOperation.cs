using Elements.Serialization.JSON;
using Newtonsoft.Json;

namespace Elements.Geometry.Solids
{
    /// <summary>
    /// The base class for all operations which create solids.
    /// </summary>
    public abstract partial class SolidOperation
    {
        internal Solid _solid;

        internal Csg.Solid _csg;

        /// <summary>
        /// The local transform of the operation.
        /// </summary>
        public Transform LocalTransform { get; set; }

        /// <summary>
        /// The solid operation's solid.
        /// </summary>
        [JsonIgnore]
        public Solid Solid
        {
            get { return _solid; }
        }
    }
}