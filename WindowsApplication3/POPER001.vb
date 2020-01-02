Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class POPER001
    Dim almacen As String
    Dim cod_provincia As Integer
    Dim cod_partido As Integer
    Dim cod_localidad As Integer
    Private UnOperario As New Class_UnOperario
    Private mensaje As New Clase_mensaje

    Private Sub POPER001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        cbEsalmacen.Items.Add("SI")
        cbEsalmacen.Items.Add("NO")
        'CARGO LAS PROVINCIAS
        cargar_provincias()
    End Sub
#Region "FUNCIONES"
    Private Sub llenar_un_operario()
        txtDNI.ReadOnly = True
        txtNombre.ReadOnly = False : txtNombre.Text = UnOperario.nombre
        txtApellido.ReadOnly = False : txtApellido.Text = UnOperario.apellido
        TxtLegajo.ReadOnly = False : TxtLegajo.Text = UnOperario.legajo
        txtDomicilio.ReadOnly = False : txtDomicilio.Text = UnOperario.direccion
        txtAlta.Text = UnOperario.alta
        If UnOperario.baja = Nothing Then
            btBaja.Enabled = True
            btModi.Enabled = True
            btAlta.Enabled = False
            txtBaja.Text = ""
        Else
            btBaja.Enabled = False
            btModi.Enabled = False
            btAlta.Enabled = True
            btAlta.Text = "Reactivar"
            txtBaja.Text = UnOperario.baja
        End If
        cbProvincia.SelectedValue = UnOperario.provincia
        cbPartido.SelectedValue = UnOperario.partido
        cbLocalidad.SelectedValue = UnOperario.localidad
        cbProvincia.Enabled = True
        cbPartido.Enabled = True
        cbLocalidad.Enabled = True
        If UnOperario.esalmacen = True Then
            cbEsalmacen.Text = "si"
        Else
            cbEsalmacen.Text = "NO"
        End If
        cbEsalmacen.Enabled = True
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
        cbLocalidad.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        cbLocalidad.DisplayMember = "DESC_802"
        cbLocalidad.ValueMember = "C_PARA_802"
        cbLocalidad.Text = Nothing
    End Sub
    Private Sub Nuevo_operario()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("INSERT INTO M_PERS_003 (NDOC_003 , NOMB_003, APELL_003, DOMI_003, PROV_003, PART_003, LOCAL_003, ALMA_003, F_ALTA_003, USRS_003, DEPO_003, LEGA_003) VALUES (@C0, @C1, @C2, @C3, @C4, @C5, @C6, @C7, @C8, @C9, @C10, @C11) ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C0", txtDNI.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", txtNombre.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", txtApellido.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", txtDomicilio.Text))
            consulta7.Parameters.Add(New SqlParameter("C4", cod_provincia))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_partido))
            consulta7.Parameters.Add(New SqlParameter("C6", cod_localidad))
            consulta7.Parameters.Add(New SqlParameter("C7", almacen))
            consulta7.Parameters.Add(New SqlParameter("C8", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C9", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C10", "0"))
            consulta7.Parameters.Add(New SqlParameter("C11", TxtLegajo.Text))
            consulta7.ExecuteNonQuery()
            borrar()
            mensaje.MADVE001()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub Modificar_operario()
        Try
            'ACTUALIZO LOS CAMBIOS
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PERS_003 SET NOMB_003 = @C1, APELL_003=@C2 , DOMI_003= @C3,PROV_003=@C4, PART_003= @C5, LOCAL_003 = @C6,ALMA_003=@C7, F_MODI_003 = @C8  WHERE NDOC_003 = @C9 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", txtNombre.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", txtApellido.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", txtDomicilio.Text))
            consulta7.Parameters.Add(New SqlParameter("C4", cod_provincia))
            consulta7.Parameters.Add(New SqlParameter("C5", cod_partido))
            consulta7.Parameters.Add(New SqlParameter("C6", cod_localidad))
            consulta7.Parameters.Add(New SqlParameter("C7", almacen))
            consulta7.Parameters.Add(New SqlParameter("C8", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C9", txtDNI.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()

        End Try
    End Sub
    Private Sub reactivar_operario()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PERS_003 SET F_BAJA_003 = NULL , F_MODI_003 = @C1  WHERE NDOC_003 = @C2 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C2", txtDNI.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub borrar()
        txtDNI.Text = Nothing
        txtNombre.Text = Nothing
        txtApellido.Text = Nothing
        TxtLegajo.Text = Nothing
        txtDomicilio.Text = Nothing
        txtBaja.Text = Nothing
        txtAlta.Text = Nothing
        cbLocalidad.Text = Nothing
        cbEsalmacen.Text = Nothing
        cbProvincia.Text = Nothing
        cbPartido.Text = Nothing
        txtDNI.ReadOnly = False
        txtNombre.ReadOnly = True
        txtApellido.ReadOnly = True
        TxtLegajo.ReadOnly = True
        txtDomicilio.ReadOnly = True
        txtAlta.ReadOnly = True
        txtBaja.ReadOnly = True
        btAlta.Text = "Dar de Alta"
        cbLocalidad.Enabled = False
        cbEsalmacen.Enabled = False
        cbProvincia.Enabled = False
        cbPartido.Enabled = False
        Button1.Enabled = True
        btAlta.Enabled = False
        btModi.Enabled = False
        btBaja.Enabled = False

    End Sub
    Private Function Verificar_NYA() As Boolean
        Dim resp As Boolean = False
        Try
            'VERIFICO QUE NO EXISTAN NOMBRES Y APELLIDOS CARGADOS IGUALES
            Dim cnn1 As SqlConnection = New SqlConnection(conexion)
            cnn1.Open()
            Dim consulta1 As New SqlClient.SqlCommand("select NDOC_003, NOMB_003, APELL_003 from M_PERS_003 where (NOMB_003=@C1) AND (APELL_003=@C2) ", cnn1)
            consulta1.Parameters.Add(New SqlParameter("C1", txtNombre.Text))
            consulta1.Parameters.Add(New SqlParameter("C2", txtApellido.Text))
            consulta1.ExecuteNonQuery()
            Dim datos As SqlDataReader = consulta1.ExecuteReader()
            If datos.Read Then
                'MENSAJE QUE EXISTE
                mensaje.MERRO025(datos.GetString(0))
                'MessageBox.Show("El Nombre y Apellido Ingresado ya existe con DNI: " + datos.GetString(0), "MENSAJE", MessageBoxButtons.OK, MessageBoxIcon.Error)
                resp = True
            End If
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
        Return resp
    End Function
    Private Sub Baja_operario()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_PERS_003 SET F_BAJA_003 = @C1  WHERE NDOC_003 = @C8 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C8", txtDNI.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Function verificar_cambio_nombre(ByVal nombre As String, ByVal apellido As String) As Boolean
        Dim resp As Boolean = True
        If nombre <> UnOperario.nombre Or apellido <> UnOperario.apellido Then
            If Verificar_NYA() = True Then
                resp = False
            End If
        End If
        Return resp
    End Function
#End Region

#Region "BOTONES"
   
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        borrar()
    End Sub
    Private Sub BTVERIFICAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtDNI.Text.Length = 8 Then
            If UnOperario.Existe_USR(txtDNI.Text) = True Then
                llenar_un_operario()
            Else
                Dim res As DialogResult
                res = MessageBox.Show("Operario Inexistente, ¿Desea cargar nuevo usuario?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If res = System.Windows.Forms.DialogResult.Yes Then
                    txtDNI.ReadOnly = True
                    txtNombre.ReadOnly = False
                    txtApellido.ReadOnly = False
                    TxtLegajo.ReadOnly = False
                    txtDomicilio.ReadOnly = False
                    cbLocalidad.Enabled = True
                    cbEsalmacen.Enabled = True
                    cbProvincia.Enabled = True
                    cbPartido.Enabled = True
                    btAlta.Enabled = True
                    Button1.Enabled = False
                End If
            End If
        Else
            mensaje.MERRO014()
        End If
    End Sub
    Private Sub btalta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAlta.Click
        If txtAlta.Text = Nothing Then
            If txtNombre.Text <> Nothing And txtApellido.Text <> Nothing And TxtLegajo.Text <> Nothing And txtDomicilio.Text <> Nothing And cbEsalmacen.Text <> Nothing And cbProvincia.Text <> Nothing And cbPartido.Text <> Nothing And cbLocalidad.Text <> Nothing Then
                If Verificar_NYA() = False Then
                    If cbEsalmacen.Text = "SI" Then
                        almacen = 1
                    Else
                        almacen = 0
                    End If
                    cod_provincia = cbProvincia.SelectedValue
                    cod_partido = cbPartido.SelectedValue
                    cod_localidad = cbLocalidad.SelectedValue
                    Nuevo_operario()
                End If
            Else
                mensaje.MERRO008()
            End If
        Else
            Dim res As DialogResult
            res = MessageBox.Show("¿Desea reactivar el operario?", "REACTIVAR OPERARIO", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If res = System.Windows.Forms.DialogResult.Yes Then
                reactivar_operario()
            End If
        End If
    End Sub
    Private Sub btBaja_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBaja.Click
        Dim res As DialogResult
        res = MessageBox.Show("¿Está seguro que desea dar de baja el usuario seleccionado?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If res = System.Windows.Forms.DialogResult.Yes Then
            Baja_operario()
        End If
    End Sub
    Private Sub btModificacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btModi.Click
        If txtNombre.Text <> Nothing And txtApellido.Text <> Nothing And TxtLegajo.Text <> Nothing And txtDomicilio.Text <> Nothing And cbEsalmacen.Text <> Nothing And cbProvincia.Text <> Nothing And cbPartido.Text <> Nothing And cbLocalidad.Text <> Nothing Then
            If verificar_cambio_nombre(txtNombre.Text, txtApellido.Text) = True Then
                If cbEsalmacen.Text = "SI" Then
                    almacen = 1
                Else
                    almacen = 0
                End If
                cod_provincia = cbProvincia.SelectedValue
                cod_partido = cbPartido.SelectedValue
                cod_localidad = cbLocalidad.SelectedValue
                Modificar_operario()
            End If
        Else
            mensaje.MERRO008()
        End If

    End Sub


#End Region
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbProvincia.SelectedIndexChanged
        If cbProvincia.ValueMember <> Nothing And cbProvincia.Text <> Nothing Then
            cargar_partidos(CInt(cbProvincia.SelectedValue))
        End If
    End Sub
    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPartido.SelectedIndexChanged
        If cbPartido.ValueMember <> Nothing And cbPartido.Text <> Nothing Then
            Cargar_localidades(CInt(cbPartido.SelectedValue))
        End If
    End Sub

    Private Sub txtDNI_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDNI.DoubleClick
        Dim PANTALLA As New POPER002
        PANTALLA.ShowDialog()
        If PANTALLA.dato = True Then
            txtDNI.Text = PANTALLA.dniobtenido
            UnOperario.Existe_USR(txtDNI.Text)
            txtDNI.ReadOnly = False
            llenar_un_operario()
        End If
    End Sub

    Private Sub txtDNI_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDNI.TextChanged

    End Sub
End Class