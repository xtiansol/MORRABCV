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

                ListHr.Items.Add("Seleccionar...");

            }
        }

        protected void ListNombre_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ListNombre.SelectedIndex > 0)
            {
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
                PanelConVisita.Controls.Clear();
                idButReg.Style.Add("display", "none");
            }
        }

        protected void ListHr_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ListHr.SelectedIndex > 0)
            {
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
                PanelConVisita.Controls.Clear();
                idButReg.Style.Add("display", "none");
            }
        }


        protected void agregaVisitantes(string idAgenda, string idUS, string idHora)
        {
            //            camposFin.Clear();
            PanelConVisita.Controls.Clear();
            PanelConVisita.Controls.Add(new LiteralControl("<table border='1'> <tr class='Titulo' ><td>NOMBRE VISITANTE</td><td>&nbsp;</td><td>Asistió</td><td>Descripción</td></tr> "));

            ArrayList arrVisitantes = ServiciosGen.getAgendaVisitantes(idUS, idHora, "E");

            foreach (ArrayList v1 in arrVisitantes)
            {
                agregarVisitante((string)v1[0], (string)v1[1]);
            }

            PanelConVisita.Controls.Add(new LiteralControl("</table>"));
            idButReg.Style.Add("display", "block");


        }
        protected void agregarVisitante(string idUS, string nombre)
        {
            try
            {
                Label campoSel = new Label();
                campoSel.Text = nombre;

                CheckBox chB = new CheckBox();
                chB.ID = "checkId" + idUS;

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
    }
}