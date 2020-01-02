Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA100
    Dim codigo_provincia As Integer
    Dim cod_partido As Integer
    Dim cod_localidad As Integer
    Dim cod_deposito As String
    Dim nombre As String
    Private mensaje As New Clase_mensaje



   

    Private Sub PALMA100_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        TBNOMBRE.Visible = False
        'LLENO COMBOBOX CON LOS DEPOSITOS CARGADOS
        llenar_DS_DEPOSITO1()
        cargar_provincias()
        borrar()
    End Sub

    

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text <> Nothing And ComboBox1.ValueMember <> Nothing Then
            cod_deposito = ComboBox1.SelectedValue
            nombre = ComboBox1.Text
            llenar_undeposito()
            BTnuevo.Enabled = False
        End If
    End Sub

    Private Sub cblocalidad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cblocalidad.SelectedIndexChanged
        If cblocalidad.Text <> Nothing And cblocalidad.ValueMember <> Nothing Then
            cod_localidad = cblocalidad.SelectedValue
        End If
    End Sub

    Private Sub cbProvincia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProvincia.SelectedIndexChanged
        If cbProvincia.ValueMember <> Nothing And cbProvincia.Text <> Nothing Then
            cargar_partidos(CInt(cbProvincia.SelectedValue))

        End If
    End Sub
    Private Sub cbPartido_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPartido.SelectedIndexChanged
        If cbPartido.ValueMember <> Nothing And cbPartido.Text <> Nothing Then
            Cargar_localidades(CInt(cbPartido.SelectedValue))

        End If
    End Sub

    Private Sub borrar()
        TBNOMBRE.Text = Nothing
        TBNOMBRE.Visible = False
        ComboBox1.Text = Nothing
        ComboBox1.Visible = True
        TBCOD.Text = Nothing
        TBDIRECCION.Text = Nothing
        TBDIRECCION.Enabled = False
        TBALTA.Text = Nothing
        TBBAJA.Text = Nothing
        cbPartido.Text = Nothing
        cbProvincia.Text = Nothing
        cblocalidad.Text = Nothing
        cbPartido.Enabled = False
        cbProvincia.Enabled = False
        cblocalidad.Enabled = False
        BTnuevo.Enabled = True
        BTALTA.Enabled = False
        BTALTA.Text = "Dar de Alta"
        BTMODIFICAR.Enabled = False
        BTBAJA.Enabled = False
        TBNOMBRE.Visible = False
    End Sub


#Region "botones"
    'boton de alta
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTALTA.Click
        If TBALTA.Text = Nothing Then
            If (TBDIRECCION.Text = Nothing Or TBNOMBRE.Text = Nothing Or cbPartido.Text = Nothing Or cblocalidad.Text = Nothing Or cbProvincia.Text = Nothing) Then
                mensaje.MERRO008()
            Else
                ' verifico que el nombre no exista
                If verificar_nombre() = True Then
                    codigo_provincia = cbProvincia.SelectedValue
                    cod_localidad = cblocalidad.SelectedValue
                    cod_partido = cbPartido.SelectedValue
                    'GRABO EN LA BASE EL NUEVO DEPOSITO
                    alta()
                End If
            End If
        Else
            'VERIFICO QUE NO EXISTAN CAMPOS VACIOS
            reactivar()
        End If
    End Sub
    'boton de baja
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTBAJA.Click
        'ACTUALIZO LOS CAMBIOS
        Dim res As DialogResult
        res = MessageBox.Show("¿Está seguro que desea dar de baja el deposito seleccionado?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If res = System.Windows.Forms.DialogResult.Yes Then
            baja()
        End If
    End Sub
    'boton de borrar
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBorrar.Click
        borrar()
    End Sub
    'boton de modificar
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTMODIFICAR.Click
        If (TBDIRECCION.Text = Nothing Or TBNOMBRE.Text = Nothing Or cbPartido.Text = Nothing Or cblocalidad.Text = Nothing Or cbProvincia.Text = Nothing) Then
            mensaje.MERRO008()
        Else
            codigo_provincia = cbProvincia.SelectedValue
            cod_localidad = cblocalidad.SelectedValue
            cod_partido = cbPartido.SelectedValue
            If TBNOMBRE.Text <> nombre Then
                If verificar_nombre() = True Then
                    modificar()
                End If
            Else
                modificar()
            End If
            
        End If
    End Sub
    'boton nuevo
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTnuevo.Click
        BTnuevo.Enabled = False
        TBNOMBRE.Visible = True
        TBNOMBRE.Enabled = True
        ComboBox1.Visible = False
        TBDIRECCION.Enabled = True
        cbPartido.Enabled = True
        cbProvincia.Enabled = True
        cblocalidad.Enabled = True
        BTALTA.Enabled = True
        'TRAIGO EL CODIGO DE DEPOSITO
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim consulta As New SqlClient.SqlCommand(" SELECT MAX (NDOC_003) FROM M_PERS_003 WHERE DEPO_003 = 1 ", cnn1)
        consulta.ExecuteNonQuery()
        Dim deposito As SqlDataReader = consulta.ExecuteReader()
        If (deposito.Read()) Then
            cod_deposito = Convert.ToInt32(deposito.GetString(0)) + 1
        End If
        cnn1.Close()
        TBCOD.Text = Convert.ToString(cod_deposito)
    End Sub
#End Region

#Region "funciones "
    Private Sub reactivar()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PERS_003 SET F_BAJA_003 = NULL , F_MODI_003 = @C1  WHERE NDOC_003 = @C2 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C2", TBCOD.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            llenar_DS_DEPOSITO1()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub alta()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("INSERT INTO M_PERS_003 (NDOC_003 , NOMB_003, APELL_003, DOMI_003, PROV_003, PART_003, LOCAL_003,F_ALTA_003, USRS_003, DEPO_003, ALMA_003) VALUES (@C0, @C1, 'DEPOSITO', @C2, @C3, @C4, @C5, @C6, @C7, @C8,0) ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C0", TBCOD.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", TBNOMBRE.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", TBDIRECCION.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", codigo_provincia))
            consulta7.Parameters.Add(New SqlParameter("C4", cod_partido))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_localidad))
            consulta7.Parameters.Add(New SqlParameter("C6", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C7", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C8", "1"))
            consulta7.ExecuteNonQuery()
            mensaje.MADVE001()
            llenar_DS_DEPOSITO1()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub llenar_DS_DEPOSITO1()
        ComboBox1.DataSource = Nothing
        Dim ds_deposito1 As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 order by NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(ds_deposito1, "M_PERS_003")
        cnn2.Close()
        ComboBox1.DataSource = ds_deposito1.Tables("M_PERS_003")
        ComboBox1.DisplayMember = "NOMB_003"
        ComboBox1.ValueMember = "NDOC_003"
        ComboBox1.Text = Nothing
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
        cbPartido.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        cbPartido.DisplayMember = "DESC_802"
        cbPartido.ValueMember = "C_PARA_802"
        cbPartido.Text = Nothing
    End Sub
    Private Sub Cargar_localidades(ByVal a As Integer)
        Dim ds_deposito As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("select C_PARA_802, DESC_802 from DET_PARAMETRO_802 where (C_TABLA_802 = 8) and (CTVINC_802 = 7) and (CODVINC_802 = @C1) and (F_BAJA_802 IS NULL) ORDER BY DESC_802", cnn2)
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("C1", a))        '
        adaptadaor.SelectCommand.ExecuteNonQuery()
        adaptadaor.Fill(ds_deposito, "DET_PARAMETRO_802")
        cnn2.Close()
        cblocalidad.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        cblocalidad.DisplayMember = "DESC_802"
        cblocalidad.ValueMember = "C_PARA_802"
        cblocalidad.Text = Nothing
    End Sub
    Private Sub llenar_undeposito()
        Try
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            Dim comando As New SqlClient.SqlCommand("SELECT NOMB_003, DOMI_003, LOCAL_003, PROV_003, PART_003, F_ALTA_003, F_BAJA_003 FROM M_PERS_003 WHERE NDOC_003=@C1", cnn4)
            comando.Parameters.Add(New SqlParameter("C1", cod_deposito))
            comando.ExecuteNonQuery()
            Dim lector As SqlDataReader = comando.ExecuteReader()
            If lector.Read() Then
                TBNOMBRE.Text = lector.GetValue(0)
                TBNOMBRE.Visible = True
                ComboBox1.Visible = False
                TBCOD.Text = cod_deposito
                TBDIRECCION.Text = lector.GetValue(1)
                cod_localidad = lector.GetInt32(2)
                codigo_provincia = lector.GetInt32(3)
                cod_partido = lector.GetInt32(4)
                cbProvincia.SelectedValue = codigo_provincia
                cbPartido.SelectedValue = cod_partido
                cblocalidad.SelectedValue = cod_localidad
                TBALTA.Text = lector.GetDateTime(5)
                If IsDBNull(lector.GetValue(6)) Then
                    TBBAJA.Text = Nothing
                    TBDIRECCION.Enabled = True
                    cblocalidad.Enabled = True
                    cbPartido.Enabled = True
                    cbProvincia.Enabled = True
                    BTALTA.Enabled = False
                    BTMODIFICAR.Enabled = True
                    BTBAJA.Enabled = True
                Else
                    TBBAJA.Text = lector.GetDateTime(6)
                    BTALTA.Text = "Reactivar"
                    BTALTA.Enabled = True
                End If
            End If
            cnn4.Close()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try

    End Sub
    Private Function verificar_nombre() As Boolean
        Dim resp As Boolean = True
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim consulta As New SqlClient.SqlCommand(" SELECT NOMB_003 FROM M_PERS_003 WHERE NOMB_003=@C1 ", cnn1)
        consulta.Parameters.Add(New SqlParameter("C1", TBNOMBRE.Text))
        consulta.ExecuteNonQuery()
        Dim DEPO As SqlDataReader = consulta.ExecuteReader()
        If DEPO.Read() Then
            mensaje.MERRO027()
            resp = False
        End If
        Return resp
    End Function
    Private Sub modificar()
        Try
            'ACTUALIZO LOS CAMBIOS
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PERS_003 SET NOMB_003 = @C1, DOMI_003= @C2, PROV_003=@C3, PART_003= @C4, LOCAL_003 = @C5 ,F_MODI_003 = @C6  WHERE NDOC_003 = @C7 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", TBNOMBRE.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", TBDIRECCION.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", codigo_provincia))
            consulta7.Parameters.Add(New SqlParameter("C4", cod_partido))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_localidad))
            consulta7.Parameters.Add(New SqlParameter("C6", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C7", TBCOD.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
            'llenar_DS_DEPOSITO1()
        Catch ex As Exception
            mensaje.MERRO001()

        End Try
    End Sub
    Private Sub baja()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PERS_003 SET F_BAJA_003 = @C1  WHERE NDOC_003 = @C8 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C8", TBCOD.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
#End Region





   
  
End Class