Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Deployment.Application

Public Class PMAIN001
    Private METODOS As New Clas_Almacen
    Private minuto As Integer = 0
    Private bodymailstock As New List(Of Clase_mensaje)
    Private Sub PMAIN001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_pc()
        imagenes()
        activar()
        If (ApplicationDeployment.IsNetworkDeployed) Then
            lblver.Text = "v" + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
        Else
            lblver.Text = "v" + Application.ProductVersion
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        DestroyHandle()
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim p As New PALER001
        p.ShowDialog()
    End Sub

#Region "FUNCIONES"
    Private Sub Cant_Peticiones_Sin_aprobar()
        'lee el ultimo numero de remito
        Dim contador As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select N_OC_105 From T_C_OC_105 where ESTA_105=1 AND TIPO_OC_105=2", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador += 1
        End While
        con1.Close()
        TextBox5.Text = contador
    End Sub
    Private Sub Cant_Peticiones_Sin_nfng()
        'lee el ultimo numero de remito
        Dim contador As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select N_OC_105 From T_C_OC_105 where ESTA_105=2 AND TIPO_OC_105=2", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador += 1
        End While
        con1.Close()
        TextBox12.Text = contador
    End Sub
    Private Sub Cant_OC_Sin_aprobar()
        'lee el ultimo numero de remito
        Dim contador As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select N_OC_105 From T_C_OC_105 where ESTA_105=1 AND TIPO_OC_105=1", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador += 1
        End While
        con1.Close()
        TextBox6.Text = contador
    End Sub
    Private Sub Transferencias_SIN_aprobar()
        'lee el ultimo numero de remito
        Dim contador As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select N_REMI_110 From TEMP_TRANSFERENCIA GROUP BY N_REMI_110", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador += 1
        End While
        con1.Close()
        TextBox4.Text = contador
    End Sub

    Private Sub Medidores_sin_asignar()
        'lee el ultimo numero de remito
        Dim contador As Decimal = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select CANT_109 From T_MED_SA_109 WHERE CDEPO_109 <>@D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", "11"))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador = contador + lector1.GetValue(0)
        End While
        con1.Close()
        TextBox2.Text = contador
    End Sub
    Private Sub Medidores_sin_registrar()
        'lee el ultimo numero de remito
        Dim contador As Decimal = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select CANT_109 From T_MED_SA_109 WHERE CDEPO_109 =@D1", con1)
        comando1.Parameters.Add(New SqlParameter("D1", "11"))
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            If lector1.GetValue(0) > 0 Then
                contador = contador + lector1.GetValue(0)
            End If

        End While
        con1.Close()
        TextBox3.Text = contador
    End Sub
    Private Sub Ult_carga_medidores()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select F_Proceso from ULT_PROCESOS WHERE Proceso = 1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            TextBox10.Text = Dusrs.GetDateTime(0).ToString
        End If
        cnn1.Close()
    End Sub
    Private Sub Ult_actualizacion_polizas()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select F_Proceso from ULT_PROCESOS WHERE Proceso = 2", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            TextBox11.Text = Dusrs.GetDateTime(0).ToString
        End If
        cnn1.Close()
    End Sub
    Private Sub Cant_Peticiones_pendientes()
        'lee el ultimo numero de remito
        Dim contador As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select N_OC_105 From T_C_OC_105 where ESTA_105=3 AND TIPO_OC_105=2", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador += 1
        End While
        con1.Close()
        TextBox7.Text = contador
    End Sub
    Private Sub Cant_OC_pendientes()
        'lee el ultimo numero de remito
        Dim contador As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select N_OC_105 From T_C_OC_105 where ESTA_105=3 AND TIPO_OC_105=1", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador += 1
        End While
        con1.Close()
        TextBox9.Text = contador
    End Sub
    Private Sub Cant_medidores()
        'lee el ultimo numero de remito
        Dim contador As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NSERIE_102 From T_MEDI_102 where ESTADO_102=6", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            contador += 1
        End While
        con1.Close()
        TextBox8.Text = contador
    End Sub
    Public Sub llenar_pc()
        Cant_Peticiones_Sin_aprobar()
        Cant_OC_Sin_aprobar()
        Transferencias_SIN_aprobar()
        Medidores_sin_asignar()
        Medidores_sin_registrar()
        Ult_carga_medidores()
        Ult_actualizacion_polizas()
        Cant_Peticiones_pendientes()
        Cant_OC_pendientes()
        Cant_Peticiones_Sin_nfng()
        Cant_medidores()
        llenar_stock_critico()
    End Sub

    Private Sub llenar_stock_critico()
        If _usr.Obt_Almacen = 0 Then
            TextBox1.Text = 0
            Button1.Enabled = False
        Else
            Dim CONT As Integer = 0
            'lee el ultimo numero de remito
            Dim contador As Integer = 0
            Dim con1 As SqlConnection = New SqlConnection(conexion)
            'abro la cadena
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("select C_MATE_108, CANT_108 From T_SCRI_108 where C_DEPO_108=@D1", con1)

            comando1.Parameters.Add(New SqlParameter("D1", _usr.Obt_Almacen))
            'creo un lector
            comando1.ExecuteNonQuery()
            Dim lector1 As SqlDataReader = comando1.ExecuteReader
            While lector1.Read
                If lector1.GetValue(1) > METODOS.Saldo(lector1.GetValue(0), _usr.Obt_Almacen, 1) Then
                    CONT += 1
                End If
            End While
            con1.Close()
            TextBox1.Text = CONT
            If TextBox1.Text <> 0 Then
                Button1.Enabled = True
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        minuto += 1
        If minuto = 300 Then
            llenar_pc()
            minuto = 0
        End If
    End Sub
#End Region


    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim pass As New PPASS001
        pass.ShowDialog()
    End Sub

    Private Sub activar()
        If _usr.Activar_BT("PCOMB001") = True Then
            PCOMB001T.Enabled = True
        Else
            PCOMB001T.Enabled = False
        End If
        If _usr.Activar_BT("PCOMB002") = True Then
            PCOMB002T.Enabled = True
        Else
            PCOMB002T.Enabled = False
        End If
        If _usr.Activar_BT("PCOMB003") = True Then
            PCOMB003T.Enabled = True
        Else
            PCOMB003T.Enabled = False
        End If
        If _usr.Activar_BT("PCOMB004") = True Then
            PCOMB004T.Enabled = True
        Else
            PCOMB004T.Enabled = False
        End If
        If _usr.Activar_BT("PDEPO003") = True Then
            PDEPO003T.Enabled = True
        Else
            PDEPO003T.Enabled = False
        End If
        If _usr.Activar_BT("PCARG001") = True Then
            PCARGA001T.Enabled = True
        Else
            PCARGA001T.Enabled = False
        End If
        If _usr.Activar_BT("PUSER001") = True Then
            Me.PUSER001T.Enabled = True
        Else
            Me.PUSER001T.Enabled = False
        End If
        If _usr.Activar_BT("PPERF001") = True Then
            Me.PPERF001T.Enabled = True
        Else
            Me.PPERF001T.Enabled = False
        End If
        If _usr.Activar_BT("POPER001") = True Then
            POPER001T.Enabled = True
        Else
            POPER001T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA100") = True Then
            PALMA100T.Enabled = True
        Else
            PALMA100T.Enabled = False
        End If
        If _usr.Activar_BT("PPROV001") = True Then
            PPROV001T.Enabled = True
        Else
            PPROV001T.Enabled = False
        End If
        If _usr.Activar_BT("PCONT001") = True Then
            PCONT001T.Enabled = True
        Else
            PCONT001T.Enabled = False
        End If
        If _usr.Activar_BT("PMATE001") = True Then
            PMATE001T.Enabled = True
        Else
            PMATE001T.Enabled = False
        End If
        If _usr.Activar_BT("PCIUD001") = True Then
            PCIUD001T.Enabled = True
        Else
            PCIUD001T.Enabled = False
        End If
        If _usr.Activar_BT("PCIUD002") = True Then
            PCIUD002T.Enabled = True
        Else
            PCIUD002T.Enabled = False
        End If
        If _usr.Activar_BT("PDEPO001") = True Then
            PDEPO001T.Enabled = True
        Else
            PDEPO001T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO001") = True Then
            PINFO001T.Enabled = True
        Else
            PINFO001T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO002") = True Then
            PINFO002T.Enabled = True
        Else
            PINFO002T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO003") = True Then
            PINFO003T.Enabled = True
        Else
            PINFO003T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO004") = True Then
            PINFO004T.Enabled = True
        Else
            PINFO004T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO005") = True Then
            PINFO005T.Enabled = True
        Else
            PINFO005T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO006") = True Then
            PINFO006T.Enabled = True
        Else
            PINFO006T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO007") = True Then
            PINFO007T.Enabled = True
        Else
            PINFO007T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO008") = True Then
            PINFO008T.Enabled = True
        Else
            PINFO008T.Enabled = False
        End If
        If _usr.Activar_BT("PINFO009") = True Then
            PINFO009T.Enabled = True
        Else
            PINFO009T.Enabled = False
        End If
        If _usr.Activar_BT("PPETI001") = True Then
            PPETI001T.Enabled = True
        Else
            PPETI001T.Enabled = False
        End If
        If _usr.Activar_BT("PPETI002") = True Then
            PPETI002T.Enabled = True
        Else
            PPETI002T.Enabled = False
        End If
        If _usr.Activar_BT("PPETI003") = True Then
            PPETI003T.Enabled = True
        Else
            PPETI003T.Enabled = False
        End If
        If _usr.Activar_BT("PPETI004") = True Then
            PPETI004T.Enabled = True
        Else
            PPETI004T.Enabled = False
        End If
        If _usr.Activar_BT("PCOMP001") = True Then
            PCOMP001T.Enabled = True
        Else
            PCOMP001T.Enabled = False
        End If
        If _usr.Activar_BT("PCOMP002") = True Then
            PCOMP002T.Enabled = True
        Else
            PCOMP002T.Enabled = False
        End If
        If _usr.Activar_BT("PCOMP003") = True Then
            PCOMP003T.Enabled = True
        Else
            PCOMP003T.Enabled = False
        End If
        If _usr.Activar_BT("PCOMP004") = True Then
            PCOMP004T.Enabled = True
        Else
            PCOMP004T.Enabled = False
        End If
        If _usr.Activar_BT("PCONF001") = True Then
            PCONF001T.Enabled = True
        Else
            PCONF001T.Enabled = False
        End If
        If _usr.Activar_BT("PCONF002") = True Then
            PCONF002T.Enabled = True
        Else
            PCONF002T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA002") = True Then
            PALMA002T.Enabled = True
        Else
            PALMA002T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA003") = True Then
            PALMA003T.Enabled = True
        Else
            PALMA003T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA004") = True Then
            PALMA004T.Enabled = True
        Else
            PALMA004T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA005") = True Then
            PALMA005T.Enabled = True
        Else
            PALMA005T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA006") = True Then
            PALMA006T.Enabled = True
        Else
            PALMA006T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA008") = True Then
            PALMA008T.Enabled = True
        Else
            PALMA008T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA009") = True Then
            PALMA009T.Enabled = True
        Else
            PALMA009T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA010") = True Then
            PALMA010T.Enabled = True
        Else
            PALMA010T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA012") = True Then
            PALMA012T.Enabled = True
        Else
            PALMA012T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA013") = True Then
            PALMA013T.Enabled = True
        Else
            PALMA013T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA014") = True Then
            PALMA014T.Enabled = True
        Else
            PALMA014T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA017") = True Then
            PALMA017T.Enabled = True
        Else
            PALMA017T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA018") = True Then
            PALMA018T.Enabled = True
        Else
            PALMA018T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA019") = True Then
            PALMA019T.Enabled = True
        Else
            PALMA019T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA020") = True Then
            PALMA020T.Enabled = True
        Else
            PALMA020T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA021") = True Then
            PALMA021T.Enabled = True
        Else
            PALMA021T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA025") = True Then
            PALMA025T.Enabled = True
        Else
            PALMA025T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA026") = True Then
            PALMA026T.Enabled = True
        Else
            PALMA026T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA031") = True Then
            PALMA031T.Enabled = True
        Else
            PALMA031T.Enabled = False
        End If

        If _usr.Activar_BT("PALMA036") = True Then
            PALMA036T.Enabled = True
        Else
            PALMA036T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA039") = True Then
            PALMA039T.Enabled = True
        Else
            PALMA039T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA041") = True Then
            PALMA041T.Enabled = True
        Else
            PALMA041T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA042") = True Then
            PALMA042T.Enabled = True
        Else
            PALMA042T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA043") = True Then
            PALMA043T.Enabled = True
        Else
            PALMA043T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA044") = True Then
            PALMA044T.Enabled = True
        Else
            PALMA044T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA046") = True Then
            PALMA046T.Enabled = True
        Else
            PALMA046T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA028") = True Then
            PALMA028T.Enabled = True
        Else
            PALMA028T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA029") = True Then
            PALMA029T.Enabled = True
        Else
            PALMA029T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA030") = True Then
            PALMA030T.Enabled = True
        Else
            PALMA030T.Enabled = False
        End If
        If _usr.Activar_BT("PALMA022") = True Then
            REVMEDIDORESToolStripMenuItem.Enabled = True
        Else
            REVMEDIDORESToolStripMenuItem.Enabled = False
        End If
    End Sub






    Private Sub PUSER001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PUSER001T.Click
        Timer1.Stop()
        Dim PANT As New PUSER001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub POPER001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POPER001T.Click
        Timer1.Stop()
        Dim PANT As New POPER001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA100T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA100T.Click
        Timer1.Stop()
        Dim PANT As New PALMA100
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PPROV001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PPROV001T.Click
        Timer1.Stop()
        Dim PANT As New PPROV001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCONT001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCONT001T.Click
        Timer1.Stop()
        Dim PANT As New PCONT001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PMATE001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PMATE001T.Click
        Timer1.Stop()
        Dim PANT As New PMATE001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCIUD001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCIUD001T.Click
        Timer1.Stop()
        Dim PANT As New PCIUD001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCIUD002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCIUD002T.Click
        Timer1.Stop()
        Dim PANT As New PCIUD002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PDEPO001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDEPO001T.Click
        Timer1.Stop()
        Dim PANT As New PDEPO001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCARGA001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCARGA001T.Click
        Timer1.Stop()
        Dim PANT As New PCARG001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA031T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA031T.Click
        Timer1.Stop()
        Dim PANT As New PALMA031
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO001T.Click
        Timer1.Stop()
        Dim PANT As New PINFO001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO006T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO006T.Click
        Timer1.Stop()
        Dim PANT As New PINFO006
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO005T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO005T.Click
        Timer1.Stop()
        Dim PANT As New PINFO005
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO002T.Click
        Timer1.Stop()
        Dim PANT As New PINFO002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO004T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO004T.Click
        Timer1.Stop()
        Dim PANT As New PINFO004
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO007T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO007T.Click
        Timer1.Stop()
        Dim PANT As New PINFO007
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO003T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO003T.Click
        Timer1.Stop()
        Dim PANT As New PINFO003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PDEPO002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDEPO002T.Click
        Timer1.Stop()
        Dim PANT As New PDEPO002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO008T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO008T.Click
        Timer1.Stop()
        Dim PANT As New PINFO008
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PINFO009T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PINFO009T.Click
        Timer1.Stop()
        Dim PANT As New PINFO009
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCOMP001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMP001T.Click
        Timer1.Stop()
        Dim PANT As New PCOMP001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCONF001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCONF001T.Click
        Timer1.Stop()
        Dim PANT As New PCONF001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCONF002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCONF002T.Click
        Timer1.Stop()
        Dim PANT As New PCONF002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PPETI002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PPETI002T.Click
        Timer1.Stop()
        Dim PANT As New PPETI002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCOMP002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMP002T.Click
        Timer1.Stop()
        Dim PANT As New PCOMP002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCOMP003T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMP003T.Click
        Timer1.Stop()
        Dim PANT As New PCOMP003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCOMP004T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMP004T.Click
        Timer1.Stop()
        Dim PANT As New PCOMP004
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PPETI001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PPETI001T.Click
        Timer1.Stop()
        Dim PANT As New PPETI001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PPETI003T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PPETI003T.Click
        Timer1.Stop()
        Dim PANT As New PPETI003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PPETI004T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PPETI004T.Click
        Timer1.Stop()
        Dim PANT As New PPETI004
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA002T.Click
        Timer1.Stop()
        Dim PANT As New PALMA002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA003T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA003T.Click
        Timer1.Stop()
        Dim PANT As New PALMA003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA004T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA004T.Click
        Timer1.Stop()
        Dim PANT As New PALMA004
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA005T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA005T.Click
        Timer1.Stop()
        Dim PANT As New PALMA005
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA026T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA026T.Click
        Timer1.Stop()
        Dim PANT As New PALMA026
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA006T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA006T.Click
        Timer1.Stop()
        Dim PANT As New PALMA006
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PLAMA007T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PLAMA007T.Click
        Timer1.Stop()
        Dim PANT As New PALMA007
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA009T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA009T.Click
        Timer1.Stop()
        Dim PANT As New PALMA009
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA008T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA008T.Click
        Timer1.Stop()
        Dim PANT As New PALMA008
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA010T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA010T.Click
        Timer1.Stop()
        Dim PANT As New PALMA010
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA012T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA012T.Click
        Timer1.Stop()
        Dim PANT As New PALMA012
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA021T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA021T.Click
        Timer1.Stop()
        Dim PANT As New PALMA021
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA025T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA025T.Click
        Timer1.Stop()
        Dim PANT As New PALMA025
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA017T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA017T.Click
        Timer1.Stop()
        Dim PANT As New PALMA017
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA018T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA018T.Click
        Timer1.Stop()
        Dim PANT As New PALMA018
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA019T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA019T.Click
        Timer1.Stop()
        Dim PANT As New PALMA019
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA020T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA020T.Click
        Timer1.Stop()
        Dim PANT As New PALMA020
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA039T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA039T.Click
        Timer1.Stop()
        Dim PANT As New PALMA039
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub





    Private Sub PALMA043T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA043T.Click
        Timer1.Stop()
        Dim PANT As New PALMA043
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA046T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA046T.Click
        Timer1.Stop()
        Dim PANT As New PALMA046
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA041T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA041T.Click
        Timer1.Stop()
        Dim PANT As New PALMA041
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA042T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA042T.Click
        Timer1.Stop()
        Dim PANT As New PALMA042
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA044T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA044T.Click
        Timer1.Stop()
        Dim PANT As New PALMA044
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA036T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA036T.Click
        Timer1.Stop()
        Dim PANT As New PALMA036
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA013T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA013T.Click
        Timer1.Stop()
        Dim PANT As New PALMA013
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA014T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA014T.Click
        Timer1.Stop()
        Dim PANT As New PALMA014
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCONS001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCONS001T.Click
        Timer1.Stop()
        Dim PANT As New PCONS001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCONS002T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCONS002T.Click
        Timer1.Stop()
        Dim PANT As New PCONS002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCONS003T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCONS003T.Click
        Timer1.Stop()
        Dim PANT As New PCONS003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA028T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA028T.Click
        Timer1.Stop()
        Dim PANT As New PALMA028
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA029T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA029T.Click
        Timer1.Stop()
        Dim PANT As New PALMA029
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PALMA030T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PALMA030T.Click
        Timer1.Stop()
        Dim PANT As New PALMA030
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PPERF001T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PPERF001T.Click
        Timer1.Stop()
        Dim PANT As New PPERF001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub GENERARToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMB001T.Click
        Timer1.Stop()
        Dim PANT As New PCOMB001
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CONSULTARTIKETToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMB004T.Click
        Timer1.Stop()
        Dim PANT As New PCOMB004
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CARGARVEHICULOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Timer1.Stop()
        Dim PANT As New PCOMB003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CARGARTICKETToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMB002T.Click
        Timer1.Stop()
        Dim PANT As New PCOMB002
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub COMBUSTIBLEToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles COMBUSTIBLEToolStripMenuItem.Click

    End Sub

    Private Sub STOCKMAXIMOEQUIPOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PDEPO003T.Click
        Timer1.Stop()
        Dim PANT As New PDEPO003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub PCOMB003T_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCOMB003T.Click
        Timer1.Stop()
        Dim PANT As New PCOMB003
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CONTROLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CONTROLToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PCOMB005
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub ENVIARToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ENVIARToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PALMA048
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub REMITOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles REMITOToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PALMA015
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CONSULTAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CONSULTAToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PALMA031BIS
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CARGASTOCKMAXIMOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CARGASTOCKMAXIMOToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PDEPO004
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CONSULTATRANSFERENCIASREALIZADASToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CONSULTATRANSFERENCIASREALIZADASToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PALMA005_BIS
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub CARGAAUTOMATICATICKETToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CARGAAUTOMATICATICKETToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PCOMB006
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub TRANSFERENCIACAJONESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TRANSFERENCIACAJONESToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PALMA036BIS4
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub REVMEDIDORESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REVMEDIDORESToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PALMA022
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub

    Private Sub REMITODEVOLUCIONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REMITODEVOLUCIONToolStripMenuItem.Click
        Timer1.Stop()
        Dim PANT As New PALMA049
        Me.Hide()
        PANT.ShowDialog()
        Me.Show()
        Timer1.Start()
    End Sub
End Class
