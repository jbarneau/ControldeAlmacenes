Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA042
    Private MENSAJE As New Clase_mensaje
    Private Metodos As New Clas_Almacen
    Private Clas_Medidor As New Clas_Medidor
    Private med_rettirar As New Clase_med_retirar
    Private cant As Integer = 0
    Private remito As Decimal
    Private fecha As Date
    Private resumen As New DataTable
    Private DS_deposito1 As New DataSet
    Private _DEPOSITO1 As String
    
  

    Private Sub PALMA042_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString

        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            cbdesde.DropDownStyle = ComboBoxStyle.DropDown
            _DEPOSITO1 = _usr.Obt_Almacen
            'escribo el nombre del deposito
            cbdesde.Text = Metodos.NOMBRE_DEPOSITO(_DEPOSITO1)
            LLENAR(_DEPOSITO1)
        Else
            cbdesde.Enabled = True
            'lleno el combo box del deposito
            llenar_DS_DEPOSITO1()
            cbdesde.Focus()
            'desactivo el combobox del equitoi
        End If
    End Sub
#Region "########### FUNCIONES ###############"
    Private Sub borrar()
        resumen.Clear()
        ListView1.Items.Clear()
        TextBox1.Text = 0
        If _usr.Obt_Almacen <> "0" Then ' SI ES DISTINTO DE CERO PONGO EL COMBOBOS CON EL NOMBRE Y GUAROD EL DEPOSITO
            LLENAR(_DEPOSITO1)
        Else
            cbdesde.Enabled = True
            'lleno el combo box del deposito
            llenar_DS_DEPOSITO1()
            cbdesde.Focus()
            'desactivo el combobox del equitoi
        End If
    End Sub
    Private Sub LLENAR(ByVal DEPOSITO As String)
        ListView1.Items.Clear()
        Dim DATA As New DataTable
        Dim renglon As New ListViewItem
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NMOVI_116, COUNT(NCAJON_116) AS CANTIDAD FROM T_TRAN_MED_RET_116 WHERE (NRECIB_116=@D1) GROUP BY NMOVI_116", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", DEPOSITO))
        adaptadaor.Fill(DATA)
        cnn2.Close()
        For i = 0 To DATA.Rows.Count - 1
            'MessageBox.Show(DATA.Rows(i).Item(0).ToString + " : " + DATA.Rows(i).Item(1).ToString)
            renglon = New ListViewItem(DATA.Rows(i).Item(0).ToString)
            renglon.SubItems.Add(DATA.Rows(i).Item(1).ToString)
            ListView1.Items.Add(renglon)
        Next
    End Sub
    Private Sub llenar_DS_DEPOSITO1()
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, NOMB_003 FROM M_PERS_003 WHERE DEPO_003 = 1 AND F_BAJA_003 is NULL order by NOMB_003", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito1, "M_PERS_003")
        cnn2.Close()
        cbdesde.DataSource = DS_deposito1.Tables("M_PERS_003")
        cbdesde.DisplayMember = "NOMB_003"
        cbdesde.ValueMember = "NDOC_003"
        cbdesde.Text = Nothing
    End Sub
    Private Sub borrar_temp_trans(nmov As Decimal)
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As New SqlClient.SqlCommand("DELETE FROM T_TRAN_MED_RET_116 WHERE (NMOVI_116 = @D1)", cnn1)
            'Ejecutamos el commnad y le pasamos el parametro
            Comando.Parameters.Add(New SqlParameter("D1", nmov))
            Comando.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn1.Close()
        End Try

    End Sub

    Private Sub modificar_medidor(mov As Decimal, depo As String)
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As New SqlClient.SqlCommand("UPDATE T_MED_DEVO_113 SET ESTADO_113 = 1, DEPOSI_113 = @D1 FROM T_TRAN_MED_RET_116 INNER JOIN T_MED_DEVO_113 ON T_TRAN_MED_RET_116.NCAJON_116 = T_MED_DEVO_113.CAJON_113 WHERE (T_TRAN_MED_RET_116.NMOVI_116 = @E1) DELETE FROM T_TRAN_MED_RET_116 WHERE (NMOVI_116 = @D1)", cnn1)
            'Ejecutamos el commnad y le pasamos el parametro
            Comando.Parameters.Add(New SqlParameter("D1", depo))
            Comando.Parameters.Add(New SqlParameter("E1", mov))
            Comando.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn1.Close()
        End Try

    End Sub
    Private Sub grabar_movimiento(mov As Decimal)
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As New SqlClient.SqlCommand("SELECT T_MED_DEVO_113.NSERI_113 FROM T_TRAN_MED_RET_116 INNER JOIN T_MED_DEVO_113 ON T_TRAN_MED_RET_116.NCAJON_116 = T_MED_DEVO_113.CAJON_113 WHERE (T_TRAN_MED_RET_116.NMOVI_116 = @D1)", cnn1)
            'Ejecutamos el commnad y le pasamos el parametro
            Comando.Parameters.Add(New SqlParameter("D1", mov))
            Dim lector As SqlDataReader = Comando.ExecuteReader
            Do While lector.Read
                med_rettirar.GRABAR_TRANS(lector.GetValue(0), remito, fecha, _usr.Obt_Usr, 4)
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn1.Close()
        End Try
    End Sub

#End Region

    Private Sub cbdesde_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbdesde.SelectedIndexChanged
        If cbdesde.ValueMember <> Nothing And cbdesde.Text <> Nothing Then
            _DEPOSITO1 = cbdesde.SelectedValue
            LLENAR(_DEPOSITO1)
        End If
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As System.EventArgs) Handles ListView1.DoubleClick
        Dim PANTALLA As New PALMA042BIS
        For I = 0 To ListView1.Items.Count - 1
            If ListView1.Items(I).Selected = True Then
                PANTALLA.LLER(CDec(ListView1.Items(I).Text))
                PANTALLA.ShowDialog()
            End If
        Next
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Dim CAN As Integer = 0
        For I = 0 To ListView1.Items.Count - 1
            If ListView1.Items(I).Selected = True Then
                'MessageBox.Show()
                CAN = CAN + CInt(ListView1.Items(I).SubItems(1).Text)
            End If
        Next
        TextBox1.Text = CAN
        If CAN <> 0 Then
            Button1.Enabled = True
        Else
            Button1.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        fecha = Date.Now
        remito = med_rettirar.Obtener_Numero_Mov
        If cbdesde.Text <> Nothing And TextBox1.Text <> 0 Then
            Dim nmovimiento As Decimal = med_rettirar.Obtener_Numero_Mov
            Dim FECHA As Date = Date.Now
            For I = 0 To ListView1.Items.Count - 1
                If ListView1.Items(I).Selected = True Then
                    modificar_medidor(ListView1.Items(I).Text, _DEPOSITO1)
                    borrar_temp_trans(ListView1.Items(I).Text)
                    grabar_movimiento(ListView1.Items(I).Text)
                End If
            Next
            MENSAJE.MADVE001()
            borrar()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        borrar()
    End Sub
End Class