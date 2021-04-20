Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA013
    Private DS_material As New DataSet
    Private DS_deposito1 As New DataSet
    Private DS_deposito2 As New DataSet
    Private _DEPOSITO1 As String
    Private _MATERIAL As String
    Private _DEPOSITO2 As String
    Private _CANT As Decimal
    Private _NREMITO As Decimal
    Private _FECHA As Date
    Private Mensaje As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private _CantItem As Integer = 0

    Private Sub PALMA013_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
            Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
            _CantItem = 0
            MAIN.serie.Clear()
            MAIN.material.Clear()
            'consulto si el usuario tiene deposito asignado
            If _usr.Obt_Almacen <> "0" Then

                cbDesde.DropDownStyle = ComboBoxStyle.DropDown
                _DEPOSITO1 = _usr.Obt_Almacen
                'escribo el nombre del deposito
                cbDesde.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO1)
                'me paro en el equipo
                cbPara.Enabled = True
                cbPara.Focus()
                cbDesde.Enabled = False
                'lleno el combobox de los materiales
                llenar_DS_MATERIAL(_DEPOSITO1)
                LLENAR_CB_MATERIAL()
            Else
                'activo el combobox del deposito
                cbPara.Enabled = True
                'lleno el combo box del deposito
                llenar_DS_DEPOSITO1()
                LLENAR_CB_DEPOSITO1()
                cbDesde.Focus()
                'desactivo el combobox del equitoi
                cbPara.Enabled = False
            End If
            'lleno el combo box del los equipos
            llenar_DS_DEPOSITO2()
            LLENAR_CB_DEPOSITO2()

            tbCantidad.Enabled = False
            cbMaterial.Enabled = False
            txtDisponible.Text = Nothing
        Catch ex As Exception
            Mensaje.MERRO001()
        End Try

    End Sub

    '#########################################################################BOTONES######################################
    Private Sub btAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAgregar.Click
        Dim var As String
        If Metodos.validarMaterial(_MATERIAL, cbMaterial.Text) = True Then
            If IsNothing(tbCantidad.Text) = False Then
                If IsNumeric(tbCantidad.Text) = True Then
                    'remplazo los puntos por coma
                    var = Metodos.Rempl_Punto_Coma(tbCantidad.Text)
                    'pregunto cuantas coma tiene la variable
                    If Metodos.Contar_Coma_Punto(var) <= 1 Then
                        'verifico que el material permita decimal 
                        If Metodos.Tiene_Decimal(_MATERIAL, var) = True Then
                            'verifico que la cantidad se mayor a 0
                            If CDec(var) > 0 Then
                                'verifico que la cantidad sea menor a la disponible
                                If CDec(var) <= CDec(txtDisponible.Text) Then
                                    If Medidor.Es_Serializado(_MATERIAL) = True Then
                                        Dim pantalla As New PALMA013BIS
                                        pantalla.grabardatos(_MATERIAL, CDec(var), _DEPOSITO1, 1)
                                        pantalla.ShowDialog()
                                        If pantalla.validar = True Then
                                            'sumo 1 a la cantidad de item
                                            _CantItem += 1
                                            'agrego al data griview
                                            cbDesde.Enabled = False
                                            cbPara.Enabled = False
                                            DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var))
                                            DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                            cbMaterial.DataSource = Nothing
                                            LLENAR_CB_MATERIAL()
                                            cbMaterial.Text = Nothing
                                            txtDisponible.Text = Nothing
                                            lbUnidad.Text = Nothing
                                            tbCantidad.Text = Nothing
                                            tbCantidad.Enabled = False
                                            If _CantItem = 20 Then
                                                btAgregar.Enabled = False
                                            End If
                                            ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                            If DataGridView1.Rows.Count <> 0 Then
                                                btConfirmar.Enabled = True
                                            End If
                                            cbMaterial.Focus()
                                        Else
                                            cbMaterial.Text = Nothing
                                            tbCantidad.Text = Nothing
                                            tbCantidad.Enabled = False
                                            txtDisponible.Text = Nothing
                                            lbUnidad.Text = Nothing
                                            cbMaterial.Focus()
                                        End If
                                    Else
                                        'sumo 1 a la cantidad de item
                                        _CantItem += 1
                                        'agrego al data griview
                                        cbDesde.Enabled = False
                                        cbPara.Enabled = False
                                        DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var))
                                        DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                        cbMaterial.DataSource = Nothing
                                        LLENAR_CB_MATERIAL()
                                        cbMaterial.Text = Nothing
                                        txtDisponible.Text = Nothing

                                        lbUnidad.Text = Nothing
                                        tbCantidad.Text = Nothing
                                        tbCantidad.Enabled = False
                                        If _CantItem = 20 Then
                                            btAgregar.Enabled = False
                                        End If
                                        ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                        If DataGridView1.Rows.Count <> 0 Then
                                            btConfirmar.Enabled = True
                                        End If
                                        cbMaterial.Focus()
                                    End If
                                Else
                                    Mensaje.MERRO007()
                                    tbCantidad.Focus()
                                    tbCantidad.SelectAll()
                                End If
                            Else
                                Mensaje.MERRO010()
                                tbCantidad.Focus()
                                tbCantidad.SelectAll()
                            End If
                        Else
                            Mensaje.MERRO009()
                            tbCantidad.Focus()
                            tbCantidad.SelectAll()
                        End If
                    Else
                        Mensaje.MERRO006()

                        tbCantidad.Focus()
                        tbCantidad.SelectAll()
                    End If
                Else
                    Mensaje.MERRO006()

                    tbCantidad.Focus()
                    tbCantidad.SelectAll()
                End If
            Else
                Mensaje.MERRO006()

                tbCantidad.Focus()
                tbCantidad.SelectAll()
            End If
        Else
            Mensaje.MERRO006()

            cbMaterial.SelectAll()
            cbMaterial.Focus()
        End If
    End Sub
    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Eliminar_Item.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                Mensaje.MERRO011()
            Else
                DS_material.Tables("T_ALMA_103").Rows.Add(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value, Me.DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value)
                If Medidor.Es_Serializado(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value) Then
                    Dim cant As Integer = MAIN.serie.Count
                    Dim i As Integer
                    Do While cant <> 0
                        If MAIN.material.Item(i) = Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value Then
                            MAIN.serie.RemoveAt(i)
                            MAIN.material.RemoveAt(i)
                        Else
                            i += 1
                        End If
                        cant -= 1
                    Loop
                End If
                DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
                cbMaterial.Text = Nothing
                LLENAR_CB_MATERIAL()
                lbUnidad.Text = Nothing
                txtDisponible.Text = Nothing
                tbCantidad.Text = Nothing
                tbCantidad.Enabled = False
                _CantItem -= 1
                If _CantItem <> 20 Then
                    btAgregar.Enabled = True
                End If
                If DataGridView1.Rows.Count = 0 Then
                    btConfirmar.Enabled = False
                End If
                cbMaterial.Focus()
                Mensaje.MADVE001()
            End If
        Catch ex As Exception
            Mensaje.MERRO001()
        End Try
    End Sub
    Private Sub btConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btConfirmar.Click
        Try
            If DataGridView1.RowCount > 0 Then

                _NREMITO = Metodos.Obtener_Numero_Remito
                _FECHA = Date.Now
                Metodos.Sumar_Num_Remito()
                Metodos.Grabar_Cabezera_Tranf(_NREMITO, _FECHA, _DEPOSITO1, _DEPOSITO2)
                For i = 0 To Me.DataGridView1.Rows.Count - 1
                    _MATERIAL = Me.DataGridView1.Item(0, i).Value
                    _CANT = Me.DataGridView1.Item(3, i).Value
                    'si es serializado se agraga un material serializado sin asignar numero
                    Metodos.Descontar_Stock_Material(_MATERIAL, _DEPOSITO1, _CANT, 1)
                    Metodos.Grabar_Trans_Temp(_NREMITO, _MATERIAL, _CANT, _usr.Obt_Usr)
                    If Medidor.Es_Serializado(_MATERIAL) = True Then
                        For G = 0 To MAIN.serie.Count - 1
                            If MAIN.material.Item(G) = _MATERIAL Then
                                Medidor.MODIFICAR_MEDIDOR_ESTADO_0(MAIN.serie.Item(G), _MATERIAL, _FECHA, _usr.Obt_Usr)
                                Medidor.Grabar_TEMP_TRANS_MEDIDOR(MAIN.serie.Item(G), _MATERIAL, _NREMITO)
                            End If
                        Next
                    End If
                Next
                'equipo
                Dim PANTALLA As New PMAIL001
                Dim direccion As String
                PANTALLA.ShowDialog()
                direccion = PANTALLA.DIRECCIONMAIL
                ENVIAR_MENSAJE(_NREMITO, direccion)
                PrintDocument1.Print()
                PrintDocument1.Print()
                'materiales
                borrar()
                Mensaje.MADVE004(_NREMITO) ''mensaje de confirmacion
            Else
                Mensaje.MERRO011()
            End If

        Catch ex As Exception
            Mensaje.MERRO001()
        End Try

    End Sub
    Private Sub btBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        borrar()
    End Sub
    '#####################################ACCIONES##########################################################################
    Private Sub cbDesde_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDesde.SelectedIndexChanged
        If cbDesde.ValueMember <> Nothing And cbDesde.Text <> Nothing Then
            _DEPOSITO1 = cbDesde.SelectedValue
            cbPara.Enabled = True
            llenar_DS_DEPOSITO2()
            LLENAR_CB_DEPOSITO2()
            cbPara.DropDownStyle = ComboBoxStyle.DropDownList
            llenar_DS_MATERIAL(_DEPOSITO1)
            LLENAR_CB_MATERIAL()
            cbMaterial.Text = Nothing
            tbCantidad.Text = Nothing
            lbUnidad.Text = Nothing
            tbCantidad.Enabled = False
            cbMaterial.Enabled = False
        End If
    End Sub
    Private Sub cbPara_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPara.SelectedIndexChanged
        If cbPara.ValueMember <> Nothing And cbDesde.Text <> Nothing Then
            _DEPOSITO2 = cbPara.SelectedValue
            cbMaterial.Enabled = True
        End If
    End Sub
    Private Sub cbMaterial_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.SelectedIndexChanged
        If cbMaterial.ValueMember <> Nothing Then
            _MATERIAL = cbMaterial.SelectedValue
            If cbPara.Text <> Nothing And cbMaterial.Text <> Nothing Then
                tbCantidad.Enabled = True
                txtDisponible.Text = Metodos.Saldo(_MATERIAL, _DEPOSITO1, 1)
                lbUnidad.Text = Metodos.Unidad(_MATERIAL)
                tbCantidad.Focus()
            End If
        End If
    End Sub
    '##########################################################FUNCIONES###################################################
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
    Private Sub llenar_DS_MATERIAL(ByVal a As String)
        DS_material.Clear()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlClient.SqlDataAdapter("SELECT T_ALMA_103.C_MATE_103 as cod, M_MATE_002.DESC_002 as des FROM T_ALMA_103 INNER JOIN M_MATE_002 ON T_ALMA_103.C_MATE_103 = M_MATE_002.CMATE_002 WHERE (T_ALMA_103.ESTA_103 = 1) AND (T_ALMA_103.C_ALMA_103 = @D1) AND (T_ALMA_103.N_CANT_103 > 0) order by M_MATE_002.DESC_002", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", a))
        adaptadaor.SelectCommand.ExecuteNonQuery()
        adaptadaor.Fill(DS_material, "T_ALMA_103")
        If DS_material.Tables("T_ALMA_103").Rows.Count = 0 Then
            DS_material.Tables("T_ALMA_103").Rows.Add("0", "NO HAY ITEMS")
        End If
        cnn2.Close()
    End Sub
    Private Sub LLENAR_CB_DEPOSITO1()
        cbDesde.DataSource = DS_deposito1.Tables("M_PERS_003")
        cbDesde.DisplayMember = "NOMB_003"
        cbDesde.ValueMember = "NDOC_003"
        cbDesde.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_DEPOSITO2()
        cbPara.DataSource = DS_deposito2.Tables("M_PERS_003")
        cbPara.DisplayMember = "NOMB_003"
        cbPara.ValueMember = "NDOC_003"
        cbPara.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_MATERIAL()
        cbMaterial.DataSource = DS_material.Tables("T_ALMA_103")
        cbMaterial.DisplayMember = "des"
        cbMaterial.ValueMember = "cod"
        cbMaterial.Text = Nothing
    End Sub
    Private Sub borrar()
        _CANT = 0

        cbMaterial.Text = Nothing
        lbUnidad.Text = Nothing
        tbCantidad.Text = Nothing
        txtDisponible.Text = Nothing
        cbMaterial.DataSource = Nothing
        DataGridView1.Rows.Clear()
        MAIN.serie.Clear()
        MAIN.material.Clear()
        cbMaterial.Enabled = False
        tbCantidad.Enabled = False
        btConfirmar.Enabled = False
        If _usr.Obt_Almacen <> "0" Then
            cbPara.Text = Nothing
            cbPara.Enabled = True
            cbPara.Focus()
        Else
            cbDesde.Enabled = True
            cbDesde.Text = Nothing
            cbPara.Enabled = False
            cbPara.Text = Nothing
            cbDesde.Focus()
        End If
        btAgregar.Enabled = True
        
    End Sub

    Public Sub ENVIAR_MENSAJE(ByVal num As Decimal, ByVal direccion As String)
        Dim _BODYMAIL As String = "Se ha cargado la siguiente transferencia entre depositos:" + vbCrLf + "Desde: " + cbDesde.Text + " A: " + cbPara.Text + vbCrLf + "Remito N: " + CStr(num).PadLeft(8, "0").ToString + vbCrLf + vbCrLf + "CODIGO|DESCRIPCION                    .|U|CANTIDAD" + vbCrLf
        For I = 0 To DataGridView1.RowCount - 1
            _BODYMAIL = _BODYMAIL + DataGridView1.Item(0, I).Value + "|" + DataGridView1.Item(1, I).Value.PadRight(30, " ") + "|" + DataGridView1.Item(2, I).Value.ToString + "|" + DataGridView1.Item(3, I).Value.ToString + vbCrLf
        Next

        If MAIN.serie.Count <> 0 Then
            _BODYMAIL = _BODYMAIL + vbCrLf + vbCrLf + "CODIGO|MATERIAL|SERIE"
            For I = 0 To MAIN.serie.Count - 1
                _BODYMAIL = _BODYMAIL + vbCrLf + MAIN.material(I) + "|" + Metodos.detalle_material(MAIN.material(I)) + "|" + MAIN.serie(I).ToString
            Next
        End If

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


    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        '#######################datos##################################
        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + _NREMITO.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + _NREMITO.ToString.PadLeft(8, "0")
        End If
        'DEFINO LAS VARIABLES
        Dim R1_T1 As String = "TIPO DE MOVIMIENTO: TRANSFERENCIA A DEPOSITOS"
        Dim R1_T2 As String = "ESTADO SIN CONFIRMAR"
        Dim R2_T1 As String = "ENTREGA: " + cbDesde.Text
        Dim R2_T2 As String = "RECIBE: " + cbPara.Text
        Dim R3_T1 As String = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        Dim R3_T2 As String = "CANTIDAD DE ITEM: " + DataGridView1.RowCount.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 358
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 810, 1150)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(n_REMITO.ToString, New Font("ARIAL", 13, FontStyle.Bold), Brushes.Black, 420, 77)
        e.Graphics.DrawString(_FECHA.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 220)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 252)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 285)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 220)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 252)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 285)





        e.Graphics.DrawString("CODIGO", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 325)
        e.Graphics.DrawString("DESCRIPCION", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 350, 325)
        e.Graphics.DrawString("U", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 680, 325)
        e.Graphics.DrawString("CANT", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 715, 325)
        'RECORRO EL DATA
        For I = 0 To DataGridView1.RowCount - 1
            e.Graphics.DrawString(Me.DataGridView1.Item(0, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 80, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 680, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(3, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 730, LINEA)
            LINEA += SALTO
        Next
    End Sub


End Class