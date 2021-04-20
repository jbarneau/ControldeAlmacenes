Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class PUSER001
    
    Dim almacen As String

    Dim nivel_usuario As Integer
    Dim SEND As Integer
    Private Un_usuario As New Class_UnUsuario
    Private mensaje As New Clase_mensaje


   

    Private Sub PUSER001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        cbDepositoSN.Items.Add("SI")
        cbDepositoSN.Items.Add("NO")
        ComboBox1.Items.Add("SI")
        ComboBox1.Items.Add("NO")
        llenar_DEPOSITO()
        llenar_niveles()
    End Sub
#Region "###### FUNCIONES #######"

    Private Sub llenar_unUsuario()
        'lleno los campos
        txtNombre.Text = Un_usuario.nombre
        txtApellido.Text = Un_usuario.apellido
        TxtUsr.Text = Un_usuario.id
        txtPass.Text = Un_usuario.clave
        txtEmail.Text = Un_usuario.email
        txtAlta.Text = Un_usuario.alta
        If Un_usuario.baja = Nothing Then
            TXTbaja.Text = Nothing
            BTbja.Enabled = True
        Else
            TXTbaja.Text = Un_usuario.baja
            BTalta.Text = "REACTIVAR"
            BTalta.Enabled = True
        End If
        BTmodif.Enabled = True
        'activo los textxbox
        cbDepositoSN.Enabled = True
        If Un_usuario.almacen = 0 Then
            cbDepositoSN.Text = "NO"
        Else
            CBdeposito.SelectedValue = Un_usuario.almacen
            cbDepositoSN.Text = "SI"
            CBdeposito.Enabled = True
        End If
        ComboBox1.Enabled = True
        If Un_usuario.send = 1 Then
            ComboBox1.Text = "SI"
        Else
            ComboBox1.Text = "NO"
        End If
        CBnivel.SelectedValue = Un_usuario.nivel
        CBnivel.Enabled = True
        txtNombre.ReadOnly = False
        txtApellido.ReadOnly = False
        TxtUsr.ReadOnly = False
        txtPass.ReadOnly = False
        txtEmail.ReadOnly = False


    End Sub
    Private Sub modificar()
        Try
            'ACTUALIZO LOS CAMBIOS
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_USRS_001 SET NOMB_001 = @C1, APELL_001=@C2 , ISUR_001= @C3, PASS_001=@C4, CALM_001= @C5, CACC_001 = @C6, F_MODI_001 = @C7, EMAIL_001 =@C9, USER_001=@C10, SEND_001=@C11  WHERE NDOC_001 = @C8 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", txtNombre.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", txtApellido.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", TxtUsr.Text))
            consulta7.Parameters.Add(New SqlParameter("C4", txtPass.Text))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_almacen))
            consulta7.Parameters.Add(New SqlParameter("C6", nivel_usuario))
            consulta7.Parameters.Add(New SqlParameter("C7", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C9", txtEmail.Text))
            consulta7.Parameters.Add(New SqlParameter("C10", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C11", SEND))
            consulta7.Parameters.Add(New SqlParameter("C8", TextBox1.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub bajaUser()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_USRS_001 SET F_BAJA_001 = @C1, USER_001=@C9  WHERE NDOC_001 = @C8 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C9", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C8", TextBox1.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub Reactivar()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_USRS_001 SET F_BAJA_001 = NULL , F_MODI_001 = @C1, USER_001=@C2 WHERE NDOC_001 = @D1 ", cnn7)

            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C2", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("D1", TextBox1.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub Grabar_nuevo()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("INSERT INTO M_USRS_001 (NDOC_001 , NOMB_001, APELL_001, ISUR_001, PASS_001, CALM_001, CACC_001, F_ALTA_001, USER_001, EMAIL_001,SEND_001) VALUES (@C0, @C1, @C2, @C3, @C4, @C5, @C6, @C7, @C8, @C9,@C10) ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C0", TextBox1.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", txtNombre.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", txtApellido.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", TxtUsr.Text))
            consulta7.Parameters.Add(New SqlParameter("C4", txtPass.Text))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_almacen))
            consulta7.Parameters.Add(New SqlParameter("C6", nivel_usuario))
            consulta7.Parameters.Add(New SqlParameter("C7", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C8", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C9", txtEmail.Text))
            consulta7.Parameters.Add(New SqlParameter("C10", SEND))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
        mensaje.MADVE001()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub borrar()
        Button1.Enabled = False
        BTalta.Text = "Dar de Alta"
        TextBox1.ReadOnly = False
        TextBox1.Text = Nothing
        txtNombre.ReadOnly = True
        txtNombre.Text = Nothing
        txtApellido.ReadOnly = True
        txtApellido.Text = Nothing
        txtPass.ReadOnly = True
        txtPass.Text = Nothing
        TxtUsr.ReadOnly = True
        TxtUsr.Text = Nothing
        txtAlta.ReadOnly = True
        txtAlta.Text = Nothing
        TXTbaja.ReadOnly = True
        TXTbaja.Text = Nothing
        txtEmail.ReadOnly = True
        txtEmail.Text = Nothing
        TextBox1.ReadOnly = False
        cbDepositoSN.Enabled = False
        cbDepositoSN.Text = Nothing
        CBnivel.Enabled = False
        CBnivel.Text = Nothing
        Button1.Enabled = True
        BTbja.Enabled = False
        BTmodif.Enabled = False
        BTalta.Enabled = False
        DataGridView1.Rows.Clear()
        TextBox1.ReadOnly = False
        CBdeposito.Text = Nothing
        CBdeposito.Enabled = False
        TextBox1.Enabled = True
        ComboBox1.Text = Nothing
        ComboBox1.Enabled = False
    End Sub
    Private Function Veri_nombre(ByVal apellido As String, ByVal nombre As String) As Boolean
        Dim respuesta As Boolean = False
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim consulta1 As New SqlClient.SqlCommand("select NDOC_001, NOMB_001, APELL_001 from M_USRS_001 where (NOMB_001=@C1) AND (APELL_001=@C2) ", cnn1)
        consulta1.Parameters.Add(New SqlParameter("C1", nombre))
        consulta1.Parameters.Add(New SqlParameter("C2", apellido))
        consulta1.ExecuteNonQuery()
        Dim datos As SqlDataReader = consulta1.ExecuteReader()
        If datos.Read Then
            'MENSAJE QUE EXISTE
            respuesta = True
        End If
        cnn1.Close()
        Return respuesta
    End Function
    Private Function Veri_user(ByVal user As String) As Boolean
        Dim respuesta As Boolean = False
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim consulta1 As New SqlClient.SqlCommand("select NDOC_001 from M_USRS_001 where (ISUR_001=@C1)", cnn1)
        consulta1.Parameters.Add(New SqlParameter("C1", user))
        consulta1.ExecuteNonQuery()
        Dim datos As SqlDataReader = consulta1.ExecuteReader()
        If datos.Read Then
            'MENSAJE QUE EXISTE
            respuesta = True
        End If
        cnn1.Close()
        Return respuesta
    End Function
    Private Sub llenar_DEPOSITO()
        Dim ds_deposito As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 and F_BAJA_003 IS NULL ORDER BY NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(ds_deposito, "M_PERS_003")
        cnn2.Close()
        CBdeposito.DataSource = ds_deposito.Tables("M_PERS_003")
        CBdeposito.DisplayMember = "NOMB_003"
        CBdeposito.ValueMember = "NDOC_003"
        CBdeposito.Text = Nothing
    End Sub
    Private Sub llenar_niveles()
        Dim ds_deposito As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 3 and F_BAJA_802 IS NULL ORDER BY DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(ds_deposito, "DET_PARAMETRO_802")
        cnn2.Close()
        CBnivel.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        CBnivel.DisplayMember = "DESC_802"
        CBnivel.ValueMember = "C_PARA_802"
        CBnivel.Text = Nothing
    End Sub
    Private Function verificar_cambio_nombre(ByVal nombre As String, ByVal apellido As String) As Boolean
        Dim resp As Boolean = True
        If nombre <> Un_usuario.nombre Or apellido <> Un_usuario.apellido Then
            If Veri_nombre(apellido, nombre) = True Then
                resp = False
            End If
        End If
        Return resp
    End Function
    Private Function Verufucar_cambio_user(ByVal user As String) As Boolean
        Dim resp As Boolean = True
        If user <> Un_usuario.id Then
            If Veri_user(user) = True Then
                resp = False
            End If
        End If
        Return resp
    End Function

#End Region



#Region "#### botones ####"
    'boton de buscar
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text.Length = 8 And IsNumeric(TextBox1.Text) Then
            If Un_usuario.Existe_USR(TextBox1.Text) = True Then
                TextBox1.Enabled = False
                llenar_unUsuario()
            Else
                Dim res As DialogResult
                res = MessageBox.Show("El usuario no existe" + vbCrLf + "¿Desea cargarlo?", "ALTA DE NUEVO USUARIO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If res = System.Windows.Forms.DialogResult.Yes Then
                    TextBox1.Enabled = True
                    BTalta.Enabled = True
                    cbDepositoSN.Enabled = True
                    ComboBox1.Enabled = True
                    CBnivel.Enabled = True
                    txtNombre.ReadOnly = False
                    txtApellido.ReadOnly = False
                    txtNombre.ReadOnly = False
                    TxtUsr.ReadOnly = False
                    txtPass.ReadOnly = False
                    txtEmail.ReadOnly = False
                End If

            End If
        Else
            mensaje.MERRO014()
        End If
    End Sub
    'boton de borrar
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        TextBox1.Enabled = True
        borrar()

    End Sub
    Private Sub BTAlta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTalta.Click
        'ME FIJO SI ESTA LLENO LA FECHA DE ALTA
        If txtAlta.Text = Nothing Then
            'CODIGO PARA ALMACENES................

            If (TextBox1.Text <> Nothing And txtNombre.Text <> Nothing And txtApellido.Text <> Nothing And TxtUsr.Text <> Nothing And txtPass.Text <> Nothing And cbDepositoSN.Text <> Nothing And CBnivel.Text <> Nothing And ComboBox1.Text <> Nothing And txtEmail.Text <> Nothing) Then
                If Veri_user(TxtUsr.Text) = False Then
                    If Veri_nombre(txtApellido.Text, txtNombre.Text) = False Then
                        If cbDepositoSN.Text = "SI" Then
                            If CBdeposito.Text <> Nothing Then
                                cod_almacen = CBdeposito.SelectedValue
                            Else
                                cod_almacen = "0"
                            End If

                        Else
                            cod_almacen = "0"
                        End If
                        If ComboBox1.Text = "SI" Then
                            SEND = 1
                        Else
                            SEND = 0
                        End If
                        Grabar_nuevo()

                    Else
                        mensaje.MERRO023()
                    End If
                Else
                    mensaje.MERRO024()
                End If
            Else
                mensaje.MERRO008()
            End If
        Else
            'REACTIVA UN USER DADO DE BAJA
            Reactivar()
        End If
    End Sub



    Private Sub BTmodi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTmodif.Click
        If (TextBox1.Text <> Nothing And txtNombre.Text <> Nothing And txtApellido.Text <> Nothing And TxtUsr.Text <> Nothing And txtPass.Text <> Nothing And cbDepositoSN.Text <> Nothing And CBnivel.Text <> Nothing And ComboBox1.Text <> Nothing) Then

            If Verufucar_cambio_user(TxtUsr.Text) = True Then
                If verificar_cambio_nombre(txtNombre.Text, txtApellido.Text) = True Then
                    If cbDepositoSN.Text = "SI" Then
                        If CBdeposito.Text <> Nothing Then
                            cod_almacen = CBdeposito.SelectedValue
                        Else
                            cod_almacen = "0"
                        End If

                    Else
                        cod_almacen = "0"
                    End If
                    If ComboBox1.Text = "SI" Then
                        SEND = 1
                    Else
                        SEND = 0
                    End If
                    modificar()

                Else
                    mensaje.MERRO023()
                End If
            Else
                mensaje.MERRO024()
            End If
        Else
            mensaje.MERRO008()
        End If
       

       
    End Sub

    Private Sub BTbaja_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTbja.Click
        'ACTUALIZO LOS CAMBIOS
        Dim res As DialogResult
        res = MessageBox.Show("¿Está seguro que desea dar de baja el usuario seleccionado?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If res = System.Windows.Forms.DialogResult.Yes Then
            bajaUser()
        End If
    End Sub



#End Region


    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBnivel.SelectedIndexChanged
        If TextBox1.Text <> Nothing Then
            'BORRO TABLA PARA EVITAR CONFUSIONES...
            nivel_usuario = CBnivel.SelectedValue
            DataGridView1.Rows.Clear()
            'LLENO LA TABLA
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("SELECT P_PROG_803.DESC_803 FROM P_PROG_803 INNER JOIN T_PERF_101 ON P_PROG_803.PROG_803 = T_PERF_101.PROG_101 WHERE (T_PERF_101.NACC_101 = @D1) AND (T_PERF_101.F_BAJA_101 IS NULL) ORDER BY P_PROG_803.DESC_803", cnn4)
            consulta4.Parameters.Add(New SqlParameter("D1", nivel_usuario))
            consulta4.ExecuteNonQuery()
            Dim nivel4 As SqlDataReader = consulta4.ExecuteReader()
            While (nivel4.Read())
                DataGridView1.Rows.Add(nivel4.GetString(0))
            End While
            cnn4.Close()
        End If
    End Sub

   

    

  

    

    

   
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDepositoSN.SelectedIndexChanged
        If cbDepositoSN.Text = "NO" Then
            CBdeposito.Enabled = False
            CBdeposito.Text = Nothing
        Else
            CBdeposito.Enabled = True
            If txtAlta.Text <> Nothing Then
                CBdeposito.SelectedValue = Un_usuario.almacen
            End If
        End If
    End Sub

    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        Dim PANTALLA As New PUSER002
        PANTALLA.ShowDialog()
        If PANTALLA.dato = True Then
            TextBox1.Text = PANTALLA.dniobtenido
            Un_usuario.Existe_USR(TextBox1.Text)
            TextBox1.Enabled = False
            llenar_unUsuario()
        End If

    End Sub

    
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class