<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Pets.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script> 
    <link rel="stylesheet" href="css/estilo.css" type="text/css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" />
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script> 
    <script type="text/javascript" src="js/jquery-ui.custom.min.js"></script> 
    <script src="/js/jqBootstrapValidation.js"></script>
    <title>Pets | Meu Perfil</title>
</head>
<body style="background-color:#e5e3e3">
    <form id="form1" runat="server">       
    <div style="height:240px; width:100%">
        <header id="principal-perfil-dono">
            <div id="menu-perfil-dono">
                <div id="img-perfil">
                    <asp:Image ID="imgPerfil" runat="server" Height="150px" Width="150px" CssClass="img-thumbnail" />
                </div>
                <input type="button" id="btnLogout" runat="server" value="Sair" onserverclick="btnLogout_Click" class="btn btn-warning" style="position: relative; top: 1px; left: 332px; height: 32px;" />
                <a href="EditarPerfil.aspx">
                    <label id="lblNomePerfil" runat="server" style="position: relative; top: 9px; left: 87px; text-align:center"></label>
                </a>
                <br />

                &nbsp&nbsp&nbsp 
       
        <br />
            </div>
        </header>
    </div>
        
        <div class="container" style="position:fixed; top:43%;left:23%; z-index:9999;width:500px">
            <h2>Cadastre pets</h2>
            <button type="button" class="btn btn-default" data-toggle="collapse" data-target="#demo">Clique!</button>
            <div id="demo" class="collapse" style="background-color:#fff;width:500px">
                <asp:Label ID="lblPetID" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <asp:ScriptManager ID="scpManager" runat="server" AsyncPostBackTimeout="1"></asp:ScriptManager>
                <div>
                    <asp:Timer ID="Timer1" OnTick="dpdEspeciePet_SelectedIndexChanged" runat="server" Interval="654321"></asp:Timer>
                </div>
                <asp:UpdatePanel ID="upDesaparecido" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </Triggers>
                    <ContentTemplate>
                <div class="form-inline">
                    <label id="lblNomePet" runat="server">Nome:</label>
                    <label id="lblEspeciePet" runat="server">Seu amigo(a)...</label><br />

                    <asp:RequiredFieldValidator 
                        ID="validaNomePet" runat="server" 
                        ValidationGroup="valGrupoCadPet" 
                        ControlToValidate="txtNomePet" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>
                    <input type="text" id="txtNomePet" runat="server" placeholder="Nome do seu pet" class="form-control" style="width:15%; height:25px" /> &nbsp&nbsp
                    
                    <asp:RequiredFieldValidator 
                        ID="validaEspeciePet" runat="server" 
                        ValidationGroup="valGrupoCadPet" 
                        ControlToValidate="dpdEspeciePet" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="dpdEspeciePet" runat="server" Height="25px" AutoPostBack="true" OnSelectedIndexChanged="dpdEspeciePet_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected="True" disabled="disabled" Text="(Selecione Opção)"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Cachorro"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Gato"></asp:ListItem>
                    </asp:DropDownList>
                    </div>
            
                <br />
                <div class="form-inline">
                    <label id="lblRacaPet" runat="server">Raça:</label> &nbsp
                    <label id="lblSexoPet" runat="server">Sexo:</label><br />

                   
                            <asp:RequiredFieldValidator 
                                ID="validaRacaPet" runat="server" 
                                ValidationGroup="valGrupoCadPet" 
                                ControlToValidate="dpdRacaPet" 
                                ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="dpdRacaPet" runat="server" Width="171px" Height="25px" AutoPostBack="false"></asp:DropDownList>                    
                    
                    <asp:RequiredFieldValidator 
                        ID="validadeSexoPet" runat="server" 
                        ValidationGroup="valGrupoCadPet" 
                        ControlToValidate="rdSexoPet" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>         
                    <asp:RadioButtonList ID="rdSexoPet" runat="server" CssClass="radio">
                        <asp:ListItem Value="Macho" Text="Macho"></asp:ListItem>
                        <asp:ListItem Value="Femea" Text="Fêmea"></asp:ListItem>
                    </asp:RadioButtonList>                        
                </div>
                    
                <br />
                             
                <div class="form-group">
                    <label id="lblCorPet" runat="server">Cor:</label><br />
                    <asp:RequiredFieldValidator 
                        ID="validaCorPet" runat="server" 
                        ValidationGroup="valGrupoCadPet" 
                        ControlToValidate="txtCorPet" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>
                    <input type="text" id="txtCorPet" runat="server" placeholder="Cor de seu pet" class="form-control" style="width:20%; height:25px" />
                </div>
                        
                <br />
                   
                <input type="button" id="btnCadastrarPet" runat="server" value="Cadastrar!" onserverclick="btnCadastrarPet_ServerClick" class="btn btn-warning" validationgroup="valGrupoCadPet" />
            </div>
        </div>
                        </ContentTemplate>
                </asp:UpdatePanel>
        
             <div style="position:fixed;top:57%;left:24%" class="meus-pets">
             <asp:GridView ID="gdvPets" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" OnRowCommand="gdvPets_RowCommand" Width="93px">
                <Columns>
                    <asp:TemplateField HeaderText="Meus Pets" SortExpression="nome_pet">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("nome_pet") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
            
                        <a href="PerfilPet.aspx"><asp:LinkButton ID="labelnomepet" CommandName="linkpet" runat="server" Text='<%# Bind("nome_pet") %>'></asp:LinkButton></a>                           
                        </ItemTemplate>                       
                        <ControlStyle BorderColor="White" BorderStyle="None" Width="300px" />
                        <HeaderStyle BorderStyle="None" />
                        <ItemStyle BorderStyle="None" />
                    </asp:TemplateField>                       
                </Columns>
                
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connectionString %>" SelectCommand="SELECT nome_pet FROM Pet WHERE (dono_pet = @email_dono)">
                <SelectParameters>
                    <asp:SessionParameter Name="email_dono" SessionField="Usuario" />
                </SelectParameters>
            </asp:SqlDataSource>
                 </div>
                
                
                <div id="text-buscarpets">
                    <asp:TextBox ID="txtBuscaDes" runat="server" Width="300px"></asp:TextBox> &nbsp&nbsp
                    <asp:Button ID="btnBuscaDes" runat="server" OnClick="btnBuscaDes_Click" Text="Buscar" />
                </div>
                
                
                <div id="busca-desaparecidos">
                    <asp:GridView ID="GridViewPerdido" runat="server" AutoGenerateColumns="false" OnRowCommand="gdvPets_RowCommand" Width="93px">
                        <Columns>
                            <asp:TemplateField HeaderText="Desaparecidos" SortExpression="nome_pet">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("nome_pet") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>

                                    <a href="PerfilPet.aspx">
                                        <asp:LinkButton ID="labelnomepet" CommandName="linkpet" runat="server" Text='<%# Bind("nome_pet") %>'></asp:LinkButton></a>
                                </ItemTemplate>
                                <ControlStyle BorderColor="White" BorderStyle="None" Width="300px" />
                                <HeaderStyle BorderStyle="None" />
                                <ItemStyle BorderStyle="None" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
    </form>
     <script>

         $(function () {
             if (typeof ValidatorUpdateDisplay != 'undefined') {
                 var originalValidatorUpdateDisplay = ValidatorUpdateDisplay;

                 ValidatorUpdateDisplay = function (val) {
                     if (!val.isvalid) {
                         $("#" + val.controltovalidate).css("border", "1px solid orange");
                     }

                     else if (typeof ValidatorUpdateDisplay != 'defined') {
                         $("#" + val.controltovalidate).css("border", "1px solid #ccc");
                     }

                     originalValidatorUpdateDisplay(val);
                 }
             }
         });

</script>
</body>
</html>
