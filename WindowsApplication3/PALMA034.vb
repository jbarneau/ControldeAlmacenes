Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA034
    Private RESPUESTA As Boolean = False
    Private FAMILIA As Integer
    Private DEPOSITO As String
    Private CAJON As String
    Public Sub TOMAR(MIDEPOSITO As String, MIFAMILIA As Integer)
        FAMILIA = MIFAMILIA
        DEPOSITO = MIDEPOSITO
    End Sub
    Private Sub PALMA034_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LLENAR()
    End Sub
    Private Sub LLENAR()
        Dim DATA As New DataTable
        Dim renglon As New ListViewItem
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CAJON_113, COUNT(NSERI_113) AS Expr1 FROM T_MED_DEVO_113 WHERE (DEPOSI_113 = @D1) AND (ESTADO_113 = 1 OR ESTADO_113=9) AND (FAMILIA_113 = @D2) GROUP BY CAJON_113 ORDER BY CAJON_113", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", DEPOSITO))
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D2", FAMILIA))
        adaptadaor.Fill(DATA)
        cnn2.Close()
        For i = 0 To DATA.Rows.Count - 1
            DataGridView1.Rows.Add(DATA.Rows(i).Item(0).ToString, DATA.Rows(i).Item(1).ToString)
        Next
    End Sub
    Public ReadOnly Property LLERRESPUESTA
        Get
            Return RESPUESTA
        End Get
    End Property
    Public ReadOnly Property LEERCAJON
        Get
            Return CAJON
        End Get
    End Property

   
    Private Sub DataGridView1_DoubleClick(sender As Object, e As System.EventArgs) Handles DataGridView1.DoubleClick
        RESPUESTA = True
        CAJON = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
        Me.Close()

    End Sub

    
End Class