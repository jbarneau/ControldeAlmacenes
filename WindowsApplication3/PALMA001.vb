Public Class PALMA001

   

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim palma2 As New PALMA002
        Me.Hide()
        palma2.ShowDialog()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim palma As New PALMA003
        Me.Hide()
        palma.ShowDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim palma3 As New PALMA026
        Me.Hide()
        palma3.ShowDialog()
    End Sub

    Private Sub PALMA001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        If _usr.Activar_BT("PALMA002") = True Then
            Button6.Enabled = True
        End If
        If _usr.Activar_BT("PALMA003") = True Then
            Button5.Enabled = True
        End If
        If _usr.Activar_BT("PALMA004") = True Then
            Button4.Enabled = True
        End If
        If _usr.Activar_BT("PALMA005") = True Then
            Button3.Enabled = True
        End If
        If _usr.Activar_BT("PALMA026") = True Then
            Button1.Enabled = True
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim palma2 As New PALMA004
        Me.Hide()
        palma2.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim palma2 As New PALMA005
        Me.Hide()
        palma2.ShowDialog()
    End Sub
End Class