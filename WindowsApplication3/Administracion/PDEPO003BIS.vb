Public Class PDEPO003BIS
    Dim RESP As Boolean = False
    Dim CANT As Decimal
    Public Sub TOOMAR(MICAN As Decimal)
        CANT = MICAN
    End Sub
    Public ReadOnly Property LLERRESP
        Get
            Return RESP
        End Get
    End Property
    Public ReadOnly Property LLERCANT
        Get
            Return CANT
        End Get
    End Property
    Private Sub PDEPO003BIS_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        NumericUpDown1.Value = CANT
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If CANT <> NumericUpDown1.Value Then
            CANT = NumericUpDown1.Value
            If CANT = 0 Then
                RESP = False
            Else
                RESP = True
            End If
        Else
            RESP = False
        End If
        Me.Close()
    End Sub
End Class