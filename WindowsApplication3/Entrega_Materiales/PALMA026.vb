Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA026

    Private DS_material As New DataSet
    Private DS_almacen As New DataSet
    Private DS_deposito As New DataSet
    Private _DEPOSITO As String
    Private _MATERIAL As String
    Private _recibe As String
    Private _CANT As Decimal
    Private _nremito As Decimal
    Private _fecha As Date
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private _CantItem As Integer = 0
    Private Sub PALMA026_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        _CantItem = 0
        If _usr.Obt_Almacen <> "0" Then
            cbDeposito.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO = _usr.Obt_Almacen
            cbDeposito.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO)
            cbDeposito.Enabled = False
            llenar_DS_MATERIAL(_DEPOSITO)
            LLENAR_CB_MATERIAL()
            lbUnidad.Text = Nothing
            txtDisponible.Text = Nothing
            tbRecibe.Enabled = True
            tbRecibe.Focus()
        Else
            cbDeposito.Enabled = True
            llenar_DS_DEPOSITO()
            LLENAR_CB_DEPOSITO()
            cbDeposito.Focus()
        End If
        tbCantidad.Enabled = False
        cbMaterial.Enabled = False
    End Sub
   








    '################################## FUNCIONES ########################################
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
        Dim adaptadaor As New SqlClient.SqlDataAdapter("SELECT T_ALMA_103.C_MATE_103 as cod, M_MATE_002.DESC_002 as des FROM T_ALMA_103 INNER JOIN M_MATE_002 ON T_ALMA_103.C_MATE_103 = M_MATE_002.CMATE_002 WHERE (T_ALMA_103.ESTA_103 = 1) AND (M_MATE_002.SERI_002=0) AND (T_ALMA_103.C_ALMA_103 = @D1) AND (T_ALMA_103.N_CANT_103 > 0) order by M_MATE_002.DESC_002", cnn2)
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
    Private Sub LLENAR_CB_MATERIAL()

        cbMaterial.DataSource = DS_material.Tables("T_ALMA_103")
        cbMaterial.DisplayMember = "des"
        cbMaterial.ValueMember = "cod"
        cbMaterial.Text = Nothing
    End Sub
    Private Function Minusculas(ByVal Texto As String, ByVal TXT As TextBox) As String
        Minusculas = UCase(Texto) ' LCase se encarga de transformar el texto en minuscula UCase a mayuscula
        TXT.SelectionStart = Len(Texto) ' Dejamos el cursor al final del texto 
    End Function
    Private Sub borrar()
        _CANT = 0
        DS_material.Clear()
        tbRecibe.Text = Nothing
        lbUnidad.Text = Nothing
        txtDisponible.Text = Nothing
        tbCantidad.Text = Nothing
        cbMaterial.Text = Nothing
        cbMaterial.DataSource = Nothing
        DataGridView1.Rows.Clear()
        cbMaterial.Enabled = False
        tbCantidad.Enabled = False
        btConfirmar.Enabled = False
        If _usr.Obt_Almacen = "0" Then
            tbRecibe.Enabled = False
            cbDeposito.Text = Nothing
            cbDeposito.Enabled = True
            cbDeposito.Focus()
        Else
            tbRecibe.Enabled = True
            tbRecibe.Focus()
            llenar_DS_MATERIAL(_DEPOSITO)
            LLENAR_CB_MATERIAL()
            txtDisponible.Text = Nothing
            tbCantidad.Enabled = False
            lbUnidad.Text = Nothing
            btAgregar.Enabled = True
        End If


    End Sub

    '################## COMBOBOX ############################################################
    Private Sub cbDeposito_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDeposito.SelectedIndexChanged
        If cbDeposito.ValueMember <> Nothing And cbDeposito.Text <> Nothing Then
            _DEPOSITO = cbDeposito.SelectedValue
            tbRecibe.Enabled = True
            llenar_DS_MATERIAL(_DEPOSITO)
            LLENAR_CB_MATERIAL()
            lbUnidad.Text = Nothing
            tbCantidad.Enabled = False
            txtDisponible.Text = Nothing
            cbMaterial.Enabled = False
        End If
    End Sub
    Private Sub tbRecibe_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbRecibe.TextChanged
        tbRecibe.Text = Minusculas(tbRecibe.Text, tbRecibe)
        cbMaterial.Enabled = True
       


    End Sub
    Private Sub CBmATERIAL_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMaterial.SelectedIndexChanged
        If cbMaterial.ValueMember <> Nothing And cbMaterial.Text <> Nothing Then
            _recibe = tbRecibe.Text
            _MATERIAL = cbMaterial.SelectedValue
            tbCantidad.Enabled = True
            lbUnidad.Text = Metodos.Unidad(_MATERIAL)
            txtDisponible.Text = Metodos.Saldo(_MATERIAL, _DEPOSITO, 1)
            tbCantidad.Focus()

        End If
    End Sub
    Private Sub tbCantidad_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbCantidad.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btAgregar.Focus()
        End If
    End Sub

    '################################# BOTONES ###########################################################

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
                                    'sumo 1 a la cantidad de item
                                    _CantItem += 1
                                    'agrego al data griview
                                    cbDeposito.Enabled = False
                                    tbRecibe.Enabled = False
                                    DataGridView1.Rows.Add(_MATERIAL, cbMaterial.Text, lbUnidad.Text, CDec(var))
                                    DS_material.Tables("T_ALMA_103").Rows.RemoveAt(CInt(cbMaterial.SelectedIndex()))
                                    cbMaterial.DataSource = Nothing
                                    LLENAR_CB_MATERIAL()
                                    cbMaterial.Text = Nothing
                                    lbUnidad.Text = Nothing
                                    tbCantidad.Text = Nothing
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
                _nremito = Metodos.Obtener_Numero_Remito
                _fecha = Date.Now
                Metodos.Sumar_Num_Remito()
                _recibe = tbRecibe.Text
                For i = 0 To Me.DataGridView1.Rows.Count - 1
                    _MATERIAL = Me.DataGridView1.Item(0, i).Value
                    _CANT = Me.DataGridView1.Item(3, i).Value
                    Metodos.Descontar_Stock_Material(_MATERIAL, _DEPOSITO, _CANT, 1)
                    Metodos.Grabar_Trans(_nremito, Date.Now, _MATERIAL, _DEPOSITO, "0", 9, Date.Now, 0, _recibe, 1, _CANT, 0, _usr.Obt_Usr, "", "0")
                    'si es serializado se agraga un material serializado sin asignar numero
                Next
                '  equipo()
                PrintDocument1.Print()
                PrintDocument1.Print()
                MENSAJE.MADVE004(_nremito) ''mensaje de confirmacion
                borrar()
            Else
                MENSAJE.MERRO011()
            End If

        Catch ex As Exception
            MENSAJE.MERRO001()
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

        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + _nremito.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + _nremito.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "TIPO DE MOVIMIENTO: ENTREGA DE MATERIALES"
        R1_T2 = "RECIBE: GAS NATURAL FENOSA"
        R2_T1 = "DEPOSITO: " + cbDeposito.Text
        R2_T2 = "PROYECTO: " + tbRecibe.Text
        R3_T1 = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "CANTIDAD DE ITEM: " + DataGridView1.RowCount.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 800, 1140)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(n_REMITO.ToString, New Font("ARIAL", 16, FontStyle.Bold), Brushes.Black, 435, 73)
        e.Graphics.DrawString(_fecha.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 215)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 247)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 280)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 215)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 247)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 280)

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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        borrar()
    End Sub
End Class