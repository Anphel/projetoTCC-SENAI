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
    public class Dados
    {
       
        
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        SqlCommand comm = new SqlCommand();

        public void Cadastro(string id_dono, string nome_dono, string sobrenome_dono, string email_dono, string senha_dono, string endereco_dono, string endereco_lat, string endereco_lng)
        {
           
            
            comm.Connection = conn; //Inserindo cadastro de usuário em banco de dados, email_dono e senha_dono serão utilizados para efetuar login;

            comm.CommandText = "INSERT INTO Dono(id_dono, nome_dono, sobrenome_dono, email_dono, senha_dono, endereco_dono, endereco_lat, endereco_long)  VALUES(@id_dono, @nome_dono, @sobrenome_dono, @email_dono, @senha_dono, @endereco_dono, @endereco_lat, @endereco_long)";

            comm.Parameters.AddWithValue("@id_dono", id_dono);
            comm.Parameters.AddWithValue("@nome_dono", nome_dono);
            comm.Parameters.AddWithValue("@sobrenome_dono", sobrenome_dono);
            comm.Parameters.AddWithValue("@email_dono", email_dono);
            comm.Parameters.AddWithValue("@senha_dono", senha_dono);
            comm.Parameters.AddWithValue("@endereco_dono", endereco_dono);
            comm.Parameters.AddWithValue("@endereco_lat", endereco_lat);
            comm.Parameters.AddWithValue("@endereco_long", endereco_lng);
            
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();       //Fecha conexão;

            //Script alerta quando cadastro for efetuado com sucesso;



            //string msg = "Cadastro Efetuado";
            //this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Alerta", "alert('" + msg + "')", true);     
        }

        
        public void AfterCadastro(Control controle)  //Função que limpará as textbox após cadastro;
        {
            foreach (Control control in controle.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
                else if (control.Controls.Count > 0)
                {
                    AfterCadastro(control);
                }
            }
        }


    }
}