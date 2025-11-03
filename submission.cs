using System;
using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;
using System.Text;

namespace ConsoleApp1
{
    /**
     * CSE445 Assignment 4 template - filled.
     * Replace the three URLs below with your own GitHub Pages links BEFORE submitting to Gradescope.
     * You only submit this file (submission.cs).
     */
    public class Program
    {
        // TODO: replace with your real URLs (step 3 in the instructions)
        public static string xmlURL = "https://alexander-gmyrek.github.io/cse445_a4/Hotels.xml";
        public static string xmlErrorURL = "https://alexander-gmyrek.github.io/cse445_a4/HotelsErrors.xml";
        public static string xsdURL = "https://alexander-gmyrek.github.io/cse445_a4/Hotels.xsd";

 


        public static void Main(string[] args)
        {
            // Q3: run all three so Gradescope sees output
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            // collect ALL schema validation errors
            StringBuilder errors = new StringBuilder();
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(null, xsdUrl);
                settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler += (object sender, ValidationEventArgs e) =>
                {
                    if (errors.Length > 0) errors.AppendLine();
                    errors.Append(e.Severity + ": " + e.Message);
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read())
                    {
                        // just read to trigger validation
                    }
                }

                if (errors.Length == 0)
                {
                    return "No errors are found";
                }
                else
                {
                    return errors.ToString();
                }
            }
            catch (Exception ex)
            {
                // network / URL / well-formedness errors
                return ex.Message;
            }
        }

        // Q2.2
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);

                // serialize XML to JSON text
                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

                // assignment sample uses "_Rating" instead of "@Rating"
                jsonText = jsonText.Replace("\"@Rating\"", "\"_Rating\"");

                return jsonText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}