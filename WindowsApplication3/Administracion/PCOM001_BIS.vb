Public Class PCOMB001_BIS
    Private resp As Boolean = False
    Private TextoF As String = ""
    Public ReadOnly Property leerRespuesta
        Get
            Return resp
        End Get
    End Property
    Public ReadOnly Property leerTexto
        Get
            Return TextoF
        End Get
    End Property

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or IsNothing(TextBox1.Text) Then
            MessageBox.Show("ES NECESARIO INGRESAR UN TEXTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            resp = True
            TextoF = TextBox1.Text
            Me.Close()
        End If
    End Sub


    Private Sub PCOMB001_BIS_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If TextBox1.Text = "" Or IsNothing(TextBox1.Text) Then
                MessageBox.Show("ES NECESARIO INGRESAR UN TEXTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                resp = True
                TextoF = TextBox1.Text
                Me.Close()
            End If
        End If
    End Sub
End Class