Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA005
    Private DS_material As New DataSet
    Private DS_almacen1 As New DataSet
    Private DS_almacen2 As New DataSet
    Private _MATERIAL As String
    Private _ALMACEN1 As String
    Private _ALMACEN2 As String
    Private _NREMITO As Decimal
    Private _FECHA As Date
    Private _CANT As Decimal
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private _CantItem As Integer = 0
    Private Sub PALAMA005_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        _CantItem = 0
        llenar_DS_ALMACEN()
        LLENAR_CB_ALMACEN()
        MAIN.serie.Clear()
        MAIN.material.Clear()
    End Sub
    '################################### FUNCIONES ########################################################
    Private Sub borrar()
        tbCantidad.Text = Nothing
        cbPara.Text = Nothing
        cbDesde.Text = Nothing
        cbMaterial.DataSource = Nothing
        cbMaterial.Text = Nothing
        DS_material.Clear()
        DataGridView1.Rows.Clear()
        txtDisponible.Text = Nothing
        lbUnidad.Text = Nothing
        MAIN.serie.Clear()
        MAIN.material.Clear()
        cbPara.Enabled = False
        cbMaterial.Enabled = False
        tbCantidad.Enabled = False
        btConfirmar.Enabled = False
        cbDesde.Enabled = True
        cbDesde.Focus()
        btAgregar.Enabled = True
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
        Dim R1_T1 As String = "TIPO DE MOVIMIENTO: TRANSFERENCIA DE MATERIALES"
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = "DESDE: " + cbDesde.Text
        Dim R2_T2 As String = "A: " + cbPara.Text
        Dim R3_T1 As String = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        Dim R3_T2 As String = "CANTIDAD DE ITEM: " + DataGridView1.RowCount.ToString

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
    Private Sub LLENAR_CB_MATERIAL()
        cbMaterial.DataSource = DS_material.Tables("T_ALMA_103")
        cbMaterial.DisplayMember = "des"
        cbMaterial.ValueMember = "cod"
        cbMaterial.Text = Nothing
    End Sub
    Private Sub llenar_DS_ALMACEN2()
        DS_almacen2.Clear()
        If _usr.Obt_Almacen = "0" Then
            'CONECTO LA BASE
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            'ABRO LA BASE
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE F_BAJA_003 is NULL order by NOMBRE", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptadaor.Fill(DS_almacen2, "M_PERS_003")
            cnn2.Close()
        Else
            'CONECTO LA BASE
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            'ABRO LA BASE
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE (ALMA_003 = 1 or NDOC_003=@D1) and F_BAJA_003 is NULL order by NOMBRE", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", _usr.Obt_Almacen))
            adaptadaor.Fill(DS_almacen2, "M_PERS_003")
            cnn2.Close()
        End If
        Dim INDICE As Integer = 0
        For I = 0 To DS_almacen2.Tables("M_PERS_003").Rows.Count - 1
            If DS_almacen2.Tables("M_PERS_003").Rows(I).Item("NDOC_003").ToString() = _ALMACEN1 Then
                INDICE = I
            End If
        Next
        DS_almacen2.Tables("M_PERS_003").Rows.RemoveAt(INDICE)
    End Sub
    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE ALMA_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen1, "M_PERS_003")
        cnn2.Close()
    End Sub
    Private Sub LLENAR_CB_ALMACEN()
        cbDesde.DataSource = DS_almacen1.Tables("M_PERS_003")
        cbDesde.DisplayMember = "NOMBRE"
        cbDesde.ValueMember = "NDOC_003"
        cbDesde.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_ALMACEN2()
        cbPara.DataSource = DS_almacen2.Tables("M_PERS_003")
        cbPara.DisplayMember = "NOMBRE"
        cbPara.ValueMember = "NDOC_003"
        cbPara.Text = Nothing
    End Sub

    '################################### BOTONES ##########################################################
    Private Sub btConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btConfirmar.Click
        If DataGridView1.RowCount > 0 Then

            Try
                _NREMITO = Metodos.Obtener_Numero_Remito
                _FECHA = Date.Now
                'inicio todos los datos
                btAgregar.Enabled = True
                tbCantidad.Text = Nothing
                _CantItem = 0
                'INCREMENTO EL NUMERO DE REMITO
                Metodos.Sumar_Num_Remito()
                For i = 0 To Me.DataGridView1.Rows.Count - 1
                    _MATERIAL = Me.DataGridView1.Item(0, i).Value
                    _CANT = Me.DataGridView1.Item(3, i).Value
                    Metodos.Descontar_Stock_Material(_MATERIAL, _ALMACEN1, _CANT, 1)
                    Metodos.Increpmentar_Stock_Material(_MATERIAL, _ALMACEN2, _CANT, 1)
                    Metodos.Grabar_Trans(_NREMITO, _FECHA, _MATERIAL, _ALMACEN1, _ALMACEN2, 3, _FECHA, 0, "", 0, _CANT, 0, _usr.Obt_Usr, "", "")
                    'si es serializado se agraga un material serializado sin asignar numero
                    If Medidor.Es_Serializado(_MATERIAL) = True Then
                        For G = 0 To MAIN.serie.Count - 1
                            If MAIN.material.Item(G) = _MATERIAL Then
                                Medidor.Grabar_Mov_Medi(MAIN.serie.Item(G), _MATERIAL, _NREMITO, Date.Now, _ALMACEN1, _ALMACEN2)
                                Medidor.MODIFICAR_MEDIDOR(MAIN.serie.Item(G), _MATERIAL, _ALMACEN2, Date.Now, _usr.Obt_Usr)
                                'Medidor.MODIFICAR_MEDIDOR_UTILIZADO(MAIN.serie.Item(G), _MATERIAL, _ALMACEN, DateTimePicker1.Value, _usr.Obt_Usr)
                            End If
                        Next
                    End If
                Next
                'equipo
                PrintDocument1.Print()
                borrar()
                MENSAJE.MADVE004(_NREMITO) ''mensaje de confirmacion
            Catch ex As Exception
                MENSAJE.MERRO001()
            End Try
        Else
            MENSAJE.MERRO011()
        End If
    End Sub
    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEliminar.Click
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
            LLENAR_CB_MATERIAL()
            cbMaterial.Text = Nothing
            lbUnidad.Text = Nothing
            tbCantidad.Text = Nothing
            tbCantidad.Enabled = False
            txtDisponible.Text = Nothing
            _CantItem -= 1
            If _CantItem <> 20 Then
                btAgregar.Enabled = True
            End If
            If DataGridView1.Rows.Count = 0 Then
                btConfirmar.Enabled = False
            End If
            MENSAJE.MADVE001()
        End If
    End Sub
    Private Sub btBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        borrar()
    End Sub
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
                                        Dim pantalla As New PALMA004BIS
                                        pantalla.grabardatos(_MATERIAL, CDec(var), _ALMACEN1, 1)
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
                                            txtDisponible.Text = Nothing
                                            lbUnidad.Text = Nothing
                                            tbCantidad.Text = Nothing
                                            tbCantidad.Enabled = False
                                            'verifico que sean menos de 20  items
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
                                            txtDisponible.Text = Nothing
                                            lbUnidad.Text = Nothing
                                            tbCantidad.Enabled = False
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
                                        txtDisponible.Text = Nothing
                                        lbUnidad.Text = Nothing
                                        tbCantidad.Text = Nothing
                                        tbCantidad.Enabled = False
                                        'verifico que sean menos de 20  items
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
    End Sub
 

    '################################## COMBOBOX Y TEXBOX ##################################################


    Private Sub cbEquipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDesde.SelectedIndexChanged
        If cbDesde.ValueMember <> Nothing And cbDesde.Text <> Nothing Then
            _ALMACEN1 = cbDesde.SelectedValue
            cbPara.Enabled = True
            llenar_DS_ALMACEN2()
            LLENAR_CB_ALMACEN2()
            llenar_DS_MATERIAL(_ALMACEN1)
            LLENAR_CB_MATERIAL()
            cbMaterial.Text = Nothing
            tbCantidad.Text = Nothing
            tbCantidad.Enabled = False
            lbUnidad.Text = Nothing
            txtDisponible.Text = Nothing
        End If

    End Sub
    Private Sub cbPara_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPara.SelectedIndexChanged
        If cbPara.ValueMember <> Nothing And cbPara.Text <> Nothing Then
            _ALMACEN2 = cbPara.SelectedValue
            cbMaterial.Enabled = True
        End If
    End Sub


    Private Sub cbMaterial_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.SelectedIndexChanged
        If cbMaterial.ValueMember <> Nothing Then
            If cbMaterial.Text <> Nothing Then
                _MATERIAL = cbMaterial.SelectedValue
                If cbDesde.Text <> Nothing Then
                    txtDisponible.Text = Metodos.Saldo(_MATERIAL, _ALMACEN1, 1)
                    tbCantidad.Enabled = True
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

  
End Class