Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PDEPO002
    Dim COD_MATE As String
    Dim codigo_deposito As Integer
    Dim material As String
    Dim mensaje As New Clase_mensaje

    Private Sub PDEPO002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        'LLENO COMBOBOX CON LOS DEPOSITOS CARGADOS
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim consulta1 As New SqlClient.SqlCommand("select NOMB_003 from M_PERS_003 where DEPO_003 = 1 and F_BAJA_003 is NULL", cnn1)
        consulta1.ExecuteNonQuery()
        Dim datos As SqlDataReader = consulta1.ExecuteReader()
        While datos.Read()
            ComboBox1.Items.Add(datos.GetString(0))
        End While
        cnn1.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        DataGridView2.Rows.Clear()
        'BUSCO CODIGO DE DEPOSITO
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select NDOC_003 from M_PERS_003 where (DEPO_003 = 1) and (F_BAJA_003 is NULL) and (NOMB_003=@C1)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C1", ComboBox1.Text))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        If datos2.Read() Then
            codigo_deposito = datos2.GetString(0)
        End If
        cnn2.Close()


        'LLENO EL DATAGRIDVIEW
        Dim cnn3 As SqlConnection = New SqlConnection(conexion)
        cnn3.Open()
        Dim consulta3 As New SqlClient.SqlCommand("select C_MATE_108, CANT_108 from T_SCRI_108 where C_DEPO_108 = @C1", cnn3)
        consulta3.Parameters.Add(New SqlParameter("C1", codigo_deposito))
        consulta3.ExecuteNonQuery()
        Dim datos3 As SqlDataReader = consulta3.ExecuteReader()
        While datos3.Read()

            COD_MATE = datos3.GetValue(0)
            'TRAIGO DESCRIPCION DE MATERIAL
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select DESC_002 from M_MATE_002 where CMATE_002 = @COD_MATE", cnn4)
            consulta4.Parameters.Add(New SqlParameter("COD_MATE", COD_MATE))
            consulta4.ExecuteNonQuery()
            Dim datos As SqlDataReader = consulta4.ExecuteReader()
            While datos.Read()
                material = datos.GetString(0)
            End While
            cnn4.Close()
            'LLENO EL DATAGRIDVIEW

            DataGridView2.Rows.Add(datos3.GetString(0), material, Convert.ToString(datos3.GetValue(1)))

            'FIN LLENADO..........................................

        End While
        cnn3.Close()

        If DataGridView2.RowCount = 0 Then
            mensaje.MERRO011()
        Else
            Button3.Enabled = True
        End If
    End Sub

  

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim fecha As DateTime
        fecha = DateTime.Now

        If DataGridView2.RowCount > 0 Then

            If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
                My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
            End If
            'EXPORTO

            Dim fichero As String = "C:\Archivo\Stock_critico_ingresado_el_" + fecha.ToString("ddMMyyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("CODIGO MATERIAL;DESCRIPCION;CANTIDAD")
            'RECORRO DATAGRIDVIEW
            Dim fila As Integer = DataGridView2.RowCount
            For i As Integer = 0 To (fila - 1)
                'AGREGO CADA LINEA
                a.WriteLine(Convert.ToString(DataGridView2.Item(0, i).Value) + ";" + Convert.ToString(DataGridView2.Item(1, i).Value) + ";" + Convert.ToString(DataGridView2.Item(2, i).Value))
            Next
            a.Close()
            mensaje.MADVE002(fichero)
        End If
    End Sub

  
End Class