Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class vehiculo
    Private midominio As String
    Private existe As Boolean = False
    Private marca As String
    Private modelo As String

    Public Sub New(dominio As String)
        midominio = dominio
        CARGAR()
    End Sub
    Private Sub CARGAR()
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT DESC_802, MODELO008 FROM M_VEHICULO_008 INNER JOIN DET_PARAMETRO_802 ON MARCA008 = C_PARA_802 WHERE (C_TABLA_802 = 20) AND (DOMINIO008=@dom)", CNN)
            ADT.Parameters.AddWithValue("dom", midominio)
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                existe = True
                marca = LECTOR.GetValue(0)
                modelo = LECTOR.GetValue(1)
            End If
        Catch ex As Exception
        Finally
            CNN.Close()
        End Try
    End Sub
    Public ReadOnly Property SIEXISTE
        Get
            Return existe
        End Get
    End Property
    Public ReadOnly Property LEERMARCA
        Get
            Return marca
        End Get
    End Property
    Public ReadOnly Property LEERMODELO
        Get
            Return modelo
        End Get
    End Property
End Class
