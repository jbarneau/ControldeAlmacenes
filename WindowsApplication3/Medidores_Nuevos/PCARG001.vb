Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class PCARG001
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor
    Private _MATERIAL As String
    Private _CANT As Decimal
    

    Private Sub PCARG001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DW1(11)
        If DataGridView2.RowCount = 0 Then
            TextBox1.Enabled = False
            NumericUpDown1.Enabled = False
            Button1.Enabled = False
            Button2.Enabled = False
            DataGridView1.Enabled = False
            Button3.Enabled = False

        End If
    End Sub



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
                        If Medidor.Exite_Medi(nmed) = False Then
                            If DataGridView1.Rows.Count = 0 Then
                                'agrego el item a la datview
                                DataGridView1.Rows.Add(_material, _descMate, nmed, Me.DataGridView2.Item(3, DataGridView2.CurrentRow.Index).Value, Me.DataGridView2.Item(4, DataGridView2.CurrentRow.Index).Value)
                                'elimino uno del dataview
                                Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value = Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value - 1

                            Else
                                If val_medi_data(nmed) = False Then
                                    If Medidor.Exite_Medi(nmed) = False Then
                                        DataGridView1.Rows.Add(_material, _descMate, nmed, Me.DataGridView2.Item(3, DataGridView2.CurrentRow.Index).Value, Me.DataGridView2.Item(4, DataGridView2.CurrentRow.Index).Value)
                                        'elimino uno del dataview
                                        Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value = Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value - 1
                                    End If
                                End If

                            End If
                        End If
                    Next
                    TextBox1.Text = Nothing
                    NumericUpDown1.Value = 1
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If DataGridView1.Rows.Count = 0 Then
                MENSAJE.MERRO011()
            Else
                DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
                Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value = Me.DataGridView2.Item(2, DataGridView2.CurrentRow.Index).Value + 1
                MENSAJE.MADVE001()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
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
            Medidor.MODIFICAR_MED_SA(Me.DataGridView2.Item(0, I).Value, 11, Me.DataGridView2.Item(2, I).Value)
        Next
        For I = 0 To DataGridView1.Rows.Count - 1
            Medidor.Grabar_medidor(Me.DataGridView1.Item(2, I).Value, Me.DataGridView1.Item(0, I).Value, 11, Date.Now, 1, _usr.Obt_Usr)
        Next
        Medidor.ELIMINAR_MED_SA(11, 0)
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            If DataGridView1.Rows.Count <> 0 Then
                If VER_DW1() = False Then
                    Dim res As DialogResult
                    res = MessageBox.Show("Quedaron medidores sin asignar desea continuar", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If res = System.Windows.Forms.DialogResult.Yes Then
                        medidores()
                        llenar_DW1(1)
                        MENSAJE.MADVE001()
                    End If
                Else
                    medidores()
                    llenar_DW1(1)
                    MENSAJE.MADVE001()
                End If
            Else
                MENSAJE.MERRO011()
            End If
        Catch ex As Exception
            MENSAJE.MERRO001()
        End Try
    End Sub
End Class