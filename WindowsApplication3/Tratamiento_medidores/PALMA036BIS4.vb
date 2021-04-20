Imports System.Data.SqlClient

Public Class PALMA036BIS4

    Private DT_medidores As New DataTable
    Private medidor As New Clase_med_retirar
    Private Sub PALMA036BIS4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llenar_DS_DEPOSITO()
        llenar_DS_FAMILIA()
        labelusr.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        lblfecha.Text = "Fecha: " & DateTime.Now().ToShortDateString
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs)
    '    For index = 0 To ListBox1.SelectedItems.Count - 1
    '        If ListBox1.SelectedItem = True Then
    '            ListBox2.Items.Add(ListBox1.Items(index))
    '        End If
    '    Next

    '    ListBox2.Items.Add(ListBox1.SelectedItem)
    '    For i = 0 To ListBox2.Items.Count - 1
    '        For g = ListBox1.Items.Count - 1 To 0 Step -1
    '            If ListBox1.Items(g) = ListBox2.Items(i) Then
    '                ListBox1.Items.RemoveAt(g)
    '            End If
    '        Next
    '    Next
    'End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs)
    '    ListBox1.Items.Add(ListBox2.SelectedItem)
    '    For i = 0 To ListBox1.Items.Count - 1
    '        For g = ListBox2.Items.Count - 1 To 0 Step -1
    '            If ListBox2.Items(g) = ListBox1.Items(i) Then
    '                ListBox2.Items.RemoveAt(g)
    '            End If
    '        Next
    '    Next
    'End Sub

    Private Sub llenar_DS_DEPOSITO()
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 and F_BAJA_003 IS NULL AND (NDOC_003 = N'11') ORDER BY NOMB_003", cnn2)
        adaptadaor.Fill(DS_deposito, "M_PERS_003")
        cnn2.Close()
        ComboBox3.DataSource = DS_deposito.Tables("M_PERS_003")
        ComboBox3.DisplayMember = "NOMB_003"
        ComboBox3.ValueMember = "NDOC_003"
        ComboBox3.Text = Nothing
    End Sub

    Private Sub llenar_LB_CAJONES_LISTOS(ByVal familia As String, ByVal deposito As String)
        Dim DATA As New DataTable
        Dim renglon As New ListViewItem
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim adaptadaor As New SqlDataAdapter("SELECT CAJON_113, COUNT(NSERI_113) AS Expr1 FROM T_MED_DEVO_113 WHERE (DEPOSI_113 = @D1) AND (ESTADO_113 = 1 OR ESTADO_113 = 9) AND (FAMILIA_113 = @D2) AND (NOT (CAJON_113 LIKE N'R%')) GROUP BY CAJON_113 ORDER BY CAJON_113", cnn2)
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", deposito))
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D2", familia))
        adaptadaor.Fill(DATA)
        cnn2.Close()
        For i = 0 To DATA.Rows.Count - 1
            ListBox2.Items.Add(DATA.Rows(i).Item(0).ToString)
        Next
    End Sub

    Private Sub llenar_LB_CAJONES(ByVal familia As String, ByVal deposito As String)
        DT_medidores.Clear()
        ListBox1.Items.Clear()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim adaptadaor As New SqlDataAdapter("SELECT CAJON_113, COUNT(NSERI_113) AS Expr1 FROM T_MED_DEVO_113 WHERE (DEPOSI_113 = @D2) AND (ESTADO_113 = 1 OR ESTADO_113 = 9) AND (FAMILIA_113 = @D1) AND (NOT (CAJON_113 LIKE N'R%')) GROUP BY CAJON_113 ORDER BY CAJON_113", cnn2)
            adaptadaor.SelectCommand.Parameters.AddWithValue("D1", familia)
            adaptadaor.SelectCommand.Parameters.AddWithValue("D2", deposito)
            adaptadaor.Fill(DT_medidores)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()
        End Try
        For i = 0 To DT_medidores.Rows.Count - 1
            ListBox1.Items.Add(DT_medidores.Rows(i).Item(0).ToString())
        Next
    End Sub


    Private Sub llenar_DS_FAMILIA()
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802 = 15) and (F_BAJA_802 IS NULL) and (C_PARA_802 = 6) ORDER BY DESC_802", cnn2)
        adaptadaor.Fill(DS_deposito, " DET_PARAMETRO_802")
        cnn2.Close()
        cmbfamilia.DataSource = DS_deposito.Tables(" DET_PARAMETRO_802")
        cmbfamilia.DisplayMember = "DESC_802"
        cmbfamilia.ValueMember = "C_PARA_802"
        cmbfamilia.Text = Nothing
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox2.Items.Count > 0 Then
            If ListBox2.SelectedItems.Count > 0 Then
                TransferirCajones(ListBox2.SelectedItem)
            End If
        End If
    End Sub

    Private Sub TransferirCajones(ByVal cajonDestino As String)
        Cursor.Current = Cursors.WaitCursor
        Dim nmov As Decimal = medidor.Obtener_Numero_Mov()
        Dim acum As Integer = 0
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            If ListBox2.SelectedItem = "ROLON" Then
                cajonDestino = "R" + Date.Now.Year.ToString() + "000000R"
                'med_rettirar.Obtener_Numero_lote.ToString.PadLeft(8, "0")
                For i = 0 To ListBox1.SelectedItems.Count - 1
                    Dim cmd As New SqlCommand("UPDATE T_MED_DEVO_113 SET CAJON_113 = @D1 WHERE CAJON_113 = @D2", cnn)
                    cmd.Parameters.AddWithValue("D1", cajonDestino)
                    cmd.Parameters.AddWithValue("D2", ListBox1.SelectedItems(i))
                    acum += cmd.ExecuteNonQuery()
                    If acum > 0 Then
                        medidor.GRABAR_TRANSFERENCIA(cajonDestino, nmov, DateTime.Now, ListBox1.SelectedItems(i), cajonDestino, _usr.Obt_Usr)
                    End If
                Next
                MessageBox.Show("SE HAN TRANSPASADO" + " " + acum.ToString() + " " + "MEDIDOR/ES AL CAJON" + " " + cajonDestino, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                '    For i = 0 To ListBox1.SelectedItems.Count - 1
                '        Dim cmd As New SqlCommand("UPDATE T_MED_DEVO_113 SET CAJON_113 = @D1 WHERE CAJON_113 = @D2", cnn)
                '        cmd.Parameters.AddWithValue("D1", cajonDestino)
                '        cmd.Parameters.AddWithValue("D2", ListBox1.SelectedItems(i))
                '        acum += cmd.ExecuteNonQuery()
                '        If acum > 0 Then
                '            medidor.GRABAR_TRANSFERENCIA(cajonDestino, nmov, DateTime.Now, ListBox1.SelectedItems(i), cajonDestino, _usr.Obt_Usr)
                '        End If
                '    Next
                '    MessageBox.Show("SE HAN TRANSPASADO" + " " + acum.ToString() + " " + "MEDIDOR/ES AL CAJON" + " " + cajonDestino, "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            cnn.Close()
        End Try
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        llenar_LB_CAJONES(cmbfamilia.SelectedValue, ComboBox3.SelectedValue)
        'llenar_LB_CAJONES_LISTOS(cmbfamilia.SelectedValue, ComboBox3.SelectedValue)
        If cmbfamilia.SelectedValue = "6" Then
            ListBox2.Items.Add("ROLON")
        End If
        Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub cmbfamilia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbfamilia.SelectedIndexChanged
        Cursor.Current = Cursors.WaitCursor
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        'If btngenerarolon.Visible = True Then
        '    btngenerarolon.Visible = False
        'End If
        'If btngenerarolon.Enabled = True Then
        '    btngenerarolon.Enabled = False
        'End If
        If ComboBox3.SelectedValue <> Nothing Then
            If cmbfamilia.SelectedValue = "6" Then
                'btngenerarolon.Visible = True
                'btngenerarolon.Enabled = True
                ListBox2.Items.Add("ROLON")
            End If
            llenar_LB_CAJONES(cmbfamilia.SelectedValue, ComboBox3.SelectedValue)
            'llenar_LB_CAJONES_LISTOS(cmbfamilia.SelectedValue, ComboBox3.SelectedValue)
        End If
        Cursor.Current = Cursors.Arrow
    End Sub

    Private Sub btngenerarolon_Click(sender As Object, e As EventArgs)
        GenerarRolon()
    End Sub


    Private Sub GenerarRolon()
        'Aca habria que generar un Rolon nuevo, Nose como.

    End Sub

End Class