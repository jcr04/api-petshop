using Newtonsoft.Json.Linq;
using System.IO;
using MySqlConnector;
using System;
using System.Collections.Generic;
namespace Looping.models
{
    public class animais
    {
        public int CodigoInterno { get; set; }
        public string Nomeanimal { get; set; }
        public string tipo { get; set; }
        public string raca { get; set; }
        public List<animal> animal { get { return Conta.BuscaPorAnimais(this.CodigoInterno); } }
        public static List<Animal> Lista(string sqlWhere = "")
        {
            var clientes = new List<animal>();

            JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json")));
            string sqlCnn = jAppSettings["ConexaoMySql"].ToString();
            using (var connection = new MySqlConnection(sqlCnn))

            {
                connection.Open();

                using (var command = new MySqlCommand($"SELECT * FROM tbcliente {sqlWhere}", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Animal()
                            {
                                CodigoInterno = Convert.ToInt32(reader["codcliente"]),
                                Nome = reader["nomeanimal"].ToString(),
                                CPF = reader["tipo"].ToString(),
                                Email = reader["raca"].ToString()
                            });
                        }
                    }
                }
            }
        
            return Animal;
        }

    public static Animal BuscaPorId(int id)
    {
        var animais = Animal.Lista("where codanimal =" + id);
        return animal.Count > 0 ? animal[0] : null;
    }

}
}
