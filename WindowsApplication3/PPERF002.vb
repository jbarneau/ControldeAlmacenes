Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PPERF002
    Dim codigo_para802 As Integer
    Private MENSAJE As New Clase_mensaje

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        'VARIOS PASOS A REALIZAR
        '1- BUSCAR EL ULTIMO NUMERO DE PERFIL, SUMARLE 1 Y AGREGAR EL TEXTO CARGADO
        '2- CARGAR TODOS LOS PROGRAMAS INHABILITADOS A T_PERF_101 Y SUMAR 1 A NACC_101
        '1-
        If TextBox1.Text <> Nothing Then

            'BUSCO SI EXISTE EL NOMBRE CARGADO.....
            Dim cnn5 As SqlConnection = New SqlConnection(conexion)
            cnn5.Open()
            Dim consulta5 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802 = 3) AND (DESC_802=@C1)", cnn5)
            consulta5.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
            consulta5.ExecuteNonQuery()
            Dim PPERF As SqlDataReader = consulta5.ExecuteReader()
            If PPERF.Read() Then
                MENSAJE.MERRO019()
            Else
                'DEBO TRAER EL ULTIMO CPARA_802 DE TVINC_802 3 Y SUMARLE 1
                Dim cnn1 As SqlConnection = New SqlConnection(conexion)
                cnn1.Open()
                Dim consulta1 As New SqlClient.SqlCommand("SELECT MAX (C_PARA_802) FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 3", cnn1)
                consulta1.ExecuteNonQuery()
                Dim codigo_part As SqlDataReader = consulta1.ExecuteReader()
                If (codigo_part.Read()) Then
                    codigo_para802 = codigo_part.GetInt32(0) + 1
                End If
                cnn1.Close()
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                Dim alta As New SqlClient.SqlCommand("INSERT INTO DET_PARAMETRO_802 (C_TABLA_802, C_PARA_802, DESC_802) VALUES( 3, @C1, @C2) ", cnn2)
                alta.Parameters.Add(New SqlParameter("C1", codigo_para802))
                alta.Parameters.Add(New SqlParameter("C2", TextBox1.Text))
                alta.ExecuteNonQuery()
                cnn2.Close()
                '........................................................................................
                '2-
                Dim cnn3 As SqlConnection = New SqlConnection(conexion)
                cnn3.Open()
                Dim consulta3 As New SqlClient.SqlCommand("SELECT PROG_803 FROM P_PROG_803", cnn3)
                consulta3.ExecuteNonQuery()
                Dim codigo_programa As SqlDataReader = consulta3.ExecuteReader()
                While (codigo_programa.Read())
                    Dim cnn4 As SqlConnection = New SqlConnection(conexion)
                    cnn4.Open()
                    Dim alta4 As New SqlClient.SqlCommand("INSERT INTO T_PERF_101 (NACC_101, PROG_101, F_ALTA_101, F_BAJA_101, USRS_101) VALUES( @C1, @C2, @C3, @C3, @C4) ", cnn4)
                    alta4.Parameters.Add(New SqlParameter("C1", codigo_para802))
                    alta4.Parameters.Add(New SqlParameter("C2", codigo_programa.GetString(0)))
                    alta4.Parameters.Add(New SqlParameter("C3", DateTime.Now))
                    alta4.Parameters.Add(New SqlParameter("C4", _usr.Obt_Usr))
                    alta4.ExecuteNonQuery()
                    cnn4.Close()

                End While
                cnn3.Close()
                MENSAJE.MADVE001()
                Me.Close()
            End If
            cnn5.Close()
            End If
    End Sub

    
End Class