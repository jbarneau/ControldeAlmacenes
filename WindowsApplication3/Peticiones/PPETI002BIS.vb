Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO

Public Class PPETI002BIS
    Private Mensaje As New Clase_mensaje
    Private OC As New Class_OC
    Private resp As Boolean = False
    Private npeti As Decimal
    Private Sub PPETI002BIS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Function respuesta() As Boolean
        Return resp
    End Function
    Public Function NUMERO_PETICION() As Decimal
        Return npeti
    End Function

 

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        resp = False
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text <> Nothing And IsNumeric(TextBox1.Text) And TextBox1.Text <> 0 Then
            If OC.VALIDAR_NPETI(TextBox1.Text) = False Then
                resp = True
                npeti = TextBox1.Text
                Me.Close()
            Else
                Mensaje.MERRO017()

            End If
        Else
            Mensaje.MERRO006()

        End If
    End Sub

   
End Class