using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Pres
{
    public partial class frmRegistro : System.Web.UI.Page
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
            PanelConVisita.Controls.Add(new LiteralControl("<table border='1' width ='90%' > <tr style='background-color:#990000'><td style='width:50%'>NOMBRE VISITANTE</td><td style='width:10%'>&nbsp;</td><td  style='width:10%'>Asistió</td><td  style='width:30%'>Descripción</td></tr> "));

            ArrayList arrVisitantes = ServiciosGen.getRegistroVisitantes(idUS, idHora, "E");

            foreach (ArrayList v1 in arrVisitantes)
            {
                agregarVisitante((string)v1[0], (string)v1[1], (string)v1[9]);
                idAgendaHid.Value = (string)v1[9];
            }

            PanelConVisita.Controls.Add(new LiteralControl("</table>"));
            idButReg.Style.Add("display", "block");


        }
        protected void agregarVisitante(string idUS, string nombre, string idAgenda)
        {
            try
            {
                Label campoSel = new Label();
                campoSel.Text = nombre;

                CheckBox chB = new CheckBox();

                chB.ID = "-checkId-" + idUS + "-" + idAgenda; ;

                TextBox tBox = new TextBox();
                tBox.ID = "txtBox" + idUS;
                tBox.CssClass = "boxLeft";

                HiddenField nuevoHidF = new HiddenField();
                nuevoHidF.ID = "hdf" + nombre;
                nuevoHidF.Value = nombre;
                AgregarControles(campoSel, chB, tBox, nuevoHidF);
                //contadorControles++;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        protected void AgregarControles(Label nombreContr, CheckBox chB, TextBox tbox, HiddenField nuevoHidF)
        {
            try
            {
                PanelConVisita.Controls.Add(new LiteralControl("<tr>"));
                PanelConVisita.Controls.Add(new LiteralControl("<td>"));
                PanelConVisita.Controls.Add(nombreContr);
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                PanelConVisita.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                PanelConVisita.Controls.Add(chB);
                PanelConVisita.Controls.Add(new LiteralControl("</td><td>"));
                PanelConVisita.Controls.Add(tbox);
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

            Response.Write("<script language=javascript>alert('Se registro la asistencia...');</script>");
        }

        protected void idButReg_Click1(object sender, EventArgs e)
        {
            string agendaID = idAgendaHid.Value;
            string idUss = idUsHid.Value;
            string observs = idDesHid.Value;

            string[] words = agendaID.Split(',');
            string[] words2 = idUss.Split(',');
            string[] words3 = observs.Split(',');

            for (int cont = 0; cont < words.Length; cont++)
            {
                String idAg = words[cont];
                string idUs = words2[cont];
                string obs = words3[cont];
                if (idAg != "" && idUs != "")
                {
                    ServiciosGen.actualizaRegistro(idAg, idUs, obs);
                }
            }

            //Response.Write("<script language=javascript>alert('Se registro la asistencia...');</script>");
            idRespuesta.Text = "Registro exitoso...";
        }
    }
}