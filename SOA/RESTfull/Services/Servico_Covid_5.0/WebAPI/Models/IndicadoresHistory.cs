/*
*	<copyright file="IndicadoresHistory.cs" company="IPCA">
*		Copyright (c) 2025 All Rights Reserved
*	</copyright>
* 	<author>lufer</author>
*   <date></date>
*	<description></description>
**/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IIndicadoresHistory
    {
        bool AddIndicador(IIndicadorModel i);
        List<IIndicadorModel> GetAllIndicadores();
        Indicador GetIndicador(string d);
        Task<Indicador> GetIndicadorAsync(string d);
        Task<bool> AddIndicadorSync(IIndicadorModel i);
        bool SaveHistory(string fileName);
        bool LoadHistory(string fileName);

    }

    /// <summary>
    /// Purpose:
    /// Created by: lufer
    /// Created on: 2025 
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>

    [Serializable]
    public class IndicadoresHistory : IIndicadoresHistory
    {
        //Simula uma BD
        private List<IIndicadorModel> hist;

        public IndicadoresHistory()
        {
            if (hist==null)
                hist = new List<IIndicadorModel>();
        }

        /// <summary>
        /// NOTA: evitar enviar os dados originais
        /// </summary>
        /// <returns></returns>
        public List<IIndicadorModel> GetAllIndicadores()
        {
            //ou
            //List<IIndicadorModel> aux = new List<IIndicadorModel>(hist);
            return hist.GetRange(0, hist.Count);    //réplica dos dados originais
            //return hist;
        }

        public Indicador GetIndicador(string d)
        {
            foreach(Indicador i in hist)
            {
                if (i.Date == d) return i;
            }
            return null;
            //tentar com LINQ ???
        }

        public async Task<Indicador> GetIndicadorAsync(string d)
        {
            foreach (Indicador i in hist)
            {
                if (i.Date == d) return i;
            }
            await Task.Delay(3000); //simular espera
            return null;
            //tentar com LINQ ???
        }

        public bool AddIndicador(IIndicadorModel i)
        {          
            if ((hist != null) && (!hist.Contains(i)))
                {
                    // criar construtor
                    Indicador aux = new Indicador();
                    aux = (Indicador)i;
                    //aux.Date= i.Date;
                    //aux.Infetados = i.Infetados;
                    //aux.Mortes = i.Mortes;
                    //aux.Recuperados = i.Recuperados;
                    hist.Add(aux);
                    return true;
                }
            return false;
        }

        public async Task<bool> AddIndicadorSync(IIndicadorModel i)
        {
            //Simular BD
            if ((hist != null) && (!hist.Contains(i)))
            {
                // criar construtor
                Indicador aux = new Indicador();
                //aux = (Indicador)i;
                aux.Date = i.Date;
                aux.Infetados = i.Infetados;
                aux.Mortes = i.Mortes;
                aux.Recuperados = i.Recuperados;
                hist.Add(aux);
                await Task.Delay(3000); //3s
                return true;
            }
            return false;
        }

        /// <summary>
        /// Preservar
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool SaveHistory(string fileName)
        {
            //DataBase Handling

            #region File
            //if (File.Exists(fileName))
            //{
            //                try
            //                {
            //                    Stream stream = File.Open(fileName, FileMode.OpenOrCreate,FileAccess.ReadWrite);
            //                    BinaryFormatter bin = new BinaryFormatter();
            //#pragma warning disable SYSLIB0011 // Type or member is obsolete
            //                    bin.Serialize(stream, hist);
            //#pragma warning restore SYSLIB0011 // Type or member is obsolete
            //                    stream.Close();
            //                    return true;
            //                }
            //                catch (IOException e)
            //                {
            //                    throw new Exception(e.Message+"- Saving Problems");
            //                }
            //}
            #endregion
            return true;
        }


        /// <summary>
        /// Load Data
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool LoadHistory(string fileName)
        {
            //DataBase Handling

            #region File
            //            if (File.Exists(fileName))
            //            {
            //                try
            //                {
            //                    Stream stream = File.Open(fileName, FileMode.Open);
            //                    BinaryFormatter bin = new BinaryFormatter();
            //#pragma warning disable SYSLIB0011 // Type or member is obsolete
            //                    hist = (List<IIndicadorModel>)bin.Deserialize(stream);
            //#pragma warning restore SYSLIB0011 // Type or member is obsolete
            //                    stream.Close();
            //                    return true;
            //                }
            //                catch (IOException e)
            //                {
            //                    throw new Exception(e.Message + " - Loading Problems");
            //                }
            //            }
            //            return false;
            #endregion
            return true;
        }
    }
}
