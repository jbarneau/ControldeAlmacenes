Imports System.Data.SqlClient

Public Class PALMA049

    Private lista As New List(Of MedDevueltos)
    Private Sub PALMA049_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DS_OPERARIO()
        llenar_DS_DEPO()
    End Sub

    Private Sub btnremito_Click(sender As Object, e As EventArgs) Handles btnremito.Click
        If cmbalma.SelectedValue <> "" And txtcantidad.Text <> "" And cmbdepo.SelectedValue <> "" Then
            GENERO_REMITO()
        End If
    End Sub

    Private Sub llenar_DS_DEPO()
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 and F_BAJA_003 IS NULL AND NDOC_003 = 11 ORDER BY NOMB_003", cnn2)
        adaptadaor.Fill(DS_deposito, "M_PERS_003")
        cnn2.Close()
        cmbdepo.DataSource = DS_deposito.Tables("M_PERS_003")
        cmbdepo.DisplayMember = "NOMB_003"
        cmbdepo.ValueMember = "NDOC_003"
        cmbdepo.Text = Nothing
    End Sub

    Private Sub llenar_DS_OPERARIO()
        Dim DS_deposito As New DataTable
        DS_deposito.Columns.Add("CODIGO")
        DS_deposito.Columns.Add("DESC")
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim adaptadaor As New SqlCommand("SELECT dbo.TEMP_MED_UBICAR.OPERARIO, dbo.M_PERS_003.APELL_003 + ' ' + dbo.M_PERS_003.NOMB_003 AS Expr1, dbo.M_PERS_003.F_BAJA_003 FROM dbo.TEMP_MED_UBICAR INNER JOIN dbo.M_PERS_003 ON dbo.TEMP_MED_UBICAR.OPERARIO = dbo.M_PERS_003.NDOC_003 GROUP BY dbo.TEMP_MED_UBICAR.OPERARIO, dbo.M_PERS_003.APELL_003 + ' ' + dbo.M_PERS_003.NOMB_003, dbo.M_PERS_003.F_BAJA_003 HAVING (dbo.M_PERS_003.F_BAJA_003 IS NULL) ORDER BY Expr1", cnn2)
        Dim LECTOR As SqlDataReader = adaptadaor.ExecuteReader
        Do While LECTOR.Read
            DS_deposito.Rows.Add(LECTOR.GetValue(0), LECTOR.GetValue(1))
        Loop
        cnn2.Close()
        cmbalma.DataSource = DS_deposito
        cmbalma.DisplayMember = "DESC"
        cmbalma.ValueMember = "CODIGO"
        cmbalma.Text = Nothing
    End Sub

    Private Sub GENERO_REMITO()
        lista.Clear()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Dim cuenta As Integer = 0
        Dim trajo As Integer = 0
        If txtcantidad.Text <> "" Then
            trajo = CInt(txtcantidad.Text)
        End If
        Try
            con1.Open()
            Dim obj As Object
            Dim comando1 As New SqlClient.SqlCommand("SELECT ALMA_123 FROM T_REG_INGRESO_123 WHERE ALMA_123 = @D2", con1)
            comando1.Parameters.Add(New SqlParameter("D2", cmbalma.SelectedValue))
            obj = comando1.ExecuteScalar()
            If obj IsNot Nothing Then
                Dim comando3 As New SqlClient.SqlCommand("UPDATE T_REG_INGRESO_123 SET ALMADEP_123 = 11, TRAJO_123 = @D2, INGRESO_123 = INGRESO_123 - @D2 WHERE ALMA_123 = @D1", con1)
                comando3.Parameters.Add(New SqlParameter("D1", cmbalma.SelectedValue))
                comando3.Parameters.Add(New SqlParameter("D2", trajo))
                comando3.ExecuteNonQuery()
            Else
                Dim comando2 As New SqlClient.SqlCommand("INSERT T_REG_INGRESO_123 (ALMA_123,INGRESO_123,TRAJO_123,ALMADEP_123) VALUES (@D1,@D2,@D3,@D4)", con1)
                comando2.Parameters.Add(New SqlParameter("D1", cmbalma.SelectedValue))
                comando2.Parameters.Add(New SqlParameter("D2", trajo))
                comando2.Parameters.Add(New SqlParameter("D3", trajo))
                comando2.Parameters.Add(New SqlParameter("D4", 11))
                comando2.ExecuteNonQuery()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR EN FUNCION GRABAR REG_INGRESO DE MEDIDORES - REMITO PASO 1")
        Finally
            con1.Close()
        End Try

        Try
            con1.Open()
            Dim obj As Object
            Dim comando1 As New SqlClient.SqlCommand("SELECT  APELL_003 + ' ' + NOMB_003 as NOMAPE FROM dbo.M_PERS_003 WHERE (NDOC_003 = @D1)", con1)
            comando1.Parameters.Add(New SqlParameter("D1", cmbalma.SelectedValue))
            obj = comando1.ExecuteScalar()
            If obj IsNot Nothing Then
                Dim item As New MedDevueltos
                item.GetSetAlmacen = Convert.ToString(obj)
                item.GetSetCantidad = trajo
                item.GetSetOperario = "DEPOSITO GENERAL THAMES"
                lista.Add(item)
                PrintDocument1.Print()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR EN FUNCION GRABAR REG_INGRESO DE MEDIDORES - REMITO PASO 2")
        Finally
            con1.Close()
        End Try

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        If lista.Count > 0 Then
            Dim cr As New RemitoMedsDevueltos
            cr.SetDataSource(lista)
            cr.PrintToPrinter(0, False, 0, 0)
        End If
    End Sub

    Private Sub txtcantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcantidad.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            e.Handled = True
        Else
            e.Handled = False
        End If
    End Sub
End Class