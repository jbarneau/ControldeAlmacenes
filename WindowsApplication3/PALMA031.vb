Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class PALMA031

    Private DS_material As New DataSet
    Private DS_almacen As New DataSet
    Private DS_contrato As New DataSet
    Private _TIPO As Integer
    Private _MATERIAL As String
    Private _ALMACEN As String
    Private _FECHA As Date
    Private _NREMITO As Decimal
    Private _CANT As Decimal
    Private _TEXTO As String
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private _CantItem As Integer = 0
    Private var As String
    Private Sub PALMA031_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MAIN.serie.Clear()
        MAIN.material.Clear()
        Try
            Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
            Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
            _CantItem = 0
            llenar_DS_MATERIAL()
            LLENAR_CB_MATERIAL()
            llenar_DS_ALMACEN()
            LLENAR_TIPO()
            CB_Material.Enabled = False
            CB_Material.Enabled = False
            txtcantidad.Enabled = False
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE ALMA_003=1 OR DEPO_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
        CB_Equipo.DataSource = DS_almacen.Tables("M_PERS_003")
        CB_Equipo.DisplayMember = "NOMBRE"
        CB_Equipo.ValueMember = "NDOC_003"
        CB_Equipo.Text = Nothing
    End Sub
    Private Sub llenar_DS_MATERIAL()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 where M_MATE_002.F_BAJA_002 IS NULL", cnn2)
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
    Private Sub LLENAR_TIPO()
        Dim DS As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where C_TABLA_802 = 1 AND (C_PARA_802 =4 OR C_PARA_802 = 5)", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS, "DET_PARAMETRO_802")
        cnn2.Close()
        CB_Tipo.DataSource = DS.Tables("DET_PARAMETRO_802")
        CB_Tipo.ValueMember = "C_PARA_802"
        CB_Tipo.DisplayMember = "DESC_802"
        CB_Tipo.Text = Nothing
    End Sub
    Private Sub CB_Tipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Tipo.SelectedIndexChanged
        If CB_Tipo.ValueMember <> Nothing And CB_Tipo.Text <> Nothing Then
            CB_Equipo.Enabled = True
            _TIPO = CB_Tipo.SelectedValue
        End If
    End Sub
    Private Sub CB_Equipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Equipo.SelectedIndexChanged
        If CB_Equipo.ValueMember <> Nothing And CB_Equipo.Text <> Nothing Then
            _ALMACEN = CB_Equipo.SelectedValue
            CB_Material.Enabled = True
            If CB_Material.Text <> Nothing Then
                TextBox2.Text = Metodos.Saldo(_MATERIAL, _ALMACEN, 1)
            End If
        End If
    End Sub
    Private Sub CB_Material_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Material.SelectedIndexChanged
        If CB_Material.ValueMember <> Nothing And CB_Material.Text <> Nothing Then
            txtcantidad.Enabled = True
            _MATERIAL = CB_Material.SelectedValue
            If CB_Equipo.Text <> Nothing Then
                TextBox2.Text = Metodos.Saldo(_MATERIAL, _ALMACEN, 1)
            End If
        End If
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
                DS_material.Tables("M_MATE_002").Rows.Add(NuevoRows)
                DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
                CB_Material.Text = Nothing
                MENSAJE.MADVE001()
                _CantItem -= 1
                If _CantItem <> 20 Then
                    B_Agregar_Item.Enabled = True
                End If
                If DataGridView1.Rows.Count = 0 Then
                    Button1.Enabled = False
                End If
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Try
            If Metodos.validarMaterial(_MATERIAL) = True Then
                If IsNothing(txtcantidad.Text) = False Then
                    If IsNumeric(txtcantidad.Text) = True Then
                        'remplazo los puntos por coma
                        var = Metodos.Rempl_Punto_Coma(txtcantidad.Text)
                        'pregunto cuantas coma tiene la variable
                        If Metodos.Contar_Coma_Punto(var) <= 1 Then
                            'verifico que el material permita decimal 
                            If Metodos.tiene_decimal(_MATERIAL, var) = True Then
                                'verifico que la cantidad se mayor a 0
                                If CDec(var) > 0 Then
                                    'verifico que la cantidad sea menor a la disponible
                                    If _TIPO = 5 Then
                                        TIPO_5()
                                    Else
                                        llenar_materiales_DW()
                                    End If
                                Else
                                    MENSAJE.MERRO010()
                                    txtcantidad.Focus()
                                    txtcantidad.SelectAll()
                                End If
                            Else
                                MENSAJE.MERRO009()
                                txtcantidad.Focus()
                                txtcantidad.SelectAll()
                            End If
                        Else
                            MENSAJE.MERRO006()
                            txtcantidad.Focus()
                            txtcantidad.SelectAll()
                        End If
                    Else
                        MENSAJE.MERRO006()
                        txtcantidad.Focus()
                        txtcantidad.SelectAll()
                    End If
                Else
                    MENSAJE.MERRO006()
                    txtcantidad.Focus()
                    txtcantidad.SelectAll()
                End If
            Else
                MENSAJE.MERRO006()
                CB_Material.SelectAll()
                CB_Material.Focus()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Public Sub TIPO_5()
        If CDec(var) <= CDec(TextBox2.Text) Then
            If Medidor.Es_Serializado(_MATERIAL) = True Then
                Dim pantalla As New PALMA004BIS
                pantalla.grabardatos(_MATERIAL, CDec(var), _ALMACEN, 1)
                pantalla.ShowDialog()
                If pantalla.validar = True Then
                    'sumo 1 a la cantidad de item
                    llenar_materiales_DW()
                Else
                    CB_Material.Text = Nothing
                    txtcantidad.Text = Nothing
                    txtcantidad.Enabled = False
                    TextBox2.Text = 0
                    CB_Material.Focus()
                End If
            Else
                llenar_materiales_DW()
            End If
        Else
            MENSAJE.MERRO007()
            txtcantidad.Focus()
            txtcantidad.SelectAll()
        End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text <> Nothing Then
                If DataGridView1.RowCount > 0 Then

                    Try
                        _NREMITO = Metodos.Obtener_Numero_Remito
                        _FECHA = Date.Now
                        Metodos.Sumar_Num_Remito()
                        'inicio todos los datos
                        B_Agregar_Item.Enabled = True
                        txtcantidad.Text = Nothing
                        _CantItem = 0
                        If _TIPO = 5 Then
                            For i = 0 To Me.DataGridView1.Rows.Count - 1
                                _MATERIAL = Me.DataGridView1.Item(0, i).Value
                                _CANT = Me.DataGridView1.Item(2, i).Value
                                Metodos.Descontar_Stock_Material(_MATERIAL, _ALMACEN, _CANT, 1)
                                Metodos.Grabar_Trans(_NREMITO, _FECHA, _MATERIAL, _ALMACEN, _ALMACEN, _TIPO, _FECHA, 0, TextBox1.Text, 0, _CANT, 0, _usr.Obt_Usr, "", "")
                                'si es serializado se agraga un material serializado sin asignar numero
                                If Medidor.Es_Serializado(_MATERIAL) = True Then
                                    For G = 0 To MAIN.serie.Count - 1
                                        If MAIN.material.Item(G) = _MATERIAL Then
                                            Medidor.MODIFICAR_MEDIDOR_ESTADO_3(MAIN.serie.Item(G), _MATERIAL, _FECHA, _usr.Obt_Usr)
                                        End If
                                    Next
                                End If

                            Next
                        Else
                            For i = 0 To Me.DataGridView1.Rows.Count - 1
                                _MATERIAL = Me.DataGridView1.Item(0, i).Value
                                _CANT = Me.DataGridView1.Item(2, i).Value
                                Metodos.Increpmentar_Stock_Material(_MATERIAL, _ALMACEN, _CANT, 1)
                                Metodos.Grabar_Trans(_NREMITO, _FECHA, _MATERIAL, _ALMACEN, _ALMACEN, _TIPO, _FECHA, 0, TextBox1.Text, 0, _CANT, 0, _usr.Obt_Usr, "", "")
                                If Medidor.Es_Serializado(_MATERIAL) = True Then
                                    Medidor.Grabar_Med_Sin_Asignar(_ALMACEN, _MATERIAL, _CANT, _NREMITO, _FECHA, 0)
                                End If
                            Next
                        End If
                        PrintDocument1.Print()
                        'equipo
                        borrar()
                        MENSAJE.MADVE004(_NREMITO) ''mensaje de confirmacion

                    Catch ex As Exception
                        MENSAJE.MERRO001()
                    End Try
                Else
                    MENSAJE.MERRO011()
                End If
            Else
                MENSAJE.MERRO006()
                TextBox1.Focus()
            End If


        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
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
        R1_T1 = "TIPO DE MOVIMIENTO: " + CB_Tipo.Text
        R1_T2 = "ALMACEN: " + CB_Equipo.Text
        R2_T1 = "OBSERVACIONES: " + TextBox1.Text
        R2_T2 = ""
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
        e.Graphics.DrawString("CANTIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 690, 326)
        'RECORRO EL DATA
        For I = 0 To DataGridView1.RowCount - 1
            e.Graphics.DrawString(Me.DataGridView1.Item(0, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 100, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 710, LINEA)
            LINEA += SALTO
        Next
    End Sub

    Public Sub llenar_materiales_DW()
        _CantItem += 1
        'agrego al data griview
        DataGridView1.Rows.Add(_MATERIAL, CB_Material.Text, CDec(var))
        txtcantidad.Text = Nothing
        txtcantidad.Enabled = False
        'elimino del data set el material
        DS_material.Tables("M_MATE_002").Rows.RemoveAt(CInt(CB_Material.SelectedIndex()))
        'vacio el combo box
        CB_Material.DataSource = Nothing
        'vuelvo a llenar el combo box
        LLENAR_CB_MATERIAL()
        txtcantidad.Enabled = False
        CB_Equipo.Enabled = False
        CB_Tipo.Enabled = False
        CB_Material.Focus()
        'verifico que sean menos de 20  items
        If _CantItem = 20 Then
            B_Agregar_Item.Enabled = False
        End If
        ' verifico que el data gridview no este vacio, y actibo el boton de entregar
        If DataGridView1.Rows.Count <> 0 Then
            Button1.Enabled = True
        End If
        TextBox2.Text = Nothing
    End Sub


    Public Sub borrar()
        DS_material.Clear()
        CB_Material.DataSource = Nothing
        llenar_DS_MATERIAL()
        LLENAR_CB_MATERIAL()
        CB_Tipo.Text = Nothing
        CB_Equipo.Text = Nothing
        CB_Material.Text = Nothing
        MAIN.serie.Clear()
        MAIN.material.Clear()
        TextBox2.Text = Nothing
        DataGridView1.Rows.Clear()
        TextBox1.Text = Nothing
        txtcantidad.Enabled = False
        CB_Equipo.Enabled = False
        CB_Material.Enabled = False
        CB_Tipo.Enabled = True
        Button1.Enabled = False
        CB_Equipo.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        borrar()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class