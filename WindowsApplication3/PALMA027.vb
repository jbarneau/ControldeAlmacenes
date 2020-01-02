Imports System.Data.SqlTypes
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.IO
Imports System
Imports System.Collections
Imports Microsoft.VisualBasic

Public Class PALMA027
    Private MEDIDOR As New Clas_Medidor
    Private MENSJA As New Clase_mensaje
    Private METODOS As New Clas_Almacen
   

    Private Sub PALMA027_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "USUARIO: " & _usr.Obt_Nombre_y_Apellido
        Label5.Text = "Fecha: " & DateTime.Now().ToShortDateString
        Ult_actualizacion()
    End Sub

    Private Sub Ult_actualizacion()
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        'Abrimos la conexion
        cnn1.Open()
        'Creamos el comando para crear la consulta
        Dim Comando As New SqlClient.SqlCommand("select F_Proceso from ULT_PROCESOS WHERE Proceso = 2", cnn1)
        'Ejecutamos el commnad y le pasamos el parametro
        Comando.ExecuteNonQuery()
        Dim Dusrs As SqlDataReader = Comando.ExecuteReader
        If Dusrs.Read.ToString Then
            Label6.Text = "Ultima actualzación: " + Dusrs.GetDateTime(0).ToShortDateString
        End If
        cnn1.Close()
    End Sub



End Class