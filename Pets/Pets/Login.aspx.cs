using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Drawing;
namespace Pets
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
       
        string latitudeBD;
        string longitudeBD;
        
        public void Page_Load(object sender, EventArgs e)
        {
           

              if (!IsPostBack)            
              {               
                txtEmailLogin.Attributes.Add("placeholder", "Seu e-mail");
                txtSenhaLogin.Attributes.Add("placeholder", "Sua senha");
                txtEndereco.Attributes.Add("placeholder", "Insira seu endereço");
                txtNome.Attributes.Add("placeholder", "Nome...");
                txtSobrenome.Attributes.Add("placeholder", "Sobrenome...");
                txtEmail.Attributes.Add("placeholder", "Seu e-mail para login");
                txtSenha.Attributes.Add("placeholder", "Senha para login");
                
                //txtCep.Attributes.Add("placeholder", "Seu CEP...");
                //txtNumero.Attributes.Add("placeholder", "Número de seu endereço");          
              }

              else
              {
                  latitudeBD = txtLatitude.Value;
                  longitudeBD = txtLongitude.Value;
              }
                          
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                SqlCommand com = new SqlCommand("SELECT senha_dono FROM Dono WHERE email_dono = '" + txtEmailLogin.Value + "'", conn);
                conn.Open();
                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    string senha = dr[0].ToString();
                    
                  conn.Close();
                  senha =  Descriptografar.DecryptData(senha);
                  
                    if (senha == txtSenhaLogin.Text)
                    {
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Dono WHERE email_dono = '" + txtEmailLogin.Value + "'", conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows.Count.ToString() == "1")
                        {
                            Session["Usuario"] = txtEmailLogin.Value;
                            Response.Redirect("Profile.aspx");
                        }

                    }
                }

                else
                {
                    lblStatus.Text = "Confira seus dados e tente novamente, por favor.";
                    txtEmailLogin.Value = null;
                }

            }

            catch (Exception)
            {
                lblStatus.Text = "Falha na conexão";
            }
        }  
                      
  
        #region Preenchimento c/ CEP
        protected void txtCep_TextChanged(object sender, EventArgs e) //Evento da TextBox de CEP, a qual é necessário OnTextChanged e AutoPostBack="true"
        //para que ao dar "TAB" para a TextBox de "Número", ele já puxa os dados  
        {
            if (IsPostBack)
            {
                //Address endereco = SearchZip.GetAddress(txtCep.Text);

                //string pais = "Brasil";
               

                //txtRua.Text = endereco.Street;
                //txtBairro.Text = endereco.District;
                //txtCidade.Text = endereco.City;
                //txtEstado.Text = endereco.State;
                //txtPais.Text = pais;

                //txtLat.Text = lblLat.Text;
                //txtLong.Text = lblLong.Text;
             
                RetomaTxt();               

            }
        }

        #endregion   //Caiu em desuso devido complexidade de retorno de lat e long e dados não específicos.

        #region RetomarTextBox
        public void RetomaTxt() //Retoma a TextBox de Número
        {
            //txtNumero.Focus();
        }
        #endregion

      
       
      
        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            long id = random.Next(10001, 999999);  //ID gerado randomicamente para novo dono/usuario
            
            txtID.Value = id.ToString();    //gerado em long e depois convertido para string para inserção em BD

            try
            {
                string emailCad = string.Empty; //variavel responsavel por armazenar retorno de email existente ou não em banco de dados;
                
                SqlCommand com = new SqlCommand("SELECT 1 FROM Dono WHERE email_dono = '" + txtEmail.Text + "'", conn); //retornará 1 caso email seja existente no banco;
                conn.Open();
                SqlDataReader drRedef = com.ExecuteReader();
                if (drRedef.Read())
                {
                    emailCad = drRedef[0].ToString();
                }

                conn.Close();
                
                Dados d = new Dados();

                if (emailCad != "1")
                {
                                      
                    d.Cadastro(txtID.Value, txtNome.Text, txtSobrenome.Text, txtEmail.Text, Criptografar.EncryptData(txtSenha.Text), txtEndereco.Text, txtLatitude.Value, txtLongitude.Value);    //criação de objeto para criação

                    string msg = "Cadastro Efetuado";
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "Alerta", "alert('" + msg + "')", true);  //mensagem de cadastro efetuado;
                  
                    d.AfterCadastro(this);  //função para limpar textbox depois de cadastro efetuado

                  
                }

                else if(emailCad == "1")        //se o e-mail já estiver cadastrado retornará um e exibirá borda vermelha e exibirá mensagem
                {
                    txtEmail.BorderColor = Color.DarkRed;
                    lblAlertaEmail.InnerText = "Este e-mail já está cadastrado";

                   
                }

                if (emailCad != "1")
                {
                    txtEmail.BorderColor = Color.LightGray;     //para o caso de correção de email do usuário ele verificará novamente se e-mail existe e revertará a formatação  
                    lblAlertaEmail.InnerText = string.Empty;                 
                }
                //de novo dono e inserção no banco
                
            }

            catch (Exception)
            { 
                
            }
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

      
       
        protected void btnEmailRecSenha_Click(object sender, EventArgs e)
        {
           
                string email = "";
              
                SqlCommand comRedef = new SqlCommand("SELECT 1 FROM Dono WHERE email_dono = '" + txtEmailRecSenha.Text + "'", conn);
                conn.Open();
                SqlDataReader drRedef = comRedef.ExecuteReader();
                if (drRedef.Read())
                { 
                    email = drRedef[0].ToString(); 
                }
               
            conn.Close();

                if (email == "1")
                {
                    erroEmail.Text = "Email enviado!";
                    try
                    {
                        SqlCommand com = new SqlCommand("SELECT senha_dono FROM Dono WHERE email_dono = '" + txtEmailRecSenha.Text + "'", conn);
                        conn.Open();
                        SqlDataReader dr = com.ExecuteReader();

                        if (dr.Read())
                        {
                            string senha = dr[0].ToString();
                            conn.Close();
                            Regex validaEMail = new
                            Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
                            Descriptografar.DecryptData(senha);
                            MailMessage msg = new MailMessage();
                            
                            msg.Body = "Sua senha é " + senha + ".";
                            msg.Subject = "Recuperação de senha Pets";
                            msg.Priority = MailPriority.Normal;

                            msg.From = new MailAddress("fullbuster.andre@gmail.com");
                            msg.To.Add(new MailAddress(txtEmailRecSenha.Text));

                            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                            smtp.EnableSsl = true;
                            NetworkCredential credencial = new NetworkCredential("fullbuster.andre@gmail.com", "QWEasd123!@");
                            smtp.Credentials = credencial;
                            
                            smtp.Send(msg);

                        }

                    }
                    catch (Exception ex)
                    { }
                }
                else
                {
                    erroEmail.Text = "Email não existente!";
                }
            

            
        }

        //protected void chbMostrarSenha_CheckedChanged(object sender, EventArgs e)
        //{
        //    if(chbMostrarSenha.Checked == true)
        //    {
        //        txtSenha.AutoPostBack = true;
        //        txtSenha.TextMode = TextBoxMode.Email;                
        //    }

        //    else if(chbMostrarSenha.Checked == false)
        //    {              
        //        txtSenha.TextMode = TextBoxMode.Password;
        //        txtSenha.AutoPostBack = false;
        //    }
        //}
    }
}