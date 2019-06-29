using System;
using ModelsShared.Models;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrireksaApp.Common;

namespace TrireksaApp
{
    public class Helper
    {
        public static string GenerateManifestOutGoingCode(int Code, DateTime created)
        {
            var result = string.Format("{0:D5}/OUTGOING/TRP-DJJ/{1}/{2}",
                Code, GetRomawiNumber(created.Month), created.Year);
            return result;
        }

        public static string GenerateInvoiceCode(int Code, DateTime created)
        {
            var result = string.Format("{0:D5}/INV/TRP-DJJ/{1}/{2}",
                Code, GetRomawiNumber(created.Month), created.Year);
            return result;
        }

        private static object GetRomawiNumber(int month)
        {
            switch (month)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
                case 5:
                    return "V";
                case 6:
                    return "VI";
                case 7:
                    return "VII";
                case 8:
                    return "VIII";
                case 9:
                    return "IX";
                case 10:
                    return "X";
                case 11:
                    return "XI";
                case 12:
                    return "XII";
                default:
                    return string.Empty;
            }
        }

        public static int GetNewOutgoingManifestCode(manifestoutgoing lastitem)
        {
            if (lastitem == null)
                return 1;
            else
                return lastitem.Code + 1;
        }





        //QR Code
        public static byte[] GenerateQRCodeBitmap(string data, int size)
        {
            Bitmap image = GenerateQR(data, size);
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);

                stream.Position = 0;
                return stream.ToArray();
            }

        }


        public static Bitmap GenerateQR(string data, int size)
        {
            try
            {
                QRCodeGenerator qrgenerator = new QRCodeGenerator();
                QRCodeData qrdata = qrgenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                QRCode qrcoder = new QRCode(qrdata);

                var path = AppDomain.CurrentDomain.BaseDirectory;
                var logo = new Bitmap(string.Format("{0}\\Images\\QrLogo2.bmp", path));
                Bitmap image = qrcoder.GetGraphic(size, Color.Black, Color.White, logo, 20, 3, true);
                return image;
            }
            catch (Exception ex)
            {

                throw new SystemException(ex.Message);
            }
        }

        public static BitmapImage GenerateQRCode(string data, int size)
        {
            Bitmap image = GenerateQR(data, size);
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
               result.EndInit();
                result.Freeze();
                return result;
            }
            
        }

        public static void PrintNotaAction(penjualan obj)
        {
            var MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            var item = obj;
            var userP = ResourcesBase.UserIsLogin.Email.Split('@');
            List<Reports.Models.NotaReportModel> list = new List<Reports.Models.NotaReportModel>();
            var data = new Reports.Models.NotaReportModel
            {
                STT = string.Format("{0:D5}", item.STT),
                Content = item.Content,
                DoNumber = item.DoNumber,
                Note = item.Note,
                PayTypeName = item.PayType.ToString(),
                Pcs = item.Details.Count,
                PortTypeName = item.PortType.ToString(),
                Price = item.Price, ChangeDate=item.ChangeDate, UserName= userP[0].ToString(),
                Tax = item.Tax,
                PackingCosts = item.PackingCosts,
                Etc = item.Etc,

            };
            var weight = item.Details.Sum(O => O.Weight);
            data.Costs = weight * item.Price;
            data.TaxValue = (item.Tax * data.Costs) / 100;
            if (item.TypeOfWeight == TypeOfWeight.Volume)
                data.Volume = weight;
            else
                data.Weight = weight;

            data.GradTotal = data.Costs + data.PackingCosts + data.Etc + data.TaxValue;
            var shiperTlp = string.Concat("Tlp/Hp. ",item.Shiper.Phone1, "/", item.Shiper.Phone2,  "/", item.Shiper.Handphone);
            var recieverTlp = string.Concat("Tlp/Hp. ", item.Reciver.Phone1, "/", item.Reciver.Phone2,"/", item.Reciver.Handphone);
            data.Shiper = string.Concat(item.Shiper.Name, Environment.NewLine, item.Shiper.Address,Environment.NewLine, shiperTlp);
            var ShiperCity = MainVM.CityCollection.Source.Where(O => O.Id == item.Shiper.CityID).FirstOrDefault();
            data.OriginPortCode = ShiperCity.CityCode;
            data.ShiperCity = ShiperCity.CityName;
            var ReciverCity = MainVM.CityCollection.Source.Where(O => O.Id == item.Reciver.CityID).FirstOrDefault();
            data.DestinationPortCode = ReciverCity.CityCode;
            data.ReciverCity = ReciverCity.CityName;
            data.Reciver = string.Concat(item.Reciver.Name, Environment.NewLine, item.Reciver.Address,Environment.NewLine,recieverTlp);
            data.STTQRCode = Helper.GenerateQRCodeBitmap(item.STT.ToString(), 20);


            list.Add(data);
                          

            HelperPrint print = new HelperPrint();
            print.PrintNota(list, "TrireksaApp.Reports.Layouts.NotaComplete.rdlc", null);

            //var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = list },
            //    "TrireksaApp.Reports.Layouts.NotaOne.rdlc", null);
            //var dlg = new ModernWindow
            //{
            //    Content = content,
            //    Title = "Nota",
            //    Style = (Style)App.Current.Resources["BlankWindow"],
            //    ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
            //    WindowState = WindowState.Maximized,
            //};

            //dlg.ShowDialog();
        }

        public static void PrintPreviewNotaAction(penjualan obj)
        {
            var MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            var item = obj;
            var userP = ResourcesBase.UserIsLogin.Email.Split('@');
            List<Reports.Models.NotaReportModel> list = new List<Reports.Models.NotaReportModel>();
            var data = new Reports.Models.NotaReportModel
            {
                STT = string.Format("{0:D5}", item.STT),
                Content = item.Content,
                DoNumber = item.DoNumber,
                Note = item.Note,
                PayTypeName = item.PayType.ToString(),
                Pcs = item.Details.Count,
                PortTypeName = item.PortType.ToString(),
                Price = item.Price,
                ChangeDate = item.ChangeDate,
                UserName = userP[0].ToString(),
                Tax = item.Tax,
                PackingCosts = item.PackingCosts,
                Etc = item.Etc,

            };
            var weight = item.Details.Sum(O => O.Weight);
            data.Costs = weight * item.Price;
            data.TaxValue = (item.Tax * data.Costs) / 100;
            if (item.TypeOfWeight == TypeOfWeight.Volume)
                data.Volume = weight;
            else
                data.Weight = weight;

            data.GradTotal = data.Costs + data.PackingCosts + data.Etc + data.TaxValue;
            var shiperTlp = string.Concat("Tlp/Hp. ", item.Shiper.Phone1, "/", item.Shiper.Phone2, "/", item.Shiper.Handphone);
            var recieverTlp = string.Concat("Tlp/Hp. ", item.Reciver.Phone1, "/", item.Reciver.Phone2, "/", item.Reciver.Handphone);
            data.Shiper = string.Concat(item.Shiper.Name, Environment.NewLine, item.Shiper.Address, Environment.NewLine, shiperTlp);
            var ShiperCity = MainVM.CityCollection.Source.Where(O => O.Id == item.Shiper.CityID).FirstOrDefault();
            data.OriginPortCode = ShiperCity.CityCode;
            data.ShiperCity = ShiperCity.CityName;
            var ReciverCity = MainVM.CityCollection.Source.Where(O => O.Id == item.Reciver.CityID).FirstOrDefault();
            data.DestinationPortCode = ReciverCity.CityCode;
            data.ReciverCity = ReciverCity.CityName;
            data.Reciver = string.Concat(item.Reciver.Name, Environment.NewLine, item.Reciver.Address, Environment.NewLine, recieverTlp);
            data.STTQRCode = Helper.GenerateQRCodeBitmap(item.STT.ToString(), 20);


            list.Add(data);



            var content = new Reports.Contents.ReportContent(new Microsoft.Reporting.WinForms.ReportDataSource { Value = list },
                "TrireksaApp.Reports.Layouts.NotaComplete.rdlc", null);
            var dlg = new ModernWindow
            {
                Content = content,
                Title = "Nota",
                Style = (Style)App.Current.Resources["BlankWindow"],
                ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip,
                WindowState = WindowState.Maximized,
            };

            dlg.ShowDialog();
        }

    }

    public static class TerbilangExtension
    {
        public static string Terbilang(this int value)
        {
            return ((decimal)value).Terbilang();
        }

        public static string Terbilang(this long value)
        {
            return ((decimal)value).Terbilang();
        }

        public static string Terbilang(this float value)
        {
            return ((decimal)Math.Truncate(value)).Terbilang();
        }

        public static string Terbilang(this double value)
        {
            return ((decimal)Math.Truncate(value)).Terbilang();
        }

        public static string Terbilang(this decimal value)
        {
            if (value <= 0) return string.Empty;

            StringBuilder sb = new StringBuilder();

            value = Math.Truncate(value);
            string sValue = value.ToString();
            int x = (sValue.Length / 3);
            if (sValue.Length % 3 == 0) x--;

            IEnumerable<string> sValues = sValue.SplitInParts(3);

            foreach (var item in sValues)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (x == 0)
                    {
                        sb.Append(item);
                    }
                    else if ((x == 1))
                    {
                        if (item == TerbilangConstant.SATU)
                            sb.Append(TerbilangConstant.SERIBU);
                        else
                            sb.bilangan(item, TerbilangConstant.RIBU);
                    }
                    else if (x == 2)
                        sb.bilangan(item, TerbilangConstant.JUTA);
                    else if (x == 3)
                        sb.bilangan(item, TerbilangConstant.MILYAR);
                    else if (x == 4)
                        sb.bilangan(item, TerbilangConstant.TRILIUN);
                    else if (x == 5)
                        sb.bilangan(item, TerbilangConstant.KUADRILIUN);
                    else if (x == 6)
                        sb.bilangan(item, TerbilangConstant.KUANTILIUN);
                    else if (x == 7)
                        sb.bilangan(item, TerbilangConstant.SEKTILIUN);
                    else if (x == 8)
                        sb.bilangan(item, TerbilangConstant.SEPTILIUN);
                    else if (x == 9)
                        sb.bilangan(item, TerbilangConstant.OKTILIUN);
                }
                x--;
            }
            return sb.ToString().Trim();
        }

        private static IEnumerable<string> SplitInParts(this string s, int partLength)
        {
            for (int i = 0; i < s.Length; i++)
            {
                int m = i;
                int l = 3;
                if (i == 0)
                {
                    l = s.Length % partLength;
                    if (l == 0) l = 3;
                }
                i += l - 1;
                yield return ParseRatusan(s.Substring(m, l).PadLeft(3, '0'));
            }
        }

        private static void bilangan(this StringBuilder sb, string value, string bilangan)
        {
            sb.Append(value);
            sb.Append(bilangan);
        }

        private static string ParseSatuan(string s)
        {
            if (s == TerbilangConstant.N_SATU)
                return TerbilangConstant.SATU;
            else if (s == TerbilangConstant.N_DUA)
                return TerbilangConstant.DUA;
            else if (s == TerbilangConstant.N_TIGA)
                return TerbilangConstant.TIGA;
            else if (s == TerbilangConstant.N_EMPAT)
                return TerbilangConstant.EMPAT;
            else if (s == TerbilangConstant.N_LIMA)
                return TerbilangConstant.LIMA;
            else if (s == TerbilangConstant.N_ENAM)
                return TerbilangConstant.ENAM;
            else if (s == TerbilangConstant.N_TUJUH)
                return TerbilangConstant.TUJUH;
            else if (s == TerbilangConstant.N_DELAPAN)
                return TerbilangConstant.DELAPAN;
            else if (s == TerbilangConstant.N_SEMBILAN)
                return TerbilangConstant.SEMBILAN;
            else return string.Empty;
        }

        private static void ParsePuluhan(this StringBuilder sb, string s)
        {
            string s1 = s.Substring(0, 1);
            string s2 = s.Substring(1);
            if (s1 == TerbilangConstant.N_SATU)
            {
                if (s2 == TerbilangConstant.N_NOL) sb.Append(TerbilangConstant.SEPULUH);
                else if (s2 == TerbilangConstant.N_SATU) sb.Append(TerbilangConstant.SEBELAS);
                else sb.bilangan(ParseSatuan(s2), TerbilangConstant.BELAS);
            }
            else
            {
                if (s1 != TerbilangConstant.N_NOL) sb.bilangan(ParseSatuan(s1), TerbilangConstant.PULUH);
                sb.Append(ParseSatuan(s2));
            }
        }
        private static string ParseRatusan(string s)
        {
            var sb = new StringBuilder();
            string s1 = s.Substring(0, 1);
            string s2 = s.Substring(1, 2);
            if (s1 == TerbilangConstant.N_SATU) sb.Append(TerbilangConstant.SERATUS);
            else if (s1 != TerbilangConstant.N_NOL) sb.bilangan(ParseSatuan(s1), TerbilangConstant.RATUS);
            sb.ParsePuluhan(s2);
            return sb.ToString();
        }
    }

    internal static class TerbilangConstant
    {
        internal const string N_NOL = "0";
        internal const string N_SATU = "1";
        internal const string N_DUA = "2";
        internal const string N_TIGA = "3";
        internal const string N_EMPAT = "4";
        internal const string N_LIMA = "5";
        internal const string N_ENAM = "6";
        internal const string N_TUJUH = "7";
        internal const string N_DELAPAN = "8";
        internal const string N_SEMBILAN = "9";

        internal const string SATU = " Satu";
        internal const string DUA = " Dua";
        internal const string TIGA = " Tiga";
        internal const string EMPAT = " Empat";
        internal const string LIMA = " Lima";
        internal const string ENAM = " Enam";
        internal const string TUJUH = " Tujuh";
        internal const string DELAPAN = " Delapan";
        internal const string SEMBILAN = " Sembilan";
        internal const string SEPULUH = " Sepuluh";
        internal const string SEBELAS = " Sebelas";
        internal const string PULUH = " Puluh";
        internal const string BELAS = " Belas";

        internal const string SERIBU = " Seribu";
        internal const string SERATUS = " Seratus";
        internal const string RATUS = " Ratus";
        internal const string RIBU = " Ribu";
        internal const string JUTA = " Juta";
        internal const string MILYAR = " Milyar";
        internal const string TRILIUN = " Triliun";
        internal const string KUADRILIUN = " Kuadriliun";
        internal const string KUANTILIUN = " Kuantiliun";
        internal const string SEKTILIUN = " Sekstiliun";
        internal const string SEPTILIUN = " Septiliun";
        internal const string OKTILIUN = " Oktiliun";
    }
}
