﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/Site.Master" AutoEventWireup="true" CodeBehind="frmCaseta.aspx.cs" Inherits="Presentacion.Pres.frmCaseta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <script type="text/javascript">
         var gPrefijo = "MainContent_";
         function marcaRegistro() {
             var datIdUs = "";
             var datIdAg = "";
             var sep = "";
             $("input[type=checkbox]:checked").each(function () {
                 //cada elemento seleccionado
                 list = $(this).attr("id").split("-");
                 datIdUs = datIdUs + sep + list[2];
                 datIdAg = datIdAg + sep + list[3];
                 sep = ","
             });
             //alert("datIdUs:" + datIdUs);
             //alert("datIdAg:"+datIdAg);

             if (datIdUs != "") {
                 $("#" + gPrefijo + "idUsHid").val(datIdUs);
                 $("#" + gPrefijo + "idAgendaHid").val(datIdAg);
             } else {
                 alert("Debe seleccionar algun asistente.");
                 return false;
             }
             return true;
         }
     </script>

            <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <table style="width:90%; margin: auto;"  >

                        <tr>
                            <td style="width:100%">
                                        <asp:Panel ID="pExtrnos" runat="server" 
                                        GroupingText="Agenda:" Font-Names="adobe arabic" Font-Size="14pt" Height="88px">
                                                <table>
                                                    <tr>
                                                        <td style="width:10%">Nombre: </td>
                                                        <td style="width:30%"><asp:DropDownList ID="ListNombre" CssClass="boxLeft" runat="server" OnSelectedIndexChanged="ListNombre_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                        <td style="width:10%">Puesto: </td>
                                                        <td style="width:30%"><asp:Label runat="server" ID="LabelPuesto" Text="---"></asp:Label></td>
                                                        <td style="width:10%">Hr: </td>
                                                        <td style="width:15%"><asp:DropDownList ID="ListHr" CssClass="boxLeft" runat="server" OnSelectedIndexChanged="ListHr_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">&nbsp;</td>
                                                    </tr>
                                                </table>
                                        </asp:Panel>
                             </td>
                        </tr>
                        <tr>
                            <td style="width:100%">
                                 <asp:Panel ID="PanelInterno" runat="server" 
                                        GroupingText="Datos Generales:" Font-Names="adobe arabic" Font-Size="14pt"  Height="88px">
                                        <table>
                                            <tr>
                                                <td style="width:10%">Día: </td>
                                                <td style="width:38%"><asp:Label runat="server" ID="LabelDia" Text="---"></asp:Label></td>
                                                <td style="width:8%">Motivo:</td>
                                                <td style="width:30%"><asp:Label runat="server" ID="LabelMotivo" Text="---"></asp:Label></td>
                                                <td colspan="2"></td>
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


                                    <br />
                    <table  style="width:90%; margin: auto;"  >
                            <tr>

                                <td  style="width:100%">
                                    <asp:Panel ID="Panel1" runat="server" 
                                        GroupingText="Lista de Visitantes:" Font-Names="adobe arabic" Font-Size="14pt" Height="88px">
                                        <table style="width:100%">
                                            <tr>
                                                <td  style="width:100%">
                                                    <asp:Panel runat="server" ID="PanelConVisita" Width="100%">
                                                        <table border='1' width ='100%' > <tr style='background-color:#990000'> <td  style='width:38%'>NOMBRE VISITANTE</td><td  style='width:18%'>TP VEHÍCULO</td><td  style='width:15%'>MARCA</td><td  style='width:10%'>COLOR</td><td  style='width:10%'>PLACA</td><td  style='width:10%'>Asistió</td></tr> </table>
                                                    </asp:Panel>
                                                </td>
                                    
                                            </tr>
                                            <tr>
                                                <td  style="width:100%">
                                                    <asp:Label ID="idRespuesta" runat="server" Text="   "></asp:Label>
                                                </td>
                                    
                                            </tr>
                                            <tr>
                                                <td  style="width:100%"><asp:Button ID="idButReg" runat="server" Text="Registrar" OnClientClick="javascript:return marcaRegistro()" OnClick="idButReg_Click" />
                                                    <asp:HiddenField ID="idAgendaHid" Value="" runat="server" />
                                                    <asp:HiddenField ID="idUsHid" Value="" runat="server" />
                                                    <asp:HiddenField ID="idDesHid" Value="" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                     </asp:Panel>
                                </td>
                            </tr>
                    </table>
                    <br /><br />
            </ContentTemplate>
        </asp:UpdatePanel>


<%--    <div style="">

        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
<%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br /><br />
                <div>
<table style="width:90%; margin: auto;"  >

    <tr>
        <td style="width:100%">
            <asp:Panel ID="pExtrnos" runat="server" 
            GroupingText="AGENDA:" Font-Names="adobe arabic" Font-Size="14pt" Height="88px">
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
                                    <td>
                                        <asp:Label ID="idRespuesta" runat="server" Text="   "></asp:Label>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td><asp:Button CssClass="btnGuarda" ID="idButReg" runat="server" Text="Registrar"  OnClientClick="javascript:return marcaRegistro()" OnClick="idButReg_Click" />
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
    </div>--%>
</asp:Content>
