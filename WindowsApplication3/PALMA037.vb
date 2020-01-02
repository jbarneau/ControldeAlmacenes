Imports System.Data.Sql
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Public Class PALMA037
    Private FAMILIA As Integer
    Private RESPUESTA As Boolean = False
    Private _ALMACEN As String
    Private CAJON As String
    Private nomfamilia As String
    Private mensaje As New Clase_mensaje
    Private med_retirar As New Clase_med_retirar
#Region "SETERS AND GETERS"
    Public Sub TOMAR(MIALMACEN As String)
        _ALMACEN = MIALMACEN
    End Sub
    Public ReadOnly Property LEERRESPUESTA
        Get
            Return RESPUESTA
        End Get
    End Property
    Public ReadOnly Property LEERFAMILIA
        Get
            Return FAMILIA
        End Get
    End Property
    Public ReadOnly Property LEERCAJON
        Get
            Return CAJON
        End Get
    End Property
    Public ReadOnly Property LEERNOMBREFAMILIA
        Get
            Return nomfamilia
        End Get
    End Property
#End Region
    Private Sub PALMA037_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Focus()
        llenar_DS_Familia()
    End Sub


    Private Sub llenar_DS_Familia()
        'CONECTO LA BASE 
        Dim DS_deposito As New DataSet
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT C_PARA_802,DESC_802 FROM DET_PARAMETRO_802 WHERE C_TABLA_802 = 15 and F_BAJA_802 IS NULL and C_PARA_802 <>0 ORDER BY DESC_802", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_deposito, " DET_PARAMETRO_802")
        cnn2.Close()
        ComboBox1.DataSource = DS_deposito.Tables(" DET_PARAMETRO_802")
        ComboBox1.DisplayMember = "DESC_802"
        ComboBox1.ValueMember = "C_PARA_802"
        ComboBox1.Text = Nothing
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RESPUESTA = False
        Me.Close()
    End Sub
    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        If ComboBox1.Text <> Nothing Then
            Dim PANTALLAS As New PALMA034
            PANTALLAS.TOMAR(_ALMACEN, ComboBox1.SelectedValue)
            PANTALLAS.ShowDialog()
            If PANTALLAS.LLERRESPUESTA = True Then
                TextBox1.Text = PANTALLAS.LEERCAJON
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CAJON = TextBox1.Text
        FAMILIA = ComboBox1.SelectedValue
        nomfamilia = ComboBox1.Text
        RESPUESTA = True
        Me.Close()
    End Sub

End Class