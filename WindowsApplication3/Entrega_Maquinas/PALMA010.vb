Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA010
    Dim alta As Integer
    Dim estado As Integer
    Private mensaje As New Clase_mensaje
    Dim TipoMaquina As TIPO_DE_MAQUINA
    Dim UNA_MAQUINA As New Clase_maquina


    Private Sub PALMA010_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        'TRAIGO LOS TIPOS DE MAQUINAS, LOS DEPOSITOS Y LOS ESTADOS
        llenar_DEPOSITOS()
        LLENAR_ESTADO()
        llenar_tipo()
    End Sub
#Region "LLENADO INICIAL"
    Private Sub llenar_tipo()
        Dim tabla As New DataTable
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptador As New SqlDataAdapter("SELECT TIPO_806, DESC_806 FROM P_MAQUINA_806 ORDER BY DESC_806", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptador.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        CBTIPO.DataSource = tabla
        CBTIPO.DisplayMember = "DESC_806"
        CBTIPO.ValueMember = "TIPO_806"
        CBTIPO.Text = Nothing
    End Sub
    Private Sub llenar_DEPOSITOS()
        Dim tabla As New DataTable
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptador As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE (F_BAJA_003 is NULL) AND (DEPO_003=1 OR ALMA_003=1) ORDER BY NOMBRE", cnn2)
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
#End Region
#Region "FUNCIONES"
    Private Sub Alta_maquina(nserie As String, tipo As String, chapa As String, marca As String, estado As Integer, deposito As String, modelo As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("insert into M_MAQUI_006 (NSERIE_006, TIPO_006, MARCA_006,CHAPA_006,ESTADO_006,ALMA_006,FALTA_006, MODELO_006) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7,@D8)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", nserie))
            adt.Parameters.Add(New SqlParameter("D2", tipo))
            adt.Parameters.Add(New SqlParameter("D3", marca))
            adt.Parameters.Add(New SqlParameter("D4", chapa))
            adt.Parameters.Add(New SqlParameter("D5", estado))
            adt.Parameters.Add(New SqlParameter("D6", deposito))
            adt.Parameters.Add(New SqlParameter("D7", Date.Today))
            adt.Parameters.Add(New SqlParameter("D8", modelo))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub Reactivar(nserie As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("UPDATE M_MAQUI_006 SET FBAJA_006= NULL WHERE NSERIE_006=@D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", nserie))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub BAJA(nserie As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("UPDATE M_MAQUI_006 SET FBAJA_006= @E1 WHERE NSERIE_006=@D1", cnn)
            adt.Parameters.Add(New SqlParameter("E1", Date.Today))
            adt.Parameters.Add(New SqlParameter("D1", nserie))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Function ValidarFechaCalibracion(ByVal numero As String, ByVal TIPO As Integer) As Date
        Dim fecha As Date
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT VTO_118 FROM T_CALIBRACION_118 WHERE NSERIE_118 = @D1 AND FBAJA_118 IS NULL AND TIPO_118=@D2 ", cnn)
            adt.Parameters.Add(New SqlParameter("D1", numero))
            adt.Parameters.Add(New SqlParameter("D2", TIPO))
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
    Private Sub LLENAR(NSERIE As String)

        UNA_MAQUINA.TOMAR(NSERIE)
        If UNA_MAQUINA.LEEREXISTE = True Then
            TXTSERIE.Enabled = False
            txtModelo.Text = UNA_MAQUINA.LEERMODELO
            TXTCHAPA.Text = UNA_MAQUINA.LEERCHAPA
            TXTMARCA.Text = UNA_MAQUINA.LEERMARCA
            txtFAlta.Text = CDate(UNA_MAQUINA.LEEFALTA).ToShortDateString
            CBDEPOSITO.SelectedValue = UNA_MAQUINA.LEERALMACEN
            CBESTADO.SelectedValue = UNA_MAQUINA.LEERESTADO
            CBTIPO.SelectedValue = UNA_MAQUINA.LEERTIPO
            TipoMaquina = New TIPO_DE_MAQUINA(UNA_MAQUINA.LEERTIPO)
            txtObs.Text = UNA_MAQUINA.LEEOBS
            If UNA_MAQUINA.LEERFBAJA <> "" Then
                txtFBaja.Text = CDate(UNA_MAQUINA.LEERFBAJA).ToShortDateString
                TXTCHAPA.Enabled = False
                txtModelo.Enabled = False
                TXTMARCA.Enabled = False
                CBDEPOSITO.Enabled = False
                CBESTADO.Enabled = False
                CBTIPO.Enabled = False
                btAlta.Text = "Reactivar"
                btAlta.Enabled = True
                txtObs.Enabled = False
            Else
                If TipoMaquina.leerCalibracion = 1 Or TipoMaquina.leerVerificacion = 1 Then
                    'tengo que verificar si el estado del instrumento esta activo
                    LinkLabel1.Visible = True
                End If
                txtFBaja.Text = ""
                txtObs.Enabled = True
                TXTCHAPA.Enabled = True
                txtModelo.Enabled = True
                TXTMARCA.Enabled = True
                CBDEPOSITO.Enabled = True
                CBESTADO.Enabled = True
                CBTIPO.Enabled = True
                btAlta.Text = "Dar de Alta"
                btAlta.Enabled = False
                btModificar.Enabled = True
                btBaja.Enabled = True
            End If
        Else
            Dim respuesta As MsgBoxResult = MessageBox.Show("EL NUMERO DE SERIE NO EXISTE,QUIERE CARGARLO?", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If respuesta = MsgBoxResult.Yes Then
                TXTSERIE.Enabled = False
                TXTCHAPA.Enabled = True
                txtModelo.Enabled = True
                txtObs.Enabled = True
                TXTMARCA.Enabled = True
                CBDEPOSITO.Enabled = True
                CBESTADO.Enabled = True
                CBTIPO.Enabled = True
                btAlta.Text = "Dar de Alta"
                btAlta.Enabled = True
                btModificar.Enabled = False
                btBaja.Enabled = False
            End If
        End If
    End Sub
    Private Sub ACTUALIZAR(ByVal NUMERO As String, ByVal CHAPA As String, ByVal MARCA As String, ByVal MODELO As String, ByVal TIPO As String, ByVal ESTADO As Integer, ByVal DEPOSITO As String, ByVal OBS As String)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("UPDATE M_MAQUI_006 SET TIPO_006=@D1, MARCA_006=@D2,MODELO_006=@D3, CHAPA_006=@D4,ESTADO_006=@D5,ALMA_006=@D6, OBS_006 = @D9 WHERE NSERIE_006=@D7", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", TIPO))
            ADT.Parameters.Add(New SqlParameter("D2", MARCA))
            ADT.Parameters.Add(New SqlParameter("D3", MODELO))
            ADT.Parameters.Add(New SqlParameter("D4", CHAPA))
            ADT.Parameters.Add(New SqlParameter("D5", ESTADO))
            ADT.Parameters.Add(New SqlParameter("D6", DEPOSITO))
            ADT.Parameters.Add(New SqlParameter("D9", OBS))
            ADT.Parameters.Add(New SqlParameter("D7", NUMERO))
            ADT.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub

    Private Sub GrabarMovimiento(ByVal tipomov As String)
        Dim nmov As Decimal = UNA_MAQUINA.Obtener_Numero_Mov()
        Dim motivo As String = ""
        If tipomov = 10 Then
            motivo = "ALTA"
        ElseIf tipomov = 11 Then
            motivo = "BAJA"
        Else
            motivo = "MODIFICACION"
        End If
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("INSERT INTO T_MAQ_MOV_119 (NMOV_119, FECHA_119, TIPO_119, NSERIE_119, DEPOEN_119, DEPORE_119, MOTIVO_119) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", nmov))
            comando1.Parameters.Add(New SqlParameter("D2", DateTime.Now))
            comando1.Parameters.Add(New SqlParameter("D3", tipomov))
            comando1.Parameters.Add(New SqlParameter("D4", TXTSERIE.Text))
            comando1.Parameters.Add(New SqlParameter("D5", CBDEPOSITO.SelectedValue))
            comando1.Parameters.Add(New SqlParameter("D6", _usr.Obt_Usr))
            comando1.Parameters.Add(New SqlParameter("D7", motivo))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub


    Private Sub BORRAR()
        LinkLabel1.Visible = False
        txtObs.Text = Nothing
        TXTSERIE.Text = Nothing
        TXTSERIE.Enabled = True
        txtModelo.Text = Nothing
        txtModelo.Enabled = False
        TXTCHAPA.Text = Nothing
        txtFAlta.Text = Nothing
        TXTMARCA.Text = Nothing
        txtFBaja.Text = Nothing
        CBTIPO.Text = Nothing
        CBESTADO.Text = Nothing
        CBDEPOSITO.Text = Nothing
        TXTCHAPA.Enabled = False
        txtObs.Enabled = False
        TXTMARCA.Enabled = False
        CBTIPO.Enabled = False
        CBESTADO.Enabled = False
        CBDEPOSITO.Enabled = False
        btVerificar.Enabled = True
        btAlta.Enabled = False
        btBaja.Enabled = False
        btModificar.Enabled = False
    End Sub
#End Region


    Private Sub LLENAR(NSERIE As String, soc As Integer)
        If soc = 0 Then
            UNA_MAQUINA.TOMAR(NSERIE)
            If UNA_MAQUINA.LEEREXISTE = True Then
                TXTSERIE.Enabled = False
                txtModelo.Text = UNA_MAQUINA.LEERMODELO
                TXTCHAPA.Text = UNA_MAQUINA.LEERCHAPA
                TXTMARCA.Text = UNA_MAQUINA.LEERMARCA
                txtFAlta.Text = CDate(UNA_MAQUINA.LEEFALTA).ToShortDateString
                CBDEPOSITO.SelectedValue = UNA_MAQUINA.LEERALMACEN
                CBESTADO.SelectedValue = UNA_MAQUINA.LEERESTADO
                CBTIPO.SelectedValue = UNA_MAQUINA.LEERTIPO
                TipoMaquina = New TIPO_DE_MAQUINA(UNA_MAQUINA.LEERTIPO)
                txtObs.Text = UNA_MAQUINA.LEEOBS
                If UNA_MAQUINA.LEERFBAJA <> "" Then
                    txtFBaja.Text = CDate(UNA_MAQUINA.LEERFBAJA).ToShortDateString
                    TXTCHAPA.Enabled = False
                    txtModelo.Enabled = False
                    TXTMARCA.Enabled = False
                    CBDEPOSITO.Enabled = False
                    CBESTADO.Enabled = False
                    CBTIPO.Enabled = False
                    btAlta.Text = "Reactivar"
                    btAlta.Enabled = True
                    txtObs.Enabled = False
                Else
                    If TipoMaquina.leerCalibracion = 1 Or TipoMaquina.leerVerificacion = 1 Then
                        'tengo que verificar si el estado del instrumento esta activo
                        LinkLabel1.Visible = True
                    End If
                    txtFBaja.Text = ""
                    txtObs.Enabled = True
                    TXTCHAPA.Enabled = True
                    txtModelo.Enabled = True
                    TXTMARCA.Enabled = True
                    CBDEPOSITO.Enabled = True
                    CBESTADO.Enabled = True
                    CBTIPO.Enabled = True
                    btAlta.Text = "Dar de Alta"
                    btAlta.Enabled = False
                    btModificar.Enabled = True
                    btBaja.Enabled = True
                End If
            Else
                Dim respuesta As MsgBoxResult = MessageBox.Show("EL NUMERO DE SERIE NO EXISTE,QUIERE CARGARLO?", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If respuesta = MsgBoxResult.Yes Then
                    TXTSERIE.Enabled = False
                    TXTCHAPA.Enabled = True
                    txtModelo.Enabled = True
                    txtObs.Enabled = True
                    TXTMARCA.Enabled = True
                    CBDEPOSITO.Enabled = True
                    CBESTADO.Enabled = True
                    CBTIPO.Enabled = True
                    btAlta.Text = "Dar de Alta"
                    btAlta.Enabled = True
                    btModificar.Enabled = False
                    btBaja.Enabled = False
                End If
            End If
        Else
            UNA_MAQUINA.TOMARCHAPA(NSERIE)
            If UNA_MAQUINA.LEEREXISTE = True Then
                TXTSERIE.Enabled = False
                TXTCHAPA.Enabled = False
                txtModelo.Text = UNA_MAQUINA.LEERMODELO
                TXTCHAPA.Text = UNA_MAQUINA.LEERCHAPA
                TXTMARCA.Text = UNA_MAQUINA.LEERMARCA
                TXTSERIE.Text = UNA_MAQUINA.LEERNSERIE
                txtFAlta.Text = CDate(UNA_MAQUINA.LEEFALTA).ToShortDateString
                CBDEPOSITO.SelectedValue = UNA_MAQUINA.LEERALMACEN
                CBESTADO.SelectedValue = UNA_MAQUINA.LEERESTADO
                CBTIPO.SelectedValue = UNA_MAQUINA.LEERTIPO
                TipoMaquina = New TIPO_DE_MAQUINA(UNA_MAQUINA.LEERTIPO)
                txtObs.Text = UNA_MAQUINA.LEEOBS
                If UNA_MAQUINA.LEERFBAJA <> "" Then
                    txtFBaja.Text = CDate(UNA_MAQUINA.LEERFBAJA).ToShortDateString
                    TXTCHAPA.Enabled = False
                    txtModelo.Enabled = False
                    TXTMARCA.Enabled = False
                    CBDEPOSITO.Enabled = False
                    CBESTADO.Enabled = False
                    CBTIPO.Enabled = False
                    btAlta.Text = "Reactivar"
                    btAlta.Enabled = True
                    txtObs.Enabled = False
                Else
                    If TipoMaquina.leerCalibracion = 1 Or TipoMaquina.leerVerificacion = 1 Then
                        'tengo que verificar si el estado del instrumento esta activo
                        LinkLabel1.Visible = True
                    End If
                    txtFBaja.Text = ""
                    txtObs.Enabled = True
                    TXTCHAPA.Enabled = True
                    txtModelo.Enabled = True
                    TXTMARCA.Enabled = True
                    CBDEPOSITO.Enabled = True
                    CBESTADO.Enabled = True
                    CBTIPO.Enabled = True
                    btAlta.Text = "Dar de Alta"
                    btAlta.Enabled = False
                    btModificar.Enabled = True
                    btBaja.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        BORRAR()
    End Sub


    Private Sub btVerificar_Click(sender As System.Object, e As System.EventArgs) Handles btVerificar.Click
        If IsNothing(TXTSERIE.Text) = False Then
            LLENAR(TXTSERIE.Text)
        Else
            mensaje.MERRO008()
        End If
    End Sub

    Private Sub btAlta_Click(sender As System.Object, e As System.EventArgs) Handles btAlta.Click
        If txtFBaja.Text = "" Then
            'damos de alta el nuevo elemento
            If CBTIPO.SelectedValue <> Nothing And CBTIPO.SelectedValue <> Nothing And CBESTADO.SelectedValue <> Nothing And TXTCHAPA.Text <> Nothing And TXTMARCA.Text <> Nothing Then
                Dim tipoMaq As New TIPO_DE_MAQUINA(CBTIPO.SelectedValue)
                If tipoMaq.leerCalibracion = 1 Or tipoMaq.leerPlazoVerif = 1 Then
                    If tipoMaq.leerCalibracion = 1 Then
                        Dim p As New PALMA011
                        p.TOMAR(CBTIPO.SelectedValue, TXTSERIE.Text, 1)
                        p.ShowDialog()
                        If p.LEERCONFIRMACION = True Then
                            Alta_maquina(TXTSERIE.Text, CBTIPO.SelectedValue, TXTCHAPA.Text, TXTMARCA.Text, CBESTADO.SelectedValue, CBDEPOSITO.SelectedValue, txtModelo.Text)
                            GrabarMovimiento(10)
                            mensaje.MADVE001()
                            BORRAR()
                        Else
                            MessageBox.Show("NO SE CARGO EL VENCIMIENTO DE LA CALIBRACION")
                        End If
                    Else
                        Dim p As New PALMA011
                        p.TOMAR(CBTIPO.SelectedValue, TXTSERIE.Text, 2)
                        p.ShowDialog()
                        If p.LEERCONFIRMACION = True Then
                            Alta_maquina(TXTSERIE.Text, CBTIPO.SelectedValue, TXTCHAPA.Text, TXTMARCA.Text, CBESTADO.SelectedValue, CBDEPOSITO.SelectedValue, txtModelo.Text)
                            GrabarMovimiento(10)
                            mensaje.MADVE001()
                            BORRAR()
                        Else
                            MessageBox.Show("NO SE CARGO EL VENCIMIENTO DE LA CALIBRACION")
                        End If
                    End If


                Else
                    Alta_maquina(TXTSERIE.Text, CBTIPO.SelectedValue, TXTCHAPA.Text, TXTMARCA.Text, CBESTADO.SelectedValue, CBDEPOSITO.SelectedValue, txtModelo.Text)
                    GrabarMovimiento(10)
                    mensaje.MADVE001()
                    BORRAR()
                End If
            Else
                mensaje.MERRO008()
            End If
        Else
            'reactivamos el elemento
            Dim RESULTADO As MsgBoxResult = MessageBox.Show("ESTA SEGURO QUE DESEA REACTIVAR", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If RESULTADO = MsgBoxResult.Yes Then
                Reactivar(TXTSERIE.Text)
                mensaje.MADVE001()
                BORRAR()
            End If
        End If
    End Sub

    Private Sub btBaja_Click(sender As System.Object, e As System.EventArgs) Handles btBaja.Click
        Dim RESULTADO As MsgBoxResult = MessageBox.Show("ESTA SEGURO QUE DESEA DAR DE BAJA", "CONSULTA", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If RESULTADO = MsgBoxResult.Yes Then
            BAJA(TXTSERIE.Text)
            GrabarMovimiento(11)
            mensaje.MADVE001()
            BORRAR()
        End If
    End Sub

    Private Sub btModificar_Click(sender As System.Object, e As System.EventArgs) Handles btModificar.Click
        If TXTCHAPA.Text <> UNA_MAQUINA.LEERCHAPA Or txtModelo.Text <> UNA_MAQUINA.LEERMODELO Or TXTMARCA.Text <> UNA_MAQUINA.LEERMARCA Or CBDEPOSITO.SelectedValue <> UNA_MAQUINA.LEERALMACEN Or CBESTADO.SelectedValue <> UNA_MAQUINA.LEERESTADO Or CBTIPO.SelectedValue <> UNA_MAQUINA.LEERTIPO Or txtObs.Text <> UNA_MAQUINA.LEEOBS Then
            'REALIZO LA MODIFICACION
            ACTUALIZAR(TXTSERIE.Text, TXTCHAPA.Text, TXTMARCA.Text, txtModelo.Text, CBTIPO.SelectedValue, CBESTADO.SelectedValue, CBDEPOSITO.SelectedValue, txtObs.Text)
            GrabarMovimiento(12)
            mensaje.MADVE001()
            BORRAR()
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim p As New PALMA016
        p.TOMAR(TXTSERIE.Text, UNA_MAQUINA.LEERTIPO)
        p.ShowDialog()
    End Sub

    Private Sub TXTSERIE_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TXTSERIE.DoubleClick
        Dim PANTALLA As New PALMA010BIS
        PANTALLA.ShowDialog()
        If PANTALLA.llerrespuesta = True Then
            TXTSERIE.Text = PANTALLA.leernserie
            LLENAR(TXTSERIE.Text)
        End If
    End Sub

    Private Sub BtnVerificarnchapa_Click(sender As Object, e As EventArgs) Handles btnVerificarnchapa.Click
        If IsNothing(TXTCHAPA) = False Then
            LLENAR(TXTCHAPA.Text, 1)
        Else
            mensaje.MERRO008()
        End If
    End Sub

    Private Sub Btnborrarnchapa_Click(sender As Object, e As EventArgs) Handles btnborrarnchapa.Click
        BORRAR()
    End Sub

    Private Sub TXTCHAPA_TextChanged(sender As Object, e As EventArgs) Handles TXTCHAPA.TextChanged

    End Sub

    Private Sub TXTCHAPA_DoubleClick(sender As Object, e As EventArgs) Handles TXTCHAPA.DoubleClick
        Cursor.Current = Cursors.WaitCursor
        Dim PANTALLA As New PALMA010BIS
        PANTALLA.ShowDialog()
        If PANTALLA.llerrespuesta = True Then
            TXTSERIE.Text = PANTALLA.leernserie
            LLENAR(TXTSERIE.Text, 0)
            Cursor.Current = Cursors.Arrow
        End If
    End Sub
End Class