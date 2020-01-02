Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA004
    Private DS_material As New DataSet
    Private DS_almacen As New DataSet
    Private DS_contrato As New DataSet
    Private _CONTRATO As String
    Private _MATERIAL As String
    Private _ALMACEN As String
    Private _FECHA As Date
    Private _NREMITO As Decimal
    Private _CANT As Decimal
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor

    '###########################SELECCIONES#################################################3

    Private Sub PALMA004_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MAIN.serie.Clear()
        MAIN.material.Clear()
        DateTimePicker1.MaxDate = Date.Now
        DateTimePicker1.MinDate = DateAdd(DateInterval.Month, -1, Date.Now)
        DateTimePicker1.Value = DateAdd(DateInterval.Day, -1, Date.Now)
        Try
            Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
            Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
            llenar_DS_Contrato()
            LLENAR_CB_CONTRATO()
            llenar_DS_ALMACEN()
            LLENAR_CB_ALMACEN()
            cbContrato.Enabled = False
            tbCantidad.Enabled = False
            cbMaterial.Enabled = False
            MAIN.serie.Clear()
            MAIN.material.Clear()
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub

    '#######################BOTONES#################################BOTONES###################
    'BOTON SALIR
    
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
                cbMaterial.Text = Nothing
                LLENAR_CB_MATERIAL()
                lbUnidad.Text = Nothing
                txtDisponible.Text = Nothing
                tbCantidad.Text = Nothing
                tbCantidad.Enabled = False
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
                    _NREMITO = Metodos.Obtener_Numero_Remito
                    _FECHA = Date.Now
                    Metodos.Sumar_Num_Remito()
                    'inicio todos los datos
                    For i = 0 To Me.DataGridView1.Rows.Count - 1
                        _MATERIAL = Me.DataGridView1.Item(0, i).Value
                        _CANT = Me.DataGridView1.Item(3, i).Value
                        Metodos.Descontar_Stock_Material(_MATERIAL, _ALMACEN, _CANT, 1)
                        Metodos.Grabar_Trans(_NREMITO, _FECHA, _MATERIAL, _ALMACEN, _ALMACEN, 2, DateTimePicker1.Value.ToShortDateString, 0, "", 0, _CANT, 0, _usr.Obt_Usr, _CONTRATO, "")
                        'si es serializado se agraga un material serializado sin asignar numero
                        If Medidor.Es_Serializado(_MATERIAL) = True Then
                            For G = 0 To MAIN.serie.Count - 1
                                If MAIN.material.Item(G) = _MATERIAL Then
                                    Medidor.MODIFICAR_MEDIDOR_UTILIZADO(MAIN.serie.Item(G), _MATERIAL, _ALMACEN, DateTimePicker1.Value.ToShortDateString, _usr.Obt_Usr)
                                End If
                            Next
                        Else
                            Metodos.Descontar_Stock_Contrato(_MATERIAL, _CONTRATO, _CANT)
                        End If
                    Next
                    Medidor.ESTADO_5()
                    ' PrintDocument1.Print()
                    borrar()
                    MENSAJE.MADVE004(_NREMITO) ''mensaje de confirmacion
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
    Private Sub btAgregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAgregar.Click
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

                                                cbEquipo.Enabled = False
                                                cbContrato.Enabled = False
                                                'agrego al data griview
                                                DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var))
                                                DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                                cbMaterial.DataSource = Nothing
                                                LLENAR_CB_MATERIAL()
                                                lbUnidad.Text = Nothing
                                                txtDisponible.Text = Nothing
                                                tbCantidad.Text = Nothing
                                                tbCantidad.Enabled = False
                                                
                                                ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                                If DataGridView1.Rows.Count <> 0 Then
                                                    btConfirmar.Enabled = True
                                                End If
                                                cbMaterial.Focus()
                                            Else
                                                cbMaterial.Text = Nothing
                                                tbCantidad.Text = Nothing
                                                lbUnidad.Text = Nothing
                                                txtDisponible.Text = Nothing
                                                tbCantidad.Enabled = False
                                                cbMaterial.Focus()
                                            End If
                                        Else
                                            'sumo 1 a la cantidad de item

                                            cbEquipo.Enabled = False
                                            cbContrato.Enabled = False
                                            'agrego al data griview
                                            DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var))
                                            DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                            cbMaterial.DataSource = Nothing
                                            LLENAR_CB_MATERIAL()
                                            lbUnidad.Text = Nothing
                                            txtDisponible.Text = Nothing
                                            tbCantidad.Text = Nothing
                                            tbCantidad.Enabled = False
                                           
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
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        borrar()
    End Sub


    '##############################FUNCIONES###########################FUNCIONES###########################
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
    Private Sub llenar_DS_Contrato()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NCONT_004, DESC_004 FROM M_CONT_004 where F_BAJA_004 is NULL order by DESC_004", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_CONT_004")
        cnn2.Close()
    End Sub
    Private Sub LLENAR_CB_ALMACEN()
        cbEquipo.DataSource = DS_almacen.Tables("M_PERS_003")
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
    Private Sub LLENAR_CB_CONTRATO()
        cbContrato.DataSource = DS_contrato.Tables("M_CONT_004")
        cbContrato.DisplayMember = "DESC_004"
        cbContrato.ValueMember = "NCONT_004"
        cbContrato.Text = Nothing
    End Sub

    Public Sub borrar()
        _CANT = 0
        cbEquipo.Text = Nothing
        cbMaterial.Text = Nothing
        cbMaterial.DataSource = Nothing
        cbContrato.Text = Nothing
        txtDisponible.Text = Nothing
        lbUnidad.Text = Nothing
        tbCantidad.Text = Nothing
        DataGridView1.Rows.Clear()
        MAIN.serie.Clear()
        MAIN.material.Clear()
        cbEquipo.Enabled = True
        cbContrato.Enabled = False
        cbMaterial.Enabled = False
        tbCantidad.Enabled = False
        btConfirmar.Enabled = False
        cbEquipo.Focus()
        btAgregar.Enabled = True
    End Sub
    '################### COMBO BOX #######################################

    Private Sub cbContrato_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbContrato.SelectedIndexChanged
        If cbContrato.ValueMember <> Nothing Then
            If cbContrato.Text <> Nothing Then
                _CONTRATO = cbContrato.SelectedValue
                cbMaterial.Enabled = True
            End If
        End If
    End Sub
    Private Sub cbMaterial_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.SelectedIndexChanged
        If cbMaterial.ValueMember <> Nothing Then
            If cbMaterial.Text <> Nothing Then
                _MATERIAL = cbMaterial.SelectedValue
                If cbEquipo.Text <> Nothing Then
                    txtDisponible.Text = Metodos.Saldo(_MATERIAL, _ALMACEN, 1)
                    tbCantidad.Enabled = True
                    lbUnidad.Text = Metodos.Unidad(_MATERIAL)
                    tbCantidad.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub cbEquipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbEquipo.SelectedIndexChanged
        If cbEquipo.ValueMember <> Nothing And cbEquipo.Text <> Nothing Then
            cbContrato.Enabled = True
            _ALMACEN = cbEquipo.SelectedValue
            llenar_DS_MATERIAL(_ALMACEN)
            LLENAR_CB_MATERIAL()
            tbCantidad.Enabled = False
            txtDisponible.Text = Nothing

        End If
    End Sub

    Private Sub tbCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbCantidad.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btAgregar.Focus()
        End If
    End Sub



   



    
   
    
    Private Sub tbCantidad_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbCantidad.TextChanged

    End Sub
End Class