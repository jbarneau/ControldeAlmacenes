Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PPETI001
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private oc As New Class_OC
    Private DS_material As New DataSet
    Private _proveedor As String
    Private _material As String
    Private _contrato As String
    Private _cant As Decimal
    Private _CantItem As Integer = 0
    Private _Nremito As Integer


    Private Sub PPETI001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DS_MATERIAL()
        LLENAR_CB_MATERIAL()
        llenar_CB_Contrato()
        llenar_CB_proveedor()
        CB_contrato.Enabled = False
        CB_Material.Enabled = False
        cmbdes.Enabled = False
        TextBox1.Enabled = False
    End Sub
    Private Sub Borrar()
        _CantItem = 0
        DS_material.Clear()
        CB_Material.DataSource = Nothing
        llenar_DS_MATERIAL()
        LLENAR_CB_MATERIAL()
        CB_Material.Text = Nothing
        CB_PROVEEDOR.Text = Nothing
        CB_contrato.Text = Nothing
        TextBox1.Text = Nothing
        lbUnidad.Text = Nothing
        CB_PROVEEDOR.Enabled = True
        CB_contrato.Enabled = False
        CB_Material.Enabled = False
        cmbdes.Enabled = False
        TextBox1.Enabled = False
        B_Entregar.Enabled = False
        B_Agregar_Item.Enabled = True
        Me.DataGridView1.Rows.Clear()
    End Sub
  

    Private Sub llenar_DS_MATERIAL()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 where TIPO_002=1 OR TIPO_002 = 3 AND F_BAJA_002 IS NULL order by DESC_002", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_material, "M_MATE_002")
        cnn2.Close()
    End Sub
    Private Sub LLENAR_CB_MATERIAL()
        CB_Material.DataSource = DS_material.Tables("M_MATE_002")
        cmbdes.DataSource = DS_material.Tables("M_MATE_002")
        CB_Material.DisplayMember = "CMATE_002"
        CB_Material.ValueMember = "CMATE_002"
        cmbdes.DisplayMember = "DESC_002"
        CB_Material.Text = Nothing
        cmbdes.Text = Nothing
    End Sub
    Private Sub llenar_CB_proveedor()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CUIT_005,RAZO_005 FROM M_PROV_005 where F_BAJA_005 is NULL and SPETI_005=1 order by RAZO_005", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_PROV_005")
        cnn2.Close()
        CB_Proveedor.DataSource = DS_contrato.Tables("M_PROV_005")
        CB_Proveedor.DisplayMember = "RAZO_005"
        CB_Proveedor.ValueMember = "CUIT_005"
        CB_Proveedor.Text = Nothing
    End Sub
    Private Sub llenar_CB_Contrato()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NCONT_004, DESC_004 FROM M_CONT_004 where F_BAJA_004 is NULL AND PETI_004 = 1 order by DESC_004", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_CONT_004")
        cnn2.Close()
        CB_contrato.DataSource = DS_contrato.Tables("M_CONT_004")
        CB_contrato.DisplayMember = "DESC_004"
        CB_contrato.ValueMember = "NCONT_004"
        CB_contrato.Text = Nothing
    End Sub

    Private Sub CB_Proveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Proveedor.SelectedIndexChanged
        If CB_Proveedor.ValueMember <> Nothing Then
            CB_contrato.Enabled = True
            If CB_Proveedor.Text <> Nothing Then
                _proveedor = CB_Proveedor.SelectedValue
            End If
        End If
    End Sub

    Private Sub CB_contrato_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_contrato.SelectedIndexChanged
        If CB_contrato.ValueMember <> Nothing Then
            CB_Material.Enabled = True
            cmbdes.Enabled = True
            If CB_contrato.Text <> Nothing Then
                _contrato = CB_contrato.SelectedValue
            End If
        End If
    End Sub

    Private Sub CB_Material_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Material.SelectedIndexChanged
        If CB_Material.ValueMember <> Nothing Then
            TextBox1.Enabled = True
            If CB_Material.Text <> Nothing Then
                _material = CB_Material.SelectedValue
                lbUnidad.Text = Metodos.Unidad(_material)
            End If
        End If
    End Sub

    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Dim var As String
        Try
            If Metodos.validarMaterial(_material, cmbdes.Text) = True Then
                'If oc.verificar_oc_abierta(_material, _contrato) = False Then
                If IsNothing(TextBox1.Text) = False Then
                    If IsNumeric(TextBox1.Text) = True Then
                        'remplazo los puntos por coma
                        var = Metodos.Rempl_Punto_Coma(TextBox1.Text)
                        'pregunto cuantas coma tiene la variable
                        If Metodos.Contar_Coma_Punto(var) <= 1 Then
                            'verifico que el material permita decimal 
                            If Metodos.tiene_decimal(_material, var) = True Then
                                'verifico que la cantidad se mayor a 0
                                If CDec(var) > 0 Then
                                    'verifico que la cantidad sea menor a la disponible
                                    'sumo 1 a la cantidad de item
                                    _CantItem += 1
                                    'agrego al data griview
                                    DataGridView1.Rows.Add(_material, cmbdes.Text, lbUnidad.Text, CDec(var))
                                    TextBox1.Text = Nothing
                                    'elimino del data set el material
                                    DS_material.Tables("M_MATE_002").Rows.RemoveAt(CInt(CB_Material.SelectedIndex()))
                                    'vacio el combo box
                                    CB_Material.DataSource = Nothing
                                    'vuelvo a llenar el combo box
                                    LLENAR_CB_MATERIAL()
                                    TextBox1.Enabled = False
                                    CB_contrato.Enabled = False
                                    CB_Proveedor.Enabled = False
                                    lbUnidad.Text = Nothing
                                    CB_Material.Focus()
                                    'verifico que sean menos de 20  items
                                    'If _CantItem = 20 Then
                                    '    B_Agregar_Item.Enabled = False
                                    'End If
                                    ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                    If DataGridView1.Rows.Count <> 0 Then
                                        B_Entregar.Enabled = True
                                    End If
                                Else
                                    MENSAJE.MERRO010()
                                    TextBox1.Focus()
                                    TextBox1.SelectAll()
                                End If
                            Else
                                MENSAJE.MERRO009()
                                TextBox1.Focus()
                                TextBox1.SelectAll()

                            End If
                        Else
                            MENSAJE.MERRO006()
                            TextBox1.Focus()
                            TextBox1.SelectAll()
                        End If
                    Else
                        MENSAJE.MERRO006()
                        TextBox1.Focus()
                        TextBox1.SelectAll()
                    End If
                Else
                    MENSAJE.MERRO006()
                    TextBox1.Focus()
                    TextBox1.SelectAll()
                End If
                'Else
                '    MENSAJE.MERRO022()
                '    DS_material.Tables("M_MATE_002").Rows.RemoveAt(CInt(CB_Material.SelectedIndex()))
                '    CB_Material.DataSource = Nothing
                '    LLENAR_CB_MATERIAL()
                '    TextBox1.Text = Nothing
                '    lbUnidad.Text = Nothing
                '    TextBox1.Enabled = False

                '    CB_Material.SelectAll()
                '    CB_Material.Focus()
                'End If

            Else
                MENSAJE.MERRO006()
                CB_Material.SelectAll()
                CB_Material.Focus()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub

    Private Sub B_Eliminar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Eliminar_Item.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                MENSAJE.MERRO011()
            Else
                Dim NuevoRows As Data.DataRow
                NuevoRows = DS_material.Tables("M_MATE_002").NewRow
                NuevoRows("CMATE_002") = Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
                NuevoRows("DESC_002") = Me.DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
                DS_material.Tables("M_MATE_002").Rows.Add(NuevoRows)
                DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
                CB_Material.Text = Nothing
                lbUnidad.Text = Nothing
                MENSAJE.MADVE001()
                _CantItem -= 1
                If _CantItem <> 20 Then
                    B_Agregar_Item.Enabled = True
                End If
                If DataGridView1.Rows.Count = 0 Then
                    B_Entregar.Enabled = False
                End If
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub

    Private Sub B_Entregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Entregar.Click
        Try
            If DataGridView1.RowCount <> 0 Then
                Try

                    _Nremito = oc.Obtener_Numero_OC
                    oc.Sumar_Num_OC()
                    'GRABAR CABECERA

                    If _usr.Activar_BT("PCONF002") = False Then
                        oc.grabar_cab_OC(_Nremito, Date.Now, _usr.Obt_Usr, _proveedor, 2, _contrato, 0, "", 0)
                        'GRABAR DETELLE
                        For I = 0 To Me.DataGridView1.RowCount - 1
                            _material = Me.DataGridView1.Item(0, I).Value
                            _cant = Me.DataGridView1.Item(3, I).Value
                            oc.grabar_det_oc(_Nremito, _material, _cant, 0, False, 0)
                        Next
                        Dim PANTALLA As New PMAIL001
                        Dim direccion As String
                        PANTALLA.ShowDialog()
                        direccion = PANTALLA.DIRECCIONMAIL
                        ENVIAR_MENSAJE(_Nremito, direccion, "PENDIENTE DE APORBACION")
                        Borrar()
                        MENSAJE.MADVE001()
                    Else
                        oc.grabar_cab_OC(_Nremito, Date.Now, _usr.Obt_Usr, _proveedor, 2, _contrato, 0, "", 0)
                        oc.aprobar_oc(_Nremito, 2, Date.Now, _usr.Obt_Usr)
                        'GRABAR DETELLE
                        For I = 0 To Me.DataGridView1.RowCount - 1
                            _material = Me.DataGridView1.Item(0, I).Value
                            _cant = Me.DataGridView1.Item(3, I).Value
                            oc.grabar_det_oc(_Nremito, _material, _cant, 0, True, 0)
                        Next
                        Dim PANTALLA As New PMAIL001
                        Dim direccion As String
                        PANTALLA.ShowDialog()
                        direccion = PANTALLA.DIRECCIONMAIL
                        ENVIAR_MENSAJE(_Nremito, direccion, "APROBADA")
                        Borrar()
                        MENSAJE.MADVE001()
                    End If
                Catch ex As Exception
                    MENSAJE.MERRO001()
                End Try
            Else
                MENSAJE.MERRO011()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try

    End Sub
    Public Sub ENVIAR_MENSAJE(ByVal num As Decimal, ByVal DIRECCION As String, ByVal texto As String)
        Dim _BODYMAIL As String = "Se ha cargado la siguiente petición:" + vbCrLf + "OC N: " + CStr(num).PadLeft(8, "0").ToString + vbCrLf + vbCrLf + "CODIGO|DESCRIPCION                             |U|CANTIDAD" + vbCrLf
        For I = 0 To DataGridView1.RowCount - 1
            _BODYMAIL = _BODYMAIL.ToString + Me.DataGridView1.Item(0, I).Value.ToString.PadRight(6, " ") + "|" + Me.DataGridView1.Item(1, I).Value.ToString.PadRight(40, " ") + "|" + Me.DataGridView1.Item(2, I).Value.ToString + "|" + Me.DataGridView1.Item(3, I).Value.ToString + vbCrLf
        Next
        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential(MAIN.mail, MAIN.passmail)
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add(DIRECCION.ToString)
        _mensage.From = New System.Net.Mail.MailAddress("almacensis@exgadetsa.com.ar", "Generacion de petición " + Date.Now.ToShortDateString, System.Text.Encoding.UTF8)
        _mensage.Subject = "NOTIFICACION DE GENERACION DE ORDEN DE PETICION N: " + num.ToString.PadLeft(10, "0") + " Realizada " + Date.Now.ToLongDateString + " - ESTADO: " + texto
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Borrar()

    End Sub
End Class