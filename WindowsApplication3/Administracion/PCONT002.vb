Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Module materiales_para_agregar
    Public materiales_agregar As New ArrayList
    Public materiales_borrar As New ArrayList
End Module

Public Class PCONT002
    Dim check As Integer
    Private Sub PCONT002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        materiales_agregar.Clear()
        'TRAIGO MATERIALES CON CODIGO
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim traer_material As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where (TIPO_002= 1) and (F_BAJA_002 IS NULL) and (SERI_002 = 0) ", cnn1)
        traer_material.ExecuteNonQuery()
        Dim descripcion_material As SqlDataReader = traer_material.ExecuteReader()
        While descripcion_material.Read()

            'TENGO QUE TILDAR LOS ELEMENTOS QUE TIENEN STOCK EN T_SCONT_107
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()
            Dim tildar_material As New SqlClient.SqlCommand("select C_MATE_107 from T_SCONT_107 where CONT_107=@C1 and C_MATE_107= @C2 ", cnn2)
            tildar_material.Parameters.Add(New SqlParameter("C1", numero_contrato))
            tildar_material.Parameters.Add(New SqlParameter("C2", descripcion_material.GetString(0)))
            tildar_material.ExecuteNonQuery()
            Dim tildar As SqlDataReader = tildar_material.ExecuteReader()
            If tildar.Read() Then
                check = 1
            Else
                check = 0
            End If
            cnn2.Close()

            DataGridView1.Rows.Add(descripcion_material.GetString(0), descripcion_material.GetString(1), check)
        End While
        cnn1.Close()


    End Sub

   
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            For i = 0 To Me.DataGridView1.RowCount - 1
                If Me.DataGridView1.Item(2, i).Value <> 1 Then
                    Me.DataGridView1.Item(2, i).Value = 1
                End If
            Next
        Else

            For j = 0 To Me.DataGridView1.RowCount - 1
                If Me.DataGridView1.Item(2, j).Value <> 0 Then
                    Me.DataGridView1.Item(2, j).Value = 0
                End If


            Next
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        materiales_agregar.Clear()
        materiales_borrar.Clear()
        'CARGO EL ARRAYLIST LOS TILDADOS
        For i = 0 To Me.DataGridView1.RowCount - 1
            If DataGridView1.Item(2, i).Value = 1 Then
                materiales_agregar.Add(DataGridView1.Item(0, i).Value)
            Else
                materiales_borrar.Add(DataGridView1.Item(0, i).Value)
            End If
        Next
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'ME PARO SOBRE LA CELDA Y HABILITO O DESHABILITO
        If (DataGridView1.Rows.Count > 0) Then
            'OBTENGO EL NUMERO DE FILA
            Dim fila As Integer = DataGridView1.CurrentCell.RowIndex()
            'CON EL NUMERO PASO LA DESCRIPCION
            If DataGridView1.Item(2, fila).Value = 1 Then
                check = 0
            Else
                check = 1
            End If
            'AGREGO O BORRO CHECK
            DataGridView1.Rows(fila).Cells(2).Value = check
        End If
    End Sub

  
End Class