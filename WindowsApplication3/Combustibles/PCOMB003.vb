Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.IO

Public Class PCOMB003
    Private Sub AgregarVehiculo(ByVal dom As String, ByVal marca As Integer, ByVal modelo As String, ByVal año As Integer)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("INSERT INTO M_VEHICULO_008 (DOMINIO008,MARCA008,MODELO008,AÑO008) VALUES (@D1,@D2,@D3,@D4)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", dom))
            adt.Parameters.Add(New SqlParameter("D2", marca))
            adt.Parameters.Add(New SqlParameter("D3", modelo))
            adt.Parameters.Add(New SqlParameter("D4", año))
            If (adt.ExecuteNonQuery() <> 0) Then
                MessageBox.Show("VEHICULO CARGADO CORRECTAMENTE", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("NO SE PUDO CARGAR CORRECTAMENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Throw New Exception("ERROR en AgregarVehiculo")
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub BorrarCampos()
        ComboBox1.Text = Nothing
        txtaño.Clear()
        txtmod.Clear()
        txtdominio.Clear()
        txtaño.Enabled = False
        txtmod.Enabled = False
        ComboBox1.Enabled = False
        btncargar.Enabled = False
        btneliminar.Enabled = False


    End Sub
    Private Sub EliminarVehiculo(ByVal dominio As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("DELETE FROM M_VEHICULO_008 WHERE DOMINIO008 = @D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", dominio))
            If (adt.ExecuteNonQuery() <> 0) Then
                MessageBox.Show("SE HA ELIMINADO CORRECTAMENTE EL VEHICULO DOMINIO: " + "" + dominio, "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("NO SE ENCONTRO EL DOMINIO EN LA BASE DE DATOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Throw New Exception("ERROR en EliminarVehiculo")
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub PCOMB003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenarmarca()
    End Sub
    Private Sub llenarmarca()
        Dim tabla As New DataTable
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=20 ORDER BY DESC_802", cnn)
            adt.Fill(tabla)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        If tabla.Rows.Count <> 0 Then
            ComboBox1.DataSource = tabla
            ComboBox1.DisplayMember = "DESC_802"
            ComboBox1.ValueMember = "C_PARA_802"
            ComboBox1.Text = Nothing
        End If
    End Sub
    Private Sub btncargar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncargar.Click
        If txtaño.Text <> Nothing And txtdominio.Text <> Nothing And ComboBox1.Text <> Nothing And txtmod.Text <> Nothing Then
            AgregarVehiculo(txtdominio.Text, ComboBox1.SelectedValue, txtdominio.Text, CInt(txtaño.Text))
            BorrarCampos()
        Else
            MessageBox.Show("NO PUEDE HABER CAMPOS VACIOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub VerificarVehiculo()
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select MARCA008, MODELO008,AÑO008 FROM M_VEHICULO_008 WHERE DOMINIO008=@D1", cnn)
            adt.Parameters.AddWithValue("D1", txtdominio.Text)
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            If LECTOR.Read Then
                txtaño.Text = LECTOR.GetValue(2)
                txtmod.Text = LECTOR.GetValue(1)
                ComboBox1.SelectedValue = LECTOR.GetValue(0)
                btneliminar.Enabled = True
            Else
                txtaño.Enabled = True
                txtmod.Enabled = True
                ComboBox1.Enabled = True
                btneliminar.Enabled = False
                btncargar.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub

  

    

    Private Sub btneliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneliminar.Click
            EliminarVehiculo(txtdominio.Text)
            BorrarCampos()
    End Sub

    Private Sub txtdominio_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtdominio.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtdominio.Text <> Nothing Then
                VerificarVehiculo()
            Else
                MessageBox.Show("NO SE PERMITE EL DOMINIO VACIO")
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        BorrarCampos()
    End Sub
End Class