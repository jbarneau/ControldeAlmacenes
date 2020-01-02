Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class PPASS001
    Private MENSAJE As New Clase_mensaje

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If TextBox1.Text = TextBox2.Text Then
            Try
                cambiar_pass(TextBox1.Text)
                MENSAJE.MADVE003()
                Me.Close()
            Catch ex As Exception
                MENSAJE.MERRO001()
            End Try
        Else
            MENSAJE.MERRO021()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub cambiar_pass(ByVal pass As String)
        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        cnn7.Open()
        Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_USRS_001 SET PASS_001=@C4 WHERE NDOC_001 = @C8 ", cnn7)
        consulta7.Parameters.Add(New SqlParameter("C4", pass))
        consulta7.Parameters.Add(New SqlParameter("C8", _usr.Obt_Usr))
        consulta7.ExecuteNonQuery()
        cnn7.Close()
    End Sub


    Private Sub PPASS001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class


