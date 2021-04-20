Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA007
    Private DS_almacen As New DataSet
    Private MENSAJE As New Clase_mensaje
    Private Sub PALMA007_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        Desde.Value = Date.Now.ToShortDateString
        Hasta.Value = Date.Now.ToShortDateString
        llenar_DS_ALMACEN()
        LLENAR_CB_ALMACEN()
        Desde.Value = Date.Now.ToShortDateString
        Hasta.Value = Date.Now.ToShortDateString
    End Sub
    '##############FUNCIONES###########################FUNCIONES#####################################
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
    Private Sub LLENAR_CB_ALMACEN()
        CB_Equipo.DataSource = DS_almacen.Tables("M_PERS_003")
        CB_Equipo.DisplayMember = "NOMBRE"
        CB_Equipo.ValueMember = "NDOC_003"
        CB_Equipo.Text = Nothing
    End Sub
    Private Sub LLENAR_DW1(ByVal ALMACEN As String, ByVal DATE1 As Date, ByVal DATE2 As Date)
        Me.DataGridView1.Rows.Clear()

        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("SELECT T_REMI_104.F_ALTA_104, M_MATE_002.DESC_002, T_REMI_104.CANT_104, M_MATE_002.UNID_002 FROM M_MATE_002 INNER JOIN T_REMI_104 ON dbo.M_MATE_002.CMATE_002 = T_REMI_104.C_MATE_104 WHERE (M_MATE_002.TIPO_002 = 2) AND (T_REMI_104.F_ALTA_104 between @D1 AND @D2) AND (T_REMI_104.ALMAR_104 = @D3)", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", DATE1))
        Comando.Parameters.Add(New SqlParameter("D2", DATE2))
        Comando.Parameters.Add(New SqlParameter("D3", ALMACEN))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        While Dusrs.Read
            DataGridView1.Rows.Add(CDate(Dusrs.GetValue(0)).ToShortDateString, Dusrs.GetString(1), Dusrs.GetString(3), Dusrs.GetDouble(2))
        End While
        If Me.DataGridView1.Rows.Count = 0 Then
            MENSAJE.MERRO011()
       
        End If
        cnn1.Close()
    End Sub
    '###########BOTONES#################BOTONES#####################################BOTONES###########3
    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Fdesde As Date = Desde.Value
        Dim Fhasta As Date = Hasta.Value
        Fdesde = Fdesde.ToShortDateString & " 0:00:00"
        Fhasta = Fhasta.ToShortDateString & " 23:59:59"
        LLENAR_DW1(CB_Equipo.SelectedValue, Fdesde.ToString, Fhasta.ToString)
    End Sub
  
    Private Sub CB_Equipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_Equipo.SelectedIndexChanged
        If CB_Equipo.ValueMember <> Nothing Then
            If CB_Equipo.Text <> Nothing Then
                Button1.Enabled = True
            End If
        End If
    End Sub

    
End Class