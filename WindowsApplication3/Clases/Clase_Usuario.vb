Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class Clase_Usuario
    Private Usuario As String
    Private NomUsuario As String
    Private NAcceso As Integer
    Private Almacen As String
    Private ArrProg As New ArrayList
    Private mensaje As New Clase_mensaje
    Public ReadOnly Property Obt_Usr
        Get
            Return Usuario
        End Get
    End Property
    Public Function Existe_USR(ByVal Id_usr As String, ByVal pass As String) As Integer
        '0 no se conecta a la base
        '1 el usuario no existe
        '2 la clave no es correcta
        '3 ingresar al sistema
        Dim Conf As Integer = 0
        Try
            Dim cnn1 As SqlConnection = New SqlConnection(conexion)
            'Abrimos la conexion
            cnn1.Open()
            'Creamos el comando para crear la consulta
            Dim Comando As SqlCommand = cnn1.CreateCommand
            Comando.CommandText = "select NDOC_001, NOMB_001, APELL_001, ISUR_001, PASS_001, CALM_001, CACC_001 from M_USRS_001 WHERE ISUR_001= @USRS AND F_BAJA_001 is NULL"
            'Ejecutamos el commnad y le pasamos el parametro
            Comando.Parameters.Add(New SqlParameter("USRS", Id_usr))
            Comando.ExecuteNonQuery()
            Dim Dusrs As SqlDataReader = Comando.ExecuteReader
            If Dusrs.Read.ToString = False Then
                mensaje.MERRO004()
                Conf = 1
            Else
                If Dusrs.GetValue(4) = pass Then
                    NomUsuario = Dusrs.GetValue(1) & " " & Dusrs.GetValue(2)
                    NAcceso = Dusrs.GetValue(6)
                    Almacen = Dusrs.GetValue(5)
                    Usuario = Dusrs.GetValue(0)
                    'cerramos el datared para poder usar el mismo data set y no tener que crearla nuevamente
                    Dusrs.Close()
                    Dim comando2 As SqlCommand = cnn1.CreateCommand
                    comando2.CommandText = "select NACC_101, PROG_101 from T_PERF_101 where NACC_101 = @NIVEL and F_BAJA_101 is NULL"
                    comando2.Parameters.Add(New SqlParameter("NIVEL", NAcceso))
                    comando2.ExecuteNonQuery()
                    Dim Dusrs2 As SqlDataReader = comando2.ExecuteReader
                    'recorremos el data reder y guardamos el valor en la arraylist
                    While (Dusrs2.Read())
                        ArrProg.Add(Dusrs2.GetValue(1))
                    End While
                    Conf = 3
                Else
                    mensaje.MERRO005()
                    Conf = 2
                End If
            End If
            cnn1.Close()
        Catch ex As Exception
            mensaje.MERRO001()
        End Try
        Return Conf
    End Function
    Public ReadOnly Property Obt_Nombre_y_Apellido
        Get
            Return NomUsuario
        End Get
    End Property
    Public ReadOnly Property Obt_Almacen
        Get
            Return Almacen
        End Get
    End Property
    Public Function Activar_BT(a As String) As Boolean
        Dim respuesta As Boolean = False
        If ArrProg.Contains(a) = True Then
            respuesta = True
        End If
        Return respuesta
    End Function
    
End Class
