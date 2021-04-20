Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA008
    Private DS_material As New DataSet
    Private DS_almacen As New DataSet
    Private _MATERIAL As String
    Private _ALMACEN As String
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Medidor As New Clas_Medidor


    Private Sub PALMA008_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Desde.Value = Date.Now.ToShortDateString
        Hasta.Value = Date.Now.ToShortDateString
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        Desde.Value = Date.Now.ToShortDateString
        Hasta.Value = Date.Now.ToShortDateString
        llenar_DS_ALMACEN()
        llenar_DS_MATERIAL()
        LLENAR_CB_ALMACEN()
        LLENAR_CB_MATERIAL()
    End Sub




   
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim pantalla As New PALMA009
        Me.Close()
        pantalla.ShowDialog()

    End Sub
    '#######################FUNSIONES###############################FUNSIONES###############################
    Private Sub llenar_DS_ALMACEN()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE DEPO_003 = 0 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
    End Sub
    Private Sub llenar_DS_MATERIAL()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CMATE_002, DESC_002 FROM M_MATE_002 where TIPO_002 = 3 AND F_BAJA_002 IS NULL order by  DESC_002", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_material, "M_MATE_002")
        cnn2.Close()
    End Sub
    Private Sub LLENAR_CB_ALMACEN()
        CB_Equipo.DataSource = DS_almacen.Tables("M_PERS_003")
        CB_Equipo.DisplayMember = "NOMBRE"
        CB_Equipo.ValueMember = "NDOC_003"
        CB_Equipo.Text = Nothing
    End Sub
    Private Sub LLENAR_CB_MATERIAL()
        ComboBox1.DataSource = DS_material.Tables("M_MATE_002")
        ComboBox1.DisplayMember = "DESC_002"
        ComboBox1.ValueMember = "CMATE_002"
        ComboBox1.Text = Nothing
    End Sub

    Private Sub LLENAR_DW1(ByVal ALMACEN As String, ByVal herramienta As String, ByVal DATE1 As Date, ByVal DATE2 As Date)
        ' Dim moti As Integer
        Me.DataGridView1.Rows.Clear()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("SELECT T_REMI_104.F_ALTA_104, M_MATE_002.DESC_002, T_REMI_104.CANT_104, T_REMI_104.MOTI_104, M_MATE_002.UNID_002 FROM M_MATE_002 INNER JOIN T_REMI_104 ON M_MATE_002.CMATE_002 = T_REMI_104.C_MATE_104 WHERE (M_MATE_002.TIPO_002 = 3) AND (T_REMI_104.F_ALTA_104 between @D1 AND @D2) AND (T_REMI_104.ALMAR_104 = @D3) AND (T_REMI_104.C_MATE_104 = @D4) order by T_REMI_104.F_ALTA_104", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", DATE1))
        Comando.Parameters.Add(New SqlParameter("D2", DATE2))
        Comando.Parameters.Add(New SqlParameter("D3", ALMACEN))
        Comando.Parameters.Add(New SqlParameter("D4", herramienta))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        While Dusrs.Read
            ' moti = Dusrs.GetInt32(3)
            Me.DataGridView1.Rows.Add(CDate(Dusrs.GetValue(0)).ToShortDateString, Dusrs.GetString(1), Dusrs.GetString(4), Dusrs.GetDouble(2), Motivo(Dusrs.GetInt32(3)))
        End While
        If Me.DataGridView1.Rows.Count = 0 Then
            MENSAJE.MERRO011()
        End If
        cnn1.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim Fdesde As Date = Desde.Value
        Dim Fhasta As Date = Hasta.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        If CB_Equipo.SelectedValue <> Nothing Then
            If ComboBox1.Text <> Nothing Then
                LLENAR_DW1(CB_Equipo.SelectedValue, ComboBox1.SelectedValue, Fdesde.ToString, Fhasta.ToString)
            Else
                LLENAR_DW2(CB_Equipo.SelectedValue, Fdesde.ToString, Fhasta.ToString)
            End If
        Else
            Me.DataGridView1.Rows.Clear()
            MENSAJE.MERRO006()
        End If

    End Sub

    Private Function Motivo(ByVal moti As Integer) As String
        Dim resp As String = ""
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 4 AND C_PARA_802 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", moti))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetValue(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Private Sub LLENAR_DW2(ByVal ALMACEN As String, ByVal DATE1 As Date, ByVal DATE2 As Date)
        ' Dim moti As Integer
        Me.DataGridView1.Rows.Clear()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("SELECT T_REMI_104.F_ALTA_104, M_MATE_002.DESC_002, T_REMI_104.CANT_104, T_REMI_104.MOTI_104,  M_MATE_002.UNID_002 FROM M_MATE_002 INNER JOIN T_REMI_104 ON M_MATE_002.CMATE_002 = T_REMI_104.C_MATE_104 WHERE (M_MATE_002.TIPO_002 = 3) AND (T_REMI_104.F_ALTA_104 between @D1 AND @D2) AND (T_REMI_104.ALMAR_104 = @D3) order by T_REMI_104.F_ALTA_104", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", DATE1))
        Comando.Parameters.Add(New SqlParameter("D2", DATE2))
        Comando.Parameters.Add(New SqlParameter("D3", ALMACEN))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        While Dusrs.Read
            ' moti = Dusrs.GetInt32(3)
            Me.DataGridView1.Rows.Add(CDate(Dusrs.GetValue(0)).ToShortDateString, Dusrs.GetString(1), Dusrs.GetString(4), Dusrs.GetDouble(2), Motivo(Dusrs.GetInt32(3)))
        End While
        If Me.DataGridView1.Rows.Count = 0 Then
            MENSAJE.MERRO011()
        End If
        cnn1.Close()
    End Sub

    Private Sub CB_Equipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Equipo.SelectedIndexChanged

    End Sub
End Class