Public Class PALMA039BIS
    Dim DIAS As Integer
    Public Sub TOMAR(MIDIAS As Integer)
        DIAS = MIDIAS
    End Sub
    Public ReadOnly Property LEERDIAS
        Get
            Return DIAS
        End Get
    End Property
    Private Sub PALMA039BIS_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        NUP1.Value = DIAS
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        DIAS = NUP1.Value
        Me.Close()
    End Sub
End Class