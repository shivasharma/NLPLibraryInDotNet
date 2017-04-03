using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.stanford.nlp.classify;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLPLibrary.Model;

namespace NLPLibrary.UnitTest
{
    [TestClass]
    public class UnitTest

    {
        [TestMethod]
        public void Should_Rest_Entity_By_Organization()
        {
            //Arrange


            var organization = new List<string> { "Organization" };
            var rawText = " The Facebook is a company APPle INC ";


            var entity = new Entity
            {
                Entities = organization,
                Rawtext = rawText
            };


            var entityEtl = new EntityExtractionService.EntityExtraction();
            //ACT
            // Classifier.classifyWithInlineXML("This is test");

           // var classifierResult = Classifier.classifyWithInlineXML(entity.Rawtext);

            var actual = entityEtl.GetData(entity, null);
            var expected = "test";
            //Assert
        }
    }
}
