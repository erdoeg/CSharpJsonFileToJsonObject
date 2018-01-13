using System;
using System.IO;

namespace StatusContrato
{
    class Program
    {

        public class RootObject
        {
            public Row[] rows { get; set; }
        }

        public class Row
        {
            public string displayName { get; set; }
            public string statusSignature { get; set; }
            public string displayRow { get; set; }
            public Document[] documents { get; set; }
        }

        public class Document
        {
            public string pdfName { get; set; }
            public string[] mandatorySignatureList { get; set; }
        }


        static void Main(string[] args)
        {
            string jsonData = File.ReadAllText(@"C:\Users\vivianem\Desktop\0111466529\metadata\inconfig.json");
            RootObject b = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(jsonData);

            var isNone = false;
            var isSigned = false;

            foreach (Row r in b.rows)
            {
                if (r.statusSignature.ToLower() == "signed")
                {
                    Console.WriteLine("-> The role [ {0} ]  signed the contract", r.displayName);
                    isSigned = true;
                }
                else
                {
                    Console.WriteLine("-> The role [ {0} ]  DID NOT sign the contract", r.displayName);
                    isNone = true;
                }
            }

            if (isNone && isSigned)
            {
                Console.WriteLine("\n*** DOCUMENT PARTIALLY SIGNED ***");
            }
            else if (isNone && !isSigned)
            {
                Console.WriteLine("\n*** DOCUMENT NOT SIGNED ***");
            }
            else
            {
                Console.WriteLine("\n*** DOCUMENT FULLY SIGNED ***");
            }            

        }
    }

}

