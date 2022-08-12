using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.FileHelper
{
    public class PdfGenerator
    {
        /// <summary>
        /// Convert Html page at a given URL to a PDF file using open-source tool wkhtml2pdf
        /// wkhtml2pdf can be found at: http://code.google.com/p/wkhtmltopdf/
        /// Useful code used in the creation of this I love the good folk of StackOverflow!: http://stackoverflow.com/questions/1331926/calling-wkhtmltopdf-to-generate-pdf-from-html/1698839
        /// An online manual can be found here: http://madalgo.au.dk/~jakobt/wkhtmltoxdoc/wkhtmltopdf-0.9.9-doc.html
        ///
        /// Ensure that the output folder specified is writeable by the ASP.NET process of IIS running on your server
        ///
        /// This code requires that the Windows installer is installed on the relevant server / client. This can either be found at:
        /// http://code.google.com/p/wkhtmltopdf/downloads/list - download wkhtmltopdf-0.9.9-installer.exe
        /// </summary>
        /// <param name="pdfOutputFolder"></param>
        /// <param name="outputFilenamePrefix"></param>
        /// <param name="urls"></param>
        /// <param name="options"></param>
        /// <param name="pdfHtmlToPdfExePath1"></param>
        /// <returns>the URL of the generated PDF</returns>
        public static string HtmlToPdf(string workingFolder, string outputFileName, string inputFileName,
            string[] options = null)
        {
            string pdfHtmlToPdfExePath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";
            string urlsSeparatedBySpaces = string.Empty;
            options = new string[] { "--disable-external-links" };

            if (!File.Exists(pdfHtmlToPdfExePath))
                pdfHtmlToPdfExePath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe";

            try
            {
                //Determine inputs
                //if ((htmlString == null) || (htmlString.Length == 0))
                //    throw new Exception("No input URLs provided for HtmlToPdf");
                //else
                //    urlsSeparatedBySpaces = String.Join(" ", urls); //Concatenate URLs

                //string outputFilename = outputFilenamePrefix + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".PDF"; // assemble destination PDF file name
                //string outputFilename = outputFileName; // assemble destination PDF file name

                if (!File.Exists(pdfHtmlToPdfExePath))
                    throw new Exception("Pdf App not found!.");

                var p = new System.Diagnostics.Process()
                {
                    StartInfo =
                    {
                        FileName = pdfHtmlToPdfExePath,
                        Arguments = ((options == null) ? "" : String.Join(" ", options)) + " " + inputFileName + " " + outputFileName,
                        UseShellExecute = false, // needs to be false in order to redirect output
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true, // redirect all 3, as it should be all 3 or none
                        WorkingDirectory = workingFolder
                    }
                };

                p.Start();

                // read the output here...
                var output = p.StandardOutput.ReadToEnd();
                var errorOutput = p.StandardError.ReadToEnd();

                // ...then wait n milliseconds for exit (as after exit, it can't read the output)
                p.WaitForExit(180000);

                // read the exit code, close process
                int returnCode = p.ExitCode;
                p.Close();

                // if 0 or 2, it worked so return path of pdf
                if ((returnCode == 0) || (returnCode == 1) || (returnCode == 2))
                    return workingFolder + outputFileName;
                else
                    throw new Exception(errorOutput);
            }
            catch (Exception exc)
            {
                throw new Exception("Problem generating PDF from HTML, URLs: " + urlsSeparatedBySpaces + ", outputFilename: " + outputFileName, exc);
            }
        }

        public static string HtmlToPdf(string outputFilenamePrefix, string[] urls,
    string[] options = null,
    string pdfHtmlToPdfExePath = @"C:\Program Files\wkhtmltopdf\bin\wkhtmltopdf.exe")
        {
            string urlsSeparatedBySpaces = string.Empty;
            try
            {
                //Determine inputs
                if ((urls == null) || (urls.Length == 0))
                    throw new Exception("No input URLs provided for HtmlToPdf");
                else
                    urlsSeparatedBySpaces = String.Join(" ", urls); //Concatenate URLs

                string outputFilename = outputFilenamePrefix + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".PDF"; // assemble destination PDF file name

                var p = new System.Diagnostics.Process()
                {
                    StartInfo =
            {
                FileName = pdfHtmlToPdfExePath,
                Arguments = ((options == null) ? "" : string.Join(" ", options)) + " " + urlsSeparatedBySpaces + " " + outputFilename,
                UseShellExecute = false, // needs to be false in order to redirect output
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true, // redirect all 3, as it should be all 3 or none
                WorkingDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)),
            }
                };

                p.Start();

                // read the output here...
                var output = p.StandardOutput.ReadToEnd();
                var errorOutput = p.StandardError.ReadToEnd();

                // ...then wait n milliseconds for exit (as after exit, it can't read the output)
                p.WaitForExit(60000);

                // read the exit code, close process
                int returnCode = p.ExitCode;
                p.Close();

                // if 0 or 2, it worked so return path of pdf
                if ((returnCode == 0) || (returnCode == 2))
                    return outputFilename;
                else
                    throw new Exception(errorOutput);
            }
            catch (Exception exc)
            {
                throw new Exception("Problem generating PDF from HTML, URLs: " + urlsSeparatedBySpaces + ", outputFilename: " + outputFilenamePrefix, exc);
            }
        }

        public static byte[] ConvertHtmlToPdf(string html)
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "ApplicationName");
            Directory.CreateDirectory(directory);
            var filePath = Path.Combine(directory, $"{Guid.NewGuid()}.html");
            File.WriteAllText(filePath, html);

            var driverOptions = new ChromeOptions();
            // In headless mode, PDF writing is enabled by default (tested with driver major version 85)
            driverOptions.AddArgument("headless");
            var driver = new ChromeDriver(driverOptions);
            driver.Navigate().GoToUrl(filePath);

            // Output a PDF of the first page in A4 size at 90% scale
            var printOptions = new Dictionary<string, object>
    {
        { "paperWidth", 210 / 25.4 },
        { "paperHeight", 297 / 25.4 },
        { "scale", 0.9 },
        { "pageRanges", "1" }
    };
            var printOutput = driver.ExecuteCustomDriverCommand("Page.printToPDF", printOptions) as Dictionary<string, object>;
            var pdf = Convert.FromBase64String(printOutput["data"] as string);

            File.Delete(filePath);

            return pdf;
        }
    }
}
