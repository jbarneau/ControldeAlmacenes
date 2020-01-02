Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA014
    Private Mensaje As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private _Deposito As String = "0"
    Private _Deposito2 As String = "0"
    Private _fecha As Date
    Private _REMITO As Decimal = 0

    Private Sub PALMA014_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        If _usr.Obt_Almacen <> "0" Then
            CB_Deposito1.DropDownStyle = ComboBoxStyle.DropDown
            _Deposito = _usr.Obt_Almacen
            CB_Deposito1.Text = Metodos.NOMBRE_DEPOSITO(_usr.Obt_Almacen)
            CB_Deposito1.Enabled = False
            llenar_dsSD(CStr(_Deposito))
        Else

            llenar_DS_DEPOSITO1()
            CB_Deposito1.DropDownStyle = ComboBoxStyle.DropDownList
        End If
    End Sub












    '############################FUNCIONES######################################################################
    Private Sub llenar_dsSD(ByVal depos As String)
        Me.DataGridView2.Rows.Clear()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT NTRANF111,FECH111,ALMAE111 FROM C_TRANF_111 WHERE ALMAR111=@D1 order by NTRANF111", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", depos))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            Me.DataGridView2.Rows.Add(lector1.GetValue(0), lector1.GetDateTime(1), Metodos.NOMBRE_DEPOSITO(lector1.GetValue(2)), lector1.GetValue(2))
        End While
        con1.Close()
    End Sub
    Private Sub llenar_DS_DEPOSITO1()
        'CONECTO LA BASE
        Dim ds_deposito1 As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 AND F_BAJA_003 is NULL order by NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito1, "M_PERS_003")
        cnn2.Close()
        CB_Deposito1.DataSource = ds_deposito1.Tables("M_PERS_003")
        CB_Deposito1.DisplayMember = "NOMB_003"
        CB_Deposito1.ValueMember = "NDOC_003"
        CB_Deposito1.Text = Nothing
    End Sub
    
    Private Sub llenar_dw2(ByVal nremito As Decimal)
        Me.DataGridView1.Rows.Clear()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT TEMP_TRANSFERENCIA.C_MATE_110, M_MATE_002.DESC_002, M_MATE_002.UNID_002, TEMP_TRANSFERENCIA.CANT_110 FROM TEMP_TRANSFERENCIA INNER JOIN M_MATE_002 ON TEMP_TRANSFERENCIA.C_MATE_110 = M_MATE_002.CMATE_002 WHERE (TEMP_TRANSFERENCIA.N_REMI_110 = @D1) ORDER BY M_MATE_002.DESC_002", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            Me.DataGridView1.Rows.Add(lector1.GetValue(0), lector1.GetValue(1), lector1.GetValue(2), lector1.GetValue(3))
        End While
        con1.Close()
    End Sub
    
    Private Sub modificar_medidores_rechazo(ByVal nremito As Decimal)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT NSERIE,CODMATE FROM TEMP_TRANS_MED WHERE N_REMI = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            Medidor.MODIFICAR_MEDIDOR_ESTADO_1(lector1.GetValue(0), lector1.GetValue(1), _Deposito2, _fecha, _usr.Obt_Usr)
        End While
        con1.Close()
        Metodos.Eliminar_med_transf(nremito)
    End Sub
    Private Sub grabar_trans_rechazo(ByVal nremito As Decimal)
        For I = 0 To DataGridView1.Rows.Count - 1
            Metodos.Increpmentar_Stock_Material(DataGridView1.Item(0, I).Value, _Deposito2, DataGridView1.Item(3, I).Value, 1)
            Metodos.Grabar_Trans(_REMITO, Date.UtcNow, DataGridView1.Item(0, I).Value, _Deposito2, _Deposito, 8, _fecha, 0, ComboBox1.Text, 0, DataGridView1.Item(3, I).Value, 0, _usr.Obt_Usr, "", "")
        Next
        Metodos.Eliminar_D_Trans(nremito)
        Metodos.Elimino_C_Trans(nremito)
    End Sub
    Private Sub modificar_medidores_confirmado(ByVal nremito As Decimal)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT NSERIE,CODMATE FROM TEMP_TRANS_MED WHERE N_REMI = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            Medidor.MODIFICAR_MEDIDOR_ESTADO_1(lector1.GetValue(0), lector1.GetValue(1), _Deposito, _fecha, _usr.Obt_Usr)
            Medidor.Grabar_Mov_Medi(lector1.GetValue(0), lector1.GetValue(1), nremito, _fecha, _Deposito2, _Deposito)
        End While
        con1.Close()
        Metodos.Eliminar_med_transf(nremito)
    End Sub
    Private Sub grabar_trans_confirmado(ByVal nremito As Decimal)
        For I = 0 To DataGridView1.Rows.Count - 1
            Metodos.Increpmentar_Stock_Material(DataGridView1.Item(0, I).Value, _Deposito, DataGridView1.Item(3, I).Value, 1)
            Metodos.Grabar_Trans(_REMITO, Date.UtcNow, DataGridView1.Item(0, I).Value, _Deposito2, _Deposito, 3, _fecha, 0, ComboBox1.Text, 0, DataGridView1.Item(3, I).Value, 0, _usr.Obt_Usr, "", "")
        Next
        Metodos.Eliminar_D_Trans(nremito)
        Metodos.Elimino_C_Trans(nremito)
    End Sub
    Public Sub ENVIAR_MENSAJE(ByVal num As Decimal, ByVal TIPO As String, ByVal direccion As String)
        Dim _BODYMAIL As String = "Se ha " + TIPO.ToString + " la siguiente transferencia entre depositos:" + vbCrLf + "Desde: " + Metodos.NOMBRE_DEPOSITO(_Deposito2).ToString + vbCrLf + " A: " + Metodos.NOMBRE_DEPOSITO(_Deposito).ToString + vbCrLf + "Remito N: " + CStr(num).PadLeft(8, "0").ToString
       
        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential(MAIN.mail, MAIN.passmail)
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add(direccion.ToString)
        _mensage.From = New System.Net.Mail.MailAddress("almacensis@exgadetsa.com.ar", "Transferencia entre almacen " + Date.Now.ToShortDateString, System.Text.Encoding.UTF8)
        _mensage.Subject = "NOTIFICACION DE TRANSFERENCIA N: " + num.ToString.PadLeft(10, "0") + TIPO.ToString + Date.Now.ToLongDateString
        _mensage.SubjectEncoding = System.Text.Encoding.UTF8
        _mensage.Body = _BODYMAIL.ToString
        _mensage.BodyEncoding = System.Text.Encoding.UTF8
        _mensage.Priority = System.Net.Mail.MailPriority.High
        _mensage.IsBodyHtml = False
        'enviar
        Try
            _Smtp.Send(_mensage)
        Catch ex As System.Net.Mail.SmtpException
            MessageBox.Show("NO SE PUDO ENVIAR EL MENSAJE  ERROR")
        End Try
    End Sub

    '################################BOTONES####################################################################
    'BOTON DE SALIR
    
    'BOTON DE RECHAZO
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text <> Nothing Then
            modificar_medidores_rechazo(_REMITO)
            grabar_trans_rechazo(_REMITO)
            Mensaje.MADVE001()
            DataGridView1.Rows.Clear()
            DataGridView2.Rows.Clear()
            ComboBox1.Text = Nothing
            Dim PANTALLA As New PMAIL001
            Dim direccion As String
            PANTALLA.ShowDialog()
            direccion = PANTALLA.DIRECCIONMAIL
            ENVIAR_MENSAJE(_REMITO, " RECHAZADO ", direccion)
            If _usr.Obt_Almacen <> "0" Then
                llenar_dsSD(CStr(_Deposito))
            Else
                CB_Deposito1.Text = Nothing
            End If
        Else
            Mensaje.MERRO006()
            ComboBox1.Focus()
        End If
    End Sub
    'BOTON DE CONFIRMAR
    Private Sub B_Entregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Entregar.Click
        modificar_medidores_confirmado(_REMITO)
        grabar_trans_confirmado(_REMITO)
        ComboBox1.Text = Nothing
        Dim PANTALLA As New PMAIL001
        Dim direccion As String
        PANTALLA.ShowDialog()
        direccion = PANTALLA.DIRECCIONMAIL
        ENVIAR_MENSAJE(_REMITO, " CONFIRMADO ", direccion)
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        If _usr.Obt_Almacen <> "0" Then
            llenar_dsSD(CStr(_Deposito))
        Else
            CB_Deposito1.Text = Nothing
        End If
        Mensaje.MADVE001()
    End Sub

    '#################################ACCIONES####################################################################
   
    'SELECCION DE DEPOSITO
    Private Sub CB_Deposito1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Deposito1.SelectedIndexChanged
        If CB_Deposito1.ValueMember <> Nothing Then
            _Deposito = CB_Deposito1.SelectedValue
            If CB_Deposito1.Text <> Nothing Then
                llenar_dsSD(CStr(_Deposito))
            End If
        End If
    End Sub
    'DOBLE CLICK PARA LLENAR EL DETALLE
    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        If DataGridView2.Rows.Count <> 0 Then
            _REMITO = Me.DataGridView2.Item(0, DataGridView2.CurrentRow.Index).Value
            _Deposito2 = Me.DataGridView2.Item(3, DataGridView2.CurrentRow.Index).Value
            _fecha = Me.DataGridView2.Item(1, DataGridView2.CurrentRow.Index).Value
            llenar_dw2(_REMITO)
        End If

    End Sub
    'DOBLE CLICK PARA CER LOS MEDIDORES DE LA TRANSFERENCIA
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Medidor.Es_Serializado(Me.DataGridView1.Item(0, Me.DataGridView1.CurrentRow.Index).Value) = True Then
            Dim PANTALLA As New PALMA014BIS
            PANTALLA.Grabar_datos(_REMITO, Me.DataGridView1.Item(0, Me.DataGridView1.CurrentRow.Index).Value)
            PANTALLA.ShowDialog()
        End If
    End Sub

    
   
End Class