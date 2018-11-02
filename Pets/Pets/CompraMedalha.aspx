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
    <title></title>
</head>
<body style="background-image:url(img/pegadas.jpg);background-repeat:repeat">
    <form id="form1" runat="server">
    
      
           
            <img src="img/medalha.jpg" style="position:absolute; top: 10%; left: 40%;" />
       
        
        
            <label>Comprar coleira</label><br /><br />
            <div style="position:absolute; top: 295px; left: 536px;">
            <label id="lblPetID" runat="server">PetID:</label><br />
            <label id="lblNomePet" runat="server">Meu Nome:</label><br />
            <label id="lblContato" runat="server"></label><br />
            <label id="lblPets" runat="server" style="font-size:0.8em">pets.com.br</label><br />
            </div>
            <br /><br />
           
           
           
           
             <asp:Button runat="server" ID="btnComprar" Text="Comprar" CssClass="file-upload" Height="5%" Width="10%" />

     
    </form>

    
</body>
</html>
 
