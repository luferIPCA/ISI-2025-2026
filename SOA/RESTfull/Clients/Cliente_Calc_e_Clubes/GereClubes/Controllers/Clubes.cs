/*
 * lufer
 * ISI
 * Model
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;     //Nugget Install System.Text.Json


namespace GereClubes
{
    /// <summary>
    /// Controla Serviço REST sobre clubes
    /// </summary>
    public class Clubes
    {
        public Clubes() { }

        /// <summary>
        /// Recebe Json
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public static RootObject GetAll()
        {
            string uri = "https://localhost:7146/Clubes/api/All";
            //1ºPrepara e envia pedido
            HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;
            //analisa resposta
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)     //via GET
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }
                    //Preserva conteudo num stream de memória
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string str = reader.ReadToEnd();

                    //Serializa de JSON para Objecto
                    RootObject res = new RootObject();
                    res.all = JsonSerializer.Deserialize<List<Team>>(str);

                    return res;
                }
            }
            catch (WebException ex)
            {
                // Handle errors
                //using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                //{
                //    string errorResponse = reader.ReadToEnd();
                //    Console.WriteLine("Error: " + errorResponse);
                //}
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// https://localhost:7146/Clubes/api/AddClubJson
        /// POST
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool AddClub(Team t)
        {
            string uri = "https://localhost:7146/Clubes/api/AddClubJson";

            //1º Serialize the Team object to JSON
            string json = JsonSerializer.Serialize(t);

            //2º Prepara e envia pedido
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            //3º Coloca o json no Request Body
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
            }
            // 4º Envia pedido e trata resposta 
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    //Console.WriteLine(response.StatusCode.ToString());

                    if (response.StatusCode == HttpStatusCode.OK)
                        return true;
                    else
                        return false;
                    //// Read the response
                    //using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    //{
                    //    string responseText = reader.ReadToEnd();
                    //    Console.WriteLine("Response: " + responseText);
                    //}
                }
            }
            catch (WebException ex)
            {
                // Handle errors
                //using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                //{
                //    string errorResponse = reader.ReadToEnd();
                //    Console.WriteLine("Error: " + errorResponse);
                //}
                throw new Exception (ex.Message);
            }
        }
    }


}
