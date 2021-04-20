Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class PALMA046BIS_3

    'Private med() As Clase_med_retirar
    'Private nmedidor As Decimal
    'Private codsap As String
    'Private codfamilia As Integer
    'Private descfamilia As String

    Private Sub PALMA046BIS_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New(ByVal medcompleto As List(Of Clase_med_retirar))
        InitializeComponent()
        LlenarGrilla(medcompleto)
        'nmedidor = medcompleto.GETSETNMED
        'codsap = medcompleto.GETSETSAP
        'codfamilia = medcompleto.GETSETCODFAMILIA
        'descfamilia = medcompleto.GETSETNOMFAMILIA
    End Sub

    Private Sub PALMA046BIS_3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'Hide()
        'Dim p As New PALMA046()
        'p.ShowDialog()
        'p.Dispose()
    End Sub

    Private Sub LlenarGrilla(ByVal nmed As List(Of Clase_med_retirar))
        dgvmostrarcajon.Rows.Add(nmed.Count)
        For index = 0 To dgvmostrarcajon.Rows.Count - 2
            dgvmostrarcajon.Rows(index).Cells(0).Value = nmed(index).GETSETNMED
            dgvmostrarcajon.Rows(index).Cells(1).Value = nmed(index).GETSETSAP
            dgvmostrarcajon.Rows(index).Cells(2).Value = nmed(index).GETSETNOMFAMILIA
        Next
    End Sub
End Class