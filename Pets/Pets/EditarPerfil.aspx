<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarPerfil.aspx.cs" Inherits="Pets.EditarPerfil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />  
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script> 
    <link rel="stylesheet" href="css/estilo.css" type="text/css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" />
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript" src="js/script.js"></script> 
    <script type="text/javascript" src="js/jquery-ui.custom.min.js"></script> 
    <script src="/js/jqBootstrapValidation.js"></script>
    <title>Pets | Editar Perfil</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="principalEP">                       
        <asp:Label runat="server" ID="lblID" >ID</asp:Label><br />
          <label>Nome:</label>
          <asp:TextBox ID="txtEditNome" runat="server" Width="70%"></asp:TextBox><br /><br />
          <label>Sobrenome:</label>
          <asp:TextBox ID="txtEditSobrenome" runat="server" Width="70%" ></asp:TextBox><br /><br />
        <label>Telefone</label>
          <asp:TextBox ID="TextBox1" runat="server" Width="70%" ></asp:TextBox><br /> <br />
        <label>Link rede social</label>
          <asp:TextBox ID="TextBox2" runat="server" Width="70%" ></asp:TextBox><br /><br />          
          <label>E-mail:</label>
          <asp:TextBox ID="txtEditEmail" runat="server" Width="70%" ></asp:TextBox><br /><br />
          <label>Senha antiga:</label><br />
          <asp:TextBox ID="txtEditSenhaAntiga" TextMode="Password" runat="server" Width="57%"></asp:TextBox> <button id="btnV" runat="server" onserverclick="btnV_ServerClick" >Verificar</button> <br /><br />
          <asp:Label ID="lblSenha" runat="server"  Visible="false" CssClass="label" Text="Digite a nova senha:"></asp:Label><br />
          <asp:TextBox ID="txtEditSenhaNova" Visible="false" TextMode="Password"  runat="server" Width="70%"></asp:TextBox><br />
        <asp:Image runat="server" ImageUrl="~/ImageHandler.ashx?id=" ID="imgPerfil" BorderStyle="Solid" BorderWidth="2px" Width="125px" Height="125px" />
        <asp:Label ID="lblStatusImg" runat="server" Visible="false"></asp:Label>
         <br />
        
        <label>Foto perfil:</label>
          <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>   <br />
             <label>Endereço:</label><asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control" Height="26px" Width="96%" BorderColor="#dddddd"></asp:TextBox>
                 <br />
                                <div id="mapa" style="height:400px; width:100%;"></div>
                    
                    <input type="text" id="txtLatitude" runat="server" class="transparencia" />
                    <input type="text" id="txtLongitude" runat="server" class="transparencia"/>
        <button id="btnSave" runat="server" onserverclick="btnSalvar_Click" >Salvar</button>
                            
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
