Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PCIUD001
    Dim codigo_provincia As Integer
    Dim cod_partido As String
    Dim desc_partido As String
    Dim habilitar As Integer
    Dim mensaje As New Clase_mensaje
    Dim cargar As Integer


    
    Private Sub PCIUD001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        cargar_provincias()
    End Sub


    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBprovincia.SelectedIndexChanged
        If CBprovincia.ValueMember <> Nothing And CBprovincia.Text <> Nothing Then
            codigo_provincia = CBprovincia.SelectedValue
            cargar_partidos(codigo_provincia)
        End If
    End Sub

    Private Sub ComboBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBprovincia.Click
        'BORRO DATAGRIDVIEW
        DataGridView1.Rows.Clear()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'ME PARO SOLO LA FILA A SELECCIONAR
        If (DataGridView1.Rows.Count <> 0) Then

            desc_partido = DataGridView1.Item(1, DataGridView1.CurrentCell.RowIndex()).Value
            cod_partido = DataGridView1.Item(0, DataGridView1.CurrentCell.RowIndex()).Value
            DataGridView1.Enabled = False
            TextBox1.Text = desc_partido
            Button4.Enabled = False
            Button2.Enabled = True
            CBprovincia.Enabled = False
            Button3.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Verificar_nom_partido(TextBox1.Text) = False Then
            modificar()
        End If

    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text <> Nothing Then
            If Verificar_nom_partido(TextBox1.Text) = False Then
                cod_partido = Nuevo_cod_partido()
                Dim cnn2 As SqlConnection = New SqlConnection(conexion)
                cnn2.Open()
                Dim alta_partidos As New SqlClient.SqlCommand("INSERT INTO DET_PARAMETRO_802 (C_TABLA_802, C_PARA_802, DESC_802, CTVINC_802, CODVINC_802) VALUES( 7, @C2, @C1, 6, @C3 ) ", cnn2)
                alta_partidos.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
                alta_partidos.Parameters.Add(New SqlParameter("C3", codigo_provincia))
                alta_partidos.Parameters.Add(New SqlParameter("C2", cod_partido))
                alta_partidos.ExecuteNonQuery()
                cnn2.Close()
                TextBox1.Text = Nothing
                CBprovincia.Enabled = True
                cargar_partidos(codigo_provincia)
            End If
        End If
    End Sub
#Region "FUNCIONES"
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
        CBprovincia.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        CBprovincia.DisplayMember = "DESC_802"
        CBprovincia.ValueMember = "C_PARA_802"
        CBprovincia.Text = Nothing
    End Sub


    Private Sub cargar_partidos(ByVal a As Integer)
        'CONECTO LA BASE
        DataGridView1.Rows.Clear()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlCommand("select C_PARA_802, DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 =7)  and (CODVINC_802 = @C1) and (CTVINC_802 = 6) and (F_BAJA_802 IS NULL) ORDER BY DESC_802", cnn2)
        adaptadaor.Parameters.Add(New SqlParameter("C1", a))        '
        adaptadaor.ExecuteNonQuery()
        Dim lector As SqlDataReader = adaptadaor.ExecuteReader
        Do While lector.Read
            DataGridView1.Rows.Add(lector.GetValue(0), lector.GetValue(1))
        Loop
        cnn2.Close()
        If (DataGridView1.Rows.Count <> 0) Then
            Button4.Enabled = True
            Button3.Enabled = True
        Else
            Button3.Enabled = True
        End If
    End Sub
    Private Sub modificar()
        'ACTUALIZO EL PARTIDO MODIFICADO
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn2.Open()
            Dim actualizar_partidos As New SqlClient.SqlCommand("UPDATE DET_PARAMETRO_802 SET DESC_802 = @C1 where (C_TABLA_802 =7)  and (C_PARA_802 = @C2) and (CTVINC_802 = 6)  ", cnn2)
            actualizar_partidos.Parameters.Add(New SqlParameter("C1", TextBox1.Text))
            actualizar_partidos.Parameters.Add(New SqlParameter("C2", cod_partido))
            actualizar_partidos.ExecuteNonQuery()
            cnn2.Close()
            TextBox1.Text = Nothing
            CBprovincia.Enabled = True
            DataGridView1.Rows.Clear()
            DataGridView1.Enabled = True
            Button4.Enabled = True
            Button2.Enabled = False
            cargar_partidos(codigo_provincia)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


    End Sub
    Private Function Verificar_nom_partido(ByVal nom As String) As Boolean
        Dim resp As Boolean = False
        Dim cnn3 As SqlConnection = New SqlConnection(conexion)
        cnn3.Open()
        Dim consulta3 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE (C_TABLA_802 = 7) AND (DESC_802=@C1)  ", cnn3)
        consulta3.Parameters.Add(New SqlParameter("C1", nom))
        consulta3.ExecuteNonQuery()
        Dim d_partidos As SqlDataReader = consulta3.ExecuteReader()
        If (d_partidos.Read()) Then
            resp = True
            mensaje.MERRO028()
        End If
        Return resp
    End Function
    Private Function Nuevo_cod_partido() As Integer
        Dim resp As Integer
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim consulta As New SqlClient.SqlCommand("SELECT MAX (C_PARA_802) FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 7", cnn1)
        consulta.ExecuteNonQuery()
        Dim codigo_part As SqlDataReader = consulta.ExecuteReader()
        If (codigo_part.Read()) Then
            resp = codigo_part.GetInt32(0) + 1
        End If
        cnn1.Close()
        Return resp
    End Function
#End Region
End Class