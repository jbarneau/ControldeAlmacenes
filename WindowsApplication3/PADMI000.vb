Public Class PADMI000

    Private Sub PADMI000_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        If _usr.Activar_BT("PUSER001") = True Then
            Button6.Enabled = True
        End If
        If _usr.Activar_BT("PALMA100") = True Then
            Button5.Enabled = True
        End If
        If _usr.Activar_BT("POPER001") = True Then
            Button1.Enabled = True
        End If
        If _usr.Activar_BT("PPERF001") = True Then
            Button8.Enabled = True
        End If
        If _usr.Activar_BT("PPROV001") = True Then
            Button2.Enabled = True
        End If
        If _usr.Activar_BT("PCONT001") = True Then
            Button4.Enabled = True
        End If
        If _usr.Activar_BT("PMATE001") = True Then
            Button9.Enabled = True
        End If
        If _usr.Activar_BT("PCIUD001") = True Then
            Button7.Enabled = True
        End If
        If _usr.Activar_BT("PCIUD002") = True Then
            Button3.Enabled = True
        End If
        If _usr.Activar_BT("PDEPO001") = True Then
            Button11.Enabled = True
        End If
        If _usr.Activar_BT("PINFO001") = True Then
            Button10.Enabled = True
        End If
        If _usr.Activar_BT("PINFO006") = True Then
            Button19.Enabled = True
        End If
        If _usr.Activar_BT("PINFO005") = True Then
            Button21.Enabled = True
        End If
        If _usr.Activar_BT("PINFO002") = True Then
            Button12.Enabled = True
        End If
        If _usr.Activar_BT("PINFO004") = True Then
            Button14.Enabled = True
        End If
        If _usr.Activar_BT("PINFO007") = True Then
            Button20.Enabled = True
        End If
        If _usr.Activar_BT("PINFO003") = True Then
            Button13.Enabled = True
        End If
        If _usr.Activar_BT("PDEPO002") = True Then
            Button22.Enabled = True
        End If
        If _usr.Activar_BT("PCONF001") = True Then
            Button18.Enabled = True
        End If
        If _usr.Activar_BT("PCONF002") = True Then
            Button17.Enabled = True
        End If
        If _usr.Activar_BT("PCARG001") = True Then
            Button16.Enabled = True
        End If
        If _usr.Activar_BT("PALMA031") = True Then
            Button15.Enabled = True
        End If
        If _usr.Activar_BT("PINFO008") = True Then
            Button23.Enabled = True
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        Dim user As New PUSER001
        Me.Hide()
        user.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim local As New PCIUD002
        Me.Hide()
        local.ShowDialog()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim contratos As New PCONT001
        Me.Hide()
        contratos.ShowDialog()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim oper As New POPER001
        Me.Hide()
        oper.ShowDialog()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim prov As New PPROV001
        Me.Hide()
        prov.ShowDialog()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim alma As New PALMA100
        Me.Hide()
        alma.ShowDialog()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim perf As New PPERF001
        Me.Hide()
        perf.ShowDialog()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim mate As New PMATE001
        Me.Hide()
        mate.ShowDialog()
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim esta As New PINFO001
        Me.Hide()
        esta.ShowDialog()
    End Sub

    Private Sub B_SALIR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_SALIR.Click
        PMAIN001.llenar_pc()
        PMAIN001.Timer1.Start()
        PMAIN001.Show()
        Me.Close()


    End Sub
    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim PANTALLA As New PCONF001
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim PANTALLA As New PCONF002
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim local1 As New PCIUD001
        Me.Hide()
        local1.ShowDialog()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim PANTALLA As New PINFO002
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim PANTALLA As New PINFO003
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim PANTALLA As New PINFO004
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button11_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim PANTALLA As New PDEPO001
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim PANTALLA As New PCARG001
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim PNTALLA As New PALMA031
        Me.Hide()
        PNTALLA.ShowDialog()

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        Dim P As New PINFO006
        Me.Hide()
        P.ShowDialog()

    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        Dim P As New PINFO005
        Me.Hide()
        P.ShowDialog()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        Dim P As New PINFO007
        Me.Hide()
        P.ShowDialog()
    End Sub

    Private Sub GroupBox6_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox6.Enter

    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        Dim PANTALLA As New PDEPO002
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        Dim pantalla As New PINFO008
        Me.Hide()
        pantalla.ShowDialog()
    End Sub

    Private Sub Button24_Click(sender As System.Object, e As System.EventArgs) Handles Button24.Click
        Dim PANTALLA As New PINFO009
        Me.Hide()
        PANTALLA.ShowDialog()
    End Sub
End Class