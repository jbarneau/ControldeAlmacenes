Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO

Public Class PCOMP006

    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen

    Private oc As New Class_OC
    Private DS_material As New DataSet

    Private _material As String
    Private _cant As Decimal
    Private _precio As Decimal = 0
    Private _totalOC As Decimal = 0
    Private _CantItem As Integer = 0
    Private _Nremito As Decimal
    Private unmaterial As New Class_UnMaterial

    Private CantContrato As Integer = 0
    Private porCtotal As Integer = 0

    Public Sub Tomar(NC As Decimal)
        _Nremito = NC
    End Sub


    Private Sub PCOMP006_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llenarDatos()
        llenar_DS_MATERIAL()
        LLENAR_CB_MATERIAL()
    End Sub





    Private Sub llenarDatos()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT T_C_OC_105.N_OC_105, T_C_OC_105.F_ALTA_105, M_PROV_005.RAZO_005, T_C_OC_105.OBS_105, T_C_OC_105.MONTO_105, T_C_OC_105.CONPRECIO_105, APROBO.APELL_001 + ' ' + APROBO.NOMB_001 AS APROBO, CONFECCIONO.APELL_001 + ' ' + CONFECCIONO.NOMB_001 AS CONFECCIONO FROM M_USRS_001 AS CONFECCIONO INNER JOIN T_C_OC_105 ON CONFECCIONO.NDOC_001 = T_C_OC_105.USERG_105 INNER JOIN M_PROV_005 ON T_C_OC_105.C_PROV_105 = M_PROV_005.CUIT_005 INNER JOIN DET_PARAMETRO_802 ON M_PROV_005.LOCA_005 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_USRS_001 AS APROBO ON T_C_OC_105.USERR_105 = APROBO.NDOC_001 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 8) AND (T_C_OC_105.N_OC_105 =@D1)", cnn)
            adt.Parameters.AddWithValue("D1", _Nremito)
            Dim lector As SqlDataReader = adt.ExecuteReader
            Do While lector.Read
                lbNumero.Text = lector.GetValue(0)
                lbFecha.Text = lector.GetDateTime(1).ToShortDateString
                lbProveedor.Text = lector.GetValue(2)

                If IsDBNull(lector.GetValue(3)) = False Then
                    txtOBS.Text = lector.GetString(3)
                End If
                If IsDBNull(lector.GetValue(4)) = False Then
                    txtTotal.Text = lector.GetValue(4)
                End If
                If IsDBNull(lector.GetValue(5)) = False Then
                    If lector.GetValue(5) = 0 Then
                        ocPrecio.Checked = False
                    Else
                        ocPrecio.Checked = True
                    End If
                Else
                    ocPrecio.Checked = False
                End If
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        'SELECT T_D_OC_106.N_OC_106, T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002, M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.PRECIO_C_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1)
        Try
            cnn.Open()
            Dim adt2 As New SqlCommand("SELECT T_D_OC_106.C_MATE_106, M_MATE_002.DESC_002, M_MATE_002.UNID_002, T_D_OC_106.CANT_106, T_D_OC_106.PRECIO_C_106 FROM T_D_OC_106 INNER JOIN M_MATE_002 ON T_D_OC_106.C_MATE_106 = M_MATE_002.CMATE_002 WHERE (T_D_OC_106.N_OC_106 = @D1)", cnn)
            adt2.Parameters.AddWithValue("D1", _Nremito)
            Dim lector2 As SqlDataReader = adt2.ExecuteReader
            Do While lector2.Read
                Dim CANTIDAD As Decimal = 0
                Dim PRECIOU As Decimal = 0
                Dim TOTAl_ITEM As Decimal = 0
                If IsDBNull(lector2.GetValue(3)) = False Then
                    CANTIDAD = FormatNumber(lector2.GetValue(3), 2)
                End If
                If IsDBNull(lector2.GetValue(4)) = False Then
                    PRECIOU = FormatNumber(lector2.GetValue(4), 2)
                End If
                TOTAl_ITEM = PRECIOU * CANTIDAD

                dgv1.Rows.Add(lector2.GetString(0), lector2.GetString(1), lector2.GetString(2), FormatNumber(CANTIDAD, 2).ToString, FormatNumber(PRECIOU, 2).ToString, FormatNumber(TOTAl_ITEM, 2).ToString)

            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Try
            cnn.Open()
            Dim adt2 As New SqlCommand("SELECT  T_C_OC_105C.CONT_105C, M_CONT_004.DESC_004, T_C_OC_105C.CANT_105C FROM T_C_OC_105C INNER JOIN M_CONT_004 ON T_C_OC_105C.CONT_105C = M_CONT_004.NCONT_004 WHERE (T_C_OC_105C.NOC_105C = @D1) ORDER BY M_CONT_004.DESC_004 ", cnn)
            adt2.Parameters.AddWithValue("D1", _Nremito)
            Dim lector2 As SqlDataReader = adt2.ExecuteReader
            Do While lector2.Read
                dgvC.Rows.Add(lector2.GetValue(0).ToString, lector2.GetValue(1).ToString, lector2.GetValue(2).ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        SumarPorcentajes()
    End Sub


    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Dim var As String
        Try
            If Metodos.validarMaterial(_material, CB_Material.Text) = True Then
                If IsNothing(TextBox1.Text) = False Then
                    'If IsNothing(TxtValor.Text) = False Then
                    If IsNumeric(TextBox1.Text) = True Then
                        'remplazo los puntos por coma
                        var = Metodos.Rempl_Punto_Coma(TextBox1.Text)
                        'pregunto cuantas coma tiene la variable
                        If Metodos.Contar_Coma_Punto(var) <= 1 Then
                            'verifico que el material permita decimal 
                            If Metodos.tiene_decimal(_material, var) = True Then
                                'verifico que la cantidad se mayor a 0
                                If CDec(var) > 0 Then
                                    'If IsNumeric(CDbl(TxtValor.Text)) Then
                                    'verifico que la cantidad sea menor a la disponible
                                    'sumo 1 a la cantidad de item
                                    _CantItem += 1
                                    'agrego al data griview
                                    If ocPrecio.Checked Then
                                        _precio = FormatNumber(TxtValor.Text, 2)
                                        _totalOC = FormatNumber((_precio * var), 2)
                                    Else
                                        _precio = 0
                                        _totalOC = 0
                                    End If
                                    dgv1.Rows.Add(_material, CB_Material.Text, lbUnidad.Text, CDec(var), _precio.ToString, _totalOC.ToString)
                                    TextBox1.Text = Nothing
                                    'elimino del data set el material
                                    DS_material.Tables("M_MATE_002").Rows.RemoveAt(CInt(CB_Material.SelectedIndex()))
                                    'vacio el combo box
                                    CB_Material.DataSource = Nothing
                                    'vuelvo a llenar el combo box
                                    LLENAR_CB_MATERIAL()
                                    TextBox1.Enabled = False

                                    lbUnidad.Text = Nothing
                                    TxtValor.Text = Nothing
                                    TxtCodmat.Text = Nothing
                                    CB_Material.Focus()
                                    'verifico que sean menos de 20  items
                                    If _CantItem = 20 Then
                                        B_Agregar_Item.Enabled = False
                                    End If
                                    ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                    If dgv1.Rows.Count <> 0 Then
                                        B_Entregar.Enabled = True
                                    End If
                                    'End If
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
                    'Else
                    '    MENSAJE.MERRO006()
                    '    TextBox1.Focus()
                    '    TextBox1.SelectAll()
                    'End If
                Else
                    MENSAJE.MERRO006()
                    TextBox1.Focus()
                    TextBox1.SelectAll()
                End If

            Else
                MENSAJE.MERRO006()
                CB_Material.SelectAll()
                CB_Material.Focus()
            End If
            SumarTotal()
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub SumarTotal()
        Dim total As Decimal = 0
        For i = 0 To dgv1.Rows.Count - 1
            total += CDec(dgv1.Item(5, i).Value)
        Next
        txtTotal.Text = FormatNumber(total, 2).ToString
    End Sub
    Private Sub llenar_DS_MATERIAL()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 where SERI_002 = 0 AND F_BAJA_002 IS NULL order by DESC_002", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_material, "M_MATE_002")
        cnn2.Close()
    End Sub
    Private Sub LLENAR_CB_MATERIAL()
        CB_Material.DataSource = DS_material.Tables("M_MATE_002")
        CB_Material.DisplayMember = "DESC_002"
        CB_Material.ValueMember = "CMATE_002"
        CB_Material.Text = Nothing
    End Sub
    Private Sub CB_Material_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Material.SelectedIndexChanged

        If CB_Material.ValueMember <> Nothing Then
            TextBox1.Enabled = True
            If CB_Material.Text <> Nothing Then
                _material = CB_Material.SelectedValue
                TxtCodmat.Text = _material
                If (ocPrecio.Checked) Then
                    TxtValor.Text = FormatNumber(UltimoPrecio(_material), 2).ToString

                Else
                    TxtValor.Text = 0
                End If
                lbUnidad.Text = Metodos.Unidad(_material)

            End If
        End If
    End Sub
    Private Sub TxtCodmat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCodmat.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim dt As New DataTable
            dt = CB_Material.DataSource
            For index = 0 To dt.Rows.Count - 1
                If dt.Rows(index)(0).ToString() = TxtCodmat.Text Then
                    CB_Material.SelectedIndex = index
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub TxtCodmat_DoubleClick(sender As Object, e As EventArgs) Handles TxtCodmat.DoubleClick
        Dim PANTALLA As New PMATE002
        PANTALLA.ShowDialog()
        If PANTALLA.dato = True Then
            TxtCodmat.Text = PANTALLA.dniobtenido
            unmaterial.Existe_material(TxtCodmat.Text)
            Dim dt As New DataTable
            dt = CB_Material.DataSource
            For index = 0 To dt.Rows.Count - 1
                If dt.Rows(index)(0).ToString() = TxtCodmat.Text Then
                    CB_Material.SelectedIndex = index
                    Exit For
                End If
            Next
        End If
    End Sub


    Private Function UltimoPrecio(codMaterla As String) As Decimal
        Dim precio As Decimal = 0
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT T_D_OC_106.PRECIO_C_106 FROM T_C_OC_105 INNER JOIN T_D_OC_106 ON T_C_OC_105.N_OC_105 = T_D_OC_106.N_OC_106 WHERE (T_C_OC_105.TIPO_OC_105 = 1) AND (T_D_OC_106.C_MATE_106 = @D1) GROUP BY T_D_OC_106.PRECIO_C_106 HAVING (T_D_OC_106.PRECIO_C_106 IS NOT NULL)", cnn)
            adt.Parameters.AddWithValue("D1", codMaterla)
            precio = adt.ExecuteScalar()
        Catch ex As Exception
            precio = 0
        Finally
            cnn.Close()
        End Try
        Return precio
    End Function

    Private Sub EliminarTemSeleccionadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarTemSeleccionadoToolStripMenuItem.Click
        Try
            If dgv1.Rows.Count = 0 Then
                MENSAJE.MERRO011()
            Else
                Dim NuevoRows As Data.DataRow
                NuevoRows = DS_material.Tables("M_MATE_002").NewRow
                NuevoRows("CMATE_002") = Me.dgv1.Item(0, dgv1.CurrentRow.Index).Value
                NuevoRows("DESC_002") = Me.dgv1.Item(1, dgv1.CurrentRow.Index).Value
                DS_material.Tables("M_MATE_002").Rows.Add(NuevoRows)
                dgv1.Rows.RemoveAt(dgv1.CurrentRow.Index)
                CB_Material.Text = Nothing
                TextBox1.Enabled = False
                TextBox1.Text = Nothing
                lbUnidad.Text = Nothing
                MENSAJE.MADVE001()
                _CantItem -= 1
                If _CantItem <> 20 Then
                    B_Agregar_Item.Enabled = True
                End If
                If dgv1.Rows.Count = 0 Then
                    B_Entregar.Enabled = False
                End If
            End If
            SumarTotal()
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub MODIFICARPRECIOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MODIFICARPRECIOToolStripMenuItem.Click
        If ocPrecio.Checked Then
            If dgv1.SelectedRows.Count <> 0 Then
                Dim indice As Integer = dgv1.CurrentRow.Index
                Dim p As New PCOMP001_MOD
                p.tomar("INGRESE EL NUEVO PRECIO")
                p.ShowDialog()
                If p.leerConfirmacion = True Then
                    dgv1.Item(4, indice).Value = p.leerValor.ToString
                    dgv1.Item(5, indice).Value = FormatNumber((CDec(dgv1.Item(4, indice).Value) * CDec(dgv1.Item(3, indice).Value)), 2).ToString
                    SumarTotal()
                End If
            End If
        End If
    End Sub
    Private Sub MODIFICARCANTIDADToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MODIFICARCANTIDADToolStripMenuItem.Click
        If ocPrecio.Checked Then
            If dgv1.SelectedRows.Count <> 0 Then
                Dim indice As Integer = dgv1.CurrentRow.Index
                Dim p As New PCOMP001_MOD
                p.tomar("INGRESE LA NUEVA CANTIDAD")
                p.ShowDialog()
                If p.leerConfirmacion = True Then
                    dgv1.Item(3, indice).Value = p.leerValor.ToString
                    dgv1.Item(5, indice).Value = FormatNumber((CDec(dgv1.Item(4, indice).Value) * CDec(dgv1.Item(3, indice).Value)), 2).ToString
                    SumarTotal()
                End If
            End If
        End If
    End Sub
    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        If dgvC.SelectedRows.Count <> 0 Then
            dgvC.Rows.RemoveAt(dgvC.CurrentRow.Index)
            SumarPorcentajes()
        End If
    End Sub

    Private Sub SumarPorcentajes()
        porCtotal = 0
        CantContrato = 0
        For i = 0 To dgvC.Rows.Count - 1
            porCtotal = porCtotal + CDec(dgvC.Item(2, i).Value)
        Next
        CantContrato = dgvC.Rows.Count
        lbporcentaje.Text = porCtotal.ToString
        If CantContrato = 0 Then
            CB_Material.Enabled = False
            TxtCodmat.Enabled = False
            TxtValor.Enabled = False
            txtOBS.Enabled = False
        Else
            CB_Material.Enabled = True
            TxtCodmat.Enabled = True
            TxtValor.Enabled = True
            txtOBS.Enabled = True
        End If
    End Sub
    Private Sub AgregarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarToolStripMenuItem.Click
        Dim p As New PCOMP005
        p.ShowDialog()
        If p.carga = True Then
            Dim esta As Integer = 0
            Dim encontro As Boolean = False
            For i = 0 To dgvC.Rows.Count - 1
                If dgvC.Item(0, i).Value = p.COD Then
                    esta = i
                    encontro = True
                End If
            Next
            If encontro = False Then
                dgvC.Rows.Add(p.COD, p.dESC, p.POR)
            Else
                dgvC.Item(2, esta).Value = dgvC.Item(2, esta).Value + p.POR
            End If
            SumarPorcentajes()
        End If
    End Sub
    Private Sub EditarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditarToolStripMenuItem.Click
        If dgvC.SelectedRows.Count <> 0 Then
            Dim indice As Integer = dgvC.CurrentRow.Index
            Dim p As New PCOMP005
            p.TOMAR(dgvC.Item(0, indice).Value, dgvC.Item(1, indice).Value, dgvC.Item(2, indice).Value)
            p.ShowDialog()
            If p.carga = True Then
                dgvC.Rows.RemoveAt(indice)
                Dim encontro As Boolean = False
                Dim esta As Integer = 0
                For i = 0 To dgvC.Rows.Count - 1
                    If dgvC.Item(0, i).Value = p.COD Then
                        esta = i
                        encontro = True
                    End If
                Next
                If encontro = False Then
                    dgvC.Rows.Add(p.COD, p.dESC, p.POR)
                Else
                    dgvC.Item(2, esta).Value = dgvC.Item(2, esta).Value + p.POR
                End If
                SumarPorcentajes()
            End If
        End If
    End Sub

    Private Sub B_Entregar_Click(sender As Object, e As EventArgs) Handles B_Entregar.Click



        Dim precioFinal As Decimal = 0
        Dim tienePrecio As Integer = 0
        Dim Obs As String = txtOBS.Text
        If CantContrato <> 0 Then
            If porCtotal = 100 Then
                If txtTotal.Text <> 0 And txtTotal.Text <> "" Then
                    precioFinal = txtTotal.Text
                End If
                If ocPrecio.Checked Then
                    tienePrecio = 1
                End If
                Try
                    If dgv1.RowCount <> 0 Then
                        Try

                            If ocPrecio.Checked Then
                                tienePrecio = 1
                            End If
                            oc.EliminarContratoyDetalle(_Nremito)
                            oc.Edicar_cab_OC(_Nremito, tienePrecio, Obs, precioFinal)
                            oc.grabar_contrato(dgvC, _Nremito)
                            oc.guardarMovimiento(_Nremito, Date.Now, 3, _usr.Obt_Usr)
                            For I = 0 To Me.dgv1.RowCount - 1
                                _material = Me.dgv1.Item(0, I).Value
                                _cant = Me.dgv1.Item(3, I).Value
                                oc.grabar_det_oc(_Nremito, _material, _cant, 0, True, CDec(Me.dgv1.Item(4, I).Value))
                            Next
                            'GRABAR DETELLE
                            Dim res As DialogResult
                                res = MessageBox.Show("Quiere imprimir la Orden de Compra?", "impresion de OC", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                                If res = System.Windows.Forms.DialogResult.Yes Then
                                    oc.ImprimirOC(_Nremito)

                                End If


                            MENSAJE.MADVE001()
                            Me.Close()
                        Catch ex As Exception
                            MENSAJE.MERRO001()
                        End Try
                    Else
                        MENSAJE.MERRO011()
                    End If
                Catch ex As Exception
                    MENSAJE.MERRO001()
                End Try

            Else
                MessageBox.Show("LA SUMATORIA DE LOS % NO ES DE 100", "ERROR EN CONTRATO", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Else
            MessageBox.Show("NO SE AGREGARON CONTRATOS", "ERROR EN CONTRATO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim pCOMP = TryCast(obj, PCOMP006)
        Return pCOMP IsNot Nothing AndAlso
               _Nremito = pCOMP._Nremito
    End Function
End Class