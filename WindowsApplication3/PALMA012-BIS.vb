Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA012_BIS
    Private Respuesta As Boolean = False
    Private codigo As String
    Private Descripcion As String
    Private micod As String
    Public Sub tomardato(ByVal tomar As String)
        micod = tomar
    End Sub
    Public ReadOnly Property ler_respuest
        Get
            Return Respuesta
        End Get
    End Property
    Public ReadOnly Property leer_codigo
        Get
            Return codigo
        End Get
    End Property
    Public ReadOnly Property leer_descripcion
        Get
            Return Descripcion
        End Get
    End Property
    Private Sub llenar()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 17 AND C_PARA_802 <> @D1 ORDER BY DESC_802", cnn)
            adt.Parameters.Add(New SqlParameter("D1", micod))
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            Do While LECTOR.Read
                DataGridView1.Rows.Add(LECTOR.GetValue(0), LECTOR.GetValue(1))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
            DataGridView1.ClearSelection()
        End Try
    End Sub
    Private Sub PALMA012_BIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenar()

    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.Rows.Count <> 0 Then
            Respuesta = True
            codigo = DataGridView1.Item(0, DataGridView1.CurrentRow.Index()).Value.ToString
            Descripcion = DataGridView1.Item(1, DataGridView1.CurrentRow.Index()).Value.ToString
            Me.Close()
        End If
    End Sub
End Class