using System.IO;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;

namespace MyMvcProject.Services
{
    public class DocumentConversionService
    {
        public string ConvertDocxToHtml(string filePath)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                // Open the DOCX file as a stream and copy it into an expandable MemoryStream
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fileStream.CopyTo(memStream);
                }

                // Reset the position of the MemoryStream
                memStream.Position = 0;

                using (WordprocessingDocument doc = WordprocessingDocument.Open(memStream, true))
                {
                    var settings = new HtmlConverterSettings();
                    XElement html = HtmlConverter.ConvertToHtml(doc, settings);
                    string htmlString = html.ToString(SaveOptions.DisableFormatting);

                    // Replace placeholders with interactive form inputs
                    htmlString = htmlString
                        .Replace("Name of Inspector:", "Name of Inspector: <input type='text' name='Name of Inspector' required />")
                        .Replace("Email:", "Email: <input type='email' name='Email' required />")
                        .Replace("Date of Birth:", "Date of Birth: <input type='date' name='DateOfBirth' />");

                    return htmlString;
                }
            }
        }

    }

}
