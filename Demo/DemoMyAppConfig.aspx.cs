using System;
using MyHelper4Web;

namespace Demo
{
    public partial class DemoMyAppConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Run());
            Response.Write(random());
        }

        private string Run()
        {
            const string format = "Before: {0} After: {1} <br/>";

            var config = new MyAppConfigHelper("Web.config");
            string s1= config.AppConfigGet("IsSmtp");
            config.AppConfigSet("IsSmtp", DateTime.Now.ToString("hh:mm:ss"));
            string s2 = config.AppConfigGet("IsSmtp");

            return string.Format(format, s1, s2);
        }
        private string random()
        {
            string result1 = "";
            string result2 = "";
            string result3 = "";
            string result4 = "";
            string result5 = "";
            string result6 = "";
            int[] n1 = MyRandomHelper.Reservoir(10, 100);
            int[] n2 = MyRandomHelper.ShuffleSelect(10, 100);
            int[] n3 = MyRandomHelper.Reservoir(10, 100);
            int[] m1 = MyRandomHelper.Reservoir(10, 100,1);
            int[] m2 = MyRandomHelper.Reservoir(10, 100,2);
            int[] m3 = MyRandomHelper.ShuffleSelect(10, 100, (int)DateTime.Now.Ticks & 0x0000FFFF);

            for (int i = 0; i < 10; i++)
			{
                result1 += string.Format("{0:000},", n1[i]);
                result2 += string.Format("{0:000},", n2[i]);
                result3 += string.Format("{0:000},", n3[i]);
                result4 += string.Format("{0:000},", m1[i]);
                result5 += string.Format("{0:000},", m2[i]);
                result6 += string.Format("{0:000},", m3[i]);

			}

            return result1 + "<br/>" + result2 + "<br/>" + result3 + "<br/>" + result4 + "<br/>" + result5 + "<br/>" + result6 + "<br/>";
        }
    }
}