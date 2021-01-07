//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.4.0 (Newtonsoft.Json v12.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Properties;
using Elements.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements
{
#pragma warning disable // Disable all warnings

    /// <summary>A wall drawn using the elevation profile</summary>
    [Newtonsoft.Json.JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class WallByProfile : GeometricElement
    {
        [Newtonsoft.Json.JsonConstructor]
        public WallByProfile(Profile @profile, double @thickness, Line @centerline, Transform @transform, Material @material, Representation @representation, bool @isElementDefinition, System.Guid @id, string @name)
            : base(transform, material, representation, isElementDefinition, id, name)
        {
            var validator = Validator.Instance.GetFirstValidatorForType<WallByProfile>();
            if (validator != null)
            {
                validator.PreConstruct(new object[] { @profile, @thickness, @centerline, @transform, @material, @representation, @isElementDefinition, @id, @name });
            }

            this.Profile = @profile;
            this.Thickness = @thickness;
            this.Centerline = @centerline;
        }

        /// <summary>The profile, which includes openings that will be extruded.</summary>
        [Newtonsoft.Json.JsonProperty("Profile", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Profile Profile { get; set; }

        /// <summary>The overall thickness of the wall</summary>
        [Newtonsoft.Json.JsonProperty("Thickness", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double Thickness { get; set; }

        /// <summary>The Centerline of the wall</summary>
        [Newtonsoft.Json.JsonProperty("Centerline", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Line Centerline { get; set; }


    }
}