using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace JSONWebServiceCall
{
    class Program
    {
        static void Main(string[] args)
        {

            // STEP - 1 --Creating a Web Request and Response Objects to read the JSON from the URL
            HttpWebResponse Response =   JSONWebRequest("http://agl-developer-test.azurewebsites.net/people.json");

            // STEP -2  --Generating Stream for json Data Response
            StreamReader JSONstream = GenerateStreamofJSON(Response);

            // STEP -3  --Process of Deserialization from JSON to .NET OBJECTS(People CLASS) using DataContractJSONDeSerializer 
            List<People> people = DeserializeJSONStreamTODotNetObject(JSONstream);

            // STEP -4  --LINQ Query to Apply the Business Logic and Print the desired output of Screen..
            ApplyBusinessRulesandPrinttheOutput(people);

        }


        private static HttpWebResponse JSONWebRequest(string URI)
        {
            //Creating a Web Request Object to read the URL
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(URI);
            Request.ContentType = "application/json";

            //Creating a Web Response Object for Receiving the JSON Data
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();

            return Response;

        }
   
        private static StreamReader GenerateStreamofJSON(HttpWebResponse Response)
        {
            Stream jsonstream = Response.GetResponseStream();
            StreamReader streamReader = new StreamReader(jsonstream);
            return streamReader;
        }

    

        private static List<People> DeserializeJSONStreamTODotNetObject(StreamReader JSONstream)
        {
            List<People> people;
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(JSONstream.ReadToEnd())))
            {
               
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<People>));

                people = (List<People>)js.ReadObject(ms);

            }
            //Filtering out  the people with valid PET records, this would eliminate the people where pet is NULL...
            List<People> validPeoplewithPets = people.Where(p => p.pets != null).ToList();

            return validPeoplewithPets;
        }


        private static void ApplyBusinessRulesandPrinttheOutput(List<People> people)
        {
            // The below LINQ to Object Query, Filtering out the People who are the owners of CAT. The below code also orders the result set based 
            // Gender of the Person and then by the name of the pet and the pet should be CAT. The entire logic is done in single query..
            var result =

                people.SelectMany(s => s.pets, (persongenders, petname) =>
                                   new { Gender = persongenders.gender, Petname = petname.name, Type = petname.type }).
                                   OrderBy(s => s.Gender).
                                   ThenBy(s => s.Petname).
                                   Where(s => s.Type == "Cat");

            //The below lINQ query is givimg me the number of Male Owner who owns CATS
            int countofCatOwners_Mail = people.SelectMany(s => s.pets, (persongenders, petname) =>
                                   new { Gender = persongenders.gender, Type = petname.type }).

                                   Where(s => s.Type == "Cat" && s.Gender == "Male").Count();

            //The below lINQ query is givimg me the number of Female Owner who owns CATS
            int countofCatMailOwners_Female = people.SelectMany(s => s.pets, (persongenders, petname) =>
                                   new { Gender = persongenders.gender, Type = petname.type }).

                                   Where(s => s.Type == "Cat" && s.Gender == "Female").Count();


            //PRINTING THE RESULT TO THE CONSOLE--IF THE MALES OR FEMALES do not own a CAT..it would show "Don't own any CAT under their Heading/"/ 

            Console.WriteLine("MALE");
            foreach (var v in result)
            {
                //Printing Cats under MALE owners
                if (countofCatOwners_Mail == 0)
                {
                    Console.WriteLine("\t" + "Don't own any CAT");

                }

                if (v.Gender == "Male")
                {

                    Console.WriteLine("\t" + v.Petname);
                }

            }
            Console.WriteLine("FEMALE");
            foreach (var v in result)
            {
                if (countofCatMailOwners_Female == 0)
                {
                    Console.WriteLine("\t" + "Don't own any CAT");

                }

                if (v.Gender == "Female")
                {

                    Console.WriteLine("\t" + v.Petname);
                }
            }

            Console.ReadLine();
        }

       
    }
}
