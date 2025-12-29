/**
 * lufer
 * 
 * 1ª ConnectionString
 * 2ª Connetar
 * 3º Preparar query
 * 4º Executar query
 * 5º Tratar resultado
 * 
 * https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/retrieving-and-modifying-data
 */

//OleDB
using System.Configuration;
using System.Data;
using System.Data.OleDb;
//using System.Data.SqlClient;
using System.Security.Permissions;
 
public class Service1 : IService1
    {

    [PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]
    public void Login()
    {

    }

    /// <summary>
    /// Devolve todos os hoteis de uma determinada cidade
    /// </summary>
    /// <param name="cidade"></param>
    /// <returns></returns>
    public DataSet GetHotelCidade(string cidade)
        {
            // *1ª ConnectionString
            // *2ª Connetar
            // *3º Preparar query
            //* 4º Executar query
            //* 5º Tratar resultado

            DataSet ds = new DataSet();

            //1 Connection
            string conString = ConfigurationManager.ConnectionStrings["turismoConnectionString"].ConnectionString;
            //string cons = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\temp\turismo.mdb";
            OleDbConnection con = new OleDbConnection(conString);
            //con.ConnectionString = "";


            //2 Query
            string query = "select * from hoteis where cidade=@cidade";

            //3 Adapter
            OleDbDataAdapter da = new OleDbDataAdapter(query, con);
            da.SelectCommand.Parameters.Add("@cidade", OleDbType.VarChar);
            da.SelectCommand.Parameters["@cidade"].Value = cidade;

            //da.SelectCommand.Parameters.AddWithValue("@cidade", cidade);

            //4 Execute

            da.Fill(ds, "Q1");

            return ds;
        }

        #region GetAll
        public DataSet GetAllHoteisComCapacidade(int capacidade)
        {
            DataSet ds = new DataSet();

            //1º ConnectionString
            string cs = ConfigurationManager.ConnectionStrings["turismoConnectionString"].ConnectionString;

            //2º OpenConnection
            OleDbConnection con = new OleDbConnection(cs);

            //3º Query Parametrizada
            string q = "SELECT nome, (capacidade-ocupacao) as livres, cidade FROM Hoteis WHERE (capacidade-ocupacao)>@capacidade; ";    //aplicar critério            

            //4º Prepara DataAdapter
            OleDbDataAdapter da = new OleDbDataAdapter(q, con);

            //Instancia parâmetros
            da.SelectCommand.Parameters.Add("@capacidade", OleDbType.Integer);
            da.SelectCommand.Parameters["@capacidade"].Value = capacidade;

            //Executa e preenche o DataSet com os resultados
            da.Fill(ds, "Hoteis2");

            return (ds);
        }

        #endregion

        #region Insert
        public bool InsertHotel(Hotel h)
        {
            OleDbConnection con;
            OleDbCommand myCommand;

            //1º ConnectionString
            string cs = ConfigurationManager.ConnectionStrings["turismoConnectionString"].ConnectionString;

            //2º OpenConnection
            con = new OleDbConnection(cs);

            //3º Query Parametrizada
            string q = "INSERT INTO Hoteis(Nome,Cidade,Capacidade) VALUES(@nome,@cidade,@capacidade);";
            myCommand = new OleDbCommand(q, con);

            //Instancia parâmetros
            myCommand.Parameters.Add("@nome", OleDbType.VarChar).Value = h.nome;
            myCommand.Parameters.Add("@cidade", OleDbType.VarChar).Value = h.cidade;
            myCommand.Parameters.Add("@capacidade", OleDbType.Integer).Value = h.capacidade;

            //4º Prepara DataAdapter       
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.InsertCommand = myCommand;

            try
            {
                con.Open();
                int count;
                //Executa e preenche o DataSet com os resultados
                count = da.InsertCommand.ExecuteNonQuery();
                return true;
            }
            catch { }

            return (false);
        }

        #endregion
}