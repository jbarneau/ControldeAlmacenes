Imports System.Data.SqlClient

Public Class PALMA036BIS

    Private DT_remito As New DataTable
    Private Data As New DataTable
    Private tipoyprove As New DataTable
    Private remito As Decimal
    Private fecha As Date
    Private ADJUNTO As String
    Private ADJUNTOPDF As String
    Private deposito As String
    Private familia As String
    Private proov As String
    Private BODY As String
    Private TABLABODY As String
    Private resumen As New DataTable
    Private cantmedsrem As Integer

    Public Property Propprov() As String
        Get
            Return proov
        End Get
        Set(ByVal value As String)
            proov = value
        End Set
    End Property

    Public Property PropFamilia() As String
        Get
            Return familia
        End Get
        Set(ByVal value As String)
            familia = value
        End Set
    End Property

    Public Property Propdepo() As String
        Get
            Return deposito
        End Get
        Set(ByVal value As String)
            deposito = value
        End Set
    End Property

    Public Sub New(ByVal Familia As String, ByVal Deposito As String, ByVal proveedor As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        PropFamilia() = Familia
        Propdepo() = Deposito
        Propprov() = proveedor
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub

    Private Sub PALMA036BIS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LLenarRemitosPendientes()
        If _usr.Obt_Almacen <> "0" Then
            deposito = _usr.Obt_Almacen
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DT_remito.Clear()
        resumen.Clear()
        For i = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Selected = True Then
                fecha = Date.Now.ToShortDateString
                remito = Convert.ToDecimal(ListView1.Items(i).SubItems.Item(0).Text)
                familia = Convert.ToDecimal(ListView1.Items(i).SubItems.Item(3).Text)
                proov = Convert.ToDecimal(ListView1.Items(i).SubItems.Item(4).Text)
                Buscar_Deposito(ListView1.Items(i).SubItems.Item(0).Text)
                llenar_dt_remito(remito)
                llenar_DT_resumen(remito)
                cantmedsrem = ListView1.Items(i).SubItems(3).Text
                If familia = 6 Then
                    Dim pantalla As New OpenFileDialog
                    pantalla.DereferenceLinks = True
                    pantalla.Filter = "JPG files (*.JPG)|*.JPG"
                    pantalla.FilterIndex = 1
                    pantalla.Title = "Seleccione el archivo"
                    pantalla.RestoreDirectory = False
                    pantalla.ShowDialog()
                    pantalla.AddExtension = False
                    pantalla.Multiselect = False
                    If pantalla.FileName = Nothing Then
                        ADJUNTO = "NO"
                    Else
                        ADJUNTO = pantalla.FileName
                    End If
                End If
                If familia <> 1 Then
                    Dim pantalla As New OpenFileDialog
                    pantalla.DereferenceLinks = True
                    pantalla.Filter = "PDF files (*.PDF)|*.PDF"
                    pantalla.FilterIndex = 1
                    pantalla.Title = "Seleccione el archivo"
                    pantalla.RestoreDirectory = False
                    pantalla.ShowDialog()
                    pantalla.AddExtension = False
                    pantalla.Multiselect = False
                    If pantalla.FileName = Nothing Then
                        ADJUNTOPDF = "NO"
                    Else
                        ADJUNTOPDF = pantalla.FileName
                    End If
                End If
                armar_archivo(remito, fecha)
                'ArmarBodyMail(ADJUNTO, ADJUNTOPDF)
                PrintDocument2.Print()
                PrintDocument2.Print()
                MessageBox.Show("PROCESO FINALIZADO CORRECTAMENTE.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'RemitoBaja(remito)
            End If
        Next
        borrar()
    End Sub


    Private Sub ListView1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        For I = 0 To ListView1.Items.Count - 1
            If ListView1.Items(I).Selected = True Then
                Dim pantalla As New PALMA036BIS2(ListView1.Items(I).SubItems.Item(1).Text, ListView1.Items(I).SubItems.Item(0).Text)
                pantalla.ShowDialog()
            End If
        Next
    End Sub

    Private Sub LLenarRemitosPendientes()
        Data.Clear()
        ListView1.Items.Clear()
        Dim linea As New ListViewItem
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        cnn.Open()
        'Dim adaptadaor As New SqlDataAdapter("SELECT T_MED_DEVO_113.NREMITO_113 AS NREMITO, T_MED_DEVO_113.CAJON_113 AS CAJON, T_MED_DEVO_113.FREMITO_113 AS FREMITO, COUNT(T_MED_DEVO_113.NSERI_113) AS CANTIDAD FROM T_MED_DEVO_113 INNER JOIN T_MED_DEVO_113 AS T_MED_DEVO_113_1 ON T_MED_DEVO_113.NSERI_113 = T_MED_DEVO_113_1.NSERI_113 WHERE (T_MED_DEVO_113.REMCOM_113 = 0) GROUP BY T_MED_DEVO_113.NREMITO_113, T_MED_DEVO_113.CAJON_113, T_MED_DEVO_113.FREMITO_113", cnn)
        'Dim adaptadaor As New SqlDataAdapter("SELECT T_MED_DEVO_113.NREMITO_113 AS NREMITO, T_MED_DEVO_113.CAJON_113 As CAJON, T_MED_DEVO_113.FREMITO_113 As FREMITO, COUNT(T_MED_DEVO_113.NSERI_113) As CANTIDAD, T_MED_DEVO_113.FAMILIA_113 as FAMILIA, T_MED_DEVO_113.PROVE_113 as PROOVEDOR From T_MED_DEVO_113 INNER Join T_MED_DEVO_113 As T_MED_DEVO_113_1 On T_MED_DEVO_113.NSERI_113 = T_MED_DEVO_113_1.NSERI_113 Where (T_MED_DEVO_113.REMCOM_113 = 0) Group By T_MED_DEVO_113.NREMITO_113, T_MED_DEVO_113.CAJON_113, T_MED_DEVO_113.FREMITO_113, T_MED_DEVO_113.FAMILIA_113, T_MED_DEVO_113.PROVE_113", cnn)
        Dim adaptadaor As New SqlDataAdapter("SELECT T_MED_DEVO_113.NREMITO_113 AS NREMITO, T_MED_DEVO_113.FREMITO_113 AS FREMITO, COUNT(T_MED_DEVO_113.NSERI_113) AS CANTIDAD, T_MED_DEVO_113.FAMILIA_113 AS FAMILIA, T_MED_DEVO_113.PROVE_113 AS PROOVEDOR FROM T_MED_DEVO_113 INNER JOIN T_MED_DEVO_113 AS T_MED_DEVO_113_1 ON T_MED_DEVO_113.NSERI_113 = T_MED_DEVO_113_1.NSERI_113 WHERE (T_MED_DEVO_113.REMCOM_113 = 0) GROUP BY T_MED_DEVO_113.NREMITO_113, T_MED_DEVO_113.FREMITO_113, T_MED_DEVO_113.FAMILIA_113, T_MED_DEVO_113.PROVE_113", cnn)
        adaptadaor.Fill(Data)
        cnn.Close()
        For i = 0 To Data.Rows.Count - 1
            linea = New ListViewItem(Data.Rows(i).Item(0).ToString)
            linea.SubItems.Add(Convert.ToDateTime(Data.Rows(i).Item(1).ToString).ToShortDateString())
            linea.SubItems.Add(Data.Rows(i).Item(2).ToString)
            linea.SubItems.Add(Data.Rows(i).Item(3).ToString)
            linea.SubItems.Add(Data.Rows(i).Item(4).ToString)
            'linea.SubItems.Add(Data.Rows(i).Item(4).ToString)
            ListView1.Items.Add(linea)
        Next
    End Sub


    Private Sub borrar()
        LLenarRemitosPendientes()
    End Sub

    Private Sub armar_archivo(ByVal remito As Decimal, ByVal fecha As Date)
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        Dim fichero As String = "C:\Archivo\Remito_Devolucion_" + remito.ToString.PadLeft(8, "0") + ".csv"
        ADJUNTO = fichero
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("N_SERIE;CAPACIDAD;CONTRATO;REMITO;FECHA_REMITO")
        Dim NMEDIDOR As String
        Dim CAPADIDAD As String
        Dim CONTRATO As String
        For I = 0 To DT_remito.Rows.Count - 1
            NMEDIDOR = DT_remito.Rows(I).Item(0)
            CAPADIDAD = DT_remito.Rows(I).Item(1)
            CONTRATO = ContratoMed(DT_remito.Rows(I).Item(2))
            a.WriteLine(NMEDIDOR.PadLeft(8, "0") + ";" + DETATIPO(CAPADIDAD) + ";" + CONTRATO.ToString + ";" + remito.ToString + ";" + fecha.ToShortDateString)
        Next
        a.Close()
        'MENSAJE.MADVE002(fichero)
    End Sub


    Private Sub llenar_dt_remito(ByVal nremi As String)
        DT_remito.Clear()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim adaptador As New SqlDataAdapter("SELECT  NSERI_113, CMATE_113, CONTRATO_113 FROM T_MED_DEVO_113 WHERE (NREMITO_113 = @D1) ORDER BY  CONTRATO_113, CMATE_113", cnn2)
        adaptador.SelectCommand.Parameters.Add(New SqlParameter("D1", nremi))
        adaptador.Fill(DT_remito)
        cnn2.Close()
    End Sub

    Private Function DETATIPO(ByVal codmat As String) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_002 FROM M_MATE_002 WHERE CMATE_002 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", codmat))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString Then
            resp = lector1.GetValue(0)
        End If
        con1.Close()
        Return resp
    End Function

    Private Sub RemitoBaja(ByVal nrem As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        cnn.Open()
        Dim cmd As New SqlCommand("UPDATE T_MED_DEVO_113 SET REMCOM_113 = 1 WHERE NREMITO_113 = @P1", cnn)
        cmd.Parameters.AddWithValue("P1", nrem)
        cmd.ExecuteNonQuery()
        cnn.Close()
    End Sub

    Private Sub Buscar_Deposito(ByVal DEPO As String)
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        cnn2.Open()
        Dim cmd As New SqlCommand("SELECT DEPOSI_113 FROM T_MED_DEVO_113 WHERE (NREMITO_113 = @depo)", cnn2)
        cmd.Parameters.AddWithValue("depo", DEPO)
        deposito = cmd.ExecuteScalar().ToString()
        cnn2.Close()
    End Sub

    Private Sub ArmarBodyMail(ByVal archivo As String, ByVal PDF As String)
        Dim n_REMITO As String
        provytipo(remito)
        PropFamilia = OBTFAMILIA(tipoyprove.Rows(0).Item(1))
        Propprov = OBTPROVE(tipoyprove.Rows(0).Item(0))
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + remito.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + remito.ToString.PadLeft(8, "0")
        End If
        BODY = "<body><table width='897' height='1017' border='0'> <tr><td height='103' bordercolor='#F0F0F0'><img src='http://www.exgadetsa.com.ar/png/exgadet sin DS.png' width='143' height='111'/></td><td colspan='2'><div align='center' class='Estilo1'>DEVOLUCION DE MEDIDORES EXGADET</div></td></tr><tr><td width='112' height='31' bordercolor='#F0F0F0'>&nbsp;</td><td colspan='2'><div align='right'>REMITO N&ordm; - " + n_REMITO + "</div></td></tr> <tr><td height='31' colspan='2' bordercolor='#F0F0F0'><div align='right' class='Estilo4'>PROVEEDOR</div></td> <td width='637' bordercolor='#F0F0F0'><span class='Estilo4'><blockquote>" + Propprov + "</blockquote></span></td></tr><tr><td height='31' colspan='2' bordercolor='#F0F0F0'><div align='right' class='Estilo4'>TIPO</div></td><td width='637' bordercolor='#F0F0F0'><span class='Estilo4'><blockquote>" + PropFamilia + "</blockquote></span></td></tr><tr><td height='31' colspan='2' bordercolor='#F0F0F0'><div align='right' class='Estilo4'>TOTAL</div></td><td width='637' bordercolor='#F0F0F0'><span class='Estilo4'><blockquote>" + DT_remito.Rows.Count.ToString + "</blockquote></span></td></tr><tr><td height='776' colspan='3' valign='top'><div align='center'> <table width='886' border='1'><tr><td width='506'><div align='center'>CAPACIDAD</div></td><td width='238'><div align='center'>CONTRATO</div></td><td width='120'><div align='center'>CANTIDAD</div></td></tr>"
        Dim contrato As String
        For I = 0 To resumen.Rows.Count - 1
            contrato = ContratoMed(resumen.Rows(I).Item(0))
            TABLABODY = TABLABODY + "<tr><td>" + resumen.Rows(I).Item(1).ToString + "-" + DETATIPO(resumen.Rows(I).Item(1).ToString) + "</td><td><div align='center'>" + contrato + "</div></td><td><div align='center'>" + resumen.Rows(I).Item(2).ToString + "</div></td></tr>"
        Next
        BODY = BODY + TABLABODY + "</table></div></td></tr></table></body>"
        ENVIAR_MENSAJE(Direcciones_mail(1).ToString, n_REMITO.ToString, BODY.ToString, archivo, PDF)
    End Sub

    Public Sub ENVIAR_MENSAJE(ByVal DIRECCION As String, ByVal NREMITO As String, ByVal _BODY As String, ByVal Archivo As String, ByVal PDF As String)

        Dim _mensage As New System.Net.Mail.MailMessage
        Dim _Smtp As New System.Net.Mail.SmtpClient
        _Smtp.Credentials = New System.Net.NetworkCredential(MAIN.mail, MAIN.passmail)
        _Smtp.Host = MAIN.smtpmail
        _Smtp.Port = MAIN.puertomail
        _Smtp.EnableSsl = False
        'configuracion del mensaje
        _mensage.To.Add(DIRECCION.ToString)
        _mensage.From = New System.Net.Mail.MailAddress("almacensis@exgadetsa.com.ar", "REMITO MEDIDORES DEVOLUCION N" + NREMITO, System.Text.Encoding.UTF8)
        _mensage.Subject = "REMITO DE ENTREGA DE MEDIDORES N" + NREMITO
        _mensage.SubjectEncoding = System.Text.Encoding.UTF8
        _mensage.Body = _BODY.ToString
        If Archivo <> "NO" Then
            If Archivo <> Nothing Then
                _mensage.Attachments.Add(New System.Net.Mail.Attachment(Archivo))
            End If
        End If
        If PDF <> "NO" Then
            If PDF <> Nothing Then
                _mensage.Attachments.Add(New System.Net.Mail.Attachment(PDF))
            End If
        End If
        _mensage.BodyEncoding = System.Text.Encoding.UTF8
        _mensage.Priority = System.Net.Mail.MailPriority.High
        _mensage.IsBodyHtml = True
        'enviar
        Try
            _Smtp.Send(_mensage)
        Catch ex As System.Net.Mail.SmtpException
            MessageBox.Show("NO SE PUDO ENVIAR EL MENSAJE ERROR")
        End Try
    End Sub

    Private Sub llenar_DT_resumen(ByVal remito As Decimal)
        resumen.Clear()
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        Try
            cnn2.Open()
            'GENERO UN ADAPTADOR
            Dim adaptadaor As New SqlDataAdapter("SELECT CONTRATO_113, CMATE_113, COUNT(NSERI_113) AS CANT FROM T_MED_DEVO_113 WHERE (NREMITO_113 = @D1) GROUP BY CONTRATO_113, CMATE_113 ORDER BY  CONTRATO_113, CMATE_113", cnn2)
            'LLENO EL ADAPTADOR CON EL DATASET
            adaptadaor.SelectCommand.Parameters.Add(New SqlParameter("D1", remito))
            adaptadaor.Fill(resumen)
            If resumen.Columns.Contains("OBS") = False Then
                resumen.Columns.Add("OBS")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn2.Close()

        End Try
    End Sub

    Public Function OBTPROVE(ByVal DNI As String) As String
        Dim RESP As String = ""
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As SqlCommand = cnn1.CreateCommand
        Comando.CommandText = "SELECT RAZON_007 FROM M_PROV_MED_RET_007 where MCUIT_007= @USRS"
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("USRS", DNI))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read Then
            RESP = Dusrs.GetValue(0)
        End If
        cnn1.Close()
        Return RESP
    End Function

    Private Function OBTFAMILIA(COD As String) As String
        Dim RESP As String = ""
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("select C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 15 and F_BAJA_802 IS NULL and C_PARA_802 = @D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", COD))
            Dim lector As SqlDataReader = adt.ExecuteReader
            If lector.Read Then
                RESP = lector.GetValue(1)
            End If
        Catch ex As Exception

        End Try
        Return RESP
    End Function

    Private Sub PrintDocument2_PrintPage(sender As System.Object, e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument2.PrintPage
        Dim fecha As Date
        Dim deposito As String = ""
        Dim texto_familia As String = ""
        Dim confecciono As String = ""
        Dim cant As Decimal = 0
        Dim proveedor As String = ""
        Dim tabla As New DataTable
        Dim cnn As New SqlConnection(conexion)
        Dim R1_T1 As String = ""
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = ""
        Dim R2_T2 As String = ""
        Dim R3_T1 As String = ""
        Dim R3_T2 As String = ""

        Try
            cnn.Open()
            Dim adt As New SqlCommand("select CPROV_115, FECHA_115, CANT_115, FAMILIA_115 FROM C_REMITO_DEV_115 WHERE NREMITO_115=@D1", cnn)
            adt.Parameters.Add(New SqlParameter("D1", remito))
            Dim lecor As SqlDataReader = adt.ExecuteReader
            If lecor.Read Then
                fecha = lecor.GetDateTime(1)
                cant = lecor.GetValue(2)
                deposito = "GENERAL THAMES"
                confecciono = _usr.Obt_Nombre_y_Apellido
                texto_familia = OBTFAMILIA(lecor.GetValue(3))
                proveedor = OBTPROVE(lecor.GetValue(0)) + "(" + lecor.GetValue(0) + ")"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Try
            cnn.Open()
            Dim adt2 As New SqlDataAdapter("select CODMATE_116,CONTRA_116,CANT_116 FROM D_REMITO_DEV_116 WHERE NREMITO_116=@D1", cnn)
            adt2.SelectCommand.Parameters.Add(New SqlParameter("D1", remito))
            adt2.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        Dim n_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            n_REMITO = "0100" + "-" + remito.ToString.PadLeft(8, "0")
        Else
            n_REMITO = _usr.Obt_Almacen.ToString.PadLeft(4, "0") + "-" + remito.ToString.PadLeft(8, "0")
        End If        'DEFINO LAS VARIABLES
        R1_T1 = "DEVOLUCION DE MEDIDORES"
        R1_T2 = "PROVEEDOR: " + proveedor
        R2_T1 = "DEPOSITO: " + deposito
        R2_T2 = "TIPO DE MEDIDORE:" + texto_familia
        R3_T1 = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "TOTAL DE MEDIDORES: " + cant.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 800, 1140)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(n_REMITO.ToString, New Font("ARIAL", 16, FontStyle.Bold), Brushes.Black, 435, 73)
        e.Graphics.DrawString(fecha.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 215)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 247)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 280)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 215)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 247)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 280)
        Dim CONTRATO As String
        'ESCRIBO EL ENCABEZADO DEL DETALLE
        e.Graphics.DrawString("CAPACIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 325)
        e.Graphics.DrawString("CONTRATO", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 350, 325)
        e.Graphics.DrawString("CANTIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 690, 325)
        'RECORRO EL DATA
        Dim cont As Integer = tabla.Rows.Count - 1
        For I = 0 To cont
            CONTRATO = ContratoMed(tabla.Rows(I).Item(1))
            e.Graphics.DrawString(tabla.Rows(I).Item(0).ToString + "-" + DETATIPO(tabla.Rows(I).Item(0).ToString), New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(CONTRATO.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 400, LINEA)
            e.Graphics.DrawString(tabla.Rows(I).Item(2).ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 710, LINEA)
            LINEA += SALTO
        Next

    End Sub

    Private Sub provytipo(ByVal nrem As String)
        tipoyprove.Clear()
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        cnn.Open()
        Dim cmd As New SqlCommand("select CPROV_115, FAMILIA_115 FROM C_REMITO_DEV_115 WHERE NREMITO_115= @D1", cnn)
        cmd.Parameters.AddWithValue("D1", nrem)
        Dim adaptadaor As New SqlDataAdapter(cmd)
        adaptadaor.Fill(tipoyprove)
        cnn.Close()
    End Sub
End Class