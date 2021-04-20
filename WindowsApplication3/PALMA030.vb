Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO


Public Class PALMA030
    Private DS_material As New DataSet
    'Private DS_deposito As New DataSet
    Private _CONTRATO As String
    Private _DEPOSITO As String
    Private _MATERIAL As String
    Private _PROVEEDOR As String
    Private _CANT As Decimal
    Private _ESTADO As Integer
    Private _nremito As Decimal
    Private _fecha As Date
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private _CantItem As Integer = 0
    Private med_rettirar As New Clase_med_retirar

    Private Sub PALMA030_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            MAIN.serie.Clear()
            MAIN.material.Clear()
            Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
            Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
            _CantItem = 0
            llenar_CB_proveedor()
            If _usr.Obt_Almacen <> "0" Then
                _DEPOSITO = _usr.Obt_Almacen
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDown
                ComboBox1.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO)
                ComboBox1.Enabled = False
                CB_Proveedor.Enabled = True
                CB_Proveedor.Focus()

            Else
                ComboBox1.Enabled = True
                llenar_CB_DEPOSITO()
                CB_Proveedor.Enabled = False
                ComboBox1.Focus()
            End If
            llenar_CB_Contrato()
            CB_contrato.Enabled = False
            CB_estado.Enabled = False
            llenar_DS_MATERIAL()
            LLENAR_CB_MATERIAL()
            CB_Material.Enabled = False
            'llenar_CB_proveedor()
            TextBox1.Enabled = False

        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub


    Private Sub llenar_CB_DEPOSITO()
        Dim DS_deposito As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 and F_BAJA_003 IS NULL ORDER BY NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito, "M_PERS_003")
        cnn2.Close()
        ComboBox1.DataSource = DS_deposito.Tables("M_PERS_003")
        ComboBox1.DisplayMember = "NOMB_003"
        ComboBox1.ValueMember = "NDOC_003"
        ComboBox1.Text = Nothing

    End Sub
    Private Sub llenar_DS_MATERIAL()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 where F_BAJA_002 IS NULL order by DESC_002", cnn2)
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

    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Dim var As String
        Try
            If Metodos.validarMaterial(_MATERIAL) = True Then
                If IsNothing(TextBox1.Text) = False Then
                    If IsNumeric(TextBox1.Text) = True Then
                        'remplazo los puntos por coma
                        var = Metodos.Rempl_Punto_Coma(TextBox1.Text)
                        'pregunto cuantas coma tiene la variable
                        If Metodos.Contar_Coma_Punto(var) <= 1 Then
                            'verifico que el material permita decimal 
                            If Metodos.Tiene_Decimal(_MATERIAL, var) = True Then
                                'verifico que la cantidad se mayor a 0
                                If CDec(var) > 0 Then
                                    'verifico que la cantidad sea menor a la disponible
                                    If CDec(var) <= CDec(TextBox2.Text) Then
                                        If Medidor.Es_Serializado(_MATERIAL) = True Then
                                            Dim pantalla As New PALMA004BIS
                                            pantalla.grabardatos(_MATERIAL, CDec(var), _DEPOSITO, _ESTADO)
                                            pantalla.ShowDialog()
                                            If pantalla.validar = True Then
                                                'sumo 1 a la cantidad de item
                                                _CantItem += 1
                                                'agrego al data griview
                                                DataGridView1.Rows.Add(_MATERIAL, CB_Material.Text, CDec(var))
                                                TextBox1.Text = Nothing
                                                TextBox1.Enabled = False
                                                'elimino del data set el material
                                                DS_material.Tables("M_MATE_002").Rows.RemoveAt(CInt(CB_Material.SelectedIndex()))
                                                'vacio el combo box
                                                CB_Material.DataSource = Nothing
                                                'vuelvo a llenar el combo box
                                                LLENAR_CB_MATERIAL()
                                                TextBox1.Enabled = False
                                                CB_Proveedor.Enabled = False
                                                ComboBox1.Enabled = False
                                                CB_estado.Enabled = False
                                                CB_contrato.Enabled = False
                                                CB_Material.Focus()
                                                'verifico que sean menos de 20  items
                                                If _CantItem = 20 Then
                                                    B_Agregar_Item.Enabled = False
                                                End If
                                                ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                                If DataGridView1.Rows.Count <> 0 Then
                                                    B_Entregar.Enabled = True
                                                End If
                                                TextBox2.Text = Nothing
                                            Else
                                                CB_Material.Text = Nothing
                                                TextBox1.Text = Nothing
                                                TextBox1.Enabled = False
                                                TextBox2.Text = 0
                                                CB_Material.Focus()
                                            End If
                                        Else
                                            'sumo 1 a la cantidad de item
                                            _CantItem += 1
                                            'agrego al data griview
                                            DataGridView1.Rows.Add(_MATERIAL, CB_Material.Text, CDec(var))
                                            TextBox1.Text = Nothing
                                            TextBox1.Enabled = False
                                            'elimino del data set el material
                                            DS_material.Tables("M_MATE_002").Rows.RemoveAt(CInt(CB_Material.SelectedIndex()))
                                            'vacio el combo box
                                            CB_Material.DataSource = Nothing
                                            'vuelvo a llenar el combo box
                                            LLENAR_CB_MATERIAL()
                                            TextBox1.Enabled = False
                                            CB_Proveedor.Enabled = False
                                            ComboBox1.Enabled = False
                                            CB_Material.Focus()
                                            'verifico que sean menos de 20  items
                                            If _CantItem = 20 Then
                                                B_Agregar_Item.Enabled = False
                                            End If
                                            ' verifico que el data gridview no este vacio, y actibo el boton de entregar
                                            If DataGridView1.Rows.Count <> 0 Then
                                                B_Entregar.Enabled = True
                                            End If
                                            TextBox2.Text = Nothing
                                        End If
                                    Else
                                        MENSAJE.MERRO007()
                                        TextBox1.Focus()
                                        TextBox1.SelectAll()
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
                    B_Entregar.Enabled = False
                End If
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub

    Private Sub llenar_CB_Contrato()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NCONT_004, DESC_004 FROM M_CONT_004 where F_BAJA_004 is NULL order by DESC_004", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_CONT_004")
        cnn2.Close()
        CB_contrato.DataSource = DS_contrato.Tables("M_CONT_004")
        CB_contrato.DisplayMember = "DESC_004"
        CB_contrato.ValueMember = "NCONT_004"
        CB_contrato.Text = Nothing
    End Sub
    Private Sub llenar_CB_proveedor()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CUIT_005,RAZO_005 FROM M_PROV_005 where F_BAJA_005 is NULL order by RAZO_005", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_PROV_005")
        cnn2.Close()
        CB_Proveedor.DataSource = DS_contrato.Tables("M_PROV_005")
        CB_Proveedor.DisplayMember = "RAZO_005"
        CB_Proveedor.ValueMember = "CUIT_005"
        CB_Proveedor.Text = Nothing
    End Sub
    

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.ValueMember <> Nothing Then
            CB_Proveedor.Enabled = True
            If ComboBox1.Text <> Nothing Then
                _DEPOSITO = ComboBox1.SelectedValue
                llenar_disponible()
            End If
        End If
    End Sub

    Private Sub llenar_disponible()
        If ComboBox1.Text <> Nothing And CB_estado.Text <> Nothing And CB_Material.Text <> Nothing Then
            TextBox2.Text = Metodos.Saldo(_MATERIAL, _DEPOSITO, _ESTADO)
                End If
    End Sub

    Private Sub CB_Proveedor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Proveedor.SelectedIndexChanged
        If CB_Proveedor.ValueMember <> Nothing Then
            If CB_Proveedor.Text <> Nothing Then
                CB_contrato.Enabled = True
                _PROVEEDOR = CB_Proveedor.SelectedValue
            End If
        End If
    End Sub

    Private Sub CB_contrato_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_contrato.SelectedIndexChanged
        If CB_contrato.ValueMember <> Nothing Then
            If CB_contrato.Text <> Nothing Then
                CB_estado.Enabled = True
                _CONTRATO = CB_contrato.SelectedValue
            End If
        End If
    End Sub
    
    Private Sub CB_estado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_estado.SelectedIndexChanged
        If CB_estado.Text <> Nothing Then
            If CB_estado.Text = "DISPONIBLE" Then
                _ESTADO = 1
            Else
                _ESTADO = 9
            End If
            CB_Material.Enabled = True
            llenar_disponible()
        End If
       
    End Sub

    Private Sub CB_Material_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Material.SelectedIndexChanged
        If CB_Material.ValueMember <> Nothing Then
            If CB_Material.Text <> Nothing Then
                _MATERIAL = CB_Material.SelectedValue
                llenar_disponible()
                TextBox1.Enabled = True
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub B_Entregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Entregar.Click
        Try
            If DataGridView1.RowCount > 0 Then
                If TextBox3.Text <> Nothing Then
                    _nremito = Metodos.Obtener_Numero_Remito
                    _fecha = Date.Now
                    Try
                        'inicio todos los datos
                        _CantItem = 0
                        'INCREMENTO EL NUMERO DE REMITO
                        Metodos.Sumar_Num_Remito()
                        For i = 0 To Me.DataGridView1.Rows.Count - 1
                            _MATERIAL = Me.DataGridView1.Item(0, i).Value
                            _CANT = Me.DataGridView1.Item(2, i).Value
                            Metodos.Descontar_Stock_Material(_MATERIAL, _DEPOSITO, _CANT, _ESTADO)
                            Metodos.Grabar_Trans(_nremito, _fecha, _MATERIAL, _DEPOSITO, _DEPOSITO, 7, _fecha, 0, TextBox3.Text, 0, _CANT, 0, _usr.Obt_Usr, _CONTRATO, "")
                            Metodos.Descontar_Stock_Contrato(_MATERIAL, _CONTRATO, _CANT)
                            'si es serializado se agraga un material serializado sin asignar numero
                            If Medidor.Es_Serializado(_MATERIAL) = True Then
                                For G = 0 To MAIN.serie.Count - 1
                                    If MAIN.material.Item(G) = _MATERIAL Then
                                        Medidor.MODIFICAR_MEDIDOR_ESTADO_4(MAIN.serie.Item(G).ToString, _MATERIAL, _fecha, _usr.Obt_Usr, _nremito)
                                        med_rettirar.GRABAR_MEDIDOR2(MAIN.serie.Item(G).ToString, Date.Today, _usr.Obt_Usr, _DEPOSITO, _MATERIAL, "SP", "01", Date.Today, 0, 0, "SO", "NF")
                                    End If
                                Next
                            End If
                        Next
                        PrintDocument1.Print()
                        borrar()
                        MENSAJE.MADVE004(_nremito) ''mensaje de confirmacion
                    Catch ex As Exception
                        MENSAJE.MERRO001()
                    End Try
                Else
                    MENSAJE.MERRO006()
                    TextBox3.Focus()
                End If
            Else
                MENSAJE.MERRO011()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    Private Sub borrar()
        CB_Material.DataSource = Nothing
        CB_Material.Text = Nothing
        DS_material.Clear()
        llenar_DS_MATERIAL()
        LLENAR_CB_MATERIAL()
        TextBox2.Text = Nothing
        CB_estado.Text = Nothing
        CB_contrato.Text = Nothing
        CB_Proveedor.Text = Nothing
        TextBox2.Text = Nothing
        TextBox3.Text = Nothing
        B_Agregar_Item.Enabled = True
        B_Eliminar_Item.Enabled = True
        TextBox1.Text = Nothing
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            CB_Proveedor.Enabled = True
            CB_Proveedor.Focus()
        Else
            ComboBox1.Text = Nothing
            ComboBox1.Enabled = True
            CB_Proveedor.Enabled = False
            ComboBox1.Focus()
        End If
        CB_Material.Enabled = False
        CB_contrato.Enabled = False
        CB_estado.Enabled = False
        TextBox1.Enabled = False
        MAIN.serie.Clear()
        MAIN.material.Clear()
        Me.DataGridView1.Rows.Clear()
        ' DataGridView1.Rows.Clear()
    End Sub
    Private Function mayusculas(ByVal Texto As String, ByVal TXT As TextBox) As String
        mayusculas = UCase(Texto) ' LCase se encarga de transformar el texto en minuscula UCase a mayuscula
        TXT.SelectionStart = Len(Texto) ' Dejamos el cursor al final del texto 
    End Function

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        TextBox3.Text = mayusculas(TextBox3.Text, TextBox3)
        If TextBox3.TextLength > 30 Then
            TextBox3.Text = TextBox3.Text.ToString.Remove(30)
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
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + _nremito.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "TIPO DE MOVIMIENTO: DEVOLUCION DE MATERIALES"
        R1_T2 = "PROVEEDOR: " + CB_Proveedor.Text
        R2_T1 = "DEPOSITO: " + ComboBox1.Text
        R2_T2 = "CONTARTO: " + CB_contrato.Text
        R3_T1 = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "ESTADO MATERIAL: " + CB_estado.Text

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 810, 1150)
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
        e.Graphics.DrawString("CANTIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 690, 325)
        'RECORRO EL DATA
        For I = 0 To DataGridView1.RowCount - 1
            e.Graphics.DrawString(Me.DataGridView1.Item(0, I).Value.ToString + I.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 100, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 710, LINEA)
            LINEA += SALTO
        Next


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        borrar()
    End Sub
End Class