using System;
using System.Collections.Generic;

#region Supporting Serializarion
    /// <summary>
    /// classe gerada a partir do json devolvido pelo REST
    /// </summary>
    public class RootObject
    {
        public List<Team> all { get; set; }
    }


    public class Team
    {
        public string name { get; set; }
    }

#endregion

