Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PINFO002
    Private codigo_almacen As String
    Dim codigo_material As String
    Dim consulta As Integer
    Dim material As String
    Dim tipo As Integer


    Dim suma As Single
    Dim cont As Integer
    Dim mensaje As New Clase_mensaje
    Private METODOS As New Clas_Almacen

   

    Private Sub PINFO002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        'CARGO ALMACEN A COMBOBOX
        llenar_DS_ALMACEN()
        'CARGO LAS HERRAMIENTAS
        llenar_DS_HERRAMIENTAS()
        codigo_almacen = ""
        codigo_material = ""
    End Sub


    Private Sub B_Agregar_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Agregar_Item.Click
        Me.DataGridView1.Rows.Clear()
        Dim Fdesde As Date = DateTimePicker1.Value
        Dim Fhasta As Date = DateTimePicker2.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        If cbdeposito.Text <> Nothing Then
            codigo_almacen = cbdeposito.SelectedValue
        End If
        If cbmateriales.Text <> Nothing Then
            codigo_material = cbmateriales.SelectedValue
        End If

        suma = 0

        'PUEDO TENER 4 ESTADOS..NADA MAS....
        If cbdeposito.Text = Nothing And cbmateriales.Text = Nothing Then 'TODO VACIO
            todos_Materiales(Fdesde, Fhasta)
        End If
        If cbdeposito.Text <> Nothing And cbmateriales.Text = Nothing Then
            Todo_un_Deposito(Fdesde, Fhasta)
        End If
        If cbdeposito.Text <> Nothing And cbmateriales.Text <> Nothing Then 'TODO LLENO
            material_deposito(Fdesde, Fhasta)
        End If
        If cbdeposito.Text = Nothing And cbmateriales.Text <> Nothing Then
            un_material(Fdesde, Fhasta, codigo_material)
        End If
        If DataGridView1.RowCount = 0 Then
            mensaje.MERRO011()
        Else
            Button1.Enabled = True
        End If

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
            Dim fichero As String = "C:\Archivo\Materiales_depositos_entregados_desde_" + DateTimePicker1.Value.ToString("dd-MM-yyyy") + "_hasta_" + DateTimePicker2.Value.ToString("dd-MM-yyyy") + ".csv"
            Dim a As New System.IO.StreamWriter(fichero)
            Dim NREMITO As String
            Dim FECHA As String
            Dim CODMATE As String
            Dim DEPOSITO As String
            Dim ALMACEN As String
            Dim CANT As String

            a.WriteLine("Nº REMITO;FECHA;COD_MATERIAL;DESC_MATERIAL;CANTIDAD;COD_DEPOSITO;DEPOSITO;U;COD_ALMACEN;ALMACEN")

            Dim cnn2 As SqlConnection = New SqlConnection(conexion)
            cnn2.Open()
            Dim consulta2 As New SqlClient.SqlCommand("select N_REMI_104, F_ALTA_104, C_MATE_104, CANT_104, ALMAE_104, ALMAR_104 from T_REMI_104 where (T_MOV_104 = 9) AND (ALMAE_104<>ALMAR_104) and (F_ALTA_104 between @C4 and @C5) ", cnn2)
            consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
            consulta2.Parameters.Add(New SqlParameter("C5", Fhasta))
            consulta2.ExecuteNonQuery()
            Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
            While datos2.Read()
                NREMITO = datos2.GetValue(0)
                FECHA = datos2.GetDateTime(1).ToShortDateString
                CODMATE = datos2.GetValue(2)
                CANT = datos2.GetValue(3)
                DEPOSITO = datos2.GetValue(4)
                ALMACEN = datos2.GetValue(5)
                a.WriteLine(NREMITO.ToString + ";" + FECHA + ";" + CODMATE + ";" + METODOS.detalle_material(CODMATE) + ";" + METODOS.Unidad(CODMATE) + ";" + CANT + ";" + DEPOSITO + ";" + METODOS.NOMBRE_DEPOSITO(DEPOSITO) + ";" + ALMACEN + ";" + METODOS.NOMBRE_DEPOSITO(ALMACEN))
            End While
            cnn2.Close()
            a.Close()
            mensaje.MADVE002(fichero)
        Catch ex As Exception
            MessageBox.Show("ERROR")
        End Try
    End Sub
    Private Sub llenar_DS_ALMACEN()
        Dim DS_almacen As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE DEPO_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
        cbdeposito.DataSource = DS_almacen.Tables("M_PERS_003")
        cbdeposito.DisplayMember = "NOMBRE"
        cbdeposito.ValueMember = "NDOC_003"
        cbdeposito.Text = Nothing
    End Sub
    Private Sub llenar_DS_HERRAMIENTAS()
        Dim DS_herramientas As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, (DESC_002) AS NOMBRE FROM M_MATE_002 WHERE F_BAJA_002 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_herramientas, "M_MATE_002")
        cnn2.Close()
        cbmateriales.DataSource = DS_herramientas.Tables("M_MATE_002")
        cbmateriales.DisplayMember = "NOMBRE"
        cbmateriales.ValueMember = "CMATE_002"
        cbmateriales.Text = Nothing
    End Sub



    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbmateriales.SelectedIndexChanged
        B_Agregar_Item.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        cbdeposito.Text = Nothing
        cbmateriales.Text = Nothing
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub ComboBox1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cbdeposito.MouseClick
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub todos_Materiales(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 9) and (ALMAR_104<>ALMAE_104) and (F_ALTA_104 between @C4 and @C5) GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), METODOS.detalle_material(datos2.GetValue(0)), METODOS.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()
    End Sub
    Private Sub Todo_un_Deposito(ByVal Fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 9) and (ALMAE_104=@C2) and (ALMAR_104<>@C2) and (F_ALTA_104 between @C4 and @C5) GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
        consulta2.Parameters.Add(New SqlParameter("C4", Fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), METODOS.detalle_material(datos2.GetValue(0)), METODOS.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()
    End Sub

    Private Sub material_deposito(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104,sum(CANT_104) from T_REMI_104 where (T_MOV_104 = 9) and (C_MATE_104 =@C3) and (ALMAE_104=@C2) and (ALMAR_104<>@C2) AND (F_ALTA_104 between @C4 and @C5) GROUP BY C_MATE_104 ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C2", codigo_almacen))
        consulta2.Parameters.Add(New SqlParameter("C3", codigo_material))
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.DataGridView1.Rows.Add(datos2.GetValue(0), METODOS.detalle_material(datos2.GetValue(0)), METODOS.Unidad(datos2.GetValue(0)), datos2.GetValue(1).ToString)
        End While
        cnn2.Close()

    End Sub
    Private Sub un_material(ByVal fdesde As Date, ByVal fhasta As Date, ByVal material As String)

        Dim cnn9 As SqlConnection = New SqlConnection(conexion)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("select C_MATE_104, CANT_104 from T_REMI_104 where (T_MOV_104 = 9) and (C_MATE_104 =@C3) and (ALMAE_104<>ALMAR_104) and (F_ALTA_104 between @C4 and @C5) ", cnn2)
        consulta2.Parameters.Add(New SqlParameter("C3", material))
        consulta2.Parameters.Add(New SqlParameter("C4", fdesde))
        consulta2.Parameters.Add(New SqlParameter("C5", fhasta))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        suma = 0
        While datos2.Read()
            suma = suma + datos2.GetValue(1)
        End While
        cnn2.Close()
        cnn9.Close()
        If suma > 0 Then
            DataGridView1.Rows.Add(material, cbmateriales.Text, METODOS.Unidad(datos2.GetValue(material)), suma.ToString)
        End If

    End Sub
End Class