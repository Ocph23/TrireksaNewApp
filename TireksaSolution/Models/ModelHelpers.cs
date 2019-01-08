using ModelsShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsShared
{
    public static class ModelHelpers
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



    }
}
