Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Module contrato
    Public numero_contrato As String
End Module
Public Class PCONT001
    Dim aux_carga As Integer
    Private mensaje As New Clase_mensaje
    Dim cont As Integer
    Dim peti As Integer

  

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox3.Text <> Nothing) Then

            Dim NCONT As String
            NCONT = TextBox3.Text
            'ACCESOS: 0 ES ALTA Y 1 ES REACTIVAR
            aux_carga = 1
            'CARGA DE DATOS...
            Try
                Dim cnn1 As SqlConnection = New SqlConnection(conexion)
                cnn1.Open()
                Dim consulta1 As New SqlClient.SqlCommand("select DESC_004, COMI_004, F_ALTA_004, F_BAJA_004, PETI_004 from M_CONT_004 where NCONT_004 = @NCONT", cnn1)
                consulta1.Parameters.Add(New SqlParameter("NCONT", NCONT))
                consulta1.ExecuteNonQuery()
                Dim datos As SqlDataReader = consulta1.ExecuteReader()

                If (datos.Read()) Then

                    TextBox1.Text = datos.GetString(0)
                    TextBox6.Text = datos.GetString(1)
                    TextBox2.Text = Convert.ToString(datos.GetDateTime(2))
                    Try
                        TextBox4.Text = Convert.ToString(datos.GetDateTime(3))
                    Catch ex As Exception
                        TextBox4.Text = Nothing
                    End Try
                    'ANALIZO PETI
                    If datos.GetBoolean(4) = True Then
                        ComboBox4.Text = "SI"
                    Else
                        ComboBox4.Text = "NO"
                    End If
                    '......................................FIN DE CARGA

                    cnn1.Close()
                    If (TextBox2.Text <> Nothing And TextBox4.Text = Nothing) Then
                        'HABILITO LOS BOTONES 
                        Button4.Enabled = True
                        Button3.Enabled = True
                        TextBox3.ReadOnly = True
                        Button1.Enabled = False
                        TextBox1.ReadOnly = False
                        TextBox6.ReadOnly = False
                        ComboBox4.Enabled = True
                        cont = 1
                        aux_carga = 1
                    End If
                    If (TextBox2.Text <> Nothing And TextBox4.Text <> Nothing) Then
                        'HABILITO LOS BOTONES 
                        Button2.Enabled = True
                        aux_carga = 0
                    End If
                Else
                    Dim res As DialogResult
                    res = MessageBox.Show("Contrato Inexistente, ¿Desea cargar nuevo?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If res = System.Windows.Forms.DialogResult.Yes Then

                        'HABILITO LOS BOTONES

                        TextBox3.ReadOnly = True
                        Button2.Enabled = True
                        Button1.Enabled = False
                        TextBox6.ReadOnly = False
                        TextBox1.ReadOnly = False
                        ComboBox4.Enabled = True
                        cont = 1
                    End If
                End If
            Catch ex As Exception

                mensaje.MERRO001()

            End Try
        End If
    End Sub

    Private Sub PCONT001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        numero_contrato = Nothing
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If aux_carga = 1 Then

            If (TextBox3.Text <> Nothing And TextBox1.Text <> Nothing And TextBox6.Text <> Nothing And ComboBox4.Text <> Nothing) Then

                Try
                    'ANALIZO PETI
                    If ComboBox4.Text = "SI" Then
                        peti = 1
                    Else
                        peti = 0
                    End If


                    Dim cnn7 As SqlConnection = New SqlConnection(conexion)
                    cnn7.Open()
                    Dim consulta7 As New SqlClient.SqlCommand("INSERT INTO M_CONT_004 (NCONT_004, DESC_004, COMI_004, F_ALTA_004, USRS_004, PETI_004) VALUES (@C0, @C1, @C2, @C3, @C4,@C5) ", cnn7)
                    consulta7.Parameters.Add(New SqlParameter("C0", TextBox3.Text))
                    consulta7.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
                    consulta7.Parameters.Add(New SqlParameter("C2", TextBox6.Text))
                    consulta7.Parameters.Add(New SqlParameter("C3", DateTime.Now))
                    consulta7.Parameters.Add(New SqlParameter("C4", _usr.Obt_Usr))
                    consulta7.Parameters.Add(New SqlParameter("C5", peti))
                    consulta7.ExecuteNonQuery()
                    cnn7.Close()
                    'GRABO LOS MATERIALES....
                    Dim mate As String
                    For Each mate In materiales_agregar

                        Dim cnn8 As SqlConnection = New SqlConnection(conexion)
                        cnn8.Open()
                        Dim consulta8 As New SqlClient.SqlCommand("INSERT INTO T_SCONT_107 (CONT_107, C_MATE_107, CANT_107) VALUES (@C0,@C1,0) ", cnn8)
                        consulta8.Parameters.Add(New SqlParameter("C0", TextBox3.Text))
                        consulta8.Parameters.Add(New SqlParameter("C1", mate))
                        consulta8.ExecuteNonQuery()
                        cnn8.Close()
                    Next
                    '........................
                    mensaje.MADVE001()

                    Me.Close()

                Catch ex As Exception
                    mensaje.MERRO001()
                End Try

            End If
        End If

        If aux_carga = 0 Then
            'REACTIVAMOS EL CONTRATO
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_CONT_004 SET F_BAJA_004 = NULL, USRS_004=@C2, F_MODI_004=@C3 WHERE NCONT_004 = @C4 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C2", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C3", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C4", TextBox3.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE001()
            borrar()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox3.Text <> Nothing And TextBox1.Text <> Nothing And TextBox6.Text <> Nothing And ComboBox4.Text <> Nothing) Then

            'ANALIZO PETI
            If ComboBox4.Text = "SI" Then
                peti = 1
            Else
                peti = 0
            End If
            Try
                'ACTUALIZO LOS CAMBIOS
                Dim cnn7 As SqlConnection = New SqlConnection(conexion)
                cnn7.Open()
                Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_CONT_004 SET DESC_004 = @C1, COMI_004=@C2, F_MODI_004 = @C5, PETI_004=@C7 WHERE NCONT_004 = @C6 ", cnn7)
                consulta7.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
                consulta7.Parameters.Add(New SqlParameter("C2", TextBox6.Text))
                consulta7.Parameters.Add(New SqlParameter("C5", DateTime.Now))
                consulta7.Parameters.Add(New SqlParameter("C6", TextBox3.Text))
                consulta7.Parameters.Add(New SqlParameter("C7", peti))
                consulta7.ExecuteNonQuery()
                cnn7.Close()
                '.............................................................................
                If cont = 1 Then

                    'ACTUALIZO LOS MATERIALES
                    Dim mate1 As String
                    For Each mate1 In materiales_agregar
                        'CONSULTO SI YA ESTA CARGADO....SI NO LO AGREGO
                        Dim cnn9 As SqlConnection = New SqlConnection(conexion)
                        cnn9.Open()
                        Dim consulta9 As New SqlClient.SqlCommand(" SELECT C_MATE_107 FROM T_SCONT_107 WHERE C_MATE_107 =@C1 AND CONT_107=@C2  ", cnn9)
                        consulta9.Parameters.Add(New SqlParameter("C1", mate1))
                        consulta9.Parameters.Add(New SqlParameter("C2", TextBox3.Text))
                        consulta9.ExecuteNonQuery()
                        Dim datos9 As SqlDataReader = consulta9.ExecuteReader()
                        If datos9.Read() = Nothing Then

                            'SI NO ESTA CARGADO LO INSERTAMOS
                            Dim cnn8 As SqlConnection = New SqlConnection(conexion)
                            cnn8.Open()
                            Dim consulta8 As New SqlClient.SqlCommand(" INSERT INTO T_SCONT_107 (CONT_107, C_MATE_107, CANT_107) VALUES (@C0,@C1,0) ", cnn8)
                            consulta8.Parameters.Add(New SqlParameter("C0", TextBox3.Text))
                            consulta8.Parameters.Add(New SqlParameter("C1", mate1))
                            consulta8.ExecuteNonQuery()
                            cnn8.Close()
                        End If
                        cnn9.Close()
                    Next
                    'AHORA BORRO LOS DESTILDADOS
                    Dim mate2 As String
                    For Each mate2 In materiales_borrar
                        'CONSULTO SI YA ESTA CARGADO....Y SI ESTA LO BORRO
                        Dim cnn10 As SqlConnection = New SqlConnection(conexion)
                        cnn10.Open()
                        Dim consulta10 As New SqlClient.SqlCommand(" SELECT C_MATE_107 FROM T_SCONT_107 WHERE C_MATE_107 =@C1  AND CONT_107 =@C2", cnn10)
                        consulta10.Parameters.Add(New SqlParameter("C1", mate2))
                        consulta10.Parameters.Add(New SqlParameter("C2", TextBox3.Text))
                        consulta10.ExecuteNonQuery()
                        Dim datos10 As SqlDataReader = consulta10.ExecuteReader()
                        If datos10.Read() Then
                            If mate2 = datos10.GetString(0) Then

                                'SI LO ENCONTRO BORRO
                                Dim cnn11 As SqlConnection = New SqlConnection(conexion)
                                cnn11.Open()
                                Dim consulta11 As New SqlClient.SqlCommand(" DELETE FROM T_SCONT_107 WHERE C_MATE_107 =@C1 AND CONT_107=@C2 ", cnn11)
                                consulta11.Parameters.Add(New SqlParameter("C1", mate2))
                                consulta11.Parameters.Add(New SqlParameter("C2", TextBox3.Text))
                                consulta11.ExecuteNonQuery()
                                cnn11.Close()
                            End If
                        End If

                        cnn10.Close()
                    Next
                End If
                mensaje.MADVE001()
                borrar()

            Catch ex As Exception
                mensaje.MERRO001()

            End Try
        Else
            mensaje.MERRO008()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If cont = 1 Then
            numero_contrato = TextBox3.Text
            Dim mate As New PCONT002
            mate.ShowDialog()
        Else
            mensaje.MERRO008()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'ACTUALIZO LOS CAMBIOS

        Dim res As DialogResult
        res = MessageBox.Show("¿Está seguro que desea dar de baja el contrato seleccionado?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If res = System.Windows.Forms.DialogResult.Yes Then
            Try

                Dim cnn7 As SqlConnection = New SqlConnection(conexion)
                cnn7.Open()
                Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_CONT_004 SET F_BAJA_004 = @C1, USRS_004=@C2, F_MODI_004=@C3 WHERE NCONT_004 = @C4 ", cnn7)
                consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
                consulta7.Parameters.Add(New SqlParameter("C2", _usr.Obt_Usr))
                consulta7.Parameters.Add(New SqlParameter("C3", DateTime.Now))
                consulta7.Parameters.Add(New SqlParameter("C4", TextBox3.Text))
                consulta7.ExecuteNonQuery()
                cnn7.Close()
                mensaje.MADVE003()
                borrar()
            Catch ex As Exception
                mensaje.MERRO001()

            End Try
        End If
     
    End Sub
    
    Private Sub borrar()
        TextBox3.Text = Nothing
        TextBox2.Text = Nothing
        TextBox4.Text = Nothing
        TextBox6.Text = Nothing
        TextBox1.Text = Nothing
        TextBox1.ReadOnly = True
        TextBox6.ReadOnly = True
        TextBox3.ReadOnly = False
        TextBox6.ReadOnly = True
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        ComboBox4.Text = Nothing
        ComboBox4.Enabled = False

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        borrar()
    End Sub
End Class