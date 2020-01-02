Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO

Public Class PCONS001

    Private Sub PCONS001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_CB_proveedor()
        CB_PROVEEDOR.Focus()
    End Sub

    Private Sub llenar_CB_proveedor()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT CUIT_005,RAZO_005 FROM M_PROV_005 where F_BAJA_005 is NULL and SPETI_005 = 0 order by RAZO_005", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_PROV_005")
        cnn2.Close()
        CB_PROVEEDOR.DataSource = DS_contrato.Tables("M_PROV_005")
        CB_PROVEEDOR.DisplayMember = "RAZO_005"
        CB_PROVEEDOR.ValueMember = "CUIT_005"
        CB_PROVEEDOR.Text = Nothing
    End Sub

    Private Function LOCALIDAD(ByVal str As Integer) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 8 AND C_PARA_802 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Private Function PARTIDO(ByVal str As Integer) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 7 AND C_PARA_802 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function
    Private Function PROVINCIA(ByVal str As Integer) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 6 AND C_PARA_802 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function

    Private Sub LLENAR_DATOS(ByVal DATO As String)
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select DIRE_005, LOCA_005,PROV_005,PART_005,CONT_005, TELE_005, F_ALTA_005,F_BAJA_005 FROM M_PROV_005 WHERE CUIT_005 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", DATO))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            'resp = Dusrs.GetString(0)
            TextBox2.Text = Dusrs.GetValue(0)
            TextBox4.Text = LOCALIDAD(Dusrs.GetInt32(1))
            TextBox5.Text = Dusrs.GetValue(4)
            TextBox7.Text = Dusrs.GetValue(5)
            TextBox8.Text = Dusrs.GetDateTime(6).ToShortDateString
            If IsDBNull(Dusrs.GetValue(7)) = True Then
                TextBox9.Text = ""
            Else
                TextBox9.Text = Dusrs.GetDateTime(7).ToShortDateString
            End If
            TextBox1.Text = PROVINCIA(Dusrs.GetInt32(2))
            TextBox3.Text = PARTIDO(Dusrs.GetInt32(3))

        End If
        cnn1.Close()
    End Sub
    
    Private Sub CB_PROVEEDOR_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_PROVEEDOR.SelectedIndexChanged
        If CB_PROVEEDOR.ValueMember <> Nothing And CB_PROVEEDOR.Text <> Nothing Then
            TextBox6.Text = CB_PROVEEDOR.SelectedValue
            LLENAR_DATOS(CB_PROVEEDOR.SelectedValue)

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LLENAR_DATOS(CB_PROVEEDOR.SelectedValue)
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub
End Class