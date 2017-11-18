using NUnit.Framework;
using System;
using JSONWebServiceCall;

namespace UnitTestingForJSON
{
    [TestFixture()]
    public class JSONTest
    {
        [Test()]
        public void TestCase1()
        {
            Program prg = new Program();

            HttpWebResponse Response = JSONWebRequest("http://agl-developer-test.azurewebsites.net/people.json");

            StreamReader JSONstream = GenerateStreamofJSON(Response);

            List<People> people = DeserializeJSONStreamTODotNetObject(JSONstream);

            Assert.AreEqual("Bob",people[0].name);
            Assert.AreEqual("Jennifer", people[1].name);
            Assert.AreEqual("Steve", people[2].name);
            Assert.AreEqual("Fred", people[3].name);    
            Assert.AreEqual("Samantha", people[4].name);
            Assert.AreEqual("Alice", people[5].name);

           
        }
        [Test()]
        public void TestCase2()
        {
            Program prg = new Program();

            HttpWebResponse Response = JSONWebRequest("http://agl-developer-test.azurewebsites.net/people.json");

            StreamReader JSONstream = GenerateStreamofJSON(Response);

            List<People> people = DeserializeJSONStreamTODotNetObject(JSONstream);

            Assert.AreEqual("Cat", people[0].pets[0].type);
            Assert.AreEqual("Cat", people[1].pets[0].type);
            Assert.AreEqual("Cat", people[3].pets[0].type);
            Assert.AreEqual("Cat", people[4].pets[0].type);
           

        }
    }
}
