Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.IO
Public Class PCOMB002

    Private ticket As String
#Region "funciones "
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
    Private Sub BorrarCampos()
        txtnticket.Text = Nothing
        txtaño.Text = Date.Today.Year.ToString
        cbcombustible.Text = Nothing
        txtdominio.Clear()
        txtnro.Clear()
        lbLitros.Text = Nothing
        txtlitros.Clear()
        lbllitros.Text = Nothing
        lbCombustible.Text = Nothing
        calendario.Value = Date.Today
        txtnro.Text = Nothing
        txtlitros.Text = Nothing
        GROUP1.Visible = False

        txtdominio.Enabled = True
        btnbuscar.Enabled = True
        txtaño.Enabled = True
        txtnticket.Enabled = True
        Button1.Enabled = True
    End Sub
    Private Sub DatosTicket(nticket As String)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT DET_PARAMETRO_802.DESC_802, T_VALE_122.CARGA122, T_VALE_122.DOMINI122, T_VALE_122.NVALE122 FROM T_VALE_122 INNER JOIN DET_PARAMETRO_802 ON T_VALE_122.CCOMB122 = DET_PARAMETRO_802.C_PARA_802 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 22) AND (T_VALE_122.NVALE122 = @D1) AND (T_VALE_122.FTICKET122 IS NULL)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", nticket))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                lbCombustible.Text = lector.GetValue(0)
                If lector.GetValue(1) = "0" Then
                    lbLitros.Text = "LLENO"
                Else
                    lbLitros.Text = lector.GetValue(1)
                End If
                txtdominio.Text = lector.GetValue(2)
                GROUP1.Visible = True
                txtdominio.Enabled = False
                btnbuscar.Enabled = False
                txtaño.Enabled = False
                txtnticket.Enabled = False
                Button1.Enabled = False
            Else
                MessageBox.Show("NO SE HAN ENCONTRADO EL TICKET", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub

#End Region



    Private Sub PCOMB002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        txtaño.Text = Date.Today.Year.ToString
        llenar_CB_TIPO_COMBUSTIBLE()
    End Sub

    Private Sub ActualizarDB(ByVal num As String, ByVal FechaTicket As Date, ByVal nTicket As String, ByVal litros As Decimal, ByVal TipoTicket As Integer)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("Update T_VALE_122 set FTICKET122 = @D2, NTICKET122 = @D3, LITROS122 = @D4, TIPOTICKET122 = @D5  where NVALE122 = @D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", num))
            adt.Parameters.Add(New SqlParameter("D2", FechaTicket))
            adt.Parameters.Add(New SqlParameter("D3", nTicket))
            adt.Parameters.Add(New SqlParameter("D4", litros))
            adt.Parameters.Add(New SqlParameter("D5", TipoTicket))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("ERROR en ACTUALIZARDB")
        Finally
            cnn.Close()
        End Try
    End Sub

    Private Sub GrabarMovimiento(ByVal FechaTicket As Date, ByVal ticket As String)
        Dim CNN As New SqlConnection(conexionIntegral)
        Dim NMOV As Decimal = ObtenerNumero()
        Dim FECMOVI As Date = Date.Now
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("INSERT INTO T114_TAREAS_X_USER (NMOV114,FECHA114,CMOVI114,CCONT114,FALTA114,NPARTE114,CMOTIVO114,USER114,NGNF114) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7,@D8,@D9)", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", NMOV))
            ADT.Parameters.Add(New SqlParameter("D2", FECMOVI))
            ADT.Parameters.Add(New SqlParameter("D3", "VACT"))
            ADT.Parameters.Add(New SqlParameter("D4", "ACT"))
            ADT.Parameters.Add(New SqlParameter("D5", FechaTicket))
            ADT.Parameters.Add(New SqlParameter("D6", ticket))
            ADT.Parameters.Add(New SqlParameter("D7", "VACT"))
            ADT.Parameters.Add(New SqlParameter("D8", _usr.Obt_Usr))
            ADT.Parameters.Add(New SqlParameter("D9", -1))
            ADT.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub

    Private Function ObtenerNumero() As Decimal
        Dim resp As Decimal = 0
        Dim cnn As New SqlConnection(conexionIntegral)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select CONTADOR FROM Z001_CONTADOR WHERE CODIGO ='NUSER'", cnn)
            Dim LECTOR As SqlDataReader = adt.ExecuteReader
            If LECTOR.Read Then
                resp = LECTOR.GetValue(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cnn.Close()
        End Try
        If resp <> 0 Then
            Try
                cnn.Open()
                Dim ADT As New SqlCommand("UPDATE Z001_CONTADOR SET CONTADOR = CONTADOR+1 WHERE CODIGO = 'NUSER'", cnn)
                ADT.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                cnn.Close()
            End Try
        End If
        Return resp
    End Function

    Private Sub btnbuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnbuscar.Click
        If txtdominio.Text <> Nothing Then
            Dim PANTALLA As New PCOMB002bis
            PANTALLA.TOMAR(txtdominio.Text)
            PANTALLA.ShowDialog()
            If PANTALLA.LEERRESP = True Then
                ticket = PANTALLA.LEERTICKET
                txtaño.Text = TICKET.Remove(4, (TICKET.Length - 4))
                txtnticket.Text = CDec(TICKET.Substring(4, CInt(TICKET.Length - 4))).ToString
                DatosTicket(TICKET)
            End If
        Else
            MessageBox.Show("INGRESE EL DOMINIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub btnactualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnactualizar.Click
        Try
            If cbcombustible.Text <> Nothing And calendario.Text <> Nothing And txtlitros.Text <> Nothing Then
                ActualizarDB(ticket, calendario.Value, txtnro.Text, CDec(txtlitros.Text), cbcombustible.SelectedValue)
                GrabarMovimiento(calendario.Value, ticket)
                MessageBox.Show("SE HA ACTUALIZADO CORRECTAMENTE", "´SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                BorrarCampos()
            Else
                MessageBox.Show("NO PUEDE HABER CAMPOS VACIOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtnro_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnro.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            ErrorTicket.SetError(txtnro, "No se permiten Letras. Sòlo Numeros")
            e.Handled = True
        Else
            ErrorTicket.SetError(txtnro, "")
            e.Handled = False
        End If
    End Sub

    Private Sub txtlitros_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtlitros.KeyPress
        If Char.IsLetter(e.KeyChar) Then
            ErrorLitros.SetError(txtlitros, "No se permiten Letras. Sòlo Numeros")
            e.Handled = True
        Else
            ErrorLitros.SetError(txtlitros, "")
            e.Handled = False
        End If
    End Sub

   

   
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If txtaño.Text <> "" And txtnticket.Text <> "" Then
            ticket = txtaño.Text + txtnticket.Text.PadLeft(8, "0")
            DatosTicket(ticket)

        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        BorrarCampos()
    End Sub
End Class