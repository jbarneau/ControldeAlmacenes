Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALAMA021_BIS
    Private operario As String
    Private respuesta As Boolean = False
    Private Nserie As String
    Private tipo As String
    Public Sub Tomar(ByVal mioperario As String)
        operario = mioperario
    End Sub
    Public ReadOnly Property Leer_respuesta
        Get
            Return respuesta
        End Get
    End Property
    Public ReadOnly Property Leer_serie
        Get
            Return Nserie
        End Get
    End Property
    Private Sub llenar_TIPO_MAQUINA()
        'CONECTO LA BASE
        Dim TABLA As New DataTable
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptador As New SqlDataAdapter("SELECT TIPO_806, DESC_806 FROM P_MAQUINA_806 ORDER BY DESC_806", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptador.Fill(TABLA)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        CBTIPO.DataSource = TABLA
        CBTIPO.DisplayMember = "DESC_806"
        CBTIPO.ValueMember = "TIPO_806"
        CBTIPO.Text = Nothing
    End Sub
    Private Sub llenar_todo()
        DataGridView1.Rows.Clear()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT NSERIE_006, DESC_806, MARCA_006,MODELO_006,ALMA_006 FROM M_MAQUI_006 INNER JOIN P_MAQUINA_806 ON TIPO_806=TIPO_006 WHERE FBAJA_006 IS NULL AND ALMA_006 = @D1 AND ESTADO_006 = 1 ORDER BY NSERIE_006", cnn)
            adt.Parameters.Add(New SqlParameter("D1", operario))
            Dim LECTR As SqlDataReader = adt.ExecuteReader
            Do While LECTR.Read
                DataGridView1.Rows.Add(LECTR.GetValue(0).ToString, LECTR.GetValue(1).ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try

    End Sub
    Private Sub llenarxTIPO()
        DataGridView1.Rows.Clear()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT NSERIE_006, DESC_806, MARCA_006,MODELO_006,ALMA_006 FROM M_MAQUI_006 INNER JOIN P_MAQUINA_806 ON TIPO_806=TIPO_006 WHERE TIPO_006=@D1 AND ALMA_006 = @D2 AND FBAJA_006 IS NULL AND ESTADO_006 = 1 ORDER BY NSERIE_006", cnn)
            adt.Parameters.Add(New SqlParameter("D1", tipo))
            adt.Parameters.Add(New SqlParameter("D2", operario))

            Dim LECTR As SqlDataReader = adt.ExecuteReader
            Do While LECTR.Read
                DataGridView1.Rows.Add(LECTR.GetValue(0).ToString, LECTR.GetValue(1).ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub PALAMA021_BIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        llenar_TIPO_MAQUINA()
        llenar_todo()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.Rows.Count <> 0 Then
            Nserie = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value.ToString
            respuesta = True
            Me.Close()
        End If
    End Sub

    Private Sub CBTIPO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBTIPO.SelectedIndexChanged
        If CBTIPO.ValueMember <> Nothing And CBTIPO.Text <> Nothing Then
            tipo = CBTIPO.SelectedValue
            llenarxTIPO()
        End If
    End Sub
End Class