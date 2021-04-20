Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO

Public Class PCONF001
    Private oc As New Class_OC
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen
    Private nremito As Decimal
   
    Private Sub PCONF001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenarDW1()
    End Sub
    Private Sub llenarDW1()
        Dim d1 As String = ""
        Dim d2 As String = ""
        Dim d3 As String = ""
        Dim d4 As String = ""
        Dim d5 As String = ""
        DataGridView1.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.USERG_105, dbo.M_PROV_005.RAZO_005, dbo.T_C_OC_105.MONTO_105 FROM dbo.T_C_OC_105 INNER JOIN dbo.M_PROV_005 ON dbo.T_C_OC_105.C_PROV_105 = dbo.M_PROV_005.CUIT_005 WHERE (dbo.T_C_OC_105.TIPO_OC_105 = 1) AND (dbo.T_C_OC_105.ESTA_105 = 1)", con1)
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            d1 = lector1.GetValue(0).ToString.PadLeft(8, "0")
            d2 = lector1.GetDateTime(1).ToShortDateString
            d3 = MAIN.OBT_NOM_USER(lector1.GetValue(2))
            d4 = lector1.GetValue(3)
            If IsDBNull(lector1.GetValue(4)) Then
                d5 = "SIN VALOR"
            Else
                d5 = lector1.GetValue(4)
            End If
            Me.DataGridView1.Rows.Add(d1, d2, d3, d4, d5)
        End While
        Me.DataGridView2.Rows.Clear()
        'ciero la conexion
        con1.Close()
    End Sub

    Private Sub DataGridView1_dobleclic(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.RowCount <> 0 Then
            nremito = Me.DataGridView1.Item(0, Me.DataGridView1.CurrentRow.Index).Value
            llenarDW2_estado1(nremito)
        End If


    End Sub

    Private Sub llenarDW2_estado1(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim u As String
        Dim PU As Decimal = 0
        Dim PT As Decimal = 0
        DataGridView2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.M_MATE_002.UNID_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106, dbo.T_D_OC_106.PRECIO_C_106 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 = @D1)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremi))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        While lector1.Read
            mate = lector1.GetValue(0)
            desc = lector1.GetValue(1)
            u = lector1.GetValue(2)
            soli = lector1.GetValue(3)
            If IsDBNull(lector1.GetValue(4)) Then
                PU = 0
            Else
                PU = lector1.GetValue(4)
            End If
            PT = FormatNumber(PU * soli, 2)

            Me.DataGridView2.Rows.Add(mate, desc, u, soli, PU, PT)
        End While
        'ciero la conexion
        con1.Close()
        CheckBox1.Checked = False
        If DataGridView2.RowCount <> 0 Then
            Button3.Enabled = True

        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        For i = 0 To Me.DataGridView2.RowCount - 1

            If Me.DataGridView2.Item(6, i).Value <> CheckBox1.Checked Then
                Me.DataGridView2.Item(6, i).Value = CheckBox1.Checked
            End If
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim aprobar = False
        For i = 0 To Me.DataGridView2.RowCount - 1
            If Me.DataGridView2.Item(6, i).Value = True Then
                aprobar = True
            End If
        Next
        If aprobar = True Then
            APROBAR_ITME_OC()
            Dim PANTALLA As New PMAIL001
            Dim direccion As String
            PANTALLA.ShowDialog()
            direccion = PANTALLA.DIRECCIONMAIL
            ENVIAR_MENSAJE(nremito, direccion, "APROBADA")
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView2.Rows.Clear()
            llenarDW1()
            Button3.Enabled = False
            CheckBox1.Checked = False
            mensaje.MADVE001()
        Else
            Dim res As DialogResult
            res = MessageBox.Show("Esta por rechazar la OC N: " + nremito.ToString.PadLeft(8, "0") + vbCrLf + "¿Desea continuar?", "MADVE006", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If res = System.Windows.Forms.DialogResult.Yes Then
                Dim PANTALLA As New PMAIL001
                Dim direccion As String
                PANTALLA.ShowDialog()
                direccion = PANTALLA.DIRECCIONMAIL
                oc.aprobar_oc(nremito, 5, Date.Now, _usr.Obt_Usr)
                ENVIAR_MENSAJE(nremito, direccion, "DESAPROBADA")
                Me.DataGridView1.Rows.Clear()
                Me.DataGridView2.Rows.Clear()
                llenarDW1()
                Button3.Enabled = False
                mensaje.MADVE001()
            End If
        End If
    End Sub
    Public Sub APROBAR_ITME_OC()
        For i = 0 To Me.DataGridView2.RowCount - 1
            If Me.DataGridView2.Item(4, i).Value = True Then
                oc.Aprobar_item_OC(nremito, Me.DataGridView2.Item(0, i).Value, Me.DataGridView2.Item(4, i).Value)
            End If
        Next
        oc.aprobar_oc(nremito, 3, Date.Now, _usr.Obt_Usr)
    End Sub

    Public Sub ENVIAR_MENSAJE(ByVal num As Decimal, ByVal direccion As String, ByVal ESTADO As String)
        Dim _BODYMAIL As String = "Se ha " + ESTADO.ToString + " la siguiente OC/Peticion: " + vbCrLf + "Orden de compra N: " + CStr(num).PadLeft(8, "0").ToString + vbCrLf + vbCrLf + "CODIGO|DESCRIPCION                    .|U|CANTIDAD" + vbCrLf
        For I = 0 To DataGridView2.RowCount - 1
            If Me.DataGridView2.Item(4, I).Value = True Then
                _BODYMAIL = _BODYMAIL + DataGridView2.Item(0, I).Value + "|" + DataGridView2.Item(1, I).Value.PadRight(30, " ") + "|" + DataGridView2.Item(2, I).Value.PadRight(3, " ") + "|" + DataGridView2.Item(3, I).Value.ToString + vbCrLf
            End If
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
        _mensage.From = New System.Net.Mail.MailAddress("almacensis@exgadetsa.com.ar", "Cambio de estado de OC" + Date.Now.ToShortDateString, System.Text.Encoding.UTF8)
        _mensage.Subject = "NOTIFICACION DE CAMBIO DE ESTADO DE OC N: " + num.ToString.PadLeft(10, "0") + " - " + Date.Now.ToLongDateString
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
    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        If DataGridView2.RowCount <> 0 Then
            Dim PANTALLA As New PCONF003
            PANTALLA.GRABARDATO(DataGridView2.Item(0, DataGridView2.CurrentRow.Index).Value)
            PANTALLA.ShowDialog()
        End If
    End Sub
End Class