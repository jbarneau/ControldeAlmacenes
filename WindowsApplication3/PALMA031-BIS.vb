Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class PALMA031BIS
    Private Sub PALMA031BIS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        LLENAR_TIPO()
    End Sub

    Private Sub LLENAR_TIPO()
        Dim DS As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where C_TABLA_802 = 1 AND (C_PARA_802 =4 OR C_PARA_802 = 5)", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS, "DET_PARAMETRO_802")
        cnn2.Close()
        cmbtipo.DataSource = DS.Tables("DET_PARAMETRO_802")
        cmbtipo.ValueMember = "C_PARA_802"
        cmbtipo.DisplayMember = "DESC_802"
        cmbtipo.Text = Nothing
    End Sub

    Public Sub EntreFechas(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.C_MATE_104, T_REMI_104.F_ALTA_104, M_PERS_003.NOMB_003 + ' ' + M_PERS_003.APELL_003 AS USR, T_REMI_104.CANT_104, DET_PARAMETRO_802.DESC_802, M_MATE_002.DESC_002 FROM T_REMI_104 INNER JOIN M_PERS_003 ON T_REMI_104.USER_104 = M_PERS_003.NDOC_003 INNER JOIN DET_PARAMETRO_802 ON T_REMI_104.T_MOV_104 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 WHERE (T_REMI_104.T_MOV_104 = @D1) AND (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) GROUP BY T_REMI_104.C_MATE_104, T_REMI_104.F_ALTA_104, T_REMI_104.USER_104, T_REMI_104.CANT_104, M_PERS_003.NOMB_003, M_PERS_003.APELL_003, T_REMI_104.T_MOV_104, DET_PARAMETRO_802.C_TABLA_802, DET_PARAMETRO_802.DESC_802, M_MATE_002.DESC_002 HAVING (DET_PARAMETRO_802.C_TABLA_802 = 1) ORDER BY T_REMI_104.F_ALTA_104", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D1", cmbtipo.SelectedValue.ToString()))
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta))
        consulta2.ExecuteNonQuery()
        Dim datos2 As SqlDataReader = consulta2.ExecuteReader()
        While datos2.Read
            Me.dgvreporte.Rows.Add(datos2.GetValue(0), datos2.GetValue(1), datos2.GetValue(2), datos2.GetValue(3), datos2.GetValue(4), datos2.GetValue(5))
        End While
        cnn2.Close()
    End Sub

    Public Function EntreFechas2(ByVal fdesde As Date, ByVal fhasta As Date)
        Dim dt As DataTable = New DataTable()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim consulta2 As New SqlClient.SqlCommand("SELECT T_REMI_104.C_MATE_104, T_REMI_104.F_ALTA_104, M_PERS_003.NOMB_003 + ' ' + M_PERS_003.APELL_003 AS USR, T_REMI_104.CANT_104, DET_PARAMETRO_802.DESC_802, M_MATE_002.DESC_002 FROM T_REMI_104 INNER JOIN M_PERS_003 ON T_REMI_104.USER_104 = M_PERS_003.NDOC_003 INNER JOIN DET_PARAMETRO_802 ON T_REMI_104.T_MOV_104 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_MATE_002 ON T_REMI_104.C_MATE_104 = M_MATE_002.CMATE_002 WHERE (T_REMI_104.T_MOV_104 = 4 OR T_REMI_104.T_MOV_104 = 5) AND (T_REMI_104.F_ALTA_104 BETWEEN @D2 AND @D3) GROUP BY T_REMI_104.C_MATE_104, T_REMI_104.F_ALTA_104, T_REMI_104.USER_104, T_REMI_104.CANT_104, M_PERS_003.NOMB_003, M_PERS_003.APELL_003, T_REMI_104.T_MOV_104, DET_PARAMETRO_802.C_TABLA_802, DET_PARAMETRO_802.DESC_802, M_MATE_002.DESC_002 HAVING (DET_PARAMETRO_802.C_TABLA_802 = 1) ORDER BY T_REMI_104.F_ALTA_104", cnn2)
        consulta2.Parameters.Add(New SqlParameter("D2", fdesde))
        consulta2.Parameters.Add(New SqlParameter("D3", fhasta))
        Dim adapter As SqlDataAdapter = New SqlDataAdapter(consulta2)
        adapter.Fill(dt)
        Return dt
        cnn2.Close()
    End Function

    Private Sub btnconsultar_Click(sender As Object, e As EventArgs) Handles btnconsultar.Click
        dgvreporte.Rows.Clear()
        EntreFechas(dtpdesde.Value, dtphasta.Value)
    End Sub

    Private Sub btnexcel_Click(sender As Object, e As EventArgs) Handles btnexcel.Click
        If dgvreporte.Rows.Count > 0 Then
            ArmarExcel1()
        Else
            ArmarExcel2(EntreFechas2(dtpdesde.Value, dtphasta.Value))
        End If

    End Sub

    Private Sub ArmarExcel1()
        Dim ofd As SaveFileDialog = New SaveFileDialog
        ofd.Filter = ".csv|"
        ofd.DefaultExt = "CSV"
        If ofd.ShowDialog = DialogResult.OK Then
            Dim fichero As String = ofd.FileName
            Dim a As New System.IO.StreamWriter(fichero)
            a.WriteLine("CODMATE;FALTA;USUARIO;CANTIDAD;TIPO;DESCRIPCIONMATE")
            Dim CODMATE As String
            Dim FALTA As DateTime
            Dim USUARIO As String
            Dim CANTIDAD As String
            Dim TIPO As String
            Dim DESC As String
            For i = 0 To dgvreporte.Rows.Count - 1
                CODMATE = dgvreporte.Item(0, i).Value
                FALTA = Convert.ToDateTime(dgvreporte.Item(1, i).Value)
                USUARIO = dgvreporte.Item(2, i).Value
                CANTIDAD = dgvreporte.Item(3, i).Value
                TIPO = dgvreporte.Item(4, i).Value
                DESC = dgvreporte.Item(5, i).Value
                a.WriteLine(CODMATE + ";" + FALTA.ToShortDateString() + ";" + USUARIO + ";" + CANTIDAD + ";" + TIPO + ";" + DESC)
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
            a.WriteLine("CODMATE;FALTA;USUARIO;CANTIDAD;TIPO;DESCRIPCIONMATE")
            Dim CODMATE As String
            Dim FALTA As DateTime
            Dim USUARIO As String
            Dim CANTIDAD As String
            Dim TIPO As String
            Dim DESC As String
            For i = 0 To dt.Rows.Count - 1
                CODMATE = dt.Rows(i)(0)
                FALTA = Convert.ToDateTime(dt.Rows(i)(1))
                USUARIO = dt.Rows(i)(2)
                CANTIDAD = dt.Rows(i)(3)
                TIPO = dt.Rows(i)(4)
                DESC = dt.Rows(i)(5)
                a.WriteLine(CODMATE + ";" + FALTA.ToShortDateString() + ";" + USUARIO + ";" + CANTIDAD + ";" + TIPO + ";" + DESC)
            Next
            a.Close()
            MessageBox.Show("DATOS EXPORTADOS CORRECTAMENTE", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class