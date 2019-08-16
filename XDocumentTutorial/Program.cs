using System;
using System.Xml.Linq;

namespace XDocumentTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set Namespace variables here
            var xmlns = XNamespace.Get("http://example.com");
            var xsi = XNamespace.Get("http://example.com/Header");

            // Set Default doc stuff (Optional)
            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null));

            // Set the Root. This will usually involve the Attributes being set
            // Remember and go in order. IE XMLNS, XMLNS:XSI, XSI:SCHEMALOCATION
            var root = new XElement(xmlns + "FormEvoMessage", 
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "schemaLocation", "http://otherExample.com/API https://otherExample.com/API/other"));

            // This is optional
            var version = new XElement(xmlns + "Version", "1.0");

            // This is blank but may have additional information and attributes to create
            var header = new XElement(xmlns + "Header");

            // This is where everything will be placed. IE documents etc
            var body = new XElement(xmlns + "Body");

            // If you need to reset the XMLNS and shit do it
            xmlns = XNamespace.Get("http://otherxml.com");

            // Create the inner tags for the body
            var getFormLists = new XElement(xmlns + "GetFormLists",
                new XAttribute(xsi + "schemaLocation", "http://blah.com"));

            // Actual document
            var formEvoDoc = new XElement(xmlns + "Document");
            var formEvoDocName = new XElement(xmlns + "Name", "IMG140");
            var formEvoDocDesc = new XElement(xmlns + "Description", "Housing application for England and Wales. Version 4 revision 5");
            var formEvoDocSubmittable = new XElement(xmlns + "Submittable", "True");

            // This section builds the document.
            // Need to start from the inside out
            formEvoDoc.Add(formEvoDocName);
            formEvoDoc.Add(formEvoDocDesc);
            formEvoDoc.Add(formEvoDocSubmittable);

            getFormLists.Add(formEvoDoc);

            body.Add(getFormLists);

            root.Add(version);
            root.Add(header);
            root.Add(body);
            doc.Add(root);

            Console.WriteLine(doc.ToString());
        }
    }
}
