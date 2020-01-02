Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.IO

Public Class PCOMB004
    Private nticket As String

    Private Sub EliminarTicket(ByVal ticket As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("DELETE FROM T_VALE_122 WHERE NVALE122 = @D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", ticket))
            If (adt.ExecuteNonQuery() <> 0) Then
                MessageBox.Show("SE HA ELIMINADO CORRECTAMENTE EL TICKET NRO: " + "" + ticket, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("NO SE ENCONTRO EL TICKET EN LA BASE DE DATOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Throw New Exception("ERROR en EliminarTicket")
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub BorrarCampos()
        txtnticket.Clear()
        txtaño.Clear()
    End Sub

    Private Sub btnbuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscar.Click
        nticket = txtaño.Text + txtnticket.Text.PadLeft(8, "0")
        Dim PANTALLA As New PCOMB004_bis
        PANTALLA.TOMAR(nticket)
        PANTALLA.ShowDialog()
        If PANTALLA.LEERRESP() = True Then
            If MessageBox.Show("DESEA ELIMINAR EL TICKET SELECCIONADO?", "ATENCIÒN", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) = DialogResult.OK Then
                EliminarTicket(PANTALLA.LEERTICKET)
                BorrarCampos()
            End If
        End If
    End Sub


    Private Sub PCOMB004_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnbuscar.Enabled = False
    End Sub

    Private Sub txtnticket_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtnticket.TextChanged
        If (txtaño.Text <> Nothing And txtnticket.Text <> Nothing) Then
            btnbuscar.Enabled = True
        Else
            btnbuscar.Enabled = False
        End If
    End Sub

    Private Sub txtaño_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtaño.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub

    Private Sub txtnticket_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnticket.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub
End Class