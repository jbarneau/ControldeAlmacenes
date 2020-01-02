Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql

Public Class PALMA015
    Private _nremito As Decimal
    Private fecha As Date
    Private alma_entrega As String
    Private alma_recive As String
    Private t_movimiento As Integer
    Private MENSAJE As New Clase_mensaje
    Private METODOS As New Clas_Almacen



    Private Sub PALMA015_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
    End Sub

    Private Sub Buscar_Remito(ByVal remi As Decimal)

    End Sub


   

    Private Function NOMBRE_DEPOSITO(ByVal str As String) As String
        Dim resp As String = "error"
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select NOMB_003 FROM M_PERS_003 WHERE NDOC_003 = @D1", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.Parameters.Add(New SqlParameter("D1", str))
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            resp = Dusrs.GetString(0)
        End If
        cnn1.Close()
        Return resp
    End Function

    Private Function detalle_material(ByVal codmat As String) As String
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

    Private Function Encontrar_remito(ByVal nremito) As Boolean
        Dim RESP As Boolean = False
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT N_REMI_104,F_ALTA_104,ALMAE_104,ALMAR_104,T_MOV_104 FROM T_REMI_104 WHERE N_REMI_104 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString Then
            RESP = True
            nremito = lector1.GetValue(0)
            fecha = lector1.GetValue(1)
            alma_entrega = lector1.GetValue(2)
            alma_recive = lector1.GetValue(3)
            t_movimiento = lector1.GetValue(4)
        End If
        con1.Close()
        Return RESP
    End Function

    Private Sub Buscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Buscar.Click
        If IsNumeric(TextBox1.Text) And TextBox1.Text <> Nothing Then
            If Encontrar_remito(TextBox1.Text) = True Then
                _nremito = TextBox1.Text
                TextBox4.Text = fecha.ToShortDateString
                TextBox2.Text = METODOS.NOMBRE_DEPOSITO(alma_entrega)
                TextBox3.Text = METODOS.NOMBRE_DEPOSITO(alma_recive)
                TextBox5.Text = detalle_movimientol(t_movimiento)
                llenardw(_nremito)
                If DataGridView1.RowCount <> 0 Then
                    B_Entregar.Enabled = True
                End If
            Else
                B_Entregar.Enabled = False
                TextBox4.Text = Nothing
                TextBox2.Text = Nothing
                TextBox3.Text = Nothing
                TextBox5.Text = Nothing
                TextBox1.Text = Nothing
                DataGridView1.Rows.Clear()
                MENSAJE.MERRO011()
            End If
        Else
            MENSAJE.MERRO006()
            TextBox1.SelectAll()
            TextBox1.Focus()
        End If
    End Sub

    Private sub llenardw(ByVal nremito) 
        DataGridView1.Rows.Clear()
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT C_MATE_104, CANT_104 FROM T_REMI_104 WHERE N_REMI_104 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremito))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            DataGridView1.Rows.Add(lector1.GetValue(0), detalle_material(lector1.GetValue(0)), lector1.GetValue(1))
        End While
        con1.Close()

    End Sub

    Private Function detalle_movimientol(ByVal codmat As Integer) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 1 AND C_PARA_802 = @D1", con1)
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

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        '#######################datos##################################
        Dim R1_T1 As String = ""
        Dim R1_T2 As String = ""
        Dim R2_T1 As String = ""
        Dim R2_T2 As String = ""
        Dim R3_T1 As String = ""
        Dim R3_T2 As String = ""
        Dim N_REMITO As String
        If _usr.Obt_Almacen = 0 Then
            N_REMITO = "0100" + "-" + _nremito.ToString.PadLeft(8, "0")
        Else
            N_REMITO = alma_entrega.PadLeft(4, "0") + "-" + _nremito.ToString.PadLeft(8, "0")
        End If


        'DEFINO LAS VARIABLES
        R1_T1 = "TIPO DE MOVIMIENTO: " + TextBox5.Text
        R1_T2 = ""
        R2_T1 = "ENTREGA: " + TextBox2.Text
        R2_T2 = "RECIBE: " + TextBox3.Text
        R3_T1 = "CONFECCIONO: " + _usr.Obt_Nombre_y_Apellido
        R3_T2 = "CANTIDAD DE ITEM: " + DataGridView1.RowCount.ToString

        'DEFINO LA LINEA DEL REMITO Y EL SALTO
        Dim LINEA As Integer = 356
        Dim SALTO As Integer = 24
        'IMAGEN ######################################
        e.Graphics.DrawImage(MAIN.REMITO_IMAGEN, 0, 0, 800, 1140)
        'ESCRIBO EL REMITO Y LA FECHA
        e.Graphics.DrawString(N_REMITO.ToString, New Font("ARIAL", 16, FontStyle.Bold), Brushes.Black, 435, 73)
        e.Graphics.DrawString(fecha.ToString, New Font("ARIAL", 12, FontStyle.Regular), Brushes.Black, 435, 101)
        'ESCRIBO LOS RENGLONES
        e.Graphics.DrawString(R1_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 215)
        e.Graphics.DrawString(R2_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 247)
        e.Graphics.DrawString(R3_T1.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 280)

        e.Graphics.DrawString(R1_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 215)
        e.Graphics.DrawString(R2_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 247)
        e.Graphics.DrawString(R3_T2.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 450, 280)

        'ESCRIBO EL ENCABEZADO DEL DETALLE
        e.Graphics.DrawString("CODIGO", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, 325)
        e.Graphics.DrawString("DESCRIPCION", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 350, 325)
        e.Graphics.DrawString("CANTIDAD", New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 690, 325)
        'RECORRO EL DATA
        For I = 0 To DataGridView1.RowCount - 1
            e.Graphics.DrawString(Me.DataGridView1.Item(0, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 15, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(1, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 100, LINEA)
            e.Graphics.DrawString(Me.DataGridView1.Item(2, I).Value.ToString, New Font("ARIAL", 11, FontStyle.Regular), Brushes.Black, 715, LINEA)
            LINEA += SALTO
        Next

        Dim can As Integer = 1
        Dim asigmed As String = ""
        Dim dt As New DataTable()
        dt = TraerMeds(alma_recive, _nremito, fecha)
        LINEA = 984
        For i = 0 To dt.Rows.Count - 1
            If asigmed = "" Then
                asigmed = dt.Rows(i).Item(0).ToString
            Else
                asigmed = asigmed + "/" + dt.Rows(i).Item(0).ToString
            End If
            If can = 8 Or i = dt.Rows.Count - 1 Then
                e.Graphics.DrawString(asigmed.ToString, New Font("ARIAL", 9, FontStyle.Regular), Brushes.Black, 15, LINEA)
                asigmed = asigmed
                asigmed = ""
                can = 1
                LINEA += 15
            Else
                can += 1
            End If
        Next
    End Sub

    Private Sub B_Entregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B_Entregar.Click
        PrintDocument1.Print()
        TextBox4.Text = Nothing
        TextBox2.Text = Nothing
        TextBox3.Text = Nothing
        TextBox5.Text = Nothing
        TextBox1.Text = Nothing
        B_Entregar.Enabled = False
        DataGridView1.Rows.Clear()

    End Sub

    Public Function TraerMeds(ByVal almacen As String, ByVal nrem As Decimal, ByVal fremi As Date)
        Dim dt As New DataTable()
        Dim f1 As String = fremi.ToShortDateString()
        Dim f2 As String = fremi.ToShortDateString() + " " + "23:59:00"
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT T_MEDI_102.NSERIE_102, T_MEDI_102.CMATE_102, T_MOV_REMI_110.REMITO_110, T_MOV_REMI_110.FREMI_110 FROM T_MEDI_102 INNER JOIN T_MOV_REMI_110 ON T_MEDI_102.NSERIE_102 = T_MOV_REMI_110.NSERI_110 AND T_MEDI_102.CMATE_102 = T_MOV_REMI_110.CMATE_110 WHERE (T_MEDI_102.CALMA_102 = @ALMA) AND (T_MOV_REMI_110.REMITO_110 = @REM) AND (T_MOV_REMI_110.FREMI_110 BETWEEN @F1 AND @F2) AND (T_MEDI_102.ESTADO_102 = 1) ORDER BY T_MEDI_102.NSERIE_102", con1)
        comando1.Parameters.Add(New SqlParameter("ALMA", almacen))
        comando1.Parameters.Add(New SqlParameter("REM", nrem))
        comando1.Parameters.Add(New SqlParameter("F1", f1))
        comando1.Parameters.Add(New SqlParameter("F2", f2))
        Dim adapt As New SqlDataAdapter(comando1)
        'creo el lector de parametros
        adapt.Fill(dt)
        con1.Close()
        Return dt
    End Function
End Class