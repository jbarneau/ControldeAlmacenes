Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA041
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Clas_Medidor As New Clas_Medidor
    Private med_rettirar As New Clase_med_retirar
    Private cant As Integer = 0
    Private DT_remito As New DataTable
    Private remito As Decimal
    Private fecha As Date
    Private resumen As New DataTable
    Private DS_deposito1 As New DataSet
    Private DS_deposito2 As New DataSet
    Private _DEPOSITO1 As String
    Private _DEPOSITO2 As String

    Private Sub PALMA041_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
       
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            cbdesde.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO1 = _usr.Obt_Almacen
            'escribo el nombre del deposito
            cbdesde.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO1)
            cbPara.Enabled = True
            cbPara.Focus()
            LLENAR(_DEPOSITO1)
            cbdesde.Enabled = False
        Else
            cbdesde.Enabled = True
            'lleno el combo box del deposito
            llenar_DS_DEPOSITO1()
            LLENAR_CB_DEPOSITO1()
            cbdesde.Focus()
            'desactivo el combobox del equitoi
            cbPara.Enabled = False
        End If
        llenar_DS_DEPOSITO2()
        LLENAR_CB_DEPOSITO2()
    End Sub
#Region "########### FUNCIONES ###############"
    Private Sub LLENAR(ByVal DEPOSITO As String)
        ListView1.Items.Clear()
        Dim DATA As New DataTable
        Dim renglon As New ListViewItem
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CAJON_113, COUNT(NSERI_113) AS CANTIDAD FROM T_MED_DEVO_113 WHERE (DEPOSI_113=@D1) AND (ESTADO_113 = 1)  GROUP BY CAJON_113", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", DEPOSITO))
        adaptadaor.Fill(DATA)
        cnn2.Close()
        For i = 0 To DATA.Rows.Count - 1
            'MessageBox.Show(DATA.Rows(i).Item(0).ToString + " : " + DATA.Rows(i).Item(1).ToString)
            renglon = New ListViewItem(DATA.Rows(i).Item(0).ToString)
            renglon.SubItems.Add(DATA.Rows(i).Item(1).ToString)
            ListView1.Items.Add(renglon)
        Next
    End Sub
    Private Sub llenar_DS_DEPOSITO1()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 AND F_BAJA_003 is NULL order by NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito1, "M_PERS_003")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_DEPOSITO2()
        DS_deposito2.Clear()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 AND F_BAJA_003 is NULL order by NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito2, "M_PERS_003")
        cnn2.Close()
        Dim INDICE As Integer = 0
        For I = 0 To DS_deposito2.Tables("M_PERS_003").Rows.Count - 1
            If DS_deposito2.Tables("M_PERS_003").Rows(I).Item("NDOC_003").ToString() = _DEPOSITO1 Then
                INDICE = I
            End If
        Next
        DS_deposito2.Tables("M_PERS_003").Rows.RemoveAt(INDICE)
    End Sub
    Private Sub LLENAR_CB_DEPOSITO1()
        cbdesde.DataSource = DS_deposito1.Tables("M_PERS_003")
        cbdesde.DisplayMember = "NOMB_003"
        cbdesde.ValueMember = "NDOC_003"
        cbdesde.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_DEPOSITO2()
        cbPara.DataSource = DS_deposito2.Tables("M_PERS_003")
        cbPara.DisplayMember = "NOMB_003"
        cbPara.ValueMember = "NDOC_003"
        cbPara.Text = Nothing
    End Sub

    Private Function NOMBRE_DEPOSITO(ByVal str As String) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select NOMB_003 FROM M_PERS_003 WHERE NDOC_003 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function

#End Region

    


    Private Sub cbdesde_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbdesde.SelectedIndexChanged
        If cbdesde.ValueMember <> Nothing And cbdesde.Text <> Nothing Then
            _DEPOSITO1 = cbdesde.SelectedValue
            cbPara.Enabled = True
            llenar_DS_DEPOSITO2()
            LLENAR_CB_DEPOSITO2()
            cbPara.DropDownStyle = ComboBoxStyle.DropDownList
            LLENAR(_DEPOSITO1)
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Dim CAN As Integer = 0
        For I = 0 To ListView1.Items.Count - 1
            If ListView1.Items(I).Selected = True Then
                'MessageBox.Show()
                CAN = CAN + CInt(ListView1.Items(I).SubItems(1).Text)
            End If
        Next
        TextBox1.Text = CAN
        If CAN <> 0 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If cbdesde.Text <> Nothing And cbPara.Text <> Nothing And TextBox1.Text <> 0 Then
            Dim pantalla As New PMAIL001
            Dim nmovimiento As Decimal = med_rettirar.Obtener_Numero_Mov
            Dim FECHA As Date = Date.Now
            Dim MEDIDOR As Decimal

            For I = 0 To ListView1.Items.Count - 1
                If ListView1.Items(I).Selected = True Then
                    llenar_dt_remito(_DEPOSITO1, ListView1.Items(I).Text)
                    med_rettirar.GRABAR_TRANSFERENCIA(ListView1.Items(I).Text, nmovimiento, FECHA, _DEPOSITO1, _DEPOSITO2)
                End If
            Next
            For I = 0 To DT_remito.Rows.Count - 1
                MEDIDOR = DT_remito.Rows(I).Item(0)
                med_rettirar.GRABAR_TRANS(MEDIDOR, nmovimiento, FECHA, _usr.Obt_Usr, 3)
                med_rettirar.actualizar_med_trans(3, MEDIDOR)
            Next
            Dim direccion As String
            pantalla.ShowDialog()
            direccion = pantalla.DIRECCIONMAIL
            ENVIAR_MENSAJE(nmovimiento, direccion)
            borrar()

        End If
    End Sub
    Public Sub ENVIAR_MENSAJE(ByVal num As Decimal, ByVal direccion As String)
        Dim _BODYMAIL As String = "Se ha cargado la siguiente transferencia entre depositos:" + vbCrLf + "Desde: " + cbdesde.Text + " A: " + cbPara.Text + vbCrLf + "Remito N: " + CStr(num).PadLeft(8, "0").ToString + vbCrLf + vbCrLf + "LOTE|CANTIDAD                    .|U|CANTIDAD" + vbCrLf
        For I = 0 To ListView1.Items.Count - 1
            _BODYMAIL = _BODYMAIL + ListView1.Items(I).Text + "|" + ListView1.Items(I).SubItems(0).Text + vbCrLf
        Next

        'Dim destinatario As String = grupomail1.ToString
        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential(MAIN.mail, MAIN.passmail)
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add(direccion.ToString)
        _mensage.From = New System.Net.Mail.MailAddress("almacensis@exgadetsa.com.ar", "Transferencia entre almacen " + Date.Now.ToShortDateString, System.Text.Encoding.UTF8)
        _mensage.Subject = "NOTIFICACION DE TRANSFERENCIA N: " + num.ToString.PadLeft(10, "0") + " Realizada " + Date.Now.ToLongDateString
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
    Private Sub borrar()
        DT_remito.Clear()
        resumen.Clear()
        ListView1.Items.Clear()
        TextBox1.Text = 0
        cbPara.Enabled = False
        cbPara.Text = Nothing
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            cbdesde.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO1 = _usr.Obt_Almacen
            'escribo el nombre del deposito
            cbdesde.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO1)
            cbPara.Enabled = True
            cbPara.Focus()
            LLENAR(_DEPOSITO1)
            cbdesde.Enabled = False
        Else
            cbdesde.Enabled = True
            'lleno el combo box del deposito
            llenar_DS_DEPOSITO1()
            LLENAR_CB_DEPOSITO1()
            cbdesde.Focus()
            'desactivo el combobox del equitoi
            cbPara.Enabled = False
        End If
        llenar_DS_DEPOSITO2()
        LLENAR_CB_DEPOSITO2()
    End Sub
    Private Sub llenar_dt_remito(ByVal deposito As String, ByVal cajon As String)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NSERI_113, CMATE_113,CONTRATO_113 FROM T_MED_DEVO_113 WHERE (DEPOSI_113=@D1) AND (ESTADO_113 = 1) AND (CAJON_113=@D2)", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", deposito))
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D2", cajon))
        adaptadaor.Fill(DT_remito)
        cnn2.Close()
    End Sub

    Private Sub cbPara_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbPara.SelectedIndexChanged
        If cbPara.ValueMember <> Nothing And cbdesde.Text <> Nothing Then
            _DEPOSITO2 = cbPara.SelectedValue
        End If
    End Sub

   


    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        borrar()
    End Sub
End Class