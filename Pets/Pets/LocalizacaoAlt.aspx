<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalizacaoAlt.aspx.cs" Inherits="Pets.LocalizacaoAlt" %>

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
    <title>Pets | Endereço Alternativo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="endereco-alt">
        <h3>Insira a localização</h3>
        <br />
        <br />
        <asp:TextBox ID="txtEndereco" runat="server" CssClass="form-control" Height="26px" Width="100%" BorderColor="#dddddd"></asp:TextBox>
        
        <br />
         
        <div id="mapa" style="width: 100%; height: 250px;"></div>

        <input type="text" id="txtLatitude" runat="server" class="transparencia" />
        <input type="text" id="txtLongitude" runat="server" class="transparencia" />

        <br />          
       
        <button id="btnProntoAlt" runat="server" value="Pronto!" class="btn btn-default" onserverclick="btnProntoAlt_ServerClick">Pronto!</button>
        
    </div>
    </form>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyCpEcOPBtVymaAsIGJa1o2V1Qo301B5NBw&amp;sensor=false"></script> 
</body>
</html>
