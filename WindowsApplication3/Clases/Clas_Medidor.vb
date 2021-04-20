Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.SqlTypes


Public Class Clas_Medidor
    Public Function Es_Serializado(ByVal _CodMate As String) As Boolean
        Dim _resp As Boolean = False
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando1 As New SqlClient.SqlCommand("select DESC_002 FROM M_MATE_002 WHERE CMATE_002=@D1 AND SERI_002=1 ", cnn4)
        comando1.Parameters.Add(New SqlParameter("D1", _CodMate))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            _resp = True
        End If
        cnn4.Close()
        Return _resp
    End Function
    Public Sub Grabar_Med_Sin_Asignar(ByVal depo As String, ByVal mate As String, ByVal cant As Decimal, ByVal nremi As Decimal, ByVal fremi As Date, ByVal npeti As Decimal)
        Dim cnn1 As SqlConnection = New SqlConnection(conexion)
        cnn1.Open()
        Dim comando1 As New SqlClient.SqlCommand("select CDEPO_109 from T_MED_SA_109 WHERE CMATE_109=@MATE AND CDEPO_109=@DEPO", cnn1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("MATE", mate))
        comando1.Parameters.Add(New SqlParameter("DEPO", depo))
        comando1.ExecuteNonQuery()
        'genero un lector
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            Dim con3 As SqlConnection = New SqlConnection(conexion)
            con3.Open()
            'si leeo el lector incremento el stock
            Dim comando2 As New SqlClient.SqlCommand("Update T_MED_SA_109 set CANT_109=CANT_109+@Cant WHERE CDEPO_109=@DEPO AND CMATE_109=@MATE", con3)
            'creo el lector de parametros
            comando2.Parameters.Add(New SqlParameter("DEPO", depo))
            comando2.Parameters.Add(New SqlParameter("MATE", mate))
            comando2.Parameters.Add(New SqlParameter("Cant", cant))
            comando2.ExecuteNonQuery()
            con3.Close()
        Else
            Dim cnn4 As SqlConnection = New SqlConnection(conexion)
            cnn4.Open()
            'Consulta SQL...
            Dim comando4 As New SqlClient.SqlCommand("INSERT INTO T_MED_SA_109 (CDEPO_109, CMATE_109, CANT_109,NREMI_109,F_REMI_109, NPETI_109) VALUES (@D1,@D2,@D3,@D4,@D5,@D6)", cnn4)
            comando4.Parameters.Add(New SqlParameter("D1", depo))
            comando4.Parameters.Add(New SqlParameter("D2", mate))
            comando4.Parameters.Add(New SqlParameter("D3", cant))
            comando4.Parameters.Add(New SqlParameter("D4", nremi))
            comando4.Parameters.Add(New SqlParameter("D5", fremi))
            comando4.Parameters.Add(New SqlParameter("D6", npeti))
            comando4.ExecuteNonQuery()
            cnn4.Close()
        End If
    End Sub
    Public Sub RESOLVER(ByVal serie As Decimal, ByVal usr As String, ByVal fecha As Date, ByVal cod_res As Integer)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102 = 1, USER_R_102 = @E1, F_RESU_102 = @E2, C_RESU_102 = @E3 WHERE NSERIE_102 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("E1", usr))
        comando1.Parameters.Add(New SqlParameter("E2", fecha))
        comando1.Parameters.Add(New SqlParameter("E3", cod_res))
        comando1.Parameters.Add(New SqlParameter("D1", serie))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub RESOLVER_final(ByVal serie As Decimal, ByVal usr As String, ByVal fecha As Date, ByVal cod_res As Integer)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102 = 5, USER_R_102 = @E1, F_RESU_102 = @E2, C_RESU_102 = @E3 WHERE NSERIE_102 = @D1", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("E1", usr))
        comando1.Parameters.Add(New SqlParameter("E2", fecha))
        comando1.Parameters.Add(New SqlParameter("E3", cod_res))
        comando1.Parameters.Add(New SqlParameter("D1", serie))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Function Serializados_sin_asignar(ByVal _CodMate As String, ByVal equipo As String) As Boolean
        Dim _resp As Boolean = False
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando1 As New SqlClient.SqlCommand("select CDEPO_109 FROM T_MED_SA_109 WHERE CMATE_109=@D1 AND CDEPO_109 = @D2 ", cnn4)
        comando1.Parameters.Add(New SqlParameter("D1", _CodMate))
        comando1.Parameters.Add(New SqlParameter("D2", equipo))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            _resp = True
        End If
        cnn4.Close()
        Return _resp
    End Function
    Public Function Ver_Disp_Medi(ByVal _CodMate As String, ByVal seri As Decimal, ByVal depo As String, ByVal EST As Integer) As Boolean
        Dim _resp As Boolean = False
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando1 As New SqlClient.SqlCommand("select NSERIE_102 FROM T_MEDI_102 WHERE CMATE_102=@D1 AND NSERIE_102 = @D2 AND ESTADO_102=@D3 AND CALMA_102 = @D4 and F_UTIL_102 IS NULL", cnn4)
        comando1.Parameters.Add(New SqlParameter("D1", _CodMate))
        comando1.Parameters.Add(New SqlParameter("D2", seri))
        comando1.Parameters.Add(New SqlParameter("D3", EST))
        comando1.Parameters.Add(New SqlParameter("D4", depo))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            _resp = True
        End If
        cnn4.Close()
        Return _resp
    End Function
    Public Sub MODIFICAR_MEDIDOR(ByVal SERIE As Decimal, ByVal Cod_Mat As String, ByVal ALMACEN As String, ByVal FINFO As Date, ByVal USER As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        Dim FR As Date = DateAdd(DateInterval.Day, +30, FINFO)
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set CALMA_102=@D1, F_INFO_102=@D3, USER_102=@D6, FREV_102=@FR WHERE CMATE_102=@E1 AND NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", ALMACEN))
        comando1.Parameters.Add(New SqlParameter("D3", FINFO))
        comando1.Parameters.Add(New SqlParameter("D6", USER))
        comando1.Parameters.Add(New SqlParameter("FR", FR))
        comando1.Parameters.Add(New SqlParameter("E1", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub MODIFICAR_MED_SA(ByVal MATE As String, ByVal ALMACEN As String, ByVal CANTIDAD As Decimal)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MED_SA_109 set CANT_109=@D3 WHERE CMATE_109=@D1 AND CDEPO_109=@D2 ", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", MATE))
        comando1.Parameters.Add(New SqlParameter("D2", ALMACEN))
        comando1.Parameters.Add(New SqlParameter("D3", CANTIDAD))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub ELIMINAR_MED_SA(ByVal ALMACEN As String, ByVal CANT As Decimal)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("DELETE FROM T_MED_SA_109 WHERE CDEPO_109=@D1 AND CANT_109=@D2 ", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", ALMACEN))
        comando1.Parameters.Add(New SqlParameter("D2", CANT))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub Grabar_Mov_Medi(ByVal SERIE As Decimal, ByVal MATE As String, ByVal REMITO As Decimal, ByVal FREMI As Date, ByVal AENTR As String, ByVal ARECI As String)
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("INSERT INTO T_MOV_REMI_110 (NSERI_110,CMATE_110,REMITO_110,FREMI_110, ALMAE_110,ALMAR_110) VALUES (@D1,@D2,@D3,@D4,@D5,@D6)", cnn4)
        comando4.Parameters.Add(New SqlParameter("D1", SERIE))
        comando4.Parameters.Add(New SqlParameter("D2", MATE))
        comando4.Parameters.Add(New SqlParameter("D3", REMITO))
        comando4.Parameters.Add(New SqlParameter("D4", FREMI))
        comando4.Parameters.Add(New SqlParameter("D5", AENTR))
        comando4.Parameters.Add(New SqlParameter("D6", ARECI))
        comando4.ExecuteNonQuery()
        cnn4.Close()
    End Sub
    Public Sub MODIFICAR_MEDIDOR_UTILIZADO(ByVal SERIE As Decimal, ByVal Cod_Mat As String, ByVal ALMACEN As String, ByVal FUTIL As Date, ByVal USER As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set CALMA_102=@D1, F_UTIL_102=@D3, USER_102=@D6, ESTADO_102=2 WHERE CMATE_102=@E1 AND NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", ALMACEN))
        comando1.Parameters.Add(New SqlParameter("D3", FUTIL))
        comando1.Parameters.Add(New SqlParameter("D6", USER))
        comando1.Parameters.Add(New SqlParameter("E1", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub MODIFICAR_MEDIDOR_REVISADO(ByVal SERIE As Decimal)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        Dim FR As Date = DateAdd(DateInterval.Day, +30, Date.Today)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set FREV_102=@D1 WHERE NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", FR))

        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub MODIFICAR_MEDIDOR_ESTADO_0(ByVal SERIE As Decimal, ByVal Cod_Mat As String, ByVal FINFO As Date, ByVal USER As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102=0, F_INFO_102=@D1, USER_102=@D2 WHERE CMATE_102=@E1 AND NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", FINFO))
        comando1.Parameters.Add(New SqlParameter("D2", USER))
        comando1.Parameters.Add(New SqlParameter("E1", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub Grabar_TEMP_TRANS_MEDIDOR(ByVal SERIE As Decimal, ByVal MATE As String, ByVal REMITO As Decimal)
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando4 As New SqlClient.SqlCommand("INSERT INTO TEMP_TRANS_MED (NSERIE,CODMATE,N_REMI) VALUES (@D1,@D2,@D3)", cnn4)
        comando4.Parameters.Add(New SqlParameter("D1", SERIE))
        comando4.Parameters.Add(New SqlParameter("D2", MATE))
        comando4.Parameters.Add(New SqlParameter("D3", REMITO))
        comando4.ExecuteNonQuery()
        cnn4.Close()
    End Sub
    Public Sub MODIFICAR_MEDIDOR_ESTADO_1(ByVal SERIE As Decimal, ByVal Cod_Mat As String, ByVal ALMACEN As String, ByVal FINFO As Date, ByVal USER As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102=1, F_INFO_102=@D1, USER_102=@D2, CALMA_102= @D3 WHERE CMATE_102=@E1 AND NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", FINFO))
        comando1.Parameters.Add(New SqlParameter("D2", USER))
        comando1.Parameters.Add(New SqlParameter("D3", ALMACEN))
        comando1.Parameters.Add(New SqlParameter("E1", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub MODIFICAR_MEDIDOR_ESTADO_9(ByVal SERIE As Decimal, ByVal Cod_Mat As String, ByVal FINFO As Date, ByVal DEPOSITO As String, ByVal USER As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102=9, F_INFO_102=@D1, USER_102=@D2, CALMA_102=@D3 WHERE CMATE_102=@E1 AND NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", FINFO))
        comando1.Parameters.Add(New SqlParameter("D2", USER))
        comando1.Parameters.Add(New SqlParameter("D3", DEPOSITO))
        comando1.Parameters.Add(New SqlParameter("E1", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub MODIFICAR_MEDIDOR_ESTADO_4(ByVal SERIE As Decimal, ByVal Cod_Mat As String, ByVal FINFO As Date, ByVal USER As String, ByVal REMITO As Decimal)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102=4, F_INFO_102=@D1, USER_102=@D2, RDEVO_102 = @D3, FDEVO_102=@D4 WHERE CMATE_102=@E1 AND NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", FINFO))
        comando1.Parameters.Add(New SqlParameter("D2", USER))
        comando1.Parameters.Add(New SqlParameter("D3", REMITO))
        comando1.Parameters.Add(New SqlParameter("D4", Date.Now))
        comando1.Parameters.Add(New SqlParameter("E1", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Function Exite_Medi(ByVal seri As Decimal) As Boolean
        Dim _resp As Boolean = False
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando1 As New SqlClient.SqlCommand("select NSERIE_102 FROM T_MEDI_102 WHERE NSERIE_102 = @D1", cnn4)
        comando1.Parameters.Add(New SqlParameter("D1", seri))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            _resp = True
        End If
        cnn4.Close()
        Return _resp
    End Function
    Public Function SERIALIZADO(ByVal A As String, ByVal B As String) As Boolean
        Dim RES As Boolean = True
        If Es_Serializado(A) = True Then
            If Serializados_sin_asignar(A, B) = True Then
                RES = False
            End If
        End If
        Return RES
    End Function
    Public Sub Grabar_medidor(ByVal NSERIE As Decimal, ByVal CODMATE As String, ByVal ALMACEN As String, ByVal FECHA As Date, ByVal ESTADO As Integer, ByVal USUARIO As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("insert T_MEDI_102(NSERIE_102, CMATE_102, CALMA_102,F_ALTA_102,ESTADO_102, USER_102) VALUES (@D1,@D2,@D3,@D4,@D5,@D6)", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", NSERIE))
        comando1.Parameters.Add(New SqlParameter("D2", CODMATE))
        comando1.Parameters.Add(New SqlParameter("D3", ALMACEN))
        comando1.Parameters.Add(New SqlParameter("D4", FECHA))
        comando1.Parameters.Add(New SqlParameter("D5", ESTADO))
        comando1.Parameters.Add(New SqlParameter("D6", USUARIO))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub MODIFICAR_MEDIDOR_ESTADO_3(ByVal SERIE As Decimal, ByVal Cod_Mat As String, ByVal FINFO As Date, ByVal USER As String)
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102=3, F_INFO_102=@D1, USER_102=@D2 WHERE CMATE_102=@E1 AND NSERIE_102=@E2", con1)
        'creo el lector de parametros
        comando1.Parameters.Add(New SqlParameter("D1", FINFO))
        comando1.Parameters.Add(New SqlParameter("D2", USER))
        comando1.Parameters.Add(New SqlParameter("E1", Cod_Mat))
        comando1.Parameters.Add(New SqlParameter("E2", SERIE))
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Function Exite_Medi(ByVal seri As Decimal, ByVal DEPO As String, ByVal codmate As String) As Boolean
        Dim _resp As Boolean = False
        Dim cnn4 As SqlConnection = New SqlConnection(conexion)
        cnn4.Open()
        'Consulta SQL...
        Dim comando1 As New SqlClient.SqlCommand("select NSERIE_102 FROM T_MEDI_102 WHERE NSERIE_102 = @D1 and CALMA_102 = @D2 and CMATE_102 = @D3 ", cnn4)
        comando1.Parameters.Add(New SqlParameter("D1", seri))
        comando1.Parameters.Add(New SqlParameter("D2", DEPO))
        comando1.Parameters.Add(New SqlParameter("D3", codmate))
        comando1.ExecuteNonQuery()
        Dim lector1 As SqlDataReader = comando1.ExecuteReader
        'pregunto si encontro
        If lector1.Read.ToString Then
            _resp = True
        End If
        cnn4.Close()
        Return _resp
    End Function
    Public Sub ESTADO_5()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102 = 5 WHERE POLIZA_102 IS NOT NULL AND ESTADO_102 = 2 AND F_UTIL_102 IS NOT NULL", con1)
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub ESTADO_5_DE7()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102 = 5 WHERE POLIZA_102 IS NOT NULL AND ESTADO_102 = 7 AND F_UTIL_102 IS NOT NULL", con1)
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub
    Public Sub ESTADO_6()
        'creo la cadena de conexion
        Dim con1 As SqlConnection = New SqlConnection(conexion)
        'abro la cadena
        con1.Open()
        'creo el comando para pasarle los parametros
        Dim comando1 As New SqlClient.SqlCommand("Update T_MEDI_102 set ESTADO_102 = 6 WHERE POLIZA_102 IS NOT NULL AND ESTADO_102 = 1 AND F_UTIL_102 IS NULL and F_RESU_102 IS NULL", con1)
        'creo el lector de parametros
        comando1.ExecuteNonQuery()
        con1.Close()
    End Sub


End Class
