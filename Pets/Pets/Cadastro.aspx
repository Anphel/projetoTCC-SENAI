<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="Pets.Cadastro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <h2>Pets</h2>

        <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox><br /><br />

        <asp:Label ID="lblNome" runat="server">Nome</asp:Label>
        <asp:TextBox ID="txtNome" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="lblSobrenome" runat="server">Sobrenome</asp:Label>
        <asp:TextBox ID="txtSobrenome" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="lblEmail" runat="server">E-mail</asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="lblSenha" runat="server">Senha</asp:Label>
        <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox><br /><br /><br />

<%--    <asp:FileUpload ID="fileImg" runat="server" Width="150px" Height="190px"/><br /><br />
        <asp:Button ID="btnInserirImg" runat="server" Text="Salvar" OnClick="btnInserirImg_Click"/><br /><br />
        <asp:Label ID="lblErro" runat="server" Text=""></asp:Label><br /><br /><br />--%>

        <h3>Agora, por favor, insira sua localização:</h3><br />
        
        <asp:Label ID="lblCep" runat="server">CEP</asp:Label>
        <asp:TextBox ID="txtCep" runat="server" OnTextChanged="txtCep_TextChanged" AutoPostBack="true"></asp:TextBox>

            <br /><br /><br />

        <asp:Label ID="lblNumero" runat="server">Número</asp:Label>
        <asp:TextBox ID="txtNumero" runat="server" ></asp:TextBox><br /><br />

        <asp:Label ID="lblRua" runat="server">Rua</asp:Label>
        <asp:TextBox ID="txtRua" runat="server" Enabled="false"></asp:TextBox><br /><br />

        <asp:Label ID="lblBairro" runat="server">Bairro</asp:Label>
        <asp:TextBox ID="txtBairro" runat="server" Enabled="false"></asp:TextBox><br /><br />

        <asp:Label ID="lblCidade" runat="server">Cidade</asp:Label>
        <asp:TextBox ID="txtCidade" runat="server" Enabled="false"></asp:TextBox><br /><br />
        
        <asp:Label ID="lblEstado" runat="server">Estado</asp:Label>
        <asp:TextBox ID="txtEstado" runat="server" Enabled="false"></asp:TextBox><br /><br />

        <asp:Button ID="btnCadastrar" runat="server" Text="Pronto!" OnClick="btnCadastrar_Click" />
    </div>
    </form>
</body>
</html>
