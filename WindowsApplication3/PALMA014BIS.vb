Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Class PALMA014BIS
    Private remito As Decimal
    Private material As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Public Sub Grabar_datos(ByVal remi As Decimal, ByVal mate As String)
        remito = remi
        material = mate
    End Sub
    Public Sub llenarLIST()
        ListBox1.Items.Clear()
        Dim numero As String
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        'Consulta SQL...
        Dim comando2 As New SqlClient.SqlCommand("select NSERIE FROM TEMP_TRANS_MED WHERE N_REMI= @D1 AND CODMATE=@D2", cnn2)
        comando2.Parameters.Add(New SqlParameter("D1", remito))
        comando2.Parameters.Add(New SqlParameter("D2", material))
        comando2.ExecuteNonQuery()
        Dim LECTOR As SqlDataReader = comando2.ExecuteReader()
        While (LECTOR.Read())
            numero = LECTOR.GetValue(0)
            ListBox1.Items.Add(numero.PadLeft(8, "0"))
        End While
        cnn2.Close()
    End Sub


    Private Sub PALMA014BIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenarLIST()
        Button1.Focus()
    End Sub
End Class