Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class PCOMP005
    Public COD As String = ""
    Public dESC As String = ""
    Public POR As Decimal = 0

    Public carga As Boolean = False

    Public Sub TOMAR(COD As String, DESC As String, POR As Decimal)
        Me.COD = COD
        Me.dESC = DESC
        Me.POR = POR
    End Sub


    Private Sub PCOMP005_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llenar_CB_Contrato()
        If COD <> "" Then
            CB_contrato.SelectedValue = COD
            NDPOR.Value = POR

        End If
    End Sub



    Private Sub llenar_CB_Contrato()
        Dim DS_contrato As New DataSet
        'CONECTO LA BASE
        Dim cnn2 As SqlConnection = New SqlConnection(conexion)
        'ABRO LA BASE
        cnn2.Open()
        'GENERO UN ADAPTADOR
        Dim adaptadaor As New SqlDataAdapter("SELECT NCONT_004, DESC_004 FROM M_CONT_004 where F_BAJA_004 is NULL order by DESC_004", cnn2)
        'LLENO EL ADAPTADOR CON EL DATASET
        adaptadaor.Fill(DS_contrato, "M_CONT_004")
        cnn2.Close()
        CB_contrato.DataSource = DS_contrato.Tables("M_CONT_004")
        CB_contrato.DisplayMember = "DESC_004"
        CB_contrato.ValueMember = "NCONT_004"
        CB_contrato.Text = Nothing
    End Sub

    Private Sub CB_contrato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_contrato.SelectedIndexChanged
        If CB_contrato.ValueMember <> Nothing Then

            If CB_contrato.Text <> Nothing Then
                NDPOR.Focus()


            End If
        End If
    End Sub

    Private Sub CBCONFIRMAR_Click(sender As Object, e As EventArgs) Handles CBCONFIRMAR.Click
        If CB_contrato.ValueMember <> Nothing Then

            If CB_contrato.Text <> Nothing Then

                carga = True
                POR = CDec(NDPOR.Value)
                dESC = CB_contrato.Text
                COD = CB_contrato.SelectedValue
                Me.Close()

            End If
        End If

    End Sub
End Class