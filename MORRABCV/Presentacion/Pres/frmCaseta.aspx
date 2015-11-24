<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/Site.Master" AutoEventWireup="true" CodeBehind="frmCaseta.aspx.cs" Inherits="Presentacion.Pres.frmCaseta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br /><br />
                    <table>
                        <tr>
                            <td>Nombre: </td><td><asp:DropDownList ID="ListNombre" runat="server" OnSelectedIndexChanged="ListNombre_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            <td>Puesto: </td><td><asp:Label runat="server" ID="LabelPuesto" Text="---"></asp:Label></td>
                            <td>Hr: </td><td><asp:DropDownList ID="ListHr" runat="server"></asp:DropDownList></td>
                        </tr>
                    </table>
                <br /><br />
                    <table>
                        <tr>
                            <td>Día: <asp:Label runat="server" ID="LabelDia" Text="---"></asp:Label></td>
                            <td>Motivo: <asp:Label runat="server" ID="LabelMotivo" Text="---"></asp:Label></td>

                        </tr>
                    </table>
                <br /><br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
