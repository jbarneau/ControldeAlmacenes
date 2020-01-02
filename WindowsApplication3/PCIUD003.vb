Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PCIUD003

    Private Sub PCIUD003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        'LLENADO DE LOCALIDADES DADAS DE BAJA............
        Dim cnn3 As SqlConnection = New SqlConnection(conexion)
        cnn3.Open()
        Dim consulta3 As New SqlClient.SqlCommand("select DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 = 8) and (F_BAJA_802 IS NOT NULL)", cnn3)
        consulta3.ExecuteNonQuery()
        Dim nivel3 As SqlDataReader = consulta3.ExecuteReader()
        While (nivel3.Read())
            ComboBox1.Items.Add(nivel3.GetString(0))
        End While
        cnn3.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim alta As New SqlClient.SqlCommand("UPDATE DET_PARAMETRO_802 SET F_BAJA_802 = NULL where (C_TABLA_802 =8)  and (DESC_802 = @C2) ", cnn2)
        alta.Parameters.Add(New SqlParameter("C2", ComboBox1.Text))
        alta.ExecuteNonQuery()
        cnn2.Close()
        ComboBox1.Items.Remove(ComboBox1.Text)
        Me.Close()
    End Sub

End Class