using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Correios.Net;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Windows;
using System.Text.RegularExpressions;
using System;

namespace Pets
{

    partial class Pet
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        SqlCommand comm = new SqlCommand();

        public void CadastroPet(string pet_id, string nome_pet, string endereco_dono, string especie_pet, string raca_pet, string sexo_pet, string cor_pet, string dono_pet)
        {
            comm.Connection = conn;  //conexão banco de dados, id_pet chave primária

            int sta = 1;

            comm.CommandText = "INSERT INTO Pet (pet_id, nome_pet, endereco_dono, especie_pet, raca_pet, sexo_pet, cor_pet, dono_pet, status_pet) VALUES (@pet_id, @nome_pet, @endereco_dono, @especie_pet, @raca_pet, @sexo_pet, @cor_pet, @dono_pet, @status_pet)";

            comm.Parameters.AddWithValue("@pet_id", pet_id);
            comm.Parameters.AddWithValue("@nome_pet", nome_pet);
            comm.Parameters.AddWithValue("@endereco_dono", endereco_dono);
            comm.Parameters.AddWithValue("@especie_pet", especie_pet);
            comm.Parameters.AddWithValue("@raca_pet", raca_pet);
            comm.Parameters.AddWithValue("@sexo_pet", sexo_pet);
            comm.Parameters.AddWithValue("cor_pet", cor_pet);
            comm.Parameters.AddWithValue("dono_pet", dono_pet);
            comm.Parameters.AddWithValue("status_pet", sta);

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();  
        }

        public void ListarPets()
        { 
            
        }

    }
}