Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PPERF001
    Dim nivel_usuario As Integer
    Dim descripcion_programa As String
    Dim codigo_programa803 As String
    Dim check As Integer
    Private mensaje As New Clase_mensaje

    Private Sub PPERF001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        ComboBox1.Items.Clear()
        DataGridView1.Rows.Clear()
        'LLENADO DE NIVEL DE ACCESO
        Dim cnn3 As SqlConnection = New SqlConnection(conexion)
        cnn3.Open()
        Dim consulta3 As New SqlClient.SqlCommand(consulta_users, cnn3)
        consulta3.ExecuteNonQuery()
        Dim nivel3 As SqlDataReader = consulta3.ExecuteReader()
        While (nivel3.Read())
            ComboBox1.Items.Add(nivel3.GetString(0))
        End While

        cnn3.Close()

    End Sub

   

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        'TENGO QUE RECORRER EL DATAGRIDVIEW Y GRABAR 0 O 1...

        If (DataGridView1.Rows.Count > 0) Then

            For i = 0 To DataGridView1.RowCount - 1


                If DataGridView1.Item(2, i).Value = 1 Then
                    'CON CHECKBOX 1, PONGO EN NULL FECHA DE BAJA

                    Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                    cnn2.Open()
                    Dim actualizar_partidos As New SqlClient.SqlCommand("UPDATE T_PERF_101 SET F_BAJA_101 = NULL, F_MODI_101 =@C1, USRS_101 =@C4 where (NACC_101= @C3) and (PROG_101 =@C2) ", cnn2)
                    actualizar_partidos.Parameters.Add(New SqlParameter("C1", DateTime.Now))
                    actualizar_partidos.Parameters.Add(New SqlParameter("C2", DataGridView1.Item(0, i).Value))
                    actualizar_partidos.Parameters.Add(New SqlParameter("C3", nivel_usuario))
                    actualizar_partidos.Parameters.Add(New SqlParameter("C4", _usr.Obt_Usr))
                    actualizar_partidos.ExecuteNonQuery()
                    cnn2.Close()

                End If

                If DataGridView1.Item(2, i).Value = 0 Then
                    'CON CHECKBOX 1, PONGO EN NULL FECHA DE BAJA

                    Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                    cnn2.Open()
                    Dim actualizar_partidos As New SqlClient.SqlCommand("UPDATE T_PERF_101 SET F_BAJA_101 = @C1, F_MODI_101 =@C1, USRS_101 =@C4 where (NACC_101= @C3) and (PROG_101 =@C2) ", cnn2)
                    actualizar_partidos.Parameters.Add(New SqlParameter("C1", DateTime.Now))
                    actualizar_partidos.Parameters.Add(New SqlParameter("C2", DataGridView1.Item(0, i).Value))
                    actualizar_partidos.Parameters.Add(New SqlParameter("C3", nivel_usuario))
                    actualizar_partidos.Parameters.Add(New SqlParameter("C4", _usr.Obt_Usr))
                    actualizar_partidos.ExecuteNonQuery()
                    cnn2.Close()

                End If

            Next
            mensaje.MADVE003()
            CheckBox1.Checked = False
            Button1.Enabled = False
            Button11.Enabled = False
            CheckBox1.Enabled = False
            Call PPERF001_Load(sender, e)
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim PANTALLA As New PPERF002
        PANTALLA.ShowDialog()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Button11.Enabled = True
        Dim activo As Integer
        CheckBox1.Enabled = True
        Button1.Enabled = True

        'BORRO TABLA PARA EVITAR CONFUSIONES...
        DataGridView1.Rows.Clear()
        'CON LA DESCRIPCION TRAIGO EL NIVEL
        Dim cnn6 As SqlConnection = New SqlConnection(conexion)
        cnn6.Open()
        Dim consulta6 As New SqlClient.SqlCommand("select C_PARA_802 from DET_PARAMETRO_802 where (DESC_802 = @NIVEL) and (C_TABLA_802=3)", cnn6)
        consulta6.Parameters.Add(New SqlParameter("NIVEL", ComboBox1.Text))
        consulta6.ExecuteNonQuery()
        Dim nivel6 As SqlDataReader = consulta6.ExecuteReader()
        If (nivel6.Read()) Then
            nivel_usuario = nivel6.GetInt32(0)
        End If

        cnn6.Close()

        'LLENO LA TABLA - TRAIGO CODIGO DE PROGRAMA

        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        Dim consulta4 As New SqlClient.SqlCommand("select PROG_101, F_BAJA_101  from T_PERF_101 where NACC_101 = @PERFIL", cnn4)
        consulta4.Parameters.Add(New SqlParameter("PERFIL", nivel_usuario))
        consulta4.ExecuteNonQuery()
        Dim nivel4 As SqlDataReader = consulta4.ExecuteReader()
        While (nivel4.Read())
            Try
                nivel4.GetDateTime(1)
                activo = 0
            Catch ex As Exception
                activo = 1
            End Try

            'CON EL CODIGO BUSCO DESCRIPCION
            Dim cnn5 As SqlConnection = New SqlConnection(conexion)
            cnn5.Open()
            Dim consulta5 As New SqlClient.SqlCommand("select DESC_803, PROG_803 from P_PROG_803 where PROG_803 = @PROGRAMAS", cnn5)
            consulta5.Parameters.Add(New SqlParameter("PROGRAMAS", nivel4.GetString(0)))
            consulta5.ExecuteNonQuery()
            Dim nivel5 As SqlDataReader = consulta5.ExecuteReader()

            While (nivel5.Read())
                DataGridView1.Rows.Add(nivel5.GetString(1), nivel5.GetString(0), activo)
            End While
            cnn5.Close()

        End While

        cnn4.Close()



    End Sub

    Private Sub ComboBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ComboBox1.MouseClick
        'LLENADO DE NIVEL DE ACCESO
        ComboBox1.Items.Clear()
        Dim cnn3 As SqlConnection = New SqlConnection(conexion)
        cnn3.Open()
        Dim consulta3 As New SqlClient.SqlCommand(consulta_users, cnn3)
        consulta3.ExecuteNonQuery()
        Dim nivel3 As SqlDataReader = consulta3.ExecuteReader()
        While (nivel3.Read())
            ComboBox1.Items.Add(nivel3.GetString(0))
        End While

        cnn3.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text = Nothing Then
            mensaje.MERRO008()
        Else
            Dim res As DialogResult
            res = MessageBox.Show("¿Está seguro que desea dar de baja el Perfil seleccionado?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If res = System.Windows.Forms.DialogResult.Yes Then
                'DOY LA BAJA
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                Dim baja As New SqlClient.SqlCommand("UPDATE DET_PARAMETRO_802 SET F_BAJA_802 = @C1 where (C_TABLA_802 =3)  and (DESC_802 = @C2) ", cnn2)
                baja.Parameters.Add(New SqlParameter("C1", DateTime.Now))
                baja.Parameters.Add(New SqlParameter("C2", ComboBox1.Text))
                baja.ExecuteNonQuery()
                cnn2.Close()
                ComboBox1.Items.Remove(ComboBox1.Text)
                DataGridView1.Rows.Clear()
                mensaje.MADVE003()
            End If
        End If
      
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim PANTALLA As New PPERF003
        PANTALLA.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            For i = 0 To Me.DataGridView1.RowCount - 1
                If Me.DataGridView1.Item(2, i).Value <> 1 Then
                    Me.DataGridView1.Item(2, i).Value = 1
                End If
            Next
        Else

            For j = 0 To Me.DataGridView1.RowCount - 1
                If Me.DataGridView1.Item(2, j).Value <> 0 Then
                    Me.DataGridView1.Item(2, j).Value = 0
                End If


            Next
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        'ME PARO SOBRE LA CELDA Y HABILITO O DESHABILITO
        If (DataGridView1.Rows.Count > 0) Then
            'OBTENGO EL NUMERO DE FILA
            Dim fila As Integer = DataGridView1.CurrentCell.RowIndex()
            'CON EL NUMERO PASO LA DESCRIPCION
            If DataGridView1.Item(2, fila).Value = 1 Then
                check = 0
            Else
                check = 1
            End If
            'AGREGO O BORRO CHECK
            DataGridView1.Rows(fila).Cells(2).Value = check
        End If
    End Sub

End Class