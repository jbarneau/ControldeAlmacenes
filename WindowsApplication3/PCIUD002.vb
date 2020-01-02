
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class PCIUD002
    Dim codigo_provincia As Integer
    Dim cod_partido As Integer
    Dim descripcion_localidad As String
    Dim codigo_para802 As Integer
    Dim habilitar As Integer
    Dim mensaje As New Clase_mensaje
   

    Private Sub PCIUD002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        cargar_provincias()

       
        var_partido = Nothing
        var_provincia = Nothing
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbpartido.SelectedIndexChanged
        Button1.Enabled = False
        Button4.Enabled = False
        DataGridView1.Rows.Clear()
        If cbpartido.ValueMember <> Nothing And cbpartido.Text <> Nothing Then
            cod_partido = cbpartido.SelectedValue
        End If
        'LLENO LA TABLA
        traer_localidades(cod_partido)
        Dim llenar_tablas As String
        For Each llenar_tablas In localidades_obtenidas
            DataGridView1.Rows.Add(llenar_tablas)
        Next
        If DataGridView1.Rows.Count > 0 Then
            Button1.Enabled = True
            Button4.Enabled = True

        Else
            Button3.Enabled = True
        End If


    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbprovincia.SelectedIndexChanged
        If cbprovincia.ValueMember <> Nothing And cbprovincia.Text <> Nothing Then
            cargar_partidos(CInt(cbprovincia.SelectedValue))
            codigo_provincia = cbprovincia.SelectedValue
            DataGridView1.Rows.Clear()
        End If
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'ME PARO SOLO LA FILA A SELECCIONAR
        If (DataGridView1.Rows.Count > 0) Then
            'OBTENGO EL NUMERO DE FILA
            Dim desc_partido As Integer = DataGridView1.CurrentCell.RowIndex()
            'CON EL NUMERO PASO LA DESCRIPCION
            TextBox1.Text = DataGridView1.Item(0, desc_partido).Value
            descripcion_localidad = TextBox1.Text
        End If

        Button1.Enabled = False
        Button2.Enabled = True
        Button3.Enabled = False
        Button4.Enabled = False
        cbprovincia.Enabled = False
        cbpartido.Enabled = False
        habilitar = 1

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'ACTUALIZO LOCALIDAD MODIFICADA
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim actualizar_partidos As New SqlClient.SqlCommand("UPDATE DET_PARAMETRO_802 SET DESC_802 = @C1 where (C_TABLA_802 =8)  and (DESC_802 = @C2) and (CTVINC_802 = 7)  ", cnn2)
        actualizar_partidos.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
        actualizar_partidos.Parameters.Add(New SqlParameter("C2", descripcion_localidad))
        actualizar_partidos.ExecuteNonQuery()
        cnn2.Close()
        TextBox1.Text = Nothing
        cbprovincia.Enabled = True
        DataGridView1.Rows.Clear()
        cbprovincia.Enabled = True
        cbpartido.Enabled = True
        Call ComboBox1_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text <> Nothing Then
            Dim cnn3 As SqlConnection = New SqlConnection(conexion)
            cnn3.Open()
            Dim consulta3 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802 = 8) AND (DESC_802=@C1)  ", cnn3)
            consulta3.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
            consulta3.ExecuteNonQuery()
            Dim d_partidos As SqlDataReader = consulta3.ExecuteReader()
            If (d_partidos.Read()) Then
                mensaje.MERRO019()
            Else
                'DEBO EL ULTIMO CPARA_802 DE TVINC_802 8 Y SUMARLE 1
                Dim cnn1 As SqlConnection = New SqlConnection(conexion)
                cnn1.Open()
                Dim consulta As New SqlClient.SqlCommand("SELECT MAX (C_PARA_802) FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 8", cnn1)
                consulta.ExecuteNonQuery()
                Dim codigo_part As SqlDataReader = consulta.ExecuteReader()
                If (codigo_part.Read()) Then
                    codigo_para802 = codigo_part.GetInt32(0) + 1
                End If
                cnn1.Close()

                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                Dim alta_partidos As New SqlClient.SqlCommand("INSERT INTO DET_PARAMETRO_802 (C_TABLA_802, C_PARA_802, DESC_802, CTVINC_802, CODVINC_802) VALUES( 8, @C2, @C1, 7, @C3 ) ", cnn2)
                alta_partidos.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
                alta_partidos.Parameters.Add(New SqlParameter("C3", cod_partido))
                alta_partidos.Parameters.Add(New SqlParameter("C2", codigo_para802))
                alta_partidos.ExecuteNonQuery()
                cnn2.Close()
                TextBox1.Text = Nothing
                cbprovincia.Enabled = True
                DataGridView1.Rows.Clear()
                habilitar = Nothing
                Call ComboBox1_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        If habilitar <> 1 Then
            Button1.Enabled = False
            Button4.Enabled = False
            Button3.Enabled = True
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (DataGridView1.Rows.Count > 0) Then
            'OBTENGO EL NUMERO DE FILA
            Dim desc_partido As Integer = DataGridView1.CurrentCell.RowIndex()
            'CON EL NUMERO PASO LA DESCRIPCION
            TextBox1.Text = DataGridView1.Item(0, desc_partido).Value
            descripcion_localidad = TextBox1.Text
        End If
        'DOY LA BAJA

        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim actualizar_partidos As New SqlClient.SqlCommand("UPDATE DET_PARAMETRO_802 SET F_BAJA_802 = @C1 where (C_TABLA_802 =8)  and (DESC_802 = @C2) and (CTVINC_802 = 7)  ", cnn2)
        actualizar_partidos.Parameters.Add(New SqlParameter("C1", DateTime.Now))
        actualizar_partidos.Parameters.Add(New SqlParameter("C2", descripcion_localidad))
        actualizar_partidos.ExecuteNonQuery()
        cnn2.Close()
        TextBox1.Text = Nothing
        cbprovincia.Enabled = True
        DataGridView1.Rows.Clear()
        cbprovincia.Enabled = True
        cbpartido.Enabled = True
        Call ComboBox1_SelectedIndexChanged(sender, e)

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim PANTALLA As New PCIUD003
        PANTALLA.ShowDialog()
    End Sub

#Region "funciones"
    Private Sub cargar_partidos(ByVal a As Integer)
        Dim ds_deposito As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("select C_PARA_802, DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 =7)  and (CODVINC_802 = @C1) and (CTVINC_802 = 6) and (F_BAJA_802 IS NULL) ORDER BY DESC_802", cnn2)
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("C1", a))        '
        adaptadaor.SelectCommand.ExecuteNonQuery()
        adaptadaor.Fill(ds_deposito, "DET_PARAMETRO_802")
        cnn2.Close()
        cbpartido.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        cbpartido.DisplayMember = "DESC_802"
        cbpartido.ValueMember = "C_PARA_802"
        cbpartido.Text = Nothing
    End Sub
    Private Sub cargar_provincias()
        Dim ds_deposito As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 where C_TABLA_802 = 6 order by DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(ds_deposito, "DET_PARAMETRO_802")
        cnn2.Close()
        cbProvincia.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        cbProvincia.DisplayMember = "DESC_802"
        cbProvincia.ValueMember = "C_PARA_802"
        cbProvincia.Text = Nothing
    End Sub
#End Region
End Class