Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.IO
Public Class PCOMB001

#Region "##### FUNCIONES  #####"

    Private Sub llenar_CB_CHOFER()
        Dim tabla As New DataTable
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE ALMA_003 = 1 AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(tabla)
        cnn2.Close()
        cbchofer.DataSource = tabla
        cbchofer.ValueMember = "NDOC_003"
        cbchofer.DisplayMember = "NOMBRE"
        cbchofer.Text = Nothing
    End Sub

    Private Sub llenar_CB_Contrato()
        'CONECTO LA BASE
        Dim TABLA As New DataTable
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NCONT_004, DESC_004 FROM M_CONT_004 where F_BAJA_004 is NULL order by DESC_004", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(TABLA)
        cnn2.Close()
        cbcontrato.DataSource = TABLA
        cbcontrato.DisplayMember = "DESC_004"
        cbcontrato.ValueMember = "NCONT_004"
        cbcontrato.Text = Nothing
    End Sub
    Private Sub llenar_CB_TIPO_CARGA()
        'CONECTO LA BASE
        Dim TABLA As New DataTable
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where F_BAJA_802 is NULL AND C_TABLA_802=21 order by C_PARA_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(TABLA)
        cnn2.Close()
        cbtipo.DataSource = TABLA
        cbtipo.DisplayMember = "DESC_802"
        cbtipo.ValueMember = "C_PARA_802"
        cbtipo.Text = Nothing
    End Sub
    Private Sub llenar_CB_USO()
        'CONECTO LA BASE
        Dim TABLA As New DataTable
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where F_BAJA_802 is NULL AND C_TABLA_802=23 order by C_PARA_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(TABLA)
        cnn2.Close()
        cbUso.DataSource = TABLA
        cbUso.DisplayMember = "DESC_802"
        cbUso.ValueMember = "C_PARA_802"
        cbUso.Text = Nothing
    End Sub
    Private Sub llenar_CB_TIPO_COMBUSTIBLE()
        'CONECTO LA BASE
        Dim TABLA As New DataTable
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where F_BAJA_802 is NULL AND C_TABLA_802=22 order by C_PARA_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(TABLA)
        cnn2.Close()
        cbcombustible.DataSource = TABLA
        cbcombustible.DisplayMember = "DESC_802"
        cbcombustible.ValueMember = "C_PARA_802"
        cbcombustible.Text = Nothing
    End Sub
    Private Sub Borrar()
        txtdominio.Enabled = True
        cbchofer.Enabled = False
        txtcarga.Text = "0"
        cbcombustible.Enabled = False
        cbcontrato.Enabled = False
        cbUso.Enabled = False
        cbtipo.Enabled = False
        txtcarga.Enabled = False
        txtdominio.Text = Nothing
        lbmarca.Text = Nothing
        lbmodelo.Text = Nothing
        cbUso.Text = Nothing
        cbtipo.Text = Nothing
        cbchofer.Text = Nothing
        cbcombustible.Text = Nothing
        cbcontrato.Text = Nothing
        lbcombustiuble.Text = Nothing
        lbuso.Text = Nothing
        txtdominio.Focus()
        Button1.Enabled = False
    End Sub
    Private Function Obtener_Numero_vale() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=9", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        Sumar_Num_vale()
        Return Numero_Remito
    End Function
    Private Sub Sumar_Num_vale()
        'incrementa el numero de remito
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=9", con1)
        comando1.ExecuteReader()
        con1.Close()
    End Sub
    Private Sub grabarvale(ByVal nvaele As String, ByVal fecha As Date, ByVal dominio As String, ByVal chofer As String, ByVal contrato As String, ByVal carga As String, ByVal USO As Integer, ByVal DESUSO As String, ByVal tipo As Integer, ByVal comb As Integer, ByVal descom As String, ByVal user As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("insert into T_VALE_122 (NVALE122,FECHA122,DOMINI122,CHOFER122, CONTRATO122,CARGA122,CUSO122, DESUSO122,CTIPO122,CCOMB122,DESCOM122,USER122) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7,@D8,@D9,@D10,@D11,@D12)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", nvaele))
            adt.Parameters.Add(New SqlParameter("D2", fecha))
            adt.Parameters.Add(New SqlParameter("D3", dominio))
            adt.Parameters.Add(New SqlParameter("D4", chofer))
            adt.Parameters.Add(New SqlParameter("D5", contrato))
            adt.Parameters.Add(New SqlParameter("D6", carga))
            adt.Parameters.Add(New SqlParameter("D7", USO))
            adt.Parameters.Add(New SqlParameter("D8", DESUSO))
            adt.Parameters.Add(New SqlParameter("D9", tipo))
            adt.Parameters.Add(New SqlParameter("D10", comb))
            adt.Parameters.Add(New SqlParameter("D11", descom))
            adt.Parameters.Add(New SqlParameter("D12", user))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
#End Region

    Private Sub PCONB001_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_CB_CHOFER()
        llenar_CB_Contrato()
        llenar_CB_TIPO_CARGA()
        llenar_CB_TIPO_COMBUSTIBLE()
        llenar_CB_USo()
    End Sub

    Private Sub txtdominio_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtdominio.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtdominio.Text <> "" Then
                Dim vehiculo As New vehiculo(txtdominio.Text)
                If vehiculo.SIEXISTE = True Then
                    lbmodelo.Text = vehiculo.LEERMODELO
                    lbmarca.Text = vehiculo.LEERMARCA
                    txtdominio.Enabled = False
                    cbUso.Enabled = True
                    cbchofer.Enabled = True
                    cbcombustible.Enabled = True
                    cbtipo.Enabled = True
                    cbcontrato.Enabled = True
                    txtcarga.Enabled = True
                    Button1.Enabled = True
                    cbchofer.Focus()
                    'VerificarCantVales(txtdominio.Text)
                Else
                    MessageBox.Show("no existe")
                    Borrar()
                End If
            Else
                MessageBox.Show("NO SE PERMITEN CAMPOS VACIOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub


    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Borrar()
    End Sub

    Private Sub cbcombustible_DropDownClosed(sender As Object, e As System.EventArgs) Handles cbcombustible.DropDownClosed
        If cbcombustible.ValueMember <> Nothing And cbcombustible.Text <> "" Then
            If cbcombustible.SelectedValue = 5 Then
                Dim pantalla As New PCOMB001_BIS
                pantalla.ShowDialog()
                If pantalla.leerRespuesta = True Then
                    lbcombustiuble.Text = pantalla.leerTexto
                    txtcarga.Focus()
                Else
                    cbcombustible.Text = Nothing
                    MessageBox.Show("NO SE AGREGO EL TEXTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                lbcombustiuble.Text = Nothing
            End If
        End If
    End Sub

    Private Sub VerificarCantVales(ByVal dom As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT COUNT(NVALE122) AS TICKETS, DOMINI122 FROM T_VALE_122 WHERE (NTICKET122 IS NULL) AND (FTICKET122 IS NULL) GROUP BY DOMINI122 HAVING (DOMINI122 = @D1)", cnn)
            adt.Parameters.AddWithValue("D1", dom)
            If (Convert.ToInt16(adt.ExecuteScalar()) > 3) Then
                MessageBox.Show("EL DOMINIO" + "  " + dom + "  " + "TIENE MAS DE 3 VALES PENDIENTES.", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)
            End If
        Catch
            Throw New Exception("ERROR EN FUNCIÒN BUSCARENDB")
        Finally
            cnn.Close()
        End Try
    End Sub
    

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If cbchofer.Text <> Nothing And cbcombustible.Text <> Nothing And cbtipo.Text <> Nothing And txtcarga.Text <> Nothing And cbUso.Text <> Nothing Then
            Dim nremito As String = Date.Today.Year.ToString + Obtener_Numero_vale.ToString.PadLeft(8, "0")
            Dim fecha As Date = Date.Now
            Dim carga As String
            Dim duso As String
            Dim dcom As String
            Dim DireccionRemito As String = "C:\ARCHIVO\"
            If Directory.Exists(DireccionRemito) = False Then
                Directory.CreateDirectory(DireccionRemito)
            End If
            If txtcarga.Text = 0 Then
                carga = "LLENO"
            Else
                carga = txtcarga.Text + " LITROS"
            End If
            If lbuso.Text = "" Then
                duso = ""
            Else
                duso = lbuso.Text
            End If
            If lbcombustiuble.Text = "" Then
                dcom = ""
            Else
                dcom = lbcombustiuble.Text
            End If
            grabarvale(nremito, fecha, txtdominio.Text, cbchofer.SelectedValue, cbcontrato.SelectedValue, txtcarga.Text, cbUso.SelectedValue, duso, cbtipo.SelectedValue, cbcombustible.SelectedValue, dcom, _usr.Obt_Usr)
            Dim lista As New List(Of UnValeDeCombustible)
            Dim vale As New UnValeDeCombustible
            With vale
                .nvale = nremito
                .fecha = fecha.ToShortDateString
                .domino = txtdominio.Text
                .marca = lbmarca.Text
                .modelo = lbmodelo.Text
                .chofer = cbchofer.Text
                .contrato = cbcontrato.Text
                .carga = carga
                .dnichofer = cbchofer.SelectedValue
                .USO = cbUso.Text
                .DESUSO = lbuso.Text
                .tipocarga = cbtipo.Text
                .tiponafta = cbcombustible.Text
                .desnafta = lbcombustiuble.Text
                .user = _usr.Obt_Nombre_y_Apellido
                .dni = _usr.Obt_Usr
            End With
            lista.Add(vale)
            Dim IMAGEN As New ValeCombustible
            IMAGEN.SetDataSource(lista)
            DireccionRemito = DireccionRemito + "VALE_N" + nremito.ToString + ".PDF"
            IMAGEN.ExportToDisk(ExportFormatType.PortableDocFormat, DireccionRemito)
            Process.Start(DireccionRemito)
            Borrar()
        Else
        MessageBox.Show("NO SE PERMITEN CAMPOS VACIOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub cbUso_DropDownClosed(sender As System.Object, e As System.EventArgs) Handles cbUso.DropDownClosed
        If cbUso.ValueMember <> Nothing And cbUso.Text <> "" Then
            If cbUso.SelectedValue = 5 Then
                Dim pantalla As New PCOMB001_BIS
                pantalla.ShowDialog()
                If pantalla.leerRespuesta = True Then
                    lbuso.Text = pantalla.leerTexto
                    cbtipo.Focus()
                Else
                    cbUso.Text = Nothing
                    MessageBox.Show("NO SE AGREGO EL TEXTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                lbuso.Text = Nothing
            End If
        End If

    End Sub

  

    Private Sub Label16_Click(sender As System.Object, e As System.EventArgs) Handles Label16.Click

    End Sub
End Class