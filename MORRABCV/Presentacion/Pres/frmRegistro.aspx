﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/Site.Master" AutoEventWireup="true" CodeBehind="frmRegistro.aspx.cs" Inherits="Presentacion.Pres.frmRegistro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <script  type="text/javascript">
         var gPrefijo = "MainContent_";
         function marcaRegistro() {
             var datIdUs = "";
             var descrip = "";
             var sep = "";
             $("input[type=checkbox]:checked").each(function () {
                 //cada elemento seleccionado
                 list = $(this).attr("id").split("-");
                 datIdUs = datIdUs + sep + list[2];
                 des = $("#"+gPrefijo+"txtBox"+list[2]).val();
                 descrip = descrip + sep +"'"+des+"'";
                 sep =","
             });
             alert("datIdUs:" + datIdUs);
             alert("descrip:" + descrip);
             return false;
         }
     </script>


       <div style="">

        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br /><br />
                <div>
<table style="width:90%; margin: auto;"  >

    <tr>
        <td style="width:100%">
            <asp:Panel ID="pExtrnos" runat="server" 
            GroupingText="Registro:" Font-Names="adobe arabic" Font-Size="14pt" Height="88px">
                    <table>
                        <tr>
                            <td>Nombre: </td><td><asp:DropDownList ID="ListNombre" CssClass="boxLeft" runat="server" OnSelectedIndexChanged="ListNombre_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                            <td>Puesto: </td><td><asp:Label runat="server" ID="LabelPuesto" Text="---"></asp:Label></td>
                            <td>Hr: </td><td><asp:DropDownList ID="ListHr" CssClass="boxLeft" runat="server" OnSelectedIndexChanged="ListHr_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                                    <asp:Panel ID="PanelInterno" runat="server" 
            GroupingText="DATOS GENERALES:" Font-Names="adobe arabic" Font-Size="14pt">
                    <table>
                        <tr>
                            <td>Día: <asp:Label runat="server" ID="LabelDia" Text="---"></asp:Label></td>
                            <td>Motivo: <asp:Label runat="server" ID="LabelMotivo" Text="---"></asp:Label></td>

                        </tr>
                    </table>
                        </asp:Panel>


                            </td>
                        </tr>
                    </table>
            </asp:Panel>
                </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
 </table>
  </div>


                <div>
                                    <br /><br /><br /><br /><br /><br />
                    <table>
                            <tr>

                                <td>
                        <asp:Panel ID="Panel1" runat="server" 
                            GroupingText="Lista de Visitantes:" Font-Names="adobe arabic" Font-Size="14pt" Height="88px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Panel runat="server" ID="PanelConVisita">
                                        </asp:Panel>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td><asp:Button CssClass="btnGuarda" ID="idButReg" runat="server" Text="Registrar" OnClientClick="javascript:return marcaRegistro()" />
                                        <asp:HiddenField ID="idAgendaHid" Value="" runat="server" />
                                        <asp:HiddenField ID="idUsHid" Value="" runat="server" />
                                    </td>
                                </tr>
                            </table>

                            

                            </asp:Panel>
        </td>

    </tr>


                    </table>
                    <br /><br />

                </div>



                <br /><br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>