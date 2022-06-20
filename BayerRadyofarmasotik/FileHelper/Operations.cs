using BayerRadyofarmasotik.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.FileHelper
{
    public class Operations
    {
        public static Dictionary<string, string> ReadFile(string path)
        {
            var configData = File
            .ReadAllLines(path)
            .Select(x => x.Split('='))
            .Where(x => x.Length > 1)
            .ToDictionary(x => x[0].Trim(), x => x[1]);

            return configData;
        }

        public static void GeneratePdf(string htmlContent)
        {
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
            System.IO.File.WriteAllBytes($"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").Replace("-", string.Empty)}.pdf", pdfBytes);
        }

        public static string CreateHtmlContent(ProductsResponse response)
        {
            string htmlString = @"<html>
                                            <head>
                                                <style>
                                                    body{
                                                        font-family: Arial, Helvetica, sans-serif;
                                                        font-size: 18;
                                                    }
                                                </style>
                                            </head>";
            htmlString += $@"<body>
                                        <h2>RadyoFarmasotik Bildirim</h2>
                                         <h3> Computer ID: lenovo</h3>
                                         <h3> Bildirim Zaman: {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace(".", "/")}</h3>
                                         </br>";

            for (int i = 0; i < response.productResponseList.Count; i++)
            {
                htmlString += $"<h3>Bildirim Urun :#{i + 1}</h3><a>BN: {response.productResponseList[i].bn}</a></br><a>GTIN: {response.productResponseList[i].gtin}</a></br></br>";
            }
            htmlString += "</body></html>";

            return htmlString;
        }
    }
}
