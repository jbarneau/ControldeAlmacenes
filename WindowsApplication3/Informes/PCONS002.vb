Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO

Public Class PCONS002

    Private Sub PCONS002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_DS_ALMACEN()
        CB_PROVEEDOR.Focus()
    End Sub


    Private Sub llenar_DS_ALMACEN()
        Dim DS_almacen As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE DEPO_003 = 0 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_almacen, "M_PERS_003")
        cnn2.Close()
        CB_PROVEEDOR.DataSource = DS_almacen.Tables("M_PERS_003")
        CB_PROVEEDOR.DisplayMember = "NOMBRE"
        CB_PROVEEDOR.ValueMember = "NDOC_003"
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
        Dim Comando As New SqlClient.SqlCommand("select DOMI_003, LOCAL_003,PROV_003, PART_003, F_ALTA_003, F_BAJA_003 FROM M_PERS_003 WHERE NDOC_003 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", DATO))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            'resp = Dusrs.GetString(0)
            TextBox2.Text = Dusrs.GetValue(0)
            TextBox4.Text = LOCALIDAD(Dusrs.GetInt32(1))
            TextBox8.Text = Dusrs.GetDateTime(4).ToShortDateString
            If IsDBNull(Dusrs.GetValue(5)) = True Then
                TextBox9.Text = ""
            Else
                TextBox9.Text = Dusrs.GetDateTime(5).ToShortDateString
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

   
    Private Sub Label8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label8.Click

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class