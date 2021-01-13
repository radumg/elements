using Xunit;
using Elements.Geometry;
using Elements.Serialization.glTF;
using System.Collections.Generic;
using System;
using System.IO;

namespace Elements.Tests
{
    public class ContentTests : ModelTest
    {
        private class TestContentElem : ContentElement
        {
            public TestContentElem(string @gltfLocation, BBox3 @bBox, Vector3 @toSource, Transform @transform, double scale, Material @material, Representation @representation, bool @isElementDefinition, System.Guid @id, string @name)
                        : base(gltfLocation, bBox, scale, @toSource, transform, material, representation, isElementDefinition, id, name)
            { }
        }

        [Fact]
        public void CatalogSerialization()
        {
            ContentElement boxType = new ContentElement("../../../models/MergeGlTF/Workstation_Pod_SemiPrivate_Harbour - Semi 1600w x 1300d 3.glb",
                                      new BBox3(new Vector3(-0.5, -0.5, 0), new Vector3(0.5, 0.5, 3)),
                                      1,
                                      new Vector3(),
                                      new Transform(new Vector3(), Vector3.ZAxis),
                                      BuiltInMaterials.Default,
                                      null,
                                      true,
                                      Guid.NewGuid(),
                                      "BoxyType");
            ContentElement boxType2 = new ContentElement("../../../models/MergeGlTF/Box.glb",
                                      new BBox3(new Vector3(-1, -1, 0), new Vector3(1, 1, 2)),
                                      1,
                                      new Vector3(),
                                      new Transform(new Vector3(), Vector3.YAxis),
                                      BuiltInMaterials.Default,
                                      null,
                                      true,
                                      Guid.NewGuid(),
                                      "BoxyType");
            var str = boxType2.ToString();
            boxType.AdditionalProperties["ImportantParameter"] = "The Value";


            var testCatalog = new ContentCatalog(new List<ContentElement> { boxType, boxType2 }, Guid.NewGuid(), "test");

            var savePath = "../../../models/ContentCatalog.json";
            var json = testCatalog.ToJson();
            File.WriteAllText(savePath, json);

            var loadedCatalog = ContentCatalog.FromJson(File.ReadAllText(savePath));

            File.Delete(savePath);
            Assert.Equal(testCatalog.Id, loadedCatalog.Id);
            Assert.Equal(testCatalog.Content.Count, loadedCatalog.Content.Count);
            Assert.Equal("The Value", loadedCatalog.Content[0].AdditionalProperties["ImportantParameter"]);
        }

        [Fact, Trait("Category", "Example")]
        public void InstanceContentElement()
        {
            var model = this.Model;
            // <example>
            var avocadoType = new TestContentElem("../../../models/MergeGlTF/Avocado.glb",
                                      new BBox3(new Vector3(-0.5, -0.5, 0), new Vector3(0.5, 0.5, 3)),
                                      new Vector3(),
                                      new Transform(new Vector3(), Vector3.XAxis),
                                      20,
                                      BuiltInMaterials.Default,
                                      null,
                                      true,
                                      Guid.NewGuid(),
                                      "Avocado Type");
            var duckType = new TestContentElem("../../../models/MergeGlTF/Duck.glb",
                                      new BBox3(new Vector3(-1, -1, 0), new Vector3(1, 1, 2)),
                                      new Vector3(),
                                      new Transform(new Vector3(), Vector3.YAxis),
                                      .005,
                                      BuiltInMaterials.Default,
                                      null,
                                      true,
                                      Guid.NewGuid(),
                                      "Duck Type");
            for (int i = 0; i < 5; i++)
            {
                var newAvo = avocadoType.CreateInstance(new Transform(2 * i, 0, 0), "An Avocado");
                model.AddElement(newAvo);
            }
            var oneDuck = duckType.CreateInstance(new Transform(new Vector3(5, 0, 0)), "A Duck");
            model.AddElement(oneDuck);
            var twoDuck = duckType.CreateInstance(new Transform(new Vector3(15, 0, 0)), "A Duck");
            model.AddElement(twoDuck);
            // </example>
            model.ToGlTF("./models/ContentInstancing.gltf", false);
            model.ToGlTF("./models/ContentInstancing.glb");
        }
    }
}