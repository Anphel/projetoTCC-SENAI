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
    public partial class LocalizacaoAlt : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        string latitude;
        string longitude;
        List<string> perdidos = new List<string>();

        string[] parametrosEmail; //parametros com dados do pet para inserção em html de email de aviso
        string mensagemEmail;   //mensagem html contida no banco para email de aviso
        int idEncDes; //ID de encontros e desaparecimentos do sistema, numero será sequencial.
        Pets2BDDataContext datacontex = new Pets2BDDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProntoAlt_ServerClick(object sender, EventArgs e)
        {
            PetFuncaoDesaparecidoAlt();

            Session["enderecoAlt"] = txtEndereco.Text;
            Session["latitudeAlt"] = txtLatitude.Value;
            Session["longitudeAlt"] = txtLongitude.Value;
            
            var perfilPetDes = from pd in datacontex.Pets_Desaparecidos
                               where pd.nome_pet == Session["nomepet"].ToString() && pd.dono_pet == Session["Usuario"].ToString()
                               select pd;
            foreach (var pd in perfilPetDes)
            {
                Session["dataHoraDes"] = "Desaparecido no dia " + pd.data_des + " por volta das " + pd.hora_des + "m.";
            }
            
            Response.Redirect("PerfilPet.aspx");
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
          
                latitude = txtLatitude.Value;
                longitude = txtLongitude.Value;
           
            List<string> perdidos = new List<string>();

            int raio = 20;

            string data = DateTime.Now.Date.ToShortDateString();
            string hora = DateTime.Now.ToShortTimeString();

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

            


            
            foreach (var p in perfilpet)
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
                            endereco = txtEndereco.Text,
                            endereco_loca_lat = txtLatitude.Value,
                            endereco_loca_lng = txtLongitude.Value,
                            data_des = data,
                            hora_des = hora
                        };

                        datacontextInsert.SubmitChanges();

                        datacontextInsert.Pets_Desaparecidos.InsertOnSubmit(petDes);

                        

                        datacontextInsert.SubmitChanges();

                        //btnDesaparecido.Text = "Encontrado!";
                        //btnDesaparecido.CssClass = "btn btn-success";
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

                foreach (var p in perfilpet)
                {
                    string[] parametros = { p.pet_id.ToString(), p.nome_pet, p.raca_pet, p.dono_pet };
                    parametrosEmail = parametros;
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
            try
            {
                smtp.Send(msg);
            }

            catch (Exception)
            {
                txtEndereco.Text = "nobody";
            }
            conn.Close();
        }

        public void IDEncDes(int numero)
        {
            Random random = new Random();
            int id = random.Next(1000, 9999);  //ID gerado randomicamente para novo caso de desaparecimento/encontro

            idEncDes = id;
        }             
    }
}