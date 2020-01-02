Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA028
    Dim codigo_almacen As String
    Dim codigo_material As String
    Dim consulta As Integer
    Dim material As String
    Dim tipo As Integer
    Private DS_almacen As New DataSet
    Private DS_herramientas As New DataSet
    Dim suma As Single
    Dim cont As Integer
    Dim almacen As String
    Dim mensaje As New Clase_mensaje
    Private METODOS As New Clas_Almacen
    Dim estado As Integer = 1
   
   

    Private Sub PALMA028_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_estado()
       llenar_DS_ALMACEN()
        'CARGO LAS HERRAMIENTAS
        llenar_DS_HERRAMIENTAS()
       

    End Sub

    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE (ALMA_003 = 1 or DEPO_003=1) AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        ComboBox1.DataSource = DS_almacen.Tables("M_PERS_003")
        ComboBox1.DisplayMember = "NOMBRE"
        ComboBox1.ValueMember = "NDOC_003"
        ComboBox1.Text = Nothing

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
        ComboBox3.DataSource = DS_herramientas.Tables("M_MATE_002")
        ComboBox3.DisplayMember = "NOMBRE"
        ComboBox3.ValueMember = "CMATE_002"
        ComboBox3.Text = Nothing
    End Sub
    Private Sub llenar_estado()
        'CONECTO LA BASE
        Dim DS_estado As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 10 AND (C_PARA_802 = 1 OR C_PARA_802 = 9) order by C_PARA_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_estado, "DET_PARAMETRO_802")
        cnn2.Close()
        ComboBox2.DataSource = DS_estado.Tables("DET_PARAMETRO_802")
        ComboBox2.DisplayMember = "DESC_802"
        ComboBox2.ValueMember = "C_PARA_802"

    End Sub
   

    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Me.DataGridView1.Rows.Clear()

        If ComboBox1.Text <> Nothing Then
            almacen = ComboBox1.Text
        Else
            almacen = "TODOS"
        End If
        codigo_almacen = ComboBox1.SelectedValue
        codigo_material = ComboBox3.SelectedValue
        estado = ComboBox2.SelectedValue
        suma = 0

        'PUEDO TENER 4 ESTADOS..NADA MAS....
        If ComboBox1.Text = Nothing And ComboBox3.Text = Nothing Then 'TODO VACIO
            total()
        ElseIf ComboBox1.Text <> Nothing And ComboBox3.Text = Nothing Then
            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()  'TRAIGO LAS CANTIDADES CON EL CODIGO DE DEPOSITO SELECCIONADO............
            Dim consulta2 As New SqlClient.SqlCommand("select T_ALMA_103.C_MATE_103, M_MATE_002.DESC_002,M_MATE_002.UNID_002,T_ALMA_103.N_CANT_103 from T_ALMA_103  INNER JOIN M_MATE_002 ON T_ALMA_103.C_MATE_103 = M_MATE_002.CMATE_002 where C_ALMA_103= @C2 AND N_CANT_103 <> 0 AND ESTA_103=@C3", cnn2)
            consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
            consulta2.Parameters.Add(New SqlParameter("C3", estado))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                DataGridView1.Rows.Add(datos2.GetValue(0), datos2.GetValue(1), datos2.GetValue(2), datos2.GetValue(3))
            End While
            cnn2.Close()
        ElseIf ComboBox1.Text <> Nothing And ComboBox3.Text <> Nothing Then 'TODO LLENO
            If METODOS.Saldo(codigo_material, codigo_almacen, estado) <> 0 Then
                DataGridView1.Rows.Add(codigo_material, ComboBox3.Text, METODOS.Unidad(codigo_material), METODOS.Saldo(codigo_material, codigo_almacen, estado))
            End If
        ElseIf ComboBox1.Text = Nothing And ComboBox3.Text <> Nothing Then
            
            un_material(codigo_material, estado)
        End If
        If DataGridView1.RowCount > 0 Then
            Button1.Enabled = True
        Else
            mensaje.MERRO011()
        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'EXPORTO A EXCEL................
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        If DataGridView1.RowCount = 0 Then
            'EXPORTO, VOY A LA BASE Y TRAIGO TODO....
            Try

                Dim fichero As String = "C:\Archivo\Stock_por_almacen_al_" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"
                Dim a As New System.IO.StreamWriter(fichero)
                a.WriteLine("COD_ALMACEN;ALMACEN;COD_MATERIAL;DESC_MATERIAL;UNIDAD;CANTIDAD,ESTADO")

                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()  'TRAIGO LAS CANTIDADES.....................
                Dim consulta2 As New SqlClient.SqlCommand("select C_ALMA_103, C_MATE_103, N_CANT_103, DESC_002, (APELL_003+ ' ' +NOMB_003) AS NOMBRE, DESC_802, UNID_002 from T_ALMA_103 inner join  M_MATE_002 on ( T_ALMA_103.C_MATE_103 = M_MATE_002.CMATE_002) inner join  M_PERS_003 on ( T_ALMA_103.C_ALMA_103 = M_PERS_003.NDOC_003) inner join  DET_PARAMETRO_802 on ( T_ALMA_103.ESTA_103 = DET_PARAMETRO_802.C_PARA_802) WHERE DET_PARAMETRO_802.C_TABLA_802 = 10 ", cnn2)
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    a.WriteLine(datos2.GetString(0) + ";" + datos2.GetString(4) + ";" + datos2.GetString(1) + ";" + datos2.GetString(3) + ";" + datos2.GetString(6) + ";" + Convert.ToString(datos2.GetValue(2)) + ";" + datos2.GetString(5))
                End While
                cnn2.Close()
                a.Close()
                mensaje.MADVE002(fichero)
                Call Button2_Click(sender, e)
            Catch ex As Exception
                mensaje.MERRO001()
            End Try

        ElseIf ComboBox1.Text <> Nothing Then
            Dim fichero As String = "C:\Archivo\Stock_" + ComboBox1.Text + "_" + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("COD_ALMACEN;ALMACEN;COD_MATERIAL;DESC_MATERIAL;UNIDAD;CANTIDAD")
            For i = 0 To DataGridView1.RowCount - 1
                a.WriteLine(ComboBox1.SelectedValue + ";" + ComboBox1.Text + ";" + Me.DataGridView1.Item(0, i).Value + ";" + Me.DataGridView1.Item(1, i).Value.ToString + ";" + Me.DataGridView1.Item(2, i).Value.ToString + ";" + Me.DataGridView1.Item(3, i).Value.ToString)
            Next
            a.Close()
            mensaje.MADVE002(fichero)
            ComboBox1.Text = Nothing
            ComboBox3.Text = Nothing
            DataGridView1.Rows.Clear()


        End If


        'FIN............................
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ComboBox1.Text = Nothing
        ComboBox3.Text = Nothing
        DataGridView1.Rows.Clear()
    End Sub
    Private Sub total()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()  'TRAIGO LAS CANTIDADES.....................
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_103 ,sum(N_CANT_103) from T_ALMA_103 where ESTA_103=@C2 GROUP BY C_MATE_103", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C2", estado))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read()
            If datos2.GetValue(1) <> 0 Then
                DataGridView1.Rows.Add(datos2.GetValue(0), METODOS.detalle_material(datos2.GetValue(0)), METODOS.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
            End If
        End While
        cnn2.Close()
    End Sub
    Private Sub un_material(ByVal material As String, ByVal estado_2 As String)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_103,SUM(N_CANT_103) from T_ALMA_103 where C_MATE_103=@C1 AND ESTA_103= @C2 GROUP BY C_MATE_103", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C1", material))
        consulta2.Parameters.Add(New SqlParameter("C2", estado_2))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        If datos2.Read Then
            If datos2.GetValue(1) <> 0 Then
                DataGridView1.Rows.Add(codigo_material, ComboBox3.Text, METODOS.Unidad(codigo_material), datos2.GetValue(1).ToString)
            End If
        End If
        cnn2.Close()
    End Sub

  
End Class