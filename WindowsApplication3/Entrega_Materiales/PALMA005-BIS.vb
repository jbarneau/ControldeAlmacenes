Imports System.Data.SqlClient

Public Class PALMA005_BIS
    Private Sub btnconsultar_Click(sender As Object, e As EventArgs) Handles btnconsultar.Click
        dgvreporte.Rows.Clear()
        If cmbentrega.SelectedValue <> Nothing And cmbrecibe.SelectedValue <> Nothing And cmbtipo.SelectedValue <> Nothing Then
            If cmbtipo.SelectedValue = 9 Then
                EntreFechasENTREGA(dtpdesde.Value, dtphasta.Value)
            Else
                If cmbtipo.SelectedValue = 1 Then
                    EntreFechasINGRESO(dtpdesde.Value, dtphasta.Value)
                Else
                    EntreFechas(dtpdesde.Value, dtphasta.Value)
                End If
            End If
        End If

    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        If dgvreporte.Rows.Count > 0 Then
            ArmarExcel1()
        Else
            If cmbtipo.SelectedValue <> Nothing Then
                If cmbtipo.SelectedValue = 9 Then
                    ArmarExcel2(EntreFechasTODOSENTREGA(dtpdesde.Value, dtphasta.Value))
                Else
                    If cmbtipo.SelectedValue = 1 Then
                        ArmarExcel2(EntreFechasTODOSINGRESO(dtpdesde.Value, dtphasta.Value))
                    Else
                        If cmbtipo.SelectedValue = 3 Then
                            ArmarExcel2(EntreFechasTRANSF(dtpdesde.Value, dtphasta.Value))
                        Else
                            ArmarExcel2(EntreFechasTODOS(dtpdesde.Value, dtphasta.Value))
                        End If
                    End If
                End If
            End If
        End If
        dgvreporte.DataSource = Nothing
    End Sub

    Public Sub EntreFechas(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMAPEENT, T_REMI_104.ALMAR_104, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS NOMAPEREC, T_REMI_104.T_MOV_104, T_REMI_104.CANT_104, T_REMI_104.USER_104, M_PERS_003_2.APELL_003 + ' ' + M_PERS_003_2.NOMB_003 AS USR_CONF FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.ALMAR_104 = M_PERS_003_1.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_2 ON T_REMI_104.USER_104 = M_PERS_003_2.NDOC_003 WHERE (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) AND (T_REMI_104.ALMAE_104 = @D1) AND (T_REMI_104.ALMAR_104 = @D4) AND (T_REMI_104.T_MOV_104 = @D5)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D1", cmbentrega.SelectedValue))
        consulta2.Parameters.Add(New SqlParameter("D4", cmbrecibe.SelectedValue))
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde.ToShortDateString))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta.ToShortDateString))
        consulta2.Parameters.Add(New SqlParameter("D5", cmbtipo.SelectedValue))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.dgvreporte.Rows.Add(datos2.GetValue(0), datos2.GetValue(1), datos2.GetValue(2), datos2.GetValue(3), datos2.GetValue(4), datos2.GetValue(5), datos2.GetValue(6), datos2.GetValue(7), datos2.GetValue(9), datos2.GetValue(11))
        End While
        cnn2.Close()
    End Sub

    Public Sub EntreFechasINGRESO(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMAPEENT, T_REMI_104.ALMAR_104, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS NOMAPEREC, T_REMI_104.T_MOV_104, T_REMI_104.CANT_104, T_REMI_104.USER_104, M_PERS_003_2.APELL_003 + ' ' + M_PERS_003_2.NOMB_003 AS USR_CONF FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.ALMAR_104 = M_PERS_003_1.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_2 ON T_REMI_104.USER_104 = M_PERS_003_2.NDOC_003 WHERE (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) AND (T_REMI_104.ALMAE_104 = @D1) AND (T_REMI_104.ALMAR_104 = @D4) AND (T_REMI_104.T_MOV_104 = @D5) AND (M_PERS_003_1.DEPO_003 = @D6)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D1", cmbentrega.SelectedValue))
        consulta2.Parameters.Add(New SqlParameter("D4", cmbrecibe.SelectedValue))
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde.ToShortDateString))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta.ToShortDateString))
        consulta2.Parameters.Add(New SqlParameter("D5", cmbtipo.SelectedValue))
        consulta2.Parameters.Add(New SqlParameter("D6", True))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.dgvreporte.Rows.Add(datos2.GetValue(0), datos2.GetValue(1), datos2.GetValue(2), datos2.GetValue(3), datos2.GetValue(4), datos2.GetValue(5), datos2.GetValue(6), datos2.GetValue(7), datos2.GetValue(9), datos2.GetValue(11))
        End While
        cnn2.Close()
    End Sub

    Public Sub EntreFechasENTREGA(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMAPEENT, T_REMI_104.ALMAR_104, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS NOMAPEREC, T_REMI_104.T_MOV_104, T_REMI_104.CANT_104, T_REMI_104.USER_104 FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.ALMAR_104 = M_PERS_003_1.NDOC_003 WHERE (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) AND (T_REMI_104.ALMAE_104 = @D1) AND (T_REMI_104.ALMAR_104 = @D4) AND (T_REMI_104.T_MOV_104 = @D5) AND (M_PERS_003_1.DEPO_003 = @D6)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D1", cmbentrega.SelectedValue))
        consulta2.Parameters.Add(New SqlParameter("D4", cmbrecibe.SelectedValue))
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde.ToShortDateString))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta.ToShortDateString))
        consulta2.Parameters.Add(New SqlParameter("D5", 9))
        consulta2.Parameters.Add(New SqlParameter("D6", False))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.dgvreporte.Rows.Add(datos2.GetValue(0), datos2.GetValue(1), datos2.GetValue(2), datos2.GetValue(3), datos2.GetValue(4), datos2.GetValue(5), datos2.GetValue(6), datos2.GetValue(7), datos2.GetValue(9), datos2.GetValue(10))
        End While
        cnn2.Close()
    End Sub

    Public Function EntreFechasTODOS(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim dt As DataTable = New DataTable()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMAPEENT, T_REMI_104.ALMAR_104, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS NOMAPEREC, T_REMI_104.T_MOV_104, T_REMI_104.CANT_104, T_REMI_104.USER_104, M_PERS_003_2.APELL_003 + ' ' + M_PERS_003_2.NOMB_003 AS USR_CONF FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.ALMAR_104 = M_PERS_003_1.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_2 ON T_REMI_104.USER_104 = M_PERS_003_2.NDOC_003 WHERE (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) AND (T_REMI_104.T_MOV_104 = @D4) AND (M_PERS_003.DEPO_003 = 0)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta))
        consulta2.Parameters.Add(New SqlParameter("D4", cmbtipo.SelectedValue))
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(consulta2)
        adapter.Fill(dt)
        Return dt
        cnn2.Close()
    End Function

    Public Function EntreFechasTRANSF(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim dt As DataTable = New DataTable()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMAPEENT, T_REMI_104.ALMAR_104, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS NOMAPEREC, T_REMI_104.T_MOV_104, T_REMI_104.CANT_104, T_REMI_104.USER_104, M_PERS_003_2.APELL_003 + ' ' + M_PERS_003_2.NOMB_003 AS USR_CONF FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.ALMAR_104 = M_PERS_003_1.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_2 ON T_REMI_104.USER_104 = M_PERS_003_2.NDOC_003 WHERE (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) AND (T_REMI_104.T_MOV_104 = @D4) AND (M_PERS_003_1.DEPO_003 = @D6)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta))
        consulta2.Parameters.Add(New SqlParameter("D4", 3))
        consulta2.Parameters.Add(New SqlParameter("D6", True))
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(consulta2)
        adapter.Fill(dt)
        Return dt
        cnn2.Close()
    End Function

    Public Function EntreFechasTODOSENTREGA(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim dt As DataTable = New DataTable()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMAPEENT, T_REMI_104.ALMAR_104, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS NOMAPEREC, T_REMI_104.T_MOV_104, T_REMI_104.CANT_104, T_REMI_104.USER_104 FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.ALMAR_104 = M_PERS_003_1.NDOC_003 WHERE (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) AND (T_REMI_104.T_MOV_104 = @D4 OR T_REMI_104.T_MOV_104 = @D5) AND (M_PERS_003_1.DEPO_003 = @D6)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta))
        consulta2.Parameters.Add(New SqlParameter("D4", 9))
        consulta2.Parameters.Add(New SqlParameter("D5", 1))
        consulta2.Parameters.Add(New SqlParameter("D6", False))
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(consulta2)
        adapter.Fill(dt)
        Return dt
        cnn2.Close()
    End Function

    Public Function EntreFechasTODOSINGRESO(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim dt As DataTable = New DataTable()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.N_REMI_104, T_REMI_104.F_ALTA_104, T_REMI_104.C_MATE_104, M_MATE_002.DESC_002, T_REMI_104.ALMAE_104, M_PERS_003.APELL_003 + ' ' + M_PERS_003.NOMB_003 AS NOMAPEENT, T_REMI_104.ALMAR_104, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS NOMAPEREC, T_REMI_104.T_MOV_104, T_REMI_104.CANT_104, T_REMI_104.USER_104, M_PERS_003_2.APELL_003 + ' ' + M_PERS_003_2.NOMB_003 AS USR_CONF FROM T_REMI_104 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 INNER JOIN M_PERS_003 ON T_REMI_104.ALMAE_104 = M_PERS_003.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_1 ON T_REMI_104.ALMAR_104 = M_PERS_003_1.NDOC_003 INNER JOIN M_PERS_003 AS M_PERS_003_2 ON T_REMI_104.USER_104 = M_PERS_003_2.NDOC_003 WHERE (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) AND (T_REMI_104.T_MOV_104 = @D4) AND (M_PERS_003_1.DEPO_003 = @D6)", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta))
        consulta2.Parameters.Add(New SqlParameter("D4", 1))
        consulta2.Parameters.Add(New SqlParameter("D6", True))
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(consulta2)
        adapter.Fill(dt)
        Return dt
        cnn2.Close()
    End Function

    Private Sub ArmarExcel1()
        Dim ofd As SaveFileDialog = New SaveFileDialog
        ofd.Filter = ".csv|"
        ofd.DefaultExt = "CSV"
        If ofd.ShowDialog = DialogResult.OK Then
            Dim fichero As String = ofd.FileName
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("NºREMITO;F_ALTA;CODMATE;DESCRIPCION;ALMA_E;NOM_E;ALMA_R;NOM_R;CANTIDAD;USR_CONF")
            Dim NREMITO As String
            Dim FALTA As DateTime
            Dim CODMATE As String
            Dim DESC As String
            Dim ALMA_E As String
            Dim NOM_E As String
            Dim ALMA_R As String
            Dim NOM_R As String
            Dim CANTIDAD As String
            Dim USR_CONF As String
            For i = 0 To dgvreporte.Rows.Count - 1
                NREMITO = dgvreporte.Item(0, i).Value
                FALTA = Convert.ToDateTime(dgvreporte.Item(1, i).Value)
                CODMATE = dgvreporte.Item(2, i).Value
                DESC = dgvreporte.Item(3, i).Value
                ALMA_E = dgvreporte.Item(4, i).Value
                NOM_E = dgvreporte.Item(5, i).Value
                ALMA_R = dgvreporte.Item(6, i).Value
                NOM_R = dgvreporte.Item(7, i).Value
                CANTIDAD = dgvreporte.Item(8, i).Value
                USR_CONF = dgvreporte.Item(9, i).Value
                a.WriteLine(NREMITO + ";" + FALTA.ToShortDateString() + ";" + CODMATE + ";" + DESC + ";" + ALMA_E + ";" + NOM_E + ";" + ALMA_R + ";" + NOM_R + ";" + CANTIDAD + ";" + USR_CONF)
            Next
            a.Close()
            MessageBox.Show("DATOS EXPORTADOS CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub ArmarExcel2(ByVal dt As DataTable)
        Dim ofd As SaveFileDialog = New SaveFileDialog
        ofd.Filter = ".csv|"
        ofd.DefaultExt = "CSV"
        If ofd.ShowDialog = DialogResult.OK Then
            Dim fichero As String = ofd.FileName
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("NºREMITO;F_ALTA;CODMATE;DESCRIPCION;ALMA_E;NOM_E;ALMA_R;NOM_R;CANTIDAD;USR_CONF")
            Dim NREMITO As String
            Dim FALTA As DateTime
            Dim CODMATE As String
            Dim DESC As String
            Dim ALMA_E As String
            Dim NOM_E As String
            Dim ALMA_R As String
            Dim NOM_R As String
            Dim CANTIDAD As String
            Dim USR_CONF As String
            For i = 0 To dt.Rows.Count - 1
                NREMITO = dt.Rows(i).Item(0)
                FALTA = Convert.ToDateTime(dt.Rows(i).Item(1))
                CODMATE = dt.Rows(i).Item(2)
                DESC = dt.Rows(i).Item(3)
                ALMA_E = dt.Rows(i).Item(4)
                NOM_E = dt.Rows(i).Item(5)
                ALMA_R = dt.Rows(i).Item(6)
                NOM_R = dt.Rows(i).Item(7)
                CANTIDAD = dt.Rows(i).Item(9)
                USR_CONF = dt.Rows(i).Item(10)
                a.WriteLine(NREMITO + ";" + FALTA.ToShortDateString() + ";" + CODMATE + ";" + DESC + ";" + ALMA_E + ";" + NOM_E + ";" + ALMA_R + ";" + NOM_R + ";" + CANTIDAD + ";" + USR_CONF)
            Next
            a.Close()
                MessageBox.Show("DATOS EXPORTADOS CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub PALMA005_BIS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        LLENARCB_ENTREGA()
        LLENARCB_RECIBE()
        LLENARCB_TIPO()
    End Sub

    Private Sub LLENARCB_ENTREGA()
        Dim DS_entrega As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_entrega, "M_PERS_003")
        cnn2.Close()
        cmbentrega.DataSource = DS_entrega.Tables("M_PERS_003")
        cmbentrega.DisplayMember = "NOMBRE"
        cmbentrega.ValueMember = "NDOC_003"
        cmbentrega.Text = Nothing
    End Sub

    Private Sub LLENARCB_RECIBE()
        Dim DS_recibe As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_recibe, "M_PERS_003")
        cnn2.Close()
        cmbrecibe.DataSource = DS_recibe.Tables("M_PERS_003")
        cmbrecibe.DisplayMember = "NOMBRE"
        cmbrecibe.ValueMember = "NDOC_003"
        cmbrecibe.Text = Nothing
    End Sub

    Private Sub LLENARCB_TIPO()
        Dim DS_tipo As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT DESC_802, C_TABLA_802, C_PARA_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802 = 1) AND (C_PARA_802 = 1 OR C_PARA_802 = 2 OR C_PARA_802 = 3 OR C_PARA_802 = 9)", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_tipo, "DET_PARAMETRO_802")
        cnn2.Close()
        'Dim dr As DataRow = DS_tipo.Tables("DET_PARAMETRO_802").NewRow
        'dr(0) = "ENTREGA"
        'dr(1) = 1
        'dr(2) = 7
        'DS_tipo.Tables("DET_PARAMETRO_802").Rows.Add(dr)
        cmbtipo.DataSource = DS_tipo.Tables("DET_PARAMETRO_802")
        cmbtipo.DisplayMember = "DESC_802"
        cmbtipo.ValueMember = "C_PARA_802"
        cmbtipo.Text = Nothing
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbtipo.SelectedIndexChanged

    End Sub

    Private Sub btnlimpiar_Click(sender As Object, e As EventArgs) Handles btnlimpiar.Click
        cmbentrega.Text = cmbrecibe.Text = cmbtipo.Text = Nothing
        dgvreporte.DataSource = Nothing
        dgvreporte.Rows.Clear()
    End Sub
End Class