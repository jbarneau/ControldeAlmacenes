Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class PCOMB005

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        If dtpfecha.Value <> dtpfecha.MinDate Then
            dt = BuscarEnDbVehiculo(dtpfecha.Value, dtpfechahasta.Value)
            ArmarExcel1(dt)
        Else
            If cmbvehiculo.Text <> Nothing Then
                dt = BuscarEnDbVehiculo(cmbvehiculo.Text)
                ArmarExcel1(dt)
            End If
        End If
        'If dtpfecha.Value >= dtpfecha.MinDate And cmbvehiculo.SelectedValue = Nothing Then
        'MessageBox.Show("SELECCIONE UN DOMINIO O VERIFIQUE LA FECHA INICIAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If
        dtpfecha.Value = dtpfecha.MinDate
        cmbvehiculo.Text = Nothing
        'cmbvehiculo.SelectedValue = Nothing
    End Sub


    Private Function BuscarEnDbVehiculo(ByVal fecha As Date, ByVal fechahasta As Date) As DataTable
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Dim dt As DataTable = New DataTable()
        fecha.ToLongDateString()
        fechahasta.ToLongDateString()
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT T_VALE_122.NVALE122, T_VALE_122.FTICKET122, T_VALE_122.NTICKET122, T_VALE_122.LITROS122, T_VALE_122.DOMINI122, T_VALE_122.CHOFER122, M_PERS_003.NOMB_003, M_PERS_003.APELL_003 FROM T_VALE_122 INNER JOIN M_PERS_003 ON T_VALE_122.CHOFER122 = M_PERS_003.NDOC_003 WHERE (T_VALE_122.FECHA122 BETWEEN @D1 AND @D2)", cnn)
            adt.Parameters.AddWithValue("D1", fecha)
            adt.Parameters.AddWithValue("D2", fechahasta)
            Dim adapt As New SqlDataAdapter(adt)
            adapt.Fill(dt)
            Return dt
        Catch
            Throw New Exception("ERROR EN FUNCIÒN BUSCARENDB FECHA")
        Finally
            cnn.Close()
        End Try
    End Function

    Private Function BuscarEnDbVehiculo(ByVal ve As String) As DataTable
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Dim dt As DataTable = New DataTable()
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT T_VALE_122.NVALE122, T_VALE_122.FTICKET122, T_VALE_122.NTICKET122, T_VALE_122.LITROS122, T_VALE_122.DOMINI122, T_VALE_122.CHOFER122, M_PERS_003.NOMB_003, M_PERS_003.APELL_003 FROM T_VALE_122 INNER JOIN M_PERS_003 ON T_VALE_122.CHOFER122 = M_PERS_003.NDOC_003 WHERE T_VALE_122.DOMINI122 =@D1", cnn)
            adt.Parameters.AddWithValue("D1", ve)
            Dim adapt As New SqlDataAdapter(adt)
            adapt.Fill(dt)
            Return dt
        Catch
            Throw New Exception("ERROR EN FUNCIÒN BUSCARENDB DOM")
        Finally
            cnn.Close()
        End Try
    End Function

    Private Sub ArmarExcel1(ByRef dt As DataTable)
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        Dim fichero As String = "C:\Archivo\ReporteCombustible" + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("VALE;FECHA;NTICKET;LITROS;DOMINIO;CHOFER")
        Dim VALE As String
        Dim FECHA As String
        Dim NTICKET As String
        Dim LITROS As String
        Dim DOM As String
        Dim NOM As String
        Dim APE As String
        For i = 0 To dt.Rows.Count - 1
            VALE = dt.Rows(i).Item(0).ToString()
            FECHA = dt.Rows(i).Item(1).ToString()
            NTICKET = dt.Rows(i).Item(2).ToString()
            LITROS = dt.Rows(i).Item(3).ToString()
            DOM = dt.Rows(i).Item(4).ToString()
            NOM = dt.Rows(i).Item(6).ToString()
            APE = dt.Rows(i).Item(7).ToString()
            a.WriteLine(VALE + ";" + FECHA + ";" + NTICKET + ";" + LITROS + ";" + DOM + ";" + NOM + " " + APE)
        Next
        a.Close()
        MessageBox.Show("DATOS EXPORTADOS CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Function GetDataVehiculo() As DataTable
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Dim dt As DataTable = New DataTable()
        Try
            cnn.Open()
            Dim adt As String = "SELECT DOMINIO008 AS PATENTE FROM M_VEHICULO_008"
            Dim adapt As New SqlDataAdapter(adt, cnn)
            adapt.Fill(dt)
            Return dt
        Catch
            Throw New Exception("ERROR EN FUNCIÒN BUSCARENDB")
        Finally
            cnn.Close()
        End Try
    End Function

    Private Sub PCOMB005_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbvehiculo.DataSource = GetDataVehiculo()
        cmbvehiculo.DisplayMember = "PATENTE"
        cmbvehiculo.SelectedItem = Nothing
        dtpfecha.Value = "01/01/2013"
        dtpfecha.MinDate = "01/01/2013"
        dtpfecha.MaxDate = Date.Today
        dtpfechahasta.MaxDate = Date.Today
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
    End Sub

    Private Sub cmbvehiculo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbvehiculo.SelectedIndexChanged

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class