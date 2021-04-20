Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PPETI004
    Private oc As New Class_OC
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen
    Private medidores As New Clas_Medidor
    Private _DEPOSITO As String
    Private _PETICION As Decimal
    Private _OC As Decimal
    Private _NREMITO As Decimal
    Private _FECHA As Date


    Private Sub PPETI004_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        DateTimePicker1.MaxDate = Date.Today.ToShortDateString
        DateTimePicker1.MinDate = DateAdd(DateInterval.Month, -1, Date.Today)
        DateTimePicker1.Value = Date.Today.ToShortDateString
        llenar_CB_PETICION()
        If _usr.Obt_Almacen <> "0" Then
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO = _usr.Obt_Almacen
            ComboBox3.Enabled = False
            ComboBox3.Text = metodos.NOMBRE_DEPOSITO(_DEPOSITO).ToString
            CB_PETICION.Enabled = True
        Else
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            llenar_CB_DEPOSITO()
            CB_PETICION.Enabled = False

        End If


    End Sub

   

    Private Sub llenar_CB_DEPOSITO()
        Dim DS As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 and F_BAJA_003 IS NULL ORDER BY NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS, "M_PERS_003")
        cnn2.Close()
        ComboBox3.DataSource = DS.Tables("M_PERS_003")
        ComboBox3.DisplayMember = "NOMB_003"
        ComboBox3.ValueMember = "NDOC_003"
        ComboBox3.Text = Nothing

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.ValueMember <> Nothing And ComboBox3.Text <> Nothing Then
            _DEPOSITO = ComboBox3.SelectedValue
            CB_PETICION.Enabled = True
        End If
    End Sub
    Private Sub llenar_CB_PETICION()
        Dim DS As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT N_OC_105, N_PETI_105 FROM T_C_OC_105 WHERE TIPO_OC_105 = 2 AND ESTA_105 = 3 ORDER BY N_OC_105", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS, "T_C_OC_105")
        cnn2.Close()
        CB_PETICION.DataSource = DS.Tables("T_C_OC_105")
        CB_PETICION.DisplayMember = "N_PETI_105"
        CB_PETICION.ValueMember = "N_OC_105"
        CB_PETICION.Text = Nothing
    End Sub




    Private Sub CB_PETICION_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_PETICION.SelectedIndexChanged
        If CB_PETICION.ValueMember <> Nothing And CB_PETICION.Text <> Nothing Then

            _OC = CB_PETICION.SelectedValue
            LLENARDW(_OC)
            If Me.DataGridView2.RowCount <> 0 Then
                Button3.Enabled = True
            End If
        End If
    End Sub

    Private Sub LLENARDW(ByVal NPETI As Decimal)

        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim unid As String = 0
        DataGridView2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106, dbo.M_MATE_002.UNID_002 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 = @D1) and (dbo.T_D_OC_106.CONF_106 = 1) ", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NPETI))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        While lector1.Read
            mate = lector1.GetValue(0)
            desc = lector1.GetValue(1)
            soli = lector1.GetValue(2)
            ent = lector1.GetValue(3)
            unid = lector1.GetValue(4)
            Me.DataGridView2.Rows.Add(mate, desc, unid, soli, ent, 0, soli - ent)
        End While
        'ciero la conexion
        con1.Close()
    End Sub

    Private Sub DataGridView2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        Try
            Dim indice As Integer = Me.DataGridView2.CurrentRow.Index
            If validar() = True Then
                Me.DataGridView2.Item(6, indice).Value = Me.DataGridView2.Item(3, indice).Value - Me.DataGridView2.Item(4, indice).Value - Me.DataGridView2.Item(5, indice).Value
            End If
        Catch ex As Exception
            mensaje.MERRO001()
        End Try

    End Sub
    Private Function validar() As Boolean
        Dim resp As Boolean = False
        Dim numero As String = Me.DataGridView2.Item(5, Me.DataGridView2.CurrentRow.Index).Value
        Dim mate As String = Me.DataGridView2.Item(0, Me.DataGridView2.CurrentRow.Index).Value
        If IsNothing(Me.DataGridView2.Item(5, Me.DataGridView2.CurrentRow.Index).Value) = False Then
            If IsNumeric(numero) Then
                If metodos.Tiene_Decimal(mate, numero) = True Then
                    resp = True
                Else
                    mensaje.MERRO009()
                End If
            Else
                mensaje.MERRO006()
            End If
        Else
            mensaje.MERRO006()
        End If
        Return resp
    End Function
   
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim res As DialogResult
            _PETICION = CDec(CB_PETICION.Text)
            Dim cerrar As Boolean = True
            Dim F_Entrega As Date = DateTimePicker1.Value
            Dim Entregada As Decimal
            Dim c_ingresada As Decimal
            Dim mate As String
            Dim solicitado As Decimal
            Dim contrato As String = oc.Obt_Contrato(_OC)
            Dim ingresomaterial As Boolean = False
            Dim Falta_materiales As Boolean = False
            For i = 0 To DataGridView2.RowCount - 1
                If Me.DataGridView2.Item(5, i).Value <> 0 Then
                    ingresomaterial = True
                End If
            Next
            If ingresomaterial = True Then
                _NREMITO = metodos.Obtener_Numero_Remito
                _FECHA = Date.Now
                metodos.Sumar_Num_Remito()
                For i = 0 To Me.DataGridView2.RowCount - 1
                    Entregada = Me.DataGridView2.Item(4, i).Value + Me.DataGridView2.Item(5, i).Value
                    c_ingresada = Me.DataGridView2.Item(5, i).Value
                    mate = Me.DataGridView2.Item(0, i).Value
                    If c_ingresada > 0 Then
                        If medidores.Es_Serializado(mate) = True Then
                            medidores.Grabar_Med_Sin_Asignar(11, mate, c_ingresada, _NREMITO, _FECHA, _PETICION)
                            metodos.Increpmentar_Stock_Material(mate, "11", c_ingresada, 1)
                        Else
                            metodos.Increpmentar_Stock_Material(mate, _DEPOSITO, c_ingresada, 1)
                        End If
                        metodos.Grabar_Trans(_NREMITO, _FECHA, mate, _DEPOSITO, _DEPOSITO, 1, F_Entrega, _PETICION, "", 0, c_ingresada, _OC, _usr.Obt_Usr, "", "")
                        oc.Actualizar_cant_entregada(mate, _OC, c_ingresada)
                        metodos.Incrementar_Stock_Contrato(mate, contrato, c_ingresada)
                    End If
                    solicitado = Me.DataGridView2.Item(3, i).Value
                    If solicitado - Entregada > 0 Then
                        cerrar = False
                    End If

                Next
                medidores.ELIMINAR_MED_SA("1", 0)
                If cerrar = True Then
                    oc.cerrar_peticion(_OC, 4, _FECHA, _usr.Obt_Usr, 4)
                Else
                    res = MessageBox.Show("Faltan entregar materiales de la peticion" + vbCrLf + "¿Desea enviar mail?", "ENVIAR MAIL", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If res = System.Windows.Forms.DialogResult.Yes Then
                        Dim PANTALLA As New PMAIL001
                        Dim direccion As String
                        PANTALLA.ShowDialog()
                        direccion = PANTALLA.DIRECCIONMAIL
                        ENVIAR_MENSAJE(_PETICION, direccion)
                    End If
                End If
                PrintDocument1.Print()
                Me.DataGridView2.Rows.Clear()
                CB_PETICION.DataSource = Nothing
                llenar_CB_PETICION()
                Me.DataGridView2.Rows.Clear()
                Button3.Enabled = True
                CB_PETICION.Text = Nothing
                If _usr.Obt_Almacen = 0 Then
                    ComboBox3.Text = Nothing
                    CB_PETICION.Enabled = False
                End If
                Button3.Enabled = False
                mensaje.MADVE004(_NREMITO)
            Else
                mensaje.MERRO018()
            End If
        Catch ex As Exception
            mensaje.MERRO001()
        End Try

    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        '#######################datos##################################
        Dim R1_T1 As String = ""
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = ""
        Dim R2_T2 As String = ""
        Dim R3_T1 As String = ""
        Dim R3_T2 As String = ""
        Dim _cant As Integer
        Dim n_REMITO As String = 0
        For i = 0 To Me.DataGridView2.RowCount - 1
            If Me.DataGridView2.Item(4, i).Value <> 0 Then
                _cant += 1
            End If
        Next

        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + _nremito.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + _NREMITO.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "TIPO DE MOVIMIENTO: INGRESO DE MATERIAL"
        R1_T2 = "PETICION N: " + CB_PETICION.Text + " - OC N: " + _OC.ToString.PadLeft(8, "0")
        R2_T1 = "CONFECCIONO: " + oc.QUIEN_CONFECCIONO(_OC)
        R2_T2 = "DEPOSITO DESTINO: " + ComboBox3.Text
        R3_T1 = "REGISTRO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "CANTIDAD DE ITEM: " + _cant.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 800, 1140)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(n_REMITO.ToString, New Font("ARIAL", 16, FontStyle.Bold), Brushes.Black, 435, 73)
        e.Graphics.DrawString(_FECHA.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 215)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 247)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 280)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 215)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 247)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 280)





        'ESCRIBO EL ENCABEZADO DEL DETALLE
        e.Graphics.DrawString("CODIGO", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 325)
        e.Graphics.DrawString("DESCRIPCION", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 350, 325)
        e.Graphics.DrawString("U", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 680, 325)
        e.Graphics.DrawString("CANT", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 715, 325)
        'RECORRO EL DATA
        For I = 0 To DataGridView2.RowCount - 1
            If Me.DataGridView2.Item(5, I).Value <> 0 Then
                e.Graphics.DrawString(Me.DataGridView2.Item(0, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
                e.Graphics.DrawString(Me.DataGridView2.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 80, LINEA)
                e.Graphics.DrawString(Me.DataGridView2.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 680, LINEA)
                e.Graphics.DrawString(Me.DataGridView2.Item(5, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 730, LINEA)


                ' e.Graphics.DrawString(Me.DataGridView2.Item(0, I).Value.ToString + I.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
                'e.Graphics.DrawString(Me.DataGridView2.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 100, LINEA)
                'e.Graphics.DrawString(Me.DataGridView2.Item(4, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 710, LINEA)
                LINEA += SALTO
            End If

        Next


    End Sub
    Public Sub ENVIAR_MENSAJE(ByVal num As String, ByVal direccion As String)
        Dim entregado As Decimal = 0
        Dim solicitado As Decimal = 0
        Dim codmate As String = ""
        Dim desc As String = ""
        Dim saldo As Decimal = 0
        Dim unid As String = ""
        Dim _BODYMAIL As String = "No se entregaron materiales de la peticion " + num.ToString + vbCrLf + vbCrLf + "CODIGO|DESCRIPCION|U|SOLICITADO|ENTREGADO|SALDO" + vbCrLf
        For I = 0 To DataGridView2.RowCount - 1
            If Me.DataGridView2.Item(6, I).Value > 0 Then
                entregado = DataGridView2.Item(4, I).Value + DataGridView2.Item(5, I).Value
                codmate = DataGridView2.Item(0, I).Value
                desc = DataGridView2.Item(1, I).Value
                solicitado = DataGridView2.Item(3, I).Value
                saldo = DataGridView2.Item(6, I).Value
                unid = DataGridView2.Item(2, I).Value
                _BODYMAIL = _BODYMAIL + codmate.ToString + "|" + desc.PadRight(30, " ") + "|" + unid.ToString + "|" + solicitado.ToString + "|" + entregado.ToString + "|" + saldo.ToString + vbCrLf
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
        _mensage.Subject = "MATERIALES SIN ENTREGAR EN LA PETICION: " + num.ToString.PadLeft(10, "0")
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

    Private Sub txtcodmat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcodmat.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If DataGridView2.Rows.Count > 0 Then
                For i = 0 To DataGridView2.Rows.Count - 1
                    If DataGridView2.Rows(i).Cells(0).Value = txtcodmat.Text Then
                        DataGridView2.ClearSelection()
                        DataGridView2.Rows(i).Cells(5).Selected = True
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub txtcant_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcant.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If DataGridView2.Rows.Count > 0 Then
                For i = 0 To DataGridView2.Rows.Count - 1
                    If DataGridView2.Rows(i).Cells(5).Selected = True Then
                        DataGridView2.Rows(i).Cells(5).Value = txtcant.Text
                        DataGridView2.Rows(i).Cells(6).Value = Convert.ToString(CInt(DataGridView2.Rows(i).Cells(6).Value) - CInt(txtcant.Text))
                    End If
                Next
            End If
        End If
    End Sub
End Class