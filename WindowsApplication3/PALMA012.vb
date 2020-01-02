Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA012
    Private mensaje As New Clase_mensaje
    Private UNA_MAQUINA As New Clase_maquina
    Private Tabla_Calibracion As New DataTable
    Private TipoMaq As TIPO_DE_MAQUINA
    Private Sub PALMA012_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString

    End Sub

    Private Sub btVerificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If IsNothing(TXTSERIE.Text) = False Then
            LLENAR(TXTSERIE.Text)
        Else
            mensaje.MERRO008()
        End If
    End Sub
    Private Sub TXTSERIE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSERIE.DoubleClick
        Dim PANTALLA As New PALMA010BIS
        PANTALLA.ShowDialog()
        If PANTALLA.llerrespuesta = True Then
            TXTSERIE.Text = PANTALLA.leernserie
            LLENAR(TXTSERIE.Text)
        End If
    End Sub
    Private Sub btBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        BORRAR()
    End Sub
#Region "FUNCIONES"
   
    Private Sub LLENAR_DGV_TRASABILIDAD(ByVal cod As String)
        DGV2.Rows.Clear()
        Dim OBS As String
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT T_TRANS_MQ_120.FECHA_120, T_TRANS_MQ_120.DEPO1_120, T_TRANS_MQ_120.DEPO2_120, T_TRANS_MQ_120.OBS_120, DET_PARAMETRO_802.DESC_802 FROM T_TRANS_MQ_120 INNER JOIN DET_PARAMETRO_802 ON T_TRANS_MQ_120.ESTADO_120 = DET_PARAMETRO_802.C_PARA_802 WHERE (T_TRANS_MQ_120.NSERIE_120 = @D1) AND (DET_PARAMETRO_802.C_TABLA_802 = 17) ORDER BY T_TRANS_MQ_120.FECHA_120", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", cod))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            Do While LECTOR.Read
                If IsDBNull(LECTOR.GetValue(3)) Then
                    OBS = ""
                Else
                    OBS = LECTOR.GetValue(3)
                End If
                DGV2.Rows.Add(CDate(LECTOR.GetDateTime(0)).ToShortDateString, NOM_DEPOSITOS2(LECTOR.GetString(1).ToString), NOM_DEPOSITOS2(LECTOR.GetString(2).ToString), OBS.ToString, LECTOR.GetValue(4))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    Private Sub LLENAR_DGV_CALIBRACION(ByVal cod As String)
        DGV1.Rows.Clear()
        Dim OBS As String
        Dim FBAJA As String
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT FCALI_118,NCALI_118,VTO_118,FBAJA_118,OBS_118 FROM T_CALIBRACION_118 WHERE NSERIE_118 = @D1 ORDER BY FCALI_118", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", cod))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            Do While LECTOR.Read
                If IsDBNull(LECTOR.GetValue(3)) Then
                    FBAJA = ""
                Else
                    FBAJA = CDate(LECTOR.GetDateTime(3)).ToShortDateString
                End If

                If IsDBNull(LECTOR.GetValue(4)) Then
                    OBS = ""
                Else
                    OBS = LECTOR.GetValue(4)
                End If
                DGV1.Rows.Add(CDate(LECTOR.GetDateTime(0)).ToShortDateString, LECTOR.GetString(1).ToString, CDate(LECTOR.GetDateTime(2)).ToShortDateString, FBAJA.ToString, OBS.ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        If DGV1.RowCount <> 0 Then
            Button1.Visible = True
        Else
            Button1.Visible = False
        End If
    End Sub
    Private Sub LLENAR_tabla_CALIBRACION(ByVal cod As String)
        Tabla_Calibracion.Clear()
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlDataAdapter("SELECT NSERIE_118, TIPO_118, FCALI_118, NCALI_118, VTO_118, FBAJA_118, OBS_118, RESPON_118 FROM T_CALIBRACION_118 WHERE (NSERIE_118 = @D1) ORDER BY FCALI_118, TIPO_118", CNN)
            ADT.SelectCommand.Parameters.Add(New SqlParameter("D1", cod))
            ADT.Fill(Tabla_Calibracion)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    Private Sub LLENAR(ByVal NSERIE As String)
        UNA_MAQUINA.TOMAR(NSERIE)
        If UNA_MAQUINA.LEEREXISTE = True Then
            TXTSERIE.Enabled = False
            lbModelo.Text = UNA_MAQUINA.LEERMODELO
            lbChapa.Text = UNA_MAQUINA.LEERCHAPA
            lbMarca.Text = UNA_MAQUINA.LEERMARCA
            lbFalta.Text = CDate(UNA_MAQUINA.LEEFALTA).ToShortDateString
            lbOBS.Text = UNA_MAQUINA.LEEOBS
            lbFbaja.Text = UNA_MAQUINA.LEERFBAJA
            TipoMaq = New TIPO_DE_MAQUINA(UNA_MAQUINA.LEERTIPO)
            lbTipo.Text = TipoMaq.leerDescripcion
            lbEstado.Text = DESC_Estado(UNA_MAQUINA.LEERESTADO)
            lbDeposito.Text = NOM_DEPOSITOS2(UNA_MAQUINA.LEERALMACEN)
            LLENAR_DGV_CALIBRACION(UNA_MAQUINA.LEERNSERIE)
            LLENAR_DGV_TRASABILIDAD(UNA_MAQUINA.LEERNSERIE)
            If TipoMaq.leerCalibracion = 1 Or TipoMaq.leerPlazoVerif = 1 Then
                LinkLabel1.Visible = True
            End If
        Else
            MessageBox.Show("EL NUMERO DE SERIE NO EXISTE", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
    End Sub
    Private Function NOM_DEPOSITOS2(ByVal cod As String) As String
        Dim resp As String = ""
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptador As New SqlCommand("SELECT (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE NDOC_003=@D1", cnn2)
            adaptador.Parameters.Add(New SqlParameter("D1", cod))
            'LLENO EL ADAPTADOR CON EL DATASET
            Dim lector As SqlDataReader = adaptador.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        Return resp
    End Function
    Private Function ValidarFechaCalibracion(ByVal numero As String) As Date
        Dim fecha As Date
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT VTO_118 FROM T_CALIBRACION_118 WHERE NSERIE_118 = @D1 AND FBAJA_118 IS NULL", cnn)
            adt.Parameters.Add(New SqlParameter("D1", numero))
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            If LECTOR.Read Then
                fecha = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return fecha
    End Function
    
    Private Sub BORRAR()
        LinkLabel1.Visible = False
        DGV1.Rows.Clear()
        DGV2.Rows.Clear()
        lbDeposito.Text = Nothing
        lbOBS.Text = Nothing
        lbEstado.Text = Nothing
        lbTipo.Text = Nothing
        TXTSERIE.Text = Nothing
        TXTSERIE.Enabled = True
        lbModelo.Text = Nothing
        lbChapa.Text = Nothing
        lbFalta.Text = Nothing
        lbMarca.Text = Nothing
        lbFbaja.Text = Nothing
        lbChapa.Enabled = False
        lbOBS.Enabled = False
        lbMarca.Enabled = False
    End Sub
   
    Private Function DESC_Estado(ByVal COD As Integer) As String
        Dim RESP As String = ""
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 17 and C_PARA_802=@D1", CNN)
            ADT.Parameters.AddWithValue("D1", COD)
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                RESP = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Return RESP
    End Function
#End Region
    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim p As New PALMA016
        p.TOMAR(TXTSERIE.Text, UNA_MAQUINA.LEERTIPO)
        p.ShowDialog()
    End Sub



  
    Private Sub lbEstado_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEstado.DoubleClick
        Dim PANTALLA As New PALMA012_BIS
        PANTALLA.tomardato(UNA_MAQUINA.LEERESTADO)
        PANTALLA.ShowDialog()
        If PANTALLA.ler_respuest = True Then
            'actualizo 
            UNA_MAQUINA.ActualizarEstado(PANTALLA.leer_codigo)
            UNA_MAQUINA.Grabar_Movimiento(UNA_MAQUINA.LEERALMACEN, "", PANTALLA.leer_codigo)
            LLENAR_DGV_TRASABILIDAD(UNA_MAQUINA.LEERNSERIE)
            lbEstado.Text = PANTALLA.leer_descripcion
        End If
    End Sub

  
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If DGV1.RowCount <> 0 Then
            LLENAR_tabla_CALIBRACION(TXTSERIE.Text)
            Dim Tabla As New INSTRUMENTO
            Tabla.Tables("ENCABEZADO").Rows.Add(TXTSERIE.Text, lbTipo.Text, lbDeposito.Text, Date.Now.ToShortDateString)
            Dim INDICADOR As String
            Dim TIPO As Integer
            Dim FCAL As String
            Dim FVERIF As String
            Dim FVTO As String
            Dim RESPONSABLE As String
            Dim OBS As String
            Dim NCAL As String
            Dim APTO As String = "X"
            For I = 0 To Tabla_Calibracion.Rows.Count - 1
                INDICADOR = Tabla_Calibracion.Rows(I).Item(0).ToString
                TIPO = Tabla_Calibracion.Rows(I).Item(1)
                If TIPO = 1 Then
                    FCAL = CDate(Tabla_Calibracion.Rows(I).Item(2)).ToShortDateString
                    FVERIF = ""
                Else
                    FCAL = ""
                    FVERIF = CDate(Tabla_Calibracion.Rows(I).Item(2)).ToShortDateString
                End If
                FVTO = CDate(Tabla_Calibracion.Rows(I).Item(4)).ToShortDateString
                NCAL = Tabla_Calibracion.Rows(I).Item(3).ToString
                If IsDBNull(Tabla_Calibracion.Rows(I).Item(7)) Then
                    RESPONSABLE = ""
                Else
                    RESPONSABLE = Tabla_Calibracion.Rows(I).Item(7).ToString
                End If
                If IsDBNull(Tabla_Calibracion.Rows(I).Item(6)) Then
                    OBS = ""
                Else
                    OBS = Tabla_Calibracion.Rows(I).Item(6).ToString
                End If
                Tabla.Tables("DETALLE").Rows.Add(INDICADOR, TIPO, FCAL, FVERIF, FVTO, RESPONSABLE, OBS, NCAL, APTO)
            Next
            Dim PANTALLA As New PALMA010BIS2
            PANTALLA.LEER(Tabla)
            PANTALLA.ShowDialog()


        End If
    End Sub

    Private Sub TXTSERIE_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TXTSERIE.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If IsNothing(TXTSERIE.Text) = False Then
                LLENAR(TXTSERIE.Text)
            Else
                mensaje.MERRO008()
            End If
        End If
    End Sub

   
End Class