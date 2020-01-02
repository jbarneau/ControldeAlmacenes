Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PMAIL001
    Private EMAIL As String = ""
    Private BANDERA As Boolean = False
    Private Sub llenar()
        ListBox1.Items.Clear()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        'AND NDOC_001 <> @D1
        Dim comando1 As New SqlClient.SqlCommand("select EMAIL_001 FROM M_USRS_001 where EMAIL_001 is not null and SEND_001=1 ORDER BY EMAIL_001", cnn2)
        'comando1.Parameters.Add(New SqlParameter("D1", _usr.Obt_Usr))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While LECTOR1.Read
            ListBox1.Items.Add(lector1.GetValue(0))
        End While
        cnn2.Close()
        'For i = 0 To ListBox1.Items.Count - 1
        'ListBox1.SelectedIndices.Add(i)
        ' Next
    End Sub
    Private Sub MAIL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenar()
    End Sub
    Private Sub CREARDIRECCION()

        For i = 0 To ListBox1.Items.Count - 1
            If ListBox1.GetSelected(i) = True Then
                If BANDERA = False Then
                    EMAIL = ListBox1.Items(i).ToString
                    BANDERA = True
                Else
                    EMAIL = EMAIL & ", " & ListBox1.Items(i).ToString
                End If
            End If
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CREARDIRECCION()
        If BANDERA = True Then
            Me.Close()
        Else
            MessageBox.Show("DEBE SELECCIONAR ALMENOS UN MAIL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Public Function DIRECCIONMAIL() As String
        Return EMAIL
    End Function
End Class