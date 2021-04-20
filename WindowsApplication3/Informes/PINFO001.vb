Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PINFO001
    Dim numero_contrato As String
    Dim codigo_almacen As String
    Dim alma As String
    Dim codigo_material As String
    Dim material_seleccionado As String
    Dim consulta As Integer
    Dim material As String
    Dim consumible As Integer
    Dim fecha As String
    Dim suma As Single
    Dim mate As String

    Dim mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen


  

    Private Sub PINFO001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DS_ALMACEN()
        llenar_contrato()
        llenar_DS_HERRAMIENTAS()
       
    End Sub


    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        DataGridView1.Rows.Clear()
        suma = 0
        consulta = 100
        Dim Fdesde As DateTime = DateTimePicker1.Value
        Dim Fhasta As DateTime = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        'BUSCO CODIGO DE ALMACEN 
        If ComboBox1.Text <> Nothing Then
            codigo_almacen = ComboBox1.SelectedValue
        Else
            codigo_almacen = ""
        End If
        If ComboBox2.Text <> Nothing Then
            numero_contrato = ComboBox2.SelectedValue
        Else
            numero_contrato = ""
        End If
        'TRAIGO CODIGO DE MATERIAL
        If ComboBox3.Text <> Nothing Then
            codigo_material = ComboBox3.SelectedValue
        Else
            codigo_material = ""
        End If
        'COMBO 1 ES EL ALMACEN
        'COMBO 2 ES EL CONTRATO
        'COMBO 3 ES EL MATERIAL


        'TIPOS DE FILTROS..............................
        If ComboBox1.Text = Nothing And ComboBox2.Text = Nothing And ComboBox3.Text = Nothing Then
            TodoVacio(Fdesde, Fhasta)
        ElseIf ComboBox1.Text <> Nothing And ComboBox2.Text <> Nothing And ComboBox3.Text <> Nothing Then
            todolleno(Fdesde, Fhasta)
        ElseIf ComboBox1.Text <> Nothing And ComboBox2.Text = Nothing And ComboBox3.Text = Nothing Then
            SoloAlmacen(Fdesde, Fhasta)
        ElseIf ComboBox1.Text = Nothing And ComboBox2.Text = Nothing And ComboBox3.Text <> Nothing Then
            SoloMaterial(Fdesde, Fhasta)
        ElseIf ComboBox1.Text = Nothing And ComboBox2.Text <> Nothing And ComboBox3.Text = Nothing Then
            SoloContrato(Fdesde, Fhasta)
        ElseIf ComboBox1.Text <> Nothing And ComboBox2.Text <> Nothing And ComboBox3.Text = Nothing Then
            contrato_almacen(Fdesde, Fhasta)
        ElseIf ComboBox1.Text = Nothing And ComboBox2.Text <> Nothing And ComboBox3.Text <> Nothing Then
            contrato_material(Fdesde, Fhasta)
        ElseIf ComboBox1.Text <> Nothing And ComboBox2.Text = Nothing And ComboBox3.Text <> Nothing Then
            almacen_material(Fdesde, Fhasta)
        End If
        '..................FIN......................................
        If DataGridView1.RowCount = 0 Then
            mensaje.MERRO011()
        Else
            Button1.Enabled = True
        End If
        '  Catch ex As Exception
        'mensaje.MERRO001()
        'End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Fdesde As Date = DateTimePicker1.Value
        Dim Fhasta As Date = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        'EXPORTO, VOY A LA BASE Y TRAIGO TODO....
        Try

            Dim fichero As String = "C:\Archivo\Materiales_consumidos_desde_" + DateTimePicker1.Value.ToString("dd-MM-yyyy") + "_hasta_" + DateTimePicker2.Value.ToString("dd-MM-yyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("N_REMITO;FECHA;COD_CONTRATO;CONTRATO;COD_MATERIAL;DESC_MATERIAL;U;CANTIDAD;COD_ALMACEN;ALMACEN")

            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()
            If ComboBox1.SelectedValue <> Nothing Then
                Dim consulta2 As New SqlClient.SqlCommand("select N_REMI_104, F_ALTA_104, C_MATE_104, ALMAR_104, CANT_104, DESC_002, (APELL_003+ ' ' +NOMB_003) AS NOMBRE, CONT_104, DESC_004,UNID_002 from T_REMI_104  inner join  M_MATE_002 on (T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002) inner join M_PERS_003 on (T_REMI_104.ALMAR_104=M_PERS_003.NDOC_003)  inner join M_CONT_004 on (T_REMI_104.CONT_104=M_CONT_004.NCONT_004) where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and (M_MATE_002.TIPO_002=1) and (M_PERS_003.NDOC_003 = @C1)", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.Parameters.Add(New SqlParameter("C1", ComboBox1.SelectedValue))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    a.WriteLine(datos2.GetValue(0).ToString + ";" + datos2.GetDateTime(1).ToShortDateString + ";" + datos2.GetValue(7).ToString + ";" + datos2.GetValue(8).ToString + ";" + datos2.GetValue(2).ToString + ";" + datos2.GetValue(5).ToString + ";" + datos2.GetValue(9).ToString + ";" + datos2.GetValue(4).ToString + ";" + datos2.GetValue(3).ToString + ";" + datos2.GetValue(6).ToString)
                End While
            Else
                'Dim consulta2 As New SqlClient.SqlCommand("select N_REMI_104, F_ALTA_104, C_MATE_104, ALMAR_104, CANT_104, DESC_002, (APELL_003+ ' ' +NOMB_003) AS NOMBRE, CONT_104, DESC_004,UNID_002 from T_REMI_104  inner join  M_MATE_002 on (T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002) inner join M_PERS_003 on (T_REMI_104.ALMAR_104=M_PERS_003.NDOC_003)  inner join M_CONT_004 on (T_REMI_104.CONT_104=M_CONT_004.NCONT_004) where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and (M_MATE_002.TIPO_002=1) ", cnn2)
                Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, T_REMI_104.ALMAR_104, T_REMI_104.CANT_104, M_MATE_002.DESC_002, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMBRE, T_REMI_104.CONT_104, M_CONT_004.DESC_004, M_MATE_002.UNID_002 FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAR_104 = M_PERS_003.NDOC_003 LEFT OUTER JOIN M_CONT_004 ON T_REMI_104.CONT_104 = M_CONT_004.NCONT_004 WHERE (T_REMI_104.T_MOV_104 = 2) AND (T_REMI_104.F_INFO_104 BETWEEN @C4 AND @C5) ORDER BY M_MATE_002.DESC_002 DESC", cnn2)
                consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
                consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
                consulta2.ExecuteNonQuery()
                Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
                While datos2.Read()
                    a.WriteLine(datos2.GetValue(0).ToString + ";" + datos2.GetDateTime(1).ToShortDateString + ";" + datos2.GetValue(7).ToString + ";" + datos2.GetValue(8).ToString + ";" + datos2.GetValue(2).ToString + ";" + datos2.GetValue(5).ToString + ";" + datos2.GetValue(9).ToString + ";" + datos2.GetValue(4).ToString + ";" + datos2.GetValue(3).ToString + ";" + datos2.GetValue(6).ToString)
                End While
            End If
            cnn2.Close()
            a.Close()
            mensaje.MADVE002(fichero)
        Catch ex As Exception
            mensaje.MERRO001()
        End Try

    End Sub

    Private Sub llenar_DS_ALMACEN()
        Dim DS_almacen As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE ALMA_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
        ComboBox1.DataSource = DS_almacen.Tables("M_PERS_003")
        ComboBox1.DisplayMember = "NOMBRE"
        ComboBox1.ValueMember = "NDOC_003"
        ComboBox1.Text = Nothing
    End Sub
    Private Sub llenar_contrato()
        Dim ds As New DataSet
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim adaptadaor As New SqlDataAdapter("select NCONT_004, DESC_004 from M_CONT_004 where F_BAJA_004 IS NULL", cnn1)
        adaptadaor.Fill(ds, "M_CONT_004")
        cnn1.Close()
        ComboBox2.DataSource = Nothing
        ComboBox2.DataSource = ds.Tables("M_CONT_004")
        ComboBox2.DisplayMember = "DESC_004"
        ComboBox2.ValueMember = "NCONT_004"
        ComboBox2.Text = Nothing
    End Sub
    Private Sub llenar_DS_HERRAMIENTAS()
        Dim DS_herramientas As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, (DESC_002) AS NOMBRE FROM M_MATE_002 WHERE TIPO_002 = 1 AND F_BAJA_002 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_herramientas, "M_MATE_002")
        cnn2.Close()
        ComboBox3.DataSource = DS_herramientas.Tables("M_MATE_002")
        ComboBox3.DisplayMember = "NOMBRE"
        ComboBox3.ValueMember = "CMATE_002"
        ComboBox3.Text = Nothing
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        BORRAR()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Button1.Enabled = False
    End Sub
    Private Sub BORRAR()
        ComboBox1.Text = Nothing
        ComboBox2.Text = Nothing
        ComboBox3.Text = Nothing
        DataGridView1.Rows.Clear()
        Button1.Enabled = False
    End Sub
    Private Sub TodoVacio(ByVal fdesde As Date, ByVal fhasta As Date)
         Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()
    End Sub
    Public Sub todolleno(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and C_MATE_104 = @C6 AND CONT_104 = @C7 AND ALMAR_104 = @C8 GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.Parameters.Add(New SqlParameter("C6", codigo_material))
        consulta2.Parameters.Add(New SqlParameter("C7", numero_contrato))
        consulta2.Parameters.Add(New SqlParameter("C8", codigo_almacen))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()

    End Sub
    Public Sub SoloContrato(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and CONT_104 = @C7 GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.Parameters.Add(New SqlParameter("C7", numero_contrato))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()
    End Sub
    Public Sub SoloAlmacen(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and ALMAR_104 = @C8 GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.Parameters.Add(New SqlParameter("C8", codigo_almacen))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()

    End Sub
    Public Sub SoloMaterial(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and C_MATE_104 = @C6 GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.Parameters.Add(New SqlParameter("C6", codigo_material))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()
    End Sub
    Public Sub contrato_material(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and C_MATE_104 = @C6 AND CONT_104 = @C7 GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.Parameters.Add(New SqlParameter("C6", codigo_material))
        consulta2.Parameters.Add(New SqlParameter("C7", numero_contrato))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()
    End Sub
    Public Sub contrato_almacen(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) AND CONT_104 = @C7 AND ALMAR_104 = @C8 GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.Parameters.Add(New SqlParameter("C7", numero_contrato))
        consulta2.Parameters.Add(New SqlParameter("C8", codigo_almacen))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()

    End Sub
    Public Sub almacen_material(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 2) and (F_INFO_104 between @C4 and @C5) and C_MATE_104 = @C6 AND ALMAR_104 = @C8 GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.Parameters.Add(New SqlParameter("C6", codigo_material))
        consulta2.Parameters.Add(New SqlParameter("C8", codigo_almacen))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), metodos.detalle_material(datos2.GetValue(0)), metodos.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub
End Class