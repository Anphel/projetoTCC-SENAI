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
using System.Collections;
namespace Pets
{
    public partial class Default : System.Web.UI.Page
    {
        public string nome { get; set; }

        string endereco_dono; //string que armazena endereço da session usuario contina no BD

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                byte[] blob = null;
                Pets2BDDataContext datacontex = new Pets2BDDataContext();
                var perfilDono = from p in datacontex.Donos
                                 where p.email_dono == Session["Usuario"].ToString()
                                 select p.imagem_dono;


                foreach (var p in perfilDono)
                {
                    if (p != null)
                    {
                        blob = p.ToArray();
                        byte[] imagem = (byte[])(blob);
                        string base64String = Convert.ToBase64String(imagem);
                        imgPerfil.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);
                    }
                    
                }
                
            
           
                string nome = string.Empty;
                string linkFacebook = string.Empty;

                SqlCommand com = new SqlCommand("SELECT nome_dono + ' '+ sobrenome_dono FROM Dono WHERE email_dono = '" + Session["Usuario"] + "'", conn); // Seleciona o nome e sobrenome da session para exibir 
                conn.Open();                                                                                                                               //no perfil
                SqlDataReader dr = com.ExecuteReader(); //armezena nome completo em dr

                if (dr.Read())
                {
                    nome = dr[0].ToString();    //dr se iguala a variavel nome;
                }

                lblNomePerfil.InnerText = nome;  //label destinada ao nome do perfil se iguala a variavel nome          

                if (Session["Usuario"] != null)
                {

                    //lblUsuario.Text = Session["Usuario"].ToString();
                }

                else
                {
                    Response.Redirect("Login.aspx");
                }

                conn.Close();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void btnCadastrarPet_ServerClick(object sender, EventArgs e)
        {

            Pet pet = new Pet();
            conn.Close();
            EnderecoDono();

            PetID();

            string dono = Session["Usuario"].ToString();
         
            pet.CadastroPet(lblPetID.Text, txtNomePet.Value.ToString(), endereco_dono, dpdEspeciePet.SelectedItem.Text.ToString(), dpdRacaPet.SelectedItem.Text.ToString(), rdSexoPet.SelectedValue.ToString(), txtCorPet.Value.ToString(), dono);

            Response.Redirect("Profile.aspx");
        }


        //funçoes EnderecoDono, PetDono e PetID 

        #region Funcoes


        #region EnderecoDono
        public void EnderecoDono()
        {
            try
            {
                SqlCommand com = new SqlCommand("SELECT endereco_dono FROM Dono WHERE email_dono = '" + Session["Usuario"] + "'", conn);    //seleciona endereço do dono para inserir em tabela Pet
                conn.Open();
                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    endereco_dono = dr[0].ToString();   //armazena endereço na variavel endereco_dono
                }

                conn.Close();
            }

            catch (Exception)
            {

            }
        }
        #endregion

        #region PetDono
        public void PetDono()
        {
            string dadosPet;

            try
            {
                //seleciona dados do pet onde o dono do pet é iagual a session logada

                SqlCommand com = new SqlCommand("SELECT nome_pet FROM Pet WHERE dono_pet = '" + Session["Usuario"].ToString() + "'", conn);
                conn.Open();
                SqlDataReader dr = com.ExecuteReader();

                List<string> l = new List<string>();

                while (dr.Read())
                {

                    l.Add(dr["nome_pet"].ToString());

                }
                //dadosPet = dr.ToString();
                gdvPets.DataSource = l;
                gdvPets.DataBind();
                
            }

            catch (Exception e)
            {
                //dvAlerta.Visible = true;
            }
        }

        #endregion

        #region PetID
        public void PetID()
        {
            Random random = new Random();
            long id = random.Next(200001, 9999999);  //ID gerado randomicamente para novo dono/usuario

            lblPetID.Text = id.ToString();    //gerado em long e depois convertido para string para inserção em BD
        }
        #endregion

        #region Session/Nome Pet
        protected void gdvPets_RowCommand(object sender, GridViewCommandEventArgs e)
        {         
            if(e.CommandName.Equals("linkpet"))
            {
                Session["nomepet"] = ((System.Web.UI.WebControls.LinkButton)(e.CommandSource)).Text;
            }

            string teste = Session["nomepet"].ToString();

            Response.Redirect("PerfilPet.aspx");
           
        }

        #endregion

        #endregion 

        protected void labelnomepet_Click(object sender, EventArgs e)
        {
         
        }

        protected void PopulaDpdRacasCachorro()
        {
            using(var datacontext = new Pets2BDDataContext())
            {
                dpdRacaPet.DataSource = from p in datacontext.Racas_Pets
                                        orderby p.pets_cachorro
                                        select new { p.pets_cachorro};
                dpdRacaPet.DataTextField = "pets_cachorro";
                dpdRacaPet.DataValueField = "pets_cachorro";
                dpdRacaPet.DataBind();
            }

        }

        protected void PopulaDpdRacasGato()
        {
            using (var datacontext = new Pets2BDDataContext())
            {
                dpdRacaPet.DataSource = from p in datacontext.Racas_Pets
                                        where p.pets_gato != ""
                                        orderby p.pets_gato
                                        select new {p.pets_gato};
                dpdRacaPet.DataTextField = "pets_gato";
                dpdRacaPet.DataValueField = "pets_gato";
                dpdRacaPet.DataBind();
            }

        }

        protected void dpdEspeciePet_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                if (dpdEspeciePet.SelectedItem.Text == "Cachorro")
                {
                    PopulaDpdRacasCachorro();
                }

                else if (dpdEspeciePet.SelectedItem.Text == "Gato")
                {
                    PopulaDpdRacasGato();
                }
            
        }

        protected void btnBuscaDes_Click(object sender, EventArgs e)
        {
            using (var datacontext = new Pets2BDDataContext())
            {

                if (txtBuscaDes.Text == "")
                {
                    IEnumerable filtroperdido = from p in datacontext.Pets
                                                where p.status_pet == 0
                                                select new { p.nome_pet };
                    GridViewPerdido.DataSource = filtroperdido;
                }
                else 
                {
                    IEnumerable filtroperdido = from p in datacontext.Pets
                                                where p.status_pet == 0 && p.raca_pet == txtBuscaDes.Text || p.status_pet == 0 && p.sexo_pet == txtBuscaDes.Text || p.status_pet == 0 && p.nome_pet == txtBuscaDes.Text || p.status_pet == 0 && p.pet_id.ToString() == txtBuscaDes.Text || p.status_pet == 0 && p.especie_pet == txtBuscaDes.Text
                                                select new { p.nome_pet };
                    GridViewPerdido.DataSource = filtroperdido;
                }
             

              
                GridViewPerdido.DataBind();
            }

        }
       
    }
}