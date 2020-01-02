Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class PALMA042BIS
    Dim NMOV As Decimal
    Public Sub LLER(NUMERO As Decimal)
        NMOV = NUMERO
    End Sub
    Private Sub LLENAR(ByVal DEPOSITO As Decimal)
        ListView1.Items.Clear()
        Dim DATA As New DataTable
        Dim renglon As New ListViewItem
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NCAJON_116 FROM T_TRAN_MED_RET_116 WHERE (NMOVI_116=@D1)  ORDER BY NCAJON_116", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", DEPOSITO))
        adaptadaor.Fill(DATA)
        cnn2.Close()
        For i = 0 To DATA.Rows.Count - 1
            'MessageBox.Show(DATA.Rows(i).Item(0).ToString + " : " + DATA.Rows(i).Item(1).ToString)
            renglon = New ListViewItem(DATA.Rows(i).Item(0).ToString)
            ListView1.Items.Add(renglon)
        Next
    End Sub
    Private Sub PALMA042BIS_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LLENAR(NMOV)
    End Sub
End Class