/*
 * RESTful Client implementado em C#
 * 
 * RECEITA: Para aceder a recursos Web:
 * 1 - URI (URL+Parametros) - HttpUtility
 * 2 - HttpWebRequest
 * 3 - HttpWebResponse
 * 
 * Mais:
 *  Parsing XML     - https://msdn.microsoft.com/en-us/library/hh534080.aspx
 *  Parsing JSON    - https://msdn.microsoft.com/en-us/library/hh674188.aspx
 *  
 *  JSON serialization in .NET
 *  --------------------------
 *  System.Runtime.Serialization
 *  System.ServiceModel.Web
 *  com DataContractJsonSerializer(typeof(T))
 *  
 *  or
 *  using System.Text.Json
 *  com: JsonSerializer.Desialize<T>(s)
 *  
 *  Ver 
 *  http://json2csharp.com/
 *  https://json2csharp.com/code-converters/xml-to-csharp
 *
 *  Exemplos REST Services:
 *    
 *  Explorar
 *      http://samples.openweathermap.org/data/2.5/forecast?q=M%C3%BCnchen,DE&appid=b1b15e88fa797225412429c1c50c122a1
 *      http://api.openweathermap.org/data/2.5/weather?q=CITYNAME&mode=MODE&appid=API
 *      APIKEY: b4e56d454f9ddc1a5b66102f99f28fa2
 * by lufer
 * LESI-EST-IPCA
 * */


using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;                            //NameSpace referência
using System.Runtime.Serialization.Json;         //DataContract
//Adicionar COM System.Runtime.Serialization em "Project Properties"
using System.Text;
using System.Web;                           //NameSpace referência - HttpUtility
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RESTWS
{
    class Program
    {
        static void Main(string[] args)
        {
            #region VAR

            //Suportar Pedido e URI
            HttpWebRequest request;
            StringBuilder uri;
            string url;
            string apiKey = "b4e56d454f9ddc1a5b66102f99f28fa2";
            string mode = "json";

            #endregion

            #region WEATHER
            try
            {
                #region Weather
                //Weather URL
                url = "http://api.openweathermap.org/data/2.5/weather?q=[CITY]&mode=[MODE]&appid=[APIKEY]";

                #region ConstroiURI

                uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[CITY]", HttpUtility.UrlEncode("Lisbon"));
                uri.Replace("[APIKEY]", apiKey);
                uri.Replace("[MODE]", mode);

                //String.Format();
                #endregion


                #region PreparaPedido

                //Prepara e envia pedido
                //
                request = WebRequest.Create(uri.ToString()) as HttpWebRequest;

                // Caso necessite de autenticação no pedido 
                //request.Credentials = new NetworkCredential("username", "password"); 

                // Mais flags
                // request.KeepAlive = false;
                // request.Timeout = 10 * 10000;

                #endregion

                #endregion

                #region EnviaPedidoAnalisaResposta

                //analisa resposta
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)     //via GET
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    //Se for JSON

                    //NOTA: "GetResponseStream" só pode ser usado uma vez!!!
                    //request: non-buffered stream that can only be read once

                    //Preserva conteudo num stream de memória
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string str = reader.ReadToEnd();
                    //Preservar stream content;
                    var copyStream = new MemoryStream();
                    var writer = new StreamWriter(copyStream);
                    writer.Write(str);
                    writer.Flush();

                    //ou
                    //var copyStream = new MemoryStream();
                    //response.GetResponseStream().CopyTo(copyStream);
                    //StreamReader s = new StreamReader(copyStream);
                    //string json = s.ReadToEnd();
                    Console.WriteLine("Resposta:" + str);

                    //ou
                    //StreamReader reader = new StreamReader(response.GetResponseStream());
                    //string str = reader.ReadToEnd();
                    //Console.WriteLine(str);

                    //ou também
                    //Console.WriteLine(GetPageAsString(uri.ToString()));

                    if (mode == "json")
                    {
                        //Serializa de JSON para Objecto
                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Root));
                        copyStream.Position = 0L;               //inicio do stream            
                        Root myClass = (Root)jsonSerializer.ReadObject(copyStream);
                        //Ou
                        //Root myClass = (Root)jsonSerializer.ReadObject(response.GetResponseStream());
                        ProcessResponse(myClass);
                    }
                    else //se for XML
                    {
                        // Converte em "ficheiro" a resposta recebida, em XML   
                        //GetXMLData(response.GetResponseStream());
                        GetXMLData(copyStream);
                    }
                }
                #endregion
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);

            }
            #endregion

            Console.ReadKey();

        }



        #region Métodos auxiliares  XML  

        /// <summary>
        /// Método para devolver o conteúdo de uma página em string
        /// </summary>
        /// <param name="address">URI completo</param>
        /// <returns></returns>
        public static string GetPageAsString(String address)
        {
            string result = "";

            // Cria pedido Web 
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            // Analisa a resposta  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Obtem a Resposta 
                StreamReader reader = new StreamReader(response.GetResponseStream());
                // Lê e converte em string
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// Trata XML
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static void GetXMLData(Stream f)
        {
            DataSet ds = new DataSet();
            // converte o Stream num DataSet
            ds.ReadXml(f);
            // Mostra o XML do DataSet
            PrintDataSet(ds);

            //or
            //StreamReader sr = new StreamReader(f);
            //string s = sr.ReadToEnd();
            //XmlSerializer sXml = new XmlSerializer(typeof(Weatherdata));
            //using (StringReader reader = new StringReader(s))
            //{
            //    Weatherdata test = (Weatherdata)sXml.Deserialize(reader);
            //}
        }

        /// <summary>
        /// Prepara output de XML
        /// </summary>
        /// <param name="ds">Dataset</param>
        public static void PrintDataSet(DataSet ds)
        {
            int x = ds.Tables.Count;
            foreach (DataTable table in ds.Tables)
            {
                for (int rn = 0; rn < table.Rows.Count; rn++)
                {
                    for (int cn = 0; cn < table.Columns.Count; cn++)
                    {

                        Console.WriteLine("Row: {0} : Col: {1} <{2}> = {3}", rn, cn, table.Columns[cn].Caption, table.Rows[rn][cn]);
                    }
                }

            }
        }

        #endregion       

        #region Métodos auxiliares_JSON
        static public void ProcessResponse(Root r)
        {
            double tmax = r.main.temp_max;
            Console.WriteLine("Temperatura: " + r.main.temp.ToString());
            Console.WriteLine("Humidade: " + r.main.humidity.ToString());
        }

        #endregion 
    }

    #region CONTRACTS_SERIALIZAÇÂO    

    #region WEATHER_CLASSES from JSON

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Root
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
    
    #endregion

    #region WEATHER_CLASSES from XML

    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(Weatherdata));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (Weatherdata)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "location")]
    public class Location
    {

        [XmlAttribute(AttributeName = "altitude")]
        public int Altitude { get; set; }

        [XmlAttribute(AttributeName = "latitude")]
        public double Latitude { get; set; }

        [XmlAttribute(AttributeName = "longitude")]
        public double Longitude { get; set; }

        [XmlAttribute(AttributeName = "geobase")]
        public string Geobase { get; set; }

        [XmlAttribute(AttributeName = "geobaseid")]
        public int Geobaseid { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "type")]
        public object Type { get; set; }

        [XmlElement(ElementName = "country")]
        public string Country { get; set; }

        [XmlElement(ElementName = "timezone")]
        public object Timezone { get; set; }

        [XmlElement(ElementName = "location")]
        public Location LocationL { get; set; }
    }

    [XmlRoot(ElementName = "meta")]
    public class Meta
    {

        [XmlElement(ElementName = "lastupdate")]
        public object Lastupdate { get; set; }

        [XmlElement(ElementName = "calctime")]
        public double Calctime { get; set; }

        [XmlElement(ElementName = "nextupdate")]
        public object Nextupdate { get; set; }
    }

    [XmlRoot(ElementName = "sun")]
    public class Sun
    {

        [XmlAttribute(AttributeName = "rise")]
        public DateTime Rise { get; set; }

        [XmlAttribute(AttributeName = "set")]
        public DateTime Set { get; set; }
    }

    [XmlRoot(ElementName = "symbol")]
    public class Symbol
    {

        [XmlAttribute(AttributeName = "number")]
        public int Number { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "var")]
        public string Var { get; set; }
    }

    [XmlRoot(ElementName = "precipitation")]
    public class Precipitation
    {

        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public double Value { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlRoot(ElementName = "windDirection")]
    public class WindDirection
    {

        [XmlAttribute(AttributeName = "deg")]
        public double Deg { get; set; }

        [XmlAttribute(AttributeName = "code")]
        public string Code { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "windSpeed")]
    public class WindSpeed
    {

        [XmlAttribute(AttributeName = "mps")]
        public DateTime Mps { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "temperature")]
    public class Temperature
    {

        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public double Value { get; set; }

        [XmlAttribute(AttributeName = "min")]
        public double Min { get; set; }

        [XmlAttribute(AttributeName = "max")]
        public double Max { get; set; }
    }

    [XmlRoot(ElementName = "pressure")]
    public class Pressure
    {

        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public double Value { get; set; }
    }

    [XmlRoot(ElementName = "humidity")]
    public class Humidity
    {

        [XmlAttribute(AttributeName = "value")]
        public int Value { get; set; }

        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }
    }

    [XmlRoot(ElementName = "clouds")]
    public class Clouds2
    {

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }

        [XmlAttribute(AttributeName = "all")]
        public int All { get; set; }

        [XmlAttribute(AttributeName = "unit")]
        public string Unit { get; set; }
    }

    [XmlRoot(ElementName = "time")]
    public class Time
    {

        [XmlElement(ElementName = "symbol")]
        public Symbol Symbol { get; set; }

        [XmlElement(ElementName = "precipitation")]
        public Precipitation Precipitation { get; set; }

        [XmlElement(ElementName = "windDirection")]
        public WindDirection WindDirection { get; set; }

        [XmlElement(ElementName = "windSpeed")]
        public WindSpeed WindSpeed { get; set; }

        [XmlElement(ElementName = "temperature")]
        public Temperature Temperature { get; set; }

        [XmlElement(ElementName = "pressure")]
        public Pressure Pressure { get; set; }

        [XmlElement(ElementName = "humidity")]
        public Humidity Humidity { get; set; }

        [XmlElement(ElementName = "clouds")]
        public Clouds Clouds { get; set; }

        [XmlAttribute(AttributeName = "from")]
        public DateTime From { get; set; }

        [XmlAttribute(AttributeName = "to")]
        public DateTime To { get; set; }
    }

    [XmlRoot(ElementName = "forecast")]
    public class Forecast
    {

        [XmlElement(ElementName = "time")]
        public List<Time> Time { get; set; }
    }

    [XmlRoot(ElementName = "weatherdata")]
    public class Weatherdata
    {

        [XmlElement(ElementName = "location")]
        public Location Location { get; set; }

        [XmlElement(ElementName = "credit")]
        public object Credit { get; set; }

        [XmlElement(ElementName = "meta")]
        public Meta Meta { get; set; }

        [XmlElement(ElementName = "sun")]
        public Sun Sun { get; set; }

        [XmlElement(ElementName = "forecast")]
        public Forecast Forecast { get; set; }
    }



    #endregion

    #endregion


}
