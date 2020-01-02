Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql


Public Class PALMA009
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
    Private _CantItem As Integer = 0
    Private DescMotivo As String



    Private Sub PALMA009_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
            Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
            _CantItem = 0

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
            LLENAR_CBMOTIVO()
            tbCantidad.Enabled = False
            cbMaterial.Enabled = False
            txtDisponible.Text = Nothing
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub

    '#####################################BOTONES################################
   
    Private Sub btAgregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAgregar.Click
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
                                    'sumo 1 a la cantidad de item
                                    _CantItem += 1
                                    'agrego al data griview
                                    If cbMotivo.SelectedValue IsNot Nothing Then
                                        DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var), cbMotivo.SelectedValue)
                                        DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                        cbMaterial.DataSource = Nothing
                                        LLENAR_CB_MATERIAL()
                                        cbMaterial.Text = Nothing
                                        tbCantidad.Text = Nothing
                                        DescMotivo = cbMotivo.Text
                                        cbMotivo.Text = Nothing
                                        txtDisponible.Text = Nothing
                                        lbUnidad.Text = Nothing
                                        tbCantidad.Enabled = False
                                        cbMotivo.Enabled = False
                                        tbCantidad.Enabled = False
                                        cbEquipo.Enabled = False
                                        cbDeposito.Enabled = False
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
                                        MessageBox.Show("SELECCIONES UN MOTIVO DE ENTREGA!!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Private Sub btEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEliminar.Click
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
                cbMotivo.Enabled = False
                cbMotivo.Text = Nothing
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
        Dim _MOTIVO As String
        Try
            If DataGridView1.RowCount > 0 Then
                _NREMITO = Metodos.Obtener_Numero_Remito
                _FECHA = Date.Now
                Metodos.Sumar_Num_Remito()
                For i = 0 To Me.DataGridView1.Rows.Count - 1
                    _MATERIAL = Me.DataGridView1.Item(0, i).Value
                    _CANT = Me.DataGridView1.Item(3, i).Value
                    _MOTIVO = Me.DataGridView1.Item(4, i).Value
                    Metodos.Descontar_Stock_Material(_MATERIAL, _DEPOSITO, _CANT, 1)
                    Metodos.Grabar_Trans(_NREMITO, _FECHA, _MATERIAL, _DEPOSITO, _ALMACEN, 1, _FECHA, 0, "", _MOTIVO, _CANT, 0, _usr.Obt_Usr, "", "")
                    If _MOTIVO = 1 Then
                        Metodos.Increpmentar_Stock_Material(_MATERIAL, _ALMACEN, _CANT, 1)
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


    '#########################################FUNSIONES############################FUNSIONES#########################
    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE DEPO_003 =0 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
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
        Dim adaptadaor As New SqlClient.SqlDataAdapter("SELECT T_ALMA_103.C_MATE_103 as cod, M_MATE_002.DESC_002 as des FROM T_ALMA_103 INNER JOIN M_MATE_002 ON T_ALMA_103.C_MATE_103 = M_MATE_002.CMATE_002 WHERE (T_ALMA_103.ESTA_103 = 1) AND (T_ALMA_103.C_ALMA_103 = @D1) AND (M_MATE_002.TIPO_002 = 3) AND (T_ALMA_103.N_CANT_103 > 0) order by M_MATE_002.DESC_002", cnn2)
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
    Private Sub LLENAR_CBMOTIVO()
        Dim DATASET1 As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 4 AND C_PARA_802 != 0 AND F_BAJA_802 is NULL order by DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DATASET1, "DET_PARAMETRO_802")
        cnn2.Close()
        cbMotivo.DataSource = DATASET1.Tables("DET_PARAMETRO_802")
        cbMotivo.DisplayMember = "DESC_802"
        cbMotivo.ValueMember = "C_PARA_802"
        cbMotivo.Text = Nothing
    End Sub
    Private Sub borrar()
        _CantItem = 0
        btConfirmar.Enabled = False
        tbCantidad.Enabled = False
        tbCantidad.Text = Nothing
        lbUnidad.Text = Nothing
        txtDisponible.Text = Nothing
        cbMotivo.Text = Nothing

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
        cbMaterial.Text = Nothing
        cbMaterial.Enabled = False
        cbMotivo.Enabled = False
        btAgregar.Enabled = True
    End Sub
  



    '################################COMBOBOX##################################
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
                lbUnidad.Text = Metodos.Unidad(_MATERIAL)
                tbCantidad.Focus()
            End If
        End If
    End Sub
    Private Sub TBcANTIDAD_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbCantidad.TextChanged
        cbMotivo.Enabled = True
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
        End If


        'DEFINO LAS VARIABLES
        R1_T1 = "TIPO DE MOVIMIENTO: ENTREGA DE HERRAMIENTA"
        R1_T2 = "MOTIVO:" + DescMotivo
        R2_T1 = "DEPOSITO: " + cbDeposito.Text
        R2_T2 = "PERSONAL: " + cbEquipo.Text
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