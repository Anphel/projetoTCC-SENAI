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
using System.Drawing.Imaging;


namespace Pets
{
    public partial class EditarPerfil : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        public string senha;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            carregarDados();
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Pets2BDDataContext bd = new Pets2BDDataContext();
            
            var perfilDono = from p in bd.Donos
                             where p.email_dono == Session["Usuario"].ToString()
                             select p;
            
            foreach (var p in perfilDono)
            {
                string filePath = FileUpload1.PostedFile.FileName;
                string filename = Path.GetFileName(filePath);
                string contenttype = String.Empty;
                string ext = Path.GetExtension(filename);
                
                switch (ext)
                {
                    case ".jpg":
                        contenttype = "image/jpg";
                        break;
                    case ".png":
                        contenttype = "image/png";
                        break;
                    case ".gif":
                        contenttype = "image/gif";
                        break;
                }
                
                if (FileUpload1.HasFile & contenttype != "image/gif")
                {
                    Byte[] imgByte = null;
                    HttpPostedFile File = FileUpload1.PostedFile;
                    imgByte = new Byte[File.ContentLength];
                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                    p.imagem_dono = imgByte;
                    lblStatusImg.Visible = true;
                    lblStatusImg.Text = "Imagem salva";
                }
                
                else
                {
                    lblStatusImg.Visible = true;
                    lblStatusImg.Text = "Imagem nao compativel";
                }
                p.email_dono = txtEditEmail.Text;
                p.endereco_dono = txtEndereco.Text;
                p.nome_dono = txtEditNome.Text;
                p.sobrenome_dono = txtEditSobrenome.Text;
                
                if (txtEditSenhaNova.Visible == true)
                {
                    p.senha_dono = Criptografar.EncryptData(txtEditSenhaNova.Text);
                }
                
                bd.SubmitChanges();
            }
            
            Response.Redirect("Profile.aspx");
        }
        
        protected void carregarDados()
        {
            Pets2BDDataContext datacontex = new Pets2BDDataContext();
            byte[] blob = null;
            var perfilDono = from p in datacontex.Donos
                             where p.email_dono == Session["Usuario"].ToString()
                             select p;
           
            foreach (var p in perfilDono)
            {
                lblID.Text = p.id_dono;
                txtEditEmail.Text = p.email_dono;
                txtEndereco.Text = p.endereco_dono;
                txtEditNome.Text = p.nome_dono;
                txtEditSobrenome.Text = p.sobrenome_dono;
                senha = Descriptografar.DecryptData(p.senha_dono);
                
                if (p.imagem_dono != null)
                {
                    blob = p.imagem_dono.ToArray();
                    byte[] imagem = (byte[])(blob);
                    string base64String = Convert.ToBase64String(imagem);
                    imgPerfil.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);
                }
                
            }
        }
        
        protected void btnV_ServerClick(object sender, EventArgs e)
        {
            if (txtEditSenhaAntiga.Text == senha)
            {
                txtEditSenhaNova.Visible = true;
                lblSenha.Visible = true;
            }
        }

    }
}