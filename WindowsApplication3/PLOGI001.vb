Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Imports System.Deployment.Application

Public Class PLOGI001
    Private cont As Integer = 0
    Private Mensaje As New Clase_mensaje



    'salida del sistema
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim res As DialogResult
        res = MessageBox.Show("Realmente, ¿Desea Salir?", "MADVE001", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If res = System.Windows.Forms.DialogResult.Yes Then
            DestroyHandle()
        End If
    End Sub
    'boton ingreso
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MAIN.Archivo_conexion(CheckBox1, TextBox3.Text)
        'incremento el contador de intentos
        cont = cont + 1
        Dim conf As Integer
        'pregunto si el textbox de usuario esta vacio 
        Dim prueba As SqlConnection = New SqlConnection(conexion)
        Try
            prueba.Open()
            If prueba.State = ConnectionState.Open Then
                ' prueba.Close()
                If TextBox2.Text = Nothing Then
                    Mensaje.MERRO002()
                    TextBox2.Focus()
                    If cont = 3 Then
                        Mensaje.MERRO003()
                        Me.Close()
                    End If
                Else
                    conf = _usr.Existe_USR(TextBox2.Text, TextBox1.Text)
                    Select Case conf
                        Case Is = 0
                            Me.Close()
                        Case Is = 1
                            TextBox2.Focus() : TextBox2.SelectAll()
                            If cont = 3 Then
                                Mensaje.MERRO003()
                                Me.Close()
                            End If
                        Case Is = 2
                            TextBox1.Focus() : TextBox1.SelectAll()
                            If cont = 3 Then
                                Mensaje.MERRO003()
                                Me.Close()
                            End If
                        Case Is = 3
                            Me.Hide()
                            'PALMA039.ShowDialog()
                            PMAIN001.ShowDialog()
                            Me.Close()
                    End Select

                End If
            Else
                MessageBox.Show("NO EXISTE LA BASE DE DATOS O NO LA DIRECCION ES INCORRECTA", "ERROR EN BASE DE DATOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)

        Finally
            prueba.Close()
            'Mensaje.MERRO001()
        End Try






    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            TextBox1.Focus()
        End If
    End Sub



    Private Function Minusculas(ByVal Texto As String, ByVal TXT As TextBox) As String
        Minusculas = LCase(Texto) ' LCase se encarga de transformar el texto en minuscula UCase a mayuscula
        TXT.SelectionStart = Len(Texto) ' Dejamos el cursor al final del texto 
    End Function


    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox3.Visible = True
        Else
            TextBox3.Visible = False
        End If

    End Sub



    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Button1.Focus()
        End If
    End Sub


    Private Sub PLOGI001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If (ApplicationDeployment.IsNetworkDeployed) Then
            lblver.Text = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        Else
            lblver.Text = Application.ProductVersion
        End If
    End Sub
End Class