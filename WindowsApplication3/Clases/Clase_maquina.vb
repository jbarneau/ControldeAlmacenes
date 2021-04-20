Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class Clase_maquina
#Region "VARIABLES"
    Private NSERIE As String
    Private TIPO As String
    Private EXISTE As Boolean = False
    Private CHAPA As String
    Private FALTA As String
    Private FBAJA As String
    Private ESTADO As Integer
    Private MARCA As String
    Private ALMACEN As String
    Private MODELO As String
    Private OBS As String
#End Region
#Region "LECTORES"
    Public ReadOnly Property LEERMODELO
        Get
            Return MODELO
        End Get
    End Property
    Public ReadOnly Property LEEOBS
        Get
            Return OBS
        End Get
    End Property
    Public ReadOnly Property LEEREXISTE
        Get
            Return EXISTE
        End Get
    End Property
    Public ReadOnly Property LEERNSERIE
        Get
            Return NSERIE
        End Get
    End Property
    Public ReadOnly Property LEERCHAPA
        Get
            Return CHAPA
        End Get
    End Property
    Public ReadOnly Property LEERESTADO
        Get
            Return ESTADO
        End Get
    End Property
    Public ReadOnly Property LEERALMACEN
        Get
            Return ALMACEN
        End Get
    End Property
    Public ReadOnly Property LEERTIPO
        Get
            Return TIPO
        End Get
    End Property
    Public ReadOnly Property LEERMARCA
        Get
            Return MARCA
        End Get
    End Property
    Public ReadOnly Property LEEFALTA
        Get
            Return FALTA
        End Get
    End Property
    Public ReadOnly Property LEERFBAJA
        Get
            Return FBAJA
        End Get
    End Property
#End Region
    Public Sub TOMAR(MINSERIE As String)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT NSERIE_006, TIPO_006,MARCA_006,CHAPA_006,ESTADO_006,ALMA_006,FALTA_006,FBAJA_006,MODELO_006, OBS_006 FROM M_MAQUI_006 WHERE NSERIE_006=@D1", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", MINSERIE))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                EXISTE = True
                NSERIE = LECTOR.GetValue(0)
                TIPO = LECTOR.GetValue(1).ToString.PadLeft(3, "0")
                MARCA = LECTOR.GetValue(2)
                CHAPA = LECTOR.GetValue(3)
                ESTADO = LECTOR.GetValue(4)
                ALMACEN = LECTOR.GetValue(5)
                FALTA = LECTOR.GetValue(6)
                If IsDBNull(LECTOR.GetValue(7)) Then
                    FBAJA = ""
                Else
                    FBAJA = LECTOR.GetValue(7)
                End If
                MODELO = LECTOR.GetValue(8)
                If IsDBNull(LECTOR.GetValue(9)) Then
                    OBS = ""
                Else
                    OBS = LECTOR.GetValue(9)
                End If
            Else
                EXISTE = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub

    Public Sub TOMARCHAPA(nchapa As String)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim ADT As New SqlCommand("SELECT NSERIE_006, TIPO_006,MARCA_006,CHAPA_006,ESTADO_006,ALMA_006,FALTA_006,FBAJA_006,MODELO_006, OBS_006 FROM M_MAQUI_006 WHERE CHAPA_006=@D1", CNN)
            ADT.Parameters.Add(New SqlParameter("D1", nchapa))
            Dim LECTOR As SqlDataReader = ADT.ExecuteReader
            If LECTOR.Read Then
                EXISTE = True
                NSERIE = LECTOR.GetValue(0)
                TIPO = LECTOR.GetValue(1).ToString.PadLeft(3, "0")
                MARCA = LECTOR.GetValue(2)
                CHAPA = LECTOR.GetValue(3)
                ESTADO = LECTOR.GetValue(4)
                ALMACEN = LECTOR.GetValue(5)
                FALTA = LECTOR.GetValue(6)
                If IsDBNull(LECTOR.GetValue(7)) Then
                    FBAJA = ""
                Else
                    FBAJA = LECTOR.GetValue(7)
                End If
                MODELO = LECTOR.GetValue(8)
                If IsDBNull(LECTOR.GetValue(9)) Then
                    OBS = ""
                Else
                    OBS = LECTOR.GetValue(9)
                End If
            Else
                EXISTE = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub

    Public Sub ActualizarEstado(ByVal estado As Integer)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim adt As New SqlCommand("update M_MAQUI_006 SET ESTADO_006 = @D1 WHERE NSERIE_006 = @D2", CNN)
            adt.Parameters.Add(New SqlParameter("D1", estado))
            adt.Parameters.Add(New SqlParameter("D2", NSERIE))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub
    Public Sub Grabar_Movimiento(ByVal depo2 As String, ByVal obs As String, ByVal Estado As Integer)
        Dim cnn As New SqlConnection(conexion)
        Try
            cnn.Open()
            Dim adt As New SqlCommand("INSERT INTO T_TRANS_MQ_120 (FECHA_120,NSERIE_120,DEPO1_120,DEPO2_120, OBS_120,ESTADO_120) VALUES (@D1,@D2,@D3,@D4,@D5,@D6)", cnn)
            adt.Parameters.Add(New SqlParameter("D1", Date.Now))
            adt.Parameters.Add(New SqlParameter("D2", NSERIE))
            adt.Parameters.Add(New SqlParameter("D3", ALMACEN))
            adt.Parameters.Add(New SqlParameter("D4", depo2))
            adt.Parameters.Add(New SqlParameter("D5", obs))
            adt.Parameters.Add(New SqlParameter("D6", Estado))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn.Close()
        End Try
    End Sub
    Public Sub ActualizarEquipo(ByVal Equipo As String)
        Dim CNN As New SqlConnection(conexion)
        Try
            CNN.Open()
            Dim adt As New SqlCommand("update M_MAQUI_006 SET ALMA_006 = @D1 WHERE NSERIE_006 = @D2", CNN)
            adt.Parameters.Add(New SqlParameter("D1", Equipo))
            adt.Parameters.Add(New SqlParameter("D2", NSERIE))
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            CNN.Close()
        End Try
    End Sub

    Public Function Obtener_Numero_Mov() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=11", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        con1.Open()
        Dim comando2 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=11", con1)
        comando2.ExecuteReader()
        con1.Close()
        Return Numero_Remito
    End Function
End Class
