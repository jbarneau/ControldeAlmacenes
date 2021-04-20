Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class PALMA039
    Private MENSAJES As New Clase_mensaje
    Private ALMACEN As New Clas_Almacen
    Private CuantosDias As Integer = 5
    Private Sub PALMA039_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        main_llenar()
    End Sub

#Region "FUNCIONES"
    Private Sub main_llenar()
        GroupBox1.Text = "PANEL DE CONTROL A " + CuantosDias.ToString + " DIAS"
        Label12.Text = "TAREA COMUNICADA SIN ENCONTRAR EL MEDIDOR CON MAS DE " + CuantosDias.ToString + " DIAS"
        Label18.Text = "MEDIDORES SIN TAREAS COMUNICADAS CON MAS DE " + CuantosDias.ToString + " DIAS"
        Label16.Text = MedEnCustodia()
        Label15.Text = MedEnCustodiaParaEntregar()
        Label17.Text = MedEnlote()
        Label24.Text = MedaBuscar()
        Label14.Text = MedSinInformarOt()
        Label26.Text = MedaBuscarComMas5Dias()
        Label19.Text = MedSinInformarOtMas5Dias()
    End Sub
    Private Function MedEnCustodia() As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select count(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE ESTADO_113=9 and FCUSTODIA_113 > @D1 ", cnn)
            adt.Parameters.Add(New SqlParameter("D1", DateAdd(DateInterval.Month, -6, Date.Now)))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Function MedEnCustodiaParaEntregar() As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select count(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE ESTADO_113=9 And FCUSTODIA_113 < @D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", DateAdd(DateInterval.Month, -6, Date.Now)))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Function MedEnlote() As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select count(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE ESTADO_113=1", cnn)
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Function MedaBuscarComMas5Dias() As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select count(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE ESTADO_113=0 and FCARGO_113<@D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", DateAdd(DateInterval.Day, -CuantosDias, Date.Now)))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Function MedaBuscar() As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select count(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE ESTADO_113=0", cnn)
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Function MedSinInformarOt() As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select count(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE FINFO_113 IS NULL", cnn)
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Function MedSinInformarOtMas5Dias() As Integer
        Dim resp As Integer = 0
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select count(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE FINFO_113 IS NULL and FCARGO_113 < @D1 ", cnn)
            adt.Parameters.Add(New SqlParameter("D1", DateAdd(DateInterval.Day, -CuantosDias, Date.Now)))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                resp = lector.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Return resp
    End Function
    Private Function desfamilia(ByVal D1 As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=15 AND C_PARA_802 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", D1))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString Then
            resp = lector1.GetValue(0)
        End If
        con1.Close()
        Return resp
    End Function
#End Region
#Region "BOTONES"

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim palma6 As New PALMA036
        Me.Hide()
        palma6.ShowDialog()
        Me.Show()
        main_llenar()
    End Sub
   
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim palma6 As New PALMA041
        Me.Hide()
        palma6.ShowDialog()
        Me.Show()
        main_llenar()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim palma6 As New PALMA042
        Me.Hide()
        palma6.ShowDialog()
        Me.Show()
        main_llenar()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim palma6 As New PALMA044
        Me.Hide()
        palma6.ShowDialog()
        Me.Show()
        main_llenar()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        Dim palma6 As New PALMA046
        Me.Hide()
        palma6.ShowDialog()
        Me.Show()
        main_llenar()
    End Sub
    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs)
        Dim palma6 As New PALMA043
        Me.Hide()
        palma6.ShowDialog()
        Me.Show()
        main_llenar()
    End Sub
#End Region
   
  
   
    Private Sub Label15_DoubleClick(sender As Object, e As System.EventArgs) Handles Label15.DoubleClick
        If Label15.Text <> 0 Then
            Dim PANTALLA As New PALMA038
            Me.Hide()
            PANTALLA.ShowDialog()
            Me.Show()
            main_llenar()
        End If
    End Sub

   
   
    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim PANTALLA As New PALMA039BIS
        PANTALLA.TOMAR(CuantosDias)
        PANTALLA.ShowDialog()
        CuantosDias = PANTALLA.LEERDIAS
        main_llenar()
    End Sub

    Private Sub Label26_DoubleClick(sender As Object, e As System.EventArgs) Handles Label26.DoubleClick
        If Label26.Text <> 0 Then
            Dim PANTALLA As New PALMA047
            PANTALLA.TOMAR(1, CuantosDias)
            PANTALLA.ShowDialog()
        End If
    End Sub
    Private Sub Label19_DoubleClick(sender As Object, e As System.EventArgs) Handles Label19.DoubleClick
        If Label19.Text <> 0 Then
            Dim PANTALLA As New PALMA047
            PANTALLA.TOMAR(2, CuantosDias)
            PANTALLA.ShowDialog()
        End If
    End Sub

    Private Sub Generar_Archivo()
        Dim tabla As New DataTable
        With tabla
            .Columns.Add("Nserie")
            .Columns.Add("CodSap")
            .Columns.Add("Descripcion")
            .Columns.Add("Poliza")
            .Columns.Add("OT")
            .Columns.Add("F_Cargo")
            .Columns.Add("F_Comunico")
            .Columns.Add("Cajon")
            .Columns.Add("Nremito")
            .Columns.Add("F_remito")
            .Columns.Add("Familia")
            .Columns.Add("Operario")
        End With
        '//importante todos los datos que no tenga tengo que poner ""
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT T_MED_DEVO_113.NSERI_113, T_MED_DEVO_113.CMATE_113, M_MATE_002.DESC_002, T_MED_DEVO_113.POLIZA_113, T_MED_DEVO_113.OT_113, T_MED_DEVO_113.FCARGO_113, T_MED_DEVO_113.FRETIRO_113, T_MED_DEVO_113.CAJON_113, T_MED_DEVO_113.OPERA_113 FROM T_MED_DEVO_113 INNER JOIN M_MATE_002 ON T_MED_DEVO_113.CMATE_113 = M_MATE_002.CMATE_002 WHERE (T_MED_DEVO_113.ESTADO_113 = 0)", CNN)
            Dim lector As SqlDataReader = ADT.ExecuteReader
            If lector.Read Then
                tabla.Rows.Add(lector.GetValue(1), lector.GetValue(2), lector.GetValue(3), lector.GetValue(4), lector.GetValue(5), lector.GetValue(6), lector.GetValue(7))
            End If

        Catch ex As Exception

        End Try
        'primero tengo que leer lo que esta sin encontrar con estado 0
        'luego tdo lo que falta agregara



    End Sub


End Class