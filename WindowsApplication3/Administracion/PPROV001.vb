Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class PPROV001
    Dim CUIT As Decimal
    Dim alta As Integer
    Dim d_localidad As Integer
    Dim d_partido As Integer
    Dim d_provincia As Integer
    Dim codigo_provincia As Integer
    Dim aux_carga As Integer
    Dim cod_partido As Integer
    Dim cod_localidad As Integer
    Dim peti As Integer
    Private mensaje As New Clase_mensaje
    Private UnProveedor As New Class_UnProveedor


   

   

    Private Sub PPROV001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        'CARGO LAS PROVINCIAS
        cargar_provincias()
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProvincia.SelectedIndexChanged
        If cbProvincia.ValueMember <> Nothing And cbProvincia.Text <> Nothing Then
            cargar_partidos(CInt(cbProvincia.SelectedValue))
            codigo_provincia = cbProvincia.SelectedValue
        End If
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPartido.SelectedIndexChanged
        If cbPartido.ValueMember <> Nothing And cbPartido.Text <> Nothing Then
            Cargar_localidades(CInt(cbPartido.SelectedValue))
            cod_partido = cbPartido.SelectedValue
        End If
    End Sub
    Private Sub cbLocalidad_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLocalidad.SelectedIndexChanged
        If cbLocalidad.Text <> Nothing And cbLocalidad.ValueMember <> Nothing Then
            cod_localidad = cbLocalidad.SelectedValue
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
        cbLocalidad.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        cbLocalidad.DisplayMember = "DESC_802"
        cbLocalidad.ValueMember = "C_PARA_802"
        cbLocalidad.Text = Nothing
    End Sub
    Private Sub borrar()
        btalta.Text = "Dar de Alta"
        btverificar.Enabled = True
        tbcuit.Text = Nothing
        tbdireccion.Text = Nothing
        tbcontacto.Text = Nothing
        tbalta.Text = Nothing
        tbtelefono.Text = Nothing
        tbrazon.Text = Nothing
        tbbaja.Text = Nothing
        cbLocalidad.Text = Nothing
        cbPartido.Text = Nothing
        cbProvincia.Text = Nothing
        tbcuit.Enabled = True
        tbdireccion.Enabled = False
        tbcontacto.Enabled = False
        tbalta.Enabled = False
        tbtelefono.Enabled = False
        tbrazon.Enabled = False
        tbbaja.Enabled = False
        btverificar.Enabled = True
        btalta.Enabled = False
        btmodificar.Enabled = False
        btbaja.Enabled = False
        cbLocalidad.Enabled = False
        cbPartido.Enabled = False
        cbProvincia.Enabled = False
        ComboBox4.Text = Nothing
        ComboBox4.Enabled = False
        btalta.Text = "Dar de Alta"
    End Sub
    Private Sub llenar_unuser()

        If UnProveedor.OBT_FBAJA = Nothing Then
            tbbaja.Text = Nothing
            btbaja.Enabled = True
            btmodificar.Enabled = True
            tbalta.Enabled = False

            tbrazon.Enabled = True
            tbcontacto.Enabled = True
            tbtelefono.Enabled = True
            tbdireccion.Enabled = True
            cbProvincia.Enabled = True
            cbLocalidad.Enabled = True
            cbPartido.Enabled = True
            ComboBox4.Enabled = True
            btmodificar.Enabled = True

        Else
            tbbaja.Text = UnProveedor.OBT_FBAJA
            btalta.Text = "Reactivar"
            btalta.Enabled = True
            btbaja.Enabled = False
            btmodificar.Enabled = False

        End If
        tbrazon.Text = UnProveedor.OBT_RAZON
        tbdireccion.Text = UnProveedor.OBT_DIRECCION
        tbcontacto.Text = UnProveedor.OBT_CONTACTO
        tbtelefono.Text = UnProveedor.OBT_TELEFONO
        cbProvincia.SelectedValue = UnProveedor.OBT_PROVINCIA
        cbPartido.SelectedValue = UnProveedor.OBT_PARTIDO
        cbLocalidad.SelectedValue = UnProveedor.OBT_LOCALIDAD
        tbalta.Text = UnProveedor.OBT_FALTA


        If UnProveedor.OBT_PETICION = 1 Then
            ComboBox4.Text = "SI"
        Else
            ComboBox4.Text = "NO"
        End If
    End Sub
    Private Sub Alta_un_proveedor()
        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("INSERT INTO M_PROV_005 (CUIT_005, RAZO_005, DIRE_005, PROV_005, PART_005, LOCA_005, CONT_005, TELE_005, F_ALTA_005, USRS_005,SPETI_005) VALUES (@C0, @C1, @C2, @C3, @C4, @C5, @C6, @C7, @C8, @C9, @C10) ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C0", tbcuit.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", tbrazon.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", tbdireccion.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", codigo_provincia))
            consulta7.Parameters.Add(New SqlParameter("C4", cod_partido))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_localidad))
            consulta7.Parameters.Add(New SqlParameter("C6", tbcontacto.Text))
            consulta7.Parameters.Add(New SqlParameter("C7", tbtelefono.Text))
            consulta7.Parameters.Add(New SqlParameter("C8", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C9", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C10", peti))
            consulta7.ExecuteNonQuery()
            mensaje.MADVE001()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        Finally
            cnn7.Close()

        End Try

    End Sub
    Private Sub Reactivar_unProveedor()
        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PROV_005 SET F_BAJA_005 = NULL , F_MODI_005 = @C1, USRS_005=@C2 WHERE CUIT_005 = @C3", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C2", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C3", tbcuit.Text))
            consulta7.ExecuteNonQuery()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        Finally
            cnn7.Close()
        End Try
    End Sub
    Private Sub baja()
        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PROV_005 SET F_BAJA_005 = @C1  WHERE CUIT_005 = @C8 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C8", tbcuit.Text))
            consulta7.ExecuteNonQuery()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        Finally
            cnn7.Close()
        End Try
    End Sub
    Private Sub modificar()
        Dim cnn7 As SqlConnection = New SqlConnection(conexion)
        Try
            'ACTUALIZO LOS CAMBIOS
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PROV_005 SET RAZO_005 = @C1, DIRE_005=@C2 ,PROV_005=@C3, PART_005= @C4, LOCA_005 = @C5, CONT_005=@C6, TELE_005 = @C7, F_MODI_005 = @C8, SPETI_005=@C10  WHERE CUIT_005 = @C9 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", tbrazon.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", tbdireccion.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", codigo_provincia))
            consulta7.Parameters.Add(New SqlParameter("C4", cod_partido))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_localidad))
            consulta7.Parameters.Add(New SqlParameter("C6", tbcontacto.Text))
            consulta7.Parameters.Add(New SqlParameter("C7", tbtelefono.Text))
            consulta7.Parameters.Add(New SqlParameter("C8", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C9", tbcuit.Text))
            consulta7.Parameters.Add(New SqlParameter("C10", peti))
            consulta7.ExecuteNonQuery()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        Finally
            cnn7.Close()
        End Try
    End Sub

#End Region

#Region "BOTONES"
    'boton de verificacion 
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btverificar.Click
        If tbcuit.Text <> Nothing Then
            If tbcuit.Text.Length = 11 And IsNumeric(tbcuit.Text) Then
                btverificar.Enabled = False
                If UnProveedor.Existe_proveedor(tbcuit.Text) Then
                    llenar_unuser()
                    tbcuit.Enabled = False
                Else
                    Dim res As DialogResult
                    res = MessageBox.Show("Usuario Inexistente, ¿Desea cargar nuevo usuario?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                    If res = System.Windows.Forms.DialogResult.Yes Then
                        'HABILITO LOS BOTONES
                        tbcuit.Enabled = False
                        tbrazon.Enabled = True
                        tbdireccion.Enabled = True
                        tbcontacto.Enabled = True
                        tbtelefono.Enabled = True
                        cbProvincia.Enabled = True
                        cbLocalidad.Enabled = True
                        cbPartido.Enabled = True
                        ComboBox4.Enabled = True
                        btmodificar.Enabled = False
                        btbaja.Enabled = False
                        btalta.Enabled = True
                    Else
                        btverificar.Enabled = True
                        tbcuit.Focus()
                    End If
                End If
            Else
                mensaje.MERRO020()
            End If
        Else
            mensaje.MERRO026()
            tbcuit.Focus()
        End If
    End Sub
    'boton de alta o rectivacion
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btalta.Click
        If tbalta.Text = Nothing Then
            'VERIFICO QUE NO EXISTAN CAMPOS VACIOS
            If (tbrazon.Text = Nothing Or tbdireccion.Text = Nothing Or tbcontacto.Text = Nothing Or tbtelefono.Text = Nothing Or cbLocalidad.Text = Nothing Or cbPartido.Text = Nothing Or cbProvincia.Text = Nothing Or ComboBox4.Text = Nothing) Then
                mensaje.MERRO008()
            Else
                'GRABO EN LA BASE EL NUEVO CLIENTE
                If ComboBox4.Text = "SI" Then
                    peti = 1
                Else
                    peti = 0
                End If
                Alta_un_proveedor()

            End If
        Else
            Reactivar_unProveedor()
        End If
    End Sub
    'boton de borrar
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btbaja.Click
        'ACTUALIZO LOS CAMBIOS
        Dim res As DialogResult
        res = MessageBox.Show("¿Está seguro que desea dar de baja el proveedor seleccionado?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If res = System.Windows.Forms.DialogResult.Yes Then
            baja()
        Else
            borrar()
        End If
    End Sub
    'boton modificar
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmodificar.Click
        'ANALIZO PETICIONES
        If cbLocalidad.Text = Nothing Or cbPartido.Text = Nothing Or cbProvincia.Text = Nothing Or tbcuit.Text = Nothing Or tbdireccion.Text = Nothing Or tbtelefono.Text = Nothing Or cbPartido.Text = Nothing Or cbProvincia.Text = Nothing Or ComboBox4.Text = Nothing Then
            mensaje.MERRO008()
        Else
            If ComboBox4.Text = "SI" Then
                peti = 1
            End If
            If ComboBox4.Text = "NO" Then
                peti = 0
            End If
            modificar()
        End If
    End Sub
    'boton borrar
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        borrar()
    End Sub
#End Region

    Private Sub tbcuit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcuit.DoubleClick
        Dim PANTALLA As New PPROV003
        PANTALLA.ShowDialog()
        If PANTALLA.dato = True Then
            tbcuit.Text = PANTALLA.dniobtenido
            UnProveedor.Existe_proveedor(tbcuit.Text)
            btverificar.Enabled = False
            tbcuit.Enabled = False
            llenar_unuser()
        End If
    End Sub
End Class