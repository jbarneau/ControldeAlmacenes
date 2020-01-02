
Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA002
    Private DS_material As New DataSet
    Private DS_almacen As New DataTable
    Private DS_deposito As New DataSet
    Private _DEPOSITO As String
    Private _MATERIAL As String
    Private _ALMACEN As String
    Private _CANT As Decimal
    Private _NREMITO As Decimal
    Private _FECHA As Date
    Private _CantItem As Integer = 0
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Dim lista As New List(Of Clase_mensaje)()
    Private PASO As Boolean = False
    Private RESPUESTA As Boolean = False
    Private BODY As String
    Private TABLABODY As String
    Private Hora As Integer = 10


#Region "####### FUNCIONES ######"
    Private Sub AGREGAR(ByVal VAR As String)
        'sumo 1 a la cantidad de item
        _CantItem += 1
        'agrego al data griview
        DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(VAR), (CDec(VAR) + CDec(txtStock.Text)))
        DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
        'empieso a poner en cero la cosa
        'vacio el cmbobox de material y lo vuelvo a llenar
        cbMaterial.DataSource = Nothing
        LLENAR_CB_MATERIAL()
        TextBox2.Text = Nothing
        txtDisponible.Text = Nothing
        txtStock.Text = Nothing
        'bloqueo el equipo y el deposito
        cbEquipo.Enabled = False
        cbDeposito.Enabled = False
        'el uadro del la cantidad la pongo vacia y la bloqueo
        tbCantidad.Text = Nothing
        tbCantidad.Enabled = False
        lbUnidad.Text = Nothing
        'verifico que sean menos de 20  items
        If _CantItem = 20 Then
            btAgregar.Enabled = False
        End If
        ' verifico que el data gridview no este vacio, y actibo el boton de entregar
        If DataGridView1.Rows.Count <> 0 Then
            btConfirmar.Enabled = True
        End If
        cbMaterial.Focus()
    End Sub

    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE ALMA_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen)
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
        cbEquipo.DataSource = DS_almacen
        cbEquipo.DisplayMember = "NOMBRE"
        cbEquipo.ValueMember = "NDOC_003"
        cbEquipo.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_MATERIAL()
        cbMaterial.DataSource = DS_material.Tables("T_ALMA_103")
        cbMaterial.DisplayMember = "des"
        cbMaterial.ValueMember = "cod"
        cbMaterial.Text = Nothing
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        '#######################datos##################################
        Dim n_REMITO As String
        Dim R1_T1 As String = "TIPO DE MOVIMIENTO: ENTREGA"
        Dim R1_T2 As String = "MOTIVO: SIN MOTIVO"
        Dim R2_T1 As String = "DEPOSITO: " + cbDeposito.Text
        Dim R2_T2 As String = "ALMACEN: " + cbEquipo.Text
        Dim R3_T1 As String = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        Dim R3_T2 As String = "CANTIDAD DE ITEM: " + DataGridView1.RowCount.ToString
        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + _NREMITO.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + _NREMITO.ToString.PadLeft(8, "0")
        End If

        'IMAGEN ######################################
        e.Graphics.DrawImage(REMITO_IMAGEN, 0, 0, 800, 1140)
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
        e.Graphics.DrawString("U", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 640, 325)
        e.Graphics.DrawString("CANT", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 655, 325)
        e.Graphics.DrawString("STOCK", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 720, 325)

        'RECORRO EL DATA
        For I = 0 To DataGridView1.RowCount - 1
            e.Graphics.DrawString(Me.DataGridView1.Item(0, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 80, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 640, LINEA)
            e.Graphics.DrawString(FormatNumber(Me.DataGridView1.Item(3, I).Value, 2).ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 657, LINEA)
            e.Graphics.DrawString(FormatNumber(Me.DataGridView1.Item(4, I).Value, 2).ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 725, LINEA)

            LINEA += SALTO
        Next
        If RESPUESTA = True Then
            Dim can As Integer = 1
            Dim asigmed As String = ""
            LINEA = 984
            For i = 0 To MAIN.serie.Count - 1
                If asigmed = "" Then
                    asigmed = MAIN.serie(i).ToString
                Else
                    asigmed = asigmed + "/" + MAIN.serie(i).ToString
                End If
                If can = 8 Or i = MAIN.serie.Count - 1 Then
                    e.Graphics.DrawString(asigmed.ToString, New Font("ARIAL", 9, FontStyle.Regular), Brushes.Black, 15, LINEA)
                    asigmed = asigmed
                    asigmed = ""
                    can = 1
                    LINEA += 15
                Else
                    can += 1
                End If
            Next
        End If
    End Sub
    Private Sub borrar()
        PASO = False
        RESPUESTA = False
        MAIN.serie.Clear()
        MAIN.material.Clear()
        TextBox2.Enabled = False
        _CantItem = 0
        btConfirmar.Enabled = False
        tbCantidad.Enabled = False
        tbCantidad.Text = Nothing
        lbUnidad.Text = Nothing
        txtDisponible.Text = Nothing
        Me.DataGridView1.Rows.Clear()
        cbEquipo.Text = Nothing
        If _usr.Obt_Almacen = "0" Then
            cbDeposito.Text = Nothing
            cbDeposito.Enabled = True
            cbDeposito.Focus()
            cbEquipo.Enabled = False
        Else
            cbEquipo.Enabled = True
            cbEquipo.Focus()
            llenar_DS_MATERIAL(_DEPOSITO)
            LLENAR_CB_MATERIAL()
        End If
        'materiales
        btAgregar.Enabled = True
        cbMaterial.Text = Nothing
        cbMaterial.Enabled = False
    End Sub
#End Region
    Private Sub PALMA002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
            Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
            _CantItem = 0
            MAIN.serie.Clear()
            MAIN.material.Clear()
            'consulto si el usuario tiene deposito asignado
            If _usr.Obt_Almacen <> "0" Then

                cbDeposito.DropDownStyle = ComboBoxStyle.DropDown
                _DEPOSITO = _usr.Obt_Almacen
                'escribo el nombre del deposito
                cbDeposito.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO)
                'me paro en el equipo
                cbEquipo.Enabled = True
                cbEquipo.Focus()
                cbDeposito.Enabled = False
                'lleno el combobox de los materiales
                llenar_DS_MATERIAL(_DEPOSITO)
                LLENAR_CB_MATERIAL()
            Else
                'activo el combobox del deposito
                cbDeposito.Enabled = True
                'lleno el combo box del deposito
                llenar_DS_DEPOSITO()
                LLENAR_CB_DEPOSITO()
                cbDeposito.Focus()
                'desactivo el combobox del equitoi
                cbEquipo.Enabled = False
            End If
            'lleno el combo box del los equipos
            llenar_DS_ALMACEN()
            LLENAR_CB_ALMACEN()
            tbCantidad.Enabled = False
            cbMaterial.Enabled = False
            txtDisponible.Text = Nothing
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
#Region "###### COMBO BOX ######"
    Private Sub cbDeposito_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDeposito.SelectedIndexChanged
        'VERIFICO SI EL DEPOSITO SE CAMBIO Y QUE NO ESTE VACIO
        If cbDeposito.ValueMember <> Nothing And cbDeposito.Text <> Nothing Then
            _DEPOSITO = cbDeposito.SelectedValue
            cbEquipo.Enabled = True
            llenar_DS_MATERIAL(_DEPOSITO)
            LLENAR_CB_MATERIAL()
            txtDisponible.Text = Nothing
            lbUnidad.Text = Nothing
            tbCantidad.Enabled = False
        End If
    End Sub
    Private Sub cbEquipo_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEquipo.SelectedIndexChanged
        If cbEquipo.ValueMember <> Nothing And cbEquipo.Text <> Nothing Then
            cbMaterial.Enabled = True
            TextBox2.Enabled = True
            _ALMACEN = cbEquipo.SelectedValue
            If cbEquipo.Text <> Nothing And cbMaterial.Text <> Nothing Then
                txtDisponible.Text = Metodos.Saldo(_MATERIAL, _DEPOSITO, 1)
                lbUnidad.Text = Metodos.Unidad(_MATERIAL)
            End If
        End If
    End Sub
    Private Sub cbMaterial_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.SelectedIndexChanged
        If cbMaterial.ValueMember <> Nothing Then
            _MATERIAL = cbMaterial.SelectedValue
            If cbEquipo.Text <> Nothing And cbMaterial.Text <> Nothing Then
                tbCantidad.Enabled = True
                txtDisponible.Text = Metodos.Saldo(_MATERIAL, _DEPOSITO, 1)
                txtStock.Text = Metodos.Saldo(_MATERIAL, _ALMACEN, 1)
                lbUnidad.Text = Metodos.Unidad(_MATERIAL)
                tbCantidad.Focus()
            End If
        End If
    End Sub
#End Region
#Region "###### BOTONES ######"
    'BOTON DE SALIR 
    Private Sub btSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub btAgragar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAgregar.Click
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
                            If Metodos.tiene_decimal(_MATERIAL, var) = True Then
                                'verifico que la cantidad se mayor a 0
                                If CDec(var) > 0 Then
                                    'verifico que la cantidad sea menor a la disponible
                                    If CDec(var) <= CDec(txtDisponible.Text) Then
                                        Dim max As Decimal = ObtMax(_ALMACEN, _MATERIAL)
                                        If (CDec(var) + CDec(txtStock.Text) > max) Then
                                            If max > 0 Then
                                                MENSAJE.MADVE006(max.ToString)
                                                Dim item As New Clase_mensaje With {
                                                .dniope = cbEquipo.SelectedValue,
                                                .nomope = cbEquipo.Text,
                                                .material = cbMaterial.SelectedValue,
                                                .descmate = cbMaterial.Text,
                                                .stockmax = max,
                                                .apedir = CDec(var),
                                                .stockope = txtStock.Text
                                                }
                                                lista.Add(item)
                                            End If
                                        End If
                                        If Medidor.Es_Serializado(_MATERIAL) Then
                                            If Medidor.Serializados_sin_asignar(_MATERIAL, _ALMACEN) = False Then
                                                If PASO = False Then
                                                    Dim res As DialogResult
                                                    res = MessageBox.Show("¿DESEA ASIGNAR MEDIDORES?", "MATI001", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                                                    If res = System.Windows.Forms.DialogResult.Yes Then
                                                        RESPUESTA = True
                                                    End If
                                                    PASO = True
                                                End If
                                                If RESPUESTA = True Then
                                                    Dim PANTALLA As New PALMA013BIS
                                                    PANTALLA.grabardatos(_MATERIAL, CDec(var), _DEPOSITO, 1)
                                                    PANTALLA.ShowDialog()
                                                    If PANTALLA.validar = True Then
                                                        AGREGAR(var)
                                                    Else
                                                        TextBox2.Text = Nothing
                                                        cbMaterial.Text = Nothing
                                                        tbCantidad.Text = Nothing
                                                        tbCantidad.Enabled = False
                                                        txtDisponible.Text = Nothing
                                                        txtStock.Text = Nothing
                                                        lbUnidad.Text = Nothing
                                                        cbMaterial.Focus()
                                                    End If
                                                Else
                                                    AGREGAR(var)
                                                End If
                                            Else
                                                MENSAJE.MERRO012()
                                                TextBox2.Text = Nothing
                                                cbMaterial.Text = Nothing
                                                tbCantidad.Text = Nothing
                                                tbCantidad.Enabled = False
                                                txtDisponible.Text = Nothing
                                                txtStock.Text = Nothing
                                                lbUnidad.Text = Nothing
                                            End If
                                        Else
                                            AGREGAR(var)
                                        End If
                                        'copias hasta aca lo del maximo
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
    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEliminar.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                MENSAJE.MERRO011()
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
                ''''''''''''
                lista.RemoveAt(DataGridView1.CurrentRow.Index)
                cbMaterial.Text = Nothing
                LLENAR_CB_MATERIAL()
                lbUnidad.Text = Nothing
                txtDisponible.Text = Nothing
                tbCantidad.Text = Nothing
                tbCantidad.Enabled = False
                txtStock.Text = Nothing
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
    Private Sub btConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btConfirmar.Click
        Try
            If DataGridView1.RowCount > 0 Then
                Try
                    'inicio todos los datos
                    _CantItem = 0
                    _NREMITO = Metodos.Obtener_Numero_Remito
                    _FECHA = Date.Now
                    Metodos.Sumar_Num_Remito()
                    For i = 0 To Me.DataGridView1.Rows.Count - 1
                        _MATERIAL = Me.DataGridView1.Item(0, i).Value
                        _CANT = Me.DataGridView1.Item(3, i).Value
                        Metodos.Increpmentar_Stock_Material(_MATERIAL, _ALMACEN, _CANT, 1)
                        Metodos.Descontar_Stock_Material(_MATERIAL, _DEPOSITO, _CANT, 1)
                        Metodos.Grabar_Trans(_NREMITO, _FECHA, _MATERIAL, _DEPOSITO, _ALMACEN, 9, _FECHA, 0, "", 1, _CANT, 0, _usr.Obt_Usr, "", "")
                        'si es serializado se agraga un material serializado sin asignar numero
                        If Medidor.Es_Serializado(_MATERIAL) = True Then
                            If RESPUESTA = True Then
                                For G = 0 To MAIN.serie.Count - 1
                                    If MAIN.material.Item(G) = _MATERIAL Then
                                        Medidor.Grabar_Mov_Medi(MAIN.serie.Item(G), _MATERIAL, _NREMITO, Date.Now, _DEPOSITO, _ALMACEN)
                                        Medidor.MODIFICAR_MEDIDOR(MAIN.serie.Item(G), _MATERIAL, _ALMACEN, Date.Now, _usr.Obt_Usr)
                                        'Medidor.MODIFICAR_MEDIDOR_UTILIZADO(MAIN.serie.Item(G), _MATERIAL, _ALMACEN, DateTimePicker1.Value, _usr.Obt_Usr)
                                    End If
                                Next

                            Else
                                Medidor.Grabar_Med_Sin_Asignar(_ALMACEN, _MATERIAL, _CANT, _NREMITO, _FECHA, 0)
                            End If

                        End If
                    Next
                    PrintDocument1.Print()
                    PrintDocument1.Print()
                    'equipo
                    borrar()
                    MENSAJE.MADVE004(_NREMITO) ''mensaje de confirmacion
                    lista = lista.Distinct().ToList()
                    'Metodos.AgregarAlertaStockMax(lista)
                    lista.Clear()
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
    Private Sub btBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        borrar()
    End Sub
#End Region
    Private Sub tbCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbCantidad.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btAgregar.Focus()

        End If
    End Sub


    Private Sub TextBox2_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            cbMaterial.SelectedValue = TextBox2.Text
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

End Class