Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic



Public Class PALMA019
    Private VAlmacen As String = "0"
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen
    Private estado As Integer = 1
    Private contador As Integer = -1

    Private Sub PALMA019_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_almacen()
        llenar_estado()
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()
    End Sub

    Private Sub llenar_almacen()
        ComboBox3.Items.Add("")
        Dim dataset1 As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NDOC_003, (APELL_003+ ' ' +NOMB_003) AS NOMBRE FROM M_PERS_003 WHERE (ALMA_003 = 1 or DEPO_003 = 1) AND F_BAJA_003 is NULL order by NOMBRE", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(dataset1, "M_PERS_003")
        cnn2.Close()
        ComboBox3.DataSource = dataset1.Tables("M_PERS_003")
        ComboBox3.ValueMember = "NDOC_003"
        ComboBox3.DisplayMember = "NOMBRE"
        ComboBox3.Text = Nothing
    End Sub

    Private Sub LLENAR_DW1(ByVal ALMACEN As String, ByVal estado_2 As Integer)
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("SELECT T_ALMA_103_1.C_MATE_103, M_MATE_002.DESC_002, T_ALMA_103_1.N_CANT_103 FROM T_ALMA_103 AS T_ALMA_103_1 INNER JOIN M_MATE_002 ON T_ALMA_103_1.C_MATE_103 = M_MATE_002.CMATE_002 WHERE (T_ALMA_103_1.ESTA_103 = @D2) AND (T_ALMA_103_1.C_ALMA_103 = @D1) AND (M_MATE_002.SERI_002 = 1) AND (T_ALMA_103_1.N_CANT_103 <> 0)", cnn4)
        comando4.Parameters.Add(New SqlParameter("D1", ALMACEN))
        comando4.Parameters.Add(New SqlParameter("D2", estado_2))
        comando4.ExecuteNonQuery()
        Dim LECTOR1 As SqlDataReader = comando4.ExecuteReader()
        DataGridView2.Rows.Clear()
        While LECTOR1.Read
            DataGridView2.Rows.Add(LECTOR1.GetValue(0), LECTOR1.GetString(1), LECTOR1.GetValue(2))
        End While
        DataGridView1.Rows.Clear()
        cnn4.Close()
    End Sub
    Private Sub LLENAR_DW2(ByVal CODMATE As String, ByVal ALMACEN As String, ByVal estado_2 As Integer)
        contador = 0
        Dim nserie As String
        Dim fecha As String
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("SELECT NSERIE_102, F_INFO_102 FROM T_MEDI_102 WHERE CMATE_102 = @D1 AND CALMA_102=@D2 AND ESTADO_102=@D3", cnn4)
        comando4.Parameters.Add(New SqlParameter("D1", CODMATE))
        comando4.Parameters.Add(New SqlParameter("D2", ALMACEN))
        comando4.Parameters.Add(New SqlParameter("D3", estado_2))
        comando4.ExecuteNonQuery()
        Dim LECTOR1 As SqlDataReader = comando4.ExecuteReader()
        DataGridView1.Rows.Clear()
        While LECTOR1.Read
            nserie = LECTOR1.GetValue(0).ToString.PadLeft(8, "0")
            If IsDBNull(LECTOR1.GetValue(1)) = False Then
                fecha = CDate(LECTOR1.GetValue(1)).ToShortDateString
            Else
                fecha = ""
            End If
            DataGridView1.Rows.Add(nserie, fecha)
            contador += 1
        End While
        cnn4.Close()

    End Sub



    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.ValueMember <> Nothing Then
            estado = ComboBox2.SelectedValue
            VAlmacen = ComboBox3.SelectedValue
            If ComboBox3.Text <> Nothing Then
                LLENAR_DW1(VAlmacen, estado)
            End If
        End If
    End Sub

    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        If DataGridView2.Rows.Count <> 0 Then
            LLENAR_DW2(Me.DataGridView2.Item(0, DataGridView2.CurrentRow.Index).Value, VAlmacen, estado)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        If ComboBox3.Text <> Nothing Then
            If DataGridView2.Rows.Count <> 0 Then
                Try
                    Button3.Enabled = False
                    Archivo_Almacen()
                    ComboBox3.Text = Nothing
                    DataGridView1.Rows.Clear()
                    DataGridView2.Rows.Clear()
                    ProgressBar1.Visible = False

                    Button3.Enabled = True
                Catch ex As Exception
                    mensaje.MERRO001()

                    Button3.Enabled = True
                End Try
            Else
                mensaje.MERRO011()
            End If
        Else
            Dim res As DialogResult
            res = MessageBox.Show("Desea generar un archivo con el stock de medidores ", "CREAR ARCHIVO CON STOCK DE MEDIDORES", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If res = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Button3.Enabled = False
                    Archivo_total2()
                    ProgressBar1.Visible = False
                    Button3.Enabled = True
                Catch ex As Exception
                    mensaje.MERRO001()
                    Button3.Enabled = True

                End Try
            End If
        End If
    End Sub
    Private Sub llenar_estado()
        'CONECTO LA BASE
        Dim DS_estado As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 10 AND (C_PARA_802 = 1 OR C_PARA_802 = 9) order by C_PARA_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_estado, "DET_PARAMETRO_802")
        cnn2.Close()
        ComboBox2.DataSource = DS_estado.Tables("DET_PARAMETRO_802")
        ComboBox2.DisplayMember = "DESC_802"
        ComboBox2.ValueMember = "C_PARA_802"

    End Sub
    

    

    Public Sub Archivo_Almacen()
        Dim fichero As String = "C:\Archivo\Stock_Medidores_" + metodos.NOMBRE_DEPOSITO(VAlmacen).ToString + "_" + Date.Now.Day.ToString.PadLeft(2, "0") + Date.Now.Month.ToString.PadLeft(2, "0") + Date.Now.Year.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("N_SERIE;COD_MATERIAL; DESC_MATERIAL;FECH_ALTA;ALMACEN;FECH_INFO")
        Dim nserie As String
        Dim fecha As String
        Dim cant As Integer = 0
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("SELECT NSERIE_102, CMATE_102, CALMA_102, F_INFO_102, F_ALTA_102 FROM T_MEDI_102 WHERE CALMA_102=@D2 AND ESTADO_102=1", cnn4)
        comando4.Parameters.Add(New SqlParameter("D2", VAlmacen))
        comando4.ExecuteNonQuery()
        Dim LECTOR1 As SqlDataReader = comando4.ExecuteReader()
        While LECTOR1.Read
            cant += 1
        End While

        LECTOR1.Close()
        LECTOR1 = comando4.ExecuteReader
        ProgressBar1.Visible = True
        ProgressBar1.Maximum = cant
        ProgressBar1.Minimum = 0
        ProgressBar1.Value = 0
        While (LECTOR1.Read)
            ProgressBar1.Value += 1
            nserie = LECTOR1.GetValue(0).ToString.PadLeft(8, "0")
            If IsDBNull(LECTOR1.GetValue(3)) = False Then
                fecha = CDate(LECTOR1.GetValue(3)).ToShortDateString
            Else
                fecha = ""
            End If
            a.WriteLine(nserie + ";" + LECTOR1.GetValue(1) + ";" + metodos.detalle_material(LECTOR1.GetValue(1)).ToString + ";" + LECTOR1.GetDateTime(4).ToShortDateString + ";" + metodos.NOMBRE_DEPOSITO(LECTOR1.GetValue(2)).ToString + ";" + fecha.ToString)
        End While
        cnn4.Close()
        a.Close()
        mensaje.MADVE002(fichero)
    End Sub
    

    Private Function Estado_medidor(ByVal D1 As Integer) As String
        Dim resp As String = ""
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802=10 AND C_PARA_802 = @D1", con1)
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
    Private Sub Archivo_total2()
        Dim fichero As String = "C:\Archivo\Stock_Medidores_EXGADET_" + Date.Now.Day.ToString.PadLeft(2, "0") + Date.Now.Month.ToString.PadLeft(2, "0") + Date.Now.Year.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("N_SERIE;COD_MATERIAL;DESC_MATERIAL;FECH_ALTA;ALMACEN;FECH_INFO;FECH_UTILIZADO;ESTADO;P_ERRO;MOTIVO")
        Dim nserie As String = ""
        Dim codmate As String = ""
        Dim descmat As String = ""
        Dim fechaalta As String = ""
        Dim almacen As String = ""
        Dim fecha As String = ""
        Dim estado As String = ""
        Dim cant As Integer = 0
        Dim perror As String
        Dim motivo As String
        Dim futil As String
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("SELECT T_MEDI_102.NSERIE_102, T_MEDI_102.CMATE_102, M_MATE_002.DESC_002, T_MEDI_102.F_ALTA_102, (M_PERS_003.APELL_003+ ' ' + M_PERS_003.NOMB_003) AS NOMBRE, T_MEDI_102.F_INFO_102, DET_PARAMETRO_802.DESC_802, T_MEDI_102.POLIZA_E_102, T_MEDI_102.MOTIVO_102, F_UTIL_102 FROM T_MEDI_102 INNER JOIN M_MATE_002 ON T_MEDI_102.CMATE_102 = M_MATE_002.CMATE_002 INNER JOIN DET_PARAMETRO_802 ON T_MEDI_102.ESTADO_102 = DET_PARAMETRO_802.C_PARA_802 INNER JOIN M_PERS_003 ON T_MEDI_102.CALMA_102 = M_PERS_003.NDOC_003 WHERE (DET_PARAMETRO_802.C_TABLA_802 = 10) AND (T_MEDI_102.ESTADO_102 = 0 OR T_MEDI_102.ESTADO_102 = 1 or T_MEDI_102.ESTADO_102 = 9 or T_MEDI_102.ESTADO_102 = 6 or T_MEDI_102.ESTADO_102 = 2 or T_MEDI_102.ESTADO_102 = 7)", cnn4)
        comando4.ExecuteNonQuery()
        Dim LECTOR1 As SqlDataReader = comando4.ExecuteReader()
        While LECTOR1.Read
            cant += 1
        End While

        LECTOR1.Close()
        LECTOR1 = comando4.ExecuteReader
        ProgressBar1.Visible = True
        ProgressBar1.Maximum = cant
        ProgressBar1.Minimum = 0
        ProgressBar1.Value = 0

        While LECTOR1.Read
            ProgressBar1.Value += 1
            nserie = LECTOR1.GetValue(0).ToString.PadLeft(8, "0")
            codmate = LECTOR1.GetValue(1)
            descmat = LECTOR1.GetValue(2)
            fechaalta = LECTOR1.GetDateTime(3).ToShortDateString
            almacen = LECTOR1.GetValue(4)
            If IsDBNull(LECTOR1.GetValue(5)) = False Then
                fecha = CDate(LECTOR1.GetValue(5)).ToShortDateString
            Else
                fecha = ""
            End If
            estado = LECTOR1.GetValue(6)

            If IsDBNull(LECTOR1.GetValue(7)) = False Then
                perror = LECTOR1.GetValue(7)
            Else
                perror = ""
            End If
            If IsDBNull(LECTOR1.GetValue(8)) = False Then
                motivo = LECTOR1.GetValue(8)
            Else
                motivo = ""
            End If
            If IsDBNull(LECTOR1.GetValue(9)) = False Then
                futil = LECTOR1.GetDateTime(9).ToShortDateString
            Else
                futil = ""
            End If
            a.WriteLine(nserie + ";" + codmate + ";" + descmat + ";" + fechaalta + ";" + almacen + ";" + fecha + ";" + futil + ";" + estado + ";" + perror + ";" + motivo)

        End While
        cnn4.Close()
        a.Close()

        mensaje.MADVE002(fichero)
    End Sub

    
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

        If ComboBox3.ValueMember <> Nothing Then

            VAlmacen = ComboBox3.SelectedValue

            If ComboBox3.Text <> Nothing Then
                estado = ComboBox2.SelectedValue
                LLENAR_DW1(VAlmacen, estado)
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.DataGridView2.SelectedRows.Count > 0 Then
            If contador = 0 Then
                Dim dr As DialogResult = MessageBox.Show("Esta Seguro que Quiere Ajustar el Material Seleccionado?", "ATENCION", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                If dr = DialogResult.Yes Then
                    Dim con3 As SqlConnection = New SqlConnection(conexion)
                    con3.Open()
                    'si leeo el lector incremento el stock
                    Dim comando2 As New SqlClient.SqlCommand("Update T_ALMA_103 set N_CANT_103=@Cantidad WHERE C_MATE_103=@MATE AND C_ALMA_103=@EQUI AND ESTA_103=@std", con3)
                    'creo el lector de parametros
                    comando2.Parameters.Add(New SqlParameter("MATE", Me.DataGridView2.Item(0, DataGridView2.CurrentRow.Index).Value))
                    comando2.Parameters.Add(New SqlParameter("EQUI", VAlmacen))
                    comando2.Parameters.Add(New SqlParameter("Cantidad", 0))
                    comando2.Parameters.Add(New SqlParameter("std", estado))
                    comando2.ExecuteNonQuery()
                    con3.Close()
                    If ComboBox3.Text <> Nothing Then
                        LLENAR_DW1(VAlmacen, estado)
                    End If
                End If
            End If
        End If
    End Sub
End Class