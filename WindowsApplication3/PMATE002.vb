Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PMATE002
    Private Confirmacion As Boolean = False
    Private CODIGO As String = ""
    Private Sub PMATE002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LLENAR()
    End Sub

#Region "FUNCIONES"
    Private Sub LLENAR()
        Try
            Dim variable As String = TextBox1.Text
            ListView1.Items.Clear()
            Dim renglon As New ListViewItem
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            'ABRO LA BASE
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlClient.SqlCommand("SELECT CMATE_002, DESC_002 FROM M_MATE_002 WHERE DESC_002 LIKE @D1+'%' ORDER BY DESC_002", cnn2)
            adaptadaor.Parameters.Add(New SqlParameter("D1", variable))
            adaptadaor.ExecuteNonQuery() 'LLENO EL ADAPTADOR CON EL DATASET
            Dim lector As SqlDataReader = adaptadaor.ExecuteReader
            While lector.Read
                renglon = New ListViewItem(lector.GetValue(0).ToString)
                renglon.SubItems.Add(lector.GetValue(1).ToString.ToString)
                ListView1.Items.Add(renglon)
            End While
            cnn2.Close()
            Label3.Text = ListView1.Items.Count
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    

    Public Function dato()

        Return Confirmacion

    End Function
    Public Function dniobtenido()

        Return CODIGO

    End Function
#End Region

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        LLENAR()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListView1.SelectedItems.Count <> 0 Then
            Confirmacion = True
            For i = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Selected = True Then
                    CODIGO = ListView1.Items(i).Text
                End If
            Next
        End If
        Me.Close()
    End Sub


    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedItems.Count <> 0 Then
            Confirmacion = True
            For i = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Selected = True Then
                    CODIGO = ListView1.Items(i).Text
                End If
            Next
        End If
        Me.Close()
    End Sub


  
End Class