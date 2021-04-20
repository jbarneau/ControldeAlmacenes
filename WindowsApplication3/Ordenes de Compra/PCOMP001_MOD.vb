Public Class PCOMP001_MOD
    Public Textocabeza As String
    Private Valor As Decimal
    Private confirmacion As Boolean = False
    Public ReadOnly Property leerValor
        Get
            Return Valor
        End Get
    End Property
    Public ReadOnly Property leerConfirmacion
        Get
            Return confirmacion
        End Get
    End Property
    Public Sub tomar(textocabeza As String)
        Me.Textocabeza = textocabeza
    End Sub

    Private Sub PCOM001_MOD_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = Textocabeza
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> Nothing Then
            If TextBox1.Text <> "" Then
                If IsNumeric(TextBox1.Text.Replace(".", ",")) Then
                    confirmacion = True
                    Valor = CDec(TextBox1.Text.Replace(".", ","))
                    Me.Close()


                Else
                    MessageBox.Show("EL VALOR DEBE SER NUMERICO")
                End If
            Else
                MessageBox.Show("NO SE INGRESO NUEVO VALOR")
            End If
        Else
            MessageBox.Show("NO SE INGRESO NUEVO VALOR")
        End If
    End Sub
End Class