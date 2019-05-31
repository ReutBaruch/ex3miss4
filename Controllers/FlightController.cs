using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Ex3.Models;


namespace Ex3.Controllers
{
    public class FlightController : Controller
    {
        private ConnectFlight connect;

        // GET: Flight
        [HttpGet]
        public ActionResult display(string ip, int port, int time)
        {
            int ipOrDoc = CheckURL(ip);
            if (ipOrDoc == 0)
            {
                Session["docName"] = ip;
                Session["seconds"] = port;
            }
            else
            {
                if (ipOrDoc == 1)
                {
                    //string[] result = new string[2];
                    connect = ConnectFlight.Instance;
                    connect.ServerConnect(ip, port);
                    //result = connect.SendCommands();
                    //connect.Flight.Lat = connect.PhraserValue(result[0]);
                    //connect.Flight.Lon = connect.PhraserValue(result[1]);

                    Session["time"] = time;
                }
            }
            
            return View();
        }


        int CheckURL(string str)
        {
            string[] words = str.Split('.');
            if (words.Length>1)
            {
                return 1;
            }
            // double result = Convert.ToDouble(words[1]);
            return 0;
        }


        [HttpPost]
        public string GetFlightData()
        {
            string[] result = new string[4];
            result = ConnectFlight.Instance.SendCommands();
            ConnectFlight.Instance.Flight.Lat = ConnectFlight.Instance.PhraserValue(result[0]);
            ConnectFlight.Instance.Flight.Lon = ConnectFlight.Instance.PhraserValue(result[1]);
            //ConnectFlight.Instance.Flight.Rudder = ConnectFlight.Instance.PhraserValue(result[2]);
            //ConnectFlight.Instance.Flight.Throttle = ConnectFlight.Instance.PhraserValue(result[3]);
            var fly = ConnectFlight.Instance.Flight;
            return ToXml(fly);
        }

        [HttpPost]
        public string GetFlightDataForSave()
        {
            string[] result = new string[4];
            result = ConnectFlight.Instance.SendCommands();
            ConnectFlight.Instance.Flight.Lat = ConnectFlight.Instance.PhraserValue(result[0]);
            ConnectFlight.Instance.Flight.Lon = ConnectFlight.Instance.PhraserValue(result[1]);
            ConnectFlight.Instance.Flight.Rudder = ConnectFlight.Instance.PhraserValue(result[2]);
            ConnectFlight.Instance.Flight.Throttle = ConnectFlight.Instance.PhraserValue(result[3]);
            var fly = ConnectFlight.Instance.Flight;
            ConnectFlight.Instance.ReadData(ConnectFlight.Instance.fileName);
            return ToXml(fly);
        }

        private string ToXml(Flight flight)
        {
            if (Models.ConnectFlight.Instance.IsConnect)
            {
                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                XmlWriter writer = XmlWriter.Create(sb, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("Flight");

                writer.WriteElementString("lat", flight.Lat);
                writer.WriteElementString("lon", flight.Lon);
                

                //flight.ToXml(writer);

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

                Console.Write(sb.ToString());
                return sb.ToString();
            }
            return null;
        }

        //string ReadFromFile(string fileName)
        public string ReadFromFile()
        {
            string line;
            string fileName = "file1";
            string path = "C:/Users/renal/Source/Repos/Ex31test/Ex3/fonts/" + fileName + ".txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            //while ((line = file.ReadLine()) != null)

            //{
            line = file.ReadLine();
            if (line != null)
            {
                string[] words = line.Split(';');
                if (words.Length != 4)
                {
                    Console.WriteLine("problem");
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    XmlWriterSettings settings = new XmlWriterSettings();
                    XmlWriter writer = XmlWriter.Create(sb, settings);
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Flight");

                    writer.WriteElementString("lat", words[0]);
                    writer.WriteElementString("lon", words[1]);
                    writer.WriteElementString("rudder", words[2]);
                    writer.WriteElementString("throttle", words[3]);



                    //flight.ToXml(writer);

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Flush();

                    Console.Write(sb.ToString());
                    return sb.ToString();
                }
            }
            //}
            return "blabls";
        }


        [HttpPost]
        public string Search(string data)
        {
            //ConnectFlight.Instance.ReadData(data);
            return ToXml(ConnectFlight.Instance.Flight);
        }

        public string Index()
        {
            return "Welcome to our Project! Please enter a URL";
        }

        [HttpGet]
        public ActionResult save(string ip, int port, int time, int seconds, string files)
        {
            connect = ConnectFlight.Instance;
            connect.ServerConnect(ip, port);
            Session["time"] = time;
            Session["seconds"] = seconds;
            connect.fileName = files;
            return View();
        }


    }
}