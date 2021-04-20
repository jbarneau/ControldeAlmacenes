Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.IO


Public Class PCOMB004_bis
    Private TICKET As String
    Private RESP As Boolean = False

    Public Sub TOMAR(ByVal MITICKET)
        TICKET = MITICKET
    End Sub

    Public ReadOnly Property LEERRESP
        Get
            Return RESP
        End Get
    End Property

    Public ReadOnly Property LEERTICKET
        Get
            Return TICKET
        End Get
    End Property

    Private Sub BuscarEnDb(ByVal ticket As String)
        Dim cnn As SqlConnection = New SqlConnection(conexion)
        Dim dt As DataTable = New DataTable()
        Try
            cnn.Open()
            Dim adt As New SqlCommand("SELECT dbo.T_VALE_122.NVALE122 AS [Nº DE VALE], dbo.T_VALE_122.FTICKET122 AS [FECHA DE CARGA DE COMBUSTIBLE], dbo.T_VALE_122.NTICKET122 AS [Nº DE TICKET], dbo.T_VALE_122.LITROS122 AS [LITROS CARGADOS], dbo.DET_PARAMETRO_802.DESC_802 AS [TIPO DE COMBUSTIBLE], dbo.T_VALE_122.DOMINI122 AS DOMINIO, M_PERS_003_1.APELL_003 + ' ' + M_PERS_003_1.NOMB_003 AS OPERARIO, dbo.M_PERS_003.APELL_003 + ' ' + dbo.M_PERS_003.NOMB_003 AS [USUARIO AUTORIZA] FROM dbo.T_VALE_122 INNER JOIN dbo.M_PERS_003 ON dbo.T_VALE_122.USER122 = dbo.M_PERS_003.NDOC_003 INNER JOIN dbo.DET_PARAMETRO_802 ON dbo.T_VALE_122.TIPOTICKET122 = dbo.DET_PARAMETRO_802.C_PARA_802 INNER JOIN dbo.M_PERS_003 AS M_PERS_003_1 ON dbo.T_VALE_122.CHOFER122 = M_PERS_003_1.NDOC_003 WHERE (dbo.T_VALE_122.NVALE122 = @D1) AND (dbo.DET_PARAMETRO_802.C_TABLA_802 = 22) AND (dbo.T_VALE_122.CCOMB122 = dbo.T_VALE_122.TIPOTICKET122)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", ticket))
            Dim adapt As New SqlDataAdapter(adt)
            adapt.Fill(dt)
            dgv.DataSource = dt
        Catch
            Throw New Exception("ERROR EN FUNCIÒN BUSCARENDB")
        Finally
            cnn.Close()
        End Try
    End Sub
    Private Sub PCOMB004_bis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BuscarEnDb(TICKET)
    End Sub

    Private Sub dgv_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellContentDoubleClick
        TICKET = dgv.Item(0, dgv.CurrentRow.Index).Value.ToString
        RESP = True
        Me.Close()
    End Sub
End Class