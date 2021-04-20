Imports System.Data.SqlClient

Public Class PALMA049

    Private lista As New List(Of MedDevueltos)
    Private Metodos As New Clas_Almacen
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
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003 AS DNI, APELL_003 + N' ' + NOMB_003 AS NOMYAPE FROM M_PERS_003 WHERE (F_BAJA_003 IS NULL) AND (ALMA_003 = 1) ORDER BY NOMYAPE", cnn2)
        adaptadaor.Fill(DS_deposito)
        cnn2.Close()
        cmbalma.DataSource = DS_deposito
        cmbalma.DisplayMember = "NOMYAPE"
        cmbalma.ValueMember = "DNI"
        cmbalma.Text = Nothing
    End Sub

    Private Sub GENERO_REMITO()
        lista.Clear()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Dim cuenta As Integer = 0
        Dim trajo As Integer = 0
        Dim _nremito As Decimal = Metodos.Obtener_Numero_Remito_Bolsas()
        Dim obs As String = "MOV BOLSAS"
        If txtcantidad.Text <> "" Then
            trajo = CInt(txtcantidad.Text)
        End If
        If txtobs.Text <> "" Then
            obs = txtobs.Text
        End If

        Try
            'Metodos.Grabar_Trans(_nremito, Date.Now, "", cmbalma.SelectedValue, "11", 1, Date.Now, 0, "MOV BOLSAS", 1, trajo, 0, _usr.Obt_Usr, "", "0")
            GrabarRemitoBolsas(_nremito, Date.Now, cmbalma.SelectedValue, trajo, 0, _usr.Obt_Usr, obs)
            con1.Open()
            Dim comando3 As New SqlClient.SqlCommand("UPDATE T_REG_INGRESO_123 SET CANT_RET_123 = CANT_RET_123 - @D2, CANT_TRAJO_123 = CANT_TRAJO_123 + @D2 WHERE OPERARIO_123 = @D1", con1)
            comando3.Parameters.Add(New SqlParameter("D1", cmbalma.SelectedValue))
            comando3.Parameters.Add(New SqlParameter("D2", trajo))
            cuenta = comando3.ExecuteNonQuery()
            Metodos.Sumar_Num_Remito_Bolsas()
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
                MessageBox.Show("SE HA GENERADO CORRECTAMENTE EL REMITO Nº  " + _nremito.ToString(), "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
                cmbdepo.SelectedValue = ""
                cmbalma.SelectedValue = ""
                txtcantidad.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR EN FUNCION GRABAR REG_INGRESO DE MEDIDORES - REMITO PASO 2")
        Finally
            con1.Close()
        End Try

    End Sub

    Private Sub GrabarRemitoBolsas(ByVal nremito As Double, ByVal falta As Date, ByVal operario As String, ByVal contrato As Integer, ByVal tipomov As Integer, ByVal usrgenrem As String, ByVal obs As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim comando As New SqlClient.SqlCommand("INSERT INTO T_REG_BOLSAS_124 (NREMITO_124,FALTA_124,OPERARIO_124,CANT_124,TIPO_124,USR_GEN_REMITO_124,OBS_124) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7)", cnn)
            comando.Parameters.Add(New SqlParameter("D1", nremito))
            comando.Parameters.Add(New SqlParameter("D2", falta))
            comando.Parameters.Add(New SqlParameter("D3", operario))
            comando.Parameters.Add(New SqlParameter("D4", contrato))
            comando.Parameters.Add(New SqlParameter("D5", tipomov))
            comando.Parameters.Add(New SqlParameter("D6", usrgenrem))
            comando.Parameters.Add(New SqlParameter("D7", obs))
            comando.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            cnn.Close()
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