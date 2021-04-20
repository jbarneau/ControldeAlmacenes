Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Data.OleDb

Public Class PDEPO004

    Private Clas_Metodos As New Clas_Almacen
    Private DT_stock As New DataTable
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Refresh()
        Dim pantalla As New OpenFileDialog
        pantalla.DereferenceLinks = True
        pantalla.Filter = "Excel files (*.xlsx)|*.xlsx"
        pantalla.FilterIndex = 1
        pantalla.RestoreDirectory = False
        pantalla.ShowDialog()
        If Windows.Forms.DialogResult.OK Then
            TextBox2.Text = pantalla.FileName
        End If
        If TextBox2.Text <> Nothing Then
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DT_stock.Clear()
        Dim deposito As String
        Dim cmate As String
        Dim stockmax As Double
        Dim total As Integer
        DT_stock = CargarExcel(TextBox2.Text, "Hoja1")
        total = DT_stock.Rows.Count
        ProgressBar1.Visible = True
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = total
        ProgressBar1.Value = 0
        Try
            If DT_stock.Rows.Count <> 0 Then
                For I = 0 To DT_stock.Rows.Count - 1
                    deposito = DT_stock.Rows(I).Item(0).ToString
                    cmate = DT_stock.Rows(I).Item(1).ToString()
                    If cmate = "" Then
                        cmate = "EXGA21"
                    End If
                    BORRAR_TABLA(deposito, cmate)
                    stockmax = DT_stock.Rows(I).Item(2)
                    GrabarRegistros(deposito, cmate, stockmax)
                    ProgressBar1.Value = ProgressBar1.Value + 1
                Next
                MessageBox.Show("CARGADO CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox2.Text = Nothing
            End If
            ProgressBar1.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Function CargarExcel(ByVal vLibro As String, ByVal vHoja As String) As DataTable
        Dim cs As String = "Provider= Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + vLibro + ";" + "Extended Properties=" + "Excel 12.0;"
        '"Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & vLibro & ";" & "Extended Properties=""Excel 4.0;xml;HDR=YES;IMEX=2"""
        Dim ds As New DataTable
        Try
            Dim cn As New OleDb.OleDbConnection(cs) 'cadena de coneccion 

            If Not System.IO.File.Exists(vLibro) Then
                MsgBox("No se encontro un libro válido en la ubicación especificada.", MsgBoxStyle.Exclamation)
            End If
            Dim da As New OleDbDataAdapter("select * from [" & vHoja & "$]", cs)
            da.Fill(ds)
        Catch ex As Exception
            MsgBox("No se encontro un libro válido en la ubicación especificada. " & ex.Message.ToString, MsgBoxStyle.Exclamation)
        End Try
        Return ds
    End Function

    Private Sub GrabarRegistros(ByVal deposito As String, ByVal cmate As String, ByVal stockmax As Double)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            'abro la cadena
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("INSERT INTO T_SCRI_108 (C_DEPO_108, C_MATE_108, CANT_108, USER_108, MAX_108) VALUES (@D1,@D2,@D3,@D4,@D5)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", deposito))
            comando1.Parameters.Add(New SqlParameter("D2", cmate))
            comando1.Parameters.Add(New SqlParameter("D3", 0))
            comando1.Parameters.Add(New SqlParameter("D4", _usr.Obt_Usr))
            comando1.Parameters.Add(New SqlParameter("D5", stockmax))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub

    Private Sub BORRAR_TABLA(ByVal dniope As String, ByVal codmate As String)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("DELETE FROM T_SCRI_108 WHERE (C_DEPO_108 = @D1) AND (C_MATE_108 = @D2)", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", dniope))
            ADT.Parameters.Add(New SqlParameter("D2", codmate))
            ADT.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub

    Private Sub PDEPO003_BIS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class