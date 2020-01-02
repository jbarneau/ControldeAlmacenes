Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic
Public Class PALMA032
    Private motivo As String
    Private poliza As String
    Private estado As Integer
    Private medidor As String
    Private mensaje As New Clase_mensaje
    Private confirmacion As Boolean = False

    Public Sub lleer_datos(ByVal m As String, ByVal p As String, ByVal e As Integer, ByVal medi As String)
        motivo = m
        poliza = p
        estado = e
        medidor = medi

    End Sub
    Private Sub PALMA032_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If estado = 7 Then

            TXTerror.Text = motivo
            TXTpoliza.Text = poliza
            TXTerror.Enabled = False
            TXTpoliza.Enabled = False
            Button1.Visible = False
        End If
        If estado = 2 Then
            Button1.Visible = True
            TXTerror.Text = Nothing
            TXTpoliza.Text = Nothing
            TXTpoliza.Enabled = True
            TXTerror.Enabled = True
            TXTpoliza.Focus()
        End If

    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Me.Close()
    End Sub

    Private Function MODIFICAR_MEDIDOR(ByVal MEDIDOR As Decimal, ByVal POLIZA As String, ByVal MOTIVO As String) As Boolean
        Dim RESPUESTA As Boolean = False
        Try
            'creo la cadena de conexion
            Dim con1 As SqlConnection = New SqlConnection(conexion)
            'abro la cadena
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set POLIZA_E_102=@D1, MOTIVO_102=@D2, ESTADO_102=7, USER_102=@D3, F_INFO_102 = @D4 WHERE NSERIE_102=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", POLIZA))
            comando1.Parameters.Add(New SqlParameter("D2", MOTIVO))
            comando1.Parameters.Add(New SqlParameter("D3", _usr.Obt_Usr))
            comando1.Parameters.Add(New SqlParameter("D4", Date.Now))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
            con1.Close()
            RESPUESTA = True
        Catch ex As Exception
            RESPUESTA = False
        End Try
        Return RESPUESTA
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If IsNumeric(TXTpoliza.Text) And TXTpoliza.Text <> Nothing Then
            If Len(TXTpoliza.Text) <= 10 Then
                If TXTerror.Text <> Nothing And Len(TXTerror.Text) <= 50 Then
                    motivo = TXTerror.Text
                    poliza = TXTpoliza.Text
                    If MODIFICAR_MEDIDOR(medidor, poliza, motivo) = True Then
                        confirmacion = True
                        Me.Close()
                    Else
                        mensaje.MERRO001()
                    End If
                Else
                    MessageBox.Show("VERIFIQUE EL TEXTO DEL ERRO", "ERROR EN MOTIVO", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("LA POLIZA TIENE MAS DE 10 DIGITOS", "ERROR EN POLIZA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("LA POLIZA DEBE SER NUMERICA", "POLIZA INCORRECTA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If



    End Sub
    Public Function confir() As Boolean
        Return confirmacion
    End Function

    
    Private Function validar() As Boolean
        Dim resp As Boolean = False
        If IsNumeric(TXTpoliza.Text) And TXTpoliza.Text <> Nothing Then
            If Len(TXTpoliza.Text) <= 10 Then
                If TXTerror.Text <> Nothing And Len(TXTerror.Text) <= 50 Then
                    resp = True
                End If
            End If
        End If
        Return resp
    End Function
End Class