Public Class PALMA010BIS2
    Private lISTa As New INSTRUMENTO

    Public Sub LEER(MILISTA As INSTRUMENTO)
        lISTa = MILISTA
    End Sub
    Private Sub PALMA010BIS2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim P As New LISTADO_CALIBRACION
        P.SetDataSource(lISTa)
        CrystalReportViewer1.ReportSource = P
    End Sub
End Class