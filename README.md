# JSONWebServiceCall

I have tried to keep the code and logic as simple as possible. So that it should be easily understandable and modifiable.

There are the following main classes which AGL can quickly refer.

People.cs 
This is the class generated for Capturing(Deserializing)  the JSON Data into this .NET Class easliy querying and manipulating.

Pets.cs
This .net class contains the pets per person. The "People: class uses this class as a generic List to capture the pets under each person.

Program.cs 
This is the main C# Program class where all the code and business logic is written in 4 different functions.

Function 1: Creating Web Request and Response Objects to call JSON Web Service 
Function 2: Converting the JSON in stream of bytes
Function 3: The entire process of deserialisation of JSON stream into .NET Classes ( People, pets)
Function 4: The entire process of applying the business rules i.e. extracting the cats for male and female owners and sorting them in alphabetical order (I have used C#- LINQ do the sorting and searching)

Test.cs
This is the NUnit Unit Testing Class, which runs two test cases for matching person names and cat types.
