using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Pres
{
    public partial class frmCaseta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ServiciosGen.inicio();
                ArrayList listNombres = ServiciosGen.getAgendaCD(null, null);
                
                ListNombre.Items.Clear();
                ListNombre.Items.Add("Seleccionar...");
                foreach (ArrayList rNombre in listNombres)
                {
                    ListItem litem = new ListItem();
                    litem.Value = (string)rNombre[0];
                    litem.Text = "" + rNombre[1];
                    ListNombre.Items.Add(litem);
                }
                idButReg.Style.Add("display", "none");
                idRespuesta.Text = "";

                ListHr.Items.Add("Seleccionar...");


                ArrayList listTPAuto = ServiciosGen.getTPAuto(null);
                ListaTPAuto.Items.Clear();
                ListaTPAuto.Items.Add("Seleccionar...");
                foreach (ArrayList rTPA in listTPAuto)
                {
                    ListItem litem = new ListItem();
                    litem.Value = (string)rTPA[0];
                    litem.Text = "" + rTPA[1];
                    ListaTPAuto.Items.Add(litem);
                }

                ArrayList listMarca = ServiciosGen.getMarca(null);
                ListaMarcaAuto.Items.Clear();
                ListaMarcaAuto.Items.Add("Seleccionar...");
                foreach (ArrayList rMA in listMarca)
                {
                    ListItem litem = new ListItem();
                    litem.Value = (string)rMA[0];
                    litem.Text = "" + rMA[1];
                    ListaMarcaAuto.Items.Add(litem);
                }

                ArrayList listColor = ServiciosGen.getColor(null);
                ListaColorAuto.Items.Clear();
                ListaColorAuto.Items.Add("Seleccionar...");
                foreach (ArrayList rCol in listColor)
                {
                    ListItem litem = new ListItem();
                    litem.Value = (string)rCol[0];
                    litem.Text = "" + rCol[1];
                    ListaColorAuto.Items.Add(litem);
                }

            }
        }

        protected void ListNombre_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ListNombre.SelectedIndex > 0)
            {
                idRespuesta.Text = "";
                idButReg.Style.Add("display", "none");
                ListItem nombreItm = ListNombre.SelectedItem;
                ArrayList listHoras = ServiciosGen.getAgendaHr(nombreItm.Value);

                ListHr.Items.Clear();
                ListHr.Items.Add("Seleccionar...");
                foreach (ArrayList rHora in listHoras)
                {
                    ListItem litem = new ListItem();
                    litem.Value = (string)rHora[0];
                    litem.Text = "" + rHora[1];
                    ListHr.Items.Add(litem);
                }



                ListItem itemSel = ListNombre.SelectedItem;

                ArrayList listNombres = ServiciosGen.getAgendaCD(null, itemSel.Value);

                foreach (ArrayList rNombre in listNombres)
                {

                    LabelPuesto.Text = (string)rNombre[3];
                    //LabelMotivo.Text = (string)rNombre[7];
                    //LabelDia.Text = (string)rNombre[9];

                }
                LabelMotivo.Text = "";
                LabelDia.Text = "";
            }
            else
            {
                ListHr.Items.Clear();
                LabelMotivo.Text = "";
                LabelDia.Text = "";
                LabelPuesto.Text = "";
                idRespuesta.Text = "";
                //PanelConVisita.Controls.Clear();
                idButReg.Style.Add("display", "none");
            }
        }

        protected void ListHr_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ListHr.SelectedIndex > 0)
            {
                idRespuesta.Text = "";
                idButReg.Style.Add("display", "none");
                ListItem nombreItm = ListNombre.SelectedItem;
                ListItem hrItm = ListHr.SelectedItem;

                ArrayList listDatos = ServiciosGen.getAgendaCompleto(null, nombreItm.Value, hrItm.Value, null);

                foreach (ArrayList rDatos in listDatos)
                {

                    //LabelPuesto.Text = (string)rNombre[5];
                    LabelMotivo.Text = (string)rDatos[5];
                    LabelDia.Text = (string)rDatos[8];

                }

                agregaVisitantes(null, nombreItm.Value, hrItm.Value);
            }
            else
            {
                LabelMotivo.Text = "";
                LabelDia.Text = "";
                idRespuesta.Text = "";
                //PanelConVisita.Controls.Clear();
                idButReg.Style.Add("display", "none");
            }
        }


        protected void agregaVisitantes(string idAgenda, string idUS, string idHora)
        {
            //            camposFin.Clear();
            PanelConVisita.Controls.Clear();
            PanelConVisita.Controls.Add(new LiteralControl("<table border='1' width ='100%' > <tr style='background-color:#990000'> <td  style='width:38%'>NOMBRE VISITANTE</td><td  style='width:18%'>TP VEHÍCULO</td><td  style='width:15%'>MARCA</td><td  style='width:10%'>COLOR</td><td  style='width:10%'>PLACA</td><td  style='width:5%'>Asistió</td><td  style='width:5%'></td></tr>"));

            ArrayList arrVisitantes = ServiciosGen.getAgendaVisitantes(idUS, idHora, "E");

            foreach(ArrayList v1 in arrVisitantes)
            {
                agregarVisitante((string)v1[0], (string)v1[1],(string)v1[9], (string)v1[10], (string)v1[11], (string)v1[12], (string)v1[13], (string)v1[14]);
            }

            PanelConVisita.Controls.Add(new LiteralControl("</table>"));
            idButReg.Style.Add("display", "block");


        }
        protected void agregarVisitante(string idUS, string nombre, string idAgenda, string tpVehi, string marca, string color, string placa, string hora)
        {
            try
            {
                Label campoSel = new Label();
                campoSel.Text = nombre;

                CheckBox chB = new CheckBox();
                chB.ID = "-checkId-"+idUS+"-"+idAgenda;
          

                HiddenField nuevoHidF = new HiddenField();
                nuevoHidF.ID = "hdf" + nombre;
                nuevoHidF.Value = nombre;

                Label lbTpVeh = new Label();
                lbTpVeh.Text = (tpVehi!=null && tpVehi.ToUpper()!="NULL")? tpVehi:"";

                Label lbMarca = new Label();
                lbMarca.Text = (marca != null && marca.ToUpper() != "NULL") ? marca : ""; 

                Label lbColor = new Label();
                lbColor.Text = (color != null && color.ToUpper() != "NULL") ? color : "";

                Label lbPlaca = new Label();
                lbPlaca.Text = (placa != null && placa.ToUpper() != "NULL") ? placa : "";

                HyperLink linkAuto = null;

                if (placa == null || placa.ToUpper() == "NULL" || placa == "" )
                {
                    linkAuto = new HyperLink();
                    linkAuto.ID = "linkRepHis-" + idAgenda + "-" + idUS;
                    linkAuto.Text = "Agrega Auto";
                    //linkAuto.ImageUrl = "../Reportes/img/PDF-icon3.png";
                    linkAuto.NavigateUrl = "#";
                    linkAuto.Attributes.Add("onClick", "javascript:return ShowAgregaAutoModal(" + idAgenda + ", " + idUS + ");");
                }

                Label lbHoralleg = new Label();
                lbHoralleg.Text = "";
                if ( hora == null || hora.ToUpper() == "NULL" || hora == "")
                {
                    lbHoralleg.Text = "";
                }
                else
                {
                    lbHoralleg.Text = hora;
                }

                AgregarControles(campoSel, chB, nuevoHidF, lbTpVeh, lbMarca, lbColor, lbPlaca, linkAuto, lbHoralleg);
                //contadorControles++;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void AgregarControles(Label nombreContr, CheckBox chB, HiddenField nuevoHidF, Label tpVeh, Label marca, Label color, Label placa, HyperLink hpl, Label hora)
        {
            try
            {
                PanelConVisita.Controls.Add(new LiteralControl("<tr><td>"));
                PanelConVisita.Controls.Add(nombreContr);
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                PanelConVisita.Controls.Add(tpVeh);
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                PanelConVisita.Controls.Add(marca);
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                PanelConVisita.Controls.Add(color);
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                PanelConVisita.Controls.Add(placa);
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                if (hora.Text=="")
                {
                    PanelConVisita.Controls.Add(chB);
                }
                else
                {
                    PanelConVisita.Controls.Add(hora);
                }
                
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                if (hpl != null)
                {
                    PanelConVisita.Controls.Add(hpl);
                }
                PanelConVisita.Controls.Add(new LiteralControl("</td></tr>"));
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void idButReg_Click(object sender, EventArgs e)
        {
            string agendaID = idAgendaHid.Value;
            string idUss = idUsHid.Value;
            ListItem nombreItm = ListNombre.SelectedItem;
            ListItem hrItm = ListHr.SelectedItem;

            string[] words = agendaID.Split(',');
            string[] words2 = idUss.Split(',');

            for (int cont = 0; cont < words.Length; cont++)
            {
                String idAg = words[cont];
                string idUs = words2[cont];
                if (idAg != "" && idUs != "")
                {
                    ServiciosGen.agregaControlVisita(idAg, idUs);
                }
            }

            //Response.Write("<script language=javascript>alert('Se registro la asistencia...');</script>");
            idRespuesta.Text = "Se registro la asistencia...";
            agregaVisitantes(null, nombreItm.Value, hrItm.Value);
        }

        protected void AgregaAuto_Click(object sender, EventArgs e)
        {
            string agendaID = idAgendaAutoHid.Value;
            string idUss = idUsAutoHid.Value;

            if (ServiciosGen.agregaAuto(agendaID, idUss, ListaTPAuto.SelectedItem.Value, ListaMarcaAuto.SelectedItem.Value, ListaColorAuto.SelectedItem.Value, TxtPlacaAuto.Text))
            {

                ListItem nombreItm = ListNombre.SelectedItem;
                ListItem hrItm = ListHr.SelectedItem;

                agregaVisitantes(null, nombreItm.Value, hrItm.Value);
                idRespuesta.Text = "";
            }
            else
            {
                idRespuesta.Text = "NO Se registro el auto...";
            }
            Response.Write("<script language='javascript'> alert('Respuesta...'); </script>");
            //Response.Write("<script language=javascript>alert('Se registro la asistencia...');</script>");

        }
    }

}