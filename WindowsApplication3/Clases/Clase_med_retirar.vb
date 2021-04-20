Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes
Public Class Clase_med_retirar
    Private Existe As Boolean = False
    Private finfo As String = "NO"
    Private nmedidor As String
    Private codsap As String
    Private codfamilia As Integer
    Private descfamilia As String
    Private contrato As String = "NO"
    Public ReadOnly Property LEEREXISTE
        Get
            Return Existe
        End Get
    End Property
    Public ReadOnly Property LEERFINFO
        Get
            Return finfo
        End Get
    End Property

    Public Property GETSETNMED
        Get
            Return nmedidor
        End Get
        Set(value)
            nmedidor = value
        End Set
    End Property

    Public Property GETSETSAP
        Get
            Return codsap
        End Get
        Set(value)
            codsap = value
        End Set
    End Property

    Public Property GETSETNOMFAMILIA
        Get
            Return descfamilia
        End Get
        Set(value)
            descfamilia = value
        End Set
    End Property

    Public Property GETSETCODFAMILIA
        Get
            Return codfamilia
        End Get
        Set(value)
            codfamilia = value
        End Set
    End Property
    Public Property GETSETCONTRATO() As String
        Get
            Return contrato
        End Get
        Set(ByVal value As String)
            contrato = value
        End Set
    End Property
    Public Sub ACTUALIZAR_MEDIDOR(MEDIDOR As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set FINFO_113=@D1 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", Date.Now))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

        'esta todo bien
    End Sub
    Public Sub ACTUALIZAR_MEDIDOR_GUARDIA(ByVal MEDIDOR As String, ByVal contrato As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set CONTRATO_113=@D1, FAMILIA_113=0, CAJON_113 = NULL , USER_AC_113 = NULL, ESTADO_113=0,FREMITO_113=NULL,NREMITO_113=NULL, USER_REM_113=NULL WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", contrato))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

        'esta todo bien
    End Sub

    Public Sub ACTUALIZAR_MEDIDOR_GUARDIA_NUEVO(ByVal MEDIDOR As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set CONTRATO_113='01', FAMILIA_113=0, CAJON_113 = NULL , USER_AC_113 = NULL, ESTADO_113=0,FREMITO_113=NULL,NREMITO_113=NULL, USER_REM_113=NULL, OPERA_113='09'  WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

        'esta todo bien
    End Sub
    Public Sub Exite_Medi_2(ByVal seri As String)

        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn4.Open()
            'Consulta SQL...
            Dim comando1 As New SqlClient.SqlCommand("select NSERI_113 FROM T_MED_DEVO_113 WHERE NSERI_113 = @D1", cnn4)
            comando1.Parameters.Add(New SqlParameter("D1", seri))
            Dim lector1 As SqlDataReader = comando1.ExecuteReader
            'pregunto si encontro
            If lector1.HasRows Then

                finfo = "SI"
            Else
                finfo = "NO"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn4.Close()
        End Try

    End Sub

    Public Sub LeerContratoMed(ByVal seri As String)

        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        Try
            cnn4.Open()
            'Consulta SQL...
            Dim comando1 As New SqlClient.SqlCommand("select CONTRATO_113 FROM T_MED_DEVO_113 WHERE NSERI_113 = @D1", cnn4)
            comando1.Parameters.Add(New SqlParameter("D1", seri))
            Dim lector1 As SqlDataReader = comando1.ExecuteReader
            'pregunto si encontro
            If lector1.HasRows Then
                GETSETCONTRATO = lector1.GetValue(0).ToString()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cnn4.Close()
        End Try

    End Sub







    Public Function GRABAR_MEDIDOR_P(ByVal NSERIE As String, ByVal DEPOSITO As String, ByVal CAPACIDAD As String, ByVal FRETIRO As Date, ByVal ESTADO As Integer, ByVal CONTRATO As String, ByVal USR As String, ByVal operario As String, ot As String, poliza As String, familia As Integer, cajon As String) As Integer
        Dim resp As Integer = 0
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("insert T_MED_DEVO_113(NSERI_113,DEPOSI_113,CMATE_113, FRETIRO_113, ESTADO_113,CONTRATO_113,USER_C_113,OPERA_113,OT_113,FCARGO_113,POLIZA_113,FAMILIA_113, CAJON_113,USER_AC_113) VALUES (@D1,@D2,@D3,@D4,@D7,@D8,@D9,@D10,@D11,@D12,@D13,@D14,@D15,@D16)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", NSERIE))
            comando1.Parameters.Add(New SqlParameter("D2", DEPOSITO))
            comando1.Parameters.Add(New SqlParameter("D3", CAPACIDAD))
            comando1.Parameters.Add(New SqlParameter("D4", FRETIRO))
            comando1.Parameters.Add(New SqlParameter("D7", ESTADO))
            comando1.Parameters.Add(New SqlParameter("D8", CONTRATO))
            comando1.Parameters.Add(New SqlParameter("D9", USR))
            comando1.Parameters.Add(New SqlParameter("D10", operario))
            If ot.Length > 3 Then
                ot = ot.Remove(2, 3)
            End If
            comando1.Parameters.Add(New SqlParameter("D11", ot))
            comando1.Parameters.Add(New SqlParameter("D12", Date.Now))
            comando1.Parameters.Add(New SqlParameter("D13", poliza))
            comando1.Parameters.Add(New SqlParameter("D14", familia))
            comando1.Parameters.Add(New SqlParameter("D15", cajon))
            comando1.Parameters.Add(New SqlParameter("D16", USR))
            resp = comando1.ExecuteNonQuery()
        Catch ex As Exception
            resp = 0
        Finally
            con1.Close()
        End Try
        Return resp
    End Function
    Public Function GRABAR_MEDIDOR_P_CUSTODIA(ByVal NSERIE As String, ByVal DEPOSITO As String, ByVal CAPACIDAD As String, ByVal FRETIRO As Date, ByVal ESTADO As Integer, ByVal CONTRATO As String, ByVal USR As String, ByVal operario As String, ot As String, poliza As String, familia As Integer, cajon As String) As Integer
        Dim resp As Integer = 0
        Dim fecha As Date = Date.Now
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("insert T_MED_DEVO_113(NSERI_113,DEPOSI_113,CMATE_113, FRETIRO_113, ESTADO_113,CONTRATO_113,USER_C_113,OPERA_113,OT_113,FCARGO_113,POLIZA_113,FAMILIA_113, CAJON_113,USER_AC_113,FCUSTODIA_113) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7,@D8,@D9,@D10,@D11,@D12,@D13,@D14,@D15)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", NSERIE))
            comando1.Parameters.Add(New SqlParameter("D2", DEPOSITO))
            comando1.Parameters.Add(New SqlParameter("D3", CAPACIDAD))
            comando1.Parameters.Add(New SqlParameter("D4", FRETIRO))
            comando1.Parameters.Add(New SqlParameter("D5", ESTADO))
            comando1.Parameters.Add(New SqlParameter("D6", CONTRATO))
            comando1.Parameters.Add(New SqlParameter("D7", USR))
            comando1.Parameters.Add(New SqlParameter("D8", operario))
            If ot.Length > 3 Then
                ot = ot.Remove(2, 3)
            End If
            comando1.Parameters.Add(New SqlParameter("D9", ot))
            comando1.Parameters.Add(New SqlParameter("D10", fecha))
            comando1.Parameters.Add(New SqlParameter("D11", poliza))
            comando1.Parameters.Add(New SqlParameter("D12", familia))
            comando1.Parameters.Add(New SqlParameter("D13", cajon))
            comando1.Parameters.Add(New SqlParameter("D14", USR))
            comando1.Parameters.Add(New SqlParameter("D15", fecha))
            resp = comando1.ExecuteNonQuery()
        Catch ex As Exception
            resp = 0
        Finally
            con1.Close()
        End Try
        Return resp
    End Function

    Public Sub GRABAR_TRANSFERENCIA(ByVal LOTE As String, ByVal MOVIMIENTO As Decimal, ByVal FECHA As Date, ByVal ENVIA As String, ByVal RECIBE As String, ByVal OBS As String)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("insert T_TRAN_MED_RET_116(NCAJON_116, NMOVI_116,FECHA_116,NENVIA_116,NRECIB_116,OBS_116) VALUES (@D1,@D2,@D3,@D4,@D5,@D6)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", LOTE))
            comando1.Parameters.Add(New SqlParameter("D2", MOVIMIENTO))
            comando1.Parameters.Add(New SqlParameter("D3", FECHA))
            comando1.Parameters.Add(New SqlParameter("D4", ENVIA))
            comando1.Parameters.Add(New SqlParameter("D5", RECIBE))
            comando1.Parameters.Add(New SqlParameter("D6", OBS))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub


    Public Sub GRABAR_TRANSFERENCIA(ByVal LOTE As String, ByVal MOVIMIENTO As Decimal, ByVal FECHA As Date, ByVal ENVIA As String, ByVal RECIBE As String)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("insert T_TRAN_MED_RET_116(NCAJON_116, NMOVI_116,FECHA_116,NENVIA_116,NRECIB_116) VALUES (@D1,@D2,@D3,@D4,@D5)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", LOTE))
            comando1.Parameters.Add(New SqlParameter("D2", MOVIMIENTO))
            comando1.Parameters.Add(New SqlParameter("D3", FECHA))
            comando1.Parameters.Add(New SqlParameter("D4", ENVIA))
            comando1.Parameters.Add(New SqlParameter("D5", RECIBE))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            ' MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub
    Public Sub MODIFICAR_MEDIDOR(ByVal ALMACEN2 As String, ByVal LOTE As String, ByVal FAMILIA As Integer, ByVal USER As String, ByVal MEDIDOR As String, ESTADO As Integer)
        'creo la cadena de conexion
        Dim aux As Object = Nothing
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set DEPOSI_113=@D1, CAJON_113=@D2, ESTADO_113=@D3, USER_AC_113=@D4, FAMILIA_113 = @D6 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", ALMACEN2))
            comando1.Parameters.Add(New SqlParameter("D2", LOTE))
            comando1.Parameters.Add(New SqlParameter("D3", ESTADO))
            comando1.Parameters.Add(New SqlParameter("D4", USER))
            comando1.Parameters.Add(New SqlParameter("D6", FAMILIA))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
        Try
            con1.Open()
            Dim adt As New SqlCommand("DELETE MED_NO_ENCONTRADO WHERE NMEDIDOR=@D1 AND ESTADO = 1", con1)
            adt.Parameters.AddWithValue("D1", MEDIDOR)
            adt.ExecuteNonQuery()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
        'esta todo bien
        'BUSCO EL OPERARIO QUE RETIRO EL MEDIDOR 
        Try
            con1.Open()
            Dim adt As New SqlCommand("SELECT OPERA_113 FROM dbo.T_MED_DEVO_113 WHERE (NSERI_113 = @D1)", con1)
            adt.Parameters.AddWithValue("D1", MEDIDOR)
            aux = adt.ExecuteScalar()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
        '''''DESCUENTO DE LA BOLSA VIRTUAL
        If Not IsNothing(aux) And Not IsDBNull(aux) Then
            Try
                con1.Open()
                Dim comando1 As New SqlClient.SqlCommand("Update T_REG_INGRESO_123 SET CANT_TRAJO_123 = CANT_TRAJO_123 - 1 WHERE OPERARIO_123 = @D1", con1)
                comando1.Parameters.Add(New SqlParameter("D1", Convert.ToString(aux)))
                comando1.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                con1.Close()
            End Try
        End If

    End Sub
    Public Sub MODIFICAR_MEDIDOR_CUSTODIA(ByVal ALMACEN2 As String, ByVal LOTE As String, ByVal FAMILIA As Integer, ByVal USER As String, ByVal FECHA As Date, ByVal MEDIDOR As String, ESTADO As Integer)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set DEPOSI_113=@D1, CAJON_113=@D2, ESTADO_113=@D3, USER_AC_113=@D4, FCUSTODIA_113=@D5, FAMILIA_113 = @D6 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", ALMACEN2))
            comando1.Parameters.Add(New SqlParameter("D2", LOTE))
            comando1.Parameters.Add(New SqlParameter("D3", ESTADO))
            comando1.Parameters.Add(New SqlParameter("D4", USER))
            comando1.Parameters.Add(New SqlParameter("D5", FECHA))
            comando1.Parameters.Add(New SqlParameter("D6", FAMILIA))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            'MessageBox.Show(ex.Message, "error en modificar")
        Finally
            con1.Close()
        End Try
        Try
            con1.Open()
            Dim adt As New SqlCommand("DELETE MED_NO_ENCONTRADO WHERE NMEDIDOR=@D1 AND ESTADO = 1", con1)
            adt.Parameters.AddWithValue("D1", MEDIDOR)
            adt.ExecuteNonQuery()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
        'esta todo bien
    End Sub
    Public Sub GRABAR_MEDIDOR(ByVal NSERIE As String, ByVal FCARGO As Date, ByVal USER As String, ByVal DEPOSITO As String, ByVal CODMATE As String, ByVal POLIZA As String, ByVal CONTRATO As String, ByVal FRETIRO As Date, ByVal FAMILIA As Integer, ByVal ESTADO As Integer, ByVal OT As String, ByVal operario As String)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            'abro la cadena
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("insert T_MED_DEVO_113(NSERI_113,FCARGO_113, USER_C_113,DEPOSI_113,CMATE_113, POLIZA_113,CONTRATO_113,FRETIRO_113,FAMILIA_113, ESTADO_113,OT_113,OPERA_113) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7,@D8,@D9,@D11,@D13,@D14)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", NSERIE))
            comando1.Parameters.Add(New SqlParameter("D2", FCARGO))
            comando1.Parameters.Add(New SqlParameter("D3", USER))
            comando1.Parameters.Add(New SqlParameter("D4", DEPOSITO))
            comando1.Parameters.Add(New SqlParameter("D5", CODMATE))
            comando1.Parameters.Add(New SqlParameter("D6", POLIZA))
            comando1.Parameters.Add(New SqlParameter("D7", CONTRATO))
            comando1.Parameters.Add(New SqlParameter("D8", FRETIRO))
            comando1.Parameters.Add(New SqlParameter("D9", FAMILIA))
            comando1.Parameters.Add(New SqlParameter("D11", ESTADO))
            comando1.Parameters.Add(New SqlParameter("D13", OT))
            comando1.Parameters.Add(New SqlParameter("D14", operario))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

    End Sub
    Public Sub GRABAR_MEDIDOR2(ByVal NSERIE As String, ByVal FCARGO As Date, ByVal USER As String, ByVal DEPOSITO As String, ByVal CODMATE As String, ByVal POLIZA As String, ByVal CONTRATO As String, ByVal FRETIRO As Date, ByVal FAMILIA As Integer, ByVal ESTADO As Integer, ByVal OT As String, ByVal operario As String)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            'abro la cadena
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("insert T_MED_DEVO_113(NSERI_113,FCARGO_113, USER_C_113,DEPOSI_113,CMATE_113, POLIZA_113,CONTRATO_113,FRETIRO_113,FAMILIA_113, ESTADO_113,FINFO_113,OT_113,OPERA_113) VALUES (@D1,@D2,@D3,@D4,@D5,@D6,@D7,@D8,@D9,@D11,@D12,@D13,@D14)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", NSERIE))
            comando1.Parameters.Add(New SqlParameter("D2", FCARGO))
            comando1.Parameters.Add(New SqlParameter("D3", USER))
            comando1.Parameters.Add(New SqlParameter("D4", DEPOSITO))
            comando1.Parameters.Add(New SqlParameter("D5", CODMATE))
            comando1.Parameters.Add(New SqlParameter("D6", POLIZA))
            comando1.Parameters.Add(New SqlParameter("D7", CONTRATO))
            comando1.Parameters.Add(New SqlParameter("D8", FRETIRO))
            comando1.Parameters.Add(New SqlParameter("D9", FAMILIA))
            comando1.Parameters.Add(New SqlParameter("D11", ESTADO))
            comando1.Parameters.Add(New SqlParameter("D12", Date.Now))
            comando1.Parameters.Add(New SqlParameter("D13", OT))
            comando1.Parameters.Add(New SqlParameter("D14", operario))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
        Try
            con1.Open()
            Dim adt As New SqlCommand("UPDATE TEMP_MED_UBICAR SET TIPO=0 WHERE NMEDIDOR=@D1", con1)
            adt.Parameters.AddWithValue("D1", NSERIE)
            adt.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

    End Sub
    Public Sub MODIFICAR_MEDIDORSC(ByVal ALMACEN2 As String, ByVal FAMILIA As Integer, ByVal USER As String, ByVal FECHA As Date, ByVal MEDIDOR As String, ESTADO As Integer)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set DEPOSI_113=@D1, ESTADO_113=@D3, USER_AC_113=@D4, FINFO_113=@D5, FAMILIA_113 = @D6 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", ALMACEN2))
            comando1.Parameters.Add(New SqlParameter("D3", ESTADO))
            comando1.Parameters.Add(New SqlParameter("D4", USER))
            comando1.Parameters.Add(New SqlParameter("D5", FECHA))
            comando1.Parameters.Add(New SqlParameter("D6", FAMILIA))
            comando1.Parameters.Add(New SqlParameter("E1", MEDIDOR))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try

        'esta todo bien
    End Sub
    Public Function Obtener_Numero_Remito() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=3", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        con1.Open()
        Dim comando2 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=3", con1)
        comando2.ExecuteReader()
        con1.Close()
        Return Numero_Remito
    End Function

    Public Sub actualizar_remito(ByVal nremito As Decimal, ByVal fecha_remito As Date, ByVal estado As Integer, ByVal medidor As String, ByVal usr As String, proveedor As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        Try
            con1.Open()
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set NREMITO_113=@D1,FREMITO_113=@D2, ESTADO_113=@D3, USER_REM_113=@D4, PROVE_113=@D5 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", nremito))
            comando1.Parameters.Add(New SqlParameter("D2", fecha_remito))
            comando1.Parameters.Add(New SqlParameter("D3", estado))
            comando1.Parameters.Add(New SqlParameter("D4", usr))
            comando1.Parameters.Add(New SqlParameter("D5", proveedor))
            comando1.Parameters.Add(New SqlParameter("E1", medidor))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub
    Public Sub actualizar_med_trans(ByVal estado As Integer, ByVal medidor As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        Try
            con1.Open()
            Dim comando1 As New SqlClient.SqlCommand("Update T_MED_DEVO_113 set ESTADO_113=@D1 WHERE NSERI_113=@E1", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", estado))
            comando1.Parameters.Add(New SqlParameter("E1", medidor))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub
    Public Function Obtener_Numero_lote() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=5", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        con1.Open()
        Dim comando2 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=5", con1)
        comando2.ExecuteReader()
        con1.Close()
        Return Numero_Remito
    End Function
    Public Function Obtener_Numero_lote_temp() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=7", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        con1.Open()
        Dim comando2 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=7", con1)
        comando2.ExecuteReader()
        con1.Close()
        Return Numero_Remito
    End Function
    Public Function Obtener_Numero_Mov() As Decimal
        'lee el ultimo numero de remito
        Dim Numero_Remito As Decimal
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("select NUMERO From NUMERACION where C_NUM=6", con1)
        'creo un lector
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        If lector1.Read.ToString() Then
            Numero_Remito = CDec(lector1.GetValue(0))
        End If
        con1.Close()
        con1.Open()
        Dim comando2 As New SqlClient.SqlCommand("Update NUMERACION set NUMERO = NUMERO+1 WHERE C_NUM=6", con1)
        comando2.ExecuteReader()
        con1.Close()
        Return Numero_Remito
    End Function
    Public Sub GRABAR_TRANS(ByVal NSERIE As String, NUMEROMOV As Decimal, FECHA As Date, USERM As String, TIPO As Integer)
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Try
            con1.Open()
            'creo el comando para pasarle los parametros
            Dim comando1 As New SqlClient.SqlCommand("insert T_MOV_MED_DEVO_114(NSERI_114,NMOVI_114,FMOVI_114,USERM_114,TMOV_114) VALUES (@D1,@D2,@D3,@D4,@D5)", con1)
            'creo el lector de parametros
            comando1.Parameters.Add(New SqlParameter("D1", NSERIE))
            comando1.Parameters.Add(New SqlParameter("D2", NUMEROMOV))
            comando1.Parameters.Add(New SqlParameter("D3", FECHA))
            comando1.Parameters.Add(New SqlParameter("D4", USERM))
            comando1.Parameters.Add(New SqlParameter("D5", TIPO))
            comando1.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            con1.Close()
        End Try
    End Sub
End Class
