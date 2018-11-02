using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Correios.Net;
using System.Configuration;
using System.Net;
using System.IO;

namespace Pets
{
    public partial class Cadastro : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        SqlCommand comm = new SqlCommand();


        protected void Page_Load(object sender, EventArgs e)
        {
            txtNome.Attributes.Add("placeholder", "Primeiro Nome...");
            txtSobrenome.Attributes.Add("placeholder", "Seu Sobrenome...");
            txtEmail.Attributes.Add("placeholder", "Seu e-mail para login...");
            txtSenha.Attributes.Add("placeholder", "Crie uma senha para login...");
            txtCep.Attributes.Add("placeholder", "Seu CEP...");
            txtNumero.Attributes.Add("placeholder", "Número de seu endereço");
            
        }

        protected void txtCep_TextChanged(object sender, EventArgs e) //Evento da TextBox de CEP, a qual é necessário OnTextChanged e AutoPostBack="true"
        //para que ao dar "TAB" para a TextBox de "Número", ele já puxa os dados  
        {
            Address endereco = SearchZip.GetAddress(txtCep.Text);

            txtRua.Text = endereco.Street;
            txtBairro.Text = endereco.District;
            txtCidade.Text = endereco.City;
            txtEstado.Text = endereco.State;

            #region LatLong

            //double latitude = 0, longitude = 0;

            //string url = "//maps.googleapis.com/maps/api/geocode/json?address=Winnetka&key=AIzaSyCpEcOPBtVymaAsIGJa1o2V1Qo301B5NBw";
            //url += HttpUtility.UrlEncode(txtRua.Text + " " + txtNumero.Text + ", " + txtCidade.Text + " - " + txtEstado.Text);
            //url += "&output=csv";

            //HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url); wr.Timeout = 50000;//5 segundos
            //WebResponse resp = wr.GetResponse(); Stream stream = resp.GetResponseStream();

            //using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //{
            //    string content = reader.ReadToEnd();//coloca todo o HTML na variável content 
            //    if (content != null && content != "")
            //    {
            //        string[] coordenadas = content.Split(',');   //separa o html em um array 
            //        if (coordenadas.Length >= 4)   //verifica se existem 4 elementos no array
            //        {
            //            if (!double.TryParse(coordenadas[2].Replace(".", ","), out latitude))
            //                latitude = 0;   //se não for um número coloca a latitude 0
            //            if (!double.TryParse(coordenadas[3].Replace(".", ","), out longitude))
            //                longitude = 0;   //se não for um número coloca a longitude 0
            //        }
            //    }

            //}

            #endregion

            RetomaTxt();
        }

        public void RetomaTxt() //Retoma a TextBox de Número
        {
            txtNumero.Focus();
        }


        #region AddFoto
        //protected void btnInserirImg_Click(object sender, EventArgs e) //Inserir imagem de perfil
        //{
        //    try
        //    {
        //        if (fileImg.PostedFile.FileName != "")
        //        {
        //            byte[] imagem;
        //            Stream s = fileImg.PostedFile.InputStream;
        //            BinaryReader br = new BinaryReader(s);
        //            imagem = br.ReadBytes((Int32)s.Length);

        //            SqlConnection con = new SqlConnection("Data Source=labinf01-51;Initial Catalog=Pets;User Id=sa;Password=sa1234;");
        //            SqlCommand comm = new SqlCommand();

        //            comm.Connection = conn;
        //            comm.CommandText = "INSERT INTO cadastro_dono VALUES(@imagem_dono)";
        //            comm.Parameters.AddWithValue("@imagem_dono", fileImg);

        //            conn.Open();
        //            int row = comm.ExecuteNonQuery();

        //            if (row > 0)
        //            {
        //                lblErro.Text = "Imagem inserida porra!";
        //            }
        //        }

        //        else lblErro.Text = "Insira uma imagem caraio!";
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion


        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            
           string senhaC = Criptografar.EncryptData(txtSenha.Text);
           string senhaC2 = Descriptografar.DecryptData(senhaC);
            comm.Connection = conn; //Inserindo cadastro de usuário em banco de dados, email_dono e senha_dono serão utilizados para efetuar login;

            comm.CommandText = "INSERT INTO cadastro_dono(nome_dono, sobrenome_dono, email_dono, senha_dono, cep, rua, bairro, cidade, estado)  VALUES(@nome_dono, @sobrenome_dono, @email_dono, @senha_dono, @cep, @rua, @bairro, @cidade, @estado)";

            comm.Parameters.AddWithValue("@nome_dono", txtNome.Text);
            comm.Parameters.AddWithValue("@sobrenome_dono", txtSobrenome.Text);
            comm.Parameters.AddWithValue("@email_dono", txtEmail.Text);
            comm.Parameters.AddWithValue("@senha_dono", senhaC);
            comm.Parameters.AddWithValue("@cep", txtCep.Text);
            comm.Parameters.AddWithValue("@rua", txtRua.Text);
            comm.Parameters.AddWithValue("@bairro", txtBairro.Text);
            comm.Parameters.AddWithValue("@cidade", txtCidade.Text);
            comm.Parameters.AddWithValue("@estado", txtEstado.Text);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

            AfterCadastro(this);

            string msg = "Cadastro Efetuado";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Alerta", "alert('" + msg + "')", true);

            
            
            Response.Redirect("Login.aspx");

        }

        public void AfterCadastro(Control controle)  //Função para redirecionamento depois do cadastro;
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