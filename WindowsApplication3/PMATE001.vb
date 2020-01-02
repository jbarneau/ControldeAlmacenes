Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PMATE001
    Private mensaje As New Clase_mensaje
    Private unmaterial As New Class_UnMaterial
    Dim cod_material As String
    Dim boton1 As String
    Dim boton2 As String
    Dim decimales As String


    Private Sub PMATE001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        cbDecimal.Items.Add("SI")
        cbDecimal.Items.Add("NO")
        cbUnidad.Items.Add("U")
        cbUnidad.Items.Add("M")
        cbUnidad.Items.Add("M2")
        cbUnidad.Items.Add("M3")
        llenar_tipomaterial()

    End Sub

   

    Private Sub txtSap_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSap.DoubleClick
        Dim PANTALLA As New PMATE002
        PANTALLA.ShowDialog()
        If PANTALLA.dato = True Then
            txtSap.Text = PANTALLA.dniobtenido
            unmaterial.Existe_material(txtSap.Text)
            txtSap.Enabled = False
            llenar_un_material()
        End If

    End Sub

   

    

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipo.SelectedIndexChanged
        If cbTipo.Text = "MATERIAL" Then
            rbSeri.Enabled = True
            rbConsumible.Enabled = True
            cbDecimal.Enabled = True
        Else
            rbSeri.Enabled = False
            rbConsumible.Enabled = False
            cbDecimal.Enabled = False
            cbDecimal.Text = Nothing
            rbSeri.Checked = False
            rbConsumible.Checked = False
        End If
    End Sub

#Region "botones"
    'boton de borrar
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        borrar()
    End Sub
    'boton de salir
   
    'boton verificar
    Private Sub verificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btVerificar.Click


        If txtSap.Text <> Nothing Then
            If unmaterial.Existe_material(txtSap.Text) = True Then
                llenar_un_material()
            Else

                'SI NO TENGO RESULTADOS
                Dim res As DialogResult
                res = MessageBox.Show("Material Inexistente, ¿Desea cargarlo como nuevo ?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                If res = System.Windows.Forms.DialogResult.Yes Then
                    'HABILITO LOS BOTONES
                    btVerificar.Enabled = False
                    txtSap.Enabled = False
                    cbDecimal.Enabled = True
                    txtDesc.Enabled = True
                    txtAlt.Enabled = True
                    cbTipo.Enabled = True
                    btAlta.Enabled = True
                    cbUnidad.Enabled = True
                End If
            End If

        End If

    End Sub

    'BOTON MODIFICAR
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btModi.Click
        If txtSap.Text <> Nothing And txtDesc.Text <> Nothing And txtAlt.Text <> Nothing And cbTipo.Text <> Nothing And cbUnidad.Text <> Nothing Then
            If rbSeri.Checked = True Then
                boton1 = "True"
                boton2 = "False"
            Else
                boton2 = "True"
                boton1 = "False"
            End If
            If rbSeri.Checked = False And rbConsumible.Checked = False Then
                boton2 = "False"
                boton1 = "False"
            End If
            If cbDecimal.Text = "SI" Then
                decimales = "True"
            Else
                decimales = "False"
            End If
            If cbDecimal.Text = Nothing Then
                decimales = "False"
            End If
            cod_material = cbTipo.SelectedValue
            Modificar()
        Else
            mensaje.MERRO008()
        End If
    End Sub
    'Boton Baja
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btBaja.Click
        Dim res As DialogResult
        res = MessageBox.Show("¿Está seguro que desea dar de baja ?", "Respuesta", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If res = System.Windows.Forms.DialogResult.Yes Then
            Dar_Baja()
        End If
    End Sub
    'BOTON DAR DE ALTA Y REACTIVAR
    Private Sub btAlta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btAlta.Click
        If txtAlta.Text = Nothing Then
            If (txtAlt.Text <> Nothing And txtSap.Text <> Nothing And txtDesc.Text <> Nothing And cbTipo.Text <> Nothing And cbUnidad.Text <> Nothing) Then
                If rbSeri.Checked = True Then
                    boton1 = "True"
                    boton2 = "False"
                Else
                    boton2 = "True"
                    boton1 = "False"
                End If
                If rbSeri.Checked = False And rbConsumible.Checked = False Then
                    boton2 = "False"
                    boton1 = "False"
                End If
                'ANALIZO DECIMALES
                If cbDecimal.Text = "SI" Then
                    decimales = "True"
                Else
                    decimales = "False"
                End If
                cod_material = cbTipo.SelectedValue
                Grabar_nuevo()
            Else
                mensaje.MERRO008()
            End If
        Else
            Reactivar(txtSap.Text)
        End If
    End Sub
#End Region
#Region "funciones"
    Private Sub llenar_tipomaterial()
        Dim ds_deposito As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("select C_PARA_802, DESC_802 from DET_PARAMETRO_802 where C_TABLA_802 =2 ", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(ds_deposito, "DET_PARAMETRO_802")
        cnn2.Close()
        cbTipo.DataSource = ds_deposito.Tables("DET_PARAMETRO_802")
        cbTipo.DisplayMember = "DESC_802"
        cbTipo.ValueMember = "C_PARA_802"
        cbTipo.Text = Nothing
    End Sub
    Private Sub llenar_un_material()
        txtSap.Enabled = False
        txtAlt.Text = unmaterial.alternativo
        txtDesc.Text = unmaterial.descripcion
        cbUnidad.Text = unmaterial.unidadmedida
        txtAlta.Text = unmaterial.fechadealta
        If unmaterial.fechadebaja <> Nothing Then
            txtBaja.Text = unmaterial.fechadebaja
            btAlta.Text = "Reactivar"
            btAlta.Enabled = True
            btBaja.Enabled = False
            btModi.Enabled = False
        Else
            txtBaja.Text = Nothing
            btBaja.Enabled = True
            btModi.Enabled = True
            btAlta.Enabled = False
        End If
        cbTipo.SelectedValue = unmaterial.codtipo
        If unmaterial.codtipo = 1 Then
            rbSeri.Enabled = True
            rbConsumible.Enabled = True
            If unmaterial.serializado Then
                rbSeri.Checked = True
            End If
            If unmaterial.consumible Then
                rbConsumible.Checked = True
            End If
        End If
        If unmaterial.tienedecimal Then
            cbDecimal.Text = "SI"
        Else
            cbDecimal.Text = "NO"
        End If
        txtAlt.Enabled = True
        txtDesc.Enabled = True
        cbTipo.Enabled = True
        cbUnidad.Enabled = True
        btVerificar.Enabled = False

    End Sub
    Private Sub borrar()

        btAlta.Text = "Dar de Alta"
        cbUnidad.Text = Nothing
        cbUnidad.Enabled = False
        rbSeri.Checked = False
        rbConsumible.Checked = False
        txtAlta.Text = Nothing
        txtBaja.Text = Nothing
        txtDesc.Text = Nothing
        txtDesc.Enabled = False
        txtAlt.Text = Nothing
        txtAlt.Enabled = False
        cbTipo.Text = Nothing
        cbTipo.Enabled = False
        cbDecimal.Text = Nothing
        cbDecimal.Enabled = False
        btVerificar.Enabled = True
        btModi.Enabled = False
        btBaja.Enabled = False
        btAlta.Enabled = False
        txtSap.Enabled = True
        txtSap.Text = Nothing
        txtSap.Focus()
    End Sub
    Private Sub Grabar_nuevo()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("INSERT INTO M_MATE_002 (CMATE_002, CALTE_002, DESC_002, TIPO_002, SERI_002, CONS_002, DECI_002, F_ALTA_002, USRS_002, UNID_002) VALUES (@C0, @C1, @C2, @C3, @C4, @C5, @C6, @C7, @C8, @C9) ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C0", txtSap.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", txtAlt.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", txtDesc.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", cod_material))
            consulta7.Parameters.Add(New SqlParameter("C4", boton1))
            consulta7.Parameters.Add(New SqlParameter("C5", boton2))
            consulta7.Parameters.Add(New SqlParameter("C6", decimales))
            consulta7.Parameters.Add(New SqlParameter("C7", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C8", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("C9", cbUnidad.Text))
            consulta7.ExecuteNonQuery()
            mensaje.MADVE001()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub Dar_Baja()
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_MATE_002 SET F_BAJA_002 =@C3 , F_MODI_002 = @C1  WHERE CMATE_002 = @C2 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C2", txtSap.Text))
            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C3", DateTime.Now))
            consulta7.ExecuteNonQuery()
            cnn7.Close()

            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub
    Private Sub Modificar()
        Try
            'ACTUALIZO LOS CAMBIOS..............................
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_MATE_002 SET CALTE_002 = @C1, DESC_002=@C2 ,SERI_002=@C3, CONS_002= @C4, DECI_002 = @C5, F_MODI_002=@C6, TIPO_002=@C8, UNID_002=@C9 WHERE CMATE_002 = @C7 ", cnn7)
            consulta7.Parameters.Add(New SqlParameter("C1", txtAlt.Text))
            consulta7.Parameters.Add(New SqlParameter("C2", txtDesc.Text))
            consulta7.Parameters.Add(New SqlParameter("C3", boton1))
            consulta7.Parameters.Add(New SqlParameter("C4", boton2))
            consulta7.Parameters.Add(New SqlParameter("C5", decimales))
            consulta7.Parameters.Add(New SqlParameter("C6", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C8", cod_material))
            consulta7.Parameters.Add(New SqlParameter("C9", cbUnidad.Text))
            consulta7.Parameters.Add(New SqlParameter("C7", txtSap.Text))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()

        End Try
    End Sub
    Private Sub Reactivar(ByVal sap As String)
        Try
            Dim cnn7 As SqlConnection = New SqlConnection(conexion)
            cnn7.Open()
            Dim consulta7 As New SqlClient.SqlCommand("UPDATE M_MATE_002 SET F_BAJA_002=NULL, F_MODI_002=@C1, USRS_002=@C2 WHERE CMATE_002=@D1", cnn7)

            consulta7.Parameters.Add(New SqlParameter("C1", DateTime.Now))
            consulta7.Parameters.Add(New SqlParameter("C2", _usr.Obt_Usr))
            consulta7.Parameters.Add(New SqlParameter("D1", sap))
            consulta7.ExecuteNonQuery()
            cnn7.Close()
            mensaje.MADVE003()
            borrar()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
    End Sub

#End Region


   
    
    Private Sub txtSap_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSap.TextChanged

    End Sub
End Class