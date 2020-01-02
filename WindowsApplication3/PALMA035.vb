Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class PALMA035
    Private RESP As Boolean = False
    Private COD As String
    Private DES As String
    Public ReadOnly Property LEERERPUESTA
        Get
            Return RESP
        End Get
    End Property
    Public ReadOnly Property LEERCOD
        Get
            Return COD
        End Get
    End Property
    Public ReadOnly Property LEERDES
        Get
            Return DES
        End Get
    End Property
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedItems.Count <> 0 Then
            RESP = True
            For i = 0 To ListView1.Items.Count - 1
                If ListView1.Items(i).Selected = True Then
                    COD = ListView1.Items(i).Text
                    DES = ListView1.Items(i).SubItems(1).Text
                End If
            Next
        End If
        Me.Close()
    End Sub
    Private Sub LLENAR()
        Try
            ListView1.Items.Clear()
            Dim renglon As New ListViewItem
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            'ABRO LA BASE
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlClient.SqlCommand("SELECT P_CAPACIDAD_804.DESC_804, M_MATE_002.DESC_002 FROM P_CAPACIDAD_804 INNER JOIN M_MATE_002 ON P_CAPACIDAD_804.DESC_804 = M_MATE_002.CMATE_002 GROUP BY P_CAPACIDAD_804.DESC_804, M_MATE_002.DESC_002 ORDER BY P_CAPACIDAD_804.DESC_804", cnn2)
            adaptadaor.ExecuteNonQuery() 'LLENO EL ADAPTADOR CON EL DATASET
            Dim lector As SqlDataReader = adaptadaor.ExecuteReader
            While lector.Read
                renglon = New ListViewItem(lector.GetValue(0).ToString)
                renglon.SubItems.Add(lector.GetValue(1).ToString.ToString)
                ListView1.Items.Add(renglon)
            End While
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub PALAMA035_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LLENAR()
    End Sub
End Class