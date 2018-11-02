<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfilPet.aspx.cs" Inherits="Pets.PerfilPet" %>

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
    <title>Pets | Meu Pet</title>
</head>
<body id="body-perfilpet">  
    <form id="form1" runat="server">
          <asp:ScriptManager ID="scpManager" runat="server"></asp:ScriptManager>
      <a href="Profile.aspx"><header id="principal-header-pet"></header></a>
        <section id="corpo-perfil-pet" class="container-fluid"> 
            <div style="position:relative;float:right;margin-right:27%;padding-top:2.5%">
                <asp:Image ID="imgPerfilPet" runat="server" Height="150px" Width="150px" CssClass="img-thumbnail" />
                
            </div>
            
            <br /><br />
            <asp:FileUpload ID="FileUp" runat="server"  />
            <br />
             <label id="lblNomePetPerfil" runat="server" Visible="true"></label>
            <br />
            <br />
            
            <label id="lblPetIDPerfil" runat="server" Visible="true"></label> 
            <br />
            <br />
            <div class="conteudo-perfilpet">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>                                  
                    
                    <asp:Button ID="btnEncontrado" runat="server" Text="Encontrado!" CssClass="btn btn-success" Width="100%" Visible="false" Enabled="false" OnClick="btnEncontrado_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="upDesaparecido" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                <ContentTemplate>
                    
                    <asp:Button ID="btnDesaparecido" runat="server" Text="Desaparecido!" Width="100%" CssClass="btn btn-danger" data-toggle="modal" data-target="#myModal2" data-backdrop="false" />
                    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">

                        <ProgressTemplate>
                            <img src="img/loading.gif" style="width: 33px; width: 33px; position: absolute; top: 277px; margin-left: 7%" />
                            <br />
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                    <!-- Modal -->
                  <span id="lblDesaparecido" runat="server" visible="false" style="padding:10px;font-size:1em;width:90%;position:absolute;margin-top:95%" class="label label-danger">Desaparecido</span>
                    <br />
                    <strong><asp:Label ID="lblDataHora" runat="server" Text=""></asp:Label></strong>
                </ContentTemplate>
            </asp:UpdatePanel>
             </div>
        <br/>
        <br />
        <div class="conteudo-perfilpet">
        <asp:TextBox ID="txtNomePetPerfil" runat="server" Enabled="false"></asp:TextBox>
        
        <br />
        <br />        
       
        <asp:DropDownList ID="dpdEspeciePetPerfil" runat="server" Enabled="false">
            <asp:ListItem Value="Cachorro" Text="Cachorro"></asp:ListItem>
            <asp:ListItem Value="Gato" Text="Gato"></asp:ListItem>
        </asp:DropDownList>
                 
        <br />
        <br />
        
        <asp:TextBox ID="txtRacaPetPerfil" runat="server" Enabled="false"></asp:TextBox>
        
        <br />
        <br />

        <asp:RadioButtonList ID="rdSexoPetPerfil" runat="server" Enabled="false">
            <asp:ListItem Value="Macho" Text="Macho"></asp:ListItem>
            <asp:ListItem Value="Femea" Text="Fêmea"></asp:ListItem>
        </asp:RadioButtonList>

        <asp:TextBox ID="txtCorPetPerfil" runat="server" Enabled="false"></asp:TextBox>
        <br />
        <br />
  
                        <!-- modal btn desaparecido-->
            <div class="modal fade" id="myModal2" role="dialog">
                        <div class="modal-dialog">

                           
                            <div class="modal-content" style="width:60%">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">×</button>
                                    <h4 class="modal-title">Local de Desaparecimento</h4>
                                </div>
                                <div class="modal-body">                                  
                                    <p><strong>Então...</strong>Você pode utilizar sua localização de cadastro para local de desaparecimento ou declarar outra.</p>
                                    <p><strong>O que deseja fazer?</strong></p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="btnLocalizacaoPadrao" class="btn btn-default" data-dismiss="modal" runat="server" onserverclick="btnDesaparecido_Click">Utilizar Padrão</button>
                                    <button type="button" id="btnOutraLocalizacao" class="btn btn-default" data-dismiss="modal" runat="server" onserverclick="btnOutraLocalizacao_ServerClick">Outra Localização</button>
                                </div>
                            </div>

                        </div>
                    </div>
           
            <br />
          
            <asp:Button ID="btnSalvarPetPerfil" runat="server" Text="Salvar!" OnClick="btnSalvarPetPerfil_Click" Enabled="false" CssClass="btn btn-default" />
                  
                    &nbsp
        
        <asp:Button ID="btnEditarPetPerfil" runat="server" Text="Editar" OnClick="Editar_Click" CssClass="btn btn-default" />

                    &nbsp
        
        <asp:DropDownList ID="dpdOpPetPerfil" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dpdOpPetPerfil_SelectedIndexChanged">
            <asp:ListItem Value="0" Selected="True" disabled="disabled" Text="(Selecione Opção)"></asp:ListItem>
            <asp:ListItem Value="1" Text="Retirar Pet"></asp:ListItem>          
            <asp:ListItem Value="2" Text="Comprar Medalha"></asp:ListItem>
        </asp:DropDownList>
                          </div>  
      
          <br />
        </section>   
    </form> 
</body>
</html>
