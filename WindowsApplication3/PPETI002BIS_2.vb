Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Public Class PPETI002BIS_2
    Private Mensaje As New Clase_mensaje
    Private OC As New Class_OC
    Private resp As Boolean = False
    Private tcierre As Integer
    Private Sub PPETI002BIS_2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenar()
    End Sub
    Public Function respuesta() As Boolean
        Return resp
    End Function
    Public Function tipo_cierre() As Integer
        Return tcierre
    End Function
    Private Sub llenar()
        Dim ds As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 11 AND F_BAJA_802 is NULL order by DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(ds, "DET_PARAMETRO_802")
        cnn2.Close()
        ComboBox1.DataSource = ds.Tables("DET_PARAMETRO_802")
        ComboBox1.DisplayMember = "DESC_802"
        ComboBox1.ValueMember = "C_PARA_802"
        ComboBox1.Text = Nothing
    End Sub







    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        resp = False
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.ValueMember <> Nothing And ComboBox1.Text <> Nothing Then

            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ComboBox1.Text <> Nothing Then
            tcierre = ComboBox1.SelectedValue
            resp = True
            Me.Close()
        End If
    End Sub
End Class