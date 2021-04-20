Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PINFO006
    Dim motivo As Integer
    Dim codigo_almacen As String
    Dim codigo_material As String
    Dim consulta As Integer
    Dim material As String
    Dim tipo As Integer
    Private DS_almacen As New DataSet
    Private DS_motivos As New DataSet
    Private DS_herramientas As New DataSet
    Dim suma As Single
    Dim cont As Integer
    Dim mensaje As New Clase_mensaje

    Dim cont_1 As Integer

   

    Private Sub PINFO006_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        'CARGO ALMACEN A COMBOBOX
        llenar_DS_ALMACEN()
        ComboBox1.DataSource = DS_almacen.Tables("M_PERS_003")
        ComboBox1.DisplayMember = "NOMBRE"
        ComboBox1.ValueMember = "NDOC_003"
        ComboBox1.Text = Nothing
        'CARGO LAS HERRAMIENTAS
        llenar_DS_HERRAMIENTAS()
        ComboBox3.DataSource = DS_herramientas.Tables("M_MATE_002")
        ComboBox3.DisplayMember = "NOMBRE"
        ComboBox3.ValueMember = "CMATE_002"
        ComboBox3.Text = Nothing
        'CARGO LOS MOTIVOS...
        llenar_DS_MOTIVOS()
        ComboBox2.DataSource = DS_motivos.Tables("DET_PARAMETRO_802")
        ComboBox2.DisplayMember = "NOMBRE"
        ComboBox2.ValueMember = "C_PARA_802"
        ComboBox2.Text = Nothing


    End Sub

    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Dim Fdesde As DateTime = DateTimePicker1.Value
        Dim Fhasta As DateTime = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        codigo_almacen = ComboBox1.SelectedValue
        codigo_material = ComboBox3.SelectedValue
        motivo = ComboBox2.SelectedValue
        suma = 0
        'TIPOS DE FILTROS..............................
        If ComboBox1.Text <> Nothing And ComboBox2.Text <> Nothing And ComboBox3.Text = Nothing Then
            consulta = 0
        End If

        If ComboBox1.Text <> Nothing And ComboBox2.Text <> Nothing And ComboBox3.Text <> Nothing Then
            consulta = 1
        End If

        If ComboBox1.Text <> Nothing And ComboBox2.Text = Nothing And ComboBox3.Text = Nothing Then
            consulta = 2
        End If

        If ComboBox1.Text = Nothing And ComboBox2.Text = Nothing And ComboBox3.Text = Nothing Then
            consulta = 3
        End If

        If ComboBox1.Text = Nothing And ComboBox2.Text = Nothing And ComboBox3.Text <> Nothing Then
            consulta = 4
        End If
        '...........................TIPOS DE FILTROS.............................
        'BUSCO EN TREMI004 LOS RESULTADOS DE ACUERDO AL TIPO DE CONSULTA

        'CON CODIGO DE MATERIAL...TODOS LOS COMBOBOX LLENOS...............

        Try

            If consulta = 1 Then
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (MOTI_104 = @C1) and (ALMAR_104 = @C2) and (C_MATE_104 =@C3) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C1", motivo))
                consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
                consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(1))
                End While
                If suma > 0 Then
                    DataGridView1.Rows.Add(codigo_material, ComboBox3.Text, Convert.ToString(suma))
                End If

                cnn2.Close()
            End If
        '..............................................................................
        'CON COMBOBOX HERRAMIENTAS VACIO..............................................................
        If consulta = 0 Then

            'TRAIGO LOS CODIGOS DE HERRAMIENTAS
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where (F_BAJA_002 IS NULL) and (TIPO_002 = 3) ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                'HASTA ACA TENGO TODOS LOS CODIGOS DE MATERIALES Y DESCRIPCION
                codigo_material = datos4.GetString(0)
                material = datos4.GetString(1)
                suma = 0
                'SIGO COMO CODIGO 1
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                    Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (MOTI_104 = @C1) and (ALMAR_104 = @C2) and (C_MATE_104 =@C3) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C1", motivo))
                consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
                consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                    consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                    consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(1))

                End While
                If suma > 0 Then
                    DataGridView1.Rows.Add(codigo_material, material, Convert.ToString(suma))
                End If
            End While

            cnn4.Close()

        End If
        'CON COMBOBOX MATERIALES VACIO Y MOTIVOS VACIO..........................
        'FILTRO POR SOLO ALMACEN
        If consulta = 2 Then

            'TRAIGO LOS CODIGOS DE LAS HERRAMIENTAS
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where (F_BAJA_002 IS NULL) and (TIPO_002 = 3) ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                'HASTA ACA TENGO TODOS LOS CODIGOS DE MATERIALES Y DESCRIPCION
                codigo_material = datos4.GetString(0)
                material = datos4.GetString(1)
                suma = 0
                'SIGO COMO CODIGO 1

                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                    Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (ALMAR_104 = @C2) and (C_MATE_104 =@C3) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
                consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                    consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                    consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(1))

                End While
                If suma > 0 Then
                    DataGridView1.Rows.Add(codigo_material, material, Convert.ToString(suma))
                End If
            End While

            cnn4.Close()


        End If
        If consulta = 3 Then
            'SIN FILTROS...........
            'TRAIGO LOS CODIGOS DE HERRAMIENTAS
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where (F_BAJA_002 IS NULL) and (TIPO_002 = 3) ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                'HASTA ACA TENGO TODOS LOS CODIGOS DE MATERIALES Y DESCRIPCION
                codigo_material = datos4.GetString(0)
                material = datos4.GetString(1)
                suma = 0
                    'SIGO COMO CODIGO 1
                    Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                    Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (C_MATE_104 =@C3) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
                    consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                    consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                    consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                    consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                        suma = suma + Convert.ToSingle(datos2.GetValue(1))
                    End While
                If suma > 0 Then
                    DataGridView1.Rows.Add(codigo_material, material, Convert.ToString(suma))
                End If
                End While
                cnn4.Close()
        End If

        If consulta = 4 Then
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()
                Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (C_MATE_104 =@C3) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
            consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                suma = suma + Convert.ToSingle(datos2.GetValue(1))
            End While
            If suma > 0 Then
                DataGridView1.Rows.Add(codigo_material, Convert.ToString(suma))
            End If
            cnn2.Close()
        End If
        Catch ex As Exception
            mensaje.MERRO011()
        End Try
        'BORRO......................
        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing
        ComboBox3.Text = Nothing
        If DataGridView1.RowCount = 0 Then
            mensaje.MERRO011()
        Else
            Button1.Enabled = True
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Fdesde As DateTime = DateTimePicker1.Value
        Dim Fhasta As DateTime = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'EXPORTO, VOY A LA BASE Y TRAIGO TODO....
        Try
            Dim fichero As String = "C:\Archivo\Herramientas_consumidas_almacen_desde_" + DateTimePicker1.Value.ToString("dd-MM-yyyy") + "_hasta_" + DateTimePicker2.Value.ToString("dd-MM-yyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("Nº REMITO;FECHA;COD_MATERIAL;DES_MATERIAL;CANTIDAD;ALMACEN;MOTIVO")
            cnn2.Open()
            'Dim consulta2 As New SqlClient.SqlCommand("select N_REMI_104, C_MATE_104, ALMAR_104, F_INFO_104, CANT_104, DESC_002, NOMB_003, APELL_003 from T_REMI_104  inner join  M_MATE_002 on (T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002) inner join M_PERS_003 on (T_REMI_104.ALMAR_104=M_PERS_003.NDOC_003)   where (T_MOV_104 = 1) and (ALMAE_104<>ALMAR_104) and (F_INFO_104 between @C4 and @C5)and (M_MATE_002.TIPO_002=3) ", cnn2)
            Dim consulta2 As New SqlClient.SqlCommand("SELECT dbo.T_REMI_104.N_REMI_104, dbo.T_REMI_104.C_MATE_104, dbo.T_REMI_104.ALMAR_104, dbo.T_REMI_104.F_INFO_104, dbo.T_REMI_104.CANT_104, dbo.M_MATE_002.DESC_002, dbo.M_PERS_003.NOMB_003, dbo.M_PERS_003.APELL_003, dbo.DET_PARAMETRO_802.DESC_802 AS MOTIVO FROM dbo.T_REMI_104 INNER JOIN dbo.M_MATE_002 ON dbo.T_REMI_104.C_MATE_104 = dbo.M_MATE_002.CMATE_002 INNER JOIN dbo.M_PERS_003 ON dbo.T_REMI_104.ALMAR_104 = dbo.M_PERS_003.NDOC_003 INNER JOIN dbo.DET_PARAMETRO_802 ON dbo.T_REMI_104.MOTI_104 = dbo.DET_PARAMETRO_802.C_PARA_802 WHERE (dbo.T_REMI_104.T_MOV_104 = 1) AND (dbo.T_REMI_104.ALMAE_104 <> dbo.T_REMI_104.ALMAR_104) AND (dbo.T_REMI_104.F_INFO_104 BETWEEN @C4 AND @C5) AND (dbo.M_MATE_002.TIPO_002 = 3) AND (dbo.DET_PARAMETRO_802.C_TABLA_802 = 4)", cnn2)
            consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
            consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                a.WriteLine(Convert.ToString(datos2.GetValue(0)) + ";" + Convert.ToString(datos2.GetDateTime(3)) + ";" + datos2.GetString(1) + ";" + datos2.GetString(5) + ";" + Convert.ToString(datos2.GetValue(4)) + ";" + datos2.GetString(6) + " " + datos2.GetString(7) + ";" + datos2.GetString(8))
            End While
            cnn2.Close()
            a.Close()
            mensaje.MADVE002(fichero)
        Catch ex As Exception
            MessageBox.Show("ERROR")
        End Try

    End Sub

    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE ALMA_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_MOTIVOS()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, (DESC_802) AS NOMBRE FROM DET_PARAMETRO_802 WHERE C_TABLA_802= 4 AND F_BAJA_802 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_motivos, "DET_PARAMETRO_802")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_HERRAMIENTAS()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, (DESC_002) AS NOMBRE FROM M_MATE_002 WHERE TIPO_002 =3 AND F_BAJA_002 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_herramientas, "M_MATE_002")
        cnn2.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing
        ComboBox3.Text = Nothing
        DataGridView1.Rows.Clear()
    End Sub
End Class