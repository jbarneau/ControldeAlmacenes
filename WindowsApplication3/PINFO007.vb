Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PINFO007
    Dim codigo_contrato As String
    Dim codigo_material As String
    Dim consulta As Integer
    Dim material As String
    Dim tipo As Integer
    Private DS_almacen As New DataSet
    Private DS_herramientas As New DataSet
    Dim suma As Single
    Dim cont As Integer
    Dim depositos As String
    Dim cargar As Integer
    Dim mensaje As New Clase_mensaje
   

    Private Sub PINFO007_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString

        'CARGO ALMACEN A COMBOBOX
        llenar_DS_CONTRATO()

        ComboBox1.DataSource = DS_almacen.Tables("M_CONT_004")
        ComboBox1.DisplayMember = "NOMBRE"
        ComboBox1.ValueMember = "NCONT_004"
        ComboBox1.Text = Nothing
        'CARGO LAS HERRAMIENTAS
        llenar_DS_HERRAMIENTAS()
        ComboBox3.DataSource = DS_herramientas.Tables("M_MATE_002")
        ComboBox3.DisplayMember = "NOMBRE"
        ComboBox3.ValueMember = "CMATE_002"
        ComboBox3.Text = Nothing

    End Sub


    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click

        If ComboBox1.Text <> Nothing Then
            depositos = ComboBox1.Text
        Else
            depositos = "TODOS"
        End If
        codigo_contrato = ComboBox1.SelectedValue
        codigo_material = ComboBox3.SelectedValue
        suma = 0

        'PUEDO TENER 4 ESTADOS..NADA MAS....
        If ComboBox1.Text = Nothing And ComboBox3.Text = Nothing Then 'TODO VACIO
            consulta = 0
        End If
        If ComboBox1.Text <> Nothing And ComboBox3.Text = Nothing Then
            consulta = 1
        End If
        If ComboBox1.Text <> Nothing And ComboBox3.Text <> Nothing Then 'TODO LLENO
            consulta = 2
        End If
        If ComboBox1.Text = Nothing And ComboBox3.Text <> Nothing Then
            consulta = 3
        End If

        'MULTIFILTROS...............
        'CON TODO VACIO.............

        'TRAIGO TODOS LOS CODIGOS DE MATERIALES
        If consulta = 0 Then

            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where F_BAJA_002 IS NULL ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                'HASTA ACA TENGO TODOS LOS CODIGOS DE MATERIALES Y DESCRIPCION
                codigo_material = datos4.GetString(0)
                material = datos4.GetString(1)
                suma = 0
                cargar = 0
                'ETAPA EN COMUN......................................................
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()  'TRAIGO LAS CANTIDADES.....................
                Dim consulta2 As New SqlClient.SqlCommand("select CANT_107 from T_SCONT_107 where C_MATE_107=@C1", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C1", codigo_material))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(0))
                    cargar = 1
                End While

                cnn2.Close()
                If cargar = 1 Then
                    DataGridView1.Rows.Add(codigo_material, material, Convert.ToString(suma))
                End If


            End While
            cnn4.Close()
        End If

        'CONSULTA 1 - CONTRATO SIN MATERIALES SELECCIONADOS....
        If consulta = 1 Then
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where F_BAJA_002 IS NULL ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                'HASTA ACA TENGO TODOS LOS CODIGOS DE MATERIALES Y DESCRIPCION
                codigo_material = datos4.GetString(0)
                material = datos4.GetString(1)
                suma = 0
                cargar = 0
                'ETAPA EN COMUN......................................................
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()  'TRAIGO LAS CANTIDADES CON EL CODIGO DE DEPOSITO SELECCIONADO............
                Dim consulta2 As New SqlClient.SqlCommand("select CANT_107 from T_SCONT_107 where (C_MATE_107=@C1) and (CONT_107=@C2)", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C1", codigo_material))
                consulta2.Parameters.Add(New SqlParameter("C2", codigo_contrato))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(0))
                    cargar = 1
                End While
                If cargar = 1 Then
                    DataGridView1.Rows.Add(codigo_material, material, Convert.ToString(suma))
                End If
                cnn2.Close()

            End While

            cnn4.Close()
        End If


        'CONSULTA 2 - TODO COMPLETO
        If consulta = 2 Then
            suma = 0
            'ETAPA EN COMUN......................................................
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()  'TRAIGO LAS CANTIDADES CON EL CODIGO DE DEPOSITO SELECCIONADO............
            Dim consulta2 As New SqlClient.SqlCommand("select CANT_107 from T_SCONT_107 where C_MATE_107=@C1 and CONT_107=@C2", cnn2)
            consulta2.Parameters.Add(New SqlParameter("C1", codigo_material))
            consulta2.Parameters.Add(New SqlParameter("C2", codigo_contrato))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                suma = suma + Convert.ToSingle(datos2.GetValue(0))
            End While
            cnn2.Close()

            DataGridView1.Rows.Add(codigo_material, ComboBox3.Text, Convert.ToString(suma))


        End If
        'CONSULTA 3 - SIN ALMACEN CON MATERIALES SELECCIONADOS...
        'TRAIGO LOS CODIGOS DE CONTRATOS....

        If consulta = 3 Then
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select NCONT_004 from M_CONT_004 where F_BAJA_004 IS NULL ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                codigo_contrato = datos4.GetString(0)

                'MODULO EN COMUN....
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                Dim consulta2 As New SqlClient.SqlCommand("select CANT_107 from T_SCONT_107 where C_MATE_107=@C1 and CONT_107=@C2", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C1", codigo_material))
                consulta2.Parameters.Add(New SqlParameter("C2", codigo_contrato))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(0))
                End While
                cnn2.Close()

            End While
            DataGridView1.Rows.Add(codigo_material, ComboBox3.Text, Convert.ToString(suma))
        End If
        
        If DataGridView1.RowCount > 0 Then
            Button1.Enabled = True
        Else
            MessageBox.Show("No se encontraron registros")
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'EXPORTO A EXCEL................
        If DataGridView1.RowCount > 0 Then

            If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
                My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
            End If
            'EXPORTO, VOY A LA BASE Y TRAIGO TODO....
            Try

                Dim fichero As String = "C:\Archivo\Stock_por_contrato_al_" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"
                Dim a As New System.IO.StreamWriter(fichero)
                a.WriteLine("CONTRATO;COD_MATERIAL;DESC_MATERIAL;CANTIDAD")
                For i = 0 To DataGridView1.RowCount - 1
                    a.WriteLine(depositos + ";" + Convert.ToString(DataGridView1.Item(0, i).Value) + ";" + Convert.ToString(DataGridView1.Item(1, i).Value) + ";" + Convert.ToString(DataGridView1.Item(2, i).Value))
                Next
                a.Close()
                mensaje.MADVE002(fichero)
            Catch ex As Exception
                MessageBox.Show("ERROR")
            End Try

        End If

        'FIN............................
    End Sub
    Private Sub llenar_DS_CONTRATO()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NCONT_004, (DESC_004) AS NOMBRE FROM M_CONT_004 WHERE F_BAJA_004 is NULL ", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_CONT_004")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_HERRAMIENTAS()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, (DESC_002) AS NOMBRE FROM M_MATE_002 WHERE F_BAJA_002 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_herramientas, "M_MATE_002")
        cnn2.Close()
    End Sub

   

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ComboBox1.Text = Nothing
        ComboBox3.Text = Nothing
        DataGridView1.Rows.Clear()
    End Sub
End Class