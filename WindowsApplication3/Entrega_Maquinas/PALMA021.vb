Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA021
    Private Mi_mensaje As New Clase_mensaje
    Private Unamaquina As New Clase_maquina
    Private ListaTranspaso As New List(Of Transpaso)
    Private TipoMaq As TIPO_DE_MAQUINA
    Private Sub PALMA021_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_Desde()
        LLENAR_ESTADO()
    End Sub

#Region "funciones"
    Private Sub BORRAR()
        CBDEPOSITO.Text = Nothing
        CBDEPOSITO.Enabled = False
        cbDesde.Text = Nothing
        cbDesde.Enabled = True
        lbSerie.Text = Nothing
        lbOBS.Text = Nothing
        CBESTADO.Text = Nothing
        CBESTADO.Enabled = False
        lbTipo.Text = Nothing
        lbModelo.Text = Nothing
        lbChapa.Text = Nothing
        lbMarca.Text = Nothing
        lbChapa.Enabled = False
        lbOBS.Enabled = False
        lbMarca.Enabled = False
        Button1.Enabled = False
        TXTOBS.Text = Nothing
        TXTOBS.Enabled = False
        btConfirmar.Visible = False
    End Sub
    Private Sub LLENAR_ESTADO()
        Dim TABLAS As New DataTable
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptador As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 17 ORDER BY DESC_802 ", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptador.Fill(TABLAS)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        CBESTADO.DataSource = TABLAS
        CBESTADO.DisplayMember = "DESC_802"
        CBESTADO.ValueMember = "C_PARA_802"
        CBESTADO.Text = Nothing
    End Sub
    Private Function UltimoCertificado(ByVal nserie As String) As String
        Dim respuesta As String = ""
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT NCALI_118, MAX(FCALI_118) AS Expr1 FROM  T_CALIBRACION_118 WHERE (TIPO_118 = 1) AND (NSERIE_118 = @D1) AND (FBAJA_118 IS NULL) GROUP BY NCALI_118", cnn)
            adt.Parameters.AddWithValue("D1", nserie)
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                'respuesta = CarpetaCalibracion + nserie + "-" + lector.GetValue(0).ToString + "-" + CDate(lector.GetValue(1)).ToShortDateString.Replace("/", "-") + ".pdf"
                Return "\\SERVER1\CertificadosCalibracion\" + nserie + "-" + lector.GetValue(0).ToString + "-" + CDate(lector.GetValue(1)).ToShortDateString.Replace("/", "-") + ".pdf"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return respuesta
    End Function
    Private Sub llenar_DEPOSITOS(ByVal COD As String)
        Dim tabla As New DataTable
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptador As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE (F_BAJA_003 is NULL) AND (DEPO_003=1 OR ALMA_003=1) AND (NDOC_003 <> @D1) ORDER BY NOMBRE", cnn2)
            adaptador.SelectCommand.Parameters.Add(New SqlParameter("D1", COD))
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptador.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        CBDEPOSITO.DataSource = tabla
        CBDEPOSITO.DisplayMember = "NOMBRE"
        CBDEPOSITO.ValueMember = "NDOC_003"
        CBDEPOSITO.Text = Nothing
    End Sub
    Private Sub llenar_Desde()
        Dim tabla As New DataTable
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptador As New SqlDataAdapter("SELECT M_PERS_003.NDOC_003, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMBRE FROM M_PERS_003 INNER JOIN M_MAQUI_006 ON M_PERS_003.NDOC_003 = M_MAQUI_006.ALMA_006 WHERE (M_PERS_003.F_BAJA_003 IS NULL) OR (M_PERS_003.F_BAJA_003 IS NULL) GROUP BY M_PERS_003.NDOC_003, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 ORDER BY NOMBRE", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptador.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        cbDesde.DataSource = tabla
        cbDesde.DisplayMember = "NOMBRE"
        cbDesde.ValueMember = "NDOC_003"
        cbDesde.Text = Nothing
    End Sub
    Private Sub LLENAR(ByVal NSERIE As String)
        Unamaquina.TOMAR(NSERIE)
        If Unamaquina.LEEREXISTE = True Then
            TipoMaq = New TIPO_DE_MAQUINA(Unamaquina.LEERTIPO)
            lbSerie.Text = Unamaquina.LEERNSERIE
            lbModelo.Text = Unamaquina.LEERMODELO
            lbChapa.Text = Unamaquina.LEERCHAPA
            lbMarca.Text = Unamaquina.LEERMARCA
            lbOBS.Text = Unamaquina.LEEOBS
            lbTipo.Text = TipoMaq.leerDescripcion
            'lbTipo.Text = TipoMaq.leerDescripcion(Unamaquina.LEERTIPO)
            CBESTADO.SelectedValue = Unamaquina.LEERESTADO

            CBESTADO.Enabled = True
            btConfirmar.Visible = True
            TXTOBS.Enabled = True
        Else
            MessageBox.Show("EL NUMERO DE SERIE NO EXISTE", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
    End Sub
    
    Private Function DESC_Estado(ByVal COD As String) As String
        Dim RESP As String = ""
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 17 and C_PARA_802=" + COD.ToString, CNN)
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
    Private Function Generar_Remito(ByVal cmbdesde As String, ByVal cmbpara As String, ByVal lblnserie As String, ByVal lbltipo As String, ByVal lblnchapa As String, ByVal lblmarca As String, ByVal lblmod As String, ByVal cmbestado As String, ByVal txtlobs As String, ByVal lblfecha As String) As List(Of Transpaso)
        Dim lista As New List(Of Transpaso)
        Dim datos As New Transpaso
        datos.propdesde = cmbdesde
        datos.proppara = cmbpara
        datos.propnserie = lblnserie
        datos.proptipo = lbltipo
        'datos.proptipo = lbltipo
        datos.propmarca = lblmarca
        datos.propmodelo = lblmod
        datos.propestado = cmbestado
        datos.propobs = txtlobs
        datos.propfecha = lblfecha
        lista.Add(datos)
        Return lista
    End Function
#End Region
    Private Sub cbDesde_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDesde.SelectedIndexChanged
        If cbDesde.ValueMember <> Nothing And cbDesde.Text <> Nothing Then
            llenar_DEPOSITOS(cbDesde.SelectedValue)
            CBDEPOSITO.Enabled = True
            Button1.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If cbDesde.Text <> Nothing And CBDEPOSITO.Text <> Nothing Then
            Dim PANTALLA As New PALAMA021_BIS
            PANTALLA.Tomar(cbDesde.SelectedValue)
            PANTALLA.ShowDialog()
            If PANTALLA.Leer_respuesta = True Then
                LLENAR(PANTALLA.Leer_serie)
            End If
        Else
            Mi_mensaje.MERRO008()
        End If
    End Sub


    Private Sub btConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btConfirmar.Click
        If CBESTADO.SelectedValue = "4" Then
            If TXTOBS.Text <> Nothing Then
                If Unamaquina.LEERESTADO <> CBESTADO.SelectedValue Then
                    Unamaquina.ActualizarEstado(CBESTADO.SelectedValue)
                End If
                Unamaquina.ActualizarEquipo(CBDEPOSITO.SelectedValue)
                Unamaquina.Grabar_Movimiento(CBDEPOSITO.SelectedValue, TXTOBS.Text, CBESTADO.SelectedValue)
                Mi_mensaje.MADVE001()
                'ACA GENERO REMITO CON LOS DATOS DE LA PANTALLA.
                Dim p As New PALMA021bis2
                p.GetDatosRem(Generar_Remito(cbDesde.Text, CBDEPOSITO.Text, lbSerie.Text, TipoMaq.leerDescripcion, lbChapa.Text, lbMarca.Text, lbModelo.Text, CBESTADO.Text, TXTOBS.Text, Label5.Text))
                p.ShowDialog()
                If TipoMaq.leerCalibracion = 1 Then
                    Dim res As MsgBoxResult = MessageBox.Show("DESEA IMPRIMIR EL CERTIFICADO DE CALIBRACION?", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If res = MsgBoxResult.Yes Then
                        Process.Start(UltimoCertificado(Unamaquina.LEERNSERIE))
                    End If
                End If
                ListaTranspaso.Clear()

                BORRAR()
            Else
                MessageBox.Show("DEBE INGRESAR UNA OBSERVACION", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            If Unamaquina.LEERESTADO <> CBESTADO.SelectedValue Then
                Unamaquina.ActualizarEstado(CBESTADO.SelectedValue)
            End If
            Unamaquina.ActualizarEquipo(CBDEPOSITO.SelectedValue)
            Unamaquina.Grabar_Movimiento(CBDEPOSITO.SelectedValue, TXTOBS.Text, CBESTADO.SelectedValue)
            Mi_mensaje.MADVE001()
            'ACA GENERO REMITO CON LOS DATOS DE LA PANTALLA.
            Dim p As New PALMA021bis2
            p.GetDatosRem(Generar_Remito(cbDesde.Text, CBDEPOSITO.Text, lbSerie.Text, TipoMaq.leerDescripcion, lbChapa.Text, lbMarca.Text, lbModelo.Text, CBESTADO.Text, TXTOBS.Text, Label5.Text))
            p.ShowDialog()
            If TipoMaq.leerCalibracion = 1 Then
                Dim res As MsgBoxResult = MessageBox.Show("DESEA IMPRIMIR EL CERTIFICADO DE CALIBRACION?", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If res = MsgBoxResult.Yes Then
                    Process.Start(UltimoCertificado(Unamaquina.LEERNSERIE))
                End If
            End If
            ListaTranspaso.Clear()
            BORRAR()
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        BORRAR()
    End Sub
End Class