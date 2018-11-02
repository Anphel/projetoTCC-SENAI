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
using System.Data.Linq;


namespace Pets
{
    public partial class PerfilPet : System.Web.UI.Page
    {

        
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        
        string latitude;
        string longitude;
        List<string> perdidos = new List<string>();

        string[] parametrosEmail; //parametros com dados do pet para inserção em html de email de aviso
        string mensagemEmail;   //mensagem html contida no banco para email de aviso
        int idEncDes; //ID de encontros e desaparecimentos do sistema, numero será sequencial.


        public string filtrasessioncar()
        {
            Pets2BDDataContext datacontex = new Pets2BDDataContext();
            string vetor = "";

            var ss = (from p in datacontex.Pets
                      where p.nome_pet == Session["nomepet"].ToString()
                      select p);


            foreach (var p in ss)
            {
                vetor = p.dono_pet;
            }

            return vetor;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string aux = Session["Usuario"].ToString();

                if (Session["Usuario"].ToString() != filtrasessioncar())
                {
                    Pets2BDDataContext datacontext = new Pets2BDDataContext();

                    var perfilPetDes = from pd in datacontext.Pets_Desaparecidos
                                       where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                                       select pd;

                    foreach (var pd in perfilPetDes)
                    {
                        lblDataHora.Text = "Desaparecido no dia " + pd.data_des + " por volta das " + pd.hora_des + "m.";
                    }

                    Session["Usuario"] = filtrasessioncar();
                    txtNomePetPerfil.Text = Session["nomepet"].ToString();
                    Session["dataHoraDes"] = lblDataHora.Text;
                    Session["botaoDesaparecido"] = btnDesaparecido.Text;
                    Session["botaoEncontrado"] = btnEncontrado.Text;
                    btnDesaparecido.Visible = false;
                    btnSalvarPetPerfil.Visible = false;
                    btnEditarPetPerfil.Visible = false;
                    dpdOpPetPerfil.Visible = false;
                    FileUp.Visible = false;
                    lblDesaparecido.Visible = true;

                    carregaDadospet();

                    Session["Usuario"] = aux;
                }
                else
                {

                    txtNomePetPerfil.Text = Session["nomepet"].ToString();
                    Session["dataHoraDes"] = lblDataHora.Text;
                    Session["botaoDesaparecido"] = btnDesaparecido.Text;
                    Session["botaoEncontrado"] = btnEncontrado.Text;

                    carregaDadospet();
                }
            }

            
                Pets2BDDataContext datacontex = new Pets2BDDataContext();

                var perfilpet = from p in datacontex.Pets
                                where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                                select p;

                var perfilpetDes = from pd in datacontex.Pets_Desaparecidos
                                where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                                select pd;

                foreach (var p in perfilpet)
                {
                    foreach (var pd in perfilpetDes)
                    {
                        if (p.status_pet == 0)
                        {
                            btnDesaparecido.Enabled = false;
                            btnDesaparecido.Visible = false;
                            btnEncontrado.Enabled = true;
                            btnEncontrado.Visible = true;

                            lblDataHora.Text = "Desaparecido no dia " + pd.data_des + " por volta das " + pd.hora_des + "m.";
                        }

                        else if (p.status_pet == 1)
                        {
                            btnEncontrado.Enabled = false;
                            btnEncontrado.Visible = false;
                            btnDesaparecido.Enabled = true;
                            btnDesaparecido.Visible = true;
                        }
                    }
                }
            
        }

        void carregaDadospet()
        {
            Pets2BDDataContext datacontex = new Pets2BDDataContext();
            #region CarregarFoto
            byte[] blob = null;
                var fotoPet = from p in datacontex.Pets
                                 where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                                 select p.foto_pet;
                foreach (var d in fotoPet)
                {
                    if (d != null)
                    {
                        blob = d.ToArray();
                        byte[] imagem = (byte[])(blob);
                        string base64String = Convert.ToBase64String(imagem);
                        imgPerfilPet.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);
                    }
                }
            #endregion
                var perfilpet = from p in datacontex.Pets
                            where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                            select p;

            var perfilpetRaca = from pr in datacontex.Racas_Pets
                                //where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                                select pr;

            foreach (var p in perfilpet)
            {
                lblNomePetPerfil.InnerText = p.nome_pet;
                  
                lblPetIDPerfil.InnerText = "PetID: " + p.pet_id.ToString();
                 
                if (p.especie_pet == "Cachorro")
                    {
                        dpdEspeciePetPerfil.SelectedValue = "Cachorro";
                    }

                    else if (p.especie_pet == "Gato")
                    {
                        dpdEspeciePetPerfil.SelectedValue = "Gato";
                    }

                    txtRacaPetPerfil.Text = p.raca_pet;

                    if (p.sexo_pet == "Macho")
                    {
                        rdSexoPetPerfil.SelectedValue = "Macho";
                    }

                    else if (p.sexo_pet == "Femea")
                    {
                        rdSexoPetPerfil.SelectedValue = "Femea";
                    }
                    txtCorPetPerfil.Text = p.cor_pet;              
            }
        }

        protected void Editar_Click(object sender, EventArgs e)
        {
            txtNomePetPerfil.Enabled = true;
            dpdEspeciePetPerfil.Enabled = true;
            txtRacaPetPerfil.Enabled = true;
            rdSexoPetPerfil.Enabled = true;
            txtCorPetPerfil.Enabled = true;
            btnSalvarPetPerfil.Enabled = true;

        }

        protected void btnSalvarPetPerfil_Click(object sender, EventArgs e)
        {
            Pets2BDDataContext bd = new Pets2BDDataContext();

            Pets2BDDataContext datacontex = new Pets2BDDataContext();


            var perfilpet = from p in datacontex.Pets
                            where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                            select p;

            foreach (var d in perfilpet)
            {
                string filePath = FileUp.PostedFile.FileName;
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

                if (FileUp.HasFile & contenttype != "image/gif")
                {
                    Byte[] imgByte = null;
                    HttpPostedFile File = FileUp.PostedFile;
                    imgByte = new Byte[File.ContentLength];
                    File.InputStream.Read(imgByte, 0, File.ContentLength);
                    d.foto_pet = imgByte;

                }


                

                foreach (var p in perfilpet)
                {
                    p.nome_pet = txtNomePetPerfil.Text.ToString();
                    p.especie_pet = dpdEspeciePetPerfil.SelectedValue.ToString();
                    p.raca_pet = txtRacaPetPerfil.Text.ToString();
                    p.sexo_pet = rdSexoPetPerfil.SelectedValue.ToString();
                    p.cor_pet = txtCorPetPerfil.Text.ToString();

                    var perfilPetDes = from pd in datacontex.Pets_Desaparecidos
                                       where pd.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                                       select pd;

                    foreach (var pd in perfilPetDes)
                    {
                        if (pd.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString())
                        {
                            pd.nome_pet = txtNomePetPerfil.Text.ToString();
                            pd.especie_pet = dpdEspeciePetPerfil.SelectedValue.ToString();
                            pd.raca_pet = txtRacaPetPerfil.Text.ToString();
                            pd.sexo_pet = rdSexoPetPerfil.SelectedValue.ToString();
                            pd.cor_pet = txtCorPetPerfil.Text.ToString();
                        }
                    }


                }

                try
                {
                    datacontex.SubmitChanges();
                }
                catch (Exception) { }

                carregaDadospet();
                Response.Redirect("Profile.aspx");
            }
        }

        protected void btnDesaparecido_Click(object sender, EventArgs e)
        {
            PetFuncaoDesaparecido();
            btnDesaparecido.Enabled = false;
            btnDesaparecido.Visible = false;          
        }

        protected void btnEncontrado_Click(object sender, EventArgs e)
        {
            PetFuncaoEncontrado();
            btnEncontrado.Enabled = false;
            btnEncontrado.Visible = false;
        }

        protected void btnOutraLocalizacao_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("LocalizacaoAlt.aspx");
        }


        public void PetFuncaoDesaparecido()
        {
           
           
            PetDesaparecido();

            Pets2BDDataContext datacontex = new Pets2BDDataContext();


            var perfilpet = from p in datacontex.Pets
                            where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                            select p;

            var perfilpetDes = from pd in datacontex.Pets_Desaparecidos
                            where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                            select pd;

            

            foreach (var p in perfilpet)
            {
                p.status_pet = 0;

            }

            var donoLatLong = from d in datacontex.Donos
                              where d.email_dono == Session["Usuario"].ToString()
                              select d;

            foreach (var d in donoLatLong)
            {
                latitude = d.endereco_lat;
                longitude = d.endereco_long;

            }

            List<string> perdidos = new List<string>();

            int raio = 5;

            string data = DateTime.Now.Date.ToShortDateString();
            string hora = DateTime.Now.ToShortTimeString();

            
            do
            {              
                foreach (var pd in perfilpetDes)
                {
                   
                }
            }

            while (raio == 20);
            
            SqlCommand com = new SqlCommand("SELECT email_dono, ACOS (SIN (RADIANS (" + latitude + ")) * SIN (RADIANS (endereco_lat)) + COS (RADIANS (" + latitude + ")) * COS (RADIANS (endereco_lat)) * COS (RADIANS (" + longitude + ") - RADIANS (endereco_long))) * 6380 AS distance FROM Dono WHERE ACOS (SIN (RADIANS (" + latitude + ")) * SIN (RADIANS (endereco_lat)) + COS (RADIANS (" + latitude + ")) * COS (RADIANS (endereco_lat)) * COS (RADIANS (" + longitude + ") - RADIANS (endereco_long))) * 6380 < "+ raio + "", conn);

            conn.Open();
            
            using (SqlDataReader proximos = com.ExecuteReader())
            {

                if (proximos.HasRows)
                {
                    while (proximos.Read())
                    {
                       
                        string dono = proximos.GetString(proximos.GetOrdinal("email_dono"));
                        perdidos.Add(dono);


                    }
                }

            }
            
            datacontex.SubmitChanges();
            EnviaEmail(perdidos);

        }

        public void PetDesaparecido()
        {

            Pets2BDDataContext datacontex = new Pets2BDDataContext();


            var perfilpet = from p in datacontex.Pets
                            where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                            select p;

            var perfilPetDes = from pd in datacontex.Pets_Desaparecidos
                               where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                               select pd;


            var perfilDono = from d in datacontex.Donos
                             where d.email_dono == Session["Usuario"].ToString()
                             select d;

            foreach (var p in perfilpet)
            {
                foreach (var d in perfilDono)
                {
                    using (Pets2BDDataContext datacontextInsert = new Pets2BDDataContext())
                    {
                        string data = DateTime.Now.Date.ToShortDateString();
                        string hora = DateTime.Now.ToShortTimeString();

                        IDEncDes(idEncDes);

                        Pets_Desaparecido petDes = new Pets_Desaparecido
                        {
                            id_desaparecimento = idEncDes,
                            pet_id = p.pet_id,
                            nome_pet = p.nome_pet,
                            especie_pet = p.especie_pet,
                            raca_pet = p.raca_pet,
                            sexo_pet = p.sexo_pet,
                            cor_pet = p.cor_pet,
                            dono_pet = p.dono_pet,
                            endereco = p.endereco_dono,
                            endereco_loca_lat = d.endereco_lat,
                            endereco_loca_lng = d.endereco_long,
                            data_des = data,
                            hora_des = hora
                        };

                        datacontextInsert.SubmitChanges();


                        datacontextInsert.Pets_Desaparecidos.InsertOnSubmit(petDes);

                        lblDataHora.Text = "Desaparecido no dia " + data + " por volta das " + hora + "m.";

                        datacontextInsert.SubmitChanges();

                    }

                    //btnDesaparecido.Text = "Encontrado!";
                    //btnDesaparecido.CssClass = "btn btn-success";


                    btnDesaparecido.Visible = false;
                    btnDesaparecido.Enabled = false;
                    btnEncontrado.Visible = true;
                    btnEncontrado.Enabled = true;
                }


            }


        }
        
        public void PetFuncaoEncontrado()
        {
           
                Pets2BDDataContext datacontext = new Pets2BDDataContext();
                {
                    var perfilpet = from p in datacontext.Pets
                                    where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                                    select p;

                    foreach (var p in perfilpet)
                    {
                        p.status_pet = 1;

                    }

                    var petEncontrado = from p in datacontext.Pets_Desaparecidos
                                        where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                                        select p;


                    foreach (var p in petEncontrado)
                    {
                        
                        string data = DateTime.Now.Date.ToShortDateString();
                        string hora = DateTime.Now.ToShortTimeString();

                        IDEncDes(idEncDes);
                                             
                        Pets_Encontrado perfilPetEnc = new Pets_Encontrado
                        {
                            
                            id_encontro = idEncDes,
                            pet_id = p.pet_id,
                            nome_pet = p.nome_pet,
                            dono_pet = p.dono_pet,
                            especie_pet = p.especie_pet,
                            raca_pet = p.raca_pet,
                            sexo_pet = p.sexo_pet,
                            data_enc = data,
                            hora_enc = hora,
                            data_des = p.data_des,
                            hora_des = p.hora_des
                        };
                        
                        datacontext.Pets_Encontrados.InsertOnSubmit(perfilPetEnc);
                   
                    }

                    foreach (var p in petEncontrado)
                    {
                        datacontext.Pets_Desaparecidos.DeleteOnSubmit(p);
                        btnEncontrado.Visible = false;
                        btnEncontrado.Enabled = false;
                        btnDesaparecido.Visible = true;
                        btnDesaparecido.Enabled = true;
                        
                       
                        

                        lblDataHora.Text = string.Empty;
                    }               

                }
                
            datacontext.SubmitChanges();       
        }

        public void PetFuncaoDesaparecidoAlt() //função para endereço de desaparecimento alternativo
        {
            PetDesaparecidoAlt();

            Pets2BDDataContext datacontex = new Pets2BDDataContext();


            var perfilpet = from p in datacontex.Pets
                            where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                            select p;

            var perfilpetDes = from pd in datacontex.Pets_Desaparecidos
                               where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                               select pd;



            foreach (var p in perfilpet)
            {
                p.status_pet = 0;

            }

            var donoLatLong = from d in datacontex.Donos
                              where d.email_dono == Session["Usuario"].ToString()
                              select d;

            foreach (var d in donoLatLong)
            {
                latitude = d.endereco_lat;
                longitude = d.endereco_long;

            }

            List<string> perdidos = new List<string>();

            int raio = 5;

            string data = DateTime.Now.Date.ToShortDateString();
            string hora = DateTime.Now.ToShortTimeString();


            do
            {
                foreach (var pd in perfilpetDes)
                {

                }
            }

            while (raio == 20);

            SqlCommand com = new SqlCommand("SELECT email_dono, ACOS (SIN (RADIANS (" + latitude + ")) * SIN (RADIANS (endereco_lat)) + COS (RADIANS (" + latitude + ")) * COS (RADIANS (endereco_lat)) * COS (RADIANS (" + longitude + ") - RADIANS (endereco_long))) * 6380 AS distance FROM Dono WHERE ACOS (SIN (RADIANS (" + latitude + ")) * SIN (RADIANS (endereco_lat)) + COS (RADIANS (" + latitude + ")) * COS (RADIANS (endereco_lat)) * COS (RADIANS (" + longitude + ") - RADIANS (endereco_long))) * 6380 < " + raio + "", conn);

            conn.Open();

            using (SqlDataReader proximos = com.ExecuteReader())
            {

                if (proximos.HasRows)
                {
                    while (proximos.Read())
                    {

                        string dono = proximos.GetString(proximos.GetOrdinal("email_dono"));
                        perdidos.Add(dono);


                    }
                }

            }

            datacontex.SubmitChanges();
            EnviaEmail(perdidos);

        }

        public void PetDesaparecidoAlt()
        {

            Pets2BDDataContext datacontex = new Pets2BDDataContext();


            var perfilpet = from p in datacontex.Pets
                            where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                            select p;

            var perfilPetDes = from pd in datacontex.Pets_Desaparecidos
                               where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                               select pd;


            var perfilDono = from d in datacontex.Donos
                             where d.email_dono == Session["Usuario"].ToString()
                             select d;

            foreach (var p in perfilpet)
            {
                foreach (var d in perfilDono)
                {
                    using (Pets2BDDataContext datacontextInsert = new Pets2BDDataContext())
                    {
                        string data = DateTime.Now.Date.ToShortDateString();
                        string hora = DateTime.Now.ToShortTimeString();

                        IDEncDes(idEncDes);

                        Pets_Desaparecido petDes = new Pets_Desaparecido
                        {
                            id_desaparecimento = idEncDes,
                            pet_id = p.pet_id,
                            nome_pet = p.nome_pet,
                            especie_pet = p.especie_pet,
                            raca_pet = p.raca_pet,
                            sexo_pet = p.sexo_pet,
                            cor_pet = p.cor_pet,
                            dono_pet = p.dono_pet,
                            endereco = Session["enderecoAlt"].ToString(),
                            endereco_loca_lat = Session["latitudeAlt"].ToString(),
                            endereco_loca_lng = Session["longitudeAlt"].ToString(),
                            data_des = data,
                            hora_des = hora
                        };

                        datacontextInsert.SubmitChanges();


                        datacontextInsert.Pets_Desaparecidos.InsertOnSubmit(petDes);

                        lblDataHora.Text = "Desaparecido no dia " + data + " por volta das " + hora + "m.";

                        datacontextInsert.SubmitChanges();

                    }

                    //btnDesaparecido.Text = "Encontrado!";
                    //btnDesaparecido.CssClass = "btn btn-success";


                    btnDesaparecido.Visible = false;
                    btnDesaparecido.Enabled = false;
                    btnEncontrado.Visible = true;
                    btnEncontrado.Enabled = true;
                }


            }

        }

        public void EnviaEmail(List<string> emailsLista)
        {
            Pets2BDDataContext datacontext = new Pets2BDDataContext();
            {
                var perfilpet = from p in datacontext.Pets
                                where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                                select p;
                
                var perfilDono = from d in datacontext.Donos
                                where d.email_dono == Session["Usuario"].ToString()
                                select d;

                foreach (var p in perfilpet)
                {
                    foreach (var d in perfilDono)
                    {
                        string[] parametros = { p.pet_id.ToString(), p.nome_pet, p.raca_pet, p.cor_pet, p.dono_pet, d.telefone_dono, d.facebook_dono };
                        parametrosEmail = parametros;
                    }
                }
            }

            var mensagem = from p in datacontext.Html_Emails
                              where p.html_email_des != string.Empty
                              select p;

            foreach (var p in mensagem)
            {
                var mensagemE = p.html_email_des;
                mensagemEmail = mensagemE;               
            }

            Regex validaEMail = new
            Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.Body = string.Format(mensagemEmail, parametrosEmail);

            msg.Subject = "Pets";
            msg.Priority = MailPriority.Normal;

            msg.From = new MailAddress("fullbuster.andre@gmail.com");
            MailAddressCollection emails = new MailAddressCollection();

            foreach (var e in emailsLista)
            {
                msg.To.Add(new MailAddress(e.ToString()));
            }

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            NetworkCredential credencial = new NetworkCredential("fullbuster.andre@gmail.com", "QWEasd123!@");
            smtp.Credentials = credencial;

            smtp.Send(msg);
            conn.Close();
        }

       

        protected void dpdOpPetPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pets2BDDataContext dataContext = new Pets2BDDataContext();

            var perfilPet = from p in dataContext.Pets
                            where p.nome_pet == Session["nomepet"].ToString() && p.dono_pet == Session["Usuario"].ToString()
                            select p;
            
            var perfilPetDes = from pd in dataContext.Pets_Desaparecidos
                            where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                            select pd;

            var perfilPetEnc = from pe in dataContext.Pets_Encontrados
                               where pe.nome_pet == Session["nomepet"].ToString() && pe.dono_pet == Session["Usuario"].ToString()
                               select pe;

            switch (dpdOpPetPerfil.SelectedValue)
            { 
                case "1":
                  
                    foreach (var p in perfilPet)
                   {
                       dataContext.Pets.DeleteOnSubmit(p);
                       
                        foreach (var pd in perfilPetDes)
                       {
                           dataContext.Pets_Desaparecidos.DeleteOnSubmit(pd);

                           foreach (var pe in perfilPetEnc)
                           {
                               dataContext.Pets_Encontrados.DeleteOnSubmit(pe);
                           }
                           
                       }
                   }
                    dataContext.SubmitChanges();
                    Response.Redirect("Profile.aspx");
                    break;

                case "2":
                    Response.Redirect("CompraMedalha.aspx");
                    
                    break;

                case "3":
                    lblDataHora.Text = "3";
                    break;
            }
         
        }

        public void IDEncDes(int numero)
        {
            Random random = new Random();
            int id = random.Next(1000, 9999);  //ID gerado randomicamente para novo caso de desaparecimento/encontro

            idEncDes = id;    
        }             
    }
}