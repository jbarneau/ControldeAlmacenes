Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO

Public Class PPETI003
    Private oc As New Class_OC
    Private mensaje As New Clase_mensaje
    Private metodos As New Clas_Almacen
    Private NESTADO As Integer


 

    Private Sub PPETI003_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        llenar_estado()
    End Sub

    Private Sub llenar_estado()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802, DESC_802 FROM DET_PARAMETRO_802 where F_BAJA_802 is NULL AND C_TABLA_802 = 9 and (C_PARA_802 = 3 OR C_PARA_802 = 2) order by DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "DET_PARAMETRO_802")
        cnn2.Close()
        CB_estado.DataSource = DS_contrato.Tables("DET_PARAMETRO_802")
        CB_estado.DisplayMember = "DESC_802"
        CB_estado.ValueMember = "C_PARA_802"
        CB_estado.Text = Nothing
    End Sub

    Private Sub CB_estado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB_estado.SelectedIndexChanged
        If CB_estado.ValueMember <> Nothing Then
            If CB_estado.Text <> Nothing Then
                NESTADO = CB_estado.SelectedValue
                llenarDW1()
            End If
        End If
    End Sub

    Private Sub llenarDW1()
        Dim d1 As String = ""
        Dim d2 As String = ""
        Dim d3 As String = ""
        Dim d4 As String = ""
        Dim d5 As String = ""
        DataGridView1.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_C_OC_105.N_OC_105, dbo.T_C_OC_105.F_ALTA_105, dbo.T_C_OC_105.N_PETI_105, dbo.T_C_OC_105.FAPRO_105, dbo.M_CONT_004.DESC_004 FROM dbo.T_C_OC_105 INNER JOIN dbo.M_CONT_004 ON dbo.T_C_OC_105.CONT_105 = dbo.M_CONT_004.NCONT_004 WHERE (dbo.T_C_OC_105.TIPO_OC_105 = 2) AND (dbo.T_C_OC_105.ESTA_105 = @D1)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NESTADO))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        While lector1.Read
            d1 = lector1.GetValue(0).ToString.PadLeft(8, "0")
            d2 = lector1.GetDateTime(1).ToShortDateString
            If IsDBNull(lector1.GetValue(2)) = False Then
                d3 = lector1.GetValue(2)
            End If
            If IsDBNull(lector1.GetValue(3)) = False Then
                d4 = lector1.GetDateTime(3).ToShortDateString
            End If
            d5 = lector1.GetValue(4)
            Me.DataGridView1.Rows.Add(d1, d2, d3, d4, d5)
        End While
        Me.DataGridView2.Rows.Clear()
        'ciero la conexion
        con1.Close()
    End Sub
    Private Sub llenarDW2_estado2y3(ByVal nremi As Decimal)
        Dim mate As String = ""
        Dim desc As String = ""
        Dim soli As Decimal = 0
        Dim ent As Decimal = 0
        Dim UNID As String = ""
        DataGridView2.Rows.Clear()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("SELECT dbo.T_D_OC_106.C_MATE_106, dbo.M_MATE_002.DESC_002, dbo.T_D_OC_106.CANT_106, dbo.T_D_OC_106.CANTE_106, dbo.M_MATE_002.UNID_002 FROM dbo.T_D_OC_106 INNER JOIN dbo.M_MATE_002 ON dbo.T_D_OC_106.C_MATE_106 = dbo.M_MATE_002.CMATE_002 WHERE (dbo.T_D_OC_106.N_OC_106 = @D1) and (dbo.T_D_OC_106.CONF_106 = 1) ", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", nremi))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        While lector1.Read
            mate = lector1.GetValue(0)
            desc = lector1.GetValue(1)
            soli = lector1.GetValue(2)
            ent = lector1.GetValue(3)
            UNID = lector1.GetValue(4)
            Me.DataGridView2.Rows.Add(mate, desc, UNID, soli, ent, soli - ent)
        End While
        'ciero la conexion
        con1.Close()
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.RowCount <> 0 Then
            llenarDW2_estado2y3(CDec(Me.DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value))
            If DataGridView2.RowCount <> 0 Then
                Button3.Enabled = True
            End If
        End If
    End Sub
    Public Sub Archivo()
        Dim fichero As String = "C:\Archivo\Peticion_de_materiales_" + Me.DataGridView1.Item(0, Me.DataGridView1.CurrentRow.Index).Value + "_" + Date.Now.Day.ToString.PadLeft(2, "0") + Date.Now.Month.ToString.PadLeft(2, "0") + Date.Now.Year.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("Cabecera")
        a.WriteLine()
        a.WriteLine("Estado: " + ";" + CB_estado.Text)
        a.WriteLine("Contrato: " + ";" + Me.DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value)
        a.WriteLine("Numero de Peticion: " + ";" + Me.DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value)
        a.WriteLine("Generada: " + ";" + Me.DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value)
        a.WriteLine()
        a.WriteLine("Descripcion")
        a.WriteLine()
        a.WriteLine("COD_MATERIAL; DESC_MATERIAL;U;CANT_SOLICITADA;CANT_ENTREGADA;SALDO")
        For I = 0 To DataGridView2.RowCount - 1
            If DataGridView2.Item(5, I).Value <> 0 Then
                a.WriteLine(DataGridView2.Item(0, I).Value.ToString + ";" + DataGridView2.Item(1, I).Value.ToString + ";" + DataGridView2.Item(2, I).Value.ToString + ";" + DataGridView2.Item(3, I).Value.ToString + ";" + DataGridView2.Item(4, I).Value.ToString + ";" + DataGridView2.Item(5, I).Value.ToString)
            End If
        Next
        a.Close()
        mensaje.MADVE002(fichero)
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'verifico que la carpeta exista
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If
        Archivo()
    End Sub

    
End Class