Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class PALMA047
    Private TEXTO As String
    Private CuantosDias As Integer
    Private METODOS As New Clas_Almacen
    Private TIPO As Integer
    Private MENSAJE As New Clase_mensaje
    'TIPO 1 SON LOS MEDIDORES CON TAREA COMUNICADA SIN ENCONTRAR FISICAMENTE
    'TIPO 2 SON LOS MEDIDORES FISICOS SIN INFORMAR OT
    Private Sub PALMA047_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LLENAR()
    End Sub
    Public Sub TOMAR(MITIPO As Integer, MIDIAS As Integer)
        TIPO = MITIPO
        CuantosDias = MIDIAS
    End Sub

    Private Sub MedaBuscar()
        Dim MEDIDOR As String
        Dim POLIZA As String
        Dim CAPACIDAD As String
        Dim FCAGO As String
        Dim FINFO As String
        Dim OPERARIO As String
        Dim tabla As New DataTable
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("select NSERI_113, POLIZA_113, CMATE_113, FCARGO_113, FINFO_113, OPERA_113 FROM T_MED_DEVO_113 WHERE ESTADO_113=0", cnn) '  and FCARGO_113>=@D1"
            'adt.SelectCommand.Parameters.Add(New SqlParameter("D1", DateAdd(DateInterval.Day, -CuantosDias, Date.Now)))
            adt.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        For I = 0 To tabla.Rows.Count - 1
            MEDIDOR = tabla.Rows(I).Item(0)
            POLIZA = tabla.Rows(I).Item(1)
            CAPACIDAD = METODOS.detalle_material(tabla.Rows(I).Item(2))
            FCAGO = CDate(tabla.Rows(I).Item(3)).ToShortDateString
            If IsDBNull(tabla.Rows(I).Item(4)) Then
                FINFO = "SIN INF"
            Else
                FINFO = CDate(tabla.Rows(I).Item(4)).ToShortDateString
            End If
            If IsDBNull(tabla.Rows(I).Item(5)) Then
                OPERARIO = ""
            Else
                OPERARIO = METODOS.NOMBRE_DEPOSITO(tabla.Rows(I).Item(5))
            End If

            DGV1.Rows.Add(MEDIDOR, POLIZA, CAPACIDAD, FCAGO, FINFO, OPERARIO)
        Next
    End Sub
    Private Sub MedaSinInformar()
        Dim MEDIDOR As String
        Dim POLIZA As String
        Dim CAPACIDAD As String
        Dim FCAGO As String
        Dim FINFO As String
        Dim OPERARIO As String
        Dim tabla As New DataTable
        Dim cnn As New SqlConnection(MAIN.conexion)
        Try
            cnn.Open()
            Dim adt As New SqlDataAdapter("select NSERI_113, POLIZA_113, CMATE_113, FCARGO_113, FINFO_113, OPERA_113 FROM T_MED_DEVO_113 WHERE FINFO_113 IS NULL and FCARGO_113>=@D1", cnn)
            adt.SelectCommand.Parameters.Add(New SqlParameter("D1", DateAdd(DateInterval.Day, -CuantosDias, Date.Now)))
            adt.Fill(tabla)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
        For I = 0 To tabla.Rows.Count - 1
            MEDIDOR = tabla.Rows(I).Item(0)
            POLIZA = tabla.Rows(I).Item(1)
            CAPACIDAD = METODOS.detalle_material(tabla.Rows(I).Item(2))
            FCAGO = CDate(tabla.Rows(I).Item(3)).ToShortDateString
            If IsDBNull(tabla.Rows(I).Item(4)) Then
                FINFO = "SIN INF"
            Else
                FINFO = CDate(tabla.Rows(I).Item(4)).ToShortDateString
            End If
            OPERARIO = METODOS.NOMBRE_DEPOSITO(tabla.Rows(I).Item(5))
            DGV1.Rows.Add(MEDIDOR, POLIZA, CAPACIDAD, FCAGO, FINFO, OPERARIO)
        Next
    End Sub

    Private Sub LLENAR()
        If TIPO = 1 Then
            MedaBuscar()
            TEXTO = "MEDIDORES COMUNICADOS SIN ENCONTRAR CON MAS DE " + CuantosDias.ToString
        End If
        If TIPO = 2 Then
            MedaSinInformar()
            TEXTO = "MEDIDORES SIN COMUNICAR LA OT CON MAS DE " + CuantosDias.ToString
        End If
        Label1.Text = TEXTO
        LBCANT.Text = DGV1.Rows.Count.ToString
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If My.Computer.FileSystem.DirectoryExists("C:\ARCHIVO") = False Then
            My.Computer.FileSystem.CreateDirectory("C:\ARCHIVO")
        End If

        Dim fichero As String = "C:\Archivo\" + TEXTO.Replace(" ", "_") + "_" + Date.Now.Day.ToString.PadLeft(2, "0") + Date.Now.Month.ToString.PadLeft(2, "0") + Date.Now.Year.ToString + ".csv"
        Dim a As New System.IO.StreamWriter(fichero)
        a.WriteLine("N_SERIE;POLIZA;MATERIAL; FCARGO;FINFO;OPERARIO")
        For I = 0 To Me.DGV1.RowCount - 1
            a.WriteLine(Me.DGV1.Item(0, I).Value.ToString + ";" + Me.DGV1.Item(1, I).Value.ToString + ";" + DGV1.Item(2, I).Value.ToString + ";" + Me.DGV1.Item(3, I).Value.ToString + ";" + Me.DGV1.Item(4, I).Value.ToString + ";" + Me.DGV1.Item(5, I).Value.ToString)
        Next
        a.Close()
        MENSAJE.MADVE002(fichero)

    End Sub
End Class