Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA010BIS
#Region "VARIABLES"
    Private TIPO As Integer = 0
    Private DEPOSITO As String
    Private res As Boolean = False
    Private nserie As String

#End Region
    Private Sub PALMA010BIS_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        llenar_TIPO_MAQUINA()
        llenar_todo()
    End Sub
    Private Function NOM_DEPOSITOS2(ByVal cod As String) As String
        Dim resp As String = ""
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptador As New SqlCommand("SELECT (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE NDOC_003=@D1", cnn2)
            adaptador.Parameters.Add(New SqlParameter("D1", cod))
            'LLENO EL ADAPTADOR CON EL DATASET
            Dim lector As SqlDataReader = adaptador.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        Return resp
    End Function
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
    Public ReadOnly Property llerrespuesta
        Get
            Return res
        End Get
    End Property
    Public ReadOnly Property leernserie
        Get
            Return nserie
        End Get
    End Property

    Private Sub llenar_todo()
        DataGridView1.Rows.Clear()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT NSERIE_006, DESC_806, MARCA_006,MODELO_006,ALMA_006, CHAPA_006 FROM M_MAQUI_006 INNER JOIN P_MAQUINA_806 ON TIPO_806=TIPO_006 WHERE FBAJA_006 IS NULL ORDER BY NSERIE_006", cnn)
            Dim LECTR As SqlDataReader = adt.ExecuteReader
            Do While LECTR.Read
                DataGridView1.Rows.Add(LECTR.GetValue(0).ToString, LECTR.GetValue(1).ToString, LECTR.GetValue(2).ToString, LECTR.GetValue(3).ToString, NOM_DEPOSITOS2(LECTR.GetValue(4).ToString), LECTR.GetValue(5).ToString)
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
            Dim adt As New SqlCommand("SELECT NSERIE_006, DESC_806, MARCA_006,MODELO_006,ALMA_006 FROM M_MAQUI_006 INNER JOIN P_MAQUINA_806 ON TIPO_806=TIPO_006 WHERE TIPO_006=@D1 AND  FBAJA_006 IS NULL ORDER BY NSERIE_006", cnn)
            adt.Parameters.Add(New SqlParameter("D1", TIPO))
            Dim LECTR As SqlDataReader = adt.ExecuteReader
            Do While LECTR.Read
                DataGridView1.Rows.Add(LECTR.GetValue(0).ToString, LECTR.GetValue(1).ToString, LECTR.GetValue(2).ToString, LECTR.GetValue(3).ToString, NOM_DEPOSITOS2(LECTR.GetValue(4).ToString))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try

    End Sub

   
    Private Sub CBTIPO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBTIPO.SelectedIndexChanged
        If CBTIPO.ValueMember <> Nothing And CBTIPO.Text <> Nothing Then
            TIPO = CBTIPO.SelectedValue
            llenarxTIPO()
        End If
        '        llenarxTIPO()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.Rows.Count <> 0 Then
            nserie = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value.ToString
            res = True
            Me.Close()
        End If
    End Sub

    Private Sub Txtnserie_TextChanged(sender As Object, e As EventArgs) Handles txtnserie.TextChanged
        If txtnserie.Text <> Nothing Then
            FiltroxNserie()
        End If
    End Sub

    Private Sub Txtnchapa_TextChanged(sender As Object, e As EventArgs) Handles txtnchapa.TextChanged
        FiltroxNchapa()
    End Sub

    Private Sub FiltroxNserie()
        DataGridView1.Rows.Clear()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT NSERIE_006, DESC_806, MARCA_006,MODELO_006,ALMA_006, CHAPA_006 FROM M_MAQUI_006 INNER JOIN P_MAQUINA_806 ON TIPO_806=TIPO_006 WHERE NSERIE_006 LIKE N'%' + @D1 + '%' AND FBAJA_006 IS NULL ORDER BY NSERIE_006", cnn)
            adt.Parameters.Add(New SqlParameter("D1", txtnserie.Text))
            Dim LECTR As SqlDataReader = adt.ExecuteReader
            Do While LECTR.Read
                DataGridView1.Rows.Add(LECTR.GetValue(0).ToString, LECTR.GetValue(1).ToString, LECTR.GetValue(2).ToString, LECTR.GetValue(3).ToString, NOM_DEPOSITOS2(LECTR.GetValue(4).ToString), LECTR.GetValue(5).ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub FiltroxNchapa()
        DataGridView1.Rows.Clear()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT NSERIE_006, DESC_806, MARCA_006,MODELO_006,ALMA_006, CHAPA_006 FROM M_MAQUI_006 INNER JOIN P_MAQUINA_806 ON TIPO_806=TIPO_006 WHERE CHAPA_006 LIKE N'%' + @D1 + '%' AND FBAJA_006 IS NULL ORDER BY NSERIE_006", cnn)
            adt.Parameters.Add(New SqlParameter("D1", txtnchapa.Text))
            Dim LECTR As SqlDataReader = adt.ExecuteReader
            Do While LECTR.Read
                DataGridView1.Rows.Add(LECTR.GetValue(0).ToString, LECTR.GetValue(1).ToString, LECTR.GetValue(2).ToString, LECTR.GetValue(3).ToString, NOM_DEPOSITOS2(LECTR.GetValue(4).ToString), LECTR.GetValue(5).ToString)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
End Class