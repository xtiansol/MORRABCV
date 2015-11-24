using System;
using System.Collections;
using AdminBD;

namespace Presentacion.Pres
{
    public class ServiciosGen
    {
        private static Config config = new Config();

        private static ConfBD confBD;

        private static ConfArch confArch;

        private static SQLDispatcher sqlDispatcher = new SQLDispatcher();


        public static void inicio()
        {
            config.getConfiguraciones();
            confArch = config.getConfArch();
            confBD = config.getConfBD();
        }

        public static ArrayList getAgendaActual(string idAgenda)
        {

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));


            string SQL = "SELECT  P.*  FROM  AGENDA P ";

            if (idAgenda != null && idAgenda != "" && idAgenda != "0")
            {
                SQL = SQL + "AND P.AGENDA_ID = " + idAgenda;
            }


            return sqlDispatcher.getColConsulta(SQL);
        }



        public static ArrayList getAgenda(string idAgenda)
        {

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));


            string SQL = "SELECT  P.*  FROM  AGENDA P ";

            if (idAgenda != null && idAgenda != "" && idAgenda != "0")
            {
                SQL = SQL + "AND P.AGENDA_ID = " + idAgenda;
            }


            return sqlDispatcher.getColConsulta(SQL);
        }


        public static ArrayList getAgendaCD(string idAgenda, string idUS)
        {

            string SQL = "SELECT DISTINCT PR.PERSONA_ID , " +
                        "PR.NOMBRE + ' ' + PR.AP_PATERNO + ' ' + PR.AP_MATERNO, " +
                        "PR.PUESTO_ID, " +
                        "PUE.DESC_PUESTO " +
                        "FROM " +
                        "AGENDA AG, " +
                        "PREREGISTRO PR, " +
                        "MOTIVO_VISITA MV, " +
                        "PUESTO PUE " +
                        "WHERE " +
                        "AG.FECHA_INICIO = CONVERT(DATE, GETDATE()) " +
                        "AND AG.ID_ANFITRION = PR.PERSONA_ID " +
                        "AND PUE.ID_PUESTO = PR.PUESTO_ID " +
                        "AND MV.ID_VISITA = PR.PUESTO_ID ";

            if (idAgenda != null && idAgenda != "" && idAgenda != "0")
            {
                SQL = SQL + " AND AG.AGENDA_ID = " + idAgenda;
            }

            if (idUS != null && idUS != "" && idUS != "0")
            {
                SQL = SQL + " AND AG.ID_ANFITRION = " + idUS;
            }


            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }

        public static ArrayList getAgendaCompleto(string idAgenda, string idUS, string idHora, string tpUs)
        {

            string SQL = "SELECT DISTINCT PR.PERSONA_ID , " +
                        "PR.NOMBRE + ' ' + PR.AP_PATERNO + ' ' + PR.AP_MATERNO, " +
                        "PR.PUESTO_ID, " +
                        "PUE.DESC_PUESTO, " +
                        "MV.ID_VISITA, " +
                        "MV.DESC_VISITA, " +
                        "HRS.IDHORA, " +
                        "HRS.DES_HORA, " +
                        "AG.FECHA_INICIO " +
                        "FROM " +
                        "AGENDA AG, " +
                        "PREREGISTRO PR, " +
                        "MOTIVO_VISITA MV, " +
                        "PUESTO PUE, " +
                        "HORAS HRS " +
                        "WHERE " +
                        "AG.FECHA_INICIO = CONVERT(DATE, GETDATE()) " +
                        "AND AG.ID_ANFITRION = PR.PERSONA_ID " +
                        "AND PUE.ID_PUESTO = PR.PUESTO_ID " +
                        "AND MV.ID_VISITA = AG.MVO_VISITA_ID " +
                        "AND HRS.IDHORA = AG.HORA ";

            if (idAgenda != null && idAgenda != "" && idAgenda != "0")
            {
                SQL = SQL + " AND AG.AGENDA_ID = " + idAgenda;
            }


            if (idUS != null && idUS != "" && idUS != "0")
            {
                SQL = SQL + " AND AG.ID_ANFITRION = " + idUS;
            }
 
            if (idHora != null && idHora != "" && idHora != "0")
            {
                SQL = SQL + " AND HRS.IDHORA = " + idHora;
            }

            if(tpUs != null && tpUs != "")
            {
                SQL = SQL + " AND PR.ESTADO_REGISTRO = '" + tpUs + "'";
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }


        public static ArrayList getAgendaVisitantes( string idUS, string idHora, string tpUs)
        {

            string SQL = "SELECT DISTINCT PR.PERSONA_ID , " +
                        "PR.NOMBRE + ' ' + PR.AP_PATERNO + ' ' + PR.AP_MATERNO, " +
                        "PR.PUESTO_ID, " +
                        "PUE.DESC_PUESTO, " +
                        "MV.ID_VISITA, " +
                        "MV.DESC_VISITA, " +
                        "HRS.IDHORA, " +
                        "HRS.DES_HORA, " +
                        "AG.FECHA_INICIO " +
                        "FROM " +
                        "AGENDA AG, " +
                        "PREREGISTRO PR, " +
                        "MOTIVO_VISITA MV, " +
                        "PUESTO PUE, " +
                        "HORAS HRS " +
                        "WHERE " +
                        "AG.FECHA_INICIO = CONVERT(DATE, GETDATE()) " +
                        "AND AG.ID_CONTACTO = PR.PERSONA_ID " +
                        "AND PUE.ID_PUESTO = PR.PUESTO_ID " +
                        "AND MV.ID_VISITA = AG.MVO_VISITA_ID " +
                        "AND HRS.IDHORA = AG.HORA " +
//                        "AND PR.ESTADO_REGISTRO = '"+tpUs+"' "+
                        "AND AG.AGENDA_ID IN ( " +
                        " SELECT AG1.AGENDA_ID " +
                        " FROM " +
                        " AGENDA AG1 " +
                        " WHERE " +
                        " AG1.ID_ANFITRION = " + idUS +
                        " AND AG1.HORA = "+ idHora +
                        ") ";


            if (tpUs != null && tpUs != "")
            {
                SQL = SQL + " AND PR.ESTADO_REGISTRO = '" + tpUs + "'";
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }


        public static ArrayList getAgendaHr(string idUS)
        {

            string SQL = "SELECT HR.IDHORA, HR.DES_HORA " +
                        "FROM " +
                        "HORAS HR, " +
                        "AGENDA AG " +
                        "WHERE " +
                        "AG.HORA = HR.IDHORA ";

            if (idUS != null && idUS != "" && idUS != "0")
            {
                SQL = SQL + "AND AG.ID_ANFITRION = " + idUS;
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }



        public static ArrayList getHr(string idHr)
        {

            string SQL = "SELECT HR.IDHORA, HR.DES_HORA " +
                                                "FROM " +
                                                "HORAS HR ";

            if (idHr != null && idHr != "" && idHr != "0")
            {
                SQL = SQL + "WHERE HR.IDHORA = " + idHr;
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }



        public static ArrayList getCaseta(string idCaseta)
        {

            string SQL = "SELECT  P.*  FROM  CASETA P ";

            if (idCaseta != null && idCaseta != "" && idCaseta != "0")
            {
                SQL = SQL + "WHERE P.CASETA_ID = " + idCaseta;
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }


        public static Boolean agregaReporteConsulta(string nombreReporte, string cadenaCampos, string cadenaConsultaReporte)
        {
            cadenaConsultaReporte = cadenaConsultaReporte.Replace("'", "''");
            string sql = "INSERT INTO HISTORICO_REPORTES VALUES('" + nombreReporte + "', '" + cadenaCampos + "','" + cadenaConsultaReporte + "', GetDate(), 1);";
            return sqlDispatcher.ejecutaSQL(sql);
        }

        public static ArrayList joinNombreAlias(ArrayList lNombre, ArrayList lAlias, string sep)
        {
            ArrayList resp = new ArrayList();
            if (sep == "")
            {
                sep = ".";
            }

            if (lNombre != null && lAlias != null)
            {
                for (int cont = 0; cont < lNombre.Count; cont++)
                {
                    resp.Add(lAlias[cont] + sep + lNombre[cont]);
                }

            }
            return resp;
        }

        public static string toStringArrayList(ArrayList lista, string sep)
        {
            string resp = "";
            string sepAux = "";
            if (sep == null)
            {
                sep = ",";
            }
            if (lista != null)
            {
                for (int cont = 0; cont < lista.Count; cont++)
                {
                    resp = resp + sepAux + lista[cont];
                    sepAux = sep;
                }
            }
            return resp;
        }

        // Recupera las tablas base de la BD
        public static ArrayList getHistorico()
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta("SELECT TOP 30 * FROM HISTORICO_REPORTES ORDER BY FECHAREG desc");
        }
        public static ArrayList getHistoricoById(string id)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta("SELECT * FROM HISTORICO_REPORTES WHERE HISTORICOID = " + id + " ORDER BY FECHAREG");
        }

        public static ArrayList getResultadoSQL(string sql)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(sql);
        }
    }
}