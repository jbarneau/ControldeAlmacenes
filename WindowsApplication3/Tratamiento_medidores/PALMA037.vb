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
    Public Sub TOMAR(MIALMACEN As String, familia As Integer, nomfamilia As String)
        _ALMACEN = MIALMACEN
        Me.FAMILIA = familia
        Me.nomfamilia = nomfamilia
    End Sub
    Public ReadOnly Property LEERRESPUESTA
        Get
            Return RESPUESTA
        End Get
    End Property

    Public ReadOnly Property LEERCAJON
        Get
            Return CAJON
        End Get
    End Property
#End Region
    Private Sub PALMA037_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox2.Text = nomfamilia

    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RESPUESTA = False
        Me.Close()
    End Sub
    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick

        Dim PANTALLAS As New PALMA034
            PANTALLAS.TOMAR(_ALMACEN, FAMILIA)
            PANTALLAS.ShowDialog()
            If PANTALLAS.LLERRESPUESTA = True Then
                TextBox1.Text = PANTALLAS.LEERCAJON
            End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CAJON = TextBox1.Text
        RESPUESTA = True
        Me.Close()
    End Sub


End Class