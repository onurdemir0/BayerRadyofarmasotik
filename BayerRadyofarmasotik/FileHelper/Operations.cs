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

        //public static void GeneratePdf(string htmlContent)
        //{
        //    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
        //    var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
        //    System.IO.File.WriteAllBytes($"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss").Replace("-", string.Empty)}.pdf", pdfBytes);
        //}

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

            for (int i = 0; i < response.productList.Count; i++)
            {
                htmlString += $"<h3>Bildirim Urun :#{i + 1}</h3><a>BN: {response.productList[i].bn}</a></br><a>GTIN: {response.productList[i].gtin}</a></br>" +
                    $"<a>UC: {GetBildiriResultByUC(response.productList[i].uc)}</a></br></br>";
            }
            htmlString += "</body></html>";

            return htmlString;
        }

        private static string GetBildiriResultByUC(string uc)
        {
            string ucMessage = "";
            switch (uc)
            {
                case "00000":
                    ucMessage = "Urun uzerinde gerceklestirilen islem basarilidir";
                    return ucMessage;
                case "11005":
                    ucMessage = "Bu urune ait bildirim yapma yetkiniz yok";
                    return ucMessage;
                case "10204":
                    ucMessage = "Belirtilen urun daha once satılmıs";
                    return ucMessage;
                case "10202":
                    ucMessage = "Urunun Son Kullanma Tarihi gecmistir";
                    return ucMessage;
                case "11004":
                    ucMessage = "Yanlis GTIN numarasi";
                    return ucMessage;
                case "10206":
                    ucMessage = "Veri tabani kayit hatasi";
                    return ucMessage;
                case "10201":
                    ucMessage = "Belirtilen urun sistemde kayitli degil";
                    return ucMessage;
                case "15029":
                    ucMessage = "Gecersiz (Kalibrasyon/Yüklenen Aktivite) birim degeri";
                    return ucMessage;
                default:
                    ucMessage = $"Bilinmeyen durum! UC kodu: {uc}";
                    return ucMessage;
            }
        }
    }
}
