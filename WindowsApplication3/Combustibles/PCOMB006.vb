Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class PCOMB006

    Dim DT_CARGA As New DataTable()
    Private Sub PCOMB006_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btncargar_Click(sender As Object, e As EventArgs) Handles btncargar.Click
        If pantalla.FileNames.Count > 0 Then
            If IO.File.Exists(txtrutaarchivo.Text) Then

                For i = 0 To pantalla.FileNames.Count - 1
                    Cursor.Current = Cursors.WaitCursor
                    DT_CARGA.Clear()
                    DT_CARGA = CargarExcel(pantalla.FileNames.ElementAt(i))
                    CargarEnDB(DT_CARGA)
                Next
                Cursor.Current = Cursors.Arrow
                MessageBox.Show("DATOS ACTUALIZADOS CORRECTAMENTE", "CARGADO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                'MessageBox.Show("REVISAR EL FORMATO DEL ARCHIVO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End If

    End Sub

    Private Sub btnbuscar_Click(sender As Object, e As EventArgs) Handles btnbuscar.Click
        Me.Refresh()
        pantalla.Multiselect = True
        pantalla.DereferenceLinks = True
        pantalla.Filter = "xlsx files (*.xlsx)|*.xlsx"
        pantalla.FilterIndex = 1
        pantalla.Title = "Seleccione el archivo"
        pantalla.RestoreDirectory = False
        pantalla.ShowDialog()
        txtrutaarchivo.Text = pantalla.FileName
        If txtrutaarchivo.Text <> Nothing Then
            btnbuscar.Enabled = True
        End If
    End Sub

    Private Function CargarExcel(ByVal vLibro As String) As DataTable
        Dim cs As String = "Provider= Microsoft.ACE.OLEDB.12.0;" & "Data Source=" & vLibro & ";" & "Extended Properties=""Excel 12.0;xml;HDR=YES;IMEX=2"""
        Dim ds As New DataTable() 'Para recuperar el nombre de la Sheet
        Dim dt As New DataTable()
        Dim cn As New OleDb.OleDbConnection(cs) 'cadena de coneccion 
        Try
            cn.Open()
            If Not System.IO.File.Exists(vLibro) Then
                MsgBox("No se encontro un libro válido en la ubicación especificada.", MsgBoxStyle.Exclamation)
            End If
            ds = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim nomhoja As String = ds.Rows(0).Field(Of String)("TABLE_NAME")
            Dim da As New OleDbDataAdapter("select * from [" & nomhoja & "]", cs)
            da.Fill(dt)
        Catch ex As Exception
            MsgBox("No se encontro un libro válido en la ubicación especificada. " & ex.Message.ToString, MsgBoxStyle.Exclamation)
        Finally
            cn.Close()
            cn.Dispose()
        End Try
        Return dt
    End Function

    Private Function CargarEnDB(ByVal dt As DataTable)
        Dim cont As Integer = 0
        Dim año As String()
        Dim separacion As String
        Dim ndevale As String = ""
        Dim p5 As Integer = 0
        'Dim uno As Integer = 1
        'Dim dos As Integer = 2
        'Dim tres As Integer = 3
        'Dim cuatro As Integer = 4
        Dim p3ticket As String = ""
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            For i = 0 To dt.Rows.Count - 1
                ndevale = dt.Rows(i)(0).ToString()
                If ndevale <> "SIN VALE" And ndevale <> "" And ndevale <> Nothing Then
                    separacion = dt.Rows(i)(1).ToString()
                    separacion = Convert.ToDateTime(dt.Rows(i)(1)).ToString("dd/MM/yyyy")
                    'separacion.Remove(10, 8)
                    año = separacion.Split("/")
                    ndevale = año(2) + dt.Rows(i)(0).ToString.PadLeft(8, "0")
                    'VERIFICO SI ES QUE AGREGARON LA COLUMNA DE Nº de TICKET
                    If dt.Columns.Count - 1 = 9 Then
                        Dim numero As Double = Convert.ToDouble(dt.Rows(i)(9))
                        p3ticket = numero.ToString()
                    Else
                        p3ticket = "0"
                    End If

                    Dim adt As New SqlCommand("Update T_VALE_122 set FTICKET122 = @D2, NTICKET122 = @D3, LITROS122 = @D4, TIPOTICKET122 = @D5  where NVALE122 = @D1", cnn)
                    adt.Parameters.Add(New SqlParameter("D1", ndevale))
                    adt.Parameters.Add(New SqlParameter("D2", Convert.ToDateTime(dt.Rows(i)(1))))
                    adt.Parameters.Add(New SqlParameter("D3", p3ticket))
                    adt.Parameters.Add(New SqlParameter("D4", Convert.ToDecimal(dt.Rows(i)(4))))

                    '5 a 8 - donde 5 es premium 6 gasoil 7 bio 8 super
                    If dt.Rows(i)(5).ToString() <> 0 Then
                        p5 = 1
                    End If
                    If dt.Rows(i)(6).ToString() <> 0 Then
                        p5 = 2
                    End If
                    If dt.Rows(i)(7).ToString() <> 0 Then
                        p5 = 3
                    End If
                    If dt.Rows(i)(8).ToString() <> 0 Then
                        p5 = 4
                    End If
                    adt.Parameters.AddWithValue("D5", p5)
                    cont = adt.ExecuteNonQuery()
                    End If
            Next
        Catch ex As Exception
            'MessageBox.Show(ndevale.ToString())
            Throw New Exception(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return cont
    End Function
End Class