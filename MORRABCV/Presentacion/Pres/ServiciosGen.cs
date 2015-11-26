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
                        "ISNULL(PR.PUESTO_ID,0), " +
                        "ISNULL((SELECT TOP 1 PU.DESC_PUESTO FROM PUESTO PU WHERE PU.ID_PUESTO = PR.PUESTO_ID),''), " +
                        "MV.ID_VISITA, " +
                        "MV.DESC_VISITA, " +
                        "HRS.IDHORA, " +
                        "HRS.DES_HORA, " +
                        "AG.FECHA_INICIO, " +
                        "AG.AGENDA_ID, " +
                        "ISNULL((SELECT TOP 1 TV.DESC_TIPO_VEHICULO FROM AUTOS CS, TIPO_VEHICULO TV WHERE TV.ID_VEHICULO = CS.ID_TIPO_VEHICULO AND CS.ID_VISITA = PR.PERSONA_ID ),'') AS TP_VEH , " +
                        "ISNULL((SELECT TOP 1 MR.DESC_MARCA FROM AUTOS CS, MARCA MR WHERE MR.ID_MARCA = CS.ID_MARCA AND CS.ID_VISITA = PR.PERSONA_ID),'') AS MARCA, " +
                        "ISNULL((SELECT TOP 1 COL.DESC_COLOR FROM AUTOS CS, COLOR COL WHERE COL.ID_COLOR = CS.ID_COLOR AND CS.ID_VISITA = PR.PERSONA_ID ),'') AS COLOR, " +
                        "ISNULL((SELECT TOP 1 CS.PLACA FROM AUTOS CS WHERE CS.ID_VISITA = PR.PERSONA_ID ),'') AS PLACA, " +
                        "ISNULL(CV.HORA_LLEGADA,'') HR_LLEGADA " +
                        "FROM " +
                        "AGENDA AG, " +
                        "PREREGISTRO PR, " +
                        "MOTIVO_VISITA MV, " +
                        "HORAS HRS, " +
                        "CONTROLVISITAS CV " +
                        "WHERE " +
                        "AG.FECHA_INICIO = CONVERT(DATE, GETDATE()) " +
                        "AND CV.PERSONA_ID = PR.PERSONA_ID " +
                        "AND CV.AGENDA_ID = AG.AGENDA_ID " +
                        "AND MV.ID_VISITA = AG.MVO_VISITA_ID " +
                        "AND HRS.IDHORA = AG.HORA " +
                        "AND AG.AGENDA_ID IN ( " +
                        " SELECT AG1.AGENDA_ID " +
                        " FROM " +
                        " AGENDA AG1 " +
                        " WHERE " +
                        " AG1.ID_ANFITRION = " + idUS +
                        " AND AG1.HORA = "+ idHora +
                        ") "
                        //+ "AND CV.HORA_LLEGADA IS NULL "
                         ;


            if (tpUs != null && tpUs != "")
            {
                SQL = SQL + " AND PR.ESTADO_REGISTRO = '" + tpUs + "'";
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }

        public static ArrayList getRegistroVisitantes(string idUS, string idHora, string tpUs)
        {

            string SQL = "SELECT DISTINCT PR.PERSONA_ID , " +
                           "PR.NOMBRE + ' ' + PR.AP_PATERNO + ' ' + PR.AP_MATERNO, " +
                           "ISNULL(PR.PUESTO_ID,0), " +
                           "ISNULL((SELECT TOP 1 PU.DESC_PUESTO FROM PUESTO PU WHERE PU.ID_PUESTO = PR.PUESTO_ID),''), " +
                           "MV.ID_VISITA, " +
                           "MV.DESC_VISITA, " +
                           "HRS.IDHORA, " +
                           "HRS.DES_HORA, " +
                           "AG.FECHA_INICIO, " +
                           "AG.AGENDA_ID, " +
                           "ISNULL((SELECT TOP 1 TV.DESC_TIPO_VEHICULO FROM AUTOS CS, TIPO_VEHICULO TV WHERE TV.ID_VEHICULO = CS.ID_TIPO_VEHICULO AND CS.ID_VISITA = PR.PERSONA_ID AND CS.ID_AGENDA = AG.AGENDA_ID ),'') AS TP_VEH , " +
                           "ISNULL((SELECT TOP 1 MR.DESC_MARCA FROM AUTOS CS, MARCA MR WHERE MR.ID_MARCA = CS.ID_MARCA AND CS.ID_VISITA = PR.PERSONA_ID AND CS.ID_AGENDA = AG.AGENDA_ID ),'') AS MARCA, " +
                           "ISNULL((SELECT TOP 1 COL.DESC_COLOR FROM AUTOS CS, COLOR COL WHERE COL.ID_COLOR = CS.ID_COLOR AND CS.ID_VISITA = PR.PERSONA_ID AND CS.ID_AGENDA = AG.AGENDA_ID ),'') AS COLOR, " +
                           "ISNULL((SELECT TOP 1 CS.PLACA FROM AUTOS CS WHERE CS.ID_VISITA = PR.PERSONA_ID AND CS.ID_AGENDA = AG.AGENDA_ID ),'') AS PLACA, " +
                           "ISNULL((SELECT TOP 1 REG.HORA_LLEGADA FROM REGISTRO REG WHERE REG.AGENDA_ID = AG.AGENDA_ID AND REG.PERSONA_ID = PR.PERSONA_ID ), '') AS HORA_REG, " +
                           "ISNULL((SELECT TOP 1 REG.OBSERVACIONES FROM REGISTRO REG WHERE REG.AGENDA_ID = AG.AGENDA_ID AND REG.PERSONA_ID = PR.PERSONA_ID ), '') AS OBSERVACIONES " +
                           "FROM " +
                           "AGENDA AG, " +
                           "PREREGISTRO PR, " +
                           "MOTIVO_VISITA MV, " +
                           "HORAS HRS, " +
                           "CONTROLVISITAS CV " +
                           "WHERE " +
                           "AG.FECHA_INICIO = CONVERT(DATE, GETDATE()) " +
                           "AND CV.PERSONA_ID = PR.PERSONA_ID " +
                           "AND CV.AGENDA_ID = AG.AGENDA_ID " +
                           "AND MV.ID_VISITA = AG.MVO_VISITA_ID " +
                           "AND HRS.IDHORA = AG.HORA " +
                           "AND AG.AGENDA_ID IN ( " +
                           " SELECT AG1.AGENDA_ID " +
                           " FROM " +
                           " AGENDA AG1 " +
                           " WHERE " +
                           " AG1.ID_ANFITRION = " + idUS +
                           " AND AG1.HORA = " + idHora +
                           ") " +
                           "AND CV.HORA_LLEGADA IS NOT NULL " 
                           //+ " AND ( NOT EXISTS( " +
                           //" SELECT * FROM REGISTRO REG " +
                           //" WHERE " +
                           //" REG.AGENDA_ID IN(SELECT AG1.AGENDA_ID  FROM  AGENDA AG1  WHERE  AG1.ID_ANFITRION = " + idUS + " AND AG1.HORA = " + idHora + ") " +
                           //" AND REG.PERSONA_ID = PR.PERSONA_ID ))"
                           ; 


            if (tpUs != null && tpUs != "")
            {
                SQL = SQL + " AND PR.ESTADO_REGISTRO = '" + tpUs + "'";
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }

        public static ArrayList getAgendaHr(string idUS)
        {

            string SQL = "SELECT DISTINCT HR.IDHORA, HR.DES_HORA " +
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

        public static ArrayList getTPAuto(string idTPAuto)
        {
            string SQL = "SELECT  TP.*  FROM  TIPO_VEHICULO TP   ";

            if (idTPAuto != null && idTPAuto != "" && idTPAuto != "0")
            {
                SQL = SQL + "WHERE TP.ID_VEHICULO = " + idTPAuto;
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }

        public static ArrayList getMarca(string idMarca)
        {
            string SQL = "SELECT  TP.*  FROM  MARCA TP ";

            if (idMarca != null && idMarca != "" && idMarca != "0")
            {
                SQL = SQL + "WHERE TP.ID_MARCA = " + idMarca;
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }

        public static ArrayList getColor(string idColor)
        {
            string SQL = "SELECT  TP.*  FROM  COLOR TP ";

            if (idColor != null && idColor != "" && idColor != "0")
            {
                SQL = SQL + "WHERE TP.ID_COLOR = " + idColor;
            }

            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(SQL);
        }

        public static Boolean agregaControlVisita(string idAgenda, string idUs)
        {

            //string sql = "INSERT INTO CONTROLVISITAS VALUES(" + idAgenda + ", " + idUs + ", GetDate())";
            string sql = "UPDATE CONTROLVISITAS SET HORA_LLEGADA = GetDate() WHERE AGENDA_ID = " + idAgenda + " AND PERSONA_ID = " + idUs + " ";
            return sqlDispatcher.ejecutaSQL(sql);
        }

        public static Boolean actualizaRegistro(string idAgenda, string idUs, string observaciones)
        {
            string sql = "INSERT INTO REGISTRO VALUES(" + idAgenda + ", " + idUs + ", GetDate(), "+observaciones+")";
            //string sql = "UPDATE REGISTRO SET HORA_LLEGADA = GetDate(), OBSERVACIONES = '"+observaciones+"' WHERE AGENDA_ID = " + idAgenda + " AND  PERSONA_ID = " + idUs + " ";
            return sqlDispatcher.ejecutaSQL(sql);

        }


        public static Boolean agregaAuto(string idAgenda, string idUs, string tpVehi, string marca, string color, string placa)
        {
            string sql = "INSERT INTO AUTOS VALUES(" + idAgenda + ", " + idUs + ", " + marca + ", " + color + ", " + tpVehi + ", '" + placa + "' )";
            //string sql = "UPDATE REGISTRO SET HORA_LLEGADA = GetDate(), OBSERVACIONES = '"+observaciones+"' WHERE AGENDA_ID = " + idAgenda + " AND  PERSONA_ID = " + idUs + " ";
            return sqlDispatcher.ejecutaSQL(sql);

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



        public static ArrayList getResultadoSQL(string sql)
        {
            sqlDispatcher.getConexion(confBD.NomBD, confBD.Servidor, confBD.Us, confBD.Pwd, Int32.Parse(confBD.BD));
            return sqlDispatcher.getColConsulta(sql);
        }
    }
}