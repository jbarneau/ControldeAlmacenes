Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes

Public Class TIPO_DE_MAQUINA
    Private DESCRIPCION As String
    Private CALIFBRACION As Integer
    Private PLAZOCALI As Integer
    Private VERIFICACION As Integer
    Private PLAZOVERI As Integer


    Public Sub New(ByVal TIPO As String)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT DESC_806, CALI_806,PLAZO_806,VERIF_806, PVERIF_806 FROM P_MAQUINA_806 WHERE TIPO_806=@D1", CNN)
            ADT.Parameters.AddWithValue("D1", TIPO)
            Dim lector As SqlDataReader = ADT.ExecuteReader
            If lector.Read Then
                DESCRIPCION = lector.GetValue(0)
                CALIFBRACION = lector.GetInt32(1)
                PLAZOCALI = lector.GetInt32(2)
                VERIFICACION = lector.GetInt32(3)
                PLAZOVERI = lector.GetInt32(4)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    Public ReadOnly Property leerDescripcion
        Get
            Return DESCRIPCION
        End Get
    End Property
    Public ReadOnly Property leerCalibracion
        Get
            Return CALIFBRACION
        End Get
    End Property
    Public ReadOnly Property leerPlazoCalibracion
        Get
            Return PLAZOCALI
        End Get
    End Property
    Public ReadOnly Property leerVerificacion
        Get
            Return VERIFICACION
        End Get
    End Property
    Public ReadOnly Property leerPlazoVerif
        Get
            Return PLAZOVERI
        End Get
    End Property
End Class
