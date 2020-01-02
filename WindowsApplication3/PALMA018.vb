Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Public Class PALMA018
    Private DT_Medidores As DataTable
    Private metodos As New Clas_Almacen
    Private mensaje As New Clase_mensaje
    Private medidor As New Clas_Medidor
    Private Sub PALMA018_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        Ult_actualizacion()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim pantalla As New OpenFileDialog
        pantalla.DereferenceLinks = True
        pantalla.Filter = "csv files (*.csv)|*.csv"
        pantalla.FilterIndex = 1
        pantalla.RestoreDirectory = False
        pantalla.Multiselect = True
        pantalla.ShowDialog()
        If Windows.Forms.DialogResult.OK Then
            TextBox1.Text = pantalla.FileName
        End If
        If rbcolabora.Checked Or rbsgc.Checked Then
            rbcolabora.Checked = False
            rbsgc.Checked = False
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Cursor.Current = Cursors.WaitCursor
            If rbsgc.Checked = True Then
                'DT_Medidores = metodos.ConstruirDatatable(",", TextBox1.Text)
                DT_Medidores = metodos.ConstruirDatatableValdes(",", TextBox1.Text)
                LLENAR_DW2()
                If Me.DataGridView2.Rows.Count = 0 Then
                    mensaje.MERRO011()
                Else
                    Button4.Enabled = True
                End If
            ElseIf rbcolabora.Checked = True Then
                DT_Medidores = metodos.ConstruirDatatable(",", TextBox1.Text)
                LLENAR_DW1()
                If Me.DataGridView2.Rows.Count = 0 Then
                    mensaje.MERRO011()
                Else
                    Button4.Enabled = True
                End If
            End If
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub Ult_actualizacion()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select F_Proceso from ULT_PROCESOS WHERE Proceso = 2", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            Label6.Text = "Ultima actualzación: " + Dusrs.GetDateTime(0).ToShortDateString
        End If
        cnn1.Close()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Button3.Enabled = True
    End Sub
    Private Sub LLENAR_DW1()
        Me.DataGridView2.Rows.Clear()
        Dim NMEDIDOR As String
        Dim POLIZA As String
        Dim F_UTIL As String = ""
        Dim ALMACEN As String = ""
        For I = 0 To DT_Medidores.Rows.Count - 1

            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Value = 0
            ProgressBar1.Maximum = DT_Medidores.Rows.Count
            'DT_Medidores.Rows(I).Item(69) para valdes o (15) colabora
            If DT_Medidores.Rows(I).Item(17) <> Nothing Then
                If medidor.Exite_Medi(DT_Medidores.Rows(I).Item(17)) = True Then
                    If DT_Medidores.Rows(I).Item(2) <> Nothing Then
                        NMEDIDOR = DT_Medidores.Rows(I).Item(17)
                        'DT_Medidores.Rows(I).Item(3) para valdes o (2) colabora
                        POLIZA = DT_Medidores.Rows(I).Item(2)
                        DATOS_MEDIDOR(F_UTIL, ALMACEN, NMEDIDOR)
                        Me.DataGridView2.Rows.Add(NMEDIDOR, POLIZA, ALMACEN, F_UTIL)
                    End If
                End If
            End If
        Next
        ProgressBar1.Visible = False
    End Sub

    Private Sub LLENAR_DW2()
        Me.DataGridView2.Rows.Clear()
        Dim NMEDIDOR As String
        Dim POLIZA As String
        Dim F_UTIL As String = ""
        Dim ALMACEN As String = ""
        Dim medcol As String
        For I = 0 To DT_Medidores.Rows.Count - 1

            ProgressBar1.Visible = True
            ProgressBar1.Minimum = 0
            ProgressBar1.Value = 0
            ProgressBar1.Maximum = DT_Medidores.Rows.Count
            medcol = DT_Medidores.Rows(I).Item(69).ToString
            medcol = medcol.Trim(" ")
            'DT_Medidores.Rows(I).Item(69) para valdes o (15) colabora
            If medcol <> Nothing Then
                If medidor.Exite_Medi(medcol) = True Then
                    If DT_Medidores.Rows(I).Item(3) <> Nothing Then
                        NMEDIDOR = medcol
                        'DT_Medidores.Rows(I).Item(3) para valdes o (2) colabora
                        POLIZA = DT_Medidores.Rows(I).Item(3)
                        DATOS_MEDIDOR(F_UTIL, ALMACEN, NMEDIDOR)
                        Me.DataGridView2.Rows.Add(NMEDIDOR, POLIZA, ALMACEN, F_UTIL)
                    End If
                End If
            End If
        Next
        ProgressBar1.Visible = False
    End Sub

    Private Sub DATOS_MEDIDOR(ByRef F_UTIL1 As String, ByRef ALMACEN1 As String, ByVal NMEDIDOR1 As Decimal)
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando1 As New SqlClient.SqlCommand("select F_UTIL_102,CALMA_102 FROM T_MEDI_102 WHERE NSERIE_102 = @D1", cnn4)
        comando1.Parameters.Add(New SqlParameter("D1", NMEDIDOR1))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            If IsDBNull(lector1.GetValue(0)) = False Then
                F_UTIL1 = lector1.GetDateTime(0).ToShortDateString
            Else
                F_UTIL1 = ""
            End If
            ALMACEN1 = metodos.NOMBRE_DEPOSITO(lector1.GetValue(1))
        End If
        cnn4.Close()
    End Sub
    Private Sub GRABAR_POLIZA(ByVal NSERIE As Decimal, ByVal POLIZA As String, ByVal USUARIO As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set POLIZA_102=@D1, F_POLIZA_102=@D2, USER_102=@D3 WHERE NSERIE_102=@E1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", POLIZA))
        comando1.Parameters.Add(New SqlParameter("D2", Date.Now))
        comando1.Parameters.Add(New SqlParameter("D3", USUARIO))
        comando1.Parameters.Add(New SqlParameter("E1", NSERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            For I = 0 To DataGridView2.Rows.Count - 1
                GRABAR_POLIZA(Me.DataGridView2.Item(0, I).Value, Me.DataGridView2.Item(1, I).Value, _usr.Obt_Usr)
            Next
            medidor.ESTADO_5()
            medidor.ESTADO_5_DE7()
            medidor.ESTADO_6()
            modificar_ultimo_actualizacion()
            Ult_actualizacion()
            Button4.Enabled = False
            Button3.Enabled = False
            Me.DataGridView2.Rows.Clear()
            TextBox1.Text = Nothing
            mensaje.MADVE001()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub modificar_ultimo_actualizacion()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update ULT_PROCESOS set F_Proceso = @D1, usuario = @D2 WHERE Proceso = 2", con1)
        comando1.Parameters.Add(New SqlParameter("D1", Date.Now))
        comando1.Parameters.Add(New SqlParameter("D2", _usr.Obt_Usr))
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
End Class