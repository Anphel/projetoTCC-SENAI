<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pets.Login" %>

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
    <title>Pets</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="principal-page" class="container-fluid">
            <header id="login-usuario" class="form-inline">
                <%--<label id="lblLogin" runat="server">Login: </label>--%>
            <strong>Login: &nbsp</strong> <input type="email" id="txtEmailLogin" runat="server" class="form-control" />

               <%-- <label id="lblSenha" runat="server">Senha:</label>--%>
                <asp:TextBox ID="txtSenhaLogin" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>&nbsp&nbsp&nbsp&nbsp           
                
                <button id="btnLogin" runat="server" value="Ir!" onserverclick="btnLogin_Click" class="btn btn-warning">Ir!</button>&nbsp&nbsp&nbsp&nbsp

                <br />

                <asp:Label ID="lblStatus" runat="server" Text="" ForeColor="DarkRed" Font-Bold="true"></asp:Label>
                <input type="hidden" id="txtID" runat="server" />

            </header>
            <div id="rec-senha">
                <a href="#" data-toggle="modal" data-target="#myModal">Esqueceu sua senha?</a>&nbsp&nbsp&nbsp&nbsp
                <asp:Label runat="server" ID="erroEmail" Font-Bold="true" Visible="true"></asp:Label>
                
                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5>Insira e-mail cadastrado para envio da senha:</h5>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <asp:TextBox ID="txtEmailRecSenha" runat="server" Width="230px"></asp:TextBox>
                            </div>
                            <div class="modal-footer">
                                <input type="button" id="btnEmailRecSenha" runat="server" value="Enviar!" onserverclick="btnEmailRecSenha_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="sublinha" />
            <section id="cadastro-usuario">
                <div id="dados-usuario" class="form-inline">
                    <br />
                    <br />
                    <h3>Cadastrar-me!</h3>

                    <br />
                   
                     <asp:RequiredFieldValidator 
                        ID="validaNome" runat="server" 
                        ValidationGroup="valGrupoCad" 
                        ControlToValidate="txtNome" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>
                   
                     <asp:RequiredFieldValidator 
                        ID="ValidaSobrenome" runat="server" 
                        ValidationGroup="valGrupoCad" 
                        ControlToValidate="txtSobreNome" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtSobrenome" runat="server" CssClass="form-control" Width="250px"></asp:TextBox><br />
                    
                    <br />
                    
                    <asp:RequiredFieldValidator 
                        ID="validaEmail" runat="server" 
                        ValidationGroup="valGrupoCad" 
                        ControlToValidate="txtEmail"                        
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>                 
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Width="250px"></asp:TextBox>                                       
 
                    <asp:RequiredFieldValidator 
                        ID="validaSenha" runat="server" 
                        ValidationGroup="valGrupoCad" 
                        ControlToValidate="txtSenha" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control pwd" Width="250px"></asp:TextBox>                  
                    
                    <span id="mostrar-senha">                       
                       <a href="#"><img src="img/pata.png" alt="Exibir sua senha" title="Exibir sua senha" class="reveal" /></a>                       
                    </span>
                     <br /> 
                    <asp:RegularExpressionValidator 
                        ID="regexEmail" runat="server" 
                        ValidationGroup="valGrupoCad" 
                        ControlToValidate="txtEmail" 
                        ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                        ErrorMessage="E-mail Inválido" 
                        ForeColor="DarkRed"></asp:RegularExpressionValidator>
                  
                      <br />
               <div id="alerta-email">
                   <label id="lblAlertaEmail" runat="server" style="color:darkred"></label>
               </div>
                     </div>
                <br />
                <br />
                <div id="localizacao-usuario">

                    <h4>Agora, insira sua localização...</h4>
                    
                    <asp:RequiredFieldValidator 
                        ID="validaEndereco" runat="server" 
                        ValidationGroup="valGrupoCad" 
                        ControlToValidate="txtEndereco" 
                        ErrorMessage="" CssClass="validacao"></asp:RequiredFieldValidator>                   
                    <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control" Height="26px" Width="96%" BorderColor="#dddddd"></asp:TextBox>
                 
                    <br />
                    <br />
                    <br />

                    <div id="mapa"></div>

                    <input type="text" id="txtLatitude" runat="server" class="transparencia" />
                    <input type="text" id="txtLongitude" runat="server" class="transparencia"/>
                   
                     <br />

                    <span title="cadastro" id="botao-cad">                      
                        <button id="btnCadastrar" runat="server" validationgroup="valGrupoCad" value="Pronto!" onserverclick="btnCadastrar_Click" class="btn-basico">Pronto!</button>
                    </span>
                </div>

                <br />
            
            </section>
        </div>       
    </form>
    <script>

    $(function(){
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
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCpEcOPBtVymaAsIGJa1o2V1Qo301B5NBw&amp;sensor=false"></script>   
   <script>
       $(".reveal").on('click', function () {
           var $pwd = $(".pwd");
           if ($pwd.attr('type') === 'password') {
               $pwd.attr('type', 'text');
           } else {
               $pwd.attr('type', 'password');
           }
       });
   </script>
</body>
</html>
