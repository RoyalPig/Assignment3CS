using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2500_A3_LINQ.Models
{
    public partial class Track
    {
        public string FormattedPriceString
        {
            get { 
                string price = this.UnitPrice.ToString();
                string formattedString = '$' + price;
                return formattedString;
            }
        }

        public string Minutes 
        {
            get {
                //https://learn.microsoft.com/en-us/dotnet/api/system.timespan.frommilliseconds?view=net-8.0
                //https://stackoverflow.com/questions/9993883/convert-milliseconds-to-human-readable-time-lapse
                TimeSpan t = TimeSpan.FromMilliseconds(this.Milliseconds);
                string answer = string.Format("{0:D2}m:{1:D2}s",
                                        t.Minutes,
                                        t.Seconds);
                return answer; 
            }
        }
        
        public string? MegaBytes
        {
            get{
                String megaBytes;
                if (this.Bytes != null)
                {
                    float tempBytes = (float)this.Bytes;

                    float megaBytesUnformat = tempBytes / 1024 / 1024;

                    megaBytes = string.Format("{0:F2}MB", megaBytesUnformat);

                } else {
                    megaBytes = "N/A";
                }
                return megaBytes;
            }
        }

    }
}
