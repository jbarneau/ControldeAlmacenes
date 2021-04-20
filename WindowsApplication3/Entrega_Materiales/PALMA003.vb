Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA003
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private DS_almacen As New DataSet
    Private DS_deposito As New DataSet
    Private DS_MED As New DataSet
    Private _DEPOSITO As String
    Private _MATERIAL As String
    Private _ALMACEN As String
    Private _CANT As Decimal
    Private Sub PALMA003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'escribo el usuario y la fecha
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        'PREGUNTO AL LA CLASE USUARIO SI TIENE ASIGNADO UN ALMACEN
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            ComboBox3.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO = _usr.Obt_Almacen
            ComboBox3.Text = NOMBRE_DEPOSITO(_DEPOSITO)
            CB_Equipo.Focus()
            CB_Equipo.Enabled = True
        Else
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            llenar_DS_DEPOSITO()
            LLENAR_CB_DEPOSITO()
            ComboBox3.Focus()
            CB_Equipo.Enabled = False
        End If
        'LLENO EL DATA COMBO BOX DE ALMACENES / OPERARIOS
        llenar_DS_ALMACEN()
        LLENAR_CB_ALMACEN()
    End Sub
    'CAMBIO LOS DATOS DEL COMBO BOX DEL ALAMCEN / OPERARIO
    Private Sub CB_Equipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Equipo.SelectedIndexChanged
        If CB_Equipo.ValueMember <> Nothing Then
            If CB_Equipo.Text <> Nothing Then
                _ALMACEN = CB_Equipo.SelectedValue
                'LLAMO A LA FUNCION PARA LLENAR EL PRIMER DATA SET
                llenar_DW1(_ALMACEN)
                If DataGridView2.RowCount <> 0 Then
                    TextBox1.Enabled = True
                    NumericUpDown1.Enabled = True
                    Button1.Enabled = True
                    Button2.Enabled = True
                Else
                    TextBox1.Enabled = False
                    NumericUpDown1.Enabled = False
                    Button1.Enabled = False
                    Button2.Enabled = False
                    DataGridView1.Enabled = False
                    Button3.Enabled = False
                End If

            End If
        End If
    End Sub
    'SELECCIONO DEL COMBO BOX DEL DEPOSITO
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.TextChanged
        If ComboBox3.ValueMember <> Nothing Then
            If ComboBox3.Text <> Nothing Then
                _DEPOSITO = ComboBox3.SelectedValue
                CB_Equipo.Enabled = True
            End If
        End If

    End Sub
    '##########BOTONES############################BOTONES#############BOTONES########################
    'BOTON DE SALIR
  
    'BOTON PARA AGREGAR MEDIDORES
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim _dispo As Integer = Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value
        Dim _material As String = Me.DataGridView2.Item(0, DataGridView2.CurrentRow.Index).Value
        Dim _descMate As String = Me.DataGridView2.Item(1, DataGridView2.CurrentRow.Index).Value
        Dim nmed As Decimal
        Try

        
        'PREGUNTO SI EL RECUADRO DE CANTIDAD ES 0 O ES MAYOR A LA CANTIDAD SIN ASIGNAR
        If NumericUpDown1.Value <= _dispo And NumericUpDown1.Value > 0 Then
            'VERIFICO QUE SE INGRESO UN NUMERO DE MEDIDOR
            If IsNumeric(TextBox1.Text) = True Then
                'VOY AGREGANDO DE A UN MEDIDOR
                For i = 0 To NumericUpDown1.Value - 1
                    'AL PRIMER MEDIDOR LE SUMO UNO HASTA LLEGAR A LA CANTIDAD
                    nmed = CDec(TextBox1.Text) + i
                    'ME FIJO SI EL DATA DE MEDIDORES ESTA LLENO O ES EL PRIMERO
                        If Medidor.Exite_Medi(nmed, _DEPOSITO, Me.DataGridView2.Item(0, Me.DataGridView2.CurrentRow.Index).Value) = True Then
                            If DataGridView1.Rows.Count = 0 Then
                                'agrego el item a la datview
                                DataGridView1.Rows.Add(_material, _descMate, nmed, Me.DataGridView2.Item(3, DataGridView2.CurrentRow.Index).Value, Me.DataGridView2.Item(4, DataGridView2.CurrentRow.Index).Value)
                                'elimino uno del dataview
                                Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value = Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value - 1

                            Else
                                If val_medi_data(nmed) = False Then
                                    DataGridView1.Rows.Add(_material, _descMate, nmed, Me.DataGridView2.Item(3, DataGridView2.CurrentRow.Index).Value, Me.DataGridView2.Item(4, DataGridView2.CurrentRow.Index).Value)
                                    'elimino uno del dataview
                                    Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value = Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value - 1

                                End If
                            End If
                        End If
                    Next
                    TextBox1.Text = Nothing
                    NumericUpDown1.Value = 0
                    If DataGridView2.RowCount <> 0 Then
                        Button3.Enabled = True
                    End If


                Else
                    MENSAJE.MERRO006()
                    TextBox1.SelectAll()
                    TextBox1.Focus()
            End If
        Else
            MENSAJE.MERRO006()
            NumericUpDown1.Focus()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    'BOTON PARA ELIMINAR ITEM DEL DATA     
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                MENSAJE.MERRO011()
            Else
                DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
                Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value = Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value + 1
                If DataGridView2.RowCount = 0 Then
                    Button3.Enabled = False
                End If
                MENSAJE.MADVE001()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    'BOTON DE CONFIRMAR
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If DataGridView1.Rows.Count <> 0 Then
                If VER_DW1() = False Then
                    Dim res As DialogResult
                    res = MessageBox.Show("Quedaron medidores sin asignar" + vbCrLf + "¿desea continuar?", "MADVE005", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If res = System.Windows.Forms.DialogResult.Yes Then
                        medidores()
                        MENSAJE.MADVE001()
                    End If
                Else
                    medidores()
                    MENSAJE.MADVE001()
                End If
            Else
                MENSAJE.MERRO011()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
    '################################FUNCIONES############FUNCIONES#########################3333
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
    Public Function NOMBRE_DEPOSITO(ByVal str As String) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select NOMB_003 FROM M_PERS_003 WHERE NDOC_003 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Private Sub LLENAR_CB_DEPOSITO()
        ComboBox3.DataSource = DS_deposito.Tables("M_PERS_003")
        ComboBox3.DisplayMember = "NOMB_003"
        ComboBox3.ValueMember = "NDOC_003"
        ComboBox3.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_ALMACEN()
        CB_Equipo.DataSource = DS_almacen.Tables("M_PERS_003")
        CB_Equipo.DisplayMember = "NOMBRE"
        CB_Equipo.ValueMember = "NDOC_003"
        CB_Equipo.Text = Nothing
    End Sub
    Private Sub llenar_DW1(ByVal a As String)
        DataGridView2.Rows.Clear()
        Dim dataset1 As New DataSet
        Dim mat As Decimal
        Dim des As String
        Dim CANT As Decimal
        Dim remi As Decimal
        Dim fec As Date
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_109,CANT_109, NREMI_109,F_REMI_109 FROM T_MED_SA_109 WHERE CDEPO_109 =@D1", cnn2)
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", a))
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(dataset1, "T_MED_SA_109")
        For i = 0 To dataset1.Tables("T_MED_SA_109").Rows.Count - 1
            mat = dataset1.Tables("T_MED_SA_109").Rows(i).Item(0)
            des = Metodos.detalle_material(mat)
            CANT = dataset1.Tables("T_MED_SA_109").Rows(i).Item(1)
            remi = dataset1.Tables("T_MED_SA_109").Rows(i).Item(2)
            fec = dataset1.Tables("T_MED_SA_109").Rows(i).Item(3)
            If CANT > 0 Then
                DataGridView2.Rows.Add(mat, des, CANT, remi, fec)
            End If
        Next
        cnn2.Close()
    End Sub
    
    Private Function val_medi_data(ByVal med As Decimal) As Boolean
        Dim resp As Boolean = False
        For i = 0 To DataGridView1.Rows.Count - 1
            If med = CDec(Me.DataGridView1.Item(2, i).Value) Then
                resp = True
            End If
        Next
        Return resp
    End Function
    Private Function VER_DW1() As Boolean
        Dim RESP As Boolean = True
        For I = 0 To DataGridView2.Rows.Count - 1
            If Me.DataGridView2.Item(2, I).Value <> 0 Then
                RESP = False
            End If
        Next
        Return RESP
    End Function
    Private Sub medidores()
        For I = 0 To DataGridView2.Rows.Count - 1
            Medidor.MODIFICAR_MED_SA(Me.DataGridView2.Item(0, I).Value, _ALMACEN, Me.DataGridView2.Item(2, I).Value)
        Next
        For I = 0 To DataGridView1.Rows.Count - 1
            Medidor.MODIFICAR_MEDIDOR(Me.DataGridView1.Item(2, I).Value, Me.DataGridView1.Item(0, I).Value, _ALMACEN, Date.Now, _usr.Obt_Usr)
            Medidor.Grabar_Mov_Medi(Me.DataGridView1.Item(2, I).Value, Me.DataGridView1.Item(0, I).Value, Me.DataGridView1.Item(3, I).Value, Me.DataGridView1.Item(4, I).Value, _DEPOSITO, _ALMACEN)
        Next
        Medidor.ELIMINAR_MED_SA(_ALMACEN, 0)
        borrar()
    End Sub
    Public Sub borrar()
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
        CB_Equipo.Text = Nothing
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            CB_Equipo.Focus()
            CB_Equipo.Enabled = True
        Else
            ComboBox3.Text = Nothing
            ComboBox3.Enabled = True
            ComboBox3.Focus()
            CB_Equipo.Enabled = False
        End If
        TextBox1.Enabled = False
        NumericUpDown1.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        borrar()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.ValueMember <> Nothing And ComboBox3.Text <> Nothing Then
            CB_Equipo.Enabled = True
        End If
    End Sub
End Class