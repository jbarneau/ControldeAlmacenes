Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports System.IO
Public Class PALMA025
    Private DT_Depositos As New DataTable
    Private DT_Estado As New DataTable

    Dim caso As Integer
    Dim estado As Integer
    Dim cont As Integer
    Dim almacenes As String
    Dim d_almacens As String
    Dim _ESTADO As String
    Dim MOSTRAR_ESTADO As String
    Dim mensaje As New Clase_mensaje

   

    Private Sub PALMA025_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        LLENAR_TIPO()
        LLENAR("00")
      
    End Sub

#Region "funciones"
    Private Sub LLENAR_TIPO()
        Dim DT_Tipo As New DataTable
        DT_Tipo.Columns.Add("COD")
        DT_Tipo.Columns.Add("DESC")
        DT_Tipo.Rows.Add("00", "---TODO---")
        Dim RESP As String = ""
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT TIPO_806, DESC_806 FROM P_MAQUINA_806 order by DESC_806", CNN)
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            Do While LECTOR.Read
                DT_Tipo.Rows.Add(LECTOR.GetValue(0), LECTOR.GetValue(1))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        CB_ESTADO.DataSource = DT_Tipo
        CB_ESTADO.DisplayMember = "DESC"
        CB_ESTADO.ValueMember = "COD"
        CB_ESTADO.SelectedValue = "00"

    End Sub
    Private Sub LLENAR(COD As String)
        DGV1.Rows.Clear()
        Me.Cursor = Cursors.WaitCursor
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT M_MAQUI_006.NSERIE_006, M_MAQUI_006.TIPO_006, P_MAQUINA_806.DESC_806, M_MAQUI_006.MARCA_006, M_MAQUI_006.MODELO_006, M_MAQUI_006.CHAPA_006, DET_PARAMETRO_802.DESC_802,  M_MAQUI_006.ALMA_006, M_PERS_003.APELL_003 + N' ' + M_PERS_003.NOMB_003 AS Expr1, M_MAQUI_006.OBS_006, P_MAQUINA_806.CALI_806, P_MAQUINA_806.PLAZO_806, P_MAQUINA_806.VERIF_806,  P_MAQUINA_806.PVERIF_806 FROM M_MAQUI_006 INNER JOIN DET_PARAMETRO_802 ON M_MAQUI_006.ESTADO_006 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_PERS_003 ON M_MAQUI_006.ALMA_006 = M_PERS_003.NDOC_003 INNER JOIN P_MAQUINA_806 ON M_MAQUI_006.TIPO_006 = P_MAQUINA_806.TIPO_806 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 17) AND (M_MAQUI_006.FBAJA_006 IS NULL)", CNN)
            'AND (M_MAQUI_006.TIPO_006 LIKE N'%' + @D1 + '%')
            ADT.Parameters.Add(New SqlParameter("D1", COD))
            Dim lector As SqlDataReader = ADT.ExecuteReader
            Do While lector.Read
                DGV1.Rows.Add(lector.GetValue(0), lector.GetValue(1), lector.GetValue(2), lector.GetValue(3), lector.GetValue(4), lector.GetValue(5), lector.GetValue(6), lector.GetValue(7), lector.GetValue(8), lector.GetValue(9), lector.GetValue(10), lector.GetValue(11), lector.GetValue(12), lector.GetValue(13))
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        If DGV1.Rows.Count <> 0 Then
            For I = 0 To DGV1.Rows.Count - 1
                If DGV1.Item(10, I).Value = "1" Then
                    Dim CALIBRACION As ArrayList = fcalibracion(DGV1.Item(0, I).Value, 1)
                    If CALIBRACION.Count <> 0 Then
                        DGV1.Item(14, I).Value = CALIBRACION(0)
                        DGV1.Item(15, I).Value = CDate(CALIBRACION(1)).ToShortDateString
                        DGV1.Item(16, I).Value = CDate(CALIBRACION(2)).ToShortDateString
                    End If

                End If
                If DGV1.Item(12, I).Value = "1" Then
                    Dim CALIBRACION As ArrayList = fcalibracion(DGV1.Item(0, I).Value, 2)
                    If CALIBRACION.Count <> 0 Then
                        DGV1.Item(17, I).Value = CALIBRACION(0)
                        DGV1.Item(18, I).Value = CDate(CALIBRACION(1)).ToShortDateString
                        DGV1.Item(19, I).Value = CDate(CALIBRACION(2)).ToShortDateString
                    End If

                End If

                colores()

            Next
            Button4.Enabled = True
        Else
            Button4.Enabled = False
        End If
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub colores()
        Dim fecha As Date = Date.Today
        For i = 0 To DGV1.Rows.Count - 1
            If DGV1.Item(10, i).Value = "1" Then
                If DGV1.Item(16, i).Value <> Nothing Or DGV1.Item(16, i).Value <> "" Then
                    Dim vto As Date = CDate(DGV1.Item(16, i).Value).ToShortDateString
                    Dim fanterior As Date = DateAdd(DateInterval.Day, -30, vto)
                    If vto > fecha And fanterior > fecha Then
                        DGV1.Item(14, i).Style.BackColor = Color.LightGreen
                        DGV1.Item(16, i).Style.BackColor = Color.LightGreen
                    End If
                    If vto > fecha And fanterior < fecha Then
                        DGV1.Item(14, i).Style.BackColor = Color.Yellow
                        DGV1.Item(16, i).Style.BackColor = Color.Yellow
                    End If
                    If vto <= fecha Then
                        DGV1.Item(14, i).Style.BackColor = Color.Red
                        DGV1.Item(16, i).Style.BackColor = Color.Red
                    End If
                Else
                    DGV1.Item(14, i).Style.BackColor = Color.Black
                    DGV1.Item(16, i).Style.BackColor = Color.Black
                End If
            End If
            If DGV1.Item(12, i).Value = "1" Then
                If DGV1.Item(19, i).Value <> Nothing Or DGV1.Item(19, i).Value <> "" Then
                    Dim vto As Date = CDate(DGV1.Item(19, i).Value).ToShortDateString
                    Dim fanterior As Date = DateAdd(DateInterval.Day, -30, vto)
                    If vto > fecha And fanterior > fecha Then
                        DGV1.Item(17, i).Style.BackColor = Color.LightGreen
                        DGV1.Item(19, i).Style.BackColor = Color.LightGreen
                    End If
                    If vto > fecha And fanterior < fecha Then
                        DGV1.Item(17, i).Style.BackColor = Color.Yellow
                        DGV1.Item(19, i).Style.BackColor = Color.Yellow
                    End If
                    If vto <= fecha Then
                        DGV1.Item(17, i).Style.BackColor = Color.Red
                        DGV1.Item(19, i).Style.BackColor = Color.Red
                    End If
                Else
                    DGV1.Item(17, i).Style.BackColor = Color.Black
                    DGV1.Item(19, i).Style.BackColor = Color.Black
                End If
            End If

        Next
    End Sub

    Private Function fcalibracion(ByVal COD As String, tipo As Integer) As ArrayList
        Dim RESP As New ArrayList
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT NCALI_118,FCALI_118,VTO_118 FROM T_CALIBRACION_118 WHERE NSERIE_118 = @D1 AND TIPO_118=@D2 AND FBAJA_118 IS NULL", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", COD))
            ADT.Parameters.Add(New SqlParameter("D2", tipo))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                RESP.Add(LECTOR.GetValue(0))
                RESP.Add(LECTOR.GetValue(1))
                RESP.Add(LECTOR.GetValue(2))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
        Return RESP
    End Function
#End Region
  

    


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'EXPORTO A EXCEL TODOS LOS DATOS DEL DATAGRIDVIEW
        If DGV1.RowCount <> 0 Then
            Dim nserie, ctipo, tipo, marca, modelo, chapa, estado, codalmacen, almacen, obs, ncali, fcali, vtocali, nverif, fverif, vtoverif As String
            If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
                My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
            End If
            'EXPORTO, VOY A LA BASE Y TRAIGO TODO....
            Try

                Dim fichero As String = "C:\Archivo\Maquinas_" + CB_ESTADO.Text.Replace(Chr(32), "_") + "_" + DateTime.Now.ToString("dd_MM_yyyy") + ".csv"
                Dim a As New StreamWriter(fichero)
                a.WriteLine("NSERIE;COD_TIPO;TIPO;MARCA;MODELO;CHAPA;ESTADO;COD_ALMACEN;ALMACEN;OBSERVACIONES;NCALIBRACION;F_CALIBRACION;VTO_CALIBRACION;NVERIFICACION;F_VERIFICACION;VTO_VERIFICACION")
                For i = 0 To DGV1.RowCount - 1
                    nserie = DGV1.Item(0, i).Value
                    ctipo = DGV1.Item(1, i).Value
                    tipo = DGV1.Item(2, i).Value
                    marca = DGV1.Item(3, i).Value
                    modelo = DGV1.Item(4, i).Value
                    chapa = DGV1.Item(5, i).Value
                    estado = DGV1.Item(6, i).Value
                    codalmacen = DGV1.Item(7, i).Value
                    almacen = DGV1.Item(8, i).Value
                    If IsDBNull(DGV1.Item(9, i).Value) Then
                        obs = ""
                    Else
                        obs = DGV1.Item(9, i).Value
                    End If
                    If IsDBNull(DGV1.Item(14, i).Value) Then
                        ncali = "--"
                    Else
                        ncali = DGV1.Item(14, i).Value
                    End If
                    If IsDBNull(DGV1.Item(15, i).Value) Then
                        fcali = "--"
                    Else
                        fcali = CDate(DGV1.Item(15, i).Value).ToShortDateString
                    End If
                    If IsDBNull(DGV1.Item(16, i).Value) Then
                        vtocali = "--"
                    Else
                        vtocali = CDate(DGV1.Item(16, i).Value).ToShortDateString
                    End If
                    If IsDBNull(DGV1.Item(17, i).Value) Then
                        nverif = "--"
                    Else
                        nverif = DGV1.Item(17, i).Value
                    End If
                    If IsDBNull(DGV1.Item(18, i).Value) Then
                        fverif = "--"
                    Else
                        fverif = CDate(DGV1.Item(18, i).Value).ToShortDateString
                    End If
                    If IsDBNull(DGV1.Item(19, i).Value) Then
                        vtoverif = "--"
                    Else
                        vtoverif = CDate(DGV1.Item(19, i).Value).ToShortDateString
                    End If
                    a.WriteLine(nserie + ";" + ctipo + ";" + tipo + ";" + marca + ";" + modelo + ";" + chapa + ";" + estado + ";" + codalmacen + ";" + almacen + ";" + obs + ";" + ncali + ";" + fcali + ";" + vtocali + ";" + nverif + ";" + fverif + ";" + vtoverif)
                Next
                a.Close()
                mensaje.MADVE002(fichero)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        'FIN............................



    End Sub


   
   
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If CB_ESTADO.SelectedValue <> Nothing Then
            LLENAR(CB_ESTADO.SelectedValue)
        End If
    End Sub
End Class