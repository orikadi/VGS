using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace VGS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //in the view, just make the table pretty and make sure that it is known that is in euro
        public ActionResult CurrencyExchange()
        {
            XmlReader xmlReader = XmlReader.Create("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml"); //get the xml from the web address
            Dictionary<string, double> dict = new Dictionary<string, double>();
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube")) //read all nodes in the xml and take only the ones with name="Cube"
                {
                    if (xmlReader.HasAttributes)
                        try //some of the nodes with the name are without the value
                        {
                            dict.Add(xmlReader.GetAttribute("currency"), double.Parse(xmlReader.GetAttribute("rate")));
                        }catch (Exception){}
                }
            }
            return View(dict);
        }
    }
}