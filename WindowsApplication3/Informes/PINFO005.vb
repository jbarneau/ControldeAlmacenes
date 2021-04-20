Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PINFO005
    Dim codigo_almacen As String
    Dim codigo_material As String
    Dim consulta As Integer
    Dim material As String
    Dim tipo As Integer
    Private DS_almacen As New DataSet
    Private DS_herramientas As New DataSet
    Dim suma As Single
    Dim cont As Integer
    Dim mensaje As New Clase_mensaje
    Dim METODOS As New Clas_Almacen
   

    Private Sub PINFO005_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        'CARGO ALMACENES
        'CARGO ALMACEN A COMBOBOX
        llenar_DS_ALMACEN()
        ComboBox1.DataSource = DS_almacen.Tables("M_PERS_003")
        ComboBox1.DisplayMember = "NOMBRE"
        ComboBox1.ValueMember = "NDOC_003"
        ComboBox1.Text = Nothing

        llenar_DS_HERRAMIENTAS()
        ComboBox3.DataSource = DS_herramientas.Tables("M_MATE_002")
        ComboBox3.DisplayMember = "NOMBRE"
        ComboBox3.ValueMember = "CMATE_002"
        ComboBox3.Text = Nothing
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
       
    End Sub

    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        codigo_almacen = ComboBox1.SelectedValue
        codigo_material = ComboBox3.SelectedValue
        suma = 0
        Dim Fdesde As DateTime = DateTimePicker1.Value
        Dim Fhasta As DateTime = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
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

        'TRAIGO LOS CODIGOS DE INDUMENTARIA
        If consulta = 0 Then
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where (F_BAJA_002 IS NULL) and (TIPO_002 = 2) ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                'HASTA ACA TENGO TODOS LOS CODIGOS DE MATERIALES Y DESCRIPCION
                codigo_material = datos4.GetString(0)
                material = datos4.GetString(1)
                suma = 0
                'ETAPA EN COMUN......................................................
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()  'TRAIGO LAS CANTIDADES.....................
                Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (C_MATE_104 =@C3) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(1))

                End While
                cnn2.Close()
                If suma > 0 Then
                    DataGridView1.Rows.Add(codigo_material, material, METODOS.Unidad(codigo_material), Convert.ToString(suma))
                End If
            End While

            cnn4.Close()
        End If

        'CONSULTA 1 - ALMACEN SIN MATERIALES SELECCIONADOS....
        If consulta = 1 Then
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim consulta4 As New SqlClient.SqlCommand("select CMATE_002, DESC_002 from M_MATE_002 where (F_BAJA_002 IS NULL) and (TIPO_002 = 2) ", cnn4)
            consulta4.ExecuteNonQuery()
            Dim datos4 As SqlDataReader = consulta4.ExecuteReader()
            While datos4.Read()
                'HASTA ACA TENGO TODOS LOS CODIGOS DE MATERIALES Y DESCRIPCION
                codigo_material = datos4.GetString(0)
                material = datos4.GetString(1)
                suma = 0
                'ETAPA EN COMUN......................................................
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()  'TRAIGO LAS CANTIDADES CON EL CODIGO DE DEPOSITO SELECCIONADO............
                Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (ALMAR_104<>ALMAE_104) AND (C_MATE_104 =@C3) and (ALMAR_104=@C2) and (F_INFO_104 between @C4 and @C5) ", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
                consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(1))

                End While
                cnn2.Close()
                If suma > 0 Then
                    DataGridView1.Rows.Add(codigo_material, material, METODOS.Unidad(codigo_material), Convert.ToString(suma))
                End If
            End While

            cnn4.Close()
        End If
        'CONSULTA 2 - TODO COMPLETO
        If consulta = 2 Then
            suma = 0
            'ETAPA EN COMUN......................................................
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()  'TRAIGO LAS CANTIDADES CON EL CODIGO DE DEPOSITO SELECCIONADO............
            Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (C_MATE_104 =@C3) and (ALMAR_104=@C2) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
            consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
            consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
            consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
            consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                suma = suma + Convert.ToSingle(datos2.GetValue(1))
            End While
            cnn2.Close()
            If suma > 0 Then
                DataGridView1.Rows.Add(codigo_material, ComboBox3.Text, METODOS.Unidad(codigo_material), Convert.ToString(suma))
            End If

        End If
        'CONSULTA 3 - SIN ALMACEN CON ROPA...
        'TRAIGO LOS CODIGOS DE ALMACEN....
        If consulta = 3 Then
            Dim cnn9 As SqlConnection = New SqlConnection(conexion)
            cnn9.Open()
            Dim traer_C_almacen As New SqlClient.SqlCommand("select NDOC_003 from M_PERS_003 where ALMA_003=1  ", cnn9)
            traer_C_almacen.ExecuteNonQuery()
            Dim C_almacenes As SqlDataReader = traer_C_almacen.ExecuteReader()
            While C_almacenes.Read()
                codigo_almacen = C_almacenes.GetString(0) ' TENGO TODOS LO ALMACENES...
                'MODULO EN COMUN....
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 1) and (C_MATE_104 =@C3) and (ALMAR_104=@C2) and (ALMAR_104<>ALMAE_104) AND (F_INFO_104 between @C4 and @C5) ", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
                consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
                consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    suma = suma + Convert.ToSingle(datos2.GetValue(1))
                End While
                cnn2.Close()

                If suma > 0 Then
                    DataGridView1.Rows.Add(codigo_material, ComboBox3.Text, METODOS.Unidad(codigo_material), Convert.ToString(suma))
                End If
            End While
            cnn9.Close()

        End If
        ComboBox1.Text = Nothing
        ComboBox3.Text = Nothing

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Cursor.Current = Cursors.WaitCursor
        Dim Fdesde As DateTime = DateTimePicker1.Value
        Dim Fhasta As DateTime = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        'EXPORTO, VOY A LA BASE Y TRAIGO TODO....
        Try

            Dim fichero As String = "C:\Archivo\Indumentarias_entregadas_desde_" + DateTimePicker1.Value.ToString("dd-MM-yyyy") + "_hasta_" + DateTimePicker2.Value.ToString("dd-MM-yyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("Nº REMITO;FECHA;COD_MATERIAL;DES_MATERIAL;U;CANTIDAD;ALMACEN")

            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()
            Dim consulta2 As New SqlClient.SqlCommand("select N_REMI_104, C_MATE_104, ALMAR_104, F_INFO_104, CANT_104, DESC_002, NOMB_003, APELL_003 from T_REMI_104  inner join  M_MATE_002 on (T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002) inner join M_PERS_003 on (T_REMI_104.ALMAR_104=M_PERS_003.NDOC_003)  where (ALMAE_104<>ALMAR_104) and (F_INFO_104 between @C4 and @C5) and (M_MATE_002.TIPO_002=2) ", cnn2)
            consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
            consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                a.WriteLine(Convert.ToString(datos2.GetValue(0)) + ";" + Convert.ToString(datos2.GetDateTime(3)) + ";" + datos2.GetString(1) + ";" + datos2.GetString(5) + ";" + METODOS.Unidad(datos2.GetString(1)) + ";" + Convert.ToString(datos2.GetValue(4)) + ";" + datos2.GetString(6) + " " + datos2.GetString(7))
            End While

            cnn2.Close()
            a.Close()
            mensaje.MADVE002(fichero)
        Catch ex As Exception
            MessageBox.Show("ERROR")
        End Try
        Cursor.Current = Cursors.Arrow
    End Sub
    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE (DEPO_003 != 1) AND (F_BAJA_003 is NULL) order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_HERRAMIENTAS()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, (DESC_002) AS NOMBRE FROM M_MATE_002 WHERE TIPO_002 =2 order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_herramientas, "M_MATE_002")
        cnn2.Close()
    End Sub

    
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        B_Agregar_Item.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ComboBox1.Text = Nothing
        ComboBox3.Text = Nothing
        DataGridView1.Rows.Clear()
    End Sub
End Class