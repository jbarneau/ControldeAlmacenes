Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.IO


Public Class PCOMB002bis
    Private DOMINIO As String
    Private RESP As Boolean = False
    Private TICKET As String
    Private LITROS As String
    Private COMBUSTIBLE As String

    Public Sub TOMAR(ByVal MIDOMINO)
        DOMINIO = MIDOMINO
    End Sub

    'Private Sub LlenarTxtTicket()
    '    Dim cnn As SqlConnection = New SqlConnection(conexion)
    '    Try
    '        cnn.Open()
    '        Dim comando As New SqlCommand("SELECT NVALE122 FROM T_VALE_122 WHERE DOMINI122 = @P1", cnn)
    '        comando.Parameters.Add(New SqlParameter("P1", Dominio))
    '        parsear = comando.ExecuteScalar()
    '        año = parsear.Substring(0, 3)
    '        nticket = parsear.Substring(4)
    '    Catch ex As Exception
    '    Finally
    '        cnn.Close()
    '    End Try
    'End Sub
    Public ReadOnly Property LEERRESP
        Get
            Return RESP
        End Get
    End Property
    Public ReadOnly Property LEERTICKET
        Get
            Return TICKET
        End Get
    End Property


    Private Sub DataGridView1_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV1.CellContentDoubleClick
        TICKET = DGV1.Item(0, DGV1.CurrentRow.Index).Value.ToString
        RESP = True
        Me.Close()
    End Sub
    Private Sub BuscarEnDb(ByVal dom As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Dim dt As DataTable = New DataTable()
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select NVALE122 AS VALE, FECHA122 AS FECHA FROM T_VALE_122 WHERE DOMINI122 = @D1 AND FTICKET122 IS NULL ORDER BY FECHA", cnn)
            adt.Parameters.Add(New SqlParameter("D1", dom))
            Dim adapt As New SqlDataAdapter(adt)
            adapt.Fill(dt)
            If dt.Rows.Count <> 0 Then
                For I = 0 To dt.Rows.Count - 1
                    DGV1.Rows.Add(dt.Rows(I).Item(0), CDate(dt.Rows(I).Item(1)).ToShortDateString)
                Next
            End If
        Catch
            Throw New Exception("ERROR EN FUNCIÒN BUSCARENDB")
        Finally
            cnn.Close()
        End Try
    End Sub
    
    Private Sub PCOMB002bis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BuscarEnDb(DOMINIO)
    End Sub

End Class