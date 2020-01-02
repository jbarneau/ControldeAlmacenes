Imports Microsoft.Reporting.WinForms
Public Class PALMA021bis2

    Private Sub PALMA021bis2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ReportViewer1.RefreshReport()
    End Sub

    Public Sub GetDatosRem(ByVal lista As List(Of Transpaso))
        Dim source As New ReportDataSource
        source.Value = lista
        source.Name = "DataSet1"
        ReportViewer1.LocalReport.DataSources.Add(source)
        ReportViewer1.RefreshReport()
    End Sub

End Class