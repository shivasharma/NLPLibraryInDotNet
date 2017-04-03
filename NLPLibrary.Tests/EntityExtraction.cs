using System;
using System.Collections.Generic;
using System.IO;
using edu.stanford.nlp.ie.crf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLPLibrary.Api;
using NLPLibrary.Model;

namespace NLPLibrary.Tests
{
    [TestClass]
    public class EntityExtraction

    {
   
        public static CRFClassifier Classifier { get; set; }
        public static CRFClassifier InitializeLibrary()
        {
            //var startupPath = Path.GetFullPath(@"./App_Data");
            var startupPath = Path.GetFullPath(@"C:\Users\Shiva\Documents\visual studio 2015\Projects\StanfordNLPProject\NLPModel");
            var jarRoot = startupPath;
            var classifiersDirecrory = jarRoot + @"\classifiers";
            var classifier = CRFClassifier.getClassifierNoExceptions(
                classifiersDirecrory + @"\english.all.3class.distsim.crf.ser.gz");
            return classifier;
        }
        [TestInitialize]
        public void TestInitialize()
        {
           
            Classifier = InitializeLibrary();
        }
        [TestMethod]
        public  void Should_Rest_Entity_By_Organization()
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

            var classifierResult = Classifier.classifyWithInlineXML(entity.Rawtext);

            var actual = entityEtl.GetData(entity, null);
            var expected = "test";
            //Assert
        }
    }
}
