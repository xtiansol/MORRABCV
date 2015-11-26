<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/Site.Master" AutoEventWireup="true" CodeBehind="frmCaseta.aspx.cs" Inherits="Presentacion.Pres.frmCaseta" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

         function ShowAgregaAutoModal(idAgenda, idUS) {
             //var frame = $get('IframeEdit');
             //frame.src = "frmFiltros.aspx?UIMODE=EDIT&EID=222";
             if (idAgenda != null && idAgenda != "" && idUS != null && idUS != "") {
                 $("#" + gPrefijo + "ListaTPAuto").prop('selectedIndex', 0);
                 $("#" + gPrefijo + "ListaMarcaAuto").prop('selectedIndex', 0);
                 $("#" + gPrefijo + "ListaColorAuto").prop('selectedIndex', 0);
                 $("#" + gPrefijo + "TxtPlacaAuto").val("");

                 $("#" + gPrefijo + "idAgendaAutoHid").val(idAgenda);
                 $("#" + gPrefijo + "idUsAutoHid").val(idUS);

                 $find('mpeSeleccion').show();
             }
         }


        function mpeSeleccionOnOk() {
            alert("ya entre...");
            //var lista1 = $("#" + gPrefijo + "CamposSeleccionados");
            //var listaF = document.getElementById(gPrefijo + "CamposSeleccionadosFin");
            //var filtroFinVar = "";
            //var campoNom = "";
            //var contF = 0;

            //if ($("#" + gPrefijo + "CamposSeleccionados option").length > 0) {

            //    $('select#' + gPrefijo + 'CamposSeleccionados').find('option').each(function () {

            //        var campoHD = $(this).val();

            //        if ($("#" + "hdf" + campoHD) != null) {

            //            var campoFin = "";
            //            var campoAlias = "";
            //            var combo = $("#" + gPrefijo + "cmb" + campoHD + " option:selected");
            //            var x = combo.index();
            //            var valorCampo = $("#" + gPrefijo + "txt" + campoHD).val();
            //            if (x > 0 && valorCampo != null && valorCampo != "") {
            //                var comboText = x > 0 ? combo.val() : "";
            //                //var text = $("#" + gPrefijo + "txt" + campoHD).val();
            //                //var filtro = document.getElementById("hiddenFiltros");
            //                campoFin = campoHD + " " + comboText + " '" + $.trim(valorCampo) + "'";
            //                filtroFinVar = filtroFinVar + "|" + campoFin;
            //                campoNom = campoNom + "|" + campoHD;
            //                var no = new Option();
            //                no.value = campoFin;
            //                no.text = campoFin;
            //                listaF[contF] = no;
            //                contF++;
            //            }
            //        }

            //    });
            //    if (filtroFinVar == "" && campoNom == "") {
            //        alert("No existen campos para agregar filtros!!");
            //    } else {
            //        $.ajax({
            //            type: "POST",
            //            url: "../Reportes/SolicitudesGen.asmx/AgregaFiltros",
            //            data: "{filtroFin:\"" + filtroFinVar + "\",campos:\"" + campoNom + "\"}",
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json",
            //            success: function (msg) {
            //                if (msg.hasOwnProperty('d')) {
            //                    msg = msg.d;
            //                }
            //                var json = JSON.parse(msg);
            //                alert(json.mensaje);
            //            },
            //            error: function (xhr, status, error) {
            //                alert("No se pudo agregar el Filtro...");
            //            }
            //        });
            //    }
            //} else {
            //    alert("Debe agregar campos a la lista de campos seleccionados.");
            //}
            return true;

        }

        function mpeSeleccionOnCancel() {
            //var txtSituacion = document.getElementById("txtSituacion");
            //txtSituacion.value = "";
            //txtSituacion.style.backgroundColor = "#FFFF99";
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

                     <asp:Panel ID="pnlAltaAuto" runat="server" CssClass="CajaDialogo"  Width="700" Style="display: none;">
                                <div class="FooterDialogo">
                                    <asp:Label ID="Label4" runat="server" Text="Agregar datos Auto:" />
                                </div>
                                <div>
                                    <asp:Panel runat="server" ID="Panel2" >
                                    </asp:Panel>
                                     <div id="contenedorAltaAuto" height="600" width="650" >
                                             <div>
                                                    <table width="100%" >
                                                        <tr>
                                                            <td>TIPO AUTO:</td><td><asp:DropDownList ID="ListaTPAuto" runat="server"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td>MARCA:</td><td><asp:DropDownList ID="ListaMarcaAuto" runat="server"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td>COLOR:</td><td><asp:DropDownList ID="ListaColorAuto" runat="server"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td>PLACA:</td><td><asp:TextBox ID="TxtPlacaAuto" runat="server"></asp:TextBox></td>
                                                            <asp:HiddenField ID="idAgendaAutoHid" runat="server" />
                                                            <asp:HiddenField ID="idUsAutoHid" runat="server" />
                                                        </tr>
                                                    </table>
                                            </div>
                                     </div>
                                    <asp:Button ID="Button1" CssClass="btn btn-default" runat="server" Text="Aceptar1" OnClick ="AgregaAuto_Click" />
                                    
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancelar" CssClass="btn btn-default" runat="server" Text="Cancelar" />

                                </div>
                               <div  style='display:none;'>
                                   <asp:Button ID="btnAceptar" CssClass="btn btn-default" runat="server" Text="Aceptar" OnClick ="AgregaAuto_Click" />
                                  <asp:Button ID="btnAsistente" runat="server" Text="GenerarReporteHid" />
                              </div>

                    </asp:Panel>
                    <cc1:ModalPopupExtender ID="mpeSeleccion" runat="server" TargetControlID="btnAsistente"
                            PopupControlID="pnlAltaAuto" OkControlID="btnAceptar" CancelControlID="btnCancelar"
                            OnOkScript="mpeSeleccionOnOk()" OnCancelScript="mpeSeleccionOnCancel()" DropShadow="True"
                            BackgroundCssClass="FondoAplicacion" BehaviorID="mpeSeleccion" />

            </ContentTemplate>
        </asp:UpdatePanel>


</asp:Content>
