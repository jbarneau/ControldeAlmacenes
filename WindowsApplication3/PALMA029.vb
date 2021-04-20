Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA029

    Private DS_material As New DataSet
    Private DS_almacen As New DataSet
    Private DS_deposito As New DataSet
    Private _DEPOSITO As String
    Private _MATERIAL As String
    Private _ALMACEN As String
    Private _NREMITO As Decimal
    Private _FECHA As Date
    Private _CANT As Decimal
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private med_rettirar As New Clase_med_retirar
    Private _CantItem As Integer = 0


    Private Sub PALMA029_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            MAIN.serie.Clear()
            MAIN.material.Clear()
            Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
            Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
            _CantItem = 0
            
            If _usr.Obt_Almacen <> "0" Then
                cbDeposito.DropDownStyle = ComboBoxStyle.DropDown
                _DEPOSITO = _usr.Obt_Almacen
                cbDeposito.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO)
                cbAlmacen.Focus()

            Else
                cbAlmacen.Enabled = False
                cbDeposito.Enabled = True
                cbDeposito.DropDownStyle = ComboBoxStyle.DropDownList
                cbDeposito.Focus()
                llenar_DS_DEPOSITO()
                LLENAR_CB_DEPOSITO()
                cbDeposito.Focus()
            End If
            llenar_DS_ALMACEN()
            LLENAR_CB_ALMACEN()
            tbCantidad.Enabled = False
            cbMaterial.Enabled = False
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    '########################### BOTONES ##################################

   
    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAgregar.Click
        Dim var As String
        Try
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
                                            Dim pantalla As New PALMA004BIS
                                            pantalla.grabardatos(_MATERIAL, CDec(var), _ALMACEN, 1)
                                            pantalla.ShowDialog()
                                            If pantalla.validar = True Then
                                                'sumo 1 a la cantidad de item
                                                _CantItem += 1
                                                'agrego al data griview
                                                cbAlmacen.Enabled = False
                                                cbDeposito.Enabled = False
                                                DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var))
                                                DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                                LLENAR_CB_MATERIAL()
                                                tbCantidad.Text = Nothing
                                                lbUnidad.Text = Nothing
                                                txtDisponible.Text = Nothing
                                                tbCantidad.Enabled = False
                                                If _CantItem = 20 Then
                                                    btAgregar.Enabled = False
                                                End If
                                                ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                                If DataGridView1.Rows.Count <> 0 Then
                                                    btConfirmar.Enabled = True
                                                End If
                                                cbMaterial.Focus()
                                                'verifico que sean menos de 20  items


                                            Else
                                                cbMaterial.Text = Nothing
                                                tbCantidad.Text = Nothing
                                                tbCantidad.Enabled = False
                                                lbUnidad.Text = Nothing
                                                txtDisponible.Text = 0
                                                cbMaterial.Focus()
                                            End If
                                        Else
                                            'sumo 1 a la cantidad de item
                                            _CantItem += 1
                                            'agrego al data griview
                                            cbAlmacen.Enabled = False
                                            cbDeposito.Enabled = False
                                            DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var))
                                            DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                            LLENAR_CB_MATERIAL()
                                            tbCantidad.Text = Nothing
                                            lbUnidad.Text = Nothing
                                            txtDisponible.Text = Nothing
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
                                        MENSAJE.MERRO007()
                                        tbCantidad.Focus()
                                        tbCantidad.SelectAll()
                                    End If
                                Else
                                    MENSAJE.MERRO010()
                                    tbCantidad.Focus()
                                    tbCantidad.SelectAll()

                                End If
                            Else
                                MENSAJE.MERRO009()
                                tbCantidad.Focus()
                                tbCantidad.SelectAll()

                            End If
                        Else
                            MENSAJE.MERRO006()
                            tbCantidad.Focus()
                            tbCantidad.SelectAll()
                        End If
                    Else
                        MENSAJE.MERRO006()
                        tbCantidad.Focus()
                        tbCantidad.SelectAll()
                    End If
                Else
                    MENSAJE.MERRO006()
                    tbCantidad.Focus()
                    tbCantidad.SelectAll()
                End If

            Else
                MENSAJE.MERRO006()
                cbMaterial.SelectAll()
                cbMaterial.Focus()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub B_Eliminar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEliminar.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                MENSAJE.MERRO011()
            Else
                DS_material.Tables("T_ALMA_103").Rows.Add(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value, Me.DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value)
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
                MENSAJE.MADVE001()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub B_Entregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btConfirmar.Click
        Try
            If DataGridView1.RowCount > 0 Then
                _NREMITO = Metodos.Obtener_Numero_Remito
                _FECHA = Date.Now
                For i = 0 To Me.DataGridView1.Rows.Count - 1
                    _MATERIAL = Me.DataGridView1.Item(0, i).Value
                    _CANT = Me.DataGridView1.Item(3, i).Value
                    Metodos.Descontar_Stock_Material(_MATERIAL, _ALMACEN, _CANT, 1)
                    Metodos.Increpmentar_Stock_Material(_MATERIAL, _DEPOSITO, _CANT, 9)
                    Metodos.Grabar_Trans(_NREMITO, _FECHA, _MATERIAL, _ALMACEN, _DEPOSITO, 6, _FECHA, 0, "", 0, _CANT, 0, _usr.Obt_Usr, "", "")
                    'si es serializado se agraga un material serializado sin asignar numero
                    If Medidor.Es_Serializado(_MATERIAL) = True Then
                        For G = 0 To MAIN.serie.Count - 1
                            If MAIN.material.Item(G) = _MATERIAL Then
                                Medidor.MODIFICAR_MEDIDOR_ESTADO_9(MAIN.serie.Item(G), _MATERIAL, _FECHA, _DEPOSITO, _usr.Obt_Usr)
                                Medidor.Grabar_Mov_Medi(MAIN.serie.Item(G), _MATERIAL, _NREMITO, _FECHA, _ALMACEN, _DEPOSITO)
                                'med_rettirar.GRABAR_MEDIDOR2(MAIN.serie.Item(G), Date.Today, _usr.Obt_Usr, _DEPOSITO, CODREZAGO(_MATERIAL), "SP", "01", Date.Today, 0, 0, "SO", "SP")
                            End If
                        Next
                    End If
                Next
                'equipo
                PrintDocument1.Print()
                borrar()
                MENSAJE.MADVE004(_NREMITO) ''mensaje de confirmacion
            Else
                MENSAJE.MERRO011()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        borrar()
    End Sub




    '################################## FUNCIONES ##########################################

    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE ALMA_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_DEPOSITO()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 and F_BAJA_003 IS NULL ORDER BY NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito, "M_PERS_003")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_MATERIAL(ByVal a As String)
        DS_material.Clear()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlClient.SqlDataAdapter("SELECT T_ALMA_103.C_MATE_103 as cod, M_MATE_002.DESC_002 as des FROM T_ALMA_103 INNER JOIN M_MATE_002 ON T_ALMA_103.C_MATE_103 = M_MATE_002.CMATE_002 WHERE (T_ALMA_103.ESTA_103 = 1) AND (T_ALMA_103.C_ALMA_103 = @D1) AND (M_MATE_002.TIPO_002 = 1) AND (T_ALMA_103.N_CANT_103 > 0) order by M_MATE_002.DESC_002", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", a))
        adaptadaor.SelectCommand.ExecuteNonQuery()
        adaptadaor.Fill(DS_material, "T_ALMA_103")
        If DS_material.Tables("T_ALMA_103").Rows.Count = 0 Then
            DS_material.Tables("T_ALMA_103").Rows.Add("0", "NO HAY ITEMS")
        End If
        cnn2.Close()
    End Sub
    Private Sub LLENAR_CB_DEPOSITO()
        cbDeposito.DataSource = DS_deposito.Tables("M_PERS_003")
        cbDeposito.DisplayMember = "NOMB_003"
        cbDeposito.ValueMember = "NDOC_003"
        cbDeposito.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_ALMACEN()
        cbAlmacen.DataSource = DS_almacen.Tables("M_PERS_003")
        cbAlmacen.DisplayMember = "NOMBRE"
        cbAlmacen.ValueMember = "NDOC_003"
        cbAlmacen.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_MATERIAL()
        cbMaterial.DataSource = Nothing
        cbMaterial.DataSource = DS_material.Tables("T_ALMA_103")
        cbMaterial.DisplayMember = "des"
        cbMaterial.ValueMember = "cod"
        cbMaterial.Text = Nothing
        txtDisponible.Text = Nothing
        lbUnidad.Text = Nothing
    End Sub
    Private Sub borrar()
        _CANT = 0
        MAIN.serie.Clear()
        MAIN.material.Clear()
        tbCantidad.Text = Nothing
        lbUnidad.Text = Nothing
        txtDisponible.Text = Nothing
        DataGridView1.Rows.Clear()
        cbAlmacen.Text = Nothing
        cbMaterial.Text = Nothing
        cbMaterial.Enabled = False
        tbCantidad.Enabled = False
        If _usr.Obt_Almacen <> "0" Then
            cbAlmacen.Enabled = True
            cbAlmacen.Focus()
        Else
            cbDeposito.Text = Nothing
            cbAlmacen.Enabled = False
            cbDeposito.Enabled = True
            cbDeposito.Focus()

        End If
        CheckBox1.Checked = False
        btAgregar.Enabled = True
        btConfirmar.Enabled = False
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDeposito.SelectedIndexChanged
        If cbDeposito.ValueMember <> Nothing Then
            If cbDeposito.Text <> Nothing Then
                cbAlmacen.Enabled = True
                _DEPOSITO = cbDeposito.SelectedValue
            End If
        End If
    End Sub

    Private Sub CB_Equipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbAlmacen.SelectedIndexChanged
        If cbAlmacen.ValueMember <> Nothing Then
            If cbAlmacen.Text <> Nothing Then
                _ALMACEN = cbAlmacen.SelectedValue
                cbMaterial.Enabled = True
                llenar_DS_MATERIAL(_ALMACEN)
                LLENAR_CB_MATERIAL()
            End If
        End If
    End Sub

    Private Sub CB_Material_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.SelectedIndexChanged
        If cbMaterial.ValueMember <> Nothing Then
            If cbMaterial.Text <> Nothing Then
                _MATERIAL = cbMaterial.SelectedValue
                tbCantidad.Enabled = True
                If cbAlmacen.Text <> Nothing Then
                    txtDisponible.Text = Metodos.Saldo(_MATERIAL, _ALMACEN, 1)
                    lbUnidad.Text = Metodos.Unidad(_MATERIAL)
                    tbCantidad.Focus()

                End If
            End If
        End If
    End Sub
    Private Sub tbCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbCantidad.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btAgregar.Focus()
        End If
    End Sub

 
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            cbMaterial.Enabled = True
            _ALMACEN = _DEPOSITO
            cbAlmacen.DropDownStyle = ComboBoxStyle.DropDown
            cbAlmacen.Text = cbDeposito.Text
            cbAlmacen.Enabled = False
            llenar_DS_MATERIAL(_ALMACEN)
            LLENAR_CB_MATERIAL()
            tbCantidad.Enabled = False

        Else
            cbAlmacen.DropDownStyle = ComboBoxStyle.DropDownList
            cbAlmacen.Enabled = True
            cbAlmacen.Text = Nothing
            txtDisponible.Text = Nothing
            cbMaterial.Enabled = False
            tbCantidad.Text = Nothing
            tbCantidad.Enabled = False
            lbUnidad.Text = Nothing
            txtDisponible.Text = Nothing

        End If

    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        '#######################datos##################################
        Dim R1_T1 As String = ""
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = ""
        Dim R2_T2 As String = ""
        Dim R3_T1 As String = ""
        Dim R3_T2 As String = ""

        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + _NREMITO.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + _NREMITO.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "TIPO DE MOVIMIENTO: MATERIALES A REZAGO"
        R1_T2 = "MOTIVO: SIN MOTIVO"
        R2_T1 = "DEPOSITO: " + cbDeposito.Text
        R2_T2 = "ALMACEN: " + cbAlmacen.Text
        R3_T1 = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "CANTIDAD DE ITEM: " + DataGridView1.RowCount.ToString

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
        For I = 0 To DataGridView1.RowCount - 1
            e.Graphics.DrawString(Me.DataGridView1.Item(0, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 80, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 680, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(3, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 730, LINEA)
            LINEA += SALTO
        Next

    End Sub


    
End Class